using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.CommonModel;

namespace Utils
{
    public class TenantHelper
    {
        public static TenantsVM GetTenantInfo(string userName, string ssoUrl)
        {
            if (userName != null && userName != "error" && userName != "roleError")
            {
                var res = HttpHelper.OpenReadWithHttps(ssoUrl + "/Login/GetTenantInfo", "name=" + userName);
                if (res != null && res != "")
                {
                    var data = JsonConvert.DeserializeObject<TenantsVM>(res);
                    var retData = new TenantsVM()
                    {
                        Id = data.Id,
                        Name = data.Name,
                        Tenant_id = data.Tenant_id,
                        CreatDate = data.CreatDate
                    };
                    return retData;

                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }


}
