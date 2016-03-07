<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="StockMarketSimulator.Forms.UserRegistration" %>

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

<body style="height: 22px">
    <div>
    <nav class="navbar navbar-inverse navbar-fixed-top">
      <div class="container">
        <div class="navbar-header">
         <span class="navbar-brand text-info text-left">Stock Market Trading Simulator</span>
        </div>
        <div id="navbar" class="collapse navbar-collapse">
        </div>
      </div>
    </nav>

        <br />
        <br />
        <br />
    </div>
    <div class="container">
    <form id="form1" runat="server" class="register">
        <h2 class="form-signin-heading">New User Registration. All fields Mandatory.<asp:Button ID="Button2" runat="server" Text="Go to Login" class="btn btn-info btn pull-right" OnClick="Button2_Click" /></h2>
        <p>
           <b>First Name:</b>  <asp:TextBox type="text" ID="TextBox1" runat="server" class="form-control" placeholder="First Name"></asp:TextBox>
        </p>
        <p>
            <b>Last Name:</b>  <asp:TextBox  type="text" ID="TextBox2" runat="server" class="form-control" placeholder="Last Name"></asp:TextBox>
        </p>
        <p>
            <b>Email:</b>  <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" AutoCompleteType="Email" placeholder="Email Address"></asp:TextBox>
        </p>
        <p>
            <b>Username:</b> <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Choose Username"></asp:TextBox>
        </p>
        <p>
            <b>Password:</b>  <asp:TextBox type="password" ID="TextBox4" runat="server" CssClass="form-control" placeholder="Choose Password"></asp:TextBox>
        </p>
        <p>
           <b>Confirm Password:</b>  <asp:TextBox ID="TextBox5" type="password" runat="server" CssClass="form-control" placeholder="Confirm Password"></asp:TextBox>   
        </p>
        <p>
            <asp:Label ID="Label1" class="text-danger" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <asp:Button ID="Button1" type="submit" runat="server" class="btn btn-md btn-primary btn-block" OnClick="Button1_Click" Text="Register" />
        </p>
        <p>
            &nbsp;</p>
        </form>
    </div>
</body>
</html>
