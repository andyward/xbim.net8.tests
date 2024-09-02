using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xbim.Common.Configuration;
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.IO;

XbimServices.Current.ConfigureServices(s => s
    .AddLogging(l => l.AddConsole())
    .AddXbimToolkit()
    );

var logger = XbimServices.Current.CreateLogger<IfcStore>();
var diVersion = new Xbim.Common.XbimAssemblyInfo(typeof(ServiceDescriptor).Assembly);
logger.LogWarning("Using DI version {assmVersion} - File version {fileVersion}", diVersion.AssemblyVersion, diVersion.FileVersion);

string ifcFilePath = "https://raw.githubusercontent.com/xBimTeam/XbimEssentials/master/Tests/TestSourceFiles/SampleHouse4.ifc";

using var client = new HttpClient();
using var httpResult = await client.GetAsync(ifcFilePath);
using var ifcStream = await httpResult.Content.ReadAsStreamAsync();

IfcStore store = IfcStore.Open(ifcStream, StorageType.Ifc, XbimModelType.MemoryModel); // Infers IFC schema

logger.LogInformation("Found {cnt} products", store.Instances.OfType<IIfcProduct>().Count());
