<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addItem.aspx.cs" Inherits="GOA.Basic.PEEBIAddInfo" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function btnOk() {


            var d = document.getElementById("dpdPEEBITYPE"); //根据DropDownList的客户端ID获取该控件
            var typeValue = d.options[d.selectedIndex].text; //获取DropDownList当前选中值
            if (typeValue == "--请选择数据类型--") {
                alert("请选择字段类型");
                return;
             }
            
            var ret = new Array(3);
            ret[0] = document.getElementById("TextBox1").value;
            ret[1] = document.getElementById("TextBox2").value;
            if (ret[0] == "") {
                alert("请填写字段名");
                return;
             }
             if (ret[1] == "") {
                 alert("请填写字段说明");
                 return;
             }
            ret[2] = typeValue;
            window.returnValue = ret;
            window.close();

            return false;
        }
    
    
    </script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        字段名：<asp:TextBox
            ID="TextBox1" name="addItemFlagName" runat="server"></asp:TextBox>
        字段说明:<asp:TextBox
            ID="TextBox2" name="addItemFullName" runat="server"></asp:TextBox>
        字段类型:<asp:DropDownList  ID = "dpdPEEBITYPE" runat = "server" Width = "130px"></asp:DropDownList>  
         <asp:Button ID="btnInputOk" runat="server" Text="确定" OnClientClick="return btnOk()" Width="26" />
    </div>
    </form>
</body>
</html>
