using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;
using TrainEngine.Core;

public partial class Training : TrainEngine.Core.Web.Controls.TrainBasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bindTabViews();
    }

    private void bindTabViews()
    {
        List<Field> fls = Field.Fields;

        int iCount = fls.Count;
        List<Field> fls1 = new List<Field>();
        List<Field> fls2 = new List<Field>();
        List<Field> fls3 = new List<Field>();

        for (int i = 0; i < iCount; i++)
        {
            int imod = i % 3;
            switch (imod)
            {
                case 0:
                    fls1.Add(fls[i]);
                    break;
                case 1:
                    fls2.Add(fls[i]);
                    break;
                case 2:
                    fls3.Add(fls[i]);
                    break;
            }
        }
        int iCount1 = fls1.Count;
        int iCount2 = fls2.Count;
        int iCount3 = fls3.Count;



        if (iCount1 >= 5)
        {
            for (int i = iCount1; i > 5; i--)
            {
                fls1.RemoveAt(i - 1);
            }
        }

        if (iCount2 >= 5)
        {
            for (int i = iCount2; i > 5; i--)
            {
                fls2.RemoveAt(i - 1);
            }
        }

        if (iCount3 >= 5)
        {
            for (int i = iCount3; i > 5; i--)
            {
                fls3.RemoveAt(i - 1);
            }
        }

        ltContainer.Text = "<div id=\"container1\" class=\"yui-skin-sam\" style=\"text-align:left; margin: 5px  5px 0 0;\"></div> ";
        ltContainer.Text += "<div id=\"container2\" class=\"yui-skin-sam\" style=\"text-align:left; margin: 5px  5px 0 0;\"></div> ";
        ltContainer.Text += "<div id=\"container3\" class=\"yui-skin-sam\" style=\"text-align:left; margin: 5px  5px 0 0;\"></div> ";
        ltContainer.Text +="<script type=\"text/javascript\">";
        ltContainer.Text +=   "(function () {";
        
        ltContainer.Text += " var tabView1 = new YAHOO.widget.TabView();";
        bool Active = false;
        foreach (Field item in fls1)
        {
            ltContainer.Text += "tabView1.addTab(new YAHOO.widget.Tab({";
            ltContainer.Text += "label: '"+item.FieldName+"',";
            ltContainer.Text += "dataSrc: '" + Utils.AbsoluteWebRoot + "Training/FieldsNoMaster.aspx?id=" + item.Id.ToString() + "',";
            ltContainer.Text += " cacheData: false,";
            if (!Active)
            {
                ltContainer.Text += " active: true" ;
                Active = true;
            }
            else
            {
                ltContainer.Text += " active: false";
            }
            ltContainer.Text += "   }));";
            ltContainer.Text += "";
        }
        ltContainer.Text += "tabView1.appendTo('container1');";
        Active = false;
        ltContainer.Text += " var tabView2 = new YAHOO.widget.TabView();";
        foreach (Field item in fls2)
        {
            ltContainer.Text += "tabView2.addTab(new YAHOO.widget.Tab({";
            ltContainer.Text += "label: '" + item.FieldName + "',";
            ltContainer.Text += "dataSrc: '" + Utils.AbsoluteWebRoot + "Training/FieldsNoMaster.aspx?id=" + item.Id.ToString() + "',";
            ltContainer.Text += " cacheData: false,";
            if (!Active)
            {
                ltContainer.Text += " active: true";
                Active = true;
            }
            else
            {
                ltContainer.Text += " active: false";
            }
            ltContainer.Text += "   }));";
            ltContainer.Text += "";
        }
        ltContainer.Text += "tabView2.appendTo('container2');";
        Active = false;
        ltContainer.Text += " var tabView3 = new YAHOO.widget.TabView();";
        foreach (Field item in fls3)
        {
            ltContainer.Text += "tabView3.addTab(new YAHOO.widget.Tab({";
            ltContainer.Text += "label: '" + item.FieldName + "',";
            ltContainer.Text += "dataSrc: '" + Utils.AbsoluteWebRoot + "Training/FieldsNoMaster.aspx?id=" + item.Id.ToString() + "',";
            ltContainer.Text += " cacheData: false,";
            if (!Active)
            {
                ltContainer.Text += " active: true";
                Active = true;
            }
            else
            {
                ltContainer.Text += " active: false";
            }
            ltContainer.Text += "   }));";
            ltContainer.Text += "";
        }
        ltContainer.Text += "tabView3.appendTo('container3');";

        ltContainer.Text += "})();</script>";

    }
}