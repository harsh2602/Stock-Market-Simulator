using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using StockMarketSimulator.Utilities;
using System.Linq;
using System.Web.Providers.Entities;

namespace StockMarketSimulator.Forms
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                if (Session["UserName"] != null)
                {
                    Label5.Text = " " + Session["Username"].ToString().ToUpperInvariant();
                    
                }
                else
                {
                    Response.Redirect("~/Forms/Home.aspx");
                }
                var request = new QR_DataSetRequest();
                var entries = request.GetDataSet_Dictionary();


            if (!IsPostBack)
            {
                ListBox1.DataSource = entries;
                ListBox1.DataTextField = "value";
                ListBox1.DataValueField = "key";

                ListBox1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "")
            {
                Label9.Text = "Please specify stock name";
            }
            else
            {
                Label9.Text = ""; //Remove all error messages
                Label10.Text = ""; //Remove favorites message
                lookupStock(TextBox1.Text);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (ListBox1.SelectedValue == "")
            {
                Label9.Text = "Please choose a company from the list";
            }
            else
            {
                Label9.Text = ""; //Remove all error messages
                Label10.Text = ""; //Remove favorites message
                lookupStock(ListBox1.SelectedValue);
            }
        }

        //Buy Stocks Event Handler
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (Label2.Text == "")
            {
                Label10.Text = ""; //Remove favorites message
                Label9.Text = "Please search a company first to buy" + Request.Form["quantity"].ToString();
            }
            else if ((Request.Form["quantity"].ToString()) == "")
            {
                Label10.Text = ""; //Remove favorites message
                Label9.Text = "Please specify quantity of stocks";
            }
            else
            {
                Label9.Text = ""; //Remove all error messages
                Label10.Text = ""; //Remove favorites message
                //var strConnection = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename='I:\\ASP.NET Tutorial\\StockMarketSimulator\\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30";// ~//DB_name

                //DB Connection
                SqlConnection db = new SqlConnection(ConnectionString.ConnString);
                db.Open();
                //SQL Query to buy stocks
                String funds = String.Format(@"
                SELECT UserDetails.Funds 
                FROM UserDetails INNER JOIN Users
                ON UserDetails.UserID=Users.UserID where Users.UserName='{0}'; ", Session["Username"].ToString());
                //Execute Command
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db;
                cmd.CommandText = funds;
                var total_funds = Convert.ToInt32(cmd.ExecuteScalar());//Total Funds Existing

                int x = Convert.ToInt32(Request.Form["quantity"].ToString());
                var funds_spend = x * Convert.ToDouble(Label4.Text.ToString());
                if (funds_spend <= total_funds)
                {
                    var newFunds = total_funds - funds_spend;
                    /* cmd.CommandText = String.Format(@"update UserDetails set Funds = {0} 
                         where UserID = (select Users.UserID from Users where Users.UserName ='{1}');
                         declare @user as int;
                         select @user = UserID from Users where UserName = '{1}';
                         insert into PurchasedStocks (UserID, CompanyCode, NumStocks, Price, _Timestamp)
                         values(@user, '{2}', {3}, {4}, GETDATE());"
                         , newFunds,
                         Session["Username"].ToString(),//UserName
                         Label2.Text, //company code
                         Int32.Parse(Request.Form["quantity"].ToString()), //number of stocks
                         Convert.ToDouble(Label4.Text.ToString()) // price                             
                         );
                     */
                    cmd.CommandText = String.Format(@"
                        declare @TotalStocks int
                        declare @UserID as int;
                        declare @CurrentPrice float

                        update UserDetails set Funds = {0} 
                        where UserID = (select Users.UserID from Users where Users.UserName ='{1}');
           
                        select @UserID = UserID from Users where UserName = '{1}';
                        
                        insert into PurchasedStocks (UserID, CompanyCode, NumStocks, Price, _Timestamp)
                        values(@UserID, '{2}', {3}, {4}, GETDATE());
                        
                        --INSERT THE STOCKS INTO TheRealPurchasesTable...

                        --select @TotalStocks = SUM(TheRealPurchasesTable.TotalStocks) from TheRealPurchasesTable
                        --where UserID = @UserID and CompanyCode = '{2}'
                        
                        --@TotalStocks = @TotalStocks + {3}                        
                        --set @CurrentPrice = {4}
                         --check if compnany exits already for a user. If so INSERT else UPDATE
                        IF 
                        (select CompanyCode from TheRealPurchasesTable where CompanyCode = '{2}' and UserID = @UserID) is null              
                            BEGIN
                                set @TotalStocks = 0
                                set @TotalStocks = @TotalStocks + {3}    
                                set @CurrentPrice = {4}
                                insert into TheRealPurchasesTable (UserID, CompanyCode, TotalStocks, CurrentPrice)
                                values (@UserID, '{2}', @TotalStocks, @CurrentPrice)
                            END
                        ELSE
                            BEGIN
                                select @TotalStocks = SUM(TheRealPurchasesTable.TotalStocks) from TheRealPurchasesTable
                                    where UserID = @UserID and CompanyCode = '{2}'
                                set @TotalStocks = @TotalStocks + {3}
                                set @CurrentPrice = {4}
                                update TheRealPurchasesTable set TotalStocks = @TotalStocks, CurrentPrice = {3}
                                where UserID = @UserID and CompanyCode = '{2}'
                            END;
                        ",
                        newFunds,
                        Session["Username"].ToString(),//UserName
                        Label2.Text, //company code
                        Int32.Parse(Request.Form["quantity"].ToString()), //number of stocks
                        Convert.ToDouble(Label4.Text.ToString()) // price                             
                        );

                    cmd.ExecuteNonQuery();
                    Label10.Text = "You bought <b>" + x + "</b> stocks for <b>$" + funds_spend + "</b>";
                }
                else
                {
                    double difference_amount = funds_spend - total_funds;
                    Label9.Text = "You need " + difference_amount + " more to buy " + Request.Form["quantity"].ToString() + " stocks.";
                }

                //Creating connection with Database
                db.Close();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Event Handler to load Charts
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

        private void lookupStock(string input_selector) //Function to evaluate requested stock data from quandl
        {
            //Current Value
            var request = (HttpWebRequest)WebRequest.Create("https://www.quandl.com/api/v3/datasets/NSE/" + input_selector + ".json?start_date=2015-10-01&column_index=4&api_key=KRx_sFwof7iJVRtbyoE1");
            var response = (HttpWebResponse)request.GetResponse();
            var rawJson = new StreamReader(response.GetResponseStream()).ReadToEnd();

            var json = JObject.Parse(rawJson);  //Turns your raw string into a key value lookup
            string jsonString = json.ToString();

            dynamic dynObj = JsonConvert.DeserializeObject(jsonString);

            string data = "" + dynObj.dataset.data;
            string date = data.Substring(13, 10);
            string price = data.Substring(30, 7);


            string stockMarket = "" + dynObj.dataset.database_code;
            string company_id = "" + dynObj.dataset.dataset_code;

            TextBox1.Text = String.Empty;

            Label1.Text = stockMarket;
            Label2.Text = company_id;
            Label3.Text = date;
            Label4.Text = price;

            // Previous day price value
            Label7.Text = data.Substring(70, 10);

            // Check if Label7.Text contains the ] character
            // Returns boolean
            var doesContain = (Label7.Text).Contains(']');

            if (doesContain)
            {
                // If contains ] character create a substring of Label7.Text
                string NewString = Label7.Text.Substring(0,5);
                Label7.Text = NewString;  
            }

            //show image function
            if ((float.Parse(Label4.Text.ToString())) > (float.Parse(Label7.Text.ToString())))
            {
                Image1.ImageUrl = "~/Green_Arrow_Up.ico";
                double percentage_cal = Math.Round((((float.Parse(Label4.Text) - float.Parse(Label7.Text)) / (float.Parse(Label7.Text))) * 100),2);
                Label8.Text = percentage_cal.ToString() + "%";
            }

            else
            {
                Image1.ImageUrl = "~/Red_Arrow_down.ico";
                double percentage_cal = Math.Round((((float.Parse(Label7.Text) - float.Parse(Label4.Text)) / (float.Parse(Label7.Text))) * 100), 2);
                Label8.Text = percentage_cal.ToString() + "%";
            }
        }

        protected void Button5_Click(object sender, EventArgs e) //Click on this button to view funds
        {
            Label9.Text = ""; //Remove all error messages
            Label10.Text = ""; //Remove favorites message
            var funds = Records.CurrentBalance(Session["Username"].ToString());
            Label6.Text = "$ " + funds.ToString();
        }

        protected void Button4_Click(object sender, EventArgs e)//Add to favorites button
        {
            if (Label2.Text == "")
            {
                Label10.Text = ""; //Remove favorites message
                Label9.Text = "Search a company first to add it to favorites" + Request.Form["quantity"].ToString();
                return;
            }
            else
            {
                //SqlConnection db = new SqlConnection(ConnectionString.ConnString);
                //db.Open();
                //get the userID for a specified UserName
                //get the CompanyCode for a specified company
                //store the retrieved data into the Favorites table of the database
                /*
                string addToFavorites = string.Format(@"
                declare @UserID int
                select @UserID = UserID from Users where UserName = '{0}'
                insert into Favorites(UserID, CompanyCode) values (@UserID, '{1}')
                ", Session["Username"].ToString(), Label2.Text);
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = db;
                cmd.CommandText = addToFavorites;
                cmd.ExecuteNonQuery();
                db.Close();
                */

                Records.AddToFavorites(Session["Username"].ToString(), Label2.Text);
                Label9.Text = ""; //Remove all error messages
                Label10.Text = Label2.Text + " added to favorites";

            }
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