<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDocument.aspx.cs" Inherits="GOA.Basic.MyDocument" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">

        function EditOffice(fullname, tailName) {
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("Office.aspx?ImgPath=" + escape(fullname) + "&tailName=" + escape(tailName));
            var returnValue = window.showModalDialog(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");

        }

        function GetFiles(fullname) {
            var tailName;
            var top = screen.height - 70;
            var left = screen.width - 10;
            var src = encodeURI("ShowFiles.aspx?ImgPath=" + escape(fullname));
            var returnValue = window.open(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");

           
        }
        
        function GetValue(fullname) {

            var index = fullname.lastIndexOf(".");
            var name = fullname.substring(index + 1);
            if (name == "xls" || name == "doc" || name == "ppt" || name == "docx" || name == "xlsx" || name == "pptx") {
                EditOffice(fullname, name);
            }
            else if (name == "jpg") {

                EditImg(fullname)
            }
            else if (name == "txt") {
                document.all.FramerControl1.close();
                var fso, f1, ts, s;
                var ForReading = 1;
                fso = new ActiveXObject("Scripting.FileSystemObject");
                ts = fso.OpenTextFile(fullname, ForReading);
                s = ts.ReadLine();
                alert("File contents = '" + s + "'");
                ts.Close();
            }
            else {

                alert("无法编辑~");
            }
        }

    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
  </cc1:ToolkitScriptManager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      <div style="margin-left: 0px; margin-top: 0px;">
        <div class="ManagerForm">
          <fieldset>
            <legend style="background: url(../../images/legendimg.jpg) no-repeat 6px 50%;">
              <asp:Label ID="lblBigTitle" runat="server" Text="我的文件"></asp:Label></legend>
            
            
            <div class="clear">
              <div class="half">
                <div style="text-align: left; float: left; width:20%">
                  <asp:TreeView ID="RightTree" ForeColor="LightSlateGray" LeafNodeStyle-ForeColor="#3333ff" ShowLines="true"  ExpandDepth="1" runat="server" OnTreeNodePopulate="TreeNodePopulate">
                  </asp:TreeView>
                </div>
              </div>
          </fieldset>
        </div>
      </div>
    </ContentTemplate>
    <Triggers>
       <asp:PostBackTrigger ControlID="RightTree" />
   </Triggers> 
  </asp:UpdatePanel>
    

    </div>
    
    
    </form>
</body>
</html>
