using Ice.Lib;
using Ice.Lib.Framework;
using Infragistics.Win.UltraWinToolbars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using Ep.common;
using Erp.BO;
using Erp.Adapters;

namespace Ep.swq
{
    public class clsPO
    {
        Ice.Lib.Customization.CustomScriptManager csm;
        UltraToolbarsManager baseToolbarsManager;
        Erp.UI.App.POEntry.Transaction oTrans;
        Ice.Core.Session session;
        EpiDataView edvUD01;
        //Ice.UI.App.UD01Entry.UD01Form TForm;
        Erp.UI.App.POEntry.POEntryForm TForm;
        UltraToolbar standardTools;
        EpiButton epiBtNew;

        PODataSet ds_po;
        PODataSet.POHeaderDataTable dt_poheder;
        PODataSet.PODetailDataTable dt_podetail;

        POAdapter poAdapter;

        /// <summary>初始化数据(InitializeCustomCode中调用该方法)</summary>
        /// <param name="csm1">接受从Epicor中传过来的CustomScriptManager对象</param>
        public void Initialize(Ice.Lib.Customization.CustomScriptManager csm1)
        {
            csm = csm1;
            //((Ice.Core.Session)(csm.GetGlobalInstance("oTrans")));//(Ice.Core.Session)(csm.GetGlobalInstance("oTrans"));//
            oTrans =  ((Erp.UI.App.POEntry.Transaction)(csm.GetGlobalInstance("oTrans")));
            session =  (Ice.Core.Session)(oTrans.Session);
            edvUD01 = ((EpiDataView)(oTrans.EpiDataViews["POHeader"]));
            //oTrans.AdapterList[""];
            poAdapter = new POAdapter(oTrans);
            baseToolbarsManager = ((UltraToolbarsManager)(csm.GetGlobalInstance("baseToolbarsManager")));
            standardTools = baseToolbarsManager.Toolbars["Standard Tools"];
            TForm = ((Erp.UI.App.POEntry.POEntryForm)(this.csm.GetGlobalInstance("POEntryForm")));
            epiBtNew = GetControlByName<EpiButton>(TForm, "eb_sendPO");//获取UI界面的控件
            epiBtNew.Click += EpiBtNew_Click;

            ds_po = oTrans.POData;
            dt_poheder = ds_po.POHeader;
            dt_podetail = ds_po.PODetail;
        }
        /// <summary>释放数据(DestroyCustomCode中调用该方法)</summary>
        public void Destroy()
        {
            epiBtNew.Click -= EpiBtNew_Click;
        }
        /// <summary>窗体加载(UD01Form_Load中调用该方法)</summary>
        public void FormLoad()
        {
            HiddenLeftTree();
        }

        public  void TestOa() {
            sr.EFNETServiceSoapClient oa_client = new sr.EFNETServiceSoapClient("EFNETServiceSoap");
            //string cpu = oa_client.GetCPUMode();
            //oa_client.State 
            string[] result = oa_client.RemoteCreateForm(generalXML());
            //MessageBox.Show(cpu);
            MessageBox.Show(result[1], result[0]);
            //poAdapter.POData.POHeader[0][""] = "";
            edvUD01.CurrentDataRow.BeginEdit();
            edvUD01.CurrentDataRow["OA05_c"] = result[0];
            edvUD01.CurrentDataRow["OA04_c"] = result[1];
            edvUD01.CurrentDataRow.EndEdit();

            dt_poheder[0]["OA05_c"] = result[0];
            dt_poheder[0]["OA04_c"] = result[1];
            poAdapter.Update();
            oTrans.Update();
        }

        /// <summary>New按钮事件</summary>
        private void EpiBtNew_Click(object sender, EventArgs e)
        {
            //oTrans.GetNew();
            //edvUD01.CurrentDataRow.BeginEdit();／你好　ＰＯＮＵＭ
            //edvUD01.CurrentDataRow["Key1"] = "K" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
            //edvUD01.CurrentDataRow.EndEdit();
            TestOa();
        }

        string convertDate(string dd) {
            DateTime dt = DateTime.Parse(dd);
            return dt.ToString("yyyyMMddHHmmss");
        }

        private string generalXML() {
            

            string formcreator = dt_poheder[0]["BuyerID"].ToString(), formcreatorC = dt_poheder[0]["BuyerIDName"].ToString();
            string buyerid = dt_poheder[0]["BuyerID"].ToString(), ponum = dt_poheder[0]["ponum"].ToString();
            string potype = dt_poheder[0]["potype"].ToString(), vendorvendorid = dt_poheder[0]["VendorVendorID"].ToString();
            string orderdate = convertDate( dt_poheder[0]["orderdate"].ToString()), doctotaltax = dt_poheder[0]["doctotaltax"].ToString();
            string doctotalorder = dt_poheder[0]["doctotalorder"].ToString(), commenttext = dt_poheder[0]["commenttext"].ToString();
            string formdate = convertDate(dt_poheder[0]["orderdate"].ToString()), formtype = "新增";
            string vendorname = dt_poheder[0]["VendorName"].ToString();

            Request request = oaxml.setRequest(session.UserID, formcreator);
            RequestContent requestContent = new RequestContent();

            Head head = oaxml.setHead(formcreator, formcreatorC, buyerid, ponum,
            potype, vendorvendorid, orderdate, doctotaltax,
            doctotalorder, commenttext, formdate, formtype, vendorname, formcreatorC);
            Body body = new Body();
            requestContent.Head = head;
            requestContent.Body = body;
            requestContent.condition = new Condition();
            request.RequestContent = requestContent;

            int i = 0;
            foreach (DataRow dr in dt_podetail.Rows)
            {
                i++;
                string classid = dr["classid"].ToString(), docextcost = dr["docextcost"].ToString();
                string docunitcost = dr["docunitcost"].ToString(), docunitcostlast = dr["docunitcost"].ToString();
                string docunitcostref = "0", duedate = convertDate(dr["duedate"].ToString());
                string jobnum = dr["CalcJobNum"].ToString()  , jobpartnum = dr["partnum"].ToString();
                string jobsequence = "0", linedesc = dr["linedesc"].ToString();
                string mx = "003", of_cgddmx = i.ToString().PadLeft(4, '0');
                string partnum = dr["partnum"].ToString(), projectid = "", pum = dr["pum"].ToString();
                string revisionnum = dr["revisionnum"].ToString(), trantype = dr["CalcTranType"].ToString() ;
                string xorderqty = dr["xorderqty"].ToString(), xorderqtylast = dr["xorderqty"].ToString();
                Record tmpr = oaxml.setRecord(classid, docextcost, docunitcost, docunitcostlast,
                     docunitcostref, duedate, jobnum, jobpartnum, jobsequence, linedesc, mx, of_cgddmx,
                     partnum, projectid, pum, revisionnum, trantype, xorderqty, xorderqtylast);
                body.addRecord(tmpr);
            }
            string xmlpo = request.xml();
            Clipboard.SetText(xmlpo);
            //EpiMessageBox.Show(xmlpo, "request");
            //EpiMessageBox.Show("Hello" + session.UserName + " " + edvUD01.CurrentDataRow["PONUM"] + xmlpo);
            return xmlpo;
        }

        /// <summary>隐藏左边面板</summary>
        public void HiddenLeftTree()
        {
            Infragistics.Win.UltraWinDock.WindowDockingArea windowDockingArea1 = (Infragistics.Win.UltraWinDock.WindowDockingArea)(TForm.Controls["windowDockingArea1"]);
            windowDockingArea1.Visible = false;
        }
        /// <summary>按Name获取Epicor控件</summary>
        public static T GetControlByName<T>(Control control, string name)
        {
            Control[] controls = control.Controls.Find(name, true);
            if (controls.Length > 0) return (T)((Object)(controls[0]));
            return default(T);
        }
    }
}
