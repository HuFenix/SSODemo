using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.CommonModel;

namespace Utils.Extentions
{
    public class SystemInfo
    {
        public SysInfo GetCurrentSystemInfo()
        {
            var sys = new SystemInfoHelper();
            var model = new SysInfo();
            model.CpuCount = sys.ProcessorCount;
            model.CpuLoad = Math.Round((double)sys.CpuLoad,2);
            model.MemoryAvailable = sys.MemoryAvailable;
            model.PhysicalMemory = sys.PhysicalMemory;

            return model;
        }
    }
}
