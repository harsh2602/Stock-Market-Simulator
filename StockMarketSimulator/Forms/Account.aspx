<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="StockMarketSimulator.Forms.Account" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="~/StockICO.ico">
    <title>Stock Market Trading Simulator</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

    
    <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <span class="sr-only">Toggle navigation</span>
              <asp:LinkButton ID="LinkButton5" runat="server" CssClass="navbar-brand text-left" OnClick="LinkButton5_Click">Stock Market Trading Simulator</asp:LinkButton>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
          <ul class="nav navbar-nav">
            <li class="active"><asp:LinkButton ID="LinkButton1" runat="server">Account</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Portfolio</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Favorites</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Leaderboard</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton4_Click">Markets Live</asp:LinkButton></li>
          </ul>
        <div class="pull-right">
            <asp:Button ID="Button6" runat="server" CssClass="btn btn-danger" Text="Logout" OnClick="Button6_Click" /><!--Logout-->
        </div>
        </div>
      </div>
    </nav>
    
        <br />
        <br />
        <br />

        <div class="container">
    
        <h3>Account Details</h3>
        <p>
            <blockquote>Change <code>Password</code> from below form</blockquote>
            <asp:Label ID="Label4" runat="server" CssClass="text-success" Text=""></asp:Label>
        </p>
       
        <p>
        <b>Username:</b> <asp:Label ID="Label1" runat="server" Text=""  class="form-control"></asp:Label>
        </p>
        <p>
        <b>First Name:</b> <asp:Label ID="Label6" runat="server" Text=""  class="form-control"></asp:Label>
        </p>
        <p>
        <b>Last Name</b>: <asp:Label ID="Label7" runat="server" Text=""  class="form-control"></asp:Label>
        </p>
        <p>
        <b>Existing Password:&nbsp;<asp:Label ID="Label5" runat="server" class="text-danger" Text=""></asp:Label> </b> <asp:TextBox ID="TextBox3" type="password" runat="server" class="form-control"></asp:TextBox>
        </p>
        <p>
        <b>New Password:&nbsp;<asp:Label ID="Label3" runat="server" class="text-danger" Text=""></asp:Label> </b> <asp:TextBox ID="TextBox4" type="password" runat="server" class="form-control"></asp:TextBox>
        </p>
        <p>
        <b>Email: </b> <asp:Label ID="Label8" runat="server" Text=""  class="form-control"></asp:Label>
        </p>
        <p>
        <b>Funds:</b> <asp:Label ID="Label2" runat="server" Text=""  class="form-control"></asp:Label>
        </p>
        <p>
            <asp:Button ID="Button1" type="submit" runat="server" class="btn btn-sm btn-danger btn-block" OnClick="Button1_Click" Text="Update" />
        </p>
    </div>
    
        
    </form>
</body>
</html>
