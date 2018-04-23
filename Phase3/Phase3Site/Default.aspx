<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #slider {
            width: 292px;
        }
    </style>
</head>
<body>
    <script type='text/javascript' src='http://code.jquery.com/jquery-1.8.3.js'></script>
    <script src="jquery-ui.js"></script>
    <link rel="stylesheet" href="jquery-ui.css"/>
    <script  type="text/javascript">
        $(document).ready(function () {
            var min = 0.5;
            var max = 5;
            if ($('#<%= HiddenField1.ClientID %>')) min = $('#<%= HiddenField1.ClientID %>').val();
            if ($('#<%= HiddenField2.ClientID %>')) max = $('#<%= HiddenField2.ClientID %>').val();

            $("#slider").slider(
  {
    min:0.5,
        max: 5,
        values: [min, max],
        step: 0.5,
        range: true,
    slide:function(event,ui)
    {
        $("#Label1").text(ui.values[0]);
        $("#Label2").text(ui.values[1]);
        $('#<%= HiddenField1.ClientID %>').val(ui.values[0]);
        $('#<%= HiddenField2.ClientID %>').val(ui.values[1]);
    }
                });

            $("#Label1").text($("#slider").slider("values", 0));
            $("#Label2").text($("#slider").slider("values", 1));
});
        </script>
    <form id="form1" runat="server">
        <div>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="0.5" />
            <asp:HiddenField ID="HiddenField2" runat="server" Value="5" />
            <br />
            &nbsp;&nbsp;
            Average Rating Filter:
    <br /><br />
            &nbsp;&nbsp;<asp:Label ID="Label1" runat="server" Text="0.5" Width="35px"></asp:Label>
            <div style ="display: inline-block;" id="slider"></div>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="5"></asp:Label>
            <br />
            <br />
&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Title Filter:" Width="110px"></asp:Label>
&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <br />
            &nbsp;
            <asp:Label ID="Label4" runat="server" Text="Tag Filter:" Width="110px"></asp:Label>
&nbsp;
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <br />
            
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
            &nbsp;
            <asp:Label ID="Label5" runat="server"></asp:Label>
            <br />

            <asp:Chart ID="Chart1" runat="server" Height="398px" Width="501px">
                <series>
                    <asp:Series ChartType="Bar" Name="Series1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <asp:Chart ID="Chart2" runat="server" Height="398px" Width="501px">
                <series>
                    <asp:Series ChartType="Bar" Name="Series1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
