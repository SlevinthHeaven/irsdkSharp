# irsdkSharp

Originally created by Scott Przybylski http://members.iracing.com/jforum/posts/list/1474031.page

Was not on GitHub, no nuget package and copy pasted into several sources of other iRacing programs on GitHub.

We have modified and released this as a .netstandard & net 5.0 project.

## Build and Release
The build and release is done through Azure Pipelines and pushes to NuGet, the reasoning behind this is due to the code being signed by a code signing cert. Now although this is possible via Actions the current cert is a hardware token cert so I do need to be involved in it.


| **DEV**     |  |
| ----------- | ----------- |
| Build      | <img src="https://dev.azure.com/LuckyNoS7evin/LuckyNoS7evin/_apis/build/status/irSdkSharp?branchName=dev"/>  |
| Nuget irsdkSharp   | ![Nuget irsdkSharp (dev)](https://img.shields.io/nuget/vpre/irsdkSharp)  |
| Nuget irsdkSharp.Serialization   | ![Nuget irsdkSharp.Serialization (dev)](https://img.shields.io/nuget/vpre/irsdkSharp.Serialization)        |
| Nuget irsdkSharp.Calculation   | ![Nuget irsdkSharp.Calculation (dev)](https://img.shields.io/nuget/vpre/irsdkSharp.Calculation)        |


| **MAIN**     |  |
| ----------- | ----------- |
| Build      |<img src="https://dev.azure.com/LuckyNoS7evin/LuckyNoS7evin/_apis/build/status/irSdkSharp?branchName=main"/>   |
| Nuget irsdkSharp   | ![Nuget irsdkSharp (dev)](https://img.shields.io/nuget/v/irsdkSharp)  |
| Nuget irsdkSharp.Serialization   | ![Nuget irsdkSharp.Serialization (dev)](https://img.shields.io/nuget/v/irsdkSharp.Serialization)        |
| Nuget irsdkSharp.Calculation   | ![Nuget irsdkSharp.Calculation (dev)](https://img.shields.io/nuget/v/irsdkSharp.Calculation)        |

## Usage and Docs
This is a TODO, we are working hard on this and other projects. Docs will come soon.

## Contributions
[Christopher Scott](https://github.com/christothes) - Added a major contribution to irskSharp however this was at this time not pulled into the library due to a difference in where processes took place. Work was done on this by LuckyNoS7evin with some of the contribution code being transferred into the library.