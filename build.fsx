#r "Source/Solutions/packages/FAKE/tools/FakeLib.dll" // include Fake lib
#r "Source/Solutions/packages/FAKE/FSharp.Data/lib/net40/FSharp.Data.dll" 
open Fake
open Fake.RestorePackageHelper
open Fake.Git
open System
open System.Linq
open System.Text.RegularExpressions
open FSharp.Data
open FSharp.Data.JsonExtensions
open FSharp.Data.HttpRequestHeaders

// https://github.com/krauthaufen/DevILSharp/blob/master/build.fsx

let versionRegex = Regex("(\d+).(\d+).(\d+)-*([a-z]+)*-*(\d+)*", RegexOptions.Compiled)
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

    member this.AsString() : string = 
        trace preString
        if String.IsNullOrEmpty(preString)  then
            if release then 
                sprintf "%d.%d.%d" major minor patch
            else 
                sprintf "%d.%d.%d+%d" major minor patch build
        else
            sprintf "%d.%d.%d-%s+%d" major minor patch preString build

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
            failwithf "Unable to resolve version tag"
            BuildVersion(0,0,0,0,"",false)

let getLatestTag repositoryDir =
    let _,msg,error = runGitCommand repositoryDir "describe --tag --abbrev=0"
    if error <> "" then failwithf "git describe failed: %s" error
    msg |> Seq.head

let getVersionFromGitTag =
    trace "Get version from Git tag"
    let gitVersionTag = getLatestTag "./"
    new BuildVersion("1.1.0-beta", 123, true)

// https://api.nuget.org/v3/registration1/bifrost/index.json
// https://fsharp.github.io/FSharp.Data/library/JsonProvider.html
let getLatestNuGetVersion =
    trace "Get latest NuGet version"

    let jsonAsString = Http.RequestString("https://api.nuget.org/v3/registration1/bifrost/index.json", headers = [ Accept HttpContentTypes.Json ])
    let json = JsonValue.Parse(jsonAsString)

    let items = json?items.AsArray().[0]?items.AsArray()
    let item = items.[items.Length-1]
    let catalogEntry = item?catalogEntry
    let version = (catalogEntry?version.AsString())
    
    new BuildVersion(version)


let versionFromGitTag = getVersionFromGitTag
let lastNuGetVersion = getLatestNuGetVersion

printfn "Git Version : %s" (versionFromGitTag.AsString())
printfn "Last NuGet version : %s" (lastNuGetVersion.AsString())


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

Run "All"