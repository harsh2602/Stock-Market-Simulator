<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="StockMarketSimulator.Forms.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="icon" href="~/StockICO.ico">
    <title>Stock Market Trading Simulator</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
    <link href="~/cover.css" rel="stylesheet">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <meta name="description" content="The description of my page" />
</head>
<body>
        <div class="site-wrapper bg-primary">

      <div class="site-wrapper-inner">

        <div class="cover-container">

          <div class="masthead clearfix">
            <div class="inner">
              <h3 class="masthead-brand">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Welcome to Stock Market Trading Simulator</h3>
              
            </div>
          </div>

        <div class="row">
            <div class="inner cover">
                    <h1 class="cover-heading">Choose appropriate option</h1>
            </div>
            <div class="col-lg-6">
                <div class="inner cover">
                    <h3 class="cover-heading">Click here to Register</h3>
                    <p class="lead">New User</p>
                    <p class="lead">
                    <a href="UserRegistration.aspx" class="btn btn-lg btn-default">Register</a>
                    </p>
                </div>
            </div>
            <div class="col-lg-6">
                 <div class="inner cover">
                    <h3 class="cover-heading">Click here to Login</h3>
                    <p class="lead">Returning User </p>
                    <p class="lead">
                    <button type="button" class="btn btn-lg btn-default" data-toggle="modal" data-target="#loginModal">Login</button>
                    </p>
                </div>
            </div>
      </div>

            <!--Modal for Login-->
            <div class="modal fade " id="loginModal" role="dialog">
                <div class="modal-dialog">
                    <!--Modal content-->
                    <form id="form1" runat="server" name="login">
                    <div class ="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title text-warning">Login</h4>
                            </div>
                            <div class="modal-body">
                                <p><asp:TextBox type="text" ID="TextBox1" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox></p>
                                <p><asp:TextBox type="password" ID="TextBox2" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox></p>
                            </div>
                            <div class="modal-footer">
                                <p><asp:Label ID="Label1" runat="server" Text="" CssClass="text-danger"></asp:Label></p>
                                <p><asp:Button ID="Button1" type="submit" runat="server" class="btn btn-lg btn-primary" Text="Login" OnClick="Button1_Click" /></p>
                            </div>
                    </div>
                    </form>
                </div>
            </div>

          <div class="mastfoot">
            <div class="inner">
              <p>CS440 Group I: Enrique, Jonathan, Gabriel, Harsh</p>
            </div>
          </div>

        </div>

      </div>

    </div>
</body>
</html>
