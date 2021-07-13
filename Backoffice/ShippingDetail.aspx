<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="ShippingDetail.aspx.cs" Inherits="Backoffice.ShippingDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
        .auto-style4 {
            height: 25px;
            width: 9%;
        }
        .auto-style5 {
            margin-left: 0px;
        }
        .auto-style6 {
            height: 25px;
            width: 4%;
        }
    .auto-style7 {
        width: 84%;
    }
        </style>
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
        .auto-style4 {
            width: 96%;
        }
        .auto-style5 {
            width: 50%;
        }
        .auto-style8 {
            height: 25px;
            margin-bottom: 0px;
        }
        .auto-style9 {
            width: 5844px;
        }
        .auto-style11 {
            width: 326px;
        }
        .auto-style12 {
            width: 126px;
        }
        </style>

 
 <strong> <big>Detalles de órden</big> </strong>
    <p></p>
<table border="0" width="80%">  

        <tr>  
         <td class="auto-style6">  
             Nro. Orden   
         </td>  
         <td class="auto-style4">  
             <asp:TextBox ID="txtOrderId" runat="server" Width="225px" CssClass="auto-style5" ReadOnly="True"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
             Empresa  
         </td>  

         <td width="10%" class="auto-style1">  
             
             <asp:DropDownList ID="drpCourier" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCourier_SelectedIndexChanged">
             </asp:DropDownList>
             
         </td>  
     </tr>  
  
      <tr>  
         <td class="auto-style6">  
             Fecha  
         </td>  
         <td class="auto-style4">  
             <asp:TextBox ID="txtCreatedAt" runat="server" Width="225px" ReadOnly="True"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
           
                Contrarembolso</td>  

         <td width="10%" class="auto-style1">  
             
             <asp:CheckBox ID="chkCashOnDelivery" runat="server" />
             
         </td>  
     </tr>  
    <tr>
        
        
        <td class="auto-style6">  
           
            Info</td>  

            <td class="auto-style1" colspan="3">  
           
             <asp:TextBox ID="txtInfo" runat="server" Width="620px" TextMode="MultiLine" Rows="10" Height="53px" ></asp:TextBox>
         </td>  

    </tr>
    <tr>
        
        
        <td class="auto-style6">  
           
            Notas</td>  

            <td class="auto-style1" colspan="3">  
           
             <asp:TextBox ID="txtNote" runat="server" Width="620px" TextMode="MultiLine" Rows="10" Height="53px" ></asp:TextBox>
         </td>  

    </tr>
  <tr>         <td class="auto-style6">  
           <strong>Receptor</strong>
         </td>   </tr>
     <tr>  
         <td class="auto-style6">  
             Nombre   
         </td>  
         <td width="10%" class="auto-style1">  
             <asp:TextBox ID="txtName" runat="server" Width="225px"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
             Dirección  
         </td>  

         <td width="10%" class="auto-style1">  
             <asp:TextBox ID="txtLine1" runat="server" Width="225px"></asp:TextBox>
         </td>  
     </tr>  
   

      <tr>  
         <td class="auto-style6">  
             Apellido</td>  
         <td width="10%" class="auto-style1">  
             <asp:TextBox ID="txtLastName" runat="server" Width="225px"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
             Departamento  
         </td>  

         <td width="10%" class="auto-style1">  
             
             <asp:DropDownList ID="drpState" runat="server" Width="225px" AutoPostBack="True" OnSelectedIndexChanged="drpState_SelectedIndexChanged">
             </asp:DropDownList>
             
         </td>  
     </tr>  


      <tr>  
         <td class="auto-style6">  
             Teléfono   
         </td>  
         <td width="10%" class="auto-style1">  
             <asp:TextBox ID="txtPhone" runat="server" Width="225px"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
             Ciudad  
         </td>  

         <td width="10%" class="auto-style1">  
             
             <asp:DropDownList ID="drpCity" runat="server" Width="225px" AutoPostBack="True" OnSelectedIndexChanged="drpCity_SelectedIndexChanged">
             </asp:DropDownList>
             
         </td>  
     </tr>  



    
      <tr>  
         <td class="auto-style6">  
             Email   
         </td>  
         <td width="10%" class="auto-style1">  
             <asp:TextBox ID="txtEmail" runat="server" Width="225px"></asp:TextBox>
         </td>  


         <td class="auto-style6">  
             Localidad  
         </td>  

         <td width="10%" class="auto-style1">  
             
             <asp:DropDownList ID="drpLocality" runat="server" Height="22px" Width="225px" AutoPostBack="True">
             </asp:DropDownList>
             
         </td>  
     </tr>  


        
      <tr>  
         <td class="auto-style6">  
              
             CI   
                       
         </td>  
         <td width="10%" class="auto-style1">  
        
             <asp:TextBox ID="txtPassport" runat="server" Width="225px"></asp:TextBox>
        
         </td>  


         <td class="auto-style6">  
               
         </td>  

     
     </tr>  


      </table>  
    <asp:GridView ID="grdPackages" runat="server" CssClass="table table-bordered table-hover" Width="811px">
            <RowStyle CssClass="table-bordered" />
        <HeaderStyle CssClass="table-bordered"/>

        <Columns>
            <asp:BoundField DataField="Reference" HeaderText="Item" />
        </Columns>
    </asp:GridView>

    <table border="0" class="auto-style7">  

        <tr>  
         <td class="auto-style8"  style="text-align: right" colspan="4">  
 
  
         <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
              

             

            </td>  
     </tr>  
  

        <tr>  
         <td style="text-align: right" class="auto-style9">  
 
                 &nbsp;</td>  
         <td style="text-align: right" class="auto-style11">  
 
                 <asp:Button ID="btnGuardar" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Guardar" OnClick="btnGuardar_OnClick"/>

             &nbsp;</td>  
         <td style="text-align: right" class="auto-style12">  
 
             <asp:Button ID="btnGenerarEtiqueta" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Generar Etiqueta" OnClick="btnGenerarEtiqueta_OnClick" data-toggle="modal" data-target="#myModal" style="margin-left: 0px"/>
             
             &nbsp;</td>  
         <td style="text-align: right">  
 
             <asp:Button ID="btnCancelar" cssclass="btn btn-warning btn-block btn-flat" runat="server" Text="Volver" OnClick="btnCancelar_OnClick"/>
 
             &nbsp;</td>  
     </tr>  
  

        </table>
            
    <p>
       <iframe id="pdfiframe" name="pdfiframe" runat="server" style="width:718px; height:370px;" >
                    </iframe> 


         </p>
       
    
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
