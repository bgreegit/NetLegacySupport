﻿#I @"packages/FAKE/tools"
#I @"packages/FAKE.BuildLib/lib/net451"
#r "FakeLib.dll"
#r "BuildLib.dll"

open Fake
open BuildLib

let solution = 
    initSolution
        "./NetLegacySupport.sln" "Release" 
        [ { emptyProject with Name="NetLegacySupport.Action";
                              Folder="./core/Action";
                              Dependencies=[] };
          { emptyProject with Name="NetLegacySupport.ConcurrentDictionary";
                              Folder="./core/ConcurrentDictionary";
                              Dependencies=[("NetLegacySupport.Action", "")] };
          { emptyProject with Name="NetLegacySupport.Tuple";
                              Folder="./core/Tuple";
                              Dependencies=[] } ]

Target "Clean" <| fun _ -> cleanBin

Target "AssemblyInfo" <| fun _ -> generateAssemblyInfo solution

Target "Restore" <| fun _ -> restoreNugetPackages solution

Target "Build" <| fun _ -> buildSolution solution

Target "Test" <| fun _ -> testSolution solution

Target "Cover" <| fun _ -> coverSolution solution

Target "PackNuget" <| fun _ -> createNugetPackages solution

Target "PackUnity" <| fun _ -> packUnityPackage "./core/UnityPackage/NetLegacySupport.unitypackage.json"

Target "Pack" <| fun _ -> ()

Target "PublishNuget" <| fun _ -> publishNugetPackages solution

Target "PublishUnity" <| fun _ -> ()

Target "Publish" <| fun _ -> ()

Target "CI" <| fun _ -> ()

Target "Help" <| fun _ -> 
    showUsage solution (fun _ -> None)

"Clean"
  ==> "AssemblyInfo"
  ==> "Restore"
  ==> "Build"
  ==> "Test"

"Build" ==> "Cover"

let isPublishOnly = getBuildParam "publishonly"

"Build" ==> "PackNuget" =?> ("PublishNuget", isPublishOnly = "")
"Build" ==> "PackUnity" =?> ("PublishUnity", isPublishOnly = "")
"PackNuget" ==> "Pack"
"PackUnity" ==> "Pack"
"PublishNuget" ==> "Publish"
"PublishUnity" ==> "Publish"

"Test" ==> "CI"
"Cover" ==> "CI"
"Publish" ==> "CI"

RunTargetOrDefault "Help"
