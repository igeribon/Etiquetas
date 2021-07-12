<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Backoffice.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 502px;
        }
        .auto-style2 {
            width: 252px;
        }
        .auto-style3 {
            width: 554px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <strong> <big>Buscar</big> </strong>



    <p>
        <table border="0" width="60%">  

        <tr>  
         <td class="auto-style2">  
                 &nbsp; 
         <asp:DropDownList ID="drpSearchType" runat="server" Height="22px" Width="125px">
             </asp:DropDownList>
         </td>  
         <td class="auto-style3">  
             &nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="225px"></asp:TextBox>
         </td>  


         <td width="10%" class="auto-style1">  
             
            <asp:Button ID="btnSearch" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Buscar" OnClick="btnAplicar_OnClick" Width="73px"/>
             
         </td>  
     </tr>  
            </table>
        
  

             

</p>
    <br />
   <%--  <asp:GridView ID="grdShippings" runat="server" CssClass="table table-bordered table-hover">
    </asp:GridView>--%>
   
    <asp:GridView ID="grdShippings" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover"
    DataKeyNames="OrderId" OnRowDataBound="OnRowDataBound">
        <RowStyle CssClass="table-bordered" />
        <HeaderStyle CssClass="table-bordered"/>
    <Columns>
        
        <asp:BoundField ItemStyle-Width="75px" DataField="OrderId" HeaderText="Orden" >
<ItemStyle Width="75px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="200px" DataField="CreatedAt" HeaderText="Fecha" >
<ItemStyle Width="200px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="75px" DataField="CourierName" HeaderText="Empresa" >
<ItemStyle Width="75px"></ItemStyle>
        </asp:BoundField>
        <asp:BoundField ItemStyle-Width="300px" DataField="NameLastname" HeaderText="Receptor" >
<ItemStyle Width="300px"></ItemStyle>
        </asp:BoundField>
        <asp:CheckBoxField DataField="CashOnDelivery" HeaderText="Contra Reembolso">
        <ItemStyle HorizontalAlign="Center" />
        </asp:CheckBoxField>
        <asp:TemplateField HeaderText="Detalles" >
            <ItemTemplate>
                <asp:ImageButton ID="btnDetail" runat="server"  ImageUrl="../dist/img/plus.png" OnClick="btnDetail_OnClick"  />
                <%--<asp:Panel ID="pnlDetail" runat="server" Style="display: none">
                    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="false" CssClass = "table-bordered ">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="NameLastname" HeaderText="Nombre y Apellido"/>
                            <asp:BoundField ItemStyle-Width="150px" DataField="Phone" HeaderText="Teléfono" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Email" HeaderText="Email" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Passport" HeaderText="CI" />

                             <asp:BoundField ItemStyle-Width="150px" DataField="AddressLine1" HeaderText="Dirección" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityStateName" HeaderText="Departamento" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityCityName" HeaderText="Ciudad" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="LocalityName" HeaderText="Localidad" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>--%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

   
&nbsp;


         <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
              
    
</asp:Content>
