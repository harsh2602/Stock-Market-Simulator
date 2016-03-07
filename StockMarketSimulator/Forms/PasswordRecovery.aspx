<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="StockMarketSimulator.Forms.PasswordRecovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" UserNameInstructionText="" UserNameLabelText="Email Address" UserNameTitleText="Recover Password" OnSendingMail="PasswordRecovery1_SendingMail"></asp:PasswordRecovery>
    </div>
    </form>
</body>
</html>
