<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GG70Add.aspx.cs" ValidateRequest="false"  
 Inherits="GOA.GG70Add" %>

<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>数据集配置</title>
    <link href="../App_Themes/Default/dntmanager.css" type="text/css" rel="stylesheet" />
    <link href="../App_Themes/Default/StyleSheet.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .tableStyle
        {
            overflow: scroll;
            width: 350px;
            padding: 0px 0px 0px 0px;
            text-align: left;
            white-space: nowrap;
        }
    </style>

    <script language="javascript" type="text/javascript">
         function $( id ){return document.getElementById( id );}
         String.prototype.Trim = function() {return this.replace(/(^\s*)|(\s*$)/g, ''); };
          function CheckInputContent()
          {  
            if($("txtDSName").value.Trim() == "")
            {
              alert("请输入数据集名称！");
              return false;
            }
                
            if($("txtDataSourceID").value.Trim() == "")
            {
              alert("请选择数据源，并设置数据集查询字串！");
             return false;         
             }
             return true;
    
          }

         function DataSourceSelect()
         {
            var url = "DataSourceSelect.aspx?GUID=" + Math.random() ;
            url+="&datasourceid=" +( ($("txtDataSourceID").value.Trim()=="")?"-1": ($("txtDataSourceID").value.Trim()));
      
            var ret = window.showModalDialog(url, 'newwin', 'dialogHeight: 500px; dialogWidth: 700px;  edge: Raised; center: Yes; help: No;');
            if ( (ret!=null ) &&  ( ret != Object ))
            { 
              $("txtDataSourceID").value=(ret[0]=="-1")?"":ret[0];
              $("txtDataSourceName").value=(ret[0]=="-1")?"":ret[1];
            }
         }
           
           function btnReturn_Click()
           {
            window.location="GG70.aspx";
           }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:updatepanel id="UpdatePanel2" runat="server">
        <contenttemplate> 
              <div >
          <cc2:Button ID="btnSubmit" ShowPostDiv="true" runat="server" Text="Button_Save"  ValidateForm="false"   ButtonImgUrl="../images/submit.gif" AutoPostBack="true"  Page_ClientValidate="false" Disable="false"  ScriptContent="CheckInputContent()"  OnClick="btnOK_Click"  />
          <cc2:Button ID="btnReturn" ShowPostDiv="true" runat="server" Text="Button_Return"  ValidateForm="false"  ButtonImgUrl="../images/browse.gif"   AutoPostBack="false"   Page_ClientValidate="false" Disable="false"  ScriptContent ="btnReturn_Click()"  />
           <cc2:Button ID="btnClear" ShowPostDiv="true" runat="server" Text="Button_Reset"   ValidateForm="false"  AutoPostBack="true" ButtonImgUrl="../images/cancel.gif"  Page_ClientValidate="false" Disable="false"   OnClick="btnClear_Click" />
        </div>    
               <table cellpadding="0" cellspacing="0" border="0" style=" overflow:scroll ; width:100%;  height:100%" >
          <tr><td  style="width:30%; height:100%"  align="left" valign="top" >
    	<fieldset>		
		<legend style=" "><asp:Label ID="lblBigTitle" runat="server" Text="表名字段选择区"></asp:Label></legend>
		  <div class="clear">
              <div class="oneline"><label class="char5">数据源：</label>
              <div class="iptblk"><cc2:TextBox ID="txtDataSourceName" runat="server"  Enabled="false" Width="150" ></cc2:TextBox><span style="color:Red;">*</span> <cc2:Button  runat="server" ID="btnConnectStringSelect" ButtonImgUrl="images/add.gif" ShowPostDiv="false"  Text="Button_Set" AutoPostBack="false" Width="80px"  ValidateForm="false"  ScriptContent="DataSourceSelect()"  Page_ClientValidate="false" /></div></div>
            </div>
               
             <div class="clear">
				  <div class="oneline"><label class="char5">数据集类型：</label>
				  <div class="iptblk">
				  <cc2:DropDownList ID="ddlDSType" runat="server" Width="150px" AutoPostBack="true"  OnSelectedIndexChanged="ddlDSType_SelectedIndexChanged">
				   <asp:ListItem Value="0">请选择</asp:ListItem>
				  <asp:ListItem Value="1" Text="SQL语句"></asp:ListItem>
				  <asp:ListItem Value="2" Text="存储过程"></asp:ListItem>
				  </cc2:DropDownList>
				  </div></div>
			 </div>
			 <div class="clear">
              <div class="oneline"><label class="char5">数据集名称：</label><div class="iptblk"><cc2:TextBox ID="txtDSName" runat="server" Width="150"></cc2:TextBox><span style="color:Red;">*</span></div></div>
            </div>
             <div class="clear">
				  <div class="oneline"><asp:Label ID="lblTable" class="char2" runat="server" Text="表"></asp:Label>
				  <div class="iptblk"><cc2:DropDownList ID="ddlTable" runat="server" Width="270px" AutoPostBack="true"    OnSelectedIndexChanged="ddlTable_SelectedIndexChanged"></cc2:DropDownList></div></div>
				  </div>
				  <div  class="clear" runat="server" id="divColumn" >
                  <div class="oneline"><asp:Label ID="lblSPCol" class="char2" runat="server" Text="字段"></asp:Label>
                  </div><br />  
                   <div class="oneline" style="overflow:auto;  height:500px" >               
                 <asp:CheckBoxList ID="cblColumn" runat="server" Font-Size="8pt"  Width="320px" CssClass="tableStyle" CellPadding="0"  CellSpacing="0" AutoPostBack="false"   RepeatDirection="Vertical"   RepeatColumns="1" RepeatLayout="Table" BorderWidth="0"   TextAlign ="left"></asp:CheckBoxList>
              <div class="oneline"> <cc2:Button ID="btnSelect" runat="server" Page_ClientValidate="false" ShowPostDiv="false" ValidateForm="false"  Text="Button_Select" AutoPostBack="true"  Width="80"   OnClick ="btnSelect_OnClick" />   </div>           
    			</div> </div>
         </fieldset>
       </td>
         <td   style="width:70%" align="left" valign="top" >
          <div class="clear" >
				<div class="oneline"><label class="char7">字段描述(以","隔开)</label>
				<div class="iptblk"><asp:TextBox ID="txtColumnsName" runat="server" Width="600px" Height="50px"   CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
				</div></div>  
             </div>
             <div class="clear" >
				<div class="oneline"><label class="char7">数据集PK(以","隔开)</label>
				<div class="iptblk"><asp:TextBox ID="txtPK" runat="server" Width="600px"   CausesValidation="false"></asp:TextBox>
				</div></div>  
             </div>
             <div class="clear">
         	<fieldset runat="server" id="fdTable">		
		<legend style=""><asp:Label ID="Label1" runat="server" Text="数据集设置区"></asp:Label></legend> 
             <div class="clear" >
				<div class="oneline"><label class="char7">字段列表(全选请输 *)</label>
				<div class="iptblk"><asp:TextBox ID="txtFieldList" runat="server" Width="600px" Height="100px"   CausesValidation="false" TextMode="MultiLine"></asp:TextBox>
				</div></div>  
             </div>
              <div class="clear" >
				<div class="oneline"><label class="char7">表列表</label>
				<div class="iptblk"><asp:TextBox ID="txtTableList" runat="server" TextMode="MultiLine" Height="100" Width="600px"  CausesValidation="false" ></asp:TextBox></div></div>  
             </div>
              <div class="clear">
				<div class="oneline"><label class="char7">查询条件区</label>
				<div class="iptblk"><asp:TextBox ID="txtQueryCondition" runat="server" TextMode="MultiLine" Height="100px"  CausesValidation="false"  Width="600px"></asp:TextBox></div></div>  
             </div>
              <div class="clear" >
				<div class="oneline"><label class="char7">排序区</label>
				<div class="iptblk"><asp:TextBox ID="txtOrder" runat="server"  CausesValidation="false"  Width="600px"></asp:TextBox></div></div>  
             </div>
               <div class="clear" >
				<div class="oneline"><label class="char7"><asp:Label ID="lblSql"  runat="server"  Text="">自定义SQL编辑区</asp:Label></label>
				<div class="iptblk"><asp:TextBox ID="txtSql" TextMode="MultiLine"  CausesValidation="false"  runat="server" Width="600px" Height="120px"></asp:TextBox></div></div>  
             </div>
              <div class="clear"  style="text-align:left;">        <cc2:Button ID="btnParam" ShowPostDiv="true" runat="server" Text="Button_AddParam"   AutoPostBack="true" ButtonImgUrl="../images/add.gif"  Page_ClientValidate="false" Disable="false"  ValidateForm="false"  OnClick="btnParam_Click" />  <br />
            <asp:GridView  ID="GridView1" runat="server" AllowPaging="False" AllowSorting="false" AutoGenerateColumns="False"    EnableModelValidation="false"   OnRowCommand="GridView1_RowCommand"       OnRowDataBound="GridView1_RowDataBound">
              <Columns>
      <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>参数名称</HeaderTemplate>
                <itemtemplate>
                  <asp:TextBox ID="txtName" runat="server"  Text='<%# Bind("ParameterName") %>' />
                </itemtemplate>
              </asp:TemplateField>
              <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>数据类型</HeaderTemplate>
                <itemtemplate>
                  <asp:DropDownList ID="ddlType" runat="server"  ></asp:DropDownList>
                </itemtemplate>
              </asp:TemplateField>
              <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>参数方向</HeaderTemplate>
                <itemtemplate>
                  <asp:DropDownList ID="ddlDirection" runat="server" ></asp:DropDownList>
                </itemtemplate>
              </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>参数大小</HeaderTemplate>
                <itemtemplate>
                  <cc2:TextBox ID="txtSize" runat="server"  Text='<%# Bind("ParameterSize") %>'  RequiredFieldType="数据校验" />
                </itemtemplate>
              </asp:TemplateField>
          <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>参数值</HeaderTemplate>
                <itemtemplate>
                  <asp:TextBox ID="txtValue" runat="server"  Text='<%# Bind("ParameterValue") %>' />
                </itemtemplate>
              </asp:TemplateField>
               <asp:TemplateField ItemStyle-Width="80px">
                <HeaderTemplate>参数描述</HeaderTemplate>
                <itemtemplate>
                  <asp:TextBox ID="txtDesc1" runat="server"  Text='<%# Bind("ParameterDesc") %>' />
                </itemtemplate>
              </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="60px">
                <HeaderTemplate></HeaderTemplate>
                <itemtemplate>
                  <asp:LinkButton ID="btnSelect" runat="server"  Text="删除" CommandName="deleterow" CommandArgument='<%# Container.DataItemIndex %>' />
                </itemtemplate>
              </asp:TemplateField>
           </Columns>
            </asp:GridView>
			</div>				
         </fieldset>
         </div>	
         <div class="clear"></div>
         <fieldset runat="server" id="fdSP">		
		<legend ><asp:Label ID="Label2" runat="server" Text="数据集设置区"></asp:Label></legend> 
             <div class="clear" >
				<div class="oneline"><label class="char9">字段列表</label>
				<div class="iptblk"><cc2:TextBox ID="txtSPCol" runat="server"  ReadOnly="true" Width="600px" Height="100px" TextMode="MultiLine"></cc2:TextBox></div></div>  
             </div>
  <div class="clear" >
            <asp:GridView  ID="GridView2" runat="server" AllowPaging="False" AllowSorting="false" AutoGenerateColumns="False"    EnableModelValidation="false" 
          Width="100%"   DataKeyNames="ParameterName"  >
              <Columns>
          <asp:BoundField DataField="ParameterName" HeaderText="参数名称" />   
          <asp:BoundField DataField="ParameterType" HeaderText="参数数据类型" />   
          <asp:BoundField DataField="ParameterDirection" HeaderText="输入/输出" />  
           <asp:BoundField DataField="ParameterSize" HeaderText="参数大小" />  
          <asp:TemplateField>
                <HeaderTemplate>存储过程参数值</HeaderTemplate>
                <itemtemplate>
                  <asp:TextBox ID="txtSelect" runat="server"  Text='<%# Bind("ParameterValue") %>' />
                </itemtemplate>
              </asp:TemplateField>
             <asp:TemplateField>
                <HeaderTemplate>参数描述</HeaderTemplate>
                <itemtemplate>
                  <asp:TextBox ID="txtDesc" runat="server"  Text='<%# Bind("ParameterDesc") %>' />
                </itemtemplate>
              </asp:TemplateField>
           </Columns>
            </asp:GridView>
			</div>
         </fieldset></div>
         </td></tr>
        </table>
      </contenttemplate>
    </asp:updatepanel>
    <asp:hiddenfield id="txtDataSetID" runat="server" />
    <asp:hiddenfield id="txtDataSourceID" runat="server" />
    </form>
</body>
</html>
