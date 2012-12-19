<%@ Page Language="C#" MasterPageFile="~/tools/tools.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="GPRPWeb.tools.Menu" Title="无标题页" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div style="float:left;width:100%;">
         <div class="title">分类</div>
         <!--left menu start-->
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <div class="titlebox" style="float:left;width:50%;">
        <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="1" ImageSet="Arrows"  OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
            <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                VerticalPadding="0px" />
            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
        </div>
    <div style="float:left;width:40%;">
       
                 <div>
                <p>
                    <asp:Label ID="lblForInsert" runat="server" Text="Label"></asp:Label> : <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;代码为：<asp:Label ID="lblParentCode" runat="server" Text="Label"></asp:Label>&nbsp;</p>
                     <p>
                         名称(简体)：<cc2:TextBox ID="txtTitleOne" runat="server" RequiredFieldType="暂无校验"
                        Width="209px"></cc2:TextBox></p>
                         <p>
                         名称(繁体)：<cc2:TextBox ID="txtTitleTW" runat="server"  RequiredFieldType="暂无校验"
                        Width="209px"></cc2:TextBox></p>
                         <p>
                         名称(英文)：<cc2:TextBox ID="txtTitleEn" runat="server"  RequiredFieldType="暂无校验"
                        Width="209px"></cc2:TextBox></p>
                <p>代码：<cc2:TextBox ID="txtCodeOne" runat="server" RequiredFieldType="暂无校验"></cc2:TextBox><asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label></p>
                <p>
                         连接页面：<cc2:TextBox ID="txtLink" runat="server"></cc2:TextBox></p>
                     <p>
                         框架名：<cc2:TextBox ID="txtiFrame" runat="server"></cc2:TextBox></p>
                     <p>
                         显示顺序：<cc2:TextBox ID="txtOrderby" runat="server" RequiredFieldType="暂无校验"></cc2:TextBox></p>
                     <p>
                         &nbsp;</p>
                    <p>
                        是否可用:&nbsp;
                        <cc2:RadioButtonList ID="chkValiad" runat="server">
                     </cc2:RadioButtonList></p>
                        <p>
                        与流程相关:&nbsp;
                        <cc2:RadioButtonList ID="chkIsProcess" runat="server">
                     </cc2:RadioButtonList></p>
                   <asp:Button ID="btnOKOne" runat="server" Text="insert" OnClick="OKOne_Click" />
                   <asp:Button ID="btnCancelOne" runat="server" Text="delete" />
                   <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" /></div>
    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnOKOne" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelOne" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
     
     </div>
        
    



      
               
   
           
        
</asp:Content>
