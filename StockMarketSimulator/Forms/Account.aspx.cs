using StockMarketSimulator.Utilities;
using System;
using System.Data.SqlClient;

namespace StockMarketSimulator.Forms
{
    public partial class Account : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                //Label5.Text = " " + Session["Username"].ToString().ToUpperInvariant();
                Label1.Text = Session["Username"].ToString();

            }
            else
            {
                Response.Redirect("~/Forms/Home.aspx");
            }
            //Label1.Text = Session["Username"].ToString();

            SqlConnection db;
            //string connInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='I:\ASP.NET Tutorial\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30");
            db = new SqlConnection(ConnectionString.ConnString);

            db.Open();

            string CommandText = string.Format(@"
                select UserDetails.FirstName,UserDetails.LastName,UserDetails.Email,UserDetails.Funds
                from UserDetails
                inner join
                Users
                on UserDetails.UserID = Users.UserID
                where Users.UserName='{0}'", Label1.Text);

            SqlCommand cmd = new SqlCommand(CommandText, db);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Label6.Text = reader.GetString(0);
                Label7.Text = reader.GetString(1);
                Label8.Text = reader.GetString(2);
                Label2.Text = (reader.GetDouble(3)).ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)//Update Record for a User
        {
            Label3.Text = "";
            Label4.Text = "";

            if (TextBox3.Text == TextBox4.Text)
            {
                Label3.Text = "Existing and New Password Cannot be Same or empty";
            }
            else
            {
                //String connInfo = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='I:\ASP.NET Tutorial\StockMarketSimulator\TradingSimDatabase.mdf';Integrated Security=True;Connect Timeout=30");
                SqlConnection db = new SqlConnection(ConnectionString.ConnString);

                db.Open();

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = db;
                cmd.CommandText = string.Format(@"
                    Begin
                    declare @Count int;
                    declare @ReturnCode int;
                    select @Count = COUNT(UserName)
                    from Users where UserName= '{0}' and Pwd = '{1}';
                    If @Count = 1
                    Begin
                        Set @ReturnCode = 1
                    End
                    Else
                    Begin
                        Set @ReturnCode = -1
                    End
                    Select @ReturnCode as ReturnValue
                    End
                    ", Label1.Text, TextBox3.Text);

                int ReturnCode = (int)cmd.ExecuteScalar();
                db.Close();

                if (ReturnCode == -1)
                    Label5.Text = "Invalid Existing Password";
                else
                {
                    db.Open();
                    cmd.Connection = db;
                    cmd.CommandText = string.Format(@"
                    update Users set Users.Pwd='{0}' where Users.UserName='{1}';
                    ", TextBox4.Text, //password
                        Label1.Text  //user name
                    );

                    cmd.ExecuteNonQuery();
                    db.Close();

                    Label4.Text = "Password Changed Successfully";
                }
            }
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            Session["Username"] = Session["Username"].ToString();
            Response.Redirect("~/Forms/UserPage.aspx");
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