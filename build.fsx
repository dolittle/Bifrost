#I "Source/Solutions/packages/FAKE/tools/"
#I "Source/Solutions/packages/FAKE/FSharp.Data/lib/net40"
#r "FakeLib.dll"
#r "FSharp.Data.dll" 
open Fake
open Fake.RestorePackageHelper
open Fake.Git
open System
open System.Diagnostics
open System.IO
open System.Linq
open System.Text
open System.Text.RegularExpressions
open FSharp.Data
open FSharp.Data.JsonExtensions
open FSharp.Data.HttpRequestHeaders
open Fake.FileHelper
open Fake.FileSystemHelper
open Fake.ProcessHelper
open Fake.MSBuildHelper
open AssemblyInfoFile

// https://github.com/krauthaufen/DevILSharp/blob/master/build.fsx
// http://blog.2mas.xyz/take-control-of-your-build-ci-and-deployment-with-fsharp-fake/

let isWindows = System.Environment.OSVersion.Platform = PlatformID.Win32NT
let appveyor = if String.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("APPVEYOR")) then false else true
let appveyor_job_id = System.Environment.GetEnvironmentVariable("APPVEYOR_JOB_ID")

let versionRegex = Regex("(\d+).(\d+).(\d+)-*([a-z]+)*[+-]*(\d+)*", RegexOptions.Compiled)
type BuildVersion(major:int, minor:int, patch: int, build:int, preReleaseString:string, release:bool) =
    let major = major
    let minor = minor
    let patch = patch
    let preReleaseString = preReleaseString

    member this.Major with get() = major
    member this.Minor with get() = minor
    member this.Patch with get() = patch
    member this.Build with get() = build
    member this.PreReleaseString with get() = preReleaseString

    member this.AsString() : string = 
        if String.IsNullOrEmpty(preReleaseString)  then
            if release then 
                sprintf "%d.%d.%d" major minor patch
            else 
                sprintf "%d.%d.%d-%d" major minor patch build
        else
            sprintf "%d.%d.%d-%s-%d" major minor patch preReleaseString build

    member this.IsPreRelease with get() : bool = preReleaseString.Length > 0

    member this.DoesMajorMinorPatchMatch(other:BuildVersion) =
        other.Major = major && other.Minor = minor && other.Patch = patch

    new (versionAsString:string) =
        BuildVersion(versionAsString,0,false)

    new (versionAsString:string, build:int, release:bool) =
        let versionResult = versionRegex.Match versionAsString
        if versionResult.Success then
            let major = versionResult.Groups.[1].Value |> int
            let minor = versionResult.Groups.[2].Value |> int
            let patch = versionResult.Groups.[3].Value |> int
            let build = if versionResult.Groups.Count = 6 && versionResult.Groups.[5].Value.Length > 0 then versionResult.Groups.[5].Value |> int else build

            if versionResult.Groups.Count >= 5 then
                BuildVersion(major,minor,patch,build,versionResult.Groups.[4].Value,release)
            else
                BuildVersion(major,minor,patch,build,"",release)
        else 
            failwithf "Unable to resolve version from '%s'" versionAsString
            BuildVersion(0,0,0,0,"",false)

let spawnProcess (processName:string, arguments:string) =
    let startInfo = new System.Diagnostics.ProcessStartInfo(processName)
    startInfo.Arguments <- arguments
    startInfo.RedirectStandardInput <- true
    startInfo.RedirectStandardOutput <- true
    startInfo.RedirectStandardError <- true
    startInfo.UseShellExecute <- false
    startInfo.CreateNoWindow <- true
    startInfo.StandardOutputEncoding = Encoding.Unicode
    startInfo.StandardErrorEncoding = Encoding.Unicode

    let result = new StringBuilder()

    let resultHandler (_sender:obj) (args:DataReceivedEventArgs) = result.AppendLine args.Data |> ignore
    let outputHandler (_sender:obj) (args:DataReceivedEventArgs) = Console.WriteLine args.Data

    use proc = new System.Diagnostics.Process(StartInfo = startInfo)
    proc.EnableRaisingEvents <- true
    
    proc.OutputDataReceived.AddHandler(DataReceivedEventHandler (resultHandler))
    proc.ErrorDataReceived.AddHandler(DataReceivedEventHandler (resultHandler))

    proc.OutputDataReceived.AddHandler(DataReceivedEventHandler (outputHandler))
    proc.ErrorDataReceived.AddHandler(DataReceivedEventHandler (outputHandler))
    
    proc.Start() |> ignore
    proc.BeginOutputReadLine()
    proc.BeginErrorReadLine()
    proc.WaitForExit()
    if proc.ExitCode <> 0 then 
        failwith ("Problems spawning ("+processName+") with arguments ("+arguments+"): \r\n" +  proc.StandardError.ReadToEnd())

    proc.Close()
    
    result.ToString()

let performGitCommand arguments:string =
    spawnProcess("git", arguments)

let gitVersion repositoryDir = 
    let arguments = sprintf "%s /output json /showvariable SemVer" repositoryDir
    let gitVersionExecutable = "Source/Solutions/packages/GitVersion.CommandLine/tools/GitVersion.exe"
    let processName = if isWindows then gitVersionExecutable else "mono"
    let fullArguments = if isWindows then arguments else sprintf "%s %s" gitVersionExecutable arguments

    spawnProcess(processName, fullArguments)

let getCurrentBranch =
    performGitCommand("rev-parse --abbrev-ref HEAD").Trim()

let getLatestTag repositoryDir =
    //let commitSha = performGitCommand "rev-list --tags --max-count=1"
    performGitCommand (sprintf "describe --tag --abbrev=0")
    
let getVersionFromGitTag(buildNumber:int) =
    trace "Get version from Git tag"
    if appveyor then
        let gitVersionTag = gitVersion "./"
        tracef "Git tag version : %s" gitVersionTag
        new BuildVersion(gitVersionTag, buildNumber, true)
    else 
        new BuildVersion("1.0.0", 0, false)

let getLatestNuGetVersion =
    trace "Get latest NuGet version"

    let jsonAsString = Http.RequestString("https://api.nuget.org/v3/registration1/bifrost/index.json", headers = [ Accept HttpContentTypes.Json ])
    let json = JsonValue.Parse(jsonAsString)

    let items = json?items.AsArray().[0]?items.AsArray()
    let item = items.[items.Length-1]
    let catalogEntry = item?catalogEntry
    let version = (catalogEntry?version.AsString())
    
    new BuildVersion(version)
    
let updateProjectJsonFile(file:FileInfo, version:BuildVersion) =
    tracef "Update version and dependency versions for '%s'" file.FullName
    let json = JsonValue.Load file.FullName
    
    let rec fixVersion json =
        match json with
        | JsonValue.String _ | JsonValue.Boolean _ | JsonValue.Float _ | JsonValue.Number _ | JsonValue.Null -> json
        | JsonValue.Record properties -> 
            properties 
            |> Array.map (fun (key, value) -> key,
                if key.StartsWith("Bifrost") || key.Equals("version") then 
                    (version.AsString()) |> JsonValue.String
                else
                    fixVersion value
                )
            |> JsonValue.Record
        | JsonValue.Array array ->
            array
            |> Array.map fixVersion
            |> JsonValue.Array

    let fixedJson = fixVersion json
    File.WriteAllText(file.FullName, sprintf "%O" fixedJson)

let updateVersionOnProjectFile(file:string, version:BuildVersion) =
    let projectFile = File.ReadAllText(file)
    let newVersionString = sprintf "<Version>%s</Version>" (version.AsString())
    let updatedProjectFile = projectFile.Replace("<Version>1.0.0</Version>", newVersionString)
    File.WriteAllText(file, updatedProjectFile)

let getMsBuildEnginePath() =
    let msbuildLocations = [|
        "c:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\MSBuild\\15.0\\Bin\\msbuild.exe";
        "c:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Professional\\MSBuild\\15.0\\Bin\\msbuild.exe";
        "c:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\msbuild.exe"
    |]

    let msbuild = Array.tryFind (fun f -> File.Exists f) msbuildLocations
    if msbuild.IsSome then 
        msbuild.Value
    else 
        ""

//*****************************************************************************
//* Globals
//*****************************************************************************

let company = "Dolittle"
let copyright = "(C) 2008 - 2017 Dolittle"
let trademark = ""

let solutionFile = "./Source/Solutions/Bifrost_All.sln"

let sourceDirectory = sprintf "%s/Source" __SOURCE_DIRECTORY__
let artifactsDirectory = sprintf "%s/artifacts" __SOURCE_DIRECTORY__
let nugetDirectory = sprintf "%s/nuget" artifactsDirectory

let msbuild = getMsBuildEnginePath()


let projectsDirectories = File.ReadAllLines "projects.txt" |> Array.map(fun f -> new DirectoryInfo(sprintf "./Source/%s" f))

let specDirectories = File.ReadAllLines "specs.txt" |> Array.map(fun f -> new DirectoryInfo(sprintf "./Source/%s" f))

let currentBranch = getCurrentBranch

// Versioning related
let envBuildNumber = System.Environment.GetEnvironmentVariable("APPVEYOR_BUILD_NUMBER")
let buildNumber = if String.IsNullOrWhiteSpace(envBuildNumber) then 0 else envBuildNumber |> int

let versionFromGitTag = getVersionFromGitTag buildNumber 
let lastNuGetVersion = getLatestNuGetVersion
let sameVersion = versionFromGitTag.DoesMajorMinorPatchMatch lastNuGetVersion
// Determine if it is a release build - check if the latest NuGet deployment is a release build matching version number or not.
let isReleaseBuild = versionFromGitTag.IsPreRelease
// sameVersion && (not versionFromGitTag.IsPreRelease && lastNuGetVersion.IsPreRelease)
System.Environment.SetEnvironmentVariable("RELEASE_BUILD",if isReleaseBuild then "true" else "false")

let buildVersion = BuildVersion(versionFromGitTag.Major, versionFromGitTag.Minor, versionFromGitTag.Patch, buildNumber, versionFromGitTag.PreReleaseString,isReleaseBuild)

// Package related
let nugetPath = "./Source/Solutions/.nuget/NuGet.exe"
let nugetUrl = "https://www.nuget.org/api/v2/package"
let mygetUrl = "https://www.myget.org/F/bifrost/api/v2/package"
let nugetKey = System.Environment.GetEnvironmentVariable("NUGET_KEY")
let mygetKey = System.Environment.GetEnvironmentVariable("MYGET_KEY")

// Documentation related
let documentationUser = System.Environment.GetEnvironmentVariable("DOCS_USER")
let documentationUserToken = System.Environment.GetEnvironmentVariable("DOCS_TOKEN")
let documentationSolutionFile = "Source/Solutions/Bifrost_Documentation.sln"

printfn "<----------------------- BUILD DETAILS ----------------------->"
printfn "Git Branch : %s" currentBranch
printfn "Git Version : %s" (versionFromGitTag.AsString())
printfn "Last NuGet version : %s" (lastNuGetVersion.AsString())
printfn "Last NuGet version - preRelease : %b" (lastNuGetVersion.IsPreRelease)
printfn "Build version : %s" (buildVersion.AsString())
printfn "Build version - preRelease : %b" (buildVersion.IsPreRelease)
printfn "Version Same : %b" sameVersion
printfn "Release Build : %b" isReleaseBuild
printfn "Documentation User : %s" documentationUser
printfn "MSBuild location : %s" msbuild
printfn "<----------------------- BUILD DETAILS ----------------------->"


//*****************************************************************************
//* Restore Packages
//*****************************************************************************
Target "RestorePackages" (fun _ ->
    trace "**** Restoring packages ****"

    let currentDir = Directory.GetCurrentDirectory()

    for directory in projectsDirectories.Concat(specDirectories) do
        tracef "Restoring packages for %s" directory.FullName
        Directory.SetCurrentDirectory directory.FullName
        let allArgs = sprintf "restore"

        spawnProcess("dotnet", allArgs) |> ignore

    Directory.SetCurrentDirectory(currentDir)
    trace "**** Restoring packages DONE ****"
)


//*****************************************************************************
//* Update project json files with correct version
//*****************************************************************************
Target "UpdateVersionOnBuildServer" (fun _ ->
    if( appveyor ) then
        tracef "Updating build version for AppVeyor to %s" (buildVersion.AsString())
        let allArgs = sprintf "UpdateBuild -Version \"%s\"" (buildVersion.AsString())
        spawnProcess("appveyor", allArgs) |> ignore
)


//*****************************************************************************
//* Update Assembly Info files with correct information
//*****************************************************************************
Target "UpdateAssemblyInfoFiles" (fun _ ->
    let version = sprintf "%d.%d.%d.%d" buildVersion.Major buildVersion.Minor buildVersion.Patch buildVersion.Build
    CreateCSharpAssemblyInfoWithConfig "Source/Common/CommonAssemblyInfo.cs" [
        Attribute.Company company
        Attribute.Copyright copyright
        Attribute.Trademark trademark
        Attribute.Version version
        Attribute.FileVersion version 
    ] <| AssemblyInfoFileConfig(false)
)

//*****************************************************************************
//* Update the version number on the project files
//*****************************************************************************
Target "UpdateVersionOnProjectFiles" (fun _ ->
    trace "**** Fixing version number for project files ****"
    for directory in projectsDirectories do
        let projectFiles = Directory.GetFiles(directory.FullName,"*.csproj")
        let file = projectFiles.[0]
        tracef "Fixing %s" file
        updateVersionOnProjectFile(file, buildVersion)
    trace "**** Fixing version number for project files - Done ****"
)    

//*****************************************************************************
//* Build
//*****************************************************************************
Target "Build" (fun _ ->
    trace "**** Building ****"
    for directory in projectsDirectories do
        tracef "Building %s" directory.FullName
        let allArgs = sprintf "build %s %s" directory.FullName (if isWindows then "" else "-f netstandard1.6")
        spawnProcess("dotnet", allArgs)

    trace "**** Building Done ****"
)

//*****************************************************************************
//* Run .NET CLI Test
//*****************************************************************************
Target "DotNetTest" (fun _ ->
    trace "**** Running Specs ****"

    let currentDir = Directory.GetCurrentDirectory()

    for directory in specDirectories do
        tracef "Running Specs for %s" directory.FullName
        Directory.SetCurrentDirectory directory.FullName
        let allArgs = sprintf "test %s %s" (if isWindows then "" else "-f netcoreapp1.1") (if appveyor then "\"--logger:trx;LogFileName=results.trx\"" else "")

        spawnProcess("dotnet", allArgs)

        let resultsFile = "./TestResults/results.trx"
        if appveyor && File.Exists(resultsFile) then
            let webClient = new System.Net.WebClient()
            let url = sprintf "https://ci.appveyor.com/api/testresults/mstest/%s" appveyor_job_id
            tracef "Posting results to %s" url
            webClient.UploadFile(url, resultsFile) |> ignore

    Directory.SetCurrentDirectory(currentDir)
    trace "**** Running Specs DONE ****"
)


//*****************************************************************************
//* Package all projects for NuGet
//*****************************************************************************
Target "PackageForNuGet" (fun _ ->
    for directory in projectsDirectories do
        let allArgs = sprintf "pack --no-build %s --output %s" directory.FullName nugetDirectory
        spawnProcess("dotnet", allArgs)
)

//*****************************************************************************
//* Run JavaScript Specifications
//*****************************************************************************
Target "JavaScriptSpecs" (fun _ ->
    if Directory.Exists("TestResults") = false then Directory.CreateDirectory("TestResults") |> ignore
    let allArgs = sprintf "Forseti.yaml ../TestResults/forseti.testresults.trx BUILD-CI"
    let errorCode = ProcessHelper.Shell.Exec("Tools/Forseti/Forseti.Output.exe", args=allArgs, dir="Source")
    if errorCode <> 0 then failwith "Running JavaScript Specifications failed"
)

//*****************************************************************************
//* Generate and publish documentation to site
//*****************************************************************************
Target "GenerateAndPublishDocumentation" (fun _ ->
    if String.IsNullOrEmpty(documentationUser) then
        trace "Skipping building and publishing documentation - user not set"
    else
        trace "**** Generating Documentation ****"

        let currentDir = Directory.GetCurrentDirectory()
        tracef "Current directory is : %s" currentDir
        Directory.SetCurrentDirectory "./Source/Documentation"

        spawnProcess("dotnet", "restore")
        spawnProcess("dotnet", "build")
        Directory.SetCurrentDirectory(currentDir)

        trace "Clone site repository"

        let siteDir = "dolittle.github.io"
        spawnProcess("git", "clone https://github.com/dolittle/dolittle.github.io.git")

        trace "Copy all the content from the generated site"
        FileHelper.CopyDir "dolittle.github.io/bifrost" "Source/Documentation/_site" (fun f -> true)

        Directory.SetCurrentDirectory(siteDir)

        trace "Push back to Git repository"
        spawnProcess("git" , "add .") |> ignore
        spawnProcess("git" , "config --global user.name \"Bifrost Documentation Account\"") |> ignore
        spawnProcess("git" , "config --global user.email \"bifrost@dolittle.com\"") |> ignore
        spawnProcess("git" , "commit -m \"<-- Autogenerated : documentation updated -->\"") |> ignore
        let remoteUrl = sprintf "remote set-url origin https://%s:%s@github.com/dolittle/dolittle.github.io.git" documentationUser documentationUserToken
        spawnProcess("git" , remoteUrl) |> ignore
        // if( ProcessHelper.Shell.Exec("git" , args="push 2>nul") <> 0) then failwith "Couldn't push documentation to repository"

        spawnProcess("git", "push")

        trace "--- Delete content of site dir ---"
        FileHelper.DeleteDir siteDir

        Directory.SetCurrentDirectory(currentDir)

        trace "**** Generating Documentation DONE ****"
)


//*****************************************************************************
//* Deploy to NuGet if release mode
//*****************************************************************************
Target "DeployNugetPackages" (fun _ ->
    let key = if( isReleaseBuild && String.IsNullOrEmpty(nugetKey) = false ) then nugetKey else mygetKey
    let source = if( isReleaseBuild && String.IsNullOrEmpty(nugetKey) = false ) then nugetUrl else mygetUrl

    if( String.IsNullOrEmpty(key) = false ) then
        let packages = !! ("artifacts/nuget/*.nupkg")
                        |> Seq.toArray
                        
        for package in packages do
            let allArgs = sprintf "push %s %s -Source %s" package key source
            spawnProcess(nugetPath, allArgs) |> ignore
    else
        trace "Not deploying to NuGet - no key set"
)

//*****************************************************************************
//* Deploy to NuGet if release mode
//*****************************************************************************
Target "PackageSamples" (fun _ ->
    let sampleProjects = ["Bifrost.QuickStart"]
    // "Source/Bifrost.Default/Bifrost.Default.nuspec"

    trace "*** Package Sample Projects ***"
    
    for sampleProject in sampleProjects do
        let specFile = sprintf "Source/%s/%s.nuspec" sampleProject sampleProject
        let projFile = sprintf "Source/%s/%s.csproj" sampleProject sampleProject

        tracef "Build %s" projFile
        spawnProcess(msbuild, projFile) |> ignore

        tracef "Packaging %s %s" specFile
        let allArgs = sprintf "pack %s -Version %s -OutputDirectory %s" specFile (buildVersion.AsString()) nugetDirectory
        spawnProcess(nugetPath, allArgs) |> ignore

    trace "*** Package Sample Projects DONE ***"
)

// ******** Pre Info 
// Get Build Number from BuildServer
// Get Version from Git Tag
// Determine if it is a release build - check if the latest NuGet deployment is a release build matching version number or not.
// If tag is not a release tag - Append build number

// ******** BUILD:
// Restore packages
// Create Assembly Version from Tag + Build Number -> Update Assembly Info
// Build
// Run MSpec Specs
// Run JavaScript Specs
//
// If daily or alpha or beta - create nuget packages
//     If daily and not alpha or beta -> Deploy to MyGet
//     Else deploy to NuGet
// Note: Deploy package only if it is a release build or build parameter saying it should publish package
//
// Clone Documentation Repository
// DocFX for documentation -> Into Documentation repository
// Push changes to Documentation Repository

// Build pipeline
Target "BuildRelease" DoNothing
"UpdateVersionOnBuildServer" ==> "BuildRelease"
"RestorePackages" ==> "BuildRelease"
"Build" ==> "BuildRelease"

// Package pipeline
Target "Package" DoNothing
"UpdateAssemblyInfoFiles" ==> "Package"
"UpdateVersionOnProjectFiles" ==> "Package"
"PackageForNuGet" ==> "Package"

// Deployment pipeline
Target "Deploy" DoNothing
"DeployNugetPackages" ==> "Deploy"

Target "PackageAndDeploy" DoNothing
"GenerateAndPublishDocumentation" ==> "PackageAndDeploy"
"Package" ==> "PackageAndDeploy"
"PackageSamples" ==> "PackageAndDeploy"
"Deploy" ==> "PackageAndDeploy"

Target "All" DoNothing
"BuildRelease" ==> "All"
"DotNetTest" ==> "All"
"JavaScriptSpecs" ==> "All"
"PackageAndDeploy" =?> ("All",  currentBranch.Equals("master") or currentBranch.Equals("HEAD"))

Target "Travis" DoNothing
"BuildRelease" ==> "Travis"
"DotNetTest" ==> "Travis"

RunTargetOrDefault "All"