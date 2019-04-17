using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.BO;
using Erp.Contracts;
using Erp.Proxy.BO;
using Ice.Core;

namespace liboa.swq
{

    public class clsoa_base {
        private Session epicorSession = null;
        protected Session createSession() {
            string userName = "manager";
            string passWord = "manager";
            string appServerUrl = "net.tcp://epicor-ser/ERP102300";// "net.tcp://192.168.6.25/ERP102300";//net.tcp://epicor-ser/ERP102300
            epicorSession = new Session(userName, passWord, appServerUrl, Ice.Core.Session.LicenseType.Default, @"C:\Epicor\ERP10\LocalClients\ERP102300\config\ERP102300.sysconfig", false, "ONOFF01", null);
            return epicorSession;
        }

        protected void closeSession() {
            epicorSession.Dispose();
        }
    }

    public class clsoa_po  : clsoa_base
    {
        public string TestAddPart(string ponum, string oastatus, string oaresult)
        {
            string sr = "";
            try
            {
                Session epicorSession = createSession();
                POImpl poAdapter = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<POImpl>(epicorSession, Epicor.ServiceModel.Channels.ImplBase<POSvcContract>.UriPath);//
                PODataSet pods = poAdapter.GetByID(int.Parse(ponum));

                string echo = string.Format("OA03: {0} OA05:{1}", pods.POHeader.Rows[0]["OA03_c"], pods.POHeader.Rows[0]["OA05_c"]);
                //lb_log.Items.Add("count: " + pods.POHeader.Rows.Count);
                //lb_log.Items.Add(echo);
                //foreach (DataColumn col in pods.POHeader.Columns)
                //{
                //    lb_log.Items.Add(string.Format("colname: {0} colvalue: {1}", col.ColumnName, pods.POHeader.Rows[0][col.ColumnName]));
                //}
                pods.POHeader.Rows[0]["OA03_c"] = oastatus;
                pods.POHeader.Rows[0]["OA02_c"] = oaresult;
                if (oastatus.Equals('Y'))
                {
                    pods.POHeader.Rows[0]["Approve"] = true;
                }
                else {
                    pods.POHeader.Rows[0]["Approve"] = false;
                }
                //PartImplpartAdapter = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<PartImpl>(epicorSession, Epicor.ServiceModel.Channels.ImplBase<PartSvcContract>.UriPath);
                //string partnum = "TestPartAdd";
                //PartDataSetds = new PartDataSet();
                //partAdapter.GetNewPart(ds);
                //ds.Tables[0].Rows[0]["PartNum"] = partnum;
                //ds.Tables[0].Rows[0]["PartDescription"] = partnum;
                //partAdapter.ChangePartNum(partnum, ds);
                //partAdapter.Update(ds);
                poAdapter.Update(pods);
                closeSession();
                sr = "OK";
            }
            catch (Exception ex)
            {
                sr = ex.Message.ToString();
            }

            return sr;
        }

    }
}
