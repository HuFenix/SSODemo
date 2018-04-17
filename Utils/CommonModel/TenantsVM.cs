using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils.CommonModel
{
    public class TenantsVM
    {
        public int Id { get; set; }
        public string Tenant_id { get; set; }
        public string Name { get; set; }
        public DateTime CreatDate { get; set; }
    }
}
