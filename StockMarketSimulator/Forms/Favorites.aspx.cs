using StockMarketSimulator.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StockMarketSimulator.Forms
{
    public partial class Favorites : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] != null)
                {
                    //Do Nothing
                }
                else
                {
                    Response.Redirect("~/Forms/Home.aspx");
                }

                //string CS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='I:\\ASP.NET Tutorial\\StockMarketSimulator\\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(ConnectionString.ConnString))
                {
                    string FavoritesQuery = string.Format(@"
                    declare @UserID int
                    select @UserID = Users.UserID from Users where UserName = '{0}'
                    select CompanyCode as 'My Favorites' from Favorites where UserID = @UserID
                    order by CompanyCode ASC
                    ", Session["Username"].ToString());

                    SqlCommand cmd = new SqlCommand(FavoritesQuery, con);
                    con.Open();

                    GridView1.DataSource = cmd.ExecuteReader();

                    GridView1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)//Remove from favorites
        {
            //String connInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='I:\ASP.NET Tutorial\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30");
            SqlConnection db = new SqlConnection(ConnectionString.ConnString);

            db.Open();

            SqlCommand cmd = new SqlCommand();

            cmd.Connection = db;
            cmd.CommandText = string.Format(@"
            Delete from Favorites where CompanyCode = '{0}' and UserID = (Select Users.UserID from Users where Users.UserName = '{1}');
            ", HiddenField1.Value, Session["Username"].ToString());

            cmd.ExecuteNonQuery();
            db.Close();

            Label1.Text = HiddenField1.Value + " removed from favorites";

            Response.Redirect(Request.RawUrl);//Refresh View 
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
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/LiveTicker.aspx");
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Forms/Home.aspx");
        }
    }
}