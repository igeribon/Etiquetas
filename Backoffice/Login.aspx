<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Backoffice.Login" %>



  <head>
    <meta charset="UTF-8">
    <title>MILGENIAL | Login</title>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <!-- Bootstrap 3.3.2 -->
    <link href="../../bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" runat="server" />
    <!-- Font Awesome Icons -->
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" runat="server"/>

          <link href="../../dist/css/skins/_all-skins.min.css" rel="stylesheet" type="text/css" runat="server" />

    <!-- Theme style -->
    <link href="../../dist/css/AdminLTE.min.css" rel="stylesheet" type="text/css" runat="server" />
    <!-- iCheck -->
    <link href="../../plugins/iCheck/square/blue.css" rel="stylesheet" type="text/css" runat="server" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
    <![endif]-->
  </head>
  <body class="login-page" runat="server">
    <div class="login-box" runat="server">
      <div class="login-logo">
       <b>MILGENIAL</b>
      </div><!-- /.login-logo -->
      <div class="login-box-body">
        <p class="login-box-msg"></p>
          <form id="form1" runat="server">
          <div class="form-group has-feedback">
              <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server" placeholder="Username"/>
            <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
          </div>
          <div class="form-group has-feedback">
            <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" placeholder="Password"  TextMode="Password"/>
            <span class="glyphicon glyphicon-lock form-control-feedback"></span>
          </div>
          <div class="row">
            <div class="col-xs-8">    
              
               
            </div><!-- /.col -->
            <div class="col-xs-4">

                 <asp:Button ID="btnLogin" cssclass="btn btn-secondary btn-block btn-flat" runat="server" Text="Login" OnClick="btnLogin_Click" />
            </div><!-- /.col -->
          </div>
          </form>


      </div><!-- /.login-box-body -->
         <asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
              
    </div><!-- /.login-box -->

    <!-- jQuery 2.1.3 -->
    <script src="../../plugins/jQuery/jQuery-2.1.3.min.js"></script>
    <!-- Bootstrap 3.3.2 JS -->
    <script src="../../bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
    <!-- iCheck -->
  
  </body>
