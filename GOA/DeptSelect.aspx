<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeptSelect.aspx.cs" Inherits="GOA.DeptSelect" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>部门选择</title>
  <base target="_parent" />
  <link href="../styles/dntmanager.css" type="text/css" rel="stylesheet" />
  <link href="../styles/StyleSheet.css" type="text/css" rel="stylesheet" />   
  <script language="JavaScript1.2" src="../JScript/modalpopupNoPageLoad.js" type="text/javascript"></script>
  <script language="JavaScript1.2" src="../JScript/common.js" type="text/javascript"></script>
  <script type="text/javascript">
                function btnSelectClick()
                {
                      //var ret = new Array(2); 
                      //ret[0] = "aa"; 
                      //ret[1] = "bb"; 
                      window.returnValue = findSelectedNodeValue(); 
                      window.close();
                      return false;
                }
                function findSelectedNodeValue()
                {
                  var ret = new Array(2); 
                  //ret[0] = "aa"; 
                  //ret[1] = "bb";
                  // 节点值存储方式
                  var sltNVSite = document.getElementById('treeDeptartment');
                 
                  var ptn = sltNVSite.selectedIndex == 0 ? "nv=(\\S+)" : "javascript:void\\('(\\S+)'\\)"
                  var regx = new RegExp(ptn, "i");
                  
                  // 获取指定 TreeView 内的 checkbox
                  var eles = treeDeptartment.getElementsByTagName("input");
                  var e1, e2;
                  var eId;
                  var departmentIds = "";
                  var departmentNames = "";
                  var mt;
                  // 遍历
                  for(var i = 0; i < eles.length; i ++)
                  {
                      e1 = eles[i];        
                      if(e1.type == "checkbox" && e1.checked) 
                      {
                          eId = "treeDeptartmentt" + e1.id.match(/treeDeptartmentn(\d+)CheckBox/i)[1];
                          
                          e2 = document.getElementById(eId);  
                          if(e2)
                          {
                              departmentIds += e2.getAttribute('href') +",";
                              departmentNames += e2.innerText + ",";
                          }
                      }
                  }
                  //alert("您选中的节点值：" + departmentIds);
                  //alert("您选中的节点值：" + departmentNames);
                  if(departmentIds.length == 0) 
                  {
                      ret[0]="";
                      ret[1]="";
                  }
                  else
                  {
                      // 去掉末尾 ,
                      departmentIds = departmentIds.slice(0, departmentIds.length - 1);
                      departmentNames = departmentNames.slice(0, departmentNames.length - 1);
                      ret[0]=departmentIds;
                      ret[1]=departmentNames;
                  }
                  return ret;
                }               
                //用于给TreeView的 chebox添加 子父节点的交替出现处理事件( 只需调用下面方法)
                function SetTreeNodeChildrenParentNodeHandle(treeID)
                {
                    var objs = document.getElementsByTagName("input");
                    for(var i=0;i<objs.length;i++)
                    {
                        if(objs[i].type=='checkbox')
                        {
                            var obj=objs[i];
                            if(obj.id.indexOf(treeID)!=-1)
                            {
                                objs[i].onclick=function(){client_OnTreeNodeChecked(this);};
                            }
                        }
                    }
                }
                function client_OnTreeNodeChecked(evt){
                  var obj = getCurrentNode(evt);
                  var treeNodeFound = false;
                  var checkedState;
                  if (obj.tagName == "INPUT" && obj.type == "checkbox" ){
                      checkedState = obj.checked;
                      var curNode = getParentNode(obj);
                      changeNodeState(curNode,checkedState,obj);
                  }
                }
                function getCurrentNode(evt){
                    var obj;
                    if(window.event)    obj = window.event.srcElement;
                    else                obj = (evt ? evt : (window.event ? window.event : null)).target; 
                    return obj;
                }

                function getParentNode(node){
                    do
                    {
                      node = node.parentNode;
                    } 
                    while (node.tagName != "TABLE")
                    return node;
                }

                function changeNodeState(node,state,curNode){
                   //change all the children
                   if(node.nextSibling!=null && node.nextSibling.tagName=="DIV"){
                        var cbArr = node.nextSibling.getElementsByTagName("INPUT");//得到所有的子结点
                        if(state){//如果为真，所有子结点为假
                            for(var i=0; i<cbArr.length; i++){
                                if(cbArr[i].type == "checkbox")   cbArr[i].checked = false;
                            } 
                        }
                        //否则不动   
                   }
                   //change its parents' state
                   var flag = true;
                   var inputArr = node.parentNode.getElementsByTagName("INPUT");//父结点
                   if(state){
                        for(var i=0; i<inputArr.length; i++){
                            if(GetNodeValue(inputArr[i]).length==GetNodeValue(curNode).length) //和其是兄弟节点
                            {
                                if(inputArr[i].type == "checkbox" && !inputArr[i].checked)  flag = false;   
                            }
                        }
                   }
                   else{
                    flag = true;            
                   }
                   var parentArr;
                   if(flag){
                      //其兄弟节点全部选中，则父结点选中，兄弟节点全部不选                      parentArr = node.parentNode.previousSibling.getElementsByTagName("INPUT");
                      for(var i=0; i<parentArr.length; i++){
                        //if(parentArr[i].type == "checkbox"){parentArr[i].checked = true; }
                      }
                      inputArr = node.parentNode.getElementsByTagName("INPUT");
                      for(var i=0; i<inputArr.length; i++){
                        //if(inputArr[i].type == "checkbox" ) { inputArr[i].checked = false;}
                      }
                   }
                   else
                   {
                      //否则父结点消失
                      parentArr = node.parentNode.previousSibling.getElementsByTagName("INPUT");
                      for(var i=0; i<parentArr.length; i++){
                          //if(parentArr[i].type == "checkbox") parentArr[i].checked = false;
                      }                
                   }

                }
                function GetNodeValue(curNode)
                {
                    var e1, e2;
                    e1 = curNode; 
                    
                    if(e1.type == "checkbox")  //&& e1.checked
                    {
                       eId = "treeDeptartmentt" + e1.id.match(/treeDeptartmentn(\d+)CheckBox/i)[1];
                       e2 = document.getElementById(eId);  
                       if(e2) {
                           return e2.getAttribute('href');                         
                       }
                    }
                }
  </script>

</head>
<body>
  <form id="form1" runat="server">
  <ajaxToolkit:ToolkitScriptManager runat="Server" EnablePartialRendering="true" ID="ScriptManager1" />
  <div>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
      <ContentTemplate>
        <div class="edit">
          <asp:Button ID="btnDepartmentSelect" runat="server" Text="选 择" OnClientClick="return btnSelectClick()" Width="80" CssClass="OkButtonCSS" />
        </div>
        <div style="margin-left: 80px;">
          <asp:Panel ID="panelTree" runat="server" ScrollBars="Auto">
            <asp:TreeView ID="treeDeptartment" runat="server" ShowCheckBoxes="All" OnTreeNodePopulate="treeDept_TreeNodePopulate" ShowLines="true">
            </asp:TreeView>
          </asp:Panel>

          <script type="text/javascript" language="javascript">
		               SetTreeNodeChildrenParentNodeHandle("<%=treeDeptartment.ClientID%>");
          </script>

        </div>
      </ContentTemplate>
    </asp:UpdatePanel>
  </div>
  </form>
</body>
</html>
