﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockTicker.aspx.cs" Inherits="StockMarketSimulator.Forms.StockTicker" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StockTicker.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <input type="button" id="open" value="Open Market" />
    <input type="button" id="close" value="Close Market" disabled="disabled" />
    <input type="button" id="reset" value="Reset" />

    <h2>Live Stock Table</h2>
    <div id="stockTable">
        <table border="1">
            <thead>
                <tr><th>Symbol</th><th>Price</th><th>Open</th><th>High</th><th>Low</th><th>Change</th><th>%</th></tr>
            </thead>
            <tbody>
                <tr class="loading"><td colspan="7">loading...</td></tr>
            </tbody>
        </table>
    </div>

    <h2>Live Stock Ticker</h2>
    <div id="stockTicker">
        <div class="inner">
            <ul>
                <li class="loading">loading...</li>
            </ul> 
        </div>
    </div>
    </div>
    </form>
     <script src="jquery-1.10.2.min.js"></script>
    <script src="jquery.color-2.1.2.min.js"></script>
    <script src="../Scripts/jquery.signalR-2.2.0.js"></script>
    <script src="../signalr/hubs"></script>
    <script src="SignalR.StockTicker.js"></script>
</body>
</html>
