using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulator.Forms
{
    public partial class LiveTicker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Account.aspx");
        }
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Portfolio.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Favorites.aspx");
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Leaderboard.aspx");
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/UserPage.aspx");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Forms/Home.aspx");
        }
    }
}