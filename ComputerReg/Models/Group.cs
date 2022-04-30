/// <summary>
/// 傳進去的
/// </summary>
    public class inGroup
    {
    //public string Function { set; get; }
    public string Way { set; get; }
    public string GROUPNO { set; get; }
    public string GroupName { set; get; }
    public string GroupDesc { set; get; }
    public string SeqNo { set; get; }
    public string ServerKey { set; get; }
    public string fingerprint { set; get; }

    }
/// <summary>
/// 吐出來的
/// </summary>
public class outGroup
{
    public string Message { set; get; }
    public object Data { set; get; }

}
