using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TrainEngine.Core;
using TrainEngine.Core.Classes;
using System.Web.Security;

public partial class admin_Pages_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindWellcom();
        BinServices();
    }

    private void BindWellcom()
    {
        string aType = "";
        if (User.IsInRole("students"))
        {
            aType = "学员";
        }
        if (User.IsInRole("teachers"))
        {
             aType = "授课讲师";
        }
        if (User.IsInRole("organs"))
        {
            aType = "培训机构";
        }
        if (User.IsInRole("administrators"))
        {
            aType = "管理员";
        }

        
        AuthorProfile ap = AuthorProfile.GetProfile(User.Identity.Name);
        string state = ap.IsPrivate && ap.NoMess != string.Empty ? "<font color='green'>已通过审核</font>" : "<font color='red'>审核不通过,请联系管理员</font>";
        string wellcom = "<font color='green'>" + AuthorProfile.GetProfile(User.Identity.Name).DisplayName + "</font>,欢迎您回来! ";
        if (aType != "管理员")
        {
            wellcom += "您现有积分:<font color='#3265C3'>" + ap.Points + "</font>点 会员类别:<font color='green'>" + aType + "</font> ";
            wellcom += "　会员级别:<font color='green'>普通会员</font> 帐户状态:" + state;
        }

        ltWellcom.Text = wellcom;
    }
    private void BinServices()
    {

    }
    #region 公开课
    public int CurriculaCount
    {
        get
        {
            List<Curricula> cls;
            if (Page.User.IsInRole("administrators"))
            {
                cls = Curricula.Curriculas;
            }
            else
            {
                cls = Curricula.Curriculas.FindAll(
                    delegate(Curricula c)
                    {
                        return c.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
            return cls.Count;
        }
    }
    public int NoCurriculaCount
    {
        get
        {
            List<Curricula> cls;
            if (Page.User.IsInRole("administrators"))
            {
                cls = Curricula.Curriculas.FindAll(
                    delegate(Curricula c)
                    {
                        return c.IsPublished == false;
                    });
            }
            else
            {
                cls = Curricula.Curriculas.FindAll(
                    delegate(Curricula c)
                    {
                        return c.Author == Page.User.Identity.Name && c.IsPublished == false;//only return Created by Owner
                    });
            }
            return cls.Count;
        }
    }
    #endregion

    #region 内训
    public int TrainingCount
    {
        get
        {
            List<Training> tng;
            if (Page.User.IsInRole("administrators"))
            {
                tng = Training.Trainings;
            }
            else
            {
                tng = Training.Trainings.FindAll(
                    delegate(Training t)
                    {
                        return t.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
            return tng.Count;
        }
    }
    public int NoTrainingCount
    {
        get
        {
            List<Training> tng;
            if (Page.User.IsInRole("administrators"))
            {
                tng = Training.Trainings.FindAll(
                    delegate(Training t)
                    {
                        return t.IsPublished == false;
                    });
            }
            else
            {
                tng = Training.Trainings.FindAll(
                    delegate(Training t)
                    {
                        return t.Author == Page.User.Identity.Name && t.IsPublished == false; ;//only return Created by Owner
                    });
            }
            return tng.Count;
        }
    }
    #endregion

    #region 培训现场
    public int ExcellentCount
    {
        get
        {
            List<Excellent> ext;
            if (Page.User.IsInRole("administrators"))
            {
                ext = Excellent.Excellents;
            }
            else
            {
                ext = Excellent.Excellents.FindAll(
                    delegate(Excellent t)
                    {
                        return t.Author == Page.User.Identity.Name;//only return Created by Owner
                    });
            }
            return ext.Count;
        }
    }
    public int NoExcellentCount
    {
        get
        {
            List<Excellent> ext;
            if (Page.User.IsInRole("administrators"))
            {
                ext = Excellent.Excellents.FindAll(
                    delegate(Excellent t)
                    {
                        return t.IsPublished == false;
                    });
            }
            else
            {
                ext = Excellent.Excellents.FindAll(
                    delegate(Excellent t)
                    {
                        return t.Author == Page.User.Identity.Name && t.IsPublished == false; ;//only return Created by Owner
                    });
            }
            return ext.Count;
        }
    }
    #endregion


    #region 上传资料
    public int ResCount
    {
        get
        {
            List<Res> rs;
            if (Page.User.IsInRole("administrators"))
            {
                rs = Res.Ress.FindAll(
                    delegate(Res t)
                    {
                        return t.Description != "Profile" && t.Description != "Update by Excellent";
                    });
            }
            else
            {
                rs = Res.Ress.FindAll(
                    delegate(Res t)
                    {
                        return t.Author == Page.User.Identity.Name && t.Description != "Profile" && t.Description != "Update by Excellent";//only return Created by Owner
                    });
            }
            return rs.Count;
        }
    }
    public int NoResCount
    {
        get
        {
            List<Res> rs;
            if (Page.User.IsInRole("administrators"))
            {
                rs = Res.Ress.FindAll(
                    delegate(Res t)
                    {
                        return t.IsPublished == false && t.Description != "Profile" && t.Description != "Update by Excellent";;
                    });
            }
            else
            {
                rs = Res.Ress.FindAll(
                    delegate(Res t)
                    {
                        return t.Author == Page.User.Identity.Name && t.IsPublished == false && t.Description != "Profile" && t.Description != "Update by Excellent";//only return Created by Owner
                    });
            }
            return rs.Count;
        }
    }
    #endregion

    #region 未审核用户
    public int NoUserCount
    {
        get
        {
            int count = 0;
            foreach (MembershipUser user in Membership.GetAllUsers())
            {
                string name = user.UserName;
                AuthorProfile ap = AuthorProfile.GetProfile(name);
                if (ap ==null || (!ap.IsPrivate && ap.NoMess == string.Empty))
                {
                    count++;
                }
            }
            return count;
        }
    }
    #endregion
}