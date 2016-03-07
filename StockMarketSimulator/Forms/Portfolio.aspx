<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Portfolio.aspx.cs" Inherits="StockMarketSimulator.Forms.Portfolio" %>

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
            <li class="active"><asp:LinkButton ID="LinkButton2" runat="server">Portfolio</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Favorites</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Leaderboard</asp:LinkButton></li>
            <li><asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Markets Live</asp:LinkButton></li>
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
             <h3>List of Purchased Stocks</h3>
             <p><asp:GridView ID="GridView1" runat="server"></asp:GridView></p>
        <div class="row">
            <div class="col-md-6"></div>
            <div class="col-md-4 text-danger"><strong>Choose stock and click here to see Current Status</strong></div>
            <div class="col-md-1"><input type="button" id="get_status" class="get_status btn btn-primary" value="Current Price" /></div>
            <div class="col-md-1"></div>
        </div>

        <div class="row">
            <div id="current_price" class="col-md-6 lead"></div><!--Current Price will show up here-->
            <div id="" class="col-md-6"></div>
        </div>

        <div class="row">
            <div id="amount_result" class="col-md-6 lead"></div><!--Current Price will show up here-->
            <div id="" class="col-md-6"></div>
        </div>
        
        <div class="row">
            <div class="col-md-2"><input type="number" id="quantity" name="quantity" min="0" max="" class="form-control" style="width:150px; height:40px" placeholder="No. to sell"/></div>
             <div class="col-md-2"><asp:Button ID="Button1" runat="server" Text="Sell" OnClick="Button1_Click" CssClass="btn btn-primary" /></div>
             <div class="col-md-2"><asp:Button ID="Button2" runat="server" Text="Show Transaction History" CssClass="btn btn-info" OnClick="Button2_Click" /></div>
        </div>
        
        <br />
        <br />
            <b><asp:Label ID="Label1" runat="server" Text="" CssClass="text-danger"></asp:Label></b>
            
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />

            <br />

                <asp:GridView ID="GridView2" runat="server"></asp:GridView>
            <br />
                <asp:GridView ID="GridView3" runat="server"></asp:GridView> 

    </form>

    <script>
        var i = 0;
        
        $('#GridView1').find('th').eq(0).before('<th>Select</th>')// 'Select' column creation

        
        $('#GridView1').find('tr').each(function () {// Create Radio Button for each row in Datagrid
            $(this).attr('id', 'row' + i)
            $(this).find('td').eq(0).before("<td> <input type='radio' id='select" + i + "' name='select'></td>")
            i++;
        });
        
        $('#GridView1').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table
        $('#GridView2').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table
        $('#GridView3').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table

        $('#get_status').click(function (){//Company Name, Profit and Loss
            var radioID = $("input[type='radio']:checked").attr("id");
            final_radioID = "#" + radioID
            
            var company = $(final_radioID).closest('td').next('td').text(); // Get Company from table
            
            var total_stocks = $(final_radioID).closest('td').next('td').next('td').text();// Get total stocks for particular company
            total_stocks = parseInt(total_stocks);

            var total_spent =  $(final_radioID).closest('td').next('td').next('td').next('td').next('td').text();// Get total spent for particular company
            total_spent = parseFloat(total_spent);

            var xmlhttp = new XMLHttpRequest();
            var url = "https://www.quandl.com/api/v3/datasets/NSE/" + company + ".json?start_date=2015-11-09&column_index=4&api_key=KRx_sFwof7iJVRtbyoE1";

            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    var quandl_data = JSON.parse(xmlhttp.responseText);
                    console.log(quandl_data.dataset.data);
                    console.log(typeof (quandl_data.dataset.data));
                    var price = ((quandl_data.dataset.data).toString()).split(","); //Price Object
                    
                    document.getElementById('current_price').innerHTML = "<strong>Current Price for <code>" + company + "</code> is <code>" + price[1] + "</code></strong>";
                    price = parseFloat(price[1]); // Price Float

                    var profit = parseFloat((price * total_stocks) - total_spent);
                    var loss = parseFloat(total_spent - (price * total_stocks));
                    
                    if (profit.toFixed(2) == 0.00 || loss.toFixed(2) == 0) {
                        document.getElementById('amount_result').innerHTML = "<strong>You are equal</strong>";
                    }
                    else if (profit > loss) {
                        document.getElementById('amount_result').innerHTML = "<strong>Making a total profit of <code>" + profit.toFixed(2) + "</code></strong>";
                    } else {
                        document.getElementById('amount_result').innerHTML = "<strong>Making a total loss of <code>" + loss.toFixed(2) + "</code></strong>";
                    }
                    
                    $('#quantity').attr('max', total_stocks);

                    $('#HiddenField1').attr('Value', company)
                    $('#HiddenField2').attr('Value', price)
                }
            }
            xmlhttp.open("GET", url, true);
            xmlhttp.send();

            
        });

    </script>
</body>
</html>
