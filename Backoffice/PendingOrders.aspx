<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PendingOrders.aspx.cs" Inherits="Backoffice.PendingOrders" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../dist/img/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../dist/img/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <strong> Pendientes de impresión </strong>
   <%--  <asp:GridView ID="grdShippings" runat="server" CssClass="table table-bordered table-hover">
    </asp:GridView>--%>
   
    <asp:GridView ID="grdShippings" runat="server" AutoGenerateColumns="false" CssClass="table-bordered "
    DataKeyNames="OrderId" OnRowDataBound="OnRowDataBound">
    <Columns>
        
        <asp:BoundField ItemStyle-Width="150px" DataField="OrderId" HeaderText="Nro. Orden" />
        <asp:BoundField ItemStyle-Width="150px" DataField="CreatedAt" HeaderText="Fecha" />
        <asp:BoundField ItemStyle-Width="150px" DataField="CourierName" HeaderText="Empresa" />
        <asp:BoundField ItemStyle-Width="150px" DataField="PackageReference" HeaderText="Descripción" />
       
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="../dist/img/plus.png" />
                <asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" CssClass = "table-bordered ">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Name" HeaderText="Nombre y Apellido"/>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Phone" HeaderText="Teléfono" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Passport" HeaderText="CI" />

                             <asp:BoundField ItemStyle-Width="150px" DataField="AddressLine1" HeaderText="Dirección" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityStateName" HeaderText="Departamento" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityCityName" HeaderText="Ciudad" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityName" HeaderText="Localidad" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>
</asp:GridView>

   
&nbsp;


</asp:Content>
