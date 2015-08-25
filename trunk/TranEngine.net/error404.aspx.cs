using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainEngine.Core.Web.Controls;
using TrainEngine.Core;
using System.Collections.Generic;

public partial class error404 : TrainBasePage
{
  protected void Page_Load(object sender, EventArgs e)
  {
        Page.Title += Server.HtmlEncode(" - " + "Page not found");
  }

  
}
