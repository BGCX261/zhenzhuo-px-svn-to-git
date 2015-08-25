using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.IO;

public partial class reg_regtch : TrainEngine.Core.Web.Controls.TrainBasePage
{
    public  string clientID;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            fudPhoto.Attributes.Add("onfocus", "fnt_on('td_pic')");
            fudPhoto.Attributes.Add("onblur" ,"fnt_pic()");
            SetFields();
        }
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        string reg_uid = Request["reg_uid"];
        Membership.CreateUser(Request["reg_uid"], Request["reg_pwd1"], Request["reg_email"]);
        
        AuthorProfile pc = new AuthorProfile(reg_uid);
        pc.DisplayName = Request["reg_xingming"];
        pc.CityTown = Request["reg_shi"];
        //pc.Company = Request["reg_company"];
        if (Request["reg_phone2"] != string.Empty)
        {
            pc.PhoneMain = (Request["reg_phone1"].Trim() != string.Empty ? Request["reg_phone1"] + "-" : "") + Request["reg_phone2"] + (Request["reg_phone3"].Trim() != string.Empty ? "-" + Request["reg_phone3"] : "");
        }

        pc.PhoneMobile = Request["reg_mobile"];
        pc.MSN_QQ = Request["reg_qqmsn"];
        pc.Pay = Request["reg_pay"];
        pc.AboutMe = Request["reg_jianjie"];
        pc.Description1 = Request["reg_kehu"];
        pc.Description2 = Request["reg_zhujiang"];

        string fields = string.Empty;
        for (int i = 0; i < Field.Fields.Count; i++)
        {
            if (Request["reg_lingyu:" + i]!=null)
            {
                Field fld = Field.Fields[i];
                fields += fld.Id + "|";
            }
        }
        pc.Fields = fields.TrimEnd('|');
        Uppic(pc);
        pc.Save();

        Roles.AddUserToRole(reg_uid, "teachers");
        Response.Redirect(Utils.AbsoluteWebRoot + "reg/regok.aspx?uType=tch", true);
    }
    private void SetFields()
    {
        Table tb = new Table();
        tb.ID = "reg_lingyu";
        tb.CellSpacing = 0;
        tb.BorderWidth = 0;
        TableRow tr = new TableRow();
        tb.Rows.Add(tr);

        int tcount = 0;
        for (int i = 0; i < Field.Fields.Count; i++)
        {
            
            Field fld = Field.Fields[i];
            TableCell td = new TableCell();
            
            td.Text = "<input id=\"reg_lingyu_" + i + "\" type=\"checkbox\" name=\"reg_lingyu:" + i + "\" /><label for=\"reg_lingyu_" + i + "\">"+fld.FieldName+"</label>";
            if (tcount < 3)
            {
                tr.Cells.Add(td);
            }
            else
            {
                tr = new TableRow();
                tb.Rows.Add(tr);
                tr.Cells.Add(td);
                tcount = 0;
            }
            tcount++;

        }
        
       phFields.Controls.Add(tb);
       clientID = tb.ClientID;
    }
    #region 上传图片相关
    
    protected void Uppic(AuthorProfile ap)
    {
        if (fudPhoto.FileName == string.Empty)
        {
            return;
        }
        HttpPostedFile pf = fudPhoto.PostedFile;
        int intDocLen = pf.ContentLength;
        string contentType = pf.ContentType;
       
        byte[] Docbuffer = new byte[intDocLen];

        Stream objStream;
        objStream = pf.InputStream;
        objStream.Read(Docbuffer, 0, intDocLen);

        Res res = new Res();
        res.FileName = Path.GetFileName(pf.FileName);
        res.ResType = pf.ContentType;
        res.Description = "Profile";
        res.Author = ap.UserName;
        res.Points = 0;
        res.Save();

        res.CurrentPostFileBuffer = Docbuffer;
        res.BlobUpdate();

        res.CurrentPostFileBuffer = null;

        ap.PhotoURL = res.Id.ToString();
    }
    #endregion
}