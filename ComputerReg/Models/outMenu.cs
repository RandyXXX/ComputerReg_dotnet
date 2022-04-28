
public class outMenu
{
    public BigMenu[] menu { set; get; }
}
public class BigMenu
{
    public string bigname { set; get; }
    public SubMenu[] item { set; get; }
}
public class SubMenu
{
    public string name { set; get; }
    public string path { set; get; }
}
