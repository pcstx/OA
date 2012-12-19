<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addItem.aspx.cs" Inherits="GOA.Basic.addItem" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function btnOk() {


            var d = document.getElementById("dpdPEEBITYPE"); //根据DropDownList的客户端ID获取该控件
            var typeValue = d.options[d.selectedIndex].text; //获取DropDownList当前选中值
            if (typeValue == "--请选择数据类型--") {
                alert("请选择字段类型");
                d.focus();
                return false ;
             }
            
            var ret = new Array(3);
            ret[0] = document.getElementById("TextBox1").value;
            ret[1] = document.getElementById("TextBox2").value;
            if (ret[0] == "") {
                alert("请填写字段名");
                return false ;
             }
             if (ret[1] == "") {
                 alert("请填写字段说明");
                 return false ;
             }

            return true ;
        }
    
    
    </script>
    
    
</head>
<body>


  <form id="form1" runat="server">
   
    
    
    <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server" EnablePageMethods="True">
    </cc1:toolkitscriptmanager>
        
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
             <div class="ManagerForm">
         	<fieldset>		
		      <legend style="background:url(../images/legendimg.jpg) no-repeat 6px 50%;"><asp:Label ID="lblBigTitle" runat="server" Text=""></asp:Label>
		      </legend>

        字段名：<asp:TextBox
            ID="TextBox1" name="addItemFlagName" runat="server"></asp:TextBox>字段说明:<asp:TextBox
            ID="TextBox2" name="addItemFullName" runat="server"></asp:TextBox>字段类型:<asp:DropDownList  ID = "dpdPEEBITYPE" runat = "server" Width = "130px"></asp:DropDownList>  
                <asp:Button ID="btnADD" runat="server"  Text="增加" Width="50px" Height="26px"   ScriptContent="btnOk() " OnClick="Btn_Click"/>
                <asp:Button ID="btnDele" runat="server"  Text="删除" Width="50px" Height="26px"  OnClick="btnDelete_Click"/>
            <!--page end-->
            <!--gridview start-->
                <!--gridview Browse mode start-->
            <yyc:SmartGridView ID="GridView1" runat="server" AllowPaging="False" AutoGenerateColumns="False" PageSize="20"  OnRowCommand="GridView1_RowCommand" onrowcreated="GridView1_RowCreated" OnRowDataBound="GridView1_RowDataBound"
                           Width="80%" BoundRowDoubleClickCommandName="" MergeCells=""  DataKeyNames="ColName">
                  <Columns>
                      <asp:TemplateField>
                        <HeaderTemplate>
                          序号
                        </HeaderTemplate>
                        <ItemTemplate>
                          <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                      
                      <asp:TemplateField>
                        <HeaderTemplate>
                          选择
                          </HeaderTemplate>
                        <ItemTemplate>
                          <asp:CheckBox ID="Item" runat="server" />
                        
                        </ItemTemplate>
                        <ItemStyle Width="35px" />
                      </asp:TemplateField>
                
                      
                       <asp:BoundField DataField="ColName" HeaderText="字段名"  />
                       <asp:BoundField DataField="ColType" HeaderText="字段类型" />
                        <asp:BoundField DataField="ColDescriptionCN" HeaderText="字段说明" />
                              
                
                    </Columns>
                
                
                  <SmartSorting AllowMultiSorting="True" AllowSortTip="True" />
                       <CascadeCheckboxes>
                          <yyc:CascadeCheckbox ChildCheckboxID="item" ParentCheckboxID="all" />
                      </CascadeCheckboxes>
                      <FixRowColumn FixRowType="Header,Pager" TableHeight="408px" TableWidth="100%" FixColumns="" />
                      <CheckedRowCssClass CheckBoxID="item" CssClass="SelectedRow" />
                       
             </yyc:SmartGridView>
            
             
             <!--gridview Browse mode end-->
          
             <!--grid view end--> 

		</fieldset>
     </div> 
     
       <div id="StatisticDate"  runat="server">
       </div>
        </ContentTemplate> 
     </asp:UpdatePanel>
     
      <!--  Hint  start -->
     <cc2:Hint id="Hint1" runat="server" HintImageUrl="../images" WidthUp="200"></cc2:Hint>
      <!--  Hint  end -->
     <cc1:UpdatePanelAnimationExtender ID="upae" BehaviorID="animation" runat="server" TargetControlID="UpdatePanel1">
        </cc1:UpdatePanelAnimationExtender> 
    </form>
</body>
</html>
