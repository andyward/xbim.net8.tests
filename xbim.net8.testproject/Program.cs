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

string ifcFilePath = @"C:\Users\AndyWard\Desktop\Demo Models\Dormitory-ARC.ifc";

using (FileStream fileStream = new FileStream(ifcFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
{
    var schemaVersion = Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3; //GetSchemaVersion(fileStream);
    IfcStore store = IfcStore.Open(fileStream, StorageType.Ifc, schemaVersion, XbimModelType.MemoryModel);

    logger.LogInformation("Found {cnt} products", store.Instances.OfType<IIfcProduct>().Count());
}