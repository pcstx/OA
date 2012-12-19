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
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecDataTable(string sql)
        {
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 执行SQL 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExecSqlText(string sql)
        {
            return ExecuteNonQuery(CommandType.Text, sql).ToString();
            
        }

        /// <summary>
        /// 获取第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ExecSqlResult(string sql)
        {
            return ExecuteScalarToStr(CommandType.Text, sql);
        }

        /// <summary>
        /// 根据分页参数,查询条件来获得DataTable
        /// </summary>
        /// <param name="columnList"></param>
        /// <param name="tableList"></param>
        /// <param name="WhereCondition"></param>
        /// <param name="orderby"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable GetDBRecords(string columnList, string tableList, string WhereCondition, string orderby, int PageSize, int PageIndex)
        {
            switch (DbHelper.SDBType)
            {
                case "Oracle":
                    int rowStart = PageSize * (PageIndex - 1) + 1;
                    int rowEnd = PageSize * PageIndex;
                    string sql = string.Format("select * from (select row_number() over(order by {3}) rowindex, {0},(select count(*) from {1} where {2}) AS RecordCount from {1} where {2} order by {3} ) tmpTable where rowindex >= {4} and rowindex <= {5} ", columnList, tableList, WhereCondition, orderby, rowStart, rowEnd);
                    return ExecuteDataset(sql).Tables[0];
                case "SqlServer":
                default:
                    DbParameter[] prams = {
                                              MakeInParam("@columnList", (DbType)SqlDbType.VarChar,2000,columnList),
                                              MakeInParam("@tableList", (DbType)SqlDbType.VarChar , 2000, tableList),
                                              MakeInParam("@WhereCondition", (DbType)SqlDbType.VarChar, 2000, WhereCondition),
                                              MakeInParam("@orderby", (DbType)SqlDbType.VarChar, 50, orderby),
                                              MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                              MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                          };
                    return ExecuteDataset(CommandType.StoredProcedure, "GetRecordSetbyPage", prams).Tables[0];
            }
        }

        /// <summary>
        /// 抓出符合条件的资料.没条件的话，传 1=1为条件
        /// </summary>
        /// <param name="columnList"></param>
        /// <param name="tableList"></param>
        /// <param name="WhereCondition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataTable GetDBRecords(string columnList, string tableList, string WhereCondition, string orderby)
        {
            string sql = "";
            if (orderby != "")
                sql = "select " + columnList + " from " + tableList + " where " + WhereCondition + " order by " + orderby;
            else
                sql = "select " + columnList + " from " + tableList + " where " + WhereCondition;
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 重新排序
        /// </summary>
        /// <param name="arlst"></param>
        /// <returns></returns>
        public string sp_ReDisplayOrder(ArrayList arlst)
        {
            DbParameter[] prams = {
									   MakeInParam("@TableName",(DbType)SqlDbType.VarChar,50,arlst[0] ),
									   MakeInParam("@ColValue1",(DbType)SqlDbType.VarChar,50,arlst[1] ),
									   MakeInParam("@ColValue2",(DbType)SqlDbType.VarChar,50,arlst[2] ),
                                   };
            string sql = "[sp_ReDisplayOrder]";
            return ExecuteNonQuery(CommandType.StoredProcedure, sql, prams).ToString();
        }


        #region "通知公告"
        /// <summary>
        /// 新增公告
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="expiredDate"></param>
        /// <param name="IsPublish"></param>
        /// <param name="Recieptor"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string SaveAnnouncement(string title, string content, string expiredDate, string IsPublish, string Recieptor, string userID)
            {
            try
                {
                DbParameter[] pramsInsert = {
									   MakeInParam("@AnnounceTitle",(DbType)SqlDbType.VarChar,2000,title ),
									   MakeInParam("@IsPublish",(DbType)SqlDbType.Char ,1,IsPublish ),
									   MakeInParam("@ExpiredDate",(DbType)SqlDbType.DateTime,23,expiredDate),
									   MakeInParam("@AnnounceContent",(DbType)SqlDbType.VarChar,4000,content ),
									   MakeInParam("@AnnounceReceiptor",(DbType)SqlDbType.VarChar,4000,Recieptor),
                                       MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,userID ),
									  

                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[GP_Announce]");
                sb.Append("(");
                sb.Append("[AnnounceTitle]");
                sb.Append(",[IsPublish]");
                sb.Append(",[ExpiredDate]");
                sb.Append(",[AnnounceContent]");
                sb.Append(",[AnnounceReceiptor]");
                sb.Append(",[Creator]");
                sb.Append(",[CreateDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@AnnounceTitle,");
                sb.Append("@IsPublish,");
                sb.Append("@ExpiredDate,");
                sb.Append("@AnnounceContent,");
                sb.Append("@AnnounceReceiptor,");
                sb.Append("@Creator,");
                sb.Append("getdate())");
                sb.Append(" select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                }
            catch (Exception err)
                {
                //return err.Message;
                throw err;

                }
            }

        /// <summary>
        /// 更新公告
        /// </summary>
        /// <param name="announceID"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="expiredDate"></param>
        /// <param name="IsPublish"></param>
        /// <param name="Recieptor"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public string UpdateAnnouncement(string announceID, string title, string content, string expiredDate, string IsPublish, string Recieptor, string userID)
            {
            try
                {
                DbParameter[] pramsU = {
									   MakeInParam("@AnnounceTitle",(DbType)SqlDbType.VarChar,2000,title ),
									   MakeInParam("@IsPublish",(DbType)SqlDbType.Char ,1,IsPublish ),
									   MakeInParam("@ExpiredDate",(DbType)SqlDbType.DateTime,23,expiredDate),
									   MakeInParam("@AnnounceContent",(DbType)SqlDbType.VarChar,4000,content ),
									   MakeInParam("@AnnounceReceiptor",(DbType)SqlDbType.VarChar,4000,Recieptor),
                                       MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,userID ),
									   MakeInParam("@AnnounceID",(DbType)SqlDbType.Int ,4,announceID  )

                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("update  [dbo].[GP_Announce]");
                sb.Append(" set");
                sb.Append(" [AnnounceTitle]=@AnnounceTitle");
                sb.Append(",[IsPublish]=@IsPublish");
                sb.Append(",[ExpiredDate]=@ExpiredDate");
                sb.Append(",[AnnounceContent]=@AnnounceContent");
                sb.Append(",[AnnounceReceiptor]=@AnnounceReceiptor");
                sb.Append(",[lastModifier]=@lastModifier");
                sb.Append(",[lastModifyDate]=getdate()");
                sb.Append(" where AnnounceID=@AnnounceID;");
                sb.Append(" select @@rowcount;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsU).ToString();
                }
            catch (Exception err)
                {
                //return err.Message;
                throw err;

                }

            }

        #endregion

    }
}
