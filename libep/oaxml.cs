using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ep.common
{
    static class oaxml
    {
        public static Request setRequest(string formCreatorID, string formOwnerID)
        {
            Request request = new Request()
            { FormCreatorID = formCreatorID, FormOwnerID = formOwnerID };
            return request;
        }

        public static Record setRecord(string classid, string docextcost, string docunitcost, string docunitcostlast,
            string docunitcostref, string duedate, string jobnum, string jobpartnum,
            string jobsequence, string linedesc, string mx, string of_cgddmx, string partnum, string projectid, string pum, string revisionnum, string trantype,
            string xorderqty, string xorderqtylast)
        {
            Record record = new Record()
            {
                classid = classid,
                docextcost = docextcost,
                docunitcost = docunitcost,
                docunitcostlast = docunitcostlast,
                docunitcostref = docunitcostref,
                duedate = duedate,
                jobnum = jobnum,
                jobpartnum = jobpartnum,
                jobsequence = jobsequence,
                linedesc = linedesc,
                mx = mx,
                of_cgddmx = of_cgddmx,
                partnum = partnum,
                projectid = projectid,
                pum = pum,
                revisionnum = revisionnum,
                trantype = trantype,
                xorderqty = xorderqty,
                xorderqtylast = xorderqtylast
            };
            return record;
        }

        public static Head setHead(string formcreator, string formcreatorC, string buyerid, string ponum,
            string potype, string vendorvendorid, string orderdate, string doctotaltax,
            string doctotalorder, string commenttext, string formdate, string formtype, string vendorname, string buyeridC)
        {

            Head head = new Head()
            {
                formcreator = formcreator,
                formcreatorC = formcreatorC,
                buyerid = buyerid,
                buyeridC = buyeridC,
                ponum = ponum,
                potype = potype,
                vendorvendorid = vendorvendorid,
                orderdate = orderdate,
                doctotaltax = doctotaltax,
                doctotalorder = doctotalorder,
                commenttext = commenttext,
                formdate = formdate,
                formtype = formtype,
                vendorname = vendorname
            };
            return head;
        }
    }

    interface Ioaxml
    {
        string xml();
    }

    class Condition : Ioaxml
    {
        public string xml()
        {
            //throw new NotImplementedException();
            return "<Condition />";
        }
    }

    class Request
    {
        /*
            <RequestIP>192.168.6.25</RequestIP> 					EPICOR服务器地址
            <ResponseIP>192.168.6.10</ResponseIP> 
            <FormCreatorID>20041</FormCreatorID> 				EPICOR登录人工号
            <FormOwnerID>2020</FormOwnerID> 					EPICOR登录人工号
            <FormID> OF_CGDD </FormID> 
            <DetailDepth>1</DetailDepth> 
            <SiteName>EFNET</SiteName> 
            <Subject>采购单审核</Subject>  	

        */
        public string xml()
        {
            StringBuilder sber = new StringBuilder();
            string tmpxml = string.Format(
                @"<Request>
            <RequestIP>{0}</RequestIP>
            <ResponseIP>{1}</ResponseIP> 
            <FormCreatorID>{2}</FormCreatorID>
            <FormOwnerID>{3}</FormOwnerID>
            <FormID>{4}</FormID> 
            <DetailDepth>{5}</DetailDepth> 
            <SiteName>{6}</SiteName> 
            <Subject>{7}</Subject> ",
                RequestIP, ResponseIP, FormCreatorID, FormOwnerID,
                FormID, DetailDepth, SiteName, Subject);
            sber.Append(tmpxml);
            sber.Append(RequestContent.xml());
            sber.Append("</Request>");
            return sber.ToString();
        }
        private string _RequestIP = "192.168.6.25";

        public string RequestIP
        {
            get { return _RequestIP; }
            set { _RequestIP = value; }
        }
        private string _ResponseIP = "192.168.6.10";
        public string ResponseIP
        {
            get { return _ResponseIP; }
            set { _ResponseIP = value; }
        }
        public string FormCreatorID { get; set; }
        public string FormOwnerID { get; set; }
        private string _FormID = "OF_CGDD";
        public string FormID
        {
            get { return _FormID; }
            set { _FormID = value; }
        }
        private string _DetailDepth = "1";
        public string DetailDepth
        {
            get { return _DetailDepth; }
            set { _DetailDepth = value; }
        }
        private string _SiteName = "EFNET";
        public string SiteName
        {
            get { return _SiteName; }
            set { _SiteName = value; }
        }
        private string _Subject = "采购单审核";
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        public RequestContent RequestContent { get; set; }

    }

    class RequestContent
    {
        public Head Head { get; set; }
        public Body Body { get; set; }
        public Condition condition { get; set; }
        public string xml()
        {
            StringBuilder sber = new StringBuilder();
            sber.Append("<RequestContent>");
            sber.Append(Head.xml());
            sber.Append(Body.xml());
            sber.Append(condition.xml());
            sber.Append("</RequestContent>");
            return sber.ToString();
        }
    }

    class Head
    {
        /*
        <formcreator>POHeader.BuyerID </formcreator>
        <formcreatorC >采购员姓名</formcreatorC>
        <buyerid>POHeader.BuyerID </buyerid>
        <buyeridC >采购员姓名</buyeridC>
        <ponum>POHeader.PONum </ponum>
        <potype>POHeader.POType </potype>
        <vendorvendorid>POHeader.VendorVendorID </vendorvendorid>
        <orderdate>POHeader.OrderDate </orderdate>
        <doctotaltax>POHeader.DocTotalTax </doctotaltax>
        <doctotalorder>POHeader.DocTotalOrder </doctotalorder>
        <commenttext>POHeader.CommentText </commenttext>
        <formdate >单据创建日期</formdate>
        <formtype >新增</formtype>							单据类型：新增or变更
        <vendorname>供应商描述 </vendorname>			

        */
        public string xml()
        {
            try
            {
                return string.Format(
              @"
        <Head tableName='of_cgdd'>
            <formcreator>{0}</formcreator>
            <formcreatorC>{1}</formcreatorC>
            <buyerid>{2}</buyerid>
            <buyeridC>{3}</buyeridC>
            <ponum>{4}</ponum>
            <potype>{5}</potype>
            <vendorvendorid>{6}</vendorvendorid>
            <orderdate>{7}</orderdate>
            <doctotaltax>{8}</doctotaltax>
            <doctotalorder>{9}</doctotalorder>
            <commenttext>{10}</commenttext>
            <formdate>{11}</formdate>
            <formtype>{12}</formtype>	
            <vendorname>{13}</vendorname>
        </Head>
                ", formcreator, formcreatorC, buyerid, buyeridC,
              ponum, potype, vendorvendorid, orderdate,
              doctotaltax, doctotalorder, commenttext, formdate, formtype, vendorname);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string formcreator { get; set; }
        public string formcreatorC { get; set; }
        public string buyerid { get; set; }
        public string buyeridC { get; set; }
        public string ponum { get; set; }
        public string potype { get; set; }
        public string vendorvendorid { get; set; }
        public string orderdate { get; set; }
        public string doctotaltax { get; set; }
        public string doctotalorder { get; set; }
        public string commenttext { get; set; }
        public string formdate { get; set; }
        public string formtype { get; set; }
        public string vendorname { get; set; }

    }

    class Body
    {
        /*
        <Body tableName=" of_cgdd mx">
        */
        public string xml()
        {
            StringBuilder sber = new StringBuilder();

            string tmpxml = "<Body tableName='of_cgddmx'>";
            sber.Append(tmpxml);
            foreach (Record rec in Records)
            {
                //tmpxml += rec.xml();
                sber.Append(rec.xml());
            }
            sber.Append("</Body>");
            return sber.ToString();
        }

        private List<Record> Records = new List<Record>();

        public void  addRecord(Record record) {
            Records.Add(record);
        }
    }



    class Record : IRecord
    {
        /*
        <of_cgdd mx 003>0001</of_cgdd mx 003> 	         （单身条目数量，如果3条，
        <projectid>PORel.ProjectID </projectid>		那么就是001，002，003）
        <trantype>PORel.TranType </trantype>
        <partnum>Part.PartNum </partnum>
        <revisionnum>PartRev.RevisionNum </revisionnum>
        <linedesc>PODetail.LineDesc </linedesc>
        <pum>PODetail.PUM </pum>
        <docunitcostref> docunitcostref </docunitcostref> 
        <docunitcostlast> PODetail.DocUnitCostLast </docunitcostlast> 
        <docunitcost>PODetail.DocUnitCost </docunitcost>
        <xorderqtylast> PODetail.XOrderQtyLast </xorderqtylast> 
        <xorderqty> PODetail.XOrderQty </xorderqty> 
        <docextcost> PODetail.DocExtCost </docextcost> 
        <classid> PODetail.ClassID </classid> 
        <jobnum> JobHead.JobNum </jobnum> 
        <jobpartnum> PartRev.PartNum </jobpartnum> 

        <jobsequence> Job Sequence </jobsequence> 
        <duedate> PODetail.DueDate </duedate> 

        */

        public string xml()
        {
            return string.Format(@" 
    <Record>       
        <of_cgddmx{0}>{1}</of_cgddmx{0}>
        <projectid>{2}</projectid >
        <trantype>{3}</trantype>
        <partnum>{4}</partnum>
        <revisionnum>{5}</revisionnum>
        <linedesc>{6}</linedesc>
        <pum>{7}</pum>
        <docunitcostref>{8}</docunitcostref> 
        <docunitcostlast>{9}</docunitcostlast> 
        <docunitcost>{10}</docunitcost>
        <xorderqtylast>{11}</xorderqtylast> 
        <xorderqty>{12}</xorderqty> 
        <docextcost>{13}</docextcost> 
        <classid>{14}</classid> 
        <jobnum>{15}</jobnum> 
        <jobpartnum>{16}</jobpartnum> 
        <jobsequence>{17}</jobsequence> 
        <duedate>{18}</duedate> 
    </Record> ", mx, of_cgddmx, projectid, trantype, partnum,
        revisionnum, linedesc, pum, docunitcostref, docunitcostlast, docunitcost,
        xorderqtylast, xorderqty, docextcost, classid, jobnum, jobpartnum, jobsequence, duedate);
        }

        public string mx { get; set; }
        public string of_cgddmx { get; set; }
        public string projectid { get; set; }
        public string trantype { get; set; }
        public string partnum { get; set; }
        public string revisionnum { get; set; }
        public string linedesc { get; set; }
        public string pum { get; set; }
        public string docunitcostref { get; set; }
        public string docunitcost { get; set; }
        public string docunitcostlast { get; set; }
        public string xorderqtylast { get; set; }
        public string xorderqty { get; set; }
        public string docextcost { get; set; }
        public string classid { get; set; }
        public string jobnum { get; set; }
        public string jobpartnum { get; set; }
        public string jobsequence { get; set; }
        public string duedate { get; set; }
    }
}
