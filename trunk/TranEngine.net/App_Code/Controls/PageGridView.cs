using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
namespace Controls
{
    /// <summary>
    ///PageGridView 的摘要说明
    /// </summary>
    [ToolboxData("<{0}:PageGridView runat=\"server\"> </{0}:PageGridView>")]
    public class PageGridView : GridView
    {
        public PageGridView()
        {

        }
        private int _recordCount;
        public int RecordCount
        {
            get { return _recordCount; }
            set { _recordCount = value; }
        }
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                LinkButton First = new LinkButton();
                LinkButton Prev = new LinkButton();
                LinkButton Next = new LinkButton();
                LinkButton Last = new LinkButton();
                DropDownList pageList = new DropDownList();
                pageList.AutoPostBack = true;
                pageList.Font.Size = new FontUnit(FontSize.Smaller);
                pageList.SelectedIndexChanged += new EventHandler(pageList_SelectedIndexChanged);
                TableCell tc = new TableCell();
                tc.Style.Add(HtmlTextWriterStyle.FontSize, "9pt");
                e.Row.Controls.Clear();

                tc.Controls.Add(new LiteralControl("  共"));

                tc.Controls.Add(new LiteralControl(_recordCount.ToString()));
                tc.Controls.Add(new LiteralControl("条  每页"));
                tc.Controls.Add(new LiteralControl(PageSize.ToString()));
                tc.Controls.Add(new LiteralControl("条  "));

                tc.Controls.Add(new LiteralControl((PageIndex + 1).ToString()));
                tc.Controls.Add(new LiteralControl("/"));
                tc.Controls.Add(new LiteralControl(PageCount.ToString()));
                tc.Controls.Add(new LiteralControl("  |  "));

                if (!String.IsNullOrEmpty(PagerSettings.FirstPageImageUrl))
                {
                    First.Text = "<img src='" + ResolveUrl(PagerSettings.FirstPageImageUrl) + "' border='0'/>";
                }
                else
                {
                    First.Text = PagerSettings.FirstPageText;
                }
                First.CommandName = "Page";
                First.CommandArgument = "First";
                First.Font.Underline = false;

                if (!String.IsNullOrEmpty(PagerSettings.PreviousPageImageUrl))
                {
                    Prev.Text = "<img src='" + ResolveUrl(PagerSettings.PreviousPageImageUrl) + "' border='0'/>";
                }
                else
                {
                    Prev.Text = PagerSettings.PreviousPageText;
                }
                Prev.CommandName = "Page";
                Prev.CommandArgument = "Prev";
                Prev.Font.Underline = false;


                if (!String.IsNullOrEmpty(PagerSettings.NextPageImageUrl))
                {
                    Next.Text = "<img src='" + ResolveUrl(PagerSettings.NextPageImageUrl) + "' border='0'/>";
                }
                else
                {
                    Next.Text = PagerSettings.NextPageText;
                }
                Next.CommandName = "Page";
                Next.CommandArgument = "Next";
                Next.Font.Underline = false;

                if (!String.IsNullOrEmpty(PagerSettings.LastPageImageUrl))
                {
                    Last.Text = "<img src='" + ResolveUrl(PagerSettings.LastPageImageUrl) + "' border='0'/>";
                }
                else
                {
                    Last.Text = PagerSettings.LastPageText;
                }
                Last.CommandName = "Page";
                Last.CommandArgument = "Last";
                Last.Font.Underline = false;

                if (this.PageIndex <= 0)
                {
                    First.Enabled = Prev.Enabled = false;
                }
                else
                {
                    First.Enabled = Prev.Enabled = true;
                }

                tc.Controls.Add(First);
                tc.Controls.Add(new LiteralControl("  "));
                tc.Controls.Add(Prev);
                tc.Controls.Add(new LiteralControl("  "));

                // 当前页左边显示的数字分页按钮的数量 
                int rightCount = (int)(PagerSettings.PageButtonCount / 2);
                // 当前页右边显示的数字分页按钮的数量 
                int leftCount = PagerSettings.PageButtonCount % 2 == 0 ? rightCount - 1 : rightCount;
                for (int i = 0; i < PageCount; i++)
                {
                    if (PageCount > PagerSettings.PageButtonCount)
                    {
                        if (i < PageIndex - leftCount && PageCount - 1 - i > PagerSettings.PageButtonCount - 1)
                        {
                            continue;
                        }
                        else if (i > PageIndex + rightCount && i > PagerSettings.PageButtonCount - 1)
                        {
                            continue;
                        }
                    }

                    if (i == PageIndex)
                    {
                        tc.Controls.Add(new LiteralControl("<span style='color:red;font-weight:bold'>" + (i + 1).ToString() + "</span>"));
                    }
                    else
                    {
                        LinkButton lb = new LinkButton();
                        lb.Text = (i + 1).ToString();
                        lb.CommandName = "Page";
                        lb.CommandArgument = (i + 1).ToString();

                        tc.Controls.Add(lb);
                    }

                    tc.Controls.Add(new LiteralControl("  "));

                    int pageNumber = i + 1;
                    ListItem item = new ListItem(pageNumber.ToString());

                    if (i == PageIndex)
                    {
                        item.Selected = true;
                    }

                    pageList.Items.Add(item);
                }

                if (this.PageIndex >= PageCount - 1)
                {
                    Next.Enabled = Last.Enabled = false;
                }
                else
                {
                    Next.Enabled = Last.Enabled = true;
                }
                tc.Controls.Add(Next);
                tc.Controls.Add(new LiteralControl("  "));
                tc.Controls.Add(Last);
                tc.Controls.Add(new LiteralControl("  |  "));
                tc.Controls.Add(pageList);
                tc.ColumnSpan = this.Columns.Count;
                e.Row.Controls.Add(tc);
            }

            base.OnRowCreated(e);
        }

        void pageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow pagerRow = BottomPagerRow;
            DropDownList pageList = (DropDownList)sender;
            int newPageIndex = pageList.SelectedIndex;
            base.OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));
        }

        
    }
}