using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.EntityModel
{
    public class Products
    {
        public int Id { get; set; }
        public string ProName { get; set; }
        public string Tenant_id { get; set; }
        public string Creater { get; set; }
        public int IsDeleted { get; set; }
    }
}
