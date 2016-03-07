﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LiveTicker.aspx.cs" Inherits="StockMarketSimulator.Forms.LiveTicker" %>

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
    <div>
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
            <li><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Account</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Portfolio</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Favorites</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Leaderboard</asp:LinkButton></li>
            <li class="active"><asp:LinkButton ID="LinkButton6" runat="server">Markets Live</asp:LinkButton></li>
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

        <iframe src="http://localhost:61675/SignalR.Sample/StockTicker.html" frameborder = 0 width="800" height="400" scrolling="no"></iframe>
    </form>
</body>
</html>
