// See https://aka.ms/new-console-template for more information
using Xbim.Ifc;
using Xbim.Ifc4.Interfaces;
using Xbim.IO;

Console.WriteLine("Hello, World!");


string ifcFilePath = @"C:\Users\AndyWard\Desktop\Demo Models\Dormitory-ARC.ifc";
using (FileStream fileStream = new FileStream(ifcFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
{
    var schemaVersion = Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3; //GetSchemaVersion(fileStream);
    IfcStore store = IfcStore.Open(fileStream, StorageType.Ifc, schemaVersion, XbimModelType.MemoryModel);

    Console.WriteLine(store.Instances.OfType<IIfcWall>().Count());
}