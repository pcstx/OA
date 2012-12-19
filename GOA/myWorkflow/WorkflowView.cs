using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using GPRP.GPRPComponents;
using GPRP.Entity;
namespace MyADO
{
    public partial class DbHelper
    {
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormBaseEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormBase2(Workflow_FormBaseEntity _Workflow_FormBaseEntity)
        {
            //判断该记录是否已经存在

            DbParameter[] prams = {
                                       MakeInParam("@FormName",(DbType)SqlDbType.VarChar,50,_Workflow_FormBaseEntity.FormName),
                                       MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.FormTypeID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.DisplayOrder ),
                                   };
            string sql = "select * from [dbo].[Workflow_FormBase] where FormName=@FormName or (FormTypeID=@FormTypeID and DisplayOrder=@DisplayOrder)";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在 
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@FormName",(DbType)SqlDbType.VarChar,200,_Workflow_FormBaseEntity.FormName ),
									   MakeInParam("@FormDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_FormBaseEntity.FormDesc ),
									   MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.FormTypeID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.DisplayOrder ),
									   MakeInParam("@Useflag",(DbType)SqlDbType.VarChar,50,_Workflow_FormBaseEntity.Useflag ),
									   MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,_Workflow_FormBaseEntity.Creator ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_Workflow_FormBaseEntity.CreateDate ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_FormBaseEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_Workflow_FormBaseEntity.lastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormBase]");
                sb.Append("(");
                sb.Append(" [FormName]");
                sb.Append(",[FormDesc]");
                sb.Append(",[FormTypeID]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[Useflag]");
                sb.Append(",[Creator]");
                sb.Append(",[CreateDate]");
                sb.Append(",[lastModifier]");
                sb.Append(",[lastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FormName,");
                sb.Append("@FormDesc,");
                sb.Append("@FormTypeID,");
                sb.Append("@DisplayOrder,");
                sb.Append("@Useflag,");
                sb.Append("@Creator,");
                sb.Append("@CreateDate,");
                sb.Append("@lastModifier,");
                sb.Append("@lastModifyDate )");
                sb.Append("select @@identity;");
                //  ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert);

                string FormID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                _Workflow_FormBaseEntity.FormID = Convert.ToInt32(FormID);

                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_FormBase");
                arlst.Add(_Workflow_FormBaseEntity.FormTypeID);
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

    }
}
