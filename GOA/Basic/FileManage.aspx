<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileManage.aspx.cs" Inherits="GOA.Basic.FileManage" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolKit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>文件管理</title>
 
     <script language="javascript" type="text/javascript">
     
         function EditImg(fullname) {
             var top = screen.height - 70;
             var left = screen.width - 10;
             var src = encodeURI("Default.aspx?ImgPath=" + escape(fullname) + "&EditWidth=248&EditHeight=248");
             var returnValue = window.showModalDialog(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:650px;dialogHeight:485px");
          //  document.getElementById("showPath").innerHTML = returnValue;
         //   var showSrc = returnValue.substring(returnValue.lastIndexOf('\\') + 1)//这里只是临时看效果而已，根据自己的需要自行修改
          //  document.getElementById("showImg").src = showSrc

         }

         function EditOffice(fullname, tailName) {
             var top = screen.height - 70;
             var left = screen.width - 10;
             var src = encodeURI("Office.aspx?ImgPath=" +escape(fullname) + "&tailName="+escape(tailName));
             var returnValue = window.showModalDialog(src, "", "scroll:1;status:0;help:0;resizable:1;dialogWidth:850px;dialogHeight:585px");

         }

         function SetPermission(FolderId) {
             var top = screen.height - 70;
             var left = screen.width - 10;
             var src = encodeURI("SetFilePermissions.aspx?GUID=" + Math.random() + "&FolderId=" + escape(FolderId));
             var returnValue = window.open(src, "FileManage", "height=760,width=760,toolbar=no,menubar=no,scrollbars=no,resizable=yes,location=no,status=no");
           
         }

       function GetValue(fullname) {
          
             var index = fullname.lastIndexOf(".");
             var name = fullname.substring(index + 1);
             if (name == "xls" || name == "doc" || name == "ppt" || name == "docx" || name == "xlsx" || name == "pptx") {
                 EditOffice(fullname,name);
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

// 点击复选框时触发事件
function postBackByObject()
{
    
var ele = event.srcElement; 
if(ele.type=='checkbox') 
{ 
var childrenDivID = ele.id.replace('CheckBox','Nodes'); 
var div = document.getElementById(childrenDivID); 
if(div==null)return; 
var checkBoxs = div.getElementsByTagName('INPUT'); 
for(var i=0;i<checkBoxs.length;i++) 
{ 
if(checkBoxs[i].type=='checkbox') 
checkBoxs[i].checked=ele.checked; 
} 
} 

}

    //checkbox点击事件
    function OnCheckEvent() {
        var objNode = event.srcElement;
        if (objNode.tagName != "INPUT" || objNode.type != "checkbox") {

        } else {
            //获得当前树结点
            var ck_ID = objNode.getAttribute("ID");
            var node_ID = ck_ID.substring(0, ck_ID.indexOf("CheckBox")) + "Nodes";
            var curTreeNode = document.getElementById(node_ID);
            //级联选择

            SetChildCheckBox(curTreeNode, objNode.checked);
            SetParentCheckBox(objNode);
        }
    }
    
    //子结点字符串
    var childIds = "";
    //获取子结点ID数组
    function GetChildIdArray(parentNode)
    {
        if (parentNode == null)
            return;
        var childNodes = parentNode.children;
        var count = childNodes.length;
        for(var i = 0;i < count;i ++)
        {
            var tmpNode = childNodes[i];
            if(tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox")
            {
                childIds = tmpNode.id + ":" + childIds;
            }
            GetChildIdArray(tmpNode);
        }
    }
    
    //设置子结点的checkbox
    function SetChildCheckBox(parentNode,checked)
    {
        if(parentNode == null)
            return;
        var childNodes = parentNode.children;
        var count = childNodes.length;
        for(var i = 0;i < count;i ++)
        {
            var tmpNode = childNodes[i];
            if(tmpNode.tagName == "INPUT" && tmpNode.type == "checkbox")
            {
                tmpNode.checked = checked;
            }
            SetChildCheckBox(tmpNode,checked);
        }
    }
    
    //设置父结点的checkbox
    function SetParentCheckBox(childNode)
    {
        if(childNode == null)
            return;
        var parent = childNode.parentNode;
        if(parent == null || parent == "undefined")
            return;
        do 
        {
        parent = parent.parentNode;
        }
        while (parent && parent.tagName != "DIV");
        if(parent == "undefined" || parent == null)
            return;
        var parentId = parent.getAttribute("ID");
        var objParent = document.getElementById(parentId);
        childIds = "";
        GetChildIdArray(objParent);
        //判断子结点状态

        childIds = childIds.substring(0,childIds.length - 1);
        var aryChild = childIds.split(":");
        var result = false;
        //当子结点的checkbox状态有一个为true，其父结点checkbox状态即为true,否则为false
        for(var i in aryChild)
        {
            var childCk = document.getElementById(aryChild[i]);
            if(childCk.checked)
                result = true;
        }
        parentId = parentId.replace("Nodes","CheckBox");
        var parentCk = document.getElementById(parentId);
        if(parentCk == null)
            return;
        if(result)
            parentCk.checked = true;
        else
            parentCk.checked = false;
        SetParentCheckBox(parentCk);
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
              <asp:Label ID="lblBigTitle" runat="server" Text="文件管理"></asp:Label></legend>
            
            
            <div class="clear">
              <div class="half">
                <div style="text-align: left; float: left; width:20%">
                  <asp:TreeView ID="RightTree" ForeColor="LightSlateGray" LeafNodeStyle-ForeColor="#3333ff" ShowLines="true"  ExpandDepth="1" runat="server" OnTreeNodePopulate="TreeNodePopulate" ShowCheckBoxes="All" >
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
