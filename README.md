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


## Benchmarks
We are looking into two different serializations and as of this moment there is a variability to the results which mean

|                    Method |         Mean |      Error |     StdDev |  Gen 0 | Allocated |
|-------------------------- |-------------:|-----------:|-----------:|-------:|----------:|
|        SerializeDataModel | 31,027.74 ns | 484.841 ns | 453.521 ns | 1.7700 |   7,560 B |
|           AccessDataModel |     31.14 ns |   0.210 ns |   0.187 ns |      - |         - |
| AccessDataModelAirDensity |     53.06 ns |   0.774 ns |   0.686 ns |      - |         - |
|                      Data |    498.18 ns |   9.903 ns |  22.151 ns | 1.9464 |   8,144 B |
|                AccessData |    200.86 ns |   1.932 ns |   1.713 ns |      - |         - |
|      AccessDataAirDensity |  3,599.90 ns |  27.307 ns |  24.207 ns |      - |         - |

## Contributions
[Christopher Scott](https://github.com/christothes) - Added a major contribution to irskSharp however this was at this time not pulled into the library due to a difference in where processes took place. Work was done on this by LuckyNoS7evin with some of the contribution code being transferred into the library.