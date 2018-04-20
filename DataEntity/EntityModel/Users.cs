using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.EntityModel
{
    public class Users
    {
        public System.Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TenantsId { get; set; }
        public string Name { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
