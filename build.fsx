#I "Source/Solutions/packages/FAKE/tools/"
#I "Source/Solutions/packages/FAKE/FSharp.Data/lib/net40"
#r "FakeLib.dll"
#r "FSharp.Data.dll" 
open Fake
open Fake.RestorePackageHelper
open Fake.Git
open System
open System.IO
open System.Linq
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
            failwithf "Unable to resolve version"
            BuildVersion(0,0,0,0,"",false)

let getLatestTag repositoryDir =
    let _,msg,error = runGitCommand repositoryDir "describe --tag --abbrev=0"
    if error <> "" then failwithf "git describe failed: %s" error
    msg |> Seq.head

let getVersionFromGitTag =
    trace "Get version from Git tag"
    let gitVersionTag = getLatestTag "./"
    new BuildVersion("1.1.0-beta", 123, true)

let getLatestNuGetVersion =
    trace "Get latest NuGet version"

    let jsonAsString = Http.RequestString("https://api.nuget.org/v3/registration1/bifrost/index.json", headers = [ Accept HttpContentTypes.Json ])
    let json = JsonValue.Parse(jsonAsString)

    let items = json?items.AsArray().[0]?items.AsArray()
    let item = items.[items.Length-1]
    let catalogEntry = item?catalogEntry
    let version = (catalogEntry?version.AsString())
    
    new BuildVersion(version)

//*****************************************************************************
//* Globals
//*****************************************************************************

let company = "Dolittle"
let copyright = "(C) 2008 - 2017 Dolittle"
let trademark = ""

let sourceDirectory = sprintf "%s/Source" __SOURCE_DIRECTORY__
let artifactsDirectory = sprintf "%s/artifacts" __SOURCE_DIRECTORY__
let nugetDirectory = sprintf "%s/nuget" artifactsDirectory

let projectDirectories = DirectoryInfo(sourceDirectory).GetDirectories "Bifrost*" 
                        |> Array.filter(fun d -> d.Name.Contains("Spec") = false )

let projectJsonFiles = projectDirectories 
                        |> Array.map(fun d -> filesInDirMatching "project.json" d)
                        |> Array.concat

let specDirectories = DirectoryInfo(sourceDirectory).GetDirectories "Bifrost*" 
                        |> Array.filter(fun d -> d.Name.Contains("Spec") )

let specProjectJsonFiles = specDirectories 
                        |> Array.map(fun d -> filesInDirMatching "project.json" d)
                        |> Array.concat
                        

let appveyor = if String.IsNullOrWhiteSpace(System.Environment.GetEnvironmentVariable("APPVEYOR")) then false else true
let envBuildNumber = System.Environment.GetEnvironmentVariable("APPVEYOR_BUILD_NUMBER")
let buildNumber = if String.IsNullOrWhiteSpace(envBuildNumber) then 0 else envBuildNumber |> int

let versionFromGitTag = getVersionFromGitTag
let lastNuGetVersion = getLatestNuGetVersion
let sameVersion = versionFromGitTag.DoesMajorMinorPatchMatch lastNuGetVersion
// Determine if it is a release build - check if the latest NuGet deployment is a release build matching version number or not.
let isReleaseBuild = sameVersion && (not versionFromGitTag.IsPreRelease && lastNuGetVersion.IsPreRelease)
System.Environment.SetEnvironmentVariable("RELEASE_BUILD",if isReleaseBuild then "true" else "false")

let buildVersion = BuildVersion(versionFromGitTag.Major, versionFromGitTag.Minor, versionFromGitTag.Patch, buildNumber, versionFromGitTag.PreReleaseString,isReleaseBuild)
let solutionFile = "./Source/Solutions/Bifrost_All.sln"

let nugetPath = "./Source/Solutions/.nuget/NuGet.exe"
let nugetUrl = "https://www.nuget.org/api/v2/package"
let mygetUrl = "https://www.myget.org/F/bifrost/api/v2/package"
let nugetKey = System.Environment.GetEnvironmentVariable("NUGET_KEY")
let mygetKey = System.Environment.GetEnvironmentVariable("MYGET_KEY")

let documentationUser = System.Environment.GetEnvironmentVariable("DOCS_USER")
let documentationUserToken = System.Environment.GetEnvironmentVariable("DOCS_TOKEN")

printfn "<----------------------- BUILD DETAILS ----------------------->"
printfn "Git Version : %s" (versionFromGitTag.AsString())
printfn "Last NuGet version : %s" (lastNuGetVersion.AsString())
printfn "Build version : %s" (buildVersion.AsString())
printfn "Version Same : %b" sameVersion
printfn "Release Build : %b" isReleaseBuild
printfn "Documentation User : %s" documentationUser
printfn "<----------------------- BUILD DETAILS ----------------------->"



//*****************************************************************************
//* Restore Packages
//*****************************************************************************
Target "RestorePackages" (fun _ ->
    solutionFile
     |> RestoreMSSolutionPackages (fun p ->
         { p with
             OutputPath = "./Source/Solutions/packages"
             Retries = 4 })
)

//*****************************************************************************
//* Build
//*****************************************************************************
Target "Build" <| fun _ ->
    let buildMode = getBuildParamOrDefault "buildMode" "Release"
    let setParams defaults =
        { defaults with
            Verbosity = Some MSBuildVerbosity.Minimal
            Properties =
                [
                    "Optimize", "True"
                ]
        }

    build setParams solutionFile
        |> DoNothing


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
//* Update project json files with correct version
//*****************************************************************************
Target "UpdateVersionOnBuildServer" (fun _ ->
    if( appveyor ) then
        let allArgs = sprintf "UpdateBuild -Version \"%s\"" (buildVersion.AsString())
        ProcessHelper.Shell.Exec("appveyor", args=allArgs) |> ignore
)


//*****************************************************************************
//* Package all projects for NuGet
//*****************************************************************************
Target "PackageForNuGet" (fun _ ->
    for file in projectJsonFiles do
        let allArgs = sprintf "pack %s -OutputDirectory %s -Version %s -Symbols" file.FullName nugetDirectory (buildVersion.AsString())
        ProcessHelper.Shell.Exec(nugetPath, args=allArgs) |> ignore
)


//*****************************************************************************
//* Run MSpec Specifications
//*****************************************************************************
Target "MSpec" (fun _ -> 
    let specFiles = !! ("Source/**/*.Specs.dll")
                    |> Seq.toArray
                    |> String.concat " "

    let allArgs = sprintf "%s" specFiles
    let mspec = if appveyor then "mspec" else "Tools/MSpec/mspec-clr4"
    ProcessHelper.Shell.Exec(mspec, args=allArgs) |> ignore
)

//*****************************************************************************
//* Run JavaScript Specifications
//*****************************************************************************
Target "JavaScriptSpecs" (fun _ ->
    if Directory.Exists("TestResults") = false then Directory.CreateDirectory("TestResults") |> ignore
    let allArgs = sprintf "Forseti.yaml ../TestResults/forseti.testresults.trx BUILD-CI"
    let errorCode = ProcessHelper.Shell.Exec("Tools/Forseti/Forseti.Output.exe", args=allArgs, dir="Source")
    if errorCode <> 0 then failwith "Running JavaScript specs failed"
)

//*****************************************************************************
//* Generate and publish documentation to site
//*****************************************************************************
Target "GenerateAndPublishDocumentation" (fun _ ->
    if String.IsNullOrEmpty(documentationUser) then
        trace "Skipping building and publishing documentation - user not set"
    else
        let buildMode = getBuildParamOrDefault "buildMode" "Release"
        let setParams defaults =
            { defaults with
                Verbosity = Some MSBuildVerbosity.Minimal
                Properties =
                    [
                        "Optimize", "True"
                    ]
            }

        build setParams "Source\Documentation\Documentation.csproj"
            |> DoNothing

        let siteDir = "dolittle.github.io"
        ProcessHelper.Shell.Exec("git" , args="clone https://github.com/dolittle/dolittle.github.io.git") |> ignore
        FileHelper.CopyDir "dolittle.github.io/bifrost" "Source/Documentation/_site" (fun f -> true)

        ProcessHelper.Shell.Exec("git" , args="add .", dir=siteDir) |> ignore
        ProcessHelper.Shell.Exec("git" , args="config --global user.name \"Bifrost Documentation Account\"", dir=siteDir) |> ignore
        ProcessHelper.Shell.Exec("git" , args="config --global user.email \"bifrost@dolittle.com\"", dir=siteDir) |> ignore
        ProcessHelper.Shell.Exec("git" , args="commit -m \"<-- Autogenerated : documentation updated -->\"", dir=siteDir) |> ignore
        let remoteUrl = sprintf "remote set-url origin https://%s:%s@github.com/dolittle/dolittle.github.io.git" documentationUser documentationUserToken
        ProcessHelper.Shell.Exec("git" , args=remoteUrl, dir=siteDir) |> ignore
        ProcessHelper.Shell.Exec("git" , args="push", dir=siteDir) |> ignore
        
        FileHelper.DeleteDir siteDir
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
            ProcessHelper.Shell.Exec(nugetPath, args=allArgs) |> ignore
    else
        trace "Not deploying to NuGet - no key set"
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
"PackageForNuGet" ==> "Package"

// Specifications pipeline
Target "Specifications" DoNothing
"MSpec" ==> "Specifications"
"JavaScriptSpecs" ==> "Specifications"

// Deployment pipeline
Target "Deploy" DoNothing
"DeployNugetPackages" ==> "Deploy"


Target "All" DoNothing
"BuildRelease" ==> "All"
"Specifications" ==> "All"
"Package" ==> "All"
"GenerateAndPublishDocumentation" ==> "All"
"Deploy" ==> "All"



RunTargetOrDefault "All"