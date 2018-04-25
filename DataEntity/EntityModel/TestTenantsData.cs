using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEntity.EntityModel
{
    public static class TestTenantsData
    {
        public static List<TenantsInfo> GetTenatns()
        {
            return new List<TenantsInfo>() {
                new TenantsInfo() {Id=1,Tenant_id = "47AB0C3994359CD1483BF348A87269F4", Name="AA玻璃加工有限公司",CreatDate= Convert.ToDateTime("2018-04-16 16:24:02.000") },
                new TenantsInfo() {Id=1,Tenant_id = "CEA6D33D7BE3204333DD1ECA3D28FA51", Name="Type B有限公司",CreatDate= Convert.ToDateTime("2018-04-17 16:24:02.000") },
                new TenantsInfo() {Id=1,Tenant_id = "167102EF81DFCC6ACDD4FD4845E53DB5", Name="浙江CC集团",CreatDate= Convert.ToDateTime("2018-04-23 16:24:02.000") }
            };
        }
    }
}
