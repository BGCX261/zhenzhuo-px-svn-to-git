using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainEngine.Core.Classes;

public partial class admin_Pages_Curricula_ＣurriculaInfo : System.Web.UI.Page
{
    private static string _id;
    private static Curricula _curricula;
   
    public Curricula CurrentCurricula { get { return _curricula; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        _id = HttpContext.Current.Request.QueryString["id"];

        if (_id != null && _id != string.Empty)
        {
            _curricula = Curricula.GetCurricula(new Guid(_id));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CurriculaInfo cli = new CurriculaInfo();
        cli.CurriculaId = _curricula.Id;
        cli.StartDate = Convert.ToDateTime(txtStart.Text);
        cli.EndDate = Convert.ToDateTime(txtEnd.Text);
        cli.CityTown = txtCity.Text;
        cli.Cast = Convert.ToInt32(txtCast.Text);
        _curricula.AddCurriculaInfo(cli);
        Reload();
    }
    protected void Reload()
    {
        ClientScript.RegisterClientScriptBlock(GetType(), "ClientScript",
            "<SCRIPT LANGUAGE='JavaScript'>parent.closeEditor(true);</SCRIPT>");
    }
}