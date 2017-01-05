#r "Source/Solutions/packages/FAKE/tools/FakeLib.dll" // include Fake lib
open Fake
open Fake.RestorePackageHelper
open Fake.Git
open System
open System.Linq
open System.Text.RegularExpressions

// https://github.com/krauthaufen/DevILSharp/blob/master/build.fsx

let versionRegex = Regex("(\d+).(\d+).(\d+)-*([a-z]+)*", RegexOptions.Compiled)
type BuildVersion(major:int, minor:int, patch: int, build:int, preString:string, release:bool) =
    let major = major
    let minor = minor
    let patch = patch
    let preString = preString

    member this.Major with get() = major
    member this.Minor with get() = minor
    member this.Patch with get() = patch
    member this.Build with get() = build
    member this.PreString with get() = preString

    member this.AsString = 
        if String.IsNullOrEmpty(preString)  then
            if release then 
                sprintf "%d.%d.%d" major minor patch
            else 
                sprintf "%d.%d.%d+%d" major minor patch build
        else
            sprintf "%d.%d.%d-%s+%d" major minor patch preString build


    new (versionAsString:string, build:int, release:bool) =
        let versionResult = versionRegex.Match versionAsString
        if versionResult.Success then
            let major = versionResult.Groups.[1].Value |> int
            let minor = versionResult.Groups.[2].Value |> int
            let patch = versionResult.Groups.[3].Value |> int

            if versionResult.Groups.Count = 5 then
                BuildVersion(major,minor,patch,build,versionResult.Groups.[4].Value,release)
            else
                BuildVersion(major,minor,patch,build,"",release)
        else 
            traceError "Unable to resolve version tag"
            BuildVersion(0,0,0,0,"",false)



let getLastTag repositoryDir =
    let _,msg,error = runGitCommand repositoryDir "describe --tag"
    if error <> "" then failwithf "git describe failed: %s" error
    msg |> Seq.head

Target "GetVersionFromGitTag" (fun _ ->
    let repositoryRoot = "./"

    trace "Get version from Git tag"

    let gitVersionTag = "10.11.0"

    let buildVersion = new BuildVersion(gitVersionTag, 123, true)
    trace buildVersion.AsString  


    trace "Version is: "
    trace gitVersionTag
)



Target "RestorePackages" (fun _ ->
    trace "Restore packages"
)



Target "Test" (fun _ ->
    trace "Testing stuff..."
)

Target "Deploy" (fun _ ->
    trace "Heavy deploy action"
)



// Get Version from Git Tag

// Determine if it is a release build - check if the latest NuGet deployment is a release build matching version number or not.

// Restore packages

// If tag is not a release tag - Append build number

// Update Project.JSON files with correct version number

// Create Assembly Version from Tag + Build Number

// Update Assembly Info

// Build

// Run MSpec Specs

// If daily or alpha or beta - create nuget packages

//     If daily and not alpha or beta -> Deploy to MyGet

//     Else deploy to NuGet

// Clone Documentation Repository

// DocFX for documentation -> Into Documentation repository

// Push changes to Documentation Repository


"Test" // define the dependencies
   ==> "Deploy"

Target "All" DoNothing
"RestorePackages" ==> "All"
"GetVersionFromGitTag" ==> "All"

Run "All"