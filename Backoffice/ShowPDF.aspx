<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPDF.aspx.cs" Inherits="Backoffice.ShowPDF" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <iframe src="../Label/Label.pdf"
  style="width:718px; height:700px;" frameborder="0"></iframe>
        </div>
    </form>
</body>
</html>
