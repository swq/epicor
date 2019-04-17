namespace Ep.common
{
    interface IRecord
    {
        string classid { get; set; }
        string docextcost { get; set; }
        string docunitcost { get; set; }
        string docunitcostlast { get; set; }
        string docunitcostref { get; set; }
        string duedate { get; set; }
        string jobnum { get; set; }
        string jobpartnum { get; set; }
        string jobsequence { get; set; }
        string linedesc { get; set; }
        string mx { get; set; }
        string of_cgddmx { get; set; }
        string partnum { get; set; }
        string projectid { get; set; }
        string pum { get; set; }
        string revisionnum { get; set; }
        string trantype { get; set; }
        string xorderqty { get; set; }
        string xorderqtylast { get; set; }

        string xml();
    }
}