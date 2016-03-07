using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using StockMarketSimulator.Utilities;

namespace StockMarketSimulator.Forms
{
    public partial class Portfolio : System.Web.UI.Page
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
                    SqlCommand cmd = new SqlCommand(@"select CompanyCode as 'Company Code', TotalStocks as 'Total Stocks', CurrentPrice as 'Buy Price', (TotalStocks*CurrentPrice) as 'Total $ Spent' from TheRealPurchasesTable where UserID = (select Users.UserID from Users where Users.UserName = '"+Session["Username"].ToString()+"')",con);//"select PurchasedStocks.CompanyCode as 'Company Name', Sum (PurchasedStocks.NumStocks) as 'Total Stocks', Sum(PurchasedStocks.Price * PurchasedStocks.NumStocks) as 'Total $ Spent', CONVERT(VARCHAR(10),PurchasedStocks._Timestamp,10) as 'Buy Date' from PurchasedStocks where PurchasedStocks.UserID = (Select Users.UserID from Users where Users.UserName = \'" + Session["Username"].ToString() + "\') group by PurchasedStocks.CompanyCode, CONVERT(VARCHAR(10), PurchasedStocks._Timestamp, 10) ", con);
                    con.Open();

                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e) // Sell Button
        {
            if (Request.Form["quantity"].ToString() == "")
            {
                Label1.Text = "Please select stock and view current status first";
            }
            else
            {
                int sell_quantity = Convert.ToInt32(Request.Form["quantity"].ToString());

                //String connInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='I:\ASP.NET Tutorial\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30");
                SqlConnection db = new SqlConnection(ConnectionString.ConnString);

                db.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = db;
                /*
                cmd.CommandText = string.Format(@"
                declare @UserID int
	            SELECT @UserID = Users.UserID from Users where UserName = '{0}';
	            
                insert into SoldStocks (UserID, CompanyCode, NumStocks, Price, _Timestamp) values (@UserID,'{1}','{2}','{3}',GETDATE());
                
                declare @Funds float
                SELECT @Funds=Funds from UserDetails where UserID=@UserID
                update UserDetails set Funds =(@Funds + {4}) where UserID = @UserID;
                Delete from PurchasedStocks value
                ", Session["Username"].ToString(), HiddenField1.Value, sell_quantity, float.Parse(HiddenField2.Value), sell_quantity * float.Parse(HiddenField2.Value));
                */

                cmd.CommandText = String.Format(@"
                declare @UserID int
                declare @Funds float
                declare @TotalStocks int 

                --get the Users ID
	            select @UserID = UserID from Users where UserName = '{0}'

	            --insert the sold stock and information into the SoldStocks table
                insert into SoldStocks (UserID, CompanyCode, NumStocks, Price, _Timestamp) values (@UserID,'{1}', {2}, {3}, GETDATE())
                
                --update the funds for a User                
                select @Funds = Funds from UserDetails where UserID = @UserID
                update UserDetails set Funds =(@Funds + {4}) where UserID = @UserID

                --update TheRealPurchasesTable purchased stocks table where the stock was sold
                --if total stocks will equal zero after selling then delete the entry else update the entry
                select @TotalStocks = TotalStocks from TheRealPurchasesTable where UserID = @UserID and CompanyCode = '{1}'
                IF
                    @TotalStocks - {2} = 0
                    BEGIN
                        delete from TheRealPurchasesTable where UserID = @UserID and CompanyCode = '{1}'
                    END 
                ELSE
                    BEGIN
                        update TheRealPurchasesTable set TotalStocks = (@TotalStocks - {2}) where UserID = @UserID and CompanyCode = '{1}'
                    END;",
                Session["Username"].ToString(), //UserName
                HiddenField1.Value.ToString(),//name of company of stocks
                sell_quantity, //Amount to sell
                float.Parse(HiddenField2.Value), //Price
                (sell_quantity * float.Parse(HiddenField2.Value)));//new funds

                cmd.ExecuteNonQuery();
                db.Close();
                Label1.Text = "You sold" + sell_quantity + " for a unit price of " + HiddenField2.Value;

                Response.Redirect(Request.RawUrl);
               
            }
        }
        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/UserPage.aspx");
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Account.aspx");
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/Favorites.aspx");
        }
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/LeaderBoard.aspx");
        }
        protected void LinkButton6_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/LiveTicker.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection db = new SqlConnection(ConnectionString.ConnString))
            {
                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                db.Open();

                cmd1.Connection = db;
                cmd2.Connection = db;

                //PURCHASED STOCKS TABLE
                cmd1.CommandText = String.Format(@"
                    declare @UserID as int
                    select @UserID = Users.UserID from Users where UserName = '{0}'
                    select CompanyCode as 'Company', NumStocks as '# Stocks', Price, _Timestamp as 'Buy Time'
                    from PurchasedStocks
                    where UserID = @UserID
                ", Session["Username"]);

                //SOLD STOCKS TABLE
                cmd2.CommandText = String.Format(@"
                    declare @UserID as int
                    select @UserID = Users.UserID from Users where UserName = '{0}'
                    select CompanyCode as 'Company', NumStocks as '# Stocks', Price, _Timestamp as 'Sell Time'
                    from SoldStocks
                    where UserID = @UserID
                ", Session["Username"]);

                using (var rdr = cmd1.ExecuteReader())
                {
                    GridView2.DataSource = rdr;
                    GridView2.DataBind();
                }

                using (var rdr = cmd2.ExecuteReader())
                {
                    GridView3.DataSource = rdr;
                    GridView3.DataBind();
                }

                db.Close();
            }
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("~/Forms/Home.aspx");
        }
    }
}