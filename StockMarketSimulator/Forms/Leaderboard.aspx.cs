using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI.WebControls;
using StockMarketSimulator.Utilities;

namespace StockMarketSimulator.Forms
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if (!IsPostBack)
            //{
            if (Session["UserName"] != null)
            {
                //Do Nothing
            }
            else
            {
                Response.Redirect("~/Forms/Home.aspx");
            }

            if (DropDownList1.SelectedItem.Value == "1")
            {
                //string CS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='I:\\ASP.NET Tutorial\\StockMarketSimulator\\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(ConnectionString.ConnString))
                {
                    //Input query
                    string Top_Buyers = string.Format(@" 
                    select top 10 UserDetails.FirstName as 'First Name', UserDetails.LastName as 'Last Name', SUM(PurchasedStocks.NumStocks) as 'Total Stocks', UserDetails.Funds as 'Remaining Funds'
                    from UserDetails Inner Join PurchasedStocks
                    On PurchasedStocks.UserID = UserDetails.UserID
                    group by UserDetails.FirstName, UserDetails.LastName, UserDetails.Funds
                    order by 'Total Stocks' Desc;
                    ");

                    SqlCommand cmd = new SqlCommand(Top_Buyers, con);
                    con.Open();

                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                }
            }
            else if (DropDownList1.SelectedItem.Value == "2")
            {
                //string CS = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='I:\\ASP.NET Tutorial\\StockMarketSimulator\\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(ConnectionString.ConnString))
                {
                    //Input query
                    string Top_Buyers = string.Format(@" 
                    select top 10 UserDetails.FirstName as 'First Name', UserDetails.LastName as 'Last Name', UserDetails.Funds as 'Remaining Funds', SUM(PurchasedStocks.NumStocks) as 'Total Stocks'
                    from UserDetails Inner Join PurchasedStocks
                    On PurchasedStocks.UserID = UserDetails.UserID
                    group by UserDetails.FirstName, UserDetails.LastName, UserDetails.Funds
                    order by 'Remaining Funds';
                    ");

                    SqlCommand cmd = new SqlCommand(Top_Buyers, con);
                    con.Open();

                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                }
            }

        }
        //}


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