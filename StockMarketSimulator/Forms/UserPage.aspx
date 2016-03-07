<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="StockMarketSimulator.Forms.UserPage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
   <link rel="icon" href="~/StockICO.ico">
   <title>Stock Market Trading Simulator</title>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
   <link href="~/dashboard.css" rel="stylesheet">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
  <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    
    <style type="text/css">
        .auto-style1 {
            display: block;
            padding: 6px 12px;
            font-size: 14px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
            -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
        }
    </style>

</head>

<body>
    <div>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container text-left">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar" style="height: 2px"></span>
            <span class="icon-bar"></span>
          </button>
          </div>  
          <span class="navbar-brand text-info text-left">Stock Market Trading Simulator</span>
          <div style="width: 1237px; height: 31px; margin-left: 0px">
        <form id="form1" runat="server" class="navbar-form">
        <ul class="nav navbar-nav">
            <li class ="">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Account</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Portfolio</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Favorites</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Leaderboard</asp:LinkButton>
            </li>
            <li>
                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Markets Live</asp:LinkButton>
            </li>
        </ul>
         <div class="pull-right">
            <div class="form-group">
                <asp:TextBox ID="TextBox1" runat="server" ToolTip="Please Enter Stock Market Company Identifier" placeholder="Enter Stock" Width="149px" CssClass="form-control"></asp:TextBox>
            </div>
             &nbsp;
            <!--Button to search from textbox-->
            <asp:Button ID="Button1" runat="server" Text="Search Stock" class="btn btn-success" OnClick="Button1_Click" Width="130px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button6" runat="server" CssClass="btn btn-danger" Text="Logout" OnClick="Button6_Click" /><!--Logout-->             
         </div>   
            </div>
           </div>
    </nav>
         <div>
             <br />
             <br />
             <br />
             <br />
         <div class="container-fluid">
      <div class="row">
        <div class="col-sm-3 col-md-2 sidebar">
          <div class="container-fluid">
      <div class="row">
          <div class="col-sm-3 col-md-2 sidebar">
              <!--Listbox Created-->
            <asp:ListBox ID="ListBox1" runat="server" Height="349px" Width="200px" CssClass="col-lg-push-4" SelectionMode="Single"></asp:ListBox>
              <br />
              <!-- Button to search from textbox -->
              <asp:Button ID="Button2" runat="server" Text="Search" class="btn btn-primary" OnClick="Button2_Click" />
          </div>
          
      </div>
    </div>
        </div>
        <div class="col-sm-9 col-sm-offset-3 col-md-10 col-md-offset-2 main">
          <h3 class="page-header">Welcome
              <asp:Label ID="Label5" runat="server" Text=""></asp:Label> <!--Shows the Username for the session-->
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="Label6" runat="server" Cssclass="text-right" Text=""></asp:Label><!-- Shows the fund on click-->
              <asp:Button ID="Button5" runat="server" Text="View Balance" class="btn btn-info btn pull-right" OnClick="Button5_Click" /><!--Get Funds from DB-->
            </h3>

          <h4 class="sub-header">Stock Detail</h4>
            <p>
                <asp:Label ID="Label9" runat="server" class="text-danger" Text=""></asp:Label><!--Error In Buying-->
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label10" runat="server" class="text-success" Text=""></asp:Label>
            </p>

          <div class="table-responsive">
            <table class="table table-striped">
              <thead>
                <tr>
                  <th>Date</th>  
                  <th>Stock Market</th>
                  <th>Company</th>
                  <th>Price</th>
                </tr>
              </thead>
              <tbody>
                <tr>
                  <td><asp:Label ID="Label3" runat="server" Text=""></asp:Label></td><!--Current Date-->                  
                  <td><asp:Label ID="Label1" runat="server" Text=""></asp:Label></td><!--Searched Stock Market-->
                  <td><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td><!--Searched Stock-->
                  <td><asp:Label ID="Label4" runat="server" Text=""></asp:Label></td><!--Current Price-->
                </tr>
                <tr>
                  <td></td>                    
                  <td></td>
                  <td>
                    <asp:Image ID="Image1" ImageURL="~/Default.ico" runat="server" />&nbsp;<asp:Label ID="Label8" runat="server" Text=""></asp:Label><!--Red or green with % change-->
                  </td>
                  <td>
                      <asp:Label ID="Label7" runat="server" Text=""></asp:Label><!--Last recorded Value-->
                    </td>
                </tr>                  
                 <tr>
                  <td></td>                    
                  <td></td>
                  <td><input type="number" id="quantity" name="quantity" min="1" max="" class="form-control" style="width:200px; height:40px" placeholder="No. to buy"/><!--Input Stock Value--></td>
                  <td>
                      <div class="col-xs-4"><asp:Button ID="Button3" runat="server" Text="Buy" class="btn btn-primary" OnClick="Button3_Click" /></div><!--Buy Button-->
                </td>
                </tr>
                  <tr>
                      <td></td>
                      <td></td>
                      <td></td>
                      <td><asp:Button ID="Button4" runat="server" Text="Add to favorites" class="btn btn-primary" OnClick="Button4_Click" /></td>
                  </tr>
                  <tr>
                      <td>
                        <button type="button" class="btn btn-info" data-toggle="modal" data-target="#7DayModal">7-Day Trend</button>
                      </td>
                      <td>
                        <button type="button" class="btn btn-info" data-toggle="modal" data-target="#WeeklyModal">Weekly</button>
                      </td>
                      <td>
                          <button type="button" class="btn btn-info" data-toggle="modal" data-target="#MonthlyModal">Monthly Trend</button>
                      </td>
                      <td>
                          <button type="button" class="btn btn-info" data-toggle="modal" data-target="#AnnualModal">Annual Trend</button>
                      </td>
                  </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    </div>

    <!--7 Day Modal-->
             <div class="modal fade" id="7DayModal" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <div class="modal-content">

                         <div class="modal-header">
                             <h3>7-Day View</h3>
                         </div>
                         <div class="modal-body">
                             <table id="table">
                                <thead>
                                    <tr>
                                        <td>Date</td>
                                        <td>Price</td>
                                    </tr>
                                </thead>
                                <tbody id="body">
                                </tbody>
                             </table>
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                         </div>
                     </div>
                 </div>
             </div>
        <!--7 Day Modal-->
    
        <!--Weekly Modal-->
            <div class="modal fade" id="WeeklyModal" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <div class="modal-content">

                         <div class="modal-header">
                             <h3>Weekly View</h3>
                         </div>
                         <div class="modal-body">
                             <table id="table1">
                                <thead>
                                    <tr>
                                        <td>Date</td>
                                        <td>Price</td>
                                    </tr>
                                </thead>
                                <tbody id="body1">
                                </tbody>
                             </table>
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                         </div>
                     </div>
                 </div>
             </div>
        <!--Weekly Modal-->

             <!--Monthly Modal-->
            <div class="modal fade" id="MonthlyModal" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <div class="modal-content">

                         <div class="modal-header">
                             <h3>Monthly View</h3>
                         </div>
                         <div class="modal-body">
                             <table id="table2">
                                <thead>
                                    <tr>
                                        <td>Date</td>
                                        <td>Price</td>
                                    </tr>
                                </thead>
                                <tbody id="body2">
                                </tbody>
                             </table>
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                         </div>
                     </div>
                 </div>
             </div>
        <!--Monthly Modal-->

             <!--Annual Modal-->
             <div class="modal fade" id="AnnualModal" role="dialog">
                 <div class="modal-dialog modal-lg">
                     <div class="modal-content">

                         <div class="modal-header">
                             <h3>Annual View</h3>
                         </div>
                         <div class="modal-body">
                             <table id="table3">
                                <thead>
                                    <tr>
                                        <td>Date</td>
                                        <td>Price</td>
                                    </tr>
                                </thead>
                                <tbody id="body3">
                                </tbody>
                             </table>
                         </div>
                         <div class="modal-footer">
                             <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                         </div>
                     </div>
                 </div>
             </div>
             <!--Annual Modal-->
       

        </div>
           </form>  
          
        
        <br />
                             
          
    </div>

 <script>
     //7 Day Data
    $('#table').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table

    var xmlhttp = new XMLHttpRequest();
    var company_name = document.getElementById("Label2").innerHTML;
    var url = "https://www.quandl.com/api/v3/datasets/NSE/" + company_name + ".json?start_date=2015-11-01&end_date=2015-11-24&column_index=4&data_frequency=daily&rows=7&exclude_column_names=true&api_key=KRx_sFwof7iJVRtbyoE1";

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var quandl_data = JSON.parse(xmlhttp.responseText);
            var company_price = ((quandl_data.dataset.data).toString()).split(",");

            for (var i = 0; i < company_price.length ; i += 2) {
                document.getElementById("body").innerHTML += "<td>" + company_price[i] + "</td><td>" + company_price[i + 1] + "</td>";
            }

        }
    };
    xmlhttp.open("GET", url, true);
    xmlhttp.send();

     //Weekly Data
    $('#table1').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table

    var xmlhttp1 = new XMLHttpRequest();
    var company_name = document.getElementById("Label2").innerHTML;
    var url = "https://www.quandl.com/api/v3/datasets/NSE/" + company_name + ".json?start_date=2014-11-01&end_date=2015-11-24&column_index=4&collapse=weekly&exclude_column_names=true&api_key=KRx_sFwof7iJVRtbyoE1";

    xmlhttp1.onreadystatechange = function () {
        if (xmlhttp1.readyState == 4 && xmlhttp1.status == 200) {
            var quandl_data = JSON.parse(xmlhttp1.responseText);
            var company_price = ((quandl_data.dataset.data).toString()).split(",");

            for (var i = 0; i < company_price.length ; i += 2) {
                document.getElementById("body1").innerHTML += "<td>" + company_price[i] + "</td><td>" + company_price[i + 1] + "</td>";
            }

        }
    };
    xmlhttp1.open("GET", url, true);
    xmlhttp1.send();

     //Monthly Data
    $('#table2').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table

    var xmlhttp2 = new XMLHttpRequest();
    var company_name = document.getElementById("Label2").innerHTML;
    var url = "https://www.quandl.com/api/v3/datasets/NSE/" + company_name + ".json?start_date=2010-11-01&end_date=2015-11-24&column_index=4&collapse=monthly&exclude_column_names=true&api_key=KRx_sFwof7iJVRtbyoE1";

    xmlhttp2.onreadystatechange = function () {
        if (xmlhttp2.readyState == 4 && xmlhttp2.status == 200) {
            var quandl_data = JSON.parse(xmlhttp2.responseText);
            var company_price = ((quandl_data.dataset.data).toString()).split(",");

            for (var i = 0; i < company_price.length ; i += 2) {
                document.getElementById("body2").innerHTML += "<td>" + company_price[i] + "</td><td>" + company_price[i + 1] + "</td>";
            }

        }
    };
    xmlhttp2.open("GET", url, true);
    xmlhttp2.send();

     //Annual Data

    $('#table3').attr('class', 'table table-hover table-bordered');//Create Bootstrapped Table

    var xmlhttp3 = new XMLHttpRequest();
    var company_name = document.getElementById("Label2").innerHTML;
    var url = "https://www.quandl.com/api/v3/datasets/NSE/" + company_name + ".json?start_date=2000-11-24&end_date=2015-11-24&column_index=4&collapse=annual&exclude_column_names=true&api_key=KRx_sFwof7iJVRtbyoE1";

    xmlhttp3.onreadystatechange = function () {
        if (xmlhttp3.readyState == 4 && xmlhttp3.status == 200) {
            var quandl_data = JSON.parse(xmlhttp3.responseText);
            var company_price = ((quandl_data.dataset.data).toString()).split(",");

            for (var i = 0; i < company_price.length ; i += 2) {
                document.getElementById("body3").innerHTML += "<td>" + company_price[i] + "</td><td>" + company_price[i + 1] + "</td>";
            }

        }
    };
    xmlhttp3.open("GET", url, true);
    xmlhttp3.send();
</script>   
    
</body>

</html>
