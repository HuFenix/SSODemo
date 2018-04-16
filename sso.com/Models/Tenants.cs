using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sso.com.Models
{
    public class Tenants
    {
        public int Id { get; set; }
        public string Tenant_id { get; set; }
        public string Name { get; set; }
        public DateTime CreatDate { get; set; }
    }
}