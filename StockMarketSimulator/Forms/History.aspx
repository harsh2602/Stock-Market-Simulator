<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="History.aspx.cs" Inherits="StockMarketSimulator.Forms.History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 482px;
        }
        .auto-style2 {
            width: 373px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Stocks You Purchased<br />
        <br />
        <table style="width: 99%;">
            <tr>
                <td class="auto-style1">Company Name</td>
                <td class="auto-style2"># of Stock</td>
                <td>Money Spent</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
