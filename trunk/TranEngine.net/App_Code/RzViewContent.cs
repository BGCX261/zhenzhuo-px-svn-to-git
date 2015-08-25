using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
///RzViewContent 的摘要说明
/// </summary>
public class RzViewContent
{
	public RzViewContent(string title, string content, string rzType)
	{
        Title = title;
        Content = content;
        RzType = rzType;
	}
    private string _title;
    public string Title
    {
        set{_title = value;}
        get{return _title.Replace("_","/");}
    }
    private string _content;
    public string Content
    {
        set { _content = value; }
        get { return _content; }
    }
    private string _rzType;
    public string RzType
    {
        set { _rzType = value; }
        get { return _rzType; }
    }

}