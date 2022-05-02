public class inRegALLComputer
{
    //string HostNameA="";
    //string FajNoA = "";
    //string remarkA = "";
    //string UseEmpnoA = "";
    //string MACA = "";
    //string WayA = "";
    //string idA = "";
    
    public string HostName { set; get; }
    public string FajNo { set; get; }
    public string remark { set; get; }
    public string UseEmpno { set; get; }
    public string MAC { set; get; }
    public string Way { set; get; }
    public string id { set; get; }
    public string QueryEmpno { set; get; }
    public string own_type { set; get; }
    public string stats { set; get; }
    public string QueryMacAddress { set; get; }
    public string QueryFajNo { set; get; }
    public int Page { set; get; }
    public string ServerKey { set; get; }
    public string fingerprint { set; get; }

}
public class outRegALLComputer
{
    public string Message { set; get; }
    public object Data { set; get; }
}

