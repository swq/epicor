using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace potest
{
    class Class1
    {
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
            sber.Append("</Request>");
            return sber.ToString();
        }
        public string RequestIP
        {
            get
            {
                return (RequestIP != null) ? RequestIP : "192.168.6.25";
            }
            set
            {
                RequestIP = value;
            }
        }
        public string ResponseIP
        {
            get { return (ResponseIP != null) ? ResponseIP : "192.168.6.10"; }
            set { ResponseIP = value; }
        }
        public string FormCreatorID { get; set; }
        public string FormOwnerID { get; set; }
        public string FormID
        {
            get
            {
                return (FormID != null) ? FormID : "OF_CGDD";
            }
            set { FormID = value; }
        }
        public string DetailDepth
        {
            get { return (DetailDepth != null) ? DetailDepth : "1"; }
            set { DetailDepth = value; }
        }
        public string SiteName
        {
            get { return (this.SiteName != null) ? this.SiteName : "EFNET"; }
            set { this.SiteName = value; }
        }
        public string Subject
        {
            get { return (Subject != null) ? Subject : "采购单审核"; }
            set { Subject = value; }
        }

        private string _test = "test";
        public string test
        {
            get
            {
                    return _test;
                
            }
            set { this._test = value; }

        }
    }
}
