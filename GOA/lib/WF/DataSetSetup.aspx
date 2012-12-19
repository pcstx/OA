<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataSetSetup.aspx.cs" Inherits="GOA.DataSetSetup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<%@ Register Assembly="GPRPControls" Namespace="GPRP.GPRPControls" TagPrefix="cc2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据集设置</title>

    <script language="javascript" type="text/javascript">
    
    function $( id ){return document.getElementById( id );}
    String.prototype.Trim = function() {return this.replace(/(^\s*)|(\s*$)/g, ''); };
    
    
     function CheckField() 
     {
        var strsql=$("txtQuerySql").value.Trim();
       if(strsql == "")
       {
         if($("txtTableList").value.Trim() == "" || $("txtFieldList").value.Trim() == ""  )
            {
              alert("请选择表、字段， 或 在自定义SQL区输入查询语句！");
              return false;
            }            
        }  
        else 
        {
            if (toLowerCase(strsql).indexOf("select")!=-1 || toLowerCase(strsql).indexOf("from")!=-1)
            {
              if($("txtTableList").value.Trim() != "" || $("txtFieldList").value.Trim() != "" || $("txtQueryCondition").value.Trim() != "" || $("txtOrderBy").value.Trim() != ""  )
                {
                  alert("已输入自定义SQL，将不考虑其他文本框中的内容！");
                  return true ;
                }                    
            } 
            else 
            {
              alert("自定义SQL语法不标准，请检查！");
                  return false  ;
            }
            return true ;
        }
      return true;            
     }
     
   function GetSQLRecords(datasetid)
     {
       PageMethods.GetSelectRecords(datasetid , GetSQLRecordsComplete);
     }
          
 function GetSQLRecordsComplete(result)
{
   if (result>0)
       alert("查询正常，可查询到 " + result +"  笔数据");
   
    else if (result == "-2")
              alert('查询出现异常，请检查SQL语句！');
    else 
              alert('查询不到任何数据，请检查SQL语句！');
          
    $("HiddenResult").value = result;
    
}
     

function OK_Once()
{

 if ( CheckField() )
         {
         var strsql="";
         var  strR=new Array(6);//用作返回数组
         strR[0]=$("HiddenDataSourceID").value.Trim();
             if ($("txtQuerySql").value.Trim() != "")
             {
                     strsql =$("txtQuerySql").value.Trim();
                     strR[1]=strsql;
                     var utrsql=strsql.toLowerCase();
                     var intselect=ustrsql.indexOf("select")+6;
                     var intfrom = ustrsql.indexOf("from") + 4;
                      var intwhere = ustrsql.indexOf("where") + 5;
                      var  intorder = ustrsql.indexOf(" order ");
                       var intorderby = ustrsql.replace(" ", "").indexOf("orderby") + 7;
                       
                       strR[2]=strsql.substring(intselect,intfrom-4-intselect );//columnlist
                       if (intwhere>0)
                       {
                         strR[3]=strsql.substring(intfrom, intwhere - 5 - intfrom);//tablelist
                             if (intorderby >0)
                             {
                             strR[4] = strsql.substring(intwhere, intorder - intwhere);//where
                             strR[5] = strsql.replace(" ", "").substring(intorderby);//orderby
                               }
                               else
                               {
                                        strR[4] = strsql.substring(intwhere);
                                        strR[5] = "";
                               }
                         }
                         else
                         {
                                strR[3] = strsql.substring(intfrom);
                                strR[4] = "";
                                strR[5] = "";
                         }              
             
               }
               else 
                 {
                        strR[2] = $("txtFieldList").value; //columnlist
                        strR[3] = $("txtTableList").value; 
                        strR[4] =$("txtQueryCondition").value;
                        strR[5] = $("txtOrderBy").value;
                        strR[1]=" select " +strR[2]+ " from " + strR[3] + ((strR[4].Trim().length>0)? ("  where " + strR[4]):"") + ((strR[5].Trim().length>0)?(" order by "+strR[5]):"");
                        
                 }
                 
                var strR2=new Array(2);
                 strR2[0]=strR[0];
                 strR2[1]=strR[1];
             GetSQLRecords(strR2);           
            
          }
}

          
     function OK_ReturnGG70()
     {
         if ( CheckField() )
         {
         
             if ($("HiddenResult").value.Trim()==null ||$("HiddenResult").value.Trim()=="" )
             {
             alert ("请先测试能否查询到数据!");
             return false;
             }
         var strsql="";
         var  strR=new Array(6);//用作返回数组
         strR[0]=$("HiddenDataSourceID").value.Trim();
             if ($("txtQuerySql").value.Trim() != "")
             {
                     strsql =$("txtQuerySql").value.Trim();
                     strR[1]=strsql;
                     var utrsql=strsql.toLowerCase();
                     var intselect=ustrsql.indexOf("select")+6;
                     var intfrom = ustrsql.indexOf("from") + 4;
                      var intwhere = ustrsql.indexOf("where") + 5;
                      var  intorder = ustrsql.indexOf(" order ");
                       var intorderby = ustrsql.replace(" ", "").indexOf("orderby") + 7;
                       
                       strR[2]=strsql.substring(intselect,intfrom-4-intselect );//columnlist
                       if (intwhere>0)
                       {
                         strR[3]=strsql.substring(intfrom, intwhere - 5 - intfrom);//tablelist
                             if (intorderby >0)
                             {
                             strR[4] = strsql.substring(intwhere, intorder - intwhere);//where
                             strR[5] = strsql.replace(" ", "").substring(intorderby);//orderby
                               }
                               else
                               {
                                        strR[4] = strsql.substring(intwhere);
                                        strR[5] = "";
                               }
                         }
                         else
                         {
                                strR[3] = strsql.substring(intfrom);
                                strR[4] = "";
                                strR[5] = "";
                         }              
             
               }
               else 
                 {
                        strR[2] = $("txtFieldList").value; //columnlist
                        strR[3] = $("txtTableList").value; 
                        strR[4] =$("txtQueryCondition").value;
                        strR[5] = $("txtOrderBy").value;
                        strR[1]=" select " +strR[2]+ " from " + strR[3] + ((strR[4].Trim().length>0)? ("  where " + strR[4]):"") + ((strR[5].Trim().length>0)?(" order by "+strR[5]):"");
                        
                 }
 
               window.returnValue=  strR ;
               window.close();
          }
     }
         
     
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </cc1:ToolkitScriptManager>
        <div>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <div class="ManagerForm">
                        <table cellpadding="0" cellspacing="0" border="0" style="overflow: scroll;">
                            <tr>
                                <td style="width: 220px; height: 100%" align="left" valign="top">
                                    <fieldset>
                                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 100%;">
                                            <asp:Label ID="lblBigTitle" runat="server" Text="表名字段选择区"></asp:Label></legend>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char2">
                                                    <asp:Label ID="lblDataSourceID" runat="server" Text="表"></asp:Label></label>
                                                <div class="third22">
                                                    <cc2:DropDownList ID="ddlTable" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlTable_SelectedIndexChanged">
                                                    </cc2:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char2">
                                                    <asp:Label ID="lblDataSourceName" runat="server" Text="字段"></asp:Label></label>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="online" style="overflow: auto; width: 220px">
                                            <cc2:CheckBoxList ID="cblColumn" runat="server" Font-Size="9pt" Width="100%" CellPadding="0" CellSpacing="0" AutoPostBack="false" RepeatDirection="Vertical" RepeatColumns="0" RepeatLayout="Table" BorderWidth="0" TextAlign="left">
                                            </cc2:CheckBoxList>
                                        </div>
                                        <div class="clear">
                                            <div class="oneline">
                                                <cc2:Button ID="btnSelect" runat="server" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_Select" AutoPostBack="true" Width="80" OnClick="btnSelect_OnClick" />
                                            </div>
                                        </div>
                                    </fieldset>
                                </td>
                                <td style="width: 580px" align="left" valign="top">
                                    <fieldset>
                                        <legend style="background: url(images/legendimg.jpg) no-repeat 6px 100%;">
                                            <asp:Label ID="Label1" runat="server" Text="数据集设置区"></asp:Label></legend>
                                        <div class="clear">
                                            <div class="oneline">
                                                <label style="width: 400px;">
                                                    <!--label-->
                                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></label></div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char9">
                                                    <asp:Label ID="Label3" runat="server" Text="字段列表(全选,请输 * )"></asp:Label></label>
                                                <div class="third22">
                                                    <cc2:TextBox ID="txtFieldList" runat="server" Width="380px" Height="100px" TextMode="MultiLine"></cc2:TextBox></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char9">
                                                    <asp:Label ID="Label5" runat="server" Text="表列表"></asp:Label></label>
                                                <div class="third22">
                                                    <cc2:TextBox ID="txtTableList" runat="server" Width="380px"></cc2:TextBox></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char9">
                                                    <asp:Label ID="Label2" runat="server" Text="查询条件区"></asp:Label></label>
                                                <div class="third22">
                                                    <cc2:TextBox ID="txtQueryCondition" runat="server" TextMode="MultiLine" Height="100px" Width="380px"></cc2:TextBox></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char9">
                                                    <asp:Label ID="Label6" runat="server" Text="排序区"></asp:Label></label>
                                                <div class="third22">
                                                    <cc2:TextBox ID="txtOrderBy" runat="server" Width="380px"></cc2:TextBox></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="third1">
                                                <label class="char9">
                                                    <asp:Label ID="Label7" runat="server" Text="自定义SQL编辑区"></asp:Label>
                                                    <div class="third22">
                                                        <cc2:TextBox ID="txtQuerySql" TextMode="MultiLine" runat="server" Width="380px" Height="100px"></cc2:TextBox></div>
                                            </div>
                                        </div>
                                        <div class="clear">
                                            <div class="oneline">
                                                <cc2:Button ID="btnTest" runat="server" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_TestConnect" AutoPostBack="false" ScriptContent="OK_Once()" Width="80" />
                                                <cc2:Button ID="btnOK" runat="server" Page_ClientValidate="false" ShowPostDiv="false" Text="Button_GoComplete" AutoPostBack="false" ScriptContent="OK_ReturnGG70()" Width="80" />
                                                <cc2:Button ID="btnClear" runat="server" Page_ClientValidate="false" ShowPostDiv="false" ButtonImgUrl="../images/browse.gif" Text="Button_Reset" AutoPostBack="true" Width="80" OnClick="ClearCheckboxlist" />
                                            </div>
                                        </div>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hiddenSearchType" runat="server" />
        <asp:HiddenField ID="HiddenResult" runat="server" />
        <asp:HiddenField ID="HiddenDataSetID" runat="server" />
        <asp:HiddenField ID="HiddenDataSourceID" runat="server" />
        <asp:HiddenField ID="HiddenQuerySql" runat="server" />
        <asp:HiddenField ID="HiddenDataBaseName" runat="server" />
    </div>
    </form>
</body>
</html>
