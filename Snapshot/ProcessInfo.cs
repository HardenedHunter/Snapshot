using System.Collections.Generic;

namespace Snapshot
{
    public class ProcessInfo
    {
        public string Name { get; set; }
        public uint TotalSize { get; set; }
        public List<ModuleInfo> Modules = new List<ModuleInfo>();
    }

    public class ModuleInfo
    {
        public string Name { get; set; }
        public uint Size { get; set; }
    }
}