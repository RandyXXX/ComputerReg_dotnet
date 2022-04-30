
/// <summary>
/// 傳進來
/// </summary>
public class inLogin{
    //AD or LDAP
    public string LoginType { set; get; }
    public string DomainName { set; get; }
    //帳號
    public string UserAccount { set; get; }
    //密碼
    public string UserPwd { set; get; }
    public string fingerprint { set; get; }

}

/// <summary>
/// 吐出去
/// </summary>
public class outLoginMoudle
{
    public string ServerKey { set; get; }
    public string UserName { set; get; }

}



