using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.CommonModel
{
    public class SysInfo
    {
        /// <summary>
        /// CPU个数
        /// </summary>
        public int CpuCount { get; set; }

        /// <summary>
        /// CPU占用率
        /// </summary>
        public double CpuLoad { get; set; }

        /// <summary>
        /// 可用内存
        /// </summary>
        public long MemoryAvailable { get; set; }

        /// <summary>
        /// 物理内存
        /// </summary>
        public long PhysicalMemory { get; set; }

        /// <summary>
        /// 内存占用率
        /// </summary>
        public double MemoryLoad
        {
            get
            {
                var res = (double)(MemoryAvailable / PhysicalMemory);
                var res2 = ((double)MemoryAvailable / (double)PhysicalMemory);
                return
                    Math.Round(res * 100, 3);

            }
        }
    }
}
