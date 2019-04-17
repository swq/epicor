using Erp.BO;
using Erp.Contracts;
using Erp.Proxy.BO;
using Erp.Tablesets;
using Ice.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace potest
{
    public partial class Form1 : Form
    {
        private liboa.swq.clsoa_po po = new liboa.swq.clsoa_po();
        private Ep.swq.clsPO epoa = new Ep.swq.clsPO();
        public Form1()
        {
            InitializeComponent();
        }

        public string TestAddPart()
        {
            
            string userName = "manager";
            string passWord = "manager";
            string appServerUrl = "net.tcp://epicor-ser/ERP102300";// "net.tcp://192.168.6.25/ERP102300";//net.tcp://epicor-ser/ERP102300
            Session epicorSession = new Session(userName, passWord, appServerUrl, Ice.Core.Session.LicenseType.Default, @"C:\Epicor\ERP10\LocalClients\ERP102300\config\ERP102300.sysconfig",false, "ONOFF01", null);
            POImpl poAdapter = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<POImpl>(epicorSession, Epicor.ServiceModel.Channels.ImplBase<POSvcContract>.UriPath);//
            PODataSet pods = poAdapter.GetByID(int.Parse(txt_ponum.Text));
            
            string echo = string.Format ( "OA03: {0} OA05:{1}", pods.POHeader.Rows[0]["OA03_c"], pods.POHeader.Rows[0]["OA05_c"]);
            lb_log.Items.Add("count: " + pods.POHeader.Rows.Count );
            lb_log.Items.Add(echo);
            foreach (DataColumn col in pods.POHeader.Columns)
            {
                lb_log.Items.Add( string.Format ("colname: {0} colvalue: {1}",col.ColumnName , pods.POHeader.Rows[0][col.ColumnName]) );
            }

            lb_log.Items.Add("Podetail count: " + pods.PODetail.Rows.Count );

            foreach (PODetailRow dr in pods.PODetail.Rows) {
                foreach (DataColumn col in pods.PODetail.Columns)
                {
                    lb_log.Items.Add(string.Format("colname: {0} colvalue: {1}", col.ColumnName, dr[col.ColumnName]));
                }
            }


            pods.POHeader.Rows[0]["OA05_c"] = "Y";
            pods.POHeader.Rows[0]["OA03_c"] = "Test";
            //PartImplpartAdapter = Ice.Lib.Framework.WCFServiceSupport.CreateImpl<PartImpl>(epicorSession, Epicor.ServiceModel.Channels.ImplBase<PartSvcContract>.UriPath);
            //string partnum = "TestPartAdd";
            //PartDataSetds = new PartDataSet();
            //partAdapter.GetNewPart(ds);
            //ds.Tables[0].Rows[0]["PartNum"] = partnum;
            //ds.Tables[0].Rows[0]["PartDescription"] = partnum;
            //partAdapter.ChangePartNum(partnum, ds);
            //partAdapter.Update(ds);
            poAdapter.Update(pods);
            epicorSession.Dispose();
            return "";
        }

        private void btn_ponum_Click(object sender, EventArgs e)
        {
            //po.TestAddPart(txt_ponum.Text, "Y", "OA审批通过");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TestOa();
        }

        private void TestOa()
        {
           epoa.TestOa();
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Request r = new Request();
            // MessageBox.Show(r.test);
            int i = 10;
            //MessageBox.Show (i.ToString().PadLeft( 4, '0'));
            TransEpicorApproveResult();
        }

        public string PostWebService(String URL, String MethodName, Hashtable ht)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL + "/" + MethodName);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            // 凭证
            request.Credentials = CredentialCache.DefaultCredentials;
            //超时时间
            request.Timeout = 10000;
            string PostStr = HashtableToPostData(ht);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(PostStr);
            request.ContentLength = data.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream, Encoding.UTF8);
            String retXml = sr.ReadToEnd();
            sr.Close();
            retXml = retXml.Replace("&lt;", "<");
            retXml = retXml.Replace("&gt;", ">");
            return retXml;
        }

        private String HashtableToPostData(Hashtable ht)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string k in ht.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(ht[k].ToString()));
            }
            return sb.ToString();
        }


        #region 审批同意，回传EPICOR
        private void TransEpicorApproveResult()
        {
            Hashtable ht = new Hashtable();
            ht.Add("ponum", 4028);
            ht.Add("oastatus", "Y");
            ht.Add("oaresult", "审批同意!");
            string data = PostWebService("http://192.168.6.25/epweb/wsoa.asmx", "setPOstatus ", ht);

        }
        #endregion
    }
}
