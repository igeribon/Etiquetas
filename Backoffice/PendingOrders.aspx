<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PendingOrders.aspx.cs" Inherits="Backoffice.PendingOrders" EnableEventValidation="false" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../dist/img/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../dist/img/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>--%>


<%--     <script type="text/javascript">
         function openModal() {
             $('#myModal').modal('show');
         }
     </script>--%>

</asp:Content>


      


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

    <style type="text/css">
.modal {
  text-align: center;
  padding: 0!important;
}
.modal:before {
  content: '';
  display: inline-block;
  height: 100%;
  vertical-align: middle;
  margin-right: -4px;
}
.modal-dialog {
  display: inline-block;
  text-align: left;
  vertical-align: middle;
}
        .auto-style1 {
            margin-bottom: 0px;
        }
        .auto-style2 {
            width: 98%;
        }
        .auto-style3 {
            width: 97%;
        }
        .auto-style4 {
            width: 96%;
        }
        .auto-style5 {
            width: 50%;
        }
    </style>

    <strong> <big>Pendientes de impresión</big> </strong>



    <p>
        <table border="0" width="60%">  

        <tr>  
         <td class="auto-style5">  
                 Desde: <asp:TextBox ID="txtFrom" runat="server" textmode="Date"></asp:TextBox> 
         </td>  
         <td class="auto-style4">  
             Hasta: <asp:TextBox ID="txtTo" runat="server" textmode="Date"></asp:TextBox>
         </td>  


         <td class="auto-style3">  
            <asp:Button ID="btnAplicar" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Aplicar" OnClick="btnAplicar_OnClick" Width="73px"/>
         </td>  

         <td width="10%" class="auto-style1">  
             
             &nbsp;</td>  
     </tr>  
            </table>
        
  
        <table border="0" class="auto-style2">  

        <tr>  


         <td style="text-align: right">  
        
  
         <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
              

             

         </td>  

     </tr>  
            </table>
        
  
             

</p>
   
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
        <asp:BoundField DataField="FinancialStatus" HeaderText="Estado" />
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
          <asp:TemplateField>
            <ItemTemplate>
               <asp:Button ID="btnGenerateLabel" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Genearar Etiqueta" OnClick="btnGenerateLabel_Click" data-toggle="modal" data-target="#myModal"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



&nbsp;


     <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js"></script>

    <div id="myModal" class="modal">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    
                    <h4 class="modal-title">
                        Generando etiqueta</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Aguarde un momento</p>

                </div>

                </div>
            </div>
        </div>
    </div>
 

</asp:Content>
