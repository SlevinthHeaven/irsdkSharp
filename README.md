# irsdkSharp

Originally created by Scott Przybylski http://members.iracing.com/jforum/posts/list/1474031.page

Was not on github, no nuget package and copy pasted into several sources of other iRacing programs on github.

We have modified and released this as a .netstandard project. nuget package soon to come.


## Build and Release
The build and release is done through Azure Pipelines and pushes to NuGet, the reasoning behind this is due to the code being signed by a code signing cert. Now although this is possible via Actions the current cert is a hardware token cert so I do need to be involved in it.


| **DEV**     |  |
| ----------- | ----------- |
| Build      | ![Build Status (dev)](https://dev.azure.com/LuckyNoS7evin/LuckyNoS7evin/_apis/build/status/irSdkSharp?branchName=dev) |
| Nuget irsdkSharp   | ![Nuget irsdkSharp (dev)](https://img.shields.io/nuget/vpre/irsdkSharp)  |
| Nuget irsdkSharp.Serialization   | ![Nuget irsdkSharp.Serialization (dev)](https://img.shields.io/nuget/vpre/irsdkSharp.Serialization)        |
| Nuget irsdkSharp.Calculation   | ![Nuget irsdkSharp.Calculation (dev)](https://img.shields.io/nuget/vpre/irsdkSharp.Calculation)        |


| **MAIN**     |  |
| ----------- | ----------- |
| Build      | ![Build Status](https://dev.azure.com/LuckyNoS7evin/LuckyNoS7evin/_apis/build/status/irSdkSharp?branchName=main)     |
| Nuget irsdkSharp   | ![Nuget irsdkSharp (dev)](https://img.shields.io/nuget/v/irsdkSharp)  |
| Nuget irsdkSharp.Serialization   | ![Nuget irsdkSharp.Serialization (dev)](https://img.shields.io/nuget/v/irsdkSharp.Serialization)        |
| Nuget irsdkSharp.Calculation   | ![Nuget irsdkSharp.Calculation (dev)](https://img.shields.io/nuget/v/irsdkSharp.Calculation)        |
