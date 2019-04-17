using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections;
namespace weboa
{
    /// <summary>
    /// wsoa 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class wsoa : System.Web.Services.WebService
    {

        private liboa.swq.clsoa_po po = new liboa.swq.clsoa_po();

        [WebMethod]
        public string HelloVersion()
        {
            return "V1.0.0";
        }


        [WebMethod]
        public string setPOstatus(string ponum, string oastatus, string oaresult)
        {
            return po.TestAddPart(ponum, oastatus, oaresult);
        }

        [WebMethod]
        public string setPOstatusByHash(string query)
        {
            System.Collections.Specialized.NameValueCollection table = HttpUtility.ParseQueryString(query);
            return po.TestAddPart(table["ponum"], table["oastatus"], table["oaresult"]);
        }
    }
}
