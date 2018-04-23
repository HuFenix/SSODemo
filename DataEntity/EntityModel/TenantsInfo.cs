using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.EntityModel
{
    public class TenantsInfo
    {
        public int Id { get; set; }
        public string Tenant_id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> CreatDate { get; set; }
    }
}
