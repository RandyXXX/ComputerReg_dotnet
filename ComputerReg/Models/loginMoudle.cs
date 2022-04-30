
/// <summary>
/// 傳進來
/// </summary>
public class inLogin{
    //AD or LDAP
    public string loginType { set; get; }
    //帳號
    public string UserAccount { set; get; }
    //密碼
    public string Password { set; get; }

}

/// <summary>
/// 吐出去
/// </summary>
public class outLoginMoudle
{
    public string ServerKey { set; get; }
    public string UserName { set; get; }

}



