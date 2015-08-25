using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.ComponentModel;

/// <summary>
///TabView 的摘要说明
/// </summary>
[ToolboxData("<{0}:TabView runat=\"server\"> </{0}:TabView>")]
public class TabView : Control
{
    
    public TabView()
    {

    }

    [DefaultValue(TabGetDataType.Auto), Description("Tab 数据获取类型.")]
    public TabGetDataType TabGetDataType
    {
        get
        {
            string s = (string)ViewState["TabGetDataType"];
            return (s == null) ? TabGetDataType.Auto : (TabGetDataType)Enum.Parse(typeof(TabGetDataType), s);
        }
        set
        {
            ViewState["TabGetDataType"] = value;
        }

    }

}

public enum TabGetDataType
{ 
    UserWrite, Auto
}