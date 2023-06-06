# IMPORTANT NOTE

This project was forked from SlevinthHeaven/irsdkSharp however that repo seems to be dead and I needed to update this stuff to support the latest iRacing updates.  These are the updates that I have made so far -
1. Added PushToTalk telemetry value (iRacing added this in season 2 of 2023).
2. Added SessionState enum (iRacing always had this - not sure why this wasn't in here already).

Below is the original readme -

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

### The basics
The base SDK is a convertion of the C++ SDK but converted to .Net and some modifications made to make it easier for you to use.

The SDK reads a memory mapped file `Local\\IRSDKMemMapFileName` which contains the following constituants:
 1. A Header
 1. A list of variables in the telemetry data
 1. Session Information in the form of a yaml file
 1. A list of telemetry data.

When this file is created or updated a second file is "pinged" to signify a data event. This file is `Local\\IRSDKDataValidEvent`.

### How does the SDK work?
When you initialise the SDK you have a couple of options
1. Empty constructor - no logging
1. Logger constructor - with logging
1. MemoryMappedViewAccessor constructor - This is used to read a static file and does not use the memory mapped file or valid data event file.

We will ignore the `MemoryMappedViewAccessor constructor` for now as this is not the important one people are after.

When you create an instance of `iRacingSDK` a looping task is created to wait for the valid data event and wait for the creation of the memory mapped file. Once this occurs you should receive a `true` for `IsConnected()`. As the SDK is running it's own loops and does not provide events we have to run our own.

### Basic use

```csharp
using irsdkSharp.Serialization;
using irsdkSharp.Serialization.Models.Session;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace irsdkSharp.ConsoleTest
{
    class Program
    {
        private static IRacingSDK sdk;
        private static IRacingSessionModel _session;
        private static int _DriverId;

        /// <summary>
        /// Gets the Id (CarIdx) of yourself (the driver running this application).
        /// </summary>
        public static int DriverId { get { return _DriverId; } }


        static void Main(string[] args)
        {
            sdk = new IRacingSDK();
            Task.Run(() => Loop());
            Console.ReadLine();
        }

        private static void Loop()
        {
            int lastUpdate = -1;

            while (true)
            {
                var currentlyConnected = sdk.IsConnected();
                
                // Check if we can find the sim
                if (currentlyConnected)
                {
                    // Parse out your own driver Id
                    if (DriverId == -1)
                    {
                        _DriverId = (int)sdk.GetData("PlayerCarIdx");
                    }

                    var data = sdk.GetSerializedData();

                    // Is the session info updated?
                    int newUpdate = sdk.Header.SessionInfoUpdate;
                    if (newUpdate != lastUpdate)
                    {
                        lastUpdate = newUpdate;
                        _session = sdk.GetSerializedSessionInfo();
                    }

                    Thread.Sleep(15);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
```



## Benchmarks
We are looking into two different serializations and as of this moment there is a variability to the results which means we may give both choices in the end. However, we are not quite sure right now. As you can see from the benchmarks below we have a costly first hit vs a costly reading system.

|                    Method |         Mean |      Error |     StdDev |  Gen 0 | Allocated |
|-------------------------- |-------------:|-----------:|-----------:|-------:|----------:|
|        SerializeDataModel | 31,027.74 ns | 484.841 ns | 453.521 ns | 1.7700 |   7,560 B |
|           AccessDataModel |     31.14 ns |   0.210 ns |   0.187 ns |      - |         - |
| AccessDataModelAirDensity |     53.06 ns |   0.774 ns |   0.686 ns |      - |         - |
|                      Data |    498.18 ns |   9.903 ns |  22.151 ns | 1.9464 |   8,144 B |
|                AccessData |    200.86 ns |   1.932 ns |   1.713 ns |      - |         - |
|      AccessDataAirDensity |  3,599.90 ns |  27.307 ns |  24.207 ns |      - |         - |

## Contributions
If you would like to contribute to the SDK please do the following:

1. Fork from the dev branch and commit all PRs into this branch
1. Try and keep the base irsdkSharp project clean, we are trying to keep serialization and calculations out of the base library. The reason around this is to keep the main library as close as possible to bare bones and allow the developer the choice on if they would like any of the serialization or calculation extras.

## Licence

MIT License

Copyright (c) 2020 Slevinth Heaven

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
