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
        #region "代理"

        /// <summary>
        /// 新增信息,前台插入时已做判断,在此处不需再判断
        /// </summary>
        /// <param name="_Workflow_AgentSettingEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_AgentSetting(Workflow_AgentSettingEntity _Workflow_AgentSettingEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.WorkflowID ),
									   MakeInParam("@BeAgentPersonID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.BeAgentPersonID ),
									   MakeInParam("@AgentPersonID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.AgentPersonID ),
									   MakeInParam("@AgentStartDate",(DbType)SqlDbType.DateTime ,23,_Workflow_AgentSettingEntity.AgentStartDate ),
									   MakeInParam("@AgentEndDate",(DbType)SqlDbType.DateTime,23,_Workflow_AgentSettingEntity.AgentEndDate ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_Workflow_AgentSettingEntity.CreateDate ),
									   MakeInParam("@Creator",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.Creator ),
									   MakeInParam("@AllowCycle",(DbType)SqlDbType.Char,1,_Workflow_AgentSettingEntity.AllowCycle ),
									   MakeInParam("@AllowCreate",(DbType)SqlDbType.Char,1,_Workflow_AgentSettingEntity.AllowCreate )//,
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_AgentSetting]");
            sb.Append("(");
            sb.Append("[WorkflowID]");
            sb.Append(",[BeAgentPersonID]");
            sb.Append(",[AgentPersonID]");
            sb.Append(",[AgentStartDate]");
            sb.Append(",[AgentEndDate]");
            sb.Append(",[CreateDate]");
            sb.Append(",[Creator]");
            //   sb.Append(",[IsCancel]");
            sb.Append(",[AllowCycle]");
            sb.Append(",[AllowCreate]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@WorkflowID,");
            sb.Append("@BeAgentPersonID,");
            sb.Append("@AgentPersonID,");
            sb.Append("@AgentStartDate,");
            sb.Append("@AgentEndDate,");
            sb.Append("@CreateDate,");
            sb.Append("@Creator,");
            sb.Append("@AllowCycle,");
            sb.Append("@AllowCreate);");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_AgentSettingEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_AgentSetting(Workflow_AgentSettingEntity _Workflow_AgentSettingEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.WorkflowID ),
									   MakeInParam("@BeAgentPersonID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.BeAgentPersonID ),
									   MakeInParam("@AgentPersonID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.AgentPersonID ),
									   MakeInParam("@AgentStartDate",(DbType)SqlDbType.DateTime,23,_Workflow_AgentSettingEntity.AgentStartDate ),
									   MakeInParam("@AgentEndDate",(DbType)SqlDbType.DateTime,23,_Workflow_AgentSettingEntity.AgentEndDate ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_Workflow_AgentSettingEntity.CreateDate ),
									   MakeInParam("@Creator",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.Creator ),
									   MakeInParam("@AllowCycle",(DbType)SqlDbType.Char,1,_Workflow_AgentSettingEntity.AllowCycle ),
									   MakeInParam("@AllowCreate",(DbType)SqlDbType.Char,1,_Workflow_AgentSettingEntity.AllowCreate ) ,
                                       MakeInParam("@AgentID",(DbType)SqlDbType.Int,4,_Workflow_AgentSettingEntity.AgentID )
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_AgentSetting]");
            sb.Append(" set ");
            sb.Append(" [WorkflowID]=@WorkflowID,");
            sb.Append(" [BeAgentPersonID]=@BeAgentPersonID,");
            sb.Append(" [AgentPersonID]=@AgentPersonID,");
            sb.Append(" [AgentStartDate]=@AgentStartDate,");
            sb.Append(" [AgentEndDate]=@AgentEndDate,");
            sb.Append(" [CreateDate]=@CreateDate,");
            sb.Append(" [Creator]=@Creator,");
            sb.Append(" [AllowCycle]=@AllowCycle,");
            sb.Append(" [AllowCreate]=@AllowCreate");
            sb.Append(" where [AgentID]=@AgentID;select @@RowCount");
            return Convert.ToString(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate));
        }

        /// <summary>
        /// 逐个取消代理CancelWorkflow_AgentSetting
        /// </summary>
        /// <param name="cancelOperator"></param>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public string CancelWorkflow_AgentSetting(int cancelOperator, string agentID)
        {
            //取消
            DbParameter[] pramsUpdate = {
                                       MakeInParam("@CancelOperator",(DbType)SqlDbType.Int,4, (cancelOperator) ),
									   MakeInParam("@CancelDate",(DbType)SqlDbType.DateTime,23, DateTime.Now.ToLongDateString() ),
                                       MakeInParam("@AgentID",(DbType)SqlDbType.Int,4,Convert.ToInt32(agentID ))
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_AgentSetting]");
            sb.Append(" set ");
            sb.Append(" [IsCancel]='1',");
            sb.Append(" [CancelOperator]=@CancelOperator,");
            sb.Append(" [CancelDate]=@CancelDate");
            sb.Append(" where [AgentID]=@AgentID;select @@RowCount");
            return Convert.ToString(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate));
        }

        /// <summary>
        /// 批量取消代理CancelWorkflow_AgentSetting
        /// </summary>
        /// <param name="BeAgentPersonID"></param>
        /// <param name="AgentPersonID"></param>
        /// <param name="CancelOperator"></param>
        /// <returns></returns>
        public string CancelBatchWorkflow_AgentSetting(string BeAgentPersonID, string AgentPersonID, int CancelOperator)
        {
            //删除前先判断有没有被
            DbParameter[] pramsUpdate = {
									   MakeInParam("@BeAgentPersonID",(DbType)SqlDbType.Int,4,Int32.Parse(BeAgentPersonID) ),
									   MakeInParam("@AgentPersonID",(DbType)SqlDbType.Int,4,Int32.Parse(AgentPersonID) ),
                                       MakeInParam("@CancelOperator",(DbType)SqlDbType.Int,4,(CancelOperator) ),
									   MakeInParam("@CancelDate",(DbType)SqlDbType.DateTime,23,DateTime.Now.ToLongDateString())
                                          };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_AgentSetting]");
            sb.Append(" set ");
            sb.Append(" [IsCancel]='1',");
            sb.Append(" [CancelOperator]=@CancelOperator,");
            sb.Append(" [CancelDate]=@CancelDate");
            sb.Append(" where [BeAgentPersonID]=@BeAgentPersonID and AgentPersonID=@AgentPersonID and IsCancel='0';select @@RowCount");
            return Convert.ToString(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate));
        }

        /// <summary>
        /// DeleteWorkflow_AgentSetting
        /// </summary>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_AgentSetting(string AgentID)
        {
            //删除前先判断有没有被
            string sql = " delete from [dbo].[Workflow_AgentSetting] Where [AgentID]=@AgentID  ";
            DbParameter[] pramsDel = {
                                          MakeInParam("@AgentID",(DbType)SqlDbType.Int,4,Convert.ToInt32(AgentID) )
                                      };
            return ExecuteNonQuery(CommandType.Text, sql, pramsDel).ToString();
        }


        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public Workflow_AgentSettingEntity GetWorkflow_AgentSettingEntityByKeyCol(string KeyCol)
        {
            string sql = "select * from Workflow_AgentSetting  Where AgentID=@KeyCol";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int ,4,Convert.ToInt32(KeyCol ))
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_AgentSettingFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_AgentSettingEntity GetWorkflow_AgentSettingFromIDataReader(DbDataReader dr)
        {
            Workflow_AgentSettingEntity dt = new Workflow_AgentSettingEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["AgentID"].ToString() != "" || dr["AgentID"] != null) dt.AgentID = Int32.Parse(dr["AgentID"].ToString());
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                if (dr["BeAgentPersonID"].ToString() != "" || dr["BeAgentPersonID"] != null) dt.BeAgentPersonID = Int32.Parse(dr["BeAgentPersonID"].ToString());
                if (dr["AgentPersonID"].ToString() != "" || dr["AgentPersonID"] != null) dt.AgentPersonID = Int32.Parse(dr["AgentPersonID"].ToString());
                if (dr["AgentStartDate"].ToString() != "" || dr["AgentStartDate"].ToString() != null) dt.AgentStartDate = Convert.ToDateTime(dr["AgentStartDate"].ToString());
                if (dr["AgentEndDate"].ToString() != "" || dr["AgentEndDate"].ToString() != null) dt.AgentEndDate = Convert.ToDateTime(dr["AgentEndDate"].ToString());
                if (dr["CreateDate"].ToString() != "" || dr["CreateDate"].ToString() != null) dt.CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString());
                if (dr["Creator"].ToString() != "" || dr["Creator"] != null) dt.Creator = Int32.Parse(dr["Creator"].ToString());
                dt.IsCancel = dr["IsCancel"].ToString();
                dt.AllowCycle = dr["AllowCycle"].ToString();
                dt.AllowCreate = dr["AllowCreate"].ToString();
                if (dr["CancelOperator"].ToString() != "" || dr["CancelOperator"] != null) dt.CancelOperator = Int32.Parse(dr["CancelOperator"].ToString());
                if (dr["CancelDate"].ToString() != "" || dr["CancelDate"].ToString() != null) dt.CancelDate = Convert.ToDateTime(dr["CancelDate"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// JudgeAgentBeforeAddAndUpdate 判断是否可插入.
        /// </summary>
        /// <param name="_ASE"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string JudgeAgentBeforeAddAndUpdate(Workflow_AgentSettingEntity _ASE)
        {
            //判断该记录是否已经存在



            DbParameter[] prams = { 
                                       MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_ASE.WorkflowID ),
									   MakeInParam("@BeAgentPersonID",(DbType)SqlDbType.Int,4,_ASE.BeAgentPersonID ),
									   MakeInParam("@AgentStartDate",(DbType)SqlDbType.DateTime,23,_ASE.AgentStartDate ),
									   MakeInParam("@AgentEndDate",(DbType)SqlDbType.DateTime,23,_ASE.AgentEndDate )
                                   };

            StringBuilder sb = new StringBuilder();
            sb.Append(" select AgentID from Workflow_AgentSetting ");
            sb.Append(" where ");
            sb.Append(" [WorkflowID]=@WorkflowID");
            sb.Append(" and IsCancel='0'");
            sb.Append(" and BeAgentPersonID=@BeAgentPersonID");
            sb.Append(" and (");
            sb.Append("        (AgentStartDate>=@AgentStartDate and AgentStartDate<=@AgentEndDate)");
            sb.Append("         or ");
            sb.Append("        (AgentStartDate<=@AgentStartDate and AgentEndDate>=@AgentStartDate)");
            sb.Append("    )");

            DataTable dt = new DataTable();
            dt = (ExecuteDataset(CommandType.Text, sb.ToString(), prams)).Tables[0];
            if ((dt.Rows.Count == 1))
            {
                if (dt.Rows[0]["AgentID"].ToString() == _ASE.AgentID.ToString())
                    return "0";//用做在修改时判定是否可更新



                else
                    return "-1";
            }
            else if (dt.Rows.Count >= 1)
            {
                return "-1";//该记录已经存在


            }
            else
                return "0";
        }

        /// <summary>
        /// 得到代理流程查询结果
        /// </summary>
        /// <param name="al"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable GetAgentWorkflowSearchResult(ArrayList al, int PageSize, int PageIndex)
        {


            DbParameter[] prams = {
                                MakeInParam("@Type",(DbType)SqlDbType.TinyInt,1,al[0] ),
                                MakeInParam("@IsFinish",(DbType)SqlDbType.TinyInt ,1,al[1] ),
                                MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,al[2] ),
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,al[3]),
                                MakeInParam("@WorkflowID",(DbType)SqlDbType.VarChar,1000,al[4] ),
                                MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,al[5]),
                                MakeInParam("@CreateStartDate",(DbType)SqlDbType.DateTime,16,al[6] ),
                                MakeInParam("@CreateEndDate",(DbType)SqlDbType.DateTime,16,al[7] ),
                                MakeInParam("@Creator",(DbType)SqlDbType.Int,4, al[8]),
                                MakeInParam("@IsCancel",(DbType)SqlDbType.TinyInt ,1,al[9] ),
                                MakeInParam("@RequestID",(DbType)SqlDbType.Int,4,al[10]),
                                MakeInParam("@FlowOrRequestName",(DbType)SqlDbType.VarChar ,100, al[11].ToString() ),
                                MakeInParam("@CreatorDept",(DbType)SqlDbType.VarChar,500, al[12].ToString() ),
                                MakeInParam("@AgentID",(DbType)SqlDbType.Int,4, al[13]),
                                MakeInParam("@BeAgentID",(DbType)SqlDbType.Int,4, al[14]),
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4,  PageSize ),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex )
                                  };

            string sql = "[Workflow_GetAgentWorkflowSearchResult]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        #endregion

        #region "基本数据"
        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="HTMLTypeID"></param>
        /// <returns></returns>
        public Workflow_HTMLTypeEntity GetWorkflow_HTMLTypeEntityByKeyCol(string HTMLTypeID)
        {
            string sql = "select * from [dbo].[Workflow_HTMLType] Where HTMLTypeID=@HTMLTypeID";
            DbParameter[] pramsGet = {
									   MakeInParam("@HTMLTypeID",(DbType)SqlDbType.VarChar,50,HTMLTypeID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_HTMLTypeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_HTMLTypeEntity GetWorkflow_HTMLTypeFromIDataReader(DbDataReader dr)
        {
            Workflow_HTMLTypeEntity dt = new Workflow_HTMLTypeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["HTMLTypeID"].ToString() != "" || dr["HTMLTypeID"] != null) dt.HTMLTypeID = Int32.Parse(dr["HTMLTypeID"].ToString());
                dt.HTMLTypeName = dr["HTMLTypeName"].ToString();
                dt.HTMLTypeDesc = dr["HTMLTypeDesc"].ToString();
                dt.Useflag = dr["Useflag"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="BrowseTypeID"></param>
        /// <returns></returns>
        public Workflow_BrowseTypeEntity GetWorkflow_BrowseTypeEntityByKeyCol(string BrowseTypeID)
        {
            string sql = "select * from [dbo].[Workflow_BrowseType] Where BrowseTypeID=@BrowseTypeID";
            DbParameter[] pramsGet = {
									   MakeInParam("@BrowseTypeID",(DbType)SqlDbType.VarChar,50,BrowseTypeID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_BrowseTypeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_BrowseTypeEntity GetWorkflow_BrowseTypeFromIDataReader(DbDataReader dr)
        {
            Workflow_BrowseTypeEntity dt = new Workflow_BrowseTypeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["BrowseTypeID"].ToString() != "" || dr["BrowseTypeID"] != null) dt.BrowseTypeID = Int32.Parse(dr["BrowseTypeID"].ToString());
                dt.BrowseTypeName = dr["BrowseTypeName"].ToString();
                dt.BrowseTypeDesc = dr["BrowseTypeDesc"].ToString();
                dt.BrowsePage = dr["BrowsePage"].ToString();
                dt.Useflag = dr["Useflag"].ToString();
                dt.BrowseValueSql = dr["BrowseValueSql"].ToString();
                dt.BrowseSqlParam = dr["BrowseSqlParam"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormType(Workflow_FormTypeEntity _Workflow_FormTypeEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                      MakeInParam("@FormTypeName",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.FormTypeName ),
                                      MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormTypeEntity.DisplayOrder ),
                                  };
            string sql = " select * from [dbo].[Workflow_FormType] where FormTypeName=@FormTypeName or DisplayOrder=@DisplayOrder ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {

                DbParameter[] pramsInsert = {
									   MakeInParam("@FormTypeName",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.FormTypeName ),
									   MakeInParam("@FormTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FormTypeEntity.FormTypeDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormTypeEntity.DisplayOrder ),
									   MakeInParam("@Useflag",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.Useflag ),
									   MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.Creator ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,16,_Workflow_FormTypeEntity.CreateDate ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,16,_Workflow_FormTypeEntity.lastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormType]");
                sb.Append("(");
                sb.Append(" [FormTypeName]");
                sb.Append(",[FormTypeDesc]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[Useflag]");
                sb.Append(",[Creator]");
                sb.Append(",[CreateDate]");
                sb.Append(",[lastModifier]");
                sb.Append(",[lastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FormTypeName,");
                sb.Append("@FormTypeDesc,");
                sb.Append("@DisplayOrder,");
                sb.Append("@Useflag,");
                sb.Append("@Creator,");
                sb.Append("@CreateDate,");
                sb.Append("@lastModifier,");
                sb.Append("@lastModifyDate )");
                sb.Append("select @@identity;");
                ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_FormType");
                arlst.Add("");
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FormTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FormType(Workflow_FormTypeEntity _Workflow_FormTypeEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,_Workflow_FormTypeEntity.FormTypeID ),
									   MakeInParam("@FormTypeName",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.FormTypeName ),
									   MakeInParam("@FormTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FormTypeEntity.FormTypeDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormTypeEntity.DisplayOrder ),
									   MakeInParam("@Useflag",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.Useflag ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_FormTypeEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_Workflow_FormTypeEntity.lastModifyDate ),
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FormType]");
            sb.Append(" set ");
            sb.Append(" [FormTypeName]=@FormTypeName,");
            sb.Append(" [FormTypeDesc]=@FormTypeDesc,");
            sb.Append(" [DisplayOrder]=@DisplayOrder,");
            sb.Append(" [Useflag]=@Useflag,");
            sb.Append(" [lastModifier]=@lastModifier,");
            sb.Append(" [lastModifyDate]=@lastModifyDate ");
            sb.Append(" where [FormTypeID]=@FormTypeID ");
            sb.Append(" select @FormTypeID;");
            ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate).ToString();

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormType");
            arlst.Add("");
            arlst.Add("");
            return sp_ReDisplayOrder(arlst);
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="FormTypeID"></param>
        /// <returns></returns>
        public Workflow_FormTypeEntity GetWorkflow_FormTypeEntityByKeyCol(string FormTypeID)
        {
            string sql = "select * from [dbo].[Workflow_FormType] Where [FormTypeID]=@FormTypeID";
            DbParameter[] pramsGet = {
									   MakeInParam("@FormTypeID",(DbType)SqlDbType.VarChar,50,FormTypeID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_FormTypeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        private Workflow_FormTypeEntity GetWorkflow_FormTypeFromIDataReader(DbDataReader dr)
        {
            Workflow_FormTypeEntity dt = new Workflow_FormTypeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["FormTypeID"].ToString() != "" || dr["FormTypeID"] != null) dt.FormTypeID = Int32.Parse(dr["FormTypeID"].ToString());
                dt.FormTypeName = dr["FormTypeName"].ToString();
                dt.FormTypeDesc = dr["FormTypeDesc"].ToString();
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dt.Useflag = dr["Useflag"].ToString();
                dt.Creator = dr["Creator"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dt.lastModifier = dr["lastModifier"].ToString();
                dt.lastModifyDate = Convert.ToDateTime(dr["lastModifyDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "工作流程"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_BaseEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_Base(Workflow_BaseEntity _Workflow_BaseEntity)
        {
            //判断该记录是否已经存在 
            DbParameter[] prams = { MakeInParam("@WorkflowName",(DbType)SqlDbType.VarChar,200,_Workflow_BaseEntity.WorkflowName),
                                     };
            string sql = "select * from [dbo].[Workflow_Base] where WorkflowName=@WorkflowName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在  
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@WorkflowName",(DbType)SqlDbType.VarChar,200,_Workflow_BaseEntity.WorkflowName ),
									   MakeInParam("@WorkflowDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_BaseEntity.WorkflowDesc ),
									   MakeInParam("@FlowTypeID",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.FlowTypeID ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.FormID ),
									   MakeInParam("@IsValid",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsValid ),
									   MakeInParam("@IsMailNotice",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsMailNotice ),
									   MakeInParam("@IsMsgNotice",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsMsgNotice ),
									   MakeInParam("@IsTransfer",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsTransfer ),
									   MakeInParam("@AttachDocPath",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.AttachDocPath ),
									   MakeInParam("@HelpDocPath",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.HelpDocPath ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.DisplayOrder ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_Base]");
                sb.Append("(");
                sb.Append(" [WorkflowName]");
                sb.Append(",[WorkflowDesc]");
                sb.Append(",[FlowTypeID]");
                sb.Append(",[FormID]");
                sb.Append(",[IsValid]");
                sb.Append(",[IsMailNotice]");
                sb.Append(",[IsMsgNotice]");
                sb.Append(",[IsTransfer]");
                sb.Append(",[AttachDocPath]");
                sb.Append(",[HelpDocPath]");
                sb.Append(",[DisplayOrder]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@WorkflowName,");
                sb.Append("@WorkflowDesc,");
                sb.Append("@FlowTypeID,");
                sb.Append("@FormID,");
                sb.Append("@IsValid,");
                sb.Append("@IsMailNotice,");
                sb.Append("@IsMsgNotice,");
                sb.Append("@IsTransfer,");
                sb.Append("@AttachDocPath,");
                sb.Append("@HelpDocPath,");
                sb.Append("@DisplayOrder )");
                sb.Append("select @@identity;");
             //   ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert);
 
                _Workflow_BaseEntity.WorkflowID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());

                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_Base");
                arlst.Add(_Workflow_BaseEntity.FlowTypeID);
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_BaseEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_Base(Workflow_BaseEntity _Workflow_BaseEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.WorkflowID ),
									   MakeInParam("@WorkflowName",(DbType)SqlDbType.VarChar,200,_Workflow_BaseEntity.WorkflowName ),
									   MakeInParam("@WorkflowDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_BaseEntity.WorkflowDesc ),
									   MakeInParam("@FlowTypeID",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.FlowTypeID ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.FormID ),
									   MakeInParam("@IsValid",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsValid ),
									   MakeInParam("@IsMailNotice",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsMailNotice ),
									   MakeInParam("@IsMsgNotice",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsMsgNotice ),
									   MakeInParam("@IsTransfer",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.IsTransfer ),
									   MakeInParam("@AttachDocPath",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.AttachDocPath ),
									   MakeInParam("@HelpDocPath",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.HelpDocPath ),
                                       MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_BaseEntity.DisplayOrder ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_Base]");
            sb.Append(" set ");
            sb.Append(" [WorkflowName]=@WorkflowName,");
            sb.Append(" [WorkflowDesc]=@WorkflowDesc,");
            sb.Append(" [FlowTypeID]=@FlowTypeID,");
            sb.Append(" [FormID]=@FormID,");
            sb.Append(" [IsValid]=@IsValid,");
            sb.Append(" [IsMailNotice]=@IsMailNotice,");
            sb.Append(" [IsMsgNotice]=@IsMsgNotice,");
            sb.Append(" [IsTransfer]=@IsTransfer,");
            sb.Append(" [AttachDocPath]=@AttachDocPath,");
            sb.Append(" [HelpDocPath]=@HelpDocPath, ");
            sb.Append(" [DisplayOrder]=@DisplayOrder ");
            sb.Append(" where [WorkflowID]=@WorkflowID ");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_Base");
            arlst.Add(_Workflow_BaseEntity.FlowTypeID);
            arlst.Add("");
            return sp_ReDisplayOrder(arlst);
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <returns></returns>
        public Workflow_BaseEntity GetWorkflow_BaseEntityByKeyCol(string WorkflowID)
        {
            string sql = "select * from [dbo].[Workflow_Base] Where [WorkflowID]=@WorkflowID";
            DbParameter[] pramsGet = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,WorkflowID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_BaseFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_BaseEntity GetWorkflow_BaseFromIDataReader(DbDataReader dr)
        {
            Workflow_BaseEntity dt = new Workflow_BaseEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                dt.WorkflowName = dr["WorkflowName"].ToString();
                dt.WorkflowDesc = dr["WorkflowDesc"].ToString();
                if (dr["FlowTypeID"].ToString() != "" || dr["FlowTypeID"] != null) dt.FlowTypeID = Int32.Parse(dr["FlowTypeID"].ToString());
                if (dr["FormID"].ToString() != "" || dr["FormID"] != null) dt.FormID = Int32.Parse(dr["FormID"].ToString());
                if (dr["IsValid"].ToString() != "" || dr["IsValid"] != null) dt.IsValid = Int32.Parse(dr["IsValid"].ToString());
                if (dr["IsMailNotice"].ToString() != "" || dr["IsMailNotice"] != null) dt.IsMailNotice = Int32.Parse(dr["IsMailNotice"].ToString());
                if (dr["IsMsgNotice"].ToString() != "" || dr["IsMsgNotice"] != null) dt.IsMsgNotice = Int32.Parse(dr["IsMsgNotice"].ToString());
                if (dr["IsTransfer"].ToString() != "" || dr["IsTransfer"] != null) dt.IsTransfer = Int32.Parse(dr["IsTransfer"].ToString());
                if (dr["AttachDocPath"].ToString() != "" || dr["AttachDocPath"] != null) dt.AttachDocPath = Int32.Parse(dr["AttachDocPath"].ToString());
                if (dr["HelpDocPath"].ToString() != "" || dr["HelpDocPath"] != null) dt.HelpDocPath = Int32.Parse(dr["HelpDocPath"].ToString());
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }
        #endregion

        #region "数据源设置"


        /// <summary>    测试通过,将数据源资料插入资料库中     </summary>
        /// <param name="_DataSourceEntity"> </param>
        /// <returns></returns>
        public int AddWorkflow_DataSource(Workflow_DataSourceEntity _DataSourceEntity)
        {
            //做判断 相同的DB,相同的连接字串,返回-1,,插入成功返回DataSourceID
            DbParameter[] parms ={
                                     MakeInParam("@DataSourceName",(DbType)SqlDbType.VarChar,200,_DataSourceEntity.DataSourceName),
                                     MakeInParam("@DataSourceDBType",(DbType)SqlDbType.VarChar,50,_DataSourceEntity.DataSourceDBType ),
                                     MakeInParam("@ConnectString",(DbType)SqlDbType.VarChar,400,_DataSourceEntity.ConnectString )
                                  };
            string strsql = "select  count(*) from [dbo].[Workflow_DataSource] where DataSourceDBType=@DataSourceDBType and ConnectString=@ConnectString";

            if ((int)ExecuteScalar(CommandType.Text, strsql, parms) > 0)
            {
                return -1;
            }
            else
            {
                StringBuilder strb = new StringBuilder();
                strb.Append(" INSERT INTO [dbo].[Workflow_DataSource] ([DataSourceName] ,[DataSourceDBType] ,[ConnectString])");
                strb.Append(" VALUES  (@DataSourceName, @DataSourceDBType,@ConnectString);");
                strb.Append("select @@identity;");
                return Convert.ToInt32(ExecuteScalar(CommandType.Text, strb.ToString(), parms));
            }
        }

        /// <summary>    测试通过,将数据源资料更新到资料库中    </summary>
        /// <param name="_DataSourceEntity"> </param>
        /// <returns></returns>
        public int UpdateWorkflow_DataSource(Workflow_DataSourceEntity _DataSourceEntity)
        {
            DbParameter[] parms ={
                                     MakeInParam("@DataSourceName",(DbType)SqlDbType.VarChar,200,_DataSourceEntity.DataSourceName),
                                     MakeInParam("@DataSourceDBType",(DbType)SqlDbType.VarChar,50,_DataSourceEntity.DataSourceDBType ),
                                     MakeInParam("@ConnectString",(DbType)SqlDbType.VarChar,400,_DataSourceEntity.ConnectString ),
                                     MakeInParam("@DataSourceID",(DbType)SqlDbType.Int ,4,_DataSourceEntity.DataSourceID ),
                                  };

            StringBuilder strb = new StringBuilder();
            strb.Append(" UPDATE [dbo].[Workflow_DataSource] ");
            strb.Append(" SET [DataSourceName]=@DataSourceName ,[DataSourceDBType]=@DataSourceDBType ,[ConnectString]=@ConnectString ");
            strb.Append(" WHERE DataSourceID=@DataSourceID");
            return ExecuteNonQuery(CommandType.Text, strb.ToString(), parms);

        }

        /// <summary>        /// 如果未被其他数据集引用,可以删除,成功返回0,不成功返回-1;如已被收用,返回数据集ID        /// </summary>
        /// <param name="DataSourceID"> </param>
        /// <returns></returns>
        public int DeleteWorkflow_DataSource(int DataSourceID)
        {
            //先判断是否已被数据集引用,有,则不能删除;无,可删除


            DbParameter[] parms = { MakeInParam("@DataSourceID", (DbType)SqlDbType.Int, 4, DataSourceID) };
            string strsql = "select count(*) from [dbo].[Workflow_DataSet] where DataSourceID=@DataSourceID";

            if ((int)ExecuteScalar(CommandType.Text, strsql, parms) > 0)
            {
                return -1;
            }
            else
            {
                StringBuilder strb = new StringBuilder();
                strb.Append(" DELETE FROM  [dbo].[Workflow_DataSource] ");
                strb.Append(" WHERE DataSourceID=@DataSourceID");
                return Convert.ToInt32(ExecuteScalar(CommandType.Text, strb.ToString(), parms));
            }
        }
        /// <summary>        /// 通过数据源ID查找        /// </summary>
        /// <param name="DataSourceID"> DataSourceID</param>
        /// <returns>Workflow_DataSourceEntity</returns>
        public Workflow_DataSourceEntity GetDataSourceByID(int DataSourceID)
        {
            string sql = "SELECT * FROM [dbo].[Workflow_DataSource] WHERE DataSourceID=@DataSourceID";
            DbParameter[] parms = { MakeInParam("@DataSourceID", (DbType)SqlDbType.Int, 4, DataSourceID) };
            DbDataReader dr = null;
            try
            {
                dr = ExecuteReader(CommandType.Text, sql, parms);
                if (dr.Read())
                {
                    return GetWorkflow_DataSourceFromIDataReader(dr);
                }
                else
                {
                    throw new Exception(ResourceManager.GetString("NO_RECORDS_FOUND"));
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }


        private Workflow_DataSourceEntity GetWorkflow_DataSourceFromIDataReader(DbDataReader dr)
        {
            Workflow_DataSourceEntity dt = new Workflow_DataSourceEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["DataSourceID"].ToString() != "" || dr["DataSourceID"] != null) dt.DataSourceID = Int32.Parse(dr["DataSourceID"].ToString());
                dt.DataSourceName = dr["DataSourceName"].ToString();
                dt.DataSourceDBType = dr["DataSourceDBType"].ToString();
                dt.ConnectString = dr["ConnectString"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }
        #endregion

        #region "数据集操作"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_DataSetEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public int AddWorkflow_DataSet(Workflow_DataSetEntity _Workflow_DataSetEntity)
        {
            DbParameter[] pramsInsert = {
								       MakeInParam("@DataSetType",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSetType ),
									   MakeInParam("@DataSetName",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetEntity.DataSetName ),
									   MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSourceID ),
									   MakeInParam("@TableList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.TableList ),
									   MakeInParam("@FieldList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.FieldList ),
									   MakeInParam("@QueryCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QueryCondition ),
									   MakeInParam("@OrderBy",(DbType)SqlDbType.VarChar,500,_Workflow_DataSetEntity.OrderBy ),
									   MakeInParam("@QuerySql",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QuerySql ),
									   MakeInParam("@ReturnColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumns ),
                                       MakeInParam("@DataSetPKColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.DataSetPKColumns ),
									   MakeInParam("@ReturnColumnsName",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumnsName )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_DataSet]");
            sb.Append("(");
            sb.Append("[DataSetType]");
            sb.Append(",[DataSetName]");
            sb.Append(",[DataSourceID]");
            sb.Append(",[TableList]");
            sb.Append(",[FieldList]");
            sb.Append(",[QueryCondition]");
            sb.Append(",[OrderBy]");
            sb.Append(",[QuerySql]");
            sb.Append(",[ReturnColumns]");
            sb.Append(",[DataSetPKColumns]");
            sb.Append(",[ReturnColumnsName]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@DataSetType,");
            sb.Append("@DataSetName,");
            sb.Append("@DataSourceID,");
            sb.Append("@TableList,");
            sb.Append("@FieldList,");
            sb.Append("@QueryCondition,");
            sb.Append("@OrderBy,");
            sb.Append("@QuerySql,");
            sb.Append("@ReturnColumns,");
            sb.Append("@DataSetPKColumns,");
            sb.Append("@ReturnColumnsName ");
            sb.Append(");");
            sb.Append("select @@identity;");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert));
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_DataSetEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public int AddWorkflow_DataSetSP(Workflow_DataSetEntity _Workflow_DataSetEntity)
        {
            DbParameter[] pramsInsert = {
								       MakeInParam("@DataSetType",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSetType ),
									   MakeInParam("@DataSetName",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetEntity.DataSetName ),
									   MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSourceID ),
									   MakeInParam("@TableList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.TableList ),
									   MakeInParam("@QueryCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QueryCondition ),
									   MakeInParam("@ReturnColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumns ),
                                       MakeInParam("@DataSetPKColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.DataSetPKColumns ),
									   MakeInParam("@ReturnColumnsName",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumnsName )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_DataSet]");
            sb.Append("(");
            sb.Append("[DataSetType]");
            sb.Append(",[DataSetName]");
            sb.Append(",[DataSourceID]");
            sb.Append(",[TableList]");
            sb.Append(",[QueryCondition]");
            sb.Append(",[ReturnColumns]");
            sb.Append(",[DataSetPKColumns]");
            sb.Append(",[ReturnColumnsName]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@DataSetType,");
            sb.Append("@DataSetName,");
            sb.Append("@DataSourceID,");
            sb.Append("@TableList,");
            sb.Append("@QueryCondition,");
            sb.Append("@ReturnColumns,");
            sb.Append("@DataSetPKColumns,");
            sb.Append("@ReturnColumnsName");
            sb.Append(");");
            sb.Append("select @@identity;");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert));
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_DataSetEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public int UpdateWorkflow_DataSet(Workflow_DataSetEntity _Workflow_DataSetEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@DataSetName",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetEntity.DataSetName ),
									   MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSourceID ),
									   MakeInParam("@TableList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.TableList ),
									   MakeInParam("@FieldList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.FieldList ),
									   MakeInParam("@QueryCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QueryCondition ),
									   MakeInParam("@OrderBy",(DbType)SqlDbType.VarChar,500,_Workflow_DataSetEntity.OrderBy ),
									   MakeInParam("@QuerySql",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QuerySql ),
									   MakeInParam("@ReturnColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumns ),
                                       MakeInParam("@DataSetPKColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.DataSetPKColumns ),
									   MakeInParam("@ReturnColumnsName",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumnsName ),
                                       MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSetID )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_DataSet]");
            sb.Append(" set ");
            sb.Append(" [DataSetType]=1,");
            sb.Append(" [DataSetName]=@DataSetName,");
            sb.Append(" [DataSourceID]=@DataSourceID,");
            sb.Append(" [TableList]=@TableList,");
            sb.Append(" [FieldList]=@FieldList,");
            sb.Append(" [QueryCondition]=@QueryCondition,");
            sb.Append(" [OrderBy]=@OrderBy,");
            sb.Append(" [QuerySql]=@QuerySql,");
            sb.Append(" [ReturnColumns]=@ReturnColumns,");
            sb.Append(" [DataSetPKColumns]=@DataSetPKColumns,");
            sb.Append(" [ReturnColumnsName]=@ReturnColumnsName");
            sb.Append(" where [DataSetID]=@DataSetID;select @@rowcount");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate));
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_DataSetEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public int UpdateWorkflow_DataSetForStoredProcedure(Workflow_DataSetEntity _Workflow_DataSetEntity)
        {
            DbParameter[] pramsUpdate = {
                                             MakeInParam("@DataSetName",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetEntity.DataSetName ),
                                             MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSourceID ),
                                             MakeInParam("@TableList",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.TableList ),
                                             MakeInParam("@QueryCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.QueryCondition ),
                                             MakeInParam("@ReturnColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumns ),
                                             MakeInParam("@DataSetPKColumns",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.DataSetPKColumns ),
                                             MakeInParam("@ReturnColumnsName",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetEntity.ReturnColumnsName ),
                                             MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Workflow_DataSetEntity.DataSetID )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_DataSet]");
            sb.Append(" set ");
            sb.Append(" [DataSetType]=2,");
            sb.Append(" [DataSetName]=@DataSetName,");
            sb.Append(" [DataSourceID]=@DataSourceID,");
            sb.Append(" [TableList]=@TableList,");
            sb.Append(" [QueryCondition]=@QueryCondition,");
            sb.Append(" [ReturnColumns]=@ReturnColumns,");
            sb.Append(" [DataSetPKColumns]=@DataSetPKColumns,");
            sb.Append(" [ReturnColumnsName]=@ReturnColumnsName");
            sb.Append(" where [DataSetID]=@DataSetID; select @@rowcount");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate));
        }

        /// <summary>        
        /// 如果未被其他数据表引用,可以删除,成功返回0,不成功返回-1; 
        /// 同时删除数据集参数表中此Datasetid相关参数
        /// </summary>
        /// <param name="DataSetID"> </param>
        /// <returns></returns>
        public int DeleteWorkflow_DataSet(int DataSetID)
        {
            //先判断是否已被引用(表Workflow_FieldDict	DataSetID),,有,则不能删除;无,可删除


            DbParameter[] parms = { MakeInParam("@DataSetID", (DbType)SqlDbType.Int, 4, DataSetID) };
            string strsql = "SELECT COUNT(*) FROM Workflow_FieldDict  WHERE  DataSetID=@DataSetID";

            if ((int)ExecuteScalar(CommandType.Text, strsql, parms) > 0)
            {
                return -1;
            }
            else
            {
                StringBuilder strb = new StringBuilder();
                strb.Append(" DELETE FROM  [dbo].[Workflow_DataSetParameter] ");
                strb.Append(" WHERE DataSetID=@DataSetID;");
                strb.Append(" DELETE FROM  [dbo].[Workflow_DataSet] ");
                strb.Append(" WHERE DataSetID=@DataSetID");
                return Convert.ToInt32(ExecuteScalar(CommandType.Text, strb.ToString(), parms));
            }
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="keyCol"></param>
        /// <returns></returns>
        public Workflow_DataSetEntity GetWorkflow_DataSetEntityByKeyCol(string keyCol)
        {
            string sql = "select * from Workflow_DataSet Where DataSetID=@KeyCol";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int ,4,Convert.ToInt32(keyCol)  )
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_DataSetFromIDataReader(sr);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_DataSetEntity GetWorkflow_DataSetFromIDataReader(DbDataReader dr)
        {
            Workflow_DataSetEntity dt = new Workflow_DataSetEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["DataSetID"].ToString() != "" || dr["DataSetID"] != null) dt.DataSetID = Int32.Parse(dr["DataSetID"].ToString());
                if (dr["DataSetType"].ToString() != "" || dr["DataSetType"] != null) dt.DataSetType = Int32.Parse(dr["DataSetType"].ToString());
                dt.DataSetName = dr["DataSetName"].ToString();
                if (dr["DataSourceID"].ToString() != "" || dr["DataSourceID"] != null) dt.DataSourceID = Int32.Parse(dr["DataSourceID"].ToString());
                dt.TableList = dr["TableList"].ToString();
                dt.FieldList = dr["FieldList"].ToString();
                dt.QueryCondition = dr["QueryCondition"].ToString();
                dt.OrderBy = dr["OrderBy"].ToString();
                dt.QuerySql = dr["QuerySql"].ToString();
                dt.ReturnColumns = dr["ReturnColumns"].ToString();
                dt.ReturnColumnsName = dr["ReturnColumnsName"].ToString();
                dt.DataSetPKColumns = dr["DataSetPKColumns"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "数据集参数操作"
        /// <summary>
        /// 新增信息,每次插入前先将之前此数据集的参数全部删除,再加重新插入.
        /// 因此,不用update
        /// </summary>
        /// <param name="_Workflow_DataSetParameterEntity"></param>
        /// <returns>返回string "-1"表示删除不成功，否则返回插入数据笔数 </returns>
        public string AddWorkflow_DataSetParameter(Workflow_DataSetParameterEntity _Workflow_DataSetParameterEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Workflow_DataSetParameterEntity.DataSetID ),
									   MakeInParam("@ParameterName",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetParameterEntity.ParameterName ),
									   MakeInParam("@ParameterType",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetParameterEntity.ParameterType ),
                                       MakeInParam("@ParameterDirection",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetParameterEntity.ParameterDirection ),
                                       MakeInParam("@ParameterSize",(DbType)SqlDbType.Int ,4,_Workflow_DataSetParameterEntity.ParameterSize ),
									   MakeInParam("@ParameterValue",(DbType)SqlDbType.VarChar,50,_Workflow_DataSetParameterEntity.ParameterValue ),
                                       MakeInParam("@ParameterDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_DataSetParameterEntity.ParameterDesc )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_DataSetParameter]");
            sb.Append("(");
            sb.Append("[DataSetID]");
            sb.Append(",[ParameterName]");
            sb.Append(",[ParameterType]");
            sb.Append(",[ParameterDirection]");
            sb.Append(",[ParameterSize]");
            sb.Append(",[ParameterValue]");
            sb.Append(",[ParameterDesc]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@DataSetID,");
            sb.Append("@ParameterName,");
            sb.Append("@ParameterType,");
            sb.Append("@ParameterDirection,");
            sb.Append("@ParameterSize,");
            sb.Append("@ParameterValue,");
            sb.Append("@ParameterDesc");
            sb.Append(");");
            sb.Append("select @@rowcount;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>        /// 如果未被其他数据集引用,可以删除,成功返回0,不成功返回-1;如已被收用,返回数据集ID        /// </summary>
        /// <param name="DataSetID"> </param>
        /// <returns></returns>
        public int DeleteWorkflow_DataSetParameter(int DataSetID)
        {
            DbParameter[] parms = { MakeInParam("@DataSetID", (DbType)SqlDbType.Int, 4, DataSetID) };
            StringBuilder strb = new StringBuilder();
            strb.Append(" DELETE FROM  [dbo].[Workflow_DataSetParameter] ");
            strb.Append(" WHERE DataSetID=@DataSetID;SELECT @@rowcount");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, strb.ToString(), parms));
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_DataSetParameterEntityByKeyCol(string KeyCol)
        {
            string sql = "select * from  Workflow_DataSetParameter  Where DataSetID=@KeyCol";
            DbParameter[] pramsGet = {
                                          MakeInParam("@KeyCol",(DbType)SqlDbType.Int ,4,Convert.ToInt32(KeyCol) )
                                      };
            DataTable dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            return dt;
        }

        #endregion

        #region "查询数据库相关信息"

        /// <summary>        /// 通过数据源ID查找此数据源下的所有数据库名称        /// </summary>
        /// <param name="DataSourceID"> DataSourceID</param>
        /// <returns>含所有数据库名称的DataTable</returns>
        public DataTable GetAllDatabaseNameByDataSourceID(int DataSourceID)
        {
            DataTable dt;
            Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
            try
            {
                string sql;
                if (_DSE.DataSourceDBType == "Oracle") //oracle数据库
                {
                    sql = "select name from v$database";
                }
                else //认为是sql server 数据库
                {
                    sql = "select  [name]  from  sys.databases";
                }
                dt = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID查找此数据源下的所有数据库名称
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public DataTable GetAllDatabaseNameByDataSourceUserNamePassword2(string DataSource, string UserName, string Password)
        {
            DataTable dt = new DataTable();

            //认为是sql server 数据库


            try
            {
                string sql = "select  [name]  from  sys.databases";
                string connStr = string.Format("Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}", DataSource, UserName, Password);
                dt = ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            catch
            {
                // throw exp;
                dt = null;
            }
            finally
            {

            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID,数据库名称查找此数据库下的所有Table名称
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <returns></returns>
        public DataTable GetAllTableNames(int DataSourceID)
        {
            DataTable dt;
            Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
            try
            {
                string sql;
                if (_DSE.DataSourceDBType == "Oracle") //oracle数据库
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select TABLE_NAME from all_tables where OWNER='{0}' AND TABLESPACE_NAME='USERS' AND STATUS='VALID'  order by TABLE_NAME", owner.ToUpper());
                }
                else //认为是sql server 数据库
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select [name] TABLE_NAME from  {0}.sys.tables order by [name]", owner);
                }
                dt = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID查找此数据源下的所有数据库名称
        /// </summary>
        /// <param name="DataSource"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public DataTable GetAllDatabaseNameByDataSourceUserNamePassword(string DataSource, string UserName, string Password)
        {
            DataTable dt;
            //认为是sql server 数据库


            try
            {
                string sql = "select  [name]  from  sys.databases";
                string connStr = string.Format("Data Source={0};Initial Catalog=master;Persist Security Info=True;User ID={1};Password={2}", DataSource, UserName, Password);
                dt = GetInstance("SqlServer", connStr).ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID,Table Name 查找此Table下所有列的信息
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetAllColumns(int DataSourceID, string TableName)
        {
            DataTable dt;
            Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
            try
            {
                string sql;
                if (_DSE.DataSourceDBType == "Oracle") //oracle数据库
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select COLUMN_NAME from DBA_TAB_COLUMNS where owner='{0}' and table_name='{1}'", owner.ToUpper(), TableName.ToUpper());
                }
                else //认为是sql server 数据库
                {
                    string DatabaseName = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select COLUMN_NAME from  {0}.information_schema.columns where table_name='{1}' order by  ORDINAL_POSITION", DatabaseName, TableName);
                }
                dt = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID,数据库名称查找此数据库下的所有StoredProcedure名称
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <returns></returns>
        public DataTable GetAllStoredProcedureNames(int DataSourceID)
        {
            DataTable dt;
            try
            {
                Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
                string sql;
                if (_DSE.DataSourceDBType == "Oracle") //oracle数据库, 稍后引入DBhelper
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    //此处需要重新修改


                    sql = string.Format("select OBJECT_NAME SP_NAME from SYS.DBA_PROCEDURES where OWNER='{0}' and OBJECT_TYPE='PROCEDURE' order by OBJECT_NAME", owner.ToUpper());
                }
                else //认为是sql server 数据库
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select SP_NAME=[name]  from  {0}.sys.objects WHERE ([type]= 'P' )  order by [name]", owner);
                }
                dt = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID,查询数据库中数据类型
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <returns></returns>
        public DataTable GetAllDataBaseDataType(int DataSourceID)
        {
            DataTable dt;
            try
            {
                Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
                string sql;
                if (_DSE.DataSourceDBType == "Oracle") //oracle数据库
                {
                    //string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    //sql = string.Format("SELECT distinct DATA_TYPE FROM USER_TAB_COLS where TABLE_NAME IN (select table_name from all_tables where OWNER='{0}') ", owner.ToUpper());
                    sql = "select DATA_TYPE=OracleType from Workflow_DBDataType";
                }
                else //认为是sql server 数据库
                {
                    string owner = _DSE.ConnectString.Split(new Char[] { ';' })[1].Split(new Char[] { '=' })[1].ToString();
                    sql = string.Format("select DATA_TYPE=[name] from  {0}.sys.systypes  ", owner);
                }
                dt = ExecDataTable(sql);
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 通过数据源ID,存储过程名 查找此存储过程下的所有参数信息(参数名,参数类型,参数值)
        /// 增加参数大小
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <param name="StoredProcedureName"></param>
        /// <returns></returns>
        public DataTable GetAllStoredProcedureParameters(int DataSourceID, string StoredProcedureName)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ParameterName");
            dt.Columns.Add("ParameterType");
            dt.Columns.Add("IsNullable");
            dt.Columns.Add("ParameterDirection");
            dt.Columns.Add("ParameterSize");

            try
            {
                Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
                IDataParameter[] opAry = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).GetSpParameterSet(StoredProcedureName, false);
                if (DbHelper.SDBType == "Oracle")
                {
                    foreach (System.Data.OracleClient.OracleParameter op in opAry)
                    {
                        object[] rowAry = new Object[5];
                        rowAry[0] = op.ParameterName;
                        rowAry[1] = op.OracleType.ToString();
                        rowAry[2] = op.IsNullable;
                        rowAry[3] = op.Direction;
                        rowAry[4] = op.Size;
                        dt.Rows.Add(rowAry);
                    }
                }
                else
                {
                    foreach (System.Data.SqlClient.SqlParameter op in opAry)
                    {
                        object[] rowAry = new Object[5];
                        rowAry[0] = op.ParameterName;
                        rowAry[1] = op.SqlDbType.ToString();
                        rowAry[2] = op.IsNullable;
                        rowAry[3] = op.Direction;
                        rowAry[4] = op.Size;
                        dt.Rows.Add(rowAry);
                    }
                }
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 得到查询语句最终的列名
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <param name="QuerySql"></param>
        /// <returns></returns>
        public DataTable GetDataSetSQLReturnColumns(int DataSourceID, string QuerySql, params object[] param)
        {
            DataTable dt;
            try
            {
                Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);
                if (param.GetLength(0) == 0)
                {
                    dt = DbHelper.GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.Text, QuerySql).Tables[0];
                }
                else
                {
                    DbParameter[] para = new DbParameter[param.Length];
                    int i = 0;
                    if (_DSE.DataSourceDBType == "Oracle")
                    {
                        foreach (Workflow_DataSetParameterEntity espe in param)
                        {
                            para[i] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(espe.ParameterName, (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), espe.ParameterType, true), espe.ParameterSize, espe.ParameterValue);
                            i++;
                        }
                    }
                    else
                    {
                        foreach (Workflow_DataSetParameterEntity espe in param)
                        {
                            para[i] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(espe.ParameterName, (DbType)Enum.Parse(typeof(SqlDbType), espe.ParameterType, true), espe.ParameterSize, espe.ParameterValue);
                            i++;
                        }
                    }
                    dt = DbHelper.GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.Text, QuerySql, para).Tables[0];
                }
            }
            catch
            {
                dt = null;
            }
            return dt;
        }

        /// <summary>
        /// 得到存储过程最终的列名
        /// </summary>
        /// <param name="DataSourceID"></param>
        /// <param name="SPName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataTable GetDataSetSPReturnColumns(int DataSourceID, string SPName, params object[] param)
        {
            Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSourceID);

            DbParameter[] para = new DbParameter[param.Length];
            IDataParameter[] sqlAry = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).GetSpParameterSet(SPName, false);

            int i = 0;
            foreach (Workflow_DataSetParameterEntity espe in param)
            {
                foreach (DbParameter sp in sqlAry)
                {
                    if ((sp.ParameterName == espe.ParameterName))//(sp.DbType.ToString() == espe.ParameterType) && 
                    {
                        if (_DSE.DataSourceDBType == "Oracle")
                        {
                            para[i] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(espe.ParameterName, (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), espe.ParameterType, true), espe.ParameterSize, espe.ParameterValue);
                            i++;
                        }
                        else
                        {
                            para[i] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(espe.ParameterName, (DbType)Enum.Parse(typeof(SqlDbType), espe.ParameterType, true), espe.ParameterSize, espe.ParameterValue);
                            i++;
                        }
                    }
                }
            }
            DataTable dt = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.StoredProcedure, SPName, para).Tables[0];
            return dt;

        }

        #endregion

        #region "我的流程"

        /// <summary>
        /// 得到流程查询结果
        /// </summary>
        /// <param name="al"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable GetMyWorkflowSearchResult(ArrayList al, int PageSize, int PageIndex)
        {
            DbParameter[] prams = {
                                MakeInParam("@Type",(DbType)SqlDbType.TinyInt,1,al[0] ),
                                MakeInParam("@IsFinish",(DbType)SqlDbType.TinyInt ,1,al[1] ),
                                MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,al[2] ),
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,al[3]),
                                MakeInParam("@WorkflowID",(DbType)SqlDbType.VarChar,1000,al[4] ),
                                MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,al[5]),
                                MakeInParam("@CreateStartDate",(DbType)SqlDbType.DateTime,16,al[6] ),
                                MakeInParam("@CreateEndDate",(DbType)SqlDbType.DateTime,16,al[7] ),
                                MakeInParam("@Creator",(DbType)SqlDbType.Int,4, al[8]),
                                MakeInParam("@IsCancel",(DbType)SqlDbType.TinyInt ,1,al[9] ),
                                MakeInParam("@RequestID",(DbType)SqlDbType.Int,4,al[10]),
                                MakeInParam("@FlowOrRequestName",(DbType)SqlDbType.VarChar ,100, al[11].ToString() ),
                                MakeInParam("@CreatorDept",(DbType)SqlDbType.VarChar,500, al[12].ToString() ),
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4,  PageSize ),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex )
                                   };

            string sql = "[Workflow_GetMyWorkflowSearchResult]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];

        }

        /// <summary>
        /// 得到流程监控查询结果
        /// </summary>
        /// <param name="al"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable GetWorkflowMonitorSearchResult(ArrayList al, int PageSize, int PageIndex)
        {

            DbParameter[] prams = {
                                MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,al[0] ),
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,al[1]),
                                MakeInParam("@WorkflowID",(DbType)SqlDbType.VarChar,1000,al[2] ),
                                MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,al[3]),
                                MakeInParam("@CreateStartDate",(DbType)SqlDbType.DateTime,16,al[4] ),
                                MakeInParam("@CreateEndDate",(DbType)SqlDbType.DateTime,16,al[5] ),
                                MakeInParam("@Creator",(DbType)SqlDbType.Int,4, al[6]),
                                MakeInParam("@IsCancel",(DbType)SqlDbType.TinyInt ,1,al[7] ),
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4,  PageSize ),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex )
                                   };

            string sql = "[Workflow_GetWorkflowMonitorSearchResult]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        #endregion

        #region "字段管理"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FieldDictEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FieldDict(Workflow_FieldDictEntity _Workflow_FieldDictEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                       MakeInParam("@FieldName",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.FieldName),
                                       MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.FieldTypeID),
                                   };
            string sql = " select * from [dbo].[Workflow_FieldDict] where FieldName=@FieldName and FieldTypeID=@FieldTypeID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@FieldName",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.FieldName ),
									   MakeInParam("@FieldDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FieldDictEntity.FieldDesc ),
									   MakeInParam("@DataTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.DataTypeID ),
									   MakeInParam("@FieldDBType",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.FieldDBType ),
									   MakeInParam("@HTMLTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.HTMLTypeID ),
									   MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.FieldTypeID ),
									   MakeInParam("@ValidateType",(DbType)SqlDbType.VarChar,100,_Workflow_FieldDictEntity.ValidateType ),
									   MakeInParam("@TextLength",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.TextLength ),
									   MakeInParam("@Dateformat",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.Dateformat ),
									   MakeInParam("@TextHeight",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.TextHeight ),
									   MakeInParam("@IsHTML",(DbType)SqlDbType.Char,1,_Workflow_FieldDictEntity.IsHTML ),
									   MakeInParam("@BrowseType",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.BrowseType ),
									   MakeInParam("@IsDynamic",(DbType)SqlDbType.Char,1,_Workflow_FieldDictEntity.IsDynamic ),
									   MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.DataSetID ),
									   MakeInParam("@ValueColumn",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.ValueColumn ),
									   MakeInParam("@TextColumn",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.TextColumn ),
									   MakeInParam("@DefaultValue",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.DefaultValue ),
									   MakeInParam("@SqlDbType",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.SqlDbType ),
									   MakeInParam("@SqlDbLength",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.SqlDbLength ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FieldDict]");
                sb.Append("(");
                sb.Append(" [FieldName]");
                sb.Append(",[FieldDesc]");
                sb.Append(",[DataTypeID]");
                sb.Append(",[FieldDBType]");
                sb.Append(",[HTMLTypeID]");
                sb.Append(",[FieldTypeID]");
                sb.Append(",[ValidateType]");
                sb.Append(",[TextLength]");
                sb.Append(",[Dateformat]");
                sb.Append(",[TextHeight]");
                sb.Append(",[IsHTML]");
                sb.Append(",[BrowseType]");
                sb.Append(",[IsDynamic]");
                sb.Append(",[DataSetID]");
                sb.Append(",[ValueColumn]");
                sb.Append(",[TextColumn]");
                sb.Append(",[DefaultValue]");
                sb.Append(",[SqlDbType]");
                sb.Append(",[SqlDbLength]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FieldName,");
                sb.Append("@FieldDesc,");
                sb.Append("@DataTypeID,");
                sb.Append("@FieldDBType,");
                sb.Append("@HTMLTypeID,");
                sb.Append("@FieldTypeID,");
                sb.Append("@ValidateType,");
                sb.Append("@TextLength,");
                sb.Append("@Dateformat,");
                sb.Append("@TextHeight,");
                sb.Append("@IsHTML,");
                sb.Append("@BrowseType,");
                sb.Append("@IsDynamic,");
                sb.Append("@DataSetID,");
                sb.Append("@ValueColumn,");
                sb.Append("@TextColumn,");
                sb.Append("@DefaultValue,");
                sb.Append("@SqlDbType,");
                sb.Append("@SqlDbLength )");
                sb.Append("select @@identity;");
                string FieldID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                _Workflow_FieldDictEntity.FieldID = Convert.ToInt32(FieldID);

                DataTable dtSelectListNow = _Workflow_FieldDictEntity.dtSelectList;
                for (int i = 0; i < dtSelectListNow.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtSelectListNow.Rows[i]["SelectNo"]) == 0)
                    {
                        DbParameter[] pramsInsertDetail = {
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,FieldID ),
									   MakeInParam("@LabelWord",(DbType)SqlDbType.VarChar,200,dtSelectListNow.Rows[i]["LabelWord"] ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["DisplayOrder"] ),
                                                   };
                        string sqlInsertDetail = @"insert into [dbo].[Workflow_FieldDictSelect] (FieldID,LabelWord,DisplayOrder)values (@FieldID,@LabelWord,@DisplayOrder) select @@identity;";
                        ExecuteScalar(CommandType.Text, sqlInsertDetail, pramsInsertDetail);
                    }
                    else
                    {
                        DbParameter[] pramsUpdateDetail = {
									   MakeInParam("@SelectNo",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["SelectNo"] ),
									   MakeInParam("@LabelWord",(DbType)SqlDbType.VarChar,200,dtSelectListNow.Rows[i]["LabelWord"] ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["DisplayOrder"] ),
                                                   };
                        string sqlUpdateDetail = @"update [dbo].[Workflow_FieldDictSelect] set LabelWord=@LabelWord,DisplayOrder=@DisplayOrder where SelectNo=@SelectNo";
                        ExecuteScalar(CommandType.Text, sqlUpdateDetail, pramsUpdateDetail);
                    }
                }
                return "0";
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FieldDictEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FieldDict(Workflow_FieldDictEntity _Workflow_FieldDictEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.FieldID ),
									   MakeInParam("@FieldName",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.FieldName ),
									   MakeInParam("@FieldDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FieldDictEntity.FieldDesc ),
									   MakeInParam("@DataTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.DataTypeID ),
									   MakeInParam("@FieldDBType",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.FieldDBType ),
									   MakeInParam("@HTMLTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.HTMLTypeID ),
									   MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.FieldTypeID ),
									   MakeInParam("@ValidateType",(DbType)SqlDbType.VarChar,100,_Workflow_FieldDictEntity.ValidateType ),
									   MakeInParam("@TextLength",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.TextLength ),
									   MakeInParam("@Dateformat",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.Dateformat ),
									   MakeInParam("@TextHeight",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.TextHeight ),
									   MakeInParam("@IsHTML",(DbType)SqlDbType.Char,1,_Workflow_FieldDictEntity.IsHTML ),
									   MakeInParam("@BrowseType",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.BrowseType ),
									   MakeInParam("@IsDynamic",(DbType)SqlDbType.Char,1,_Workflow_FieldDictEntity.IsDynamic ),
									   MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.DataSetID ),
									   MakeInParam("@ValueColumn",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.ValueColumn ),
									   MakeInParam("@TextColumn",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.TextColumn ),
									   MakeInParam("@DefaultValue",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.DefaultValue ),
									   MakeInParam("@SqlDbType",(DbType)SqlDbType.VarChar,50,_Workflow_FieldDictEntity.SqlDbType ),
									   MakeInParam("@SqlDbLength",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.SqlDbLength ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FieldDict]");
            sb.Append(" set ");
            sb.Append(" [FieldName]=@FieldName,");
            sb.Append(" [FieldDesc]=@FieldDesc,");
            sb.Append(" [DataTypeID]=@DataTypeID,");
            sb.Append(" [FieldDBType]=@FieldDBType,");
            sb.Append(" [HTMLTypeID]=@HTMLTypeID,");
            sb.Append(" [FieldTypeID]=@FieldTypeID,");
            sb.Append(" [ValidateType]=@ValidateType,");
            sb.Append(" [TextLength]=@TextLength,");
            sb.Append(" [Dateformat]=@Dateformat,");
            sb.Append(" [TextHeight]=@TextHeight,");
            sb.Append(" [IsHTML]=@IsHTML,");
            sb.Append(" [BrowseType]=@BrowseType,");
            sb.Append(" [IsDynamic]=@IsDynamic,");
            sb.Append(" [DataSetID]=@DataSetID,");
            sb.Append(" [ValueColumn]=@ValueColumn,");
            sb.Append(" [TextColumn]=@TextColumn,");
            sb.Append(" [DefaultValue]=@DefaultValue,");
            sb.Append(" [SqlDbType]=@SqlDbType,");
            sb.Append(" [SqlDbLength]=@SqlDbLength ");
            sb.Append(" where [FieldID]=@FieldID");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate);

            DataTable dtSelectListNow = _Workflow_FieldDictEntity.dtSelectList;
            DataTable dtSelectListOrignal = GetDBRecords("*", "Workflow_FieldDictSelect", "FieldID=" + _Workflow_FieldDictEntity.FieldID, "DisplayOrder");
            for (int i = 0; i < dtSelectListOrignal.Rows.Count; i++)
            {
                string SelectNo = dtSelectListOrignal.Rows[i]["SelectNo"].ToString();
                if (dtSelectListNow.Select("SelectNo=" + SelectNo).Length < 1)
                {
                    ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_FieldDictSelect] where SelectNo=" + SelectNo);
                }
            }

            for (int i = 0; i < dtSelectListNow.Rows.Count; i++)
            {
                if (Convert.ToInt32(dtSelectListNow.Rows[i]["SelectNo"]) == 0)
                {
                    DbParameter[] pramsInsertDetail = {
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FieldDictEntity.FieldID ),
									   MakeInParam("@LabelWord",(DbType)SqlDbType.VarChar,200,dtSelectListNow.Rows[i]["LabelWord"] ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["DisplayOrder"] ),
                                                   };
                    string sql = @"insert into [dbo].[Workflow_FieldDictSelect] (FieldID,LabelWord,DisplayOrder)values (@FieldID,@LabelWord,@DisplayOrder) select @@identity;";
                    ExecuteScalar(CommandType.Text, sql, pramsInsertDetail);
                }
                else
                {
                    DbParameter[] pramsUpdateDetail = {
									   MakeInParam("@SelectNo",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["SelectNo"] ),
									   MakeInParam("@LabelWord",(DbType)SqlDbType.VarChar,200,dtSelectListNow.Rows[i]["LabelWord"] ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,dtSelectListNow.Rows[i]["DisplayOrder"] ),
                                                   };
                    string sql = @"update [dbo].[Workflow_FieldDictSelect] set LabelWord=@LabelWord,DisplayOrder=@DisplayOrder where SelectNo=@SelectNo";
                    ExecuteScalar(CommandType.Text, sql, pramsUpdateDetail);
                }
            }
            return "0";
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        public Workflow_FieldDictEntity GetWorkflow_FieldDictEntityByKeyCol(string FieldID)
        {
            string sql = "select * from [dbo].[Workflow_FieldDict] Where FieldID=@FieldID";
            DbParameter[] pramsGet = {
                                          MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,FieldID ),
                                      };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_FieldDictFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_FieldDictEntity GetWorkflow_FieldDictFromIDataReader(DbDataReader dr)
        {
            Workflow_FieldDictEntity dt = new Workflow_FieldDictEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["FieldID"].ToString() != "" || dr["FieldID"] != null) dt.FieldID = Int32.Parse(dr["FieldID"].ToString());
                dt.FieldName = dr["FieldName"].ToString();
                dt.FieldDesc = dr["FieldDesc"].ToString();
                if (dr["DataTypeID"].ToString() != "" || dr["DataTypeID"] != null) dt.DataTypeID = Int32.Parse(dr["DataTypeID"].ToString());
                dt.FieldDBType = dr["FieldDBType"].ToString();
                if (dr["HTMLTypeID"].ToString() != "" || dr["HTMLTypeID"] != null) dt.HTMLTypeID = Int32.Parse(dr["HTMLTypeID"].ToString());
                if (dr["FieldTypeID"].ToString() != "" || dr["FieldTypeID"] != null) dt.FieldTypeID = Int32.Parse(dr["FieldTypeID"].ToString());
                dt.ValidateType = dr["ValidateType"].ToString();
                if (dr["TextLength"].ToString() != "" || dr["TextLength"] != null) dt.TextLength = Int32.Parse(dr["TextLength"].ToString());
                dt.Dateformat = dr["Dateformat"].ToString();
                if (dr["TextHeight"].ToString() != "" || dr["TextHeight"] != null) dt.TextHeight = Int32.Parse(dr["TextHeight"].ToString());
                dt.IsHTML = dr["IsHTML"].ToString();
                if (dr["BrowseType"].ToString() != "" || dr["BrowseType"] != null) dt.BrowseType = Int32.Parse(dr["BrowseType"].ToString());
                dt.IsDynamic = dr["IsDynamic"].ToString();
                dt.DataSetID = Convert.ToInt32(dr["DataSetID"]);
                dt.ValueColumn = dr["ValueColumn"].ToString();
                dt.TextColumn = dr["TextColumn"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "表单基本信息"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormBaseEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormBase(Workflow_FormBaseEntity _Workflow_FormBaseEntity)
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

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FormBaseEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FormBase(Workflow_FormBaseEntity _Workflow_FormBaseEntity)
        {
            //判断该记录是否已经存在 
            DbParameter[] prams = {
                                       MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.FormID ),
                                       MakeInParam("@FormName",(DbType)SqlDbType.VarChar,50,_Workflow_FormBaseEntity.FormName),
                                       MakeInParam("@FormTypeID",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.FormTypeID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.DisplayOrder ),
                                   };
            string sql = "select * from [dbo].[Workflow_FormBase] where FormID<>@FormID and ( FormName=@FormName or (FormTypeID=@FormTypeID and DisplayOrder=@DisplayOrder))";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            Workflow_FormBaseEntity _OriginalWorkflow_FormBaseEntity = GetWorkflow_FormBaseEntityByKeyCol(_Workflow_FormBaseEntity.FormID.ToString());
            DbParameter[] pramsUpdate = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormBaseEntity.FormID ),
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
            sb.Append("Update [dbo].[Workflow_FormBase]");
            sb.Append(" set ");
            sb.Append(" [FormName]=@FormName,");
            sb.Append(" [FormDesc]=@FormDesc,");
            sb.Append(" [FormTypeID]=@FormTypeID,");
            sb.Append(" [DisplayOrder]=@DisplayOrder,");
            sb.Append(" [Useflag]=@Useflag,");
            sb.Append(" [Creator]=@Creator,");
            sb.Append(" [CreateDate]=@CreateDate,");
            sb.Append(" [lastModifier]=@lastModifier,");
            sb.Append(" [lastModifyDate]=@lastModifyDate ");
            sb.Append(" where [FormID]=@FormID ");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate);

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormBase");
            arlst.Add(_OriginalWorkflow_FormBaseEntity.FormTypeID);
            arlst.Add(_Workflow_FormBaseEntity.FormTypeID);
            return sp_ReDisplayOrder(arlst);
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="FormID"></param>
        /// <returns></returns>
        public Workflow_FormBaseEntity GetWorkflow_FormBaseEntityByKeyCol(string FormID)
        {
            string sql = "select * from [dbo].[Workflow_FormBase] Where [FormID]=@FormID";
            DbParameter[] pramsGet = {
                                          MakeInParam("@FormID",(DbType)SqlDbType.Int,4,Convert.ToInt32(FormID) )
                                      };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_FormBaseFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        /// <summary>
        /// GetWorkflow_FormBaseFromIDataReader
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private Workflow_FormBaseEntity GetWorkflow_FormBaseFromIDataReader(DbDataReader dr)
        {
            Workflow_FormBaseEntity dt = new Workflow_FormBaseEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["FormID"].ToString() != "" || dr["FormID"] != null) dt.FormID = Int32.Parse(dr["FormID"].ToString());
                dt.FormName = dr["FormName"].ToString();
                dt.FormDesc = dr["FormDesc"].ToString();
                if (dr["FormTypeID"].ToString() != "" || dr["FormTypeID"] != null) dt.FormTypeID = Int32.Parse(dr["FormTypeID"].ToString());
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dt.Useflag = dr["Useflag"].ToString();
                dt.Creator = dr["Creator"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dt.lastModifier = dr["lastModifier"].ToString();
                dt.lastModifyDate = Convert.ToDateTime(dr["lastModifyDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// DeleteWorkflow_FormField
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="FieldID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_FormField(string FormID, string FieldID, string GroupID)
        {
            string sql = " delete from [dbo].[Workflow_FormField] Where [FormID]=@FormID and [FieldID]=@FieldID  and [GroupID]=@GroupID ";
            DbParameter[] pramsDel = {
                                          MakeInParam("@FormID",(DbType)SqlDbType.Int,4,FormID ),
                                          MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,FieldID ),
                                          MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID ),
                                      };
            return ExecuteNonQuery(CommandType.Text, sql, pramsDel).ToString();
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormFieldEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormField(Workflow_FormFieldEntity _Workflow_FormFieldEntity)
        {
            //判断该记录是否已经存在 
            DbParameter[] prams = {
                                       MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FieldID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupID ),
                                   };
            string sql = " select * from [dbo].[Workflow_FormField] where FormID=@FormID and FieldID=@FieldID  and GroupID=@GroupID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在 
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FieldID ),
									   MakeInParam("@FieldLabel",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldEntity.FieldLabel ),
									   MakeInParam("@CSSStyle",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.CSSStyle ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.DisplayOrder ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupID ),
                                       MakeInParam("@IsDetail",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.IsDetail ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormField]");
                sb.Append("(");
                sb.Append(" [FormID]");
                sb.Append(",[FieldID]");
                sb.Append(",[FieldLabel]");
                sb.Append(",[IsDetail]");
                sb.Append(",[CSSStyle]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[GroupID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FormID,");
                sb.Append("@FieldID,");
                sb.Append("@FieldLabel,");
                sb.Append("@IsDetail,");
                sb.Append("@CSSStyle,");
                sb.Append("@DisplayOrder,");
                sb.Append("@GroupID )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        public string AddWorkflow_FormField2(Workflow_FormFieldEntity _Workflow_FormFieldEntity)
        {
            //判断该记录是否已经存在 
            DbParameter[] prams = {
                                       MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FieldID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupID ),
                                   };
            string sql = " select * from [dbo].[Workflow_FormField] where FormID=@FormID and FieldID=@FieldID  and GroupID=@GroupID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在 
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FieldID ),
									   MakeInParam("@FieldLabel",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldEntity.FieldLabel ),
									   MakeInParam("@CSSStyle",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.CSSStyle ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.DisplayOrder ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupID ),
                                       MakeInParam("@IsDetail",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.IsDetail ),
                                       MakeInParam("@GroupLineDataSetID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupLineDataSetID ),
                                       MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.TargetGroupID ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormField]");
                sb.Append("(");
                sb.Append(" [FormID]");
                sb.Append(",[FieldID]");
                sb.Append(",[FieldLabel]");
                sb.Append(",[IsDetail]");
                sb.Append(",[CSSStyle]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[GroupID]");
                sb.Append(",[GroupLineDataSetID]");
                sb.Append(",[TargetGroupID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FormID,");
                sb.Append("@FieldID,");
                sb.Append("@FieldLabel,");
                sb.Append("@IsDetail,");
                sb.Append("@CSSStyle,");
                sb.Append("@DisplayOrder,");
                sb.Append("@GroupID,");
                sb.Append("@GroupLineDataSetID,");
                sb.Append("@TargetGroupID )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }
         
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FormFieldEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FormField(Workflow_FormFieldEntity _Workflow_FormFieldEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FormID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupID ),
                                       MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.FieldID ),
									   MakeInParam("@FieldLabel",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldEntity.FieldLabel ),
									   MakeInParam("@CSSStyle",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.CSSStyle ),
									   MakeInParam("@GroupLineDataSetID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.GroupLineDataSetID ),
									   MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.TargetGroupID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormFieldEntity.DisplayOrder ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FormField]");
            sb.Append(" set ");
            sb.Append(" [FieldLabel]=@FieldLabel,");
            sb.Append(" [CSSStyle]=@CSSStyle,");
            sb.Append(" [GroupLineDataSetID]=@GroupLineDataSetID,");
            sb.Append(" [TargetGroupID]=@TargetGroupID,");
            sb.Append(" [DisplayOrder]=@DisplayOrder ");
            sb.Append(" where FormID=@FormID and FieldID=@FieldID and GroupID=@GroupID ");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FieldList"></param>
        /// <param name="FieldType"></param>
        /// <returns></returns>
        public string sp_AlterWork_form_TableColumn(string FieldList, string FieldType)
        {
            DbParameter[] prams = {
                                MakeInParam("@FieldList",(DbType)SqlDbType.VarChar,2000,FieldList ),//表单字段
                                MakeInParam("@FieldType",(DbType)SqlDbType.Int,4,FieldType ),//字段类型
                                  };

            string sql = "[sp_AlterWork_form_TableColumn]";
            return ExecuteNonQuery(CommandType.StoredProcedure, sql, prams).ToString();
        }

        #endregion

        #region "表单明细组"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormFieldGroupEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormFieldGroup(Workflow_FormFieldGroupEntity _Workflow_FormFieldGroupEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                       MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldGroupEntity.GroupName ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.FormID ),
                                   };
            string sql = " select * from [dbo].[Workflow_FormFieldGroup] where FormID=@FormID and GroupName=@GroupName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldGroupEntity.GroupName ),
									   MakeInParam("@GroupDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldGroupEntity.GroupDesc ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.FormID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.DisplayOrder ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormFieldGroup]");
                sb.Append("(");
                sb.Append(" [GroupName]");
                sb.Append(",[GroupDesc]");
                sb.Append(",[FormID]");
                sb.Append(",[DisplayOrder]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@GroupName,");
                sb.Append("@GroupDesc,");
                sb.Append("@FormID,");
                sb.Append("@DisplayOrder )");
                sb.Append("select @@identity;");
               
                int GroupID =Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert));
                _Workflow_FormFieldGroupEntity.GroupID = GroupID;  //返回groupID
                 
                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_FormFieldGroup");
                arlst.Add(_Workflow_FormFieldGroupEntity.FormID);
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FormFieldGroupEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FormFieldGroup(Workflow_FormFieldGroupEntity _Workflow_FormFieldGroupEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.GroupID ),
									   MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldGroupEntity.GroupName ),
									   MakeInParam("@GroupDesc",(DbType)SqlDbType.VarChar,200,_Workflow_FormFieldGroupEntity.GroupDesc ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.FormID ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FormFieldGroupEntity.DisplayOrder ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FormFieldGroup]");
            sb.Append(" set ");
            sb.Append(" [GroupName]=@GroupName,");
            sb.Append(" [GroupDesc]=@GroupDesc,");
            sb.Append(" [FormID]=@FormID,");
            sb.Append(" [DisplayOrder]=@DisplayOrder ");
            sb.Append(" where [GroupID]=@GroupID ");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public Workflow_FormFieldGroupEntity GetWorkflow_FormFieldGroupEntityByKeyCol(string GroupID)
        {
            string sql = "select * from [dbo].[Workflow_FormFieldGroup] Where GroupID=@GroupID";
            DbParameter[] pramsGet = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.VarChar,50,GroupID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_FormFieldGroupFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_FormFieldGroupEntity GetWorkflow_FormFieldGroupFromIDataReader(DbDataReader dr)
        {
            Workflow_FormFieldGroupEntity dt = new Workflow_FormFieldGroupEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["GroupID"].ToString() != "" || dr["GroupID"] != null) dt.GroupID = Int32.Parse(dr["GroupID"].ToString());
                dt.GroupName = dr["GroupName"].ToString();
                dt.GroupDesc = dr["GroupDesc"].ToString();
                if (dr["FormID"].ToString() != "" || dr["FormID"] != null) dt.FormID = Int32.Parse(dr["FormID"].ToString());
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="FormID"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string DeleteWorkflow_FormFieldGroup(string GroupID, string FormID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int, 4,GroupID ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("delete [dbo].[Workflow_FormFieldGroup] where GroupID=@GroupID");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsDelete);

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FormFieldGroup");
            arlst.Add(FormID);
            arlst.Add("");
            return sp_ReDisplayOrder(arlst);
        }

        #endregion

        #region "行列规则"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FormDetailRuleEntity"></param>
        /// <param name="dtComputeRoute"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FormDetailRule(Workflow_FormDetailRuleEntity _Workflow_FormDetailRuleEntity, DataTable dtComputeRoute)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                       MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.FormID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.GroupID ),
									   MakeInParam("@FieldIDTo",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.FiledIDTo ),
                                   };
            string sql = " select * from [dbo].[Workflow_FormDetailRule] where FormID=@FormID and GroupID=@GroupID and FieldIDTo=@FieldIDTo ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.FormID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.GroupID ),
									   MakeInParam("@FieldIDTo",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.FiledIDTo ),
									   MakeInParam("@RuleDetail",(DbType)SqlDbType.VarChar,2000,_Workflow_FormDetailRuleEntity.RuleDetail ),
									   MakeInParam("@RuleType",(DbType)SqlDbType.Int,4,_Workflow_FormDetailRuleEntity.RuleType ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FormDetailRule]");
                sb.Append("(");
                sb.Append(" [FormID]");
                sb.Append(",[GroupID]");
                sb.Append(",[FieldIDTo]");
                sb.Append(",[RuleDetail]");
                sb.Append(",[RuleType]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@FormID,");
                sb.Append("@GroupID,");
                sb.Append("@FieldIDTo,");
                sb.Append("@RuleDetail,");
                sb.Append("@RuleType ) ");
                sb.Append("select @@identity;");
                string RuleID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
                {
                    DbParameter[] pramsRouteInsert = {
									   MakeInParam("@RuleID",(DbType)SqlDbType.Int,4,RuleID ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRoute.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["RouteOrder"] ),
                                             };
                    string sqlInsert = @" insert into Workflow_ComputeRoute(RuleID,ComputeType,RouteValue,RouteOrder) values (@RuleID,@ComputeType,@RouteValue,@RouteOrder) ";
                    ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
                }

                UpdateWorkflow_FormDetailRule4Calculate(RuleID);
                return "0";
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_FormDetailRule(string RuleID)
        {
            DbParameter[] prams = {
                                       MakeInParam("@RuleID",(DbType)SqlDbType.Int,4,RuleID ),
                                   };
            string sql = "delete from Workflow_FormDetailRule where RuleID=@RuleID; delete from Workflow_ComputeRoute where RuleID=@RuleID";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public string UpdateWorkflow_FormDetailRule4Calculate(string RuleID)
        {
            string RuleRelationField = "";
            DataTable dtRuleRelationField = GetDBRecords("a.RuleID,a.FieldIDTo,b.RouteValue,d.FieldName,FieldID=c.FieldIDTo", "Workflow_FormDetailRule a,Workflow_ComputeRoute b,Workflow_FormDetailRule c,Workflow_FieldDict d", "a.RuleID=b.RuleID and b.ComputeType=2 and a.GroupID=c.GroupID and a.FormID=c.FormID and b.RouteValue=d.FieldID and c.RuleID=" + RuleID, "a.RuleID");
            DataRow[] drRuleRelationField = dtRuleRelationField.Select("FieldID=FieldIDTo", "");
            for (int i = 0; i < drRuleRelationField.Length; i++)
            {
                string RouteValue = drRuleRelationField[i]["RouteValue"].ToString();
                string FieldName = drRuleRelationField[i]["FieldName"].ToString();
                RuleRelationField = GetRuleRelationField(RuleRelationField, RouteValue, FieldName, dtRuleRelationField);
            }
            RuleRelationField = RuleRelationField.Trim(new char[] { ',' });

            DataTable dtFunctionName = GetDBRecords("b.ComputeSymbol", "Workflow_ComputeRoute a,Workflow_ComputeSymbol b", "a.RouteValue=b.ComputeID and b.ComputeTypeID='10' and a.ComputeType=1 and a.RuleID=" + RuleID, "a.RouteID");
            string FunctionName = "";
            for (int i = 0; i < dtFunctionName.Rows.Count; i++)
            {
                FunctionName += dtFunctionName.Rows[i]["ComputeSymbol"].ToString() + ",";
            }
            FunctionName = FunctionName.Trim(new char[] { ',' });

            DataTable dtRuleField = GetDBRecords("d.FieldName,c.FieldLabel", "Workflow_FormDetailRule a,Workflow_ComputeRoute b,Workflow_FormField c,Workflow_FieldDict d", "a.RuleID=b.RuleID and a.FormID=c.FormID and a.GroupID=c.GroupID and b.RouteValue=c.FieldID and c.FieldID=d.FieldID and b.ComputeType=2 and a.RuleID=" + RuleID, "b.RouteID");
            string RuleField = "";
            for (int i = 0; i < dtRuleField.Rows.Count; i++)
            {
                RuleField += dtRuleField.Rows[i]["FieldName"].ToString() + ",";
            }
            RuleField = RuleField.Trim(new char[] { ',' });

            DbParameter[] pramsUpdate = {
									   MakeInParam("@RuleID",(DbType)SqlDbType.Int,4,RuleID ),
									   MakeInParam("@RuleRelationField",(DbType)SqlDbType.VarChar,200,RuleRelationField ),
									   MakeInParam("@RuleFunctionName",(DbType)SqlDbType.VarChar,2000,FunctionName ),
									   MakeInParam("@RuleField",(DbType)SqlDbType.VarChar,2000,RuleField ),
	                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FormDetailRule]");
            sb.Append(" set ");
            sb.Append(" [RuleRelationField]=@RuleRelationField,");
            sb.Append(" [RuleFunctionName]=@RuleFunctionName,");
            sb.Append(" [RuleField]=@RuleField ");
            sb.Append(" where [RuleID]=@RuleID");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate);

            return "1";
        }

        private string GetRuleRelationField(string RuleRelationField, string RouteValue, string FieldName, DataTable dtRuleRelationField)
        {
            DataRow[] drRuleRelationField = dtRuleRelationField.Select("FieldIDTo=" + RouteValue, "");
            if (drRuleRelationField.Length == 0)
            {
                RuleRelationField += RuleRelationField.Contains(FieldName + ",") ? "" : (FieldName + ",");
                return RuleRelationField;
            }
            else
            {
                for (int i = 0; i < drRuleRelationField.Length; i++)
                {
                    RuleRelationField = GetRuleRelationField(RuleRelationField, drRuleRelationField[i]["RouteValue"].ToString(), drRuleRelationField[i]["FieldName"].ToString(), dtRuleRelationField);
                }
                return RuleRelationField;
            }
        }

        #endregion

        #region "表赋值"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_GroupLineFieldMapEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_GroupLineFieldMap(Workflow_GroupLineFieldMapEntity _Workflow_GroupLineFieldMapEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_GroupLineFieldMapEntity.FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_GroupLineFieldMapEntity.FieldID ),
									   MakeInParam("@DataSetColumn",(DbType)SqlDbType.VarChar,50,_Workflow_GroupLineFieldMapEntity.DataSetColumn ),
									   MakeInParam("@TargetGroupField",(DbType)SqlDbType.VarChar,50,_Workflow_GroupLineFieldMapEntity.TargetGroupField ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_GroupLineFieldMap]");
            sb.Append("(");
            sb.Append(" [FormID]");
            sb.Append(",[FieldID]");
            sb.Append(",[DataSetColumn]");
            sb.Append(",[TargetGroupField]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@FormID,");
            sb.Append("@FieldID,");
            sb.Append("@DataSetColumn,");
            sb.Append("@TargetGroupField )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_GroupLineFieldMap(string FormID, string FieldID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,FormID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,FieldID ),
                                         };
            string sql = " Delete from [dbo].[Workflow_GroupLineFieldMap] where FormID=@FormID and FieldID=@FieldID";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }

        #endregion

        #region "节点信息"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_FlowNodeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_FlowNode(Workflow_FlowNodeEntity _Workflow_FlowNodeEntity)
        {
            //判断该记录是否已经存在             
            DbParameter[] prams = {
                                       MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.WorkflowID ),
                                       MakeInParam("@NodeName",(DbType)SqlDbType.VarChar,200,_Workflow_FlowNodeEntity.NodeName),
                                   };
            string sql = " select * from [dbo].[Workflow_FlowNode] where WorkflowID=@WorkflowID and NodeName=@NodeName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在 
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@NodeName",(DbType)SqlDbType.VarChar,200,_Workflow_FlowNodeEntity.NodeName ),
									   MakeInParam("@NodeDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_FlowNodeEntity.NodeDesc ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.WorkflowID ),
									   MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.NodeTypeID ),
                                       MakeInParam("@IsOverTime",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.IsOverTime ),
									   MakeInParam("@OverTimeLen",(DbType)SqlDbType.Float,8,_Workflow_FlowNodeEntity.OverTimeLen ),
									   MakeInParam("@SignType",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.SignType ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.DisplayOrder ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FlowNode]");
                sb.Append("(");
                sb.Append(" [NodeName]");
                sb.Append(",[NodeDesc]");
                sb.Append(",[WorkflowID]");
                sb.Append(",[NodeTypeID]");
                sb.Append(",[IsOverTime]");
                sb.Append(",[OverTimeLen]");
                sb.Append(",[SignType]");
                sb.Append(",[DisplayOrder]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@NodeName,");
                sb.Append("@NodeDesc,");
                sb.Append("@WorkflowID,");
                sb.Append("@NodeTypeID,");
                sb.Append("@IsOverTime,");
                sb.Append("@OverTimeLen,");
                sb.Append("@SignType,");
                sb.Append("@DisplayOrder )");
                sb.Append("select @@identity;");

                //ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert);

                _Workflow_FlowNodeEntity.NodeID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());
                
                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_FlowNode");
                arlst.Add(_Workflow_FlowNodeEntity.WorkflowID);
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

        public string AddWorkflow_FlowNode2(Workflow_FlowNodeEntity2 _Workflow_FlowNodeEntity)
        {
            //判断该记录是否已经存在

            DbParameter[] prams = {
                                       MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.WorkflowID ),
                                       MakeInParam("@NodeName",(DbType)SqlDbType.VarChar,200,_Workflow_FlowNodeEntity.NodeName),
                                   };
            string sql = " select * from [dbo].[Workflow_FlowNode] where WorkflowID=@WorkflowID and NodeName=@NodeName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在 
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@NodeName",(DbType)SqlDbType.VarChar,200,_Workflow_FlowNodeEntity.NodeName ),
									   MakeInParam("@NodeDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_FlowNodeEntity.NodeDesc ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.WorkflowID ),
									   MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.NodeTypeID ),
                                       MakeInParam("@IsOverTime",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.IsOverTime ),
									   MakeInParam("@OverTimeLen",(DbType)SqlDbType.Float,8,_Workflow_FlowNodeEntity.OverTimeLen ),
									   MakeInParam("@SignType",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.SignType ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.DisplayOrder ),
                                      MakeInParam("@x",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.X ),
                                      MakeInParam("@y",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.Y ),
                                      MakeInParam("@width",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.Width ),
                                      MakeInParam("@height",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.Height ),
                                      };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_FlowNode]");
                sb.Append("(");
                sb.Append(" [NodeName]");
                sb.Append(",[NodeDesc]");
                sb.Append(",[WorkflowID]");
                sb.Append(",[NodeTypeID]");
                sb.Append(",[IsOverTime]");
                sb.Append(",[OverTimeLen]");
                sb.Append(",[SignType]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[x]");
                sb.Append(",[y]");
                sb.Append(",[width]");
                sb.Append(",[height]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@NodeName,");
                sb.Append("@NodeDesc,");
                sb.Append("@WorkflowID,");
                sb.Append("@NodeTypeID,");
                sb.Append("@IsOverTime,");
                sb.Append("@OverTimeLen,");
                sb.Append("@SignType,");
                sb.Append("@DisplayOrder,");
                sb.Append("@x,");
                sb.Append("@y,");
                sb.Append("@width,");
                sb.Append("@height )");

                sb.Append("select @@identity;");

                //ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert);

                _Workflow_FlowNodeEntity.NodeID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());

                ArrayList arlst = new ArrayList();
                arlst.Add("Workflow_FlowNode");
                arlst.Add(_Workflow_FlowNodeEntity.WorkflowID);
                arlst.Add("");
                return sp_ReDisplayOrder(arlst);
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FlowNodeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FlowNode(Workflow_FlowNodeEntity _Workflow_FlowNodeEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.NodeID ),
									   MakeInParam("@NodeName",(DbType)SqlDbType.VarChar,200,_Workflow_FlowNodeEntity.NodeName ),
									   MakeInParam("@NodeDesc",(DbType)SqlDbType.VarChar,2000,_Workflow_FlowNodeEntity.NodeDesc ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.WorkflowID ),
									   MakeInParam("@NodeTypeID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.NodeTypeID ),
                                       MakeInParam("@IsOverTime",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.IsOverTime ),
									   MakeInParam("@OverTimeLen",(DbType)SqlDbType.Float,8,_Workflow_FlowNodeEntity.OverTimeLen ),
									   MakeInParam("@SignType",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.SignType ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.DisplayOrder ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FlowNode]");
            sb.Append(" set ");
            sb.Append(" [NodeName]=@NodeName,");
            sb.Append(" [NodeDesc]=@NodeDesc,");
            sb.Append(" [WorkflowID]=@WorkflowID,");
            sb.Append(" [NodeTypeID]=@NodeTypeID,");
            sb.Append(" [IsOverTime]=@IsOverTime,");
            sb.Append(" [OverTimeLen]=@OverTimeLen,");
            sb.Append(" [SignType]=@SignType,");
            sb.Append(" [DisplayOrder]=@DisplayOrder ");
            sb.Append(" where [NodeID]=@NodeID ");
            ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate);

            ArrayList arlst = new ArrayList();
            arlst.Add("Workflow_FlowNode");
            arlst.Add(_Workflow_FlowNodeEntity.WorkflowID);
            arlst.Add("");
            return sp_ReDisplayOrder(arlst);
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_FlowNode(string NodeID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,NodeID ),
                                         };
            string sql = " Delete from [dbo].[Workflow_FlowNode] where NodeID=@NodeID";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public Workflow_FlowNodeEntity GetWorkflow_FlowNodeEntityByKeyCol(string NodeID)
        {
            string sql = "select * from [dbo].[Workflow_FlowNode] Where NodeID=@NodeID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,50,NodeID ),
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_FlowNodeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_FlowNodeEntity GetWorkflow_FlowNodeFromIDataReader(DbDataReader dr)
        {
            Workflow_FlowNodeEntity dt = new Workflow_FlowNodeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["NodeID"].ToString() != "" || dr["NodeID"] != null) dt.NodeID = Int32.Parse(dr["NodeID"].ToString());
                dt.NodeName = dr["NodeName"].ToString();
                dt.NodeDesc = dr["NodeDesc"].ToString();
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                if (dr["NodeTypeID"].ToString() != "" || dr["NodeTypeID"] != null) dt.NodeTypeID = Int32.Parse(dr["NodeTypeID"].ToString());
                if (dr["IsOverTime"].ToString() != "" || dr["IsOverTime"] != null) dt.IsOverTime = Int32.Parse(dr["IsOverTime"].ToString());
                dt.OverTimeLen = dr["OverTimeLen"].ToString();
                if (dr["SignType"].ToString() != "" || dr["SignType"] != null) dt.SignType = Int32.Parse(dr["SignType"].ToString());
                dt.WithdrawTypeID = dr["WithdrawTypeID"].ToString();
                if (dr["ArchiveFlag"].ToString() != "" || dr["ArchiveFlag"] != null) dt.ArchiveFlag = Int32.Parse(dr["ArchiveFlag"].ToString());
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "出口信息"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeLinkEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeLink(Workflow_NodeLinkEntity _Workflow_NodeLinkEntity)
        {
            //判断该记录是否已经存在 

            DbParameter[] prams = {
                                       MakeInParam("@LinkName",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.LinkName),
                                       MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.WorkflowID ),
                                   };
            string sql = " select * from [dbo].[Workflow_NodeLink] where LinkName=@LinkName and WorkflowID=@WorkflowID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@LinkName",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.LinkName ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.WorkflowID ),
									   MakeInParam("@StartNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.StartNodeID ),
									   MakeInParam("@TargetNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.TargetNodeID ),
									   MakeInParam("@IsRejected",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.IsRejected ),
									   MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.Creator ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_Workflow_NodeLinkEntity.CreateDate ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_Workflow_NodeLinkEntity.lastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeLink]");
                sb.Append("(");
                sb.Append(" [LinkName]");
                sb.Append(",[WorkflowID]");
                sb.Append(",[StartNodeID]");
                sb.Append(",[TargetNodeID]");
                sb.Append(",[IsRejected]");
                sb.Append(",[Creator]");
                sb.Append(",[CreateDate]");
                sb.Append(",[lastModifier]");
                sb.Append(",[lastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@LinkName,");
                sb.Append("@WorkflowID,");
                sb.Append("@StartNodeID,");
                sb.Append("@TargetNodeID,");
                sb.Append("@IsRejected,");
                sb.Append("@Creator,");
                sb.Append("@CreateDate,");
                sb.Append("@lastModifier,");
                sb.Append("@lastModifyDate )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        public string AddWorkflow_NodeLink2(NodeLink _Workflow_NodeLinkEntity)
        {
            //判断该记录是否已经存在 

            DbParameter[] prams = {
                                       MakeInParam("@LinkName",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.LinkName),
                                       MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.WorkflowID ),
                                   };
            string sql = " select * from [dbo].[Workflow_NodeLink] where LinkName=@LinkName and WorkflowID=@WorkflowID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在  
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@LinkName",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.LinkName ),
                                       MakeInParam("@SqlCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeLinkEntity.SqlCondition ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.WorkflowID ),
									   MakeInParam("@StartNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.StartNodeID ),
									   MakeInParam("@TargetNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.TargetNodeID ),
									   MakeInParam("@IsRejected",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.IsRejected ),
									   MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.Creator ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_Workflow_NodeLinkEntity.CreateDate ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_Workflow_NodeLinkEntity.lastModifyDate ),
                                        MakeInParam("@x",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.x ),
									   MakeInParam("@y",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.y ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeLink]");
                sb.Append("(");
                sb.Append(" [LinkName]");
                sb.Append(",[SqlCondition]");
                sb.Append(",[WorkflowID]");
                sb.Append(",[StartNodeID]");
                sb.Append(",[TargetNodeID]");
                sb.Append(",[IsRejected]");
                sb.Append(",[Creator]");
                sb.Append(",[CreateDate]");
                sb.Append(",[lastModifier]");
                sb.Append(",[lastModifyDate]");
                sb.Append(",[x]");
                sb.Append(",[y]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@LinkName,");
                sb.Append("@SqlCondition,");
                sb.Append("@WorkflowID,");
                sb.Append("@StartNodeID,");
                sb.Append("@TargetNodeID,");
                sb.Append("@IsRejected,");
                sb.Append("@Creator,");
                sb.Append("@CreateDate,");
                sb.Append("@lastModifier,");
                sb.Append("@lastModifyDate,");
                sb.Append("@x,");
                sb.Append("@y )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_NodeLinkEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_NodeLink(Workflow_NodeLinkEntity _Workflow_NodeLinkEntity)
        {
            DbParameter[] pramsUpdate = {
                                            MakeInParam("@LinkID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.LinkID ),
                                            MakeInParam("@LinkName",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.LinkName ),
                                            MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.WorkflowID ),
                                            MakeInParam("@StartNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.StartNodeID ),
                                            MakeInParam("@TargetNodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.TargetNodeID ),
                                            MakeInParam("@IsRejected",(DbType)SqlDbType.Int,4,_Workflow_NodeLinkEntity.IsRejected ),
                                            MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_Workflow_NodeLinkEntity.lastModifier ),
                                            MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_Workflow_NodeLinkEntity.lastModifyDate ),
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeLink]");
            sb.Append(" set ");
            sb.Append(" [LinkName]=@LinkName,");
            sb.Append(" [WorkflowID]=@WorkflowID,");
            sb.Append(" [StartNodeID]=@StartNodeID,");
            sb.Append(" [TargetNodeID]=@TargetNodeID,");
            sb.Append(" [IsRejected]=@IsRejected,");
            sb.Append(" [lastModifier]=@lastModifier,");
            sb.Append(" [lastModifyDate]=@lastModifyDate ");
            sb.Append(" where [LinkID]=@LinkID ");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="LinkID"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string DeleteWorkflow_NodeLink(string LinkID)
        {
            DbParameter[] pramsDelete = {
                                            MakeInParam("@LinkID",(DbType)SqlDbType.Int,4,LinkID ),
                                        };
            string sql = "delete from Workflow_NodeLink where LinkID=@LinkID";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }
        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="LinkID"></param>
        /// <returns></returns>
        public Workflow_NodeLinkEntity GetWorkflow_NodeLinkEntityByKeyCol(string LinkID)
        {
            string sql = "select * from [dbo].[Workflow_NodeLink] Where LinkID=@LinkID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@LinkID",(DbType)SqlDbType.VarChar,50,LinkID ),
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_NodeLinkFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_NodeLinkEntity GetWorkflow_NodeLinkFromIDataReader(DbDataReader dr)
        {
            Workflow_NodeLinkEntity dt = new Workflow_NodeLinkEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["LinkID"].ToString() != "" || dr["LinkID"] != null) dt.LinkID = Int32.Parse(dr["LinkID"].ToString());
                dt.LinkName = dr["LinkName"].ToString();
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                if (dr["StartNodeID"].ToString() != "" || dr["StartNodeID"] != null) dt.StartNodeID = Int32.Parse(dr["StartNodeID"].ToString());
                if (dr["TargetNodeID"].ToString() != "" || dr["TargetNodeID"] != null) dt.TargetNodeID = Int32.Parse(dr["TargetNodeID"].ToString());
                if (dr["IsRejected"].ToString() != "" || dr["IsRejected"] != null) dt.IsRejected = Int32.Parse(dr["IsRejected"].ToString());
                dt.Creator = dr["Creator"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dt.lastModifier = dr["lastModifier"].ToString();
                dt.lastModifyDate = Convert.ToDateTime(dr["lastModifyDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "节点字段控制"

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeMainFieldControl(string NodeID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,NodeID ),
                                         };
            return ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_NodeMainFieldControl] where NodeID=@NodeID", pramsDelete).ToString();
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeMainFieldControlEntity"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeMainFieldControl(Workflow_NodeMainFieldControlEntity _Workflow_NodeMainFieldControlEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.NodeID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.FieldID ),
									   MakeInParam("@IsView",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.IsView ),
									   MakeInParam("@IsEdit",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.IsEdit ),
									   MakeInParam("@IsMandatory",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.IsMandatory ),
									   MakeInParam("@BasicValidType",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.BasicValidType ),
									   MakeInParam("@ValidTimeType",(DbType)SqlDbType.Int,4,_Workflow_NodeMainFieldControlEntity.ValidTimeType ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeMainFieldControl]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[FieldID]");
            sb.Append(",[IsView]");
            sb.Append(",[IsEdit]");
            sb.Append(",[IsMandatory]");
            sb.Append(",[BasicValidType]");
            sb.Append(",[ValidTimeType]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@FieldID,");
            sb.Append("@IsView,");
            sb.Append("@IsEdit,");
            sb.Append("@IsMandatory,");
            sb.Append("@BasicValidType,");
            sb.Append("@ValidTimeType )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        public Workflow_NodeMainFieldControlEntity GetWorkflow_NodeMainFieldControlEntityByKeyCol(string NodeID, string FieldID)
        {
            string sql = "select * from [dbo].[Workflow_NodeMainFieldControl] Where NodeID=@NodeID and FieldID=@FieldID";
            DbParameter[] pramsGet = {
                                          MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,50,NodeID ),
                                          MakeInParam("@FieldID",(DbType)SqlDbType.VarChar,50,FieldID ),
                                      };
            Workflow_FlowNodeEntity _FlowNodeEntity = GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
            Workflow_NodeMainFieldControlEntity dt = new Workflow_NodeMainFieldControlEntity();
            dt.NodeID = Int32.Parse(NodeID);
            dt.FieldID = Int32.Parse(FieldID);
            DbDataReader dr = null;
            try
            {
                dr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (dr.Read())
                {
                    if (dr["IsView"].ToString() != "" || dr["IsView"] != null) dt.IsView = Int32.Parse(dr["IsView"].ToString());
                    if (dr["IsEdit"].ToString() != "" || dr["IsEdit"] != null) dt.IsEdit = Int32.Parse(dr["IsEdit"].ToString());
                    if (dr["IsMandatory"].ToString() != "" || dr["IsMandatory"] != null) dt.IsMandatory = Int32.Parse(dr["IsMandatory"].ToString());
                    if (dr["BasicValidType"].ToString() != "" || dr["BasicValidType"] != null) dt.BasicValidType = Int32.Parse(dr["BasicValidType"].ToString());
                    if (dr["ValidTimeType"].ToString() != "" || dr["ValidTimeType"] != null) dt.ValidTimeType = Int32.Parse(dr["ValidTimeType"].ToString());
                    dr.Close();
                }
                else
                {
                    dt.IsView = 1;
                    dt.IsEdit = _FlowNodeEntity.NodeTypeID == 1 ? 1 : 0;
                    dt.IsMandatory = 0;
                    dt.BasicValidType = 0;
                    dt.ValidTimeType = 0;
                }
                return dt;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public Workflow_NodeDetailFieldControlEntity GetWorkflow_NodeDetailFieldControlEntityByKeyCol(string NodeID, string GroupID)
        {
            string sql = "select * from [dbo].[Workflow_NodeDetailFieldControl] Where NodeID=@NodeID and GroupID=@GroupID";
            DbParameter[] pramsGet = {
                                          MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,50,NodeID ),
                                          MakeInParam("@GroupID",(DbType)SqlDbType.VarChar,50,GroupID ),
                                      };
            Workflow_FlowNodeEntity _FlowNodeEntity = GetWorkflow_FlowNodeEntityByKeyCol(NodeID);
            Workflow_NodeDetailFieldControlEntity dt = new Workflow_NodeDetailFieldControlEntity();
            dt.NodeID = Int32.Parse(NodeID);
            dt.GroupID = Int32.Parse(GroupID);
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    if (sr["NodeID"].ToString() != "" || sr["NodeID"] != null) dt.NodeID = Int32.Parse(sr["NodeID"].ToString());
                    if (sr["GroupID"].ToString() != "" || sr["GroupID"] != null) dt.GroupID = Int32.Parse(sr["GroupID"].ToString());
                    if (sr["IsView"].ToString() != "" || sr["IsView"] != null) dt.IsView = Int32.Parse(sr["IsView"].ToString());
                    if (sr["IsAdd"].ToString() != "" || sr["IsAdd"] != null) dt.IsAdd = Int32.Parse(sr["IsAdd"].ToString());
                    if (sr["IsEdit"].ToString() != "" || sr["IsEdit"] != null) dt.IsEdit = Int32.Parse(sr["IsEdit"].ToString());
                    if (sr["IsDelete"].ToString() != "" || sr["IsDelete"] != null) dt.IsDelete = Int32.Parse(sr["IsDelete"].ToString());
                    sr.Close();
                }
                else
                {
                    dt.IsView = 1;
                    dt.IsAdd = _FlowNodeEntity.NodeTypeID == 1 ? 1 : 0;
                    dt.IsEdit = _FlowNodeEntity.NodeTypeID == 1 ? 1 : 0;
                    dt.IsDelete = _FlowNodeEntity.NodeTypeID == 1 ? 1 : 0;
                }
                return dt;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_Workflow_NodeDetailFieldControlEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_NodeDetailFieldControl(Workflow_NodeDetailFieldControlEntity _Workflow_NodeDetailFieldControlEntity)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.NodeID ),
                                             MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.GroupID ),
                                         };
            ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_NodeDetailFieldControl] where NodeID=@NodeID and GroupID=@GroupID", pramsDelete);

            DbParameter[] pramsInsert = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.NodeID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.GroupID ),
									   MakeInParam("@IsView",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.IsView ),
									   MakeInParam("@IsAdd",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.IsAdd ),
									   MakeInParam("@IsEdit",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.IsEdit ),
									   MakeInParam("@IsDelete",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlEntity.IsDelete ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeDetailFieldControl]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[GroupID]");
            sb.Append(",[IsView]");
            sb.Append(",[IsAdd]");
            sb.Append(",[IsEdit]");
            sb.Append(",[IsDelete]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@GroupID,");
            sb.Append("@IsView,");
            sb.Append("@IsAdd,");
            sb.Append("@IsEdit,");
            sb.Append("@IsDelete )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public DataTable sp_GetNodeMainFieldControl(string NodeID)
        {
            DbParameter[] prams = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,NodeID ),
                                   };
            string sql = "[sp_GetNodeMainFieldControl]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }
        #endregion

        #region "功能控制"

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_FlowNodeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_FlowNode2(Workflow_FlowNodeEntity _Workflow_FlowNodeEntity)
        {
            DbParameter[] pramsUpdate = {
                                             MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.NodeID ),
                                             MakeInParam("@WithdrawTypeID",(DbType)SqlDbType.VarChar,50,_Workflow_FlowNodeEntity.WithdrawTypeID ),
                                             MakeInParam("@ArchiveFlag",(DbType)SqlDbType.Int,4,_Workflow_FlowNodeEntity.ArchiveFlag ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_FlowNode]");
            sb.Append(" set ");
            sb.Append(" [WithdrawTypeID]=@WithdrawTypeID,");
            sb.Append(" [ArchiveFlag]=@ArchiveFlag ");
            sb.Append(" where [NodeID]=@NodeID");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        #endregion

        #region "明细字段节点控制"
        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeDetailFieldControlDetail(string NodeID, string GroupID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,NodeID ),
                                             MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID ),
                                         };
            return ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_NodeDetailFieldControlDetail] where NodeID=@NodeID and GroupID=@GroupID", pramsDelete).ToString();
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeDetailFieldControlDetailEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeDetailFieldControlDetail(Workflow_NodeDetailFieldControlDetailEntity _Workflow_NodeDetailFieldControlDetailEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.NodeID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.GroupID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.FieldID ),
									   MakeInParam("@IsView",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.IsView ),
									   MakeInParam("@IsEdit",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.IsEdit ),
									   MakeInParam("@IsMandatory",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.IsMandatory ),
									   MakeInParam("@BasicValidType",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.BasicValidType ),
									   MakeInParam("@ValidTimeType",(DbType)SqlDbType.Int,4,_Workflow_NodeDetailFieldControlDetailEntity.ValidTimeType ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeDetailFieldControlDetail]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[GroupID]");
            sb.Append(",[FieldID]");
            sb.Append(",[IsView]");
            sb.Append(",[IsEdit]");
            sb.Append(",[IsMandatory]");
            sb.Append(",[BasicValidType]");
            sb.Append(",[ValidTimeType]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@GroupID,");
            sb.Append("@FieldID,");
            sb.Append("@IsView,");
            sb.Append("@IsEdit,");
            sb.Append("@IsMandatory,");
            sb.Append("@BasicValidType,");
            sb.Append("@ValidTimeType )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataTable sp_GetNodeDetailFieldControlDetail(string NodeID, string GroupID)
        {
            DbParameter[] prams = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,NodeID ),
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID ),
                                   };
            string sql = "[sp_GetNodeDetailFieldControlDetail]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }
        #endregion

        #region "附加动作0"

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arlparams"></param>
        /// <returns></returns>
        public DataTable sp_GetNodeAddInOperation_Type0(ArrayList arlparams)
        {
            DbParameter[] prams = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.VarChar,100,arlparams[0] ),
									   MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,100,arlparams[1] ),
									   MakeInParam("@OPTime",(DbType)SqlDbType.Char,1,arlparams[2] ),
                                   };
            string sql = "[sp_GetNodeAddInOperation_Type0]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type0Entity"></param>
        /// <param name="dtComputeRouteOperation"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeAddInOperation_Type0(Workflow_NodeAddInOperation_Type0Entity _Type0Entity, DataTable dtComputeRouteOperation)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Type0Entity.NodeID ),
									   MakeInParam("@TargetFieldID",(DbType)SqlDbType.Int,4,_Type0Entity.TargetFieldID ),
									   MakeInParam("@CaculateType",(DbType)SqlDbType.Int,4,_Type0Entity.CaculateType ),
									   MakeInParam("@CaculateValue",(DbType)SqlDbType.VarChar,2000,_Type0Entity.CaculateValue ),
									   MakeInParam("@DataSetID",(DbType)SqlDbType.Int,4,_Type0Entity.DataSetID ),
									   MakeInParam("@ValueField",(DbType)SqlDbType.VarChar,50,_Type0Entity.ValueField ),
									   MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Type0Entity.DataSourceID ),
									   MakeInParam("@StorageProcedure",(DbType)SqlDbType.VarChar,100,_Type0Entity.StorageProcedure ),
									   MakeInParam("@OutputParameter",(DbType)SqlDbType.VarChar,50,_Type0Entity.OutputParameter ),
									   MakeInParam("@OPTime",(DbType)SqlDbType.Int,4,_Type0Entity.OPTime ),
									   MakeInParam("@OPCondition",(DbType)SqlDbType.VarChar,2000,_Type0Entity.OPCondition ),
                                       MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,16,_Type0Entity.CreateDate ),
                                       MakeInParam("@CreateSID",(DbType)SqlDbType.Int,4,_Type0Entity.CreateSID ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type0]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[TargetFieldID]");
            sb.Append(",[CaculateType]");
            sb.Append(",[CaculateValue]");
            sb.Append(",[DataSetID]");
            sb.Append(",[ValueField]");
            sb.Append(",[DataSourceID]");
            sb.Append(",[StorageProcedure]");
            sb.Append(",[OutputParameter]");
            sb.Append(",[OPTime]");
            sb.Append(",[OPCondition]");
            sb.Append(",[CreateDate]");
            sb.Append(",[CreateSID]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@TargetFieldID,");
            sb.Append("@CaculateType,");
            sb.Append("@CaculateValue,");
            sb.Append("@DataSetID,");
            sb.Append("@ValueField,");
            sb.Append("@DataSourceID,");
            sb.Append("@StorageProcedure,");
            sb.Append("@OutputParameter,");
            sb.Append("@OPTime,");
            sb.Append("@OPCondition,");
            sb.Append("@CreateDate,");
            sb.Append("@CreateSID )");
            sb.Append("select @@identity;");
            string AddInOPID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

            for (int i = 0; i < dtComputeRouteOperation.Rows.Count; i++)
            {
                DbParameter[] pramsRouteInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRouteOperation.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRouteOperation.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRouteOperation.Rows[i]["RouteOrder"] ),
                                                  };
                string sqlInsert = @" insert into Workflow_ComputeRouteOperation(AddInOPID,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@ComputeType,@RouteValue,@RouteOrder) ";
                ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
            }
            ExecuteNonQuery(CommandType.Text, @"
insert into Workflow_NodeAddInOperation_Type0_DataSetParameter(AddInOPID,DSParameter,TartgetValue)
select a.AddInOPID,b.ParameterName,b.ParameterValue from  Workflow_NodeAddInOperation_Type0 a
left join Workflow_DataSetParameter b on a.DataSetID=b.DataSetID
left join Workflow_NodeAddInOperation_Type0_DataSetParameter c on a.AddInOPID=c.AddInOPID
where a.CaculateType=3 and c.AddInOPID is null
");
            return AddInOPID;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="CancelSID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type0(string AddInOPID, string CancelSID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
                                             MakeInParam("@CancelSID",(DbType)SqlDbType.Int,4,CancelSID ),
                                         };
            string sql = @"
UPDATE  [dbo].[Workflow_NodeAddInOperation_Type0]
    SET IsCancel   = 1,
        CancelDate = getdate(),
        CancelSID  = @CancelSID
WHERE   AddInOPID = @AddInOPID;
";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }

        #endregion

        #region "附加动作0参数"

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_DataSetParameterEntity"></param>
        /// <param name="dtComputeRouteDetail"></param>
        /// <returns></returns>
        public string UpdateWorkflow_NodeAddInOperation_Type0_DataSetParameter(Workflow_NodeAddInOperation_Type0_DataSetParameterEntity _DataSetParameterEntity, DataTable dtComputeRouteDetail)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_DataSetParameterEntity.AddInOPID ),
									   MakeInParam("@DSParameter",(DbType)SqlDbType.VarChar,100,_DataSetParameterEntity.DSParameter ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_DataSetParameterEntity.TartgetValue ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeAddInOperation_Type0_DataSetParameter]");
            sb.Append(" set ");
            sb.Append(" [TartgetValue]=@TartgetValue ");
            sb.Append(" where [AddInOPID]=@AddInOPID and [DSParameter]=@DSParameter");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate);

            for (int i = 0; i < dtComputeRouteDetail.Rows.Count; i++)
            {
                DbParameter[] pramsRouteInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_DataSetParameterEntity.AddInOPID ),
									   MakeInParam("@DSParameter",(DbType)SqlDbType.VarChar,50,_DataSetParameterEntity.DSParameter ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRouteDetail.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRouteDetail.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRouteDetail.Rows[i]["RouteOrder"] ),
                                                  };
                string sqlInsert = @" insert into Workflow_ComputeRouteDetail(AddInOPID,DSParameter,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@DSParameter,@ComputeType,@RouteValue,@RouteOrder) ";
                ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
            }

            return "1";
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="DSParameter"></param>
        /// <returns></returns>
        public string DeleteNodeAddInOperation_Type0_DataSetParameter(string AddInOPID, string DSParameter)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
                                             MakeInParam("@DSParameter",(DbType)SqlDbType.VarChar,50,DSParameter ),
                                         };
            string sql = @"
update Workflow_NodeAddInOperation_Type0_DataSetParameter
set TartgetValue=a.ParameterValue
from Workflow_DataSetParameter a,Workflow_NodeAddInOperation_Type0 b
where a.DataSetID=b.DataSetID and b.AddInOPID=@AddInOPID
and Workflow_NodeAddInOperation_Type0_DataSetParameter.DSParameter=a.ParameterName
and a.ParameterName=DSParameter
delete from Workflow_ComputeRouteDetail where AddInOPID=@AddInOPID and DSParameter=@DSParameter
";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();

        }

        #endregion

        #region "附加动作1"

        /// <summary>
        /// 获取一览信息
        /// </summary>
        /// <param name="arlparams"></param>
        /// <returns></returns>
        public DataTable sp_GetNodeAddInOperation_Type1(ArrayList arlparams)
        {
            DbParameter[] prams = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.VarChar,100,arlparams[0] ),
									   MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,100,arlparams[1] ),
									   MakeInParam("@OPTime",(DbType)SqlDbType.Char,1,arlparams[2] ),
                                   };
            string sql = "[sp_GetNodeAddInOperation_Type1]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type1Entity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeAddInOperation_Type1(Workflow_NodeAddInOperation_Type1Entity _Type1Entity)
        {
            DbParameter[] pramsInsert = {
                                            MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1Entity.AddInOPID ),
                                            MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Type1Entity.NodeID ),
                                            MakeInParam("@CaculateType",(DbType)SqlDbType.Int,4,_Type1Entity.CaculateType ),
                                            MakeInParam("@DataSourceID",(DbType)SqlDbType.Int,4,_Type1Entity.DataSourceID ),
                                            MakeInParam("@DataSourceTable",(DbType)SqlDbType.VarChar,50,_Type1Entity.DataSourceTable ),
                                            MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_Type1Entity.GroupID ),
                                            MakeInParam("@SelectRange",(DbType)SqlDbType.VarChar,2000,_Type1Entity.SelectRange ),
                                            MakeInParam("@OPCycleType",(DbType)SqlDbType.Int,4,_Type1Entity.OPCycleType ),
                                            MakeInParam("@OPTime",(DbType)SqlDbType.Int,4,_Type1Entity.OPTime ),
                                            MakeInParam("@OPCondition",(DbType)SqlDbType.VarChar,2000,_Type1Entity.OPCondition ),
                                            MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,16,_Type1Entity.CreateDate ),
                                            MakeInParam("@CreateSID",(DbType)SqlDbType.Int,4,_Type1Entity.CreateSID ),
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type1]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[CaculateType]");
            sb.Append(",[DataSourceID]");
            sb.Append(",[DataSourceTable]");
            sb.Append(",[GroupID]");
            sb.Append(",[SelectRange]");
            sb.Append(",[OPCycleType]");
            sb.Append(",[OPTime]");
            sb.Append(",[OPCondition]");
            sb.Append(",[CreateDate]");
            sb.Append(",[CreateSID]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@CaculateType,");
            sb.Append("@DataSourceID,");
            sb.Append("@DataSourceTable,");
            sb.Append("@GroupID,");
            sb.Append("@SelectRange,");
            sb.Append("@OPCycleType,");
            sb.Append("@OPTime,");
            sb.Append("@OPCondition,");
            sb.Append("@CreateDate,");
            sb.Append("@CreateSID )");
            sb.Append("select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="CancelSID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type1(string AddInOPID, string CancelSID)
        {
            DbParameter[] pramsDelete = {
                                            MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
                                            MakeInParam("@CancelSID",(DbType)SqlDbType.Int,4,CancelSID ),
                                        };
            string sql = @"
UPDATE  [dbo].[Workflow_NodeAddInOperation_Type1]
    SET IsCancel   = 1,
        CancelDate = getdate(),
        CancelSID  = @CancelSID
WHERE   AddInOPID = @AddInOPID;
";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }

        #endregion

        #region "对象值匹配"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type1_ColMappingEntity"></param>
        /// <param name="dtComputeRoute"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeAddInOperation_Type1_ColMapping(Workflow_NodeAddInOperation_Type1_ColMappingEntity _Type1_ColMappingEntity, DataTable dtComputeRoute)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                      MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColMappingEntity.AddInOPID ),
                                      MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,_Type1_ColMappingEntity.TargetFieldName ),
                                  };
            string sql = " select * from [dbo].[Workflow_NodeAddInOperation_Type1_ColMapping] where AddInOPID=@AddInOPID and TargetFieldName=@TargetFieldName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColMappingEntity.AddInOPID ),
									   MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,_Type1_ColMappingEntity.TargetFieldName ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type1_ColMappingEntity.TartgetValue ),
									   MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Type1_ColMappingEntity.FieldTypeID ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type1_ColMapping]");
                sb.Append("(");
                sb.Append(" [AddInOPID]");
                sb.Append(",[TargetFieldName]");
                sb.Append(",[TartgetValue]");
                sb.Append(",[FieldTypeID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@AddInOPID,");
                sb.Append("@TargetFieldName,");
                sb.Append("@TartgetValue,");
                sb.Append("@FieldTypeID )");
                sb.Append("select @@identity;");
                ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

                for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
                {
                    DbParameter[] pramsRouteInsert = {
                                                         MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColMappingEntity.AddInOPID ),
                                                         MakeInParam("@TargetField",(DbType)SqlDbType.VarChar,50,_Type1_ColMappingEntity.TargetFieldName ),
                                                         MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["ComputeType"] ),
                                                         MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRoute.Rows[i]["RouteValue"] ),
                                                         MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["RouteOrder"] ),
                                                     };
                    string sqlInsert = @" insert into Workflow_ComputeRouteColMapping(AddInOPID,TargetField,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@TargetField,@ComputeType,@RouteValue,@RouteOrder) ";
                    ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
                }
                return "1";
            }
        }

        /// <summary>
        /// 删除对象值匹配
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="TargetFieldName"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type1_ColMapping(string AddInOPID, string TargetFieldName)
        {
            DbParameter[] pramsDelete = {
                                            MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
                                            MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,TargetFieldName ),
                                        };
            string sql = @"
delete from [dbo].[Workflow_NodeAddInOperation_Type1_ColMapping] where AddInOPID=@AddInOPID and TargetFieldName=@TargetFieldName;
delete from [dbo].[Workflow_ComputeRouteColMapping] where AddInOPID=@AddInOPID and TargetField=@TargetFieldName;

";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }



        #endregion

        #region "对象值筛选"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type1_ColConditionEntity"></param>
        /// <param name="dtComputeRoute"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeAddInOperation_Type1_ColCondition(Workflow_NodeAddInOperation_Type1_ColConditionEntity _Type1_ColConditionEntity, DataTable dtComputeRoute)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                      MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColConditionEntity.AddInOPID ),
                                      MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,_Type1_ColConditionEntity.TargetFieldName ),
                                  };
            string sql = " select * from [dbo].[Workflow_NodeAddInOperation_Type1_ColCondition] where AddInOPID=@AddInOPID and TargetFieldName=@TargetFieldName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColConditionEntity.AddInOPID ),
									   MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,_Type1_ColConditionEntity.TargetFieldName ),
									   MakeInParam("@CompareSymbol",(DbType)SqlDbType.VarChar,50,_Type1_ColConditionEntity.CompareSymbol ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type1_ColConditionEntity.TartgetValue ),
									   MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Type1_ColConditionEntity.FieldTypeID ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE [dbo].[Workflow_NodeAddInOperation_Type1_ColCondition] set AndOr='AND' where AddInOPID=@AddInOPID and (AndOr='' or AndOr is null)");
                sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type1_ColCondition]");
                sb.Append("(");
                sb.Append(" [AddInOPID]");
                sb.Append(",[TargetFieldName]");
                sb.Append(",[CompareSymbol]");
                sb.Append(",[TartgetValue]");
                sb.Append(",[FieldTypeID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@AddInOPID,");
                sb.Append("@TargetFieldName,");
                sb.Append("@CompareSymbol,");
                sb.Append("@TartgetValue,");
                sb.Append("@FieldTypeID )");
                sb.Append("select @@identity;");
                ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

                for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
                {
                    DbParameter[] pramsRouteInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_ColConditionEntity.AddInOPID ),
									   MakeInParam("@TargetField",(DbType)SqlDbType.VarChar,50,_Type1_ColConditionEntity.TargetFieldName ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRoute.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["RouteOrder"] ),
                                                  };
                    string sqlInsert = @" insert into Workflow_ComputeRouteColCondition(AddInOPID,TargetField,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@TargetField,@ComputeType,@RouteValue,@RouteOrder) ";
                    ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
                }

                return "1";
            }
        }

        /// <summary>
        /// 删除对象值筛选
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="TargetFieldName"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type1_ColCondition(string AddInOPID, string TargetFieldName)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),
                                             MakeInParam("@TargetFieldName",(DbType)SqlDbType.VarChar,50,TargetFieldName ),
                                         };
            string sql = @"
delete from [dbo].[Workflow_NodeAddInOperation_Type1_ColCondition] where AddInOPID=@AddInOPID and TargetFieldName=@TargetFieldName;
delete from [dbo].[Workflow_ComputeRouteColCondition] where AddInOPID=@AddInOPID and TargetField=@TargetFieldName;

";
            return ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();
        }



        #endregion

        #region "SP参数"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type1_SpParameterEntity"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeAddInOperation_Type1_SpParameter(Workflow_NodeAddInOperation_Type1_SpParameterEntity _Type1_SpParameterEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                       MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,100,_Type1_SpParameterEntity.SpParameter ),
                                   };
            string sql = " SELECT * from [dbo].[Workflow_NodeAddInOperation_Type1_SpParameter] where AddInOPID=@AddInOPID and SpParameter=@SpParameter";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
                                                 MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.AddInOPID ),
                                                 MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,100,_Type1_SpParameterEntity.SpParameter ),
                                                 MakeInParam("@ParameterType",(DbType)SqlDbType.VarChar,50,_Type1_SpParameterEntity.ParameterType ),
                                                 MakeInParam("@ParameterDirection",(DbType)SqlDbType.VarChar,50,_Type1_SpParameterEntity.ParameterDirection ),
                                                 MakeInParam("@ParameterSize",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.ParameterSize ),
                                                 MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type1_SpParameterEntity.TartgetValue ),
                                                 MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.FieldTypeID ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type1_SpParameter]");
                sb.Append("(");
                sb.Append(" [AddInOPID]");
                sb.Append(",[SpParameter]");
                sb.Append(",[ParameterType]");
                sb.Append(",[ParameterDirection]");
                sb.Append(",[ParameterSize]");
                sb.Append(",[TartgetValue]"); ;
                sb.Append(",[FieldTypeID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@AddInOPID,");
                sb.Append("@SpParameter,");
                sb.Append("@ParameterType,");
                sb.Append("@ParameterDirection,");
                sb.Append("@ParameterSize,");
                sb.Append("@TartgetValue,");
                sb.Append("@FieldTypeID )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_Type1_SpParameterEntity"></param>
        /// <param name="dtComputeRoute"></param>
        /// <returns></returns>
        public string UpdateWorkflow_NodeAddInOperation_Type1_SpParameter(Workflow_NodeAddInOperation_Type1_SpParameterEntity _Type1_SpParameterEntity, DataTable dtComputeRoute)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type1_SpParameterEntity.SpParameter ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type1_SpParameterEntity.TartgetValue ),
									   MakeInParam("@FieldTypeID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.FieldTypeID ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeAddInOperation_Type1_SpParameter]");
            sb.Append(" set ");
            sb.Append(" [TartgetValue]=@TartgetValue,");
            sb.Append(" [FieldTypeID]=@FieldTypeID ");
            sb.Append(" where [AddInOPID]=@AddInOPID and [SpParameter]=@SpParameter ");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();

            DbParameter[] pramsDelete = {
                                             MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.AddInOPID ),
                                             MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type1_SpParameterEntity.SpParameter ),
                                         };
            string sql = @"
delete from [dbo].[Workflow_ComputeRouteSpParameter] where AddInOPID=@AddInOPID and SpParameter=@SpParameter;
";
            ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();

            for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
            {
                DbParameter[] pramsRouteInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type1_SpParameterEntity.SpParameter ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRoute.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["RouteOrder"] ),
                                                  };
                string sqlInsert = @" insert into Workflow_ComputeRouteSpParameter(AddInOPID,SpParameter,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@SpParameter,@ComputeType,@RouteValue,@RouteOrder) ";
                ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
            }

            return "1";
        }

        #endregion

        #region "SP参数"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type0_SpParameterEntity"></param>
        /// <returns></returns>
        public string AddWorkflow_NodeAddInOperation_Type0_SpParameter(Workflow_NodeAddInOperation_Type0_SpParameterEntity _Type0_SpParameterEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = {
                                      MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.AddInOPID ),
                                      MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,100,_Type0_SpParameterEntity.SpParameter ),
                                  };
            string sql = " SELECT * from [dbo].[Workflow_NodeAddInOperation_Type0_SpParameter] where AddInOPID=@AddInOPID and SpParameter=@SpParameter";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,100,_Type0_SpParameterEntity.SpParameter ),
									   MakeInParam("@ParameterType",(DbType)SqlDbType.VarChar,50,_Type0_SpParameterEntity.ParameterType ),
									   MakeInParam("@ParameterDirection",(DbType)SqlDbType.VarChar,50,_Type0_SpParameterEntity.ParameterDirection ),
									   MakeInParam("@ParameterSize",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.ParameterSize ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type0_SpParameterEntity.TartgetValue ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type0_SpParameter]");
                sb.Append("(");
                sb.Append(" [AddInOPID]");
                sb.Append(",[SpParameter]");
                sb.Append(",[ParameterType]");
                sb.Append(",[ParameterDirection]");
                sb.Append(",[ParameterSize]");
                sb.Append(",[TartgetValue]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@AddInOPID,");
                sb.Append("@SpParameter,");
                sb.Append("@ParameterType,");
                sb.Append("@ParameterDirection,");
                sb.Append("@ParameterSize,");
                sb.Append("@TartgetValue )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="_Type0_SpParameterEntity"></param>
        /// <param name="dtComputeRoute"></param>
        /// <returns></returns>
        public string UpdateWorkflow_NodeAddInOperation_Type0_SpParameter(Workflow_NodeAddInOperation_Type0_SpParameterEntity _Type0_SpParameterEntity, DataTable dtComputeRoute)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type0_SpParameterEntity.SpParameter ),
									   MakeInParam("@TartgetValue",(DbType)SqlDbType.VarChar,2000,_Type0_SpParameterEntity.TartgetValue ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeAddInOperation_Type0_SpParameter]");
            sb.Append(" set ");
            sb.Append(" [TartgetValue]=@TartgetValue ");
            sb.Append(" where [AddInOPID]=@AddInOPID and [SpParameter]=@SpParameter ");
            ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();

            DbParameter[] pramsDelete = {
                                             MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.AddInOPID ),
                                             MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type0_SpParameterEntity.SpParameter ),
                                         };
            string sql = @"
delete from [dbo].[Workflow_ComputeRouteSpParameter0] where AddInOPID=@AddInOPID and SpParameter=@SpParameter;
";
            ExecuteNonQuery(CommandType.Text, sql, pramsDelete).ToString();

            for (int i = 0; i < dtComputeRoute.Rows.Count; i++)
            {
                DbParameter[] pramsRouteInsert = {
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0_SpParameterEntity.AddInOPID ),
									   MakeInParam("@SpParameter",(DbType)SqlDbType.VarChar,50,_Type0_SpParameterEntity.SpParameter ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["ComputeType"] ),
									   MakeInParam("@RouteValue",(DbType)SqlDbType.VarChar,50,dtComputeRoute.Rows[i]["RouteValue"] ),
									   MakeInParam("@RouteOrder",(DbType)SqlDbType.Int,4,dtComputeRoute.Rows[i]["RouteOrder"] ),
                                                  };
                string sqlInsert = @" insert into Workflow_ComputeRouteSpParameter0(AddInOPID,SpParameter,ComputeType,RouteValue,RouteOrder)values(@AddInOPID,@SpParameter,@ComputeType,@RouteValue,@RouteOrder) ";
                ExecuteScalar(CommandType.Text, sqlInsert, pramsRouteInsert);
            }

            return "1";
        }

        #endregion

        #region "附加动作0触发条件"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type0ConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeAddInOperation_Type0Condition(Workflow_NodeAddInOperation_Type0ConditionEntity _Type0ConditionEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.Int,4,_Type0ConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.Int,4,_Type0ConditionEntity.BranchBatchSeq ),
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type0ConditionEntity.AddInOPID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Type0ConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_Type0ConditionEntity.SymbolCode ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_Type0ConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,50,_Type0ConditionEntity.AndOr ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type0Condition]");
            sb.Append("(");
            sb.Append(" [BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[AddInOPID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@AddInOPID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type0Condition(string ConditionID)
        {
            DbParameter[] prams = {
                                MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,ConditionID ),//条件ID
                                  };

            string sql = @"
delete from Workflow_NodeAddInOperation_Type0Condition 
where BatchSeq in
(
select BatchSeq
from
Workflow_NodeAddInOperation_Type0Condition
where ConditionID=@ConditionID
) and AddInOPID = 
(
select AddInOPID
from
Workflow_NodeAddInOperation_Type0Condition
where ConditionID=@ConditionID
)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        #endregion

        #region "附加动作1触发条件"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Type1ConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeAddInOperation_Type1Condition(Workflow_NodeAddInOperation_Type1ConditionEntity _Type1ConditionEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.Int,4,_Type1ConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.Int,4,_Type1ConditionEntity.BranchBatchSeq ),
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_Type1ConditionEntity.AddInOPID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Type1ConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_Type1ConditionEntity.SymbolCode ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_Type1ConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,50,_Type1ConditionEntity.AndOr ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeAddInOperation_Type1Condition]");
            sb.Append("(");
            sb.Append(" [BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[AddInOPID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@AddInOPID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeAddInOperation_Type1Condition(string ConditionID)
        {
            DbParameter[] prams = {
                                MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,ConditionID ),//条件ID
                                  };

            string sql = @"
delete from Workflow_NodeAddInOperation_Type1Condition 
where BatchSeq in
(
select BatchSeq
from
Workflow_NodeAddInOperation_Type1Condition
where ConditionID=@ConditionID
) and AddInOPID = 
(
select AddInOPID
from
Workflow_NodeAddInOperation_Type1Condition
where ConditionID=@ConditionID
)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        #endregion

        #region "节点操作者信息"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeOperatorDetailEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeOperatorDetail(Workflow_NodeOperatorDetailEntity _Workflow_NodeOperatorDetailEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.NodeID ),
									   MakeInParam("@RuleType",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.RuleType ),
									   MakeInParam("@RuleCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeOperatorDetailEntity.RuleCondition ),
									   MakeInParam("@ObjectValue",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeOperatorDetailEntity.ObjectValue ),
									   MakeInParam("@RuleName",(DbType)SqlDbType.VarChar,200,_Workflow_NodeOperatorDetailEntity.RuleName ),
									   MakeInParam("@RuleSeq",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.RuleSeq ),
									   MakeInParam("@SecurityStart",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SecurityStart ),
									   MakeInParam("@SecurityEnd",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SecurityEnd ),
									   MakeInParam("@LevelStart",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.LevelStart ),
									   MakeInParam("@LevelEnd",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.LevelEnd ),
									   MakeInParam("@SignType",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SignType ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeOperatorDetail]");
            sb.Append("(");
            sb.Append(" [NodeID]");
            sb.Append(",[RuleType]");
            sb.Append(",[RuleCondition]");
            sb.Append(",[ObjectValue]");
            sb.Append(",[RuleName]");
            sb.Append(",[RuleSeq]");
            sb.Append(",[SecurityStart]");
            sb.Append(",[SecurityEnd]");
            sb.Append(",[LevelStart]");
            sb.Append(",[LevelEnd]");
            sb.Append(",[SignType]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@NodeID,");
            sb.Append("@RuleType,");
            sb.Append("@RuleCondition,");
            sb.Append("@ObjectValue,");
            sb.Append("@RuleName,");
            sb.Append("@RuleSeq,");
            sb.Append("@SecurityStart,");
            sb.Append("@SecurityEnd,");
            sb.Append("@LevelStart,");
            sb.Append("@LevelEnd,");
            sb.Append("@SignType )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_NodeOperatorDetailEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_NodeOperatorDetail(Workflow_NodeOperatorDetailEntity _Workflow_NodeOperatorDetailEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@RuleID",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.RuleID ),
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.NodeID ),
									   MakeInParam("@RuleType",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.RuleType ),
									   MakeInParam("@RuleCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeOperatorDetailEntity.RuleCondition ),
									   MakeInParam("@ObjectValue",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeOperatorDetailEntity.ObjectValue ),
									   MakeInParam("@RuleName",(DbType)SqlDbType.VarChar,200,_Workflow_NodeOperatorDetailEntity.RuleName ),
									   MakeInParam("@RuleSeq",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.RuleSeq ),
									   MakeInParam("@SecurityStart",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SecurityStart ),
									   MakeInParam("@SecurityEnd",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SecurityEnd ),
									   MakeInParam("@LevelStart",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.LevelStart ),
									   MakeInParam("@LevelEnd",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.LevelEnd ),
									   MakeInParam("@SignType",(DbType)SqlDbType.Int,4,_Workflow_NodeOperatorDetailEntity.SignType ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeOperatorDetail]");
            sb.Append(" set ");
            sb.Append(" [NodeID]=@NodeID,");
            sb.Append(" [RuleType]=@RuleType,");
            sb.Append(" [RuleCondition]=@RuleCondition,");
            sb.Append(" [ObjectValue]=@ObjectValue,");
            sb.Append(" [RuleName]=@RuleName,");
            sb.Append(" [RuleSeq]=@RuleSeq,");
            sb.Append(" [SecurityStart]=@SecurityStart,");
            sb.Append(" [SecurityEnd]=@SecurityEnd,");
            sb.Append(" [LevelStart]=@LevelStart,");
            sb.Append(" [LevelEnd]=@LevelEnd,");
            sb.Append(" [SignType]=@SignType ");
            sb.Append(" where [RuleID]=@RuleID ");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public Workflow_NodeOperatorDetailEntity GetWorkflow_NodeOperatorDetailEntityByKeyCol(string RuleID)
        {
            string sql = "select * from [dbo].[Workflow_NodeOperatorDetail] Where [RuleID]=@RuleID";
            DbParameter[] pramsGet = {
                                          MakeInParam("@RuleID",(DbType)SqlDbType.VarChar,50,RuleID ),
                                      };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_NodeOperatorDetailFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_NodeOperatorDetailEntity GetWorkflow_NodeOperatorDetailFromIDataReader(DbDataReader dr)
        {
            Workflow_NodeOperatorDetailEntity dt = new Workflow_NodeOperatorDetailEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["RuleID"].ToString() != "" || dr["RuleID"] != null) dt.RuleID = Int32.Parse(dr["RuleID"].ToString());
                if (dr["NodeID"].ToString() != "" || dr["NodeID"] != null) dt.NodeID = Int32.Parse(dr["NodeID"].ToString());
                if (dr["RuleType"].ToString() != "" || dr["RuleType"] != null) dt.RuleType = Int32.Parse(dr["RuleType"].ToString());
                dt.RuleCondition = dr["RuleCondition"].ToString();
                dt.ObjectValue = dr["ObjectValue"].ToString();
                dt.RuleName = dr["RuleName"].ToString();
                if (dr["RuleSeq"].ToString() != "" || dr["RuleSeq"] != null) dt.RuleSeq = Int32.Parse(dr["RuleSeq"].ToString());
                if (dr["SecurityStart"].ToString() != "" || dr["SecurityStart"] != null) dt.SecurityStart = Int32.Parse(dr["SecurityStart"].ToString());
                if (dr["SecurityEnd"].ToString() != "" || dr["SecurityEnd"] != null) dt.SecurityEnd = Int32.Parse(dr["SecurityEnd"].ToString());
                if (dr["LevelStart"].ToString() != "" || dr["LevelStart"] != null) dt.LevelStart = Int32.Parse(dr["LevelStart"].ToString());
                if (dr["LevelEnd"].ToString() != "" || dr["LevelEnd"] != null) dt.LevelEnd = Int32.Parse(dr["LevelEnd"].ToString());
                if (dr["SignType"].ToString() != "" || dr["SignType"] != null) dt.SignType = Int32.Parse(dr["SignType"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 生成工作流的创建者
        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <returns></returns>
        public string sp_GeneratorWorkflowCreatorList(string WorkflowID)
        {
            DbParameter[] prams = {
                                MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,WorkflowID ),//工作流ID
                                  };

            string sql = "[sp_GeneratorWorkflowCreatorList]";
            return ExecuteNonQuery(CommandType.StoredProcedure, sql, prams).ToString();
        }

        /// <summary>
        /// 获取操作者抓取规则一览
        /// </summary>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public DataTable sp_GetNodeOperatorDetail(string NodeID)
        {
            DbParameter[] prams = {
                                MakeInParam("@NodeID",(DbType)SqlDbType.VarChar,100,NodeID ),//节点ID
                                  };

            string sql = "[sp_GetNodeOperatorDetail]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 删除操作者抓取规则
        /// </summary>
        /// <param name="RuleID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeOperatorDetail(string RuleID)
        {
            DbParameter[] prams = {
                                MakeInParam("@RuleID",(DbType)SqlDbType.VarChar,100,RuleID ),//规则ID
                                  };

            string sql = "delete from [dbo].[Workflow_NodeOperatorDetail] Where [RuleID]=@RuleID ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }
        #endregion

        #region "流转条件"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeCondition(Workflow_NodeConditionEntity _Workflow_NodeConditionEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.Int,4,_Workflow_NodeConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.Int,4,_Workflow_NodeConditionEntity.BranchBatchSeq ),
									   MakeInParam("@LinkID",(DbType)SqlDbType.Int,4,_Workflow_NodeConditionEntity.LinkID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_Workflow_NodeConditionEntity.SymbolCode ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_Workflow_NodeConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,50,_Workflow_NodeConditionEntity.AndOr ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeCondition]");
            sb.Append("(");
            sb.Append(" [BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[LinkID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@LinkID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeCondition(string ConditionID)
        {
            DbParameter[] prams = {
                                MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,ConditionID ),//条件ID
                                  };

            string sql = @"
delete from Workflow_NodeCondition 
where BatchSeq in
(
select BatchSeq
from
Workflow_NodeCondition
where ConditionID=@ConditionID
) and LinkID = 
(
select LinkID
from
Workflow_NodeCondition
where ConditionID=@ConditionID
)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// UpdateSqlCondition
        /// </summary>
        /// <param name="WhereCondition"></param>
        /// <param name="LinkID"></param>
        /// <returns></returns>
        public string UpdateSqlCondition(string WhereCondition, string LinkID)
        {
            DbParameter[] prams = {
                                MakeInParam("@WhereCondition",(DbType)SqlDbType.VarChar,2000,WhereCondition ),//流转ID
                                MakeInParam("@LinkID",(DbType)SqlDbType.Int,4,LinkID ),//流转ID
                                   };

            string sql = @" update Workflow_NodeLink set SqlCondition=@WhereCondition where LinkID=@LinkID ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }




        #endregion

        #region "规则条件"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_OperatorConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_OperatorCondition(Workflow_OperatorConditionEntity _Workflow_OperatorConditionEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.Int,4,_Workflow_OperatorConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.Int,4,_Workflow_OperatorConditionEntity.BranchBatchSeq ),
									   MakeInParam("@RuleID",(DbType)SqlDbType.Int,4,_Workflow_OperatorConditionEntity.RuleID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_OperatorConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_Workflow_OperatorConditionEntity.SymbolCode ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_Workflow_OperatorConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,50,_Workflow_OperatorConditionEntity.AndOr ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_OperatorCondition]");
            sb.Append("(");
            sb.Append(" [BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[RuleID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@RuleID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_OperatorCondition(string ConditionID)
        {
            DbParameter[] prams = {
                                MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,ConditionID ),//条件ID
                                  };

            string sql = @"
delete from Workflow_OperatorCondition 
where BatchSeq in
(
select BatchSeq
from
Workflow_OperatorCondition
where ConditionID=@ConditionID
) and RuleID = 
(
select RuleID
from
Workflow_OperatorCondition
where ConditionID=@ConditionID
)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        #endregion

        #region "取值范围"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_NodeOperationType1ConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeOperationType1Condition(Workflow_NodeOperationType1ConditionEntity _NodeOperationType1ConditionEntity)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.Int,4,_NodeOperationType1ConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.Int,4,_NodeOperationType1ConditionEntity.BranchBatchSeq ),
									   MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,_NodeOperationType1ConditionEntity.AddInOPID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_NodeOperationType1ConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_NodeOperationType1ConditionEntity.SymbolCode ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_NodeOperationType1ConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,50,_NodeOperationType1ConditionEntity.AndOr ),
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeOperationType1Condition]");
            sb.Append("(");
            sb.Append(" [BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[AddInOPID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@AddInOPID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr )");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 条件ID
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeOperationType1Condition(string ConditionID)
        {
            DbParameter[] prams = {
                                MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,ConditionID ),//条件ID
                                  };

            string sql = @"
delete from Workflow_NodeOperationType1Condition 
where BatchSeq in
(
select BatchSeq
from
Workflow_NodeOperationType1Condition
where ConditionID=@ConditionID
) and AddInOPID = 
(
select AddInOPID
from
Workflow_NodeOperationType1Condition
where ConditionID=@ConditionID
)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// UpdateNodeOperationType1Condition
        /// </summary>
        /// <param name="WhereCondition"></param>
        /// <param name="AddInOPID"></param>
        /// <returns></returns>
        public string UpdateNodeOperationType1Condition(string WhereCondition, string AddInOPID)
        {
            DbParameter[] prams = {
                                MakeInParam("@WhereCondition",(DbType)SqlDbType.VarChar,2000,WhereCondition ),//流转ID
                                MakeInParam("@AddInOPID",(DbType)SqlDbType.Int,4,AddInOPID ),//流转ID
                                   };

            string sql = @" update Workflow_NodeAddInOperation_Type1 set SelectRange=@WhereCondition where AddInOPID=@AddInOPID ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        #endregion

        #region "节点触发子流程"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeTriggerWorkflowEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeTriggerWorkflow(Workflow_NodeTriggerWorkflowEntity _Workflow_NodeTriggerWorkflowEntity)
        {
            //判断该记录是否已经存在


            DbParameter[] prams = { MakeInParam("@TriggerID", (DbType)SqlDbType.Int, 4, _Workflow_NodeTriggerWorkflowEntity.TriggerID) };

            string sql = "select * from  Workflow_NodeTriggerWorkflow where IsCancel=0 and  TriggerID=@TriggerID";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在


            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WorkflowID ),
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.NodeID ),
									   MakeInParam("@OPTime",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.OPTime ),
									   MakeInParam("@OPCondition",(DbType)SqlDbType.VarChar,2000,_Workflow_NodeTriggerWorkflowEntity.OPCondition ),
									   MakeInParam("@TriggerWFID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.TriggerWFID ),
									   MakeInParam("@TriggerWFCreator",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.TriggerWFCreator ),
									   MakeInParam("@WFCreateNode",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WFCreateNode ),
									   MakeInParam("@WFCreateFieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WFCreateFieldID ),
									  MakeInParam("@CreateSID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.CreateSID )

                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_NodeTriggerWorkflow]");
                sb.Append("(");
                sb.Append("[WorkflowID]");
                sb.Append(",[NodeID]");
                sb.Append(",[OPTime]");
                sb.Append(",[OPCondition]");
                sb.Append(",[TriggerWFID]");
                sb.Append(",[TriggerWFCreator]");
                sb.Append(",[WFCreateNode]");
                sb.Append(",[WFCreateFieldID]");
                sb.Append(",[CreateSID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@WorkflowID,");
                sb.Append("@NodeID,");
                sb.Append("@OPTime,");
                sb.Append("@OPCondition,");
                sb.Append("@TriggerWFID,");
                sb.Append("@TriggerWFCreator,");
                sb.Append("@WFCreateNode,");
                sb.Append("@WFCreateFieldID");
                sb.Append(",@CreateSID");
                sb.Append(") ;");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_NodeTriggerWorkflowEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_NodeTriggerWorkflow(Workflow_NodeTriggerWorkflowEntity _Workflow_NodeTriggerWorkflowEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WorkflowID ),
									   MakeInParam("@NodeID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.NodeID ),
									   MakeInParam("@OPTime",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.OPTime ),
									   MakeInParam("@TriggerWFID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.TriggerWFID ),
									   MakeInParam("@TriggerWFCreator",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.TriggerWFCreator ),
									   MakeInParam("@WFCreateNode",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WFCreateNode ),
									   MakeInParam("@WFCreateFieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.WFCreateFieldID ),
                                       MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerWorkflowEntity.TriggerID )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_NodeTriggerWorkflow]");
            sb.Append(" set ");
            sb.Append(" [WorkflowID]=@WorkflowID,");
            sb.Append(" [NodeID]=@NodeID,");
            sb.Append(" [OPTime]=@OPTime,");
            sb.Append(" [TriggerWFID]=@TriggerWFID,");
            sb.Append(" [TriggerWFCreator]=@TriggerWFCreator,");
            sb.Append(" [WFCreateNode]=@WFCreateNode,");
            sb.Append(" [WFCreateFieldID]=@WFCreateFieldID");
            sb.Append(" where ");
            sb.Append(" [TriggerID]=@TriggerID;select @@rowcount");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="TriggerID"></param>
        /// <param name="CancelSID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_NodeTriggerWorkflow(string TriggerID, int CancelSID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,Int32.Parse(TriggerID )),
                                             MakeInParam("@CancelSID",(DbType)SqlDbType.Int,4,CancelSID )
                                         };
            return ExecuteNonQuery(CommandType.Text, "update  [Workflow_NodeTriggerWorkflow] set IsCancel=1,CancelSID=@CancelSID where TriggerID=@TriggerID", pramsDelete).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public Workflow_NodeTriggerWorkflowEntity GetWorkflow_NodeTriggerWorkflowEntityByKeyCol(string KeyCol)
        {
            string sql = "select *  from Workflow_NodeTriggerWorkflow Where [TriggerID]=@KeyCol";
            DbParameter[] pramsGet = { MakeInParam("@KeyCol", (DbType)SqlDbType.Int, 4, Int32.Parse(KeyCol)) };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_NodeTriggerWorkflowFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_NodeTriggerWorkflowEntity GetWorkflow_NodeTriggerWorkflowFromIDataReader(DbDataReader dr)
        {
            Workflow_NodeTriggerWorkflowEntity dt = new Workflow_NodeTriggerWorkflowEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["TriggerID"].ToString() != "" || dr["TriggerID"] != null) dt.TriggerID = Int32.Parse(dr["TriggerID"].ToString());
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                if (dr["NodeID"].ToString() != "" || dr["NodeID"] != null) dt.NodeID = Int32.Parse(dr["NodeID"].ToString());
                if (dr["OPTime"].ToString() != "" || dr["OPTime"] != null) dt.OPTime = Int32.Parse(dr["OPTime"].ToString());
                dt.OPCondition = dr["OPCondition"].ToString();
                if (dr["TriggerWFID"].ToString() != "" || dr["TriggerWFID"] != null) dt.TriggerWFID = Int32.Parse(dr["TriggerWFID"].ToString());
                if (dr["TriggerWFCreator"].ToString() != "" || dr["TriggerWFCreator"] != null) dt.TriggerWFCreator = Int32.Parse(dr["TriggerWFCreator"].ToString());
                if (dr["WFCreateNode"].ToString() != "" || dr["WFCreateNode"] != null) dt.WFCreateNode = Int32.Parse(dr["WFCreateNode"].ToString());
                if (dr["WFCreateFieldID"].ToString() != "" || dr["WFCreateFieldID"] != null) dt.WFCreateFieldID = Int32.Parse(dr["WFCreateFieldID"].ToString());
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                if (dr["CreateSID"].ToString() != "" || dr["CreateSID"] != null) dt.CreateSID = Int32.Parse(dr["CreateSID"].ToString());
                if (dr["IsCancel"].ToString() != "" || dr["IsCancel"] != null) dt.IsCancel = Int32.Parse(dr["IsCancel"].ToString());
                if (dr["CancelDate"].ToString() != "" || dr["CancelDate"] != null) dt.CancelDate = Convert.ToDateTime(dr["CancelDate"]);
                if (dr["CancelSID"].ToString() != "" || dr["CancelSID"] != null) dt.CancelSID = Int32.Parse(dr["CancelSID"].ToString());

                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "触发流程的字段对应 old"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_TriggerWFFieldMappingEntity"></param>
        /// <param name="dtTriggerExpression"></param>
        /// <returns></returns>
        public string AddWorkflow_TriggerWFFieldMapping(Workflow_TriggerWFFieldMappingEntity _Workflow_TriggerWFFieldMappingEntity, DataTable dtTriggerExpression)
        {
            //判断该记录是否已经存在




            DbParameter[] prams = { MakeInParam("@TriggerID",(DbType)SqlDbType.Int ,4,_Workflow_TriggerWFFieldMappingEntity.TriggerID ),
									MakeInParam("@TargetFieldID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingEntity.TargetFieldID)
                                     };
            string sql = "select * from  Workflow_TriggerWFFieldMapping where TriggerID=@TriggerID and TargetFieldID=@TargetFieldID";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在




            }
            else
            {
                try
                {
                    DbParameter[] pramsInsert = {
									   MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingEntity.TriggerID ),
									   MakeInParam("@TargetFieldID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingEntity.TargetFieldID   ),
                                        MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingEntity.TargetGroupID   ),
                                         MakeInParam("@SourceGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingEntity.SourceGroupID   ),
									   MakeInParam("@SourceFieldName",(DbType)SqlDbType.VarChar,2000,_Workflow_TriggerWFFieldMappingEntity.SourceFieldName ),
                 	   MakeInParam("@OPCycleType",(DbType)SqlDbType.TinyInt ,1,_Workflow_TriggerWFFieldMappingEntity.OPCycleType ),
                       MakeInParam("@SourceFieldTypeID",(DbType)SqlDbType.TinyInt ,1,_Workflow_TriggerWFFieldMappingEntity.SourceFieldTypeID  )
                                             };
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO [dbo].[Workflow_TriggerWFFieldMapping]");
                    sb.Append("(");
                    sb.Append("[TriggerID]");
                    sb.Append(",[TargetFieldID]");
                    sb.Append(",[TargetGroupID]");
                    sb.Append(",[SourceGroupID]");
                    sb.Append(",[SourceFieldName]");
                    sb.Append(",[OPCycleType]");
                    sb.Append(",[SourceFieldTypeID]");
                    sb.Append(") ");
                    sb.Append(" VALUES (");
                    sb.Append("@TriggerID,");
                    sb.Append("@TargetFieldID,");
                    sb.Append("@TargetGroupID,");
                    sb.Append("@SourceGroupID,");
                    sb.Append("@SourceFieldName,");
                    sb.Append("@OPCycleType,");
                    sb.Append("@SourceFieldTypeID");
                    sb.Append("); ");
                    sb.Append("select @@identity;");
                    int mappingID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert));

                    for (int i = 0; i < dtTriggerExpression.Rows.Count; i++)
                    {
                        DbParameter[] pramsExprInsert = {
									   MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,mappingID  ),
                                //       MakeInParam("@SourceFieldID",(DbType)SqlDbType.Int,4,dtTriggerExpression.Rows[i]["SourceFieldID"]   ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtTriggerExpression.Rows[i]["ComputeType"] ),
									   MakeInParam("@ExpressionValue",(DbType)SqlDbType.VarChar,500,dtTriggerExpression.Rows[i]["ExpressionValue"] ),
									   MakeInParam("@ExpressionOrder",(DbType)SqlDbType.TinyInt ,1,dtTriggerExpression.Rows[i]["ExpressionOrder"] ),
                                                  };
                        string sqlInsert = @" insert into Workflow_NodeTriggerExpression(MappingID,ComputeType,ExpressionValue,ExpressionOrder) values(@MappingID,@ComputeType,@ExpressionValue,@ExpressionOrder) ";
                        ExecuteScalar(CommandType.Text, sqlInsert, pramsExprInsert);
                    }
                    return "1";
                }
                catch
                {
                    return "0";
                }
                finally
                {

                }

            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="MappingID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_TriggerWFFieldMapping(string MappingID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,Int32.Parse(MappingID ))
                                         };
            return ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_TriggerWFFieldMapping] where MappingID=@MappingID;delete from [dbo].[Workflow_NodeTriggerExpression] where  MappingID=@MappingID", pramsDelete).ToString();
        }


        #endregion

        #region "触发流程的字段对应 Main new "

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_TriggerWFFieldMappingMainEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_TriggerWFFieldMappingMain(Workflow_TriggerWFFieldMappingMainEntity _Workflow_TriggerWFFieldMappingMainEntity)
        {
            //判断该记录是否已经存在




            DbParameter[] prams = { 
                                   MakeInParam("@TriggerID",(DbType)SqlDbType.Int ,4,_Workflow_TriggerWFFieldMappingMainEntity.TriggerID ),
									MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingMainEntity.TargetGroupID)
                                     };
            string sql = "select * from  Workflow_TriggerWFFieldMappingMain where TriggerID=@TriggerID and TargetGroupID=@TargetGroupID";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在




            }
            else
            {
                try
                {
                    DbParameter[] pramsInsert = {
									   MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingMainEntity.TriggerID ),
                                        MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingMainEntity.TargetGroupID   ),
                 	   MakeInParam("@OPCycleType",(DbType)SqlDbType.TinyInt ,1,_Workflow_TriggerWFFieldMappingMainEntity.OPCycleType )
                                             };
                    StringBuilder sb = new StringBuilder();
                    sb.Append("INSERT INTO [dbo].[Workflow_TriggerWFFieldMappingMain]");
                    sb.Append("(");
                    sb.Append("[TriggerID]");
                    sb.Append(",[TargetGroupID]");
                    sb.Append(",[OPCycleType]");
                    sb.Append(") ");
                    sb.Append(" VALUES (");
                    sb.Append("@TriggerID,");
                    sb.Append("@TargetGroupID,");
                    sb.Append("@OPCycleType");
                    sb.Append("); ");
                    sb.Append("select @@identity;");
                    int mappingID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert));

                    return mappingID.ToString();
                }
                catch
                {
                    return "0";
                }
                finally
                {

                }

            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="MappingID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_TriggerWFFieldMappingMain(string MappingID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,Int32.Parse(MappingID ))
                                         };
            return ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_TriggerWFFieldMappingMain] where MappingID=@MappingID;delete from [dbo].[Workflow_TriggerWFFieldMappingDetail] where  MappingID=@MappingID;delete from [dbo].[Workflow_NodeTriggerExpression] where  MappingID in ( select SubMappingID from  Workflow_TriggerWFFieldMappingDetail where MappingID=@MappingID);", pramsDelete).ToString();
        }



        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public Workflow_TriggerWFFieldMappingMainEntity GetWorkflow_TriggerWFFieldMappingMainEntityByKeyCol(string KeyCol)
        {
            string sql = "select * from Workflow_TriggerWFFieldMappingMain  Where MappingID=@KeyCol";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Int32.Parse(KeyCol) )
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_TriggerWFFieldMappingMainFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_TriggerWFFieldMappingMainEntity GetWorkflow_TriggerWFFieldMappingMainFromIDataReader(DbDataReader dr)
        {
            Workflow_TriggerWFFieldMappingMainEntity dt = new Workflow_TriggerWFFieldMappingMainEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["MappingID"].ToString() != "" || dr["MappingID"] != null) dt.MappingID = Int32.Parse(dr["MappingID"].ToString());
                if (dr["TriggerID"].ToString() != "" || dr["TriggerID"] != null) dt.TriggerID = Int32.Parse(dr["TriggerID"].ToString());
                if (dr["TargetGroupID"].ToString() != "" || dr["TargetGroupID"] != null) dt.TargetGroupID = Int32.Parse(dr["TargetGroupID"].ToString());
                if (dr["OPCycleType"].ToString() != "" || dr["OPCycleType"] != null) dt.MappingID = Convert.ToByte(dr["OPCycleType"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "对应字段Mapping Detail "

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_TriggerWFFieldMappingDetailEntity"></param>
        /// <param name="dtTriggerExpression"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_TriggerWFFieldMappingDetail(Workflow_TriggerWFFieldMappingDetailEntity _Workflow_TriggerWFFieldMappingDetailEntity, DataTable dtTriggerExpression)
        {

            //判断该记录是否已经存在




            DbParameter[] prams = {   MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.MappingID ),
									   MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TriggerID ),
									   MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TargetGroupID ),
									   MakeInParam("@TargetFieldID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TargetFieldID )

                                     };
            string sql = "select SubMappingID  from  Workflow_TriggerWFFieldMappingDetail where MappingID=@MappingID and TriggerID=@TriggerID and TargetGroupID=@TargetGroupID and TargetFieldID=@TargetFieldID";
            DataTable dt = ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

            try
            {

                if (dt.Rows.Count > 0)
                {
                    // //该记录已经存在,删除
                    DeleteWorkflow_TriggerWFFieldMappingDetail(dt.Rows[0][0].ToString());

                }

                DbParameter[] pramsInsert = {
									   MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.MappingID ),
									   MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TriggerID ),
									   MakeInParam("@TargetGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TargetGroupID ),
									   MakeInParam("@TargetFieldID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.TargetFieldID ),
									   MakeInParam("@SourceGroupID",(DbType)SqlDbType.Int,4,_Workflow_TriggerWFFieldMappingDetailEntity.SourceGroupID ),
									   MakeInParam("@SourceFieldName",(DbType)SqlDbType.VarChar,2000,_Workflow_TriggerWFFieldMappingDetailEntity.SourceFieldName ),
									   MakeInParam("@SourceFieldTypeID",(DbType)SqlDbType.TinyInt,1,_Workflow_TriggerWFFieldMappingDetailEntity.SourceFieldTypeID )
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].Workflow_TriggerWFFieldMappingDetail");
                sb.Append("(");
                sb.Append("MappingID");
                sb.Append(",TriggerID");
                sb.Append(",TargetGroupID");
                sb.Append(",TargetFieldID");
                sb.Append(",SourceGroupID");
                sb.Append(",SourceFieldName");
                sb.Append(",SourceFieldTypeID");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@MappingID,");
                sb.Append("@TriggerID,");
                sb.Append("@TargetGroupID,");
                sb.Append("@TargetFieldID,");
                sb.Append("@SourceGroupID,");
                sb.Append("@SourceFieldName,");
                sb.Append("@SourceFieldTypeID);");
                sb.Append("select @@identity;");
                int SubMappingID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());

                for (int i = 0; i < dtTriggerExpression.Rows.Count; i++)
                {
                    DbParameter[] pramsExprInsert = {
									   MakeInParam("@MappingID",(DbType)SqlDbType.Int,4,SubMappingID  ),
                                //       MakeInParam("@SourceFieldID",(DbType)SqlDbType.Int,4,dtTriggerExpression.Rows[i]["SourceFieldID"]   ),
									   MakeInParam("@ComputeType",(DbType)SqlDbType.Int,4,dtTriggerExpression.Rows[i]["ComputeType"] ),
									   MakeInParam("@ExpressionValue",(DbType)SqlDbType.VarChar,500,dtTriggerExpression.Rows[i]["ExpressionValue"] ),
									   MakeInParam("@ExpressionOrder",(DbType)SqlDbType.TinyInt ,1,dtTriggerExpression.Rows[i]["ExpressionOrder"] ),
                                                  };
                    string sqlInsert = @" insert into Workflow_NodeTriggerExpression(MappingID,ComputeType,ExpressionValue,ExpressionOrder) values(@MappingID,@ComputeType,@ExpressionValue,@ExpressionOrder) ";
                    ExecuteScalar(CommandType.Text, sqlInsert, pramsExprInsert);
                }
                return "1";
            }
            catch
            {
                return "0";
            }
            finally
            {

            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="SubMappingID"></param>
        /// <returns></returns>
        public string DeleteWorkflow_TriggerWFFieldMappingDetail(string SubMappingID)
        {
            DbParameter[] pramsDelete = {
                                             MakeInParam("@SubMappingID",(DbType)SqlDbType.Int,4,Int32.Parse(SubMappingID ))
                                         };
            return ExecuteNonQuery(CommandType.Text, "delete from [dbo].[Workflow_TriggerWFFieldMappingDetail] where SubMappingID=@SubMappingID;delete from [dbo].[Workflow_NodeTriggerExpression] where  MappingID=@SubMappingID", pramsDelete).ToString();
        }

        #endregion

        #region "触发条件设置"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_NodeTriggerConditionEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_NodeTriggerCondition(Workflow_NodeTriggerConditionEntity _Workflow_NodeTriggerConditionEntity)
        {

            DbParameter[] pramsInsert = {
									   MakeInParam("@ConditionID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerConditionEntity.ConditionID ),
									   MakeInParam("@BatchSeq",(DbType)SqlDbType.TinyInt ,1,_Workflow_NodeTriggerConditionEntity.BatchSeq ),
									   MakeInParam("@BranchBatchSeq",(DbType)SqlDbType.TinyInt,1,_Workflow_NodeTriggerConditionEntity.BranchBatchSeq ),
									   MakeInParam("@TriggerID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerConditionEntity.TriggerID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerConditionEntity.FieldID ),
									   MakeInParam("@SymbolCode",(DbType)SqlDbType.VarChar,50,_Workflow_NodeTriggerConditionEntity.SymbolCode ),
									//    MakeInParam("@CompareFieldID",(DbType)SqlDbType.Int,4,_Workflow_NodeTriggerConditionEntity. ),
									   MakeInParam("@CompareToValue",(DbType)SqlDbType.VarChar,200,_Workflow_NodeTriggerConditionEntity.CompareToValue ),
									   MakeInParam("@AndOr",(DbType)SqlDbType.VarChar,20,_Workflow_NodeTriggerConditionEntity.AndOr )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_NodeTriggerCondition]");
            sb.Append("(");
            sb.Append("[BatchSeq]");
            sb.Append(",[BranchBatchSeq]");
            sb.Append(",[TriggerID]");
            sb.Append(",[FieldID]");
            sb.Append(",[SymbolCode]");
            //   sb.Append(",[CompareFieldID]");
            sb.Append(",[CompareToValue]");
            sb.Append(",[AndOr]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@BatchSeq,");
            sb.Append("@BranchBatchSeq,");
            sb.Append("@TriggerID,");
            sb.Append("@FieldID,");
            sb.Append("@SymbolCode,");
            // sb.Append("@CompareFieldID,");
            sb.Append("@CompareToValue,");
            sb.Append("@AndOr");
            sb.Append("); ");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ConditionID"></param>
        /// <returns>返回string "-1"表示未成功，否则成功 </returns>
        public string DeleteWorkflow_NodeTriggerCondition(int ConditionID)
        {
            DbParameter prams = MakeInParam("@ConditionID", (DbType)SqlDbType.Int, 4, ConditionID)
                                            ;
            string sql = @"
delete from Workflow_NodeTriggerCondition 
where BatchSeq in (select BatchSeq  from Workflow_NodeTriggerCondition where ConditionID=@ConditionID) 
    and TriggerID =(select TriggerID  from Workflow_NodeTriggerCondition  where ConditionID=@ConditionID)
";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public Workflow_NodeTriggerConditionEntity GetWorkflow_NodeTriggerConditionEntityByKeyCol(string KeyCol)
        {
            string sql = "select * from Workflow_NodeTriggerCondition Where ConditionID=@KeyCol";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int ,4,Int32.Parse(KeyCol )),
                                      };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_NodeTriggerConditionFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_NodeTriggerConditionEntity GetWorkflow_NodeTriggerConditionFromIDataReader(DbDataReader dr)
        {
            Workflow_NodeTriggerConditionEntity dt = new Workflow_NodeTriggerConditionEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["ConditionID"].ToString() != "" || dr["ConditionID"] != null) dt.ConditionID = Int32.Parse(dr["ConditionID"].ToString());
                dt.BatchSeq = Convert.ToByte(dr["BatchSeq"]);
                dt.BranchBatchSeq = Convert.ToByte(dr["BranchBatchSeq"]);
                if (dr["TriggerID"].ToString() != "" || dr["TriggerID"] != null) dt.TriggerID = Int32.Parse(dr["TriggerID"].ToString());
                if (dr["FieldID"].ToString() != "" || dr["FieldID"] != null) dt.FieldID = Int32.Parse(dr["FieldID"].ToString());
                dt.SymbolCode = dr["SymbolCode"].ToString();
                dt.CompareToValue = dr["CompareToValue"].ToString();
                dt.AndOr = dr["AndOr"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "报表明细操作"

        /// <summary>
        /// 新增信息，插入前先删除
        /// </summary>
        /// <param name="_Workflow_ReportDetailEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_ReportDetail(Workflow_ReportDetailEntity _Workflow_ReportDetailEntity)
        {

            DbParameter[] pramsInsert = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,_Workflow_ReportDetailEntity.ReportID ),
									   MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,_Workflow_ReportDetailEntity.FieldID ),
									   MakeInParam("@IsStatistics",(DbType)SqlDbType.TinyInt ,1,_Workflow_ReportDetailEntity.IsStatistics ),
									   MakeInParam("@IsOrder",(DbType)SqlDbType.TinyInt,1,_Workflow_ReportDetailEntity.IsOrder ),
									   MakeInParam("@OrderPattern",(DbType)SqlDbType.TinyInt,1,_Workflow_ReportDetailEntity.OrderPattern ),
									   MakeInParam("@OrderIndex",(DbType)SqlDbType.Int,4,_Workflow_ReportDetailEntity.OrderIndex ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_ReportDetailEntity.DisplayOrder )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_ReportDetail]");
            sb.Append("(");
            sb.Append("[ReportID]");
            sb.Append(",[FieldID]");
            sb.Append(",[IsStatistics]");
            sb.Append(",[IsOrder]");
            sb.Append(",[OrderPattern]");
            sb.Append(",[OrderIndex]");
            sb.Append(",[DisplayOrder]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@ReportID,");
            sb.Append("@FieldID,");
            sb.Append("@IsStatistics,");
            sb.Append("@IsOrder,");
            sb.Append("@OrderPattern,");
            sb.Append("@OrderIndex,");
            sb.Append("@DisplayOrder);");
            sb.Append(" select @@rowcount;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns> </returns>
        public string DeleteWorkflow_ReportDetail(int ReportID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,ReportID )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from [dbo].[Workflow_ReportDetail]");
            sb.Append("  where ");
            sb.Append(" [ReportID]=@ReportID");

            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsDelete).ToString();
        }

        /// <summary>
        /// 删除显示字段信息
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="FieldID"></param>
        /// <returns> </returns>
        public string DeleteWorkflow_ReportDetailByReportIDandFieldID(int ReportID, int FieldID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,ReportID ),
                                       MakeInParam("@FieldID",(DbType)SqlDbType.Int,4,FieldID )
                                          };
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from [dbo].[Workflow_ReportDetail]");
            sb.Append(" where ");
            sb.Append(" [ReportID]=@ReportID  and @FieldID=FieldID");

            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsDelete).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_ReportDetailDataTableByKeyCol(string KeyCol)
        {
            string sql = "select * from Workflow_ReportDetail Where ReportID=@KeyCol";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int ,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }

        /// <summary>
        /// 返回ReportDetail显示画面内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_ReportDetailDisplayTableByReportID(string KeyCol)
        {
            string sql = @"SELECT d.[ReportID]
                          ,d.[FieldID]
                          ,fd.FieldName
                          ,fd.DataTypeID
                          ,case d.IsStatistics when 1 then '√' else '' end as IsStatisticsN
                          ,d.[IsStatistics]
                          ,d.[IsOrder]
                          ,d.[OrderPattern]
                          ,d.[OrderIndex]
                          ,case isnull(d.OrderIndex,0) when 0 then null else d.OrderIndex end as OrderIndexN
                          ,d.[DisplayOrder]
                          ,f.FieldLabel+case f.IsDetail when 1 then '('+g.GroupName+')' else ' ' end+' ('+isnull(fd.FieldName,'')+')' as FieldLabel
	                      ,case d.IsOrder when 1 then '√' else '' end as IsOrderN
	                      ,case d.OrderPattern  when 2 then '降' when '1' then '升' else '' end as OrderPatternN
                            FROM [Workflow_ReportMain] m
                            inner join [Workflow_ReportDetail] d on d.[ReportID]=m.[ReportID] 
                            left join Workflow_FormField f  on m.FormID=f.FormID and d.FieldID=f.FieldID 
                            LEFT join Workflow_FormFieldGroup g on  f.FormID=g.FormID and f.GroupID=g.GroupID
                            left join Workflow_FieldDict fd on f.FieldID=fd.FieldID 
                            WHERE m.ReportID=@KeyCol
                            ORDER BY f.IsDetail,f.DisplayOrder";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }

        /// <summary>
        /// 返回ReportDetail编辑画面内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_ReportDetailEditTableByReportID(string KeyCol)
        {
            string sql = @"select * from 
                            (
                            SELECT m.[ReportID]
                                  ,f.[FieldID]
	                              ,case f.IsDetail when 0 then 0 else g.GroupID end as GroupID
                                  ,d.[IsStatistics]
                                  ,d.[IsOrder]
                                  ,d.[OrderPattern]
                                  ,d.[OrderIndex]
                                  ,d.[DisplayOrder]
                                  ,fd.DataTypeID
	                              ,f.IsDetail
                                  ,case isnull(d.FieldID,0) when 0 then 0 else 1 end  as IsShow
	                              ,f.DisplayOrder DisplayOrder2
                                  ,f.FieldLabel+case f.IsDetail when 1 then '('+g.GroupName+')' else ' ' end+' ('+isnull(fd.FieldName,'')+')' as FieldLabel
                            FROM Workflow_FormField f 
                            LEFT join Workflow_FormFieldGroup g on  f.FormID=g.FormID and f.GroupID=g.GroupID
                            left join Workflow_FieldDict fd on f.FieldID=fd.FieldID
                            left join [Workflow_ReportMain] m on m.FormID=f.FormID 
                            left join [Workflow_ReportDetail] d on d.[ReportID]=m.[ReportID] and d.FieldID=f.FieldID 
                           WHERE m.ReportID=@KeyCol 
                            ) t where GroupID is not null
                            ORDER BY IsDetail,DisplayOrder2
                         ";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }



        #endregion

        #region "报表 主表操作"
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_ReportMainEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_ReportMain(Workflow_ReportMainEntity _Workflow_ReportMainEntity)
        {
            //判断该记录是否已经存在



            DbParameter[] prams = {   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportTypeID ),
									   MakeInParam("@ReportName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportMainEntity.ReportName )
                                  };
            string sql = " select * from  Workflow_ReportMain where ReportTypeID=@ReportTypeID and ReportName=@ReportName";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在



            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportTypeID ),
									   MakeInParam("@ReportName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportMainEntity.ReportName ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.FormID ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.VarChar,2000,_Workflow_ReportMainEntity.WorkflowID )
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_ReportMain]");
                sb.Append("(");
                sb.Append("[ReportTypeID]");
                sb.Append(",[ReportName]");
                sb.Append(",[FormID]");
                sb.Append(",[WorkflowID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@ReportTypeID,");
                sb.Append("@ReportName,");
                sb.Append("@FormID,");
                sb.Append("@WorkflowID);");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_ReportMainEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_ReportMain(Workflow_ReportMainEntity _Workflow_ReportMainEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportID ),
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportTypeID ),
									   MakeInParam("@ReportName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportMainEntity.ReportName ),
									   MakeInParam("@FormID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.FormID ),
									   MakeInParam("@WorkflowID",(DbType)SqlDbType.VarChar,2000,_Workflow_ReportMainEntity.WorkflowID )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_ReportMain]");
            sb.Append(" set ");
            sb.Append(" [ReportTypeID]=@ReportTypeID,");
            sb.Append(" [ReportName]=@ReportName,");
            sb.Append(" [FormID]=@FormID,");
            sb.Append(" [WorkflowID]=@WorkflowID");
            sb.Append(" where [ReportID]=@ReportID");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 修改报表类型信息
        /// </summary>
        /// <param name="_Workflow_ReportMainEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_ReportMainReportType(Workflow_ReportMainEntity _Workflow_ReportMainEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportID ),
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportMainEntity.ReportTypeID )
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_ReportMain]");
            sb.Append(" set ");
            sb.Append(" [ReportTypeID]=@ReportTypeID");
            sb.Append(" where [ReportID]=@ReportID");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }



        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public int DeleteWorkflow_ReportMain(string ReportID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int, 4,Convert.ToInt32 (ReportID) )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from [dbo].[Workflow_ReportMain] where ReportID=@ReportID;");
            //  sb.Append("delete from [dbo].[Workflow_ReportDetail] where ReportID=@ReportID");

            int Ret = ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsDelete);

            if (Ret > 0)
            {
                DeleteWorkflow_ReportDetail(Convert.ToInt32(ReportID));
            }

            return Ret;

        }


        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="ReportID"></param>
        /// <returns></returns>
        public Workflow_ReportMainEntity GetWorkflow_ReportMainEntityByReportID(string ReportID)
        {
            string sql = "select * from Workflow_ReportMain Where ReportID=@ReportID";
            DbParameter[] pramsGet = {
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,Convert.ToInt32 (ReportID) ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_ReportMainFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }


        private Workflow_ReportMainEntity GetWorkflow_ReportMainFromIDataReader(DbDataReader dr)
        {
            Workflow_ReportMainEntity dt = new Workflow_ReportMainEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["ReportID"].ToString() != "" || dr["ReportID"] != null) dt.ReportID = Int32.Parse(dr["ReportID"].ToString());
                if (dr["ReportTypeID"].ToString() != "" || dr["ReportTypeID"] != null) dt.ReportTypeID = Int32.Parse(dr["ReportTypeID"].ToString());
                dt.ReportName = dr["ReportName"].ToString();
                if (dr["FormID"].ToString() != "" || dr["FormID"] != null) dt.FormID = Int32.Parse(dr["FormID"].ToString());
                dt.WorkflowID = dr["WorkflowID"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }



        /// <summary>
        /// 返回ReportMain显示画面内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_ReportMainDisplayTableByReportID(string KeyCol)
        {
            string sql = @"SELECT m.[ReportID]
                              ,m.[ReportTypeID]
                              ,m.[ReportName]
                              ,m.[FormID]
                              ,WorkflowID2.RID as  WorkflowID
                        --	  ,t.ReportTypeName
                              ,f.FormName
	                            ,b.WorkflowName
                        FROM [Workflow_ReportMain] m outer apply  dbo.Fun_GetIDTableByString(m.[WorkflowID]) as WorkflowID2
                        --left join Workflow_ReportType t on m.ReportTypeID=t.ReportTypeID
                        left join Workflow_FormBase f on m.FormID=f.FormID 
                        left join Workflow_Base b on WorkflowID2.RID=b.WorkflowID
                        where m.ReportID=@KeyCol 
                         ";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }
        #endregion

        #region "报表种类"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_ReportTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_ReportType(Workflow_ReportTypeEntity _Workflow_ReportTypeEntity)
        {
            //判断该记录是否已经存在



            DbParameter[] prams = { MakeInParam("@ReportTypeName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportTypeEntity.ReportTypeName),
                                     MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_ReportTypeEntity.DisplayOrder )
                                     };
            string sql = " select * from  Workflow_ReportType where ReportTypeName=@ReportTypeName or DisplayOrder=@DisplayOrder";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在



            }
            else
            {
                DbParameter[] pramsInsert = {
									 //  MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportTypeEntity.ReportTypeID ),
									   MakeInParam("@ReportTypeName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportTypeEntity.ReportTypeName ),
									   MakeInParam("@ReportTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_ReportTypeEntity.ReportTypeDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_ReportTypeEntity.DisplayOrder )
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[Workflow_ReportType]");
                sb.Append("(");
                sb.Append("[ReportTypeName]");
                sb.Append(",[ReportTypeDesc]");
                sb.Append(",[DisplayOrder]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@ReportTypeName,");
                sb.Append("@ReportTypeDesc,");
                sb.Append("@DisplayOrder");
                sb.Append(" );");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_ReportTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_ReportType(Workflow_ReportTypeEntity _Workflow_ReportTypeEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,_Workflow_ReportTypeEntity.ReportTypeID ),
									   MakeInParam("@ReportTypeName",(DbType)SqlDbType.VarChar,200,_Workflow_ReportTypeEntity.ReportTypeName ),
									   MakeInParam("@ReportTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_ReportTypeEntity.ReportTypeDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_Workflow_ReportTypeEntity.DisplayOrder )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_ReportType]");
            sb.Append(" set ");
            sb.Append(" [ReportTypeName]=@ReportTypeName,");
            sb.Append(" [ReportTypeDesc]=@ReportTypeDesc,");
            sb.Append(" [DisplayOrder]=@DisplayOrder");
            sb.Append(" where ");
            sb.Append(" [ReportTypeID]=@ReportTypeID");

            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="ReportTypeID"></param>
        /// <returns></returns>
        public Workflow_ReportTypeEntity GetWorkflow_ReportTypeEntityByReportTypeID(string ReportTypeID)
        {
            string sql = "select * from Workflow_ReportType Where ReportTypeID=@ReportTypeID";
            DbParameter[] pramsGet = {
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int,4,ReportTypeID )
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_ReportTypeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_ReportTypeEntity GetWorkflow_ReportTypeFromIDataReader(DbDataReader dr)
        {
            Workflow_ReportTypeEntity dt = new Workflow_ReportTypeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["ReportTypeID"].ToString() != "" || dr["ReportTypeID"] != null) dt.ReportTypeID = Int32.Parse(dr["ReportTypeID"].ToString());
                dt.ReportTypeName = dr["ReportTypeName"].ToString();
                dt.ReportTypeDesc = dr["ReportTypeDesc"].ToString();
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }


        /// <summary>
        /// 删除报表种类信息
        /// </summary>
        /// <param name="ReportTypeID"></param>
        /// <returns>返回string "-1"表示该其下有报表，不能删除，不存在，则可成功 </returns>
        public int DeleteWorkflow_ReportType(string ReportTypeID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@ReportTypeID",(DbType)SqlDbType.Int, 4,Convert.ToInt32 (ReportTypeID) )
                                         };
            StringBuilder sb = new StringBuilder();
            sb.Append("select count(*)  from [dbo].[Workflow_ReportMain] where ReportTypeID=@ReportTypeID");


            int Ret = Int32.Parse(ExecuteScalar(CommandType.Text, sb.ToString(), pramsDelete).ToString());

            if (Ret > 0)
            {
                return -1;
            }
            else
            {
                sb.Remove(0, sb.Length);
                sb.Append(" delete from Workflow_ReportType where  ReportTypeID=@ReportTypeID");
                Ret = ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsDelete);
                return Ret;
            }

        }


        /// <summary>
        /// 返回所有报表类型内容
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkflow_ReportTypeAll()
        {
            return ExecuteDataset(CommandType.Text, "select * from Workflow_ReportType").Tables[0];
        }

        #endregion

        #region "自定义报表部分"
        /// <summary>
        /// 返回自定义报表画面内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_UserDefinedReportTableByReportID(string KeyCol)
        {
            string sql = @"select * from 
	                        (
		                        SELECT m.[ReportID]
		                          ,f.[FormID]
		                          ,case f.IsDetail when 0 then 0 else g.GroupID end as GroupID
		                          ,case isnull(d.FieldID,0) when 0 then 0 else 1 end  as IsShow
		                          ,f.DisplayOrder DisplayOrder2
		                          ,f.FieldLabel+case f.IsDetail when 1 then '('+g.GroupName+')' else ' ' end+' ('+isnull(fd.FieldName,'')+')' as FieldLabel
		                          ,fd.FieldID
                                  ,fd.FieldName
                                  ,fd.FieldDesc
                                  ,case isnull(fd.[DataTypeID],0) when 0 then 1 else fd.[DataTypeID] end DataTypeID
                                  ,fd.FieldDBType
                                  ,fd.HTMLTypeID
                                  ,fd.FieldTypeID
                                  ,fd.ValidateType
                                  ,fd.TextLength
                                  ,fd.[Dateformat]
                                  ,fd.[TextHeight]
                                  ,fd.[IsHTML]
                                  ,fd.[BrowseType]
                                  ,fd.[IsDynamic]
                                  ,fd.[DataSetID]
                                  ,fd.[ValueColumn]
                                  ,fd.[TextColumn]
                                  ,fd.[DefaultValue]
                                  ,bt.BrowsePage,bt.BrowseTypeName,bt.BrowseValueSql,bt.BrowseSqlParam
		                        FROM Workflow_FormField f 
		                        LEFT join Workflow_FormFieldGroup g on  f.FormID=g.FormID and f.GroupID=g.GroupID
		                        left join Workflow_FieldDict fd on f.FieldID=fd.FieldID
		                        left join [Workflow_ReportMain] m on m.FormID=f.FormID 
		                        left join [Workflow_ReportDetail] d on d.[ReportID]=m.[ReportID] and d.FieldID=f.FieldID 
                                left join  Workflow_BrowseType bt on fd.BrowseType=bt.BrowseTypeID

		                         WHERE m.ReportID=@KeyCol 
	                        ) t where GroupID is not null
	                        ORDER BY GroupID,DisplayOrder2
                         ";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }


        /// <summary>
        /// 返回自定义报表用于显示报表内容时所需动态数据
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_UserDefinedReportDynamicDataTableByReportID(string KeyCol)
        {
            string sql = @" select * from 
	                        (
		                        SELECT m.[ReportID]   
                                  ,d.[IsStatistics]
                                  ,d.[IsOrder]
                                  ,d.[OrderPattern]
                                  ,d.[OrderIndex]
                                  ,d.[DisplayOrder]
		                          ,f.[FormID]  
                                  ,f.FieldLabel as FieldLabelNoName
		                          ,case f.IsDetail when 0 then 0 else g.GroupID end as GroupID
		                          ,f.DisplayOrder DisplayOrder2
		                          ,f.FieldID
                                  ,fd.FieldName
                                  ,case isnull(fd.[DataTypeID],0) when 0 then 1 else fd.[DataTypeID] end DataTypeID
                                  ,fd.FieldDBType
                                  ,fd.HTMLTypeID
                                  ,fd.FieldTypeID
                                  ,fd.ValidateType
                                  ,fd.TextLength
                                  ,fd.[Dateformat]
                                  ,fd.[TextHeight]
                                  ,fd.[IsHTML]
                                  ,fd.[BrowseType]
                                  ,fd.[IsDynamic]
                                  ,fd.[DataSetID]
                                  ,fd.[ValueColumn]
                                  ,fd.[TextColumn]
                                  ,fd.[DefaultValue]
                                  ,bt.BrowsePage,bt.BrowseTypeName,bt.BrowseValueSql,bt.BrowseSqlParam
		                        FROM [Workflow_ReportMain] m 
                                inner join [Workflow_ReportDetail] d on d.[ReportID]=m.[ReportID]
                                left join Workflow_FormField f   on m.FormID=f.FormID and d.FieldID=f.FieldID 
		                        left join Workflow_FormFieldGroup g on  f.FormID=g.FormID and f.GroupID=g.GroupID
		                        left join Workflow_FieldDict fd on f.FieldID=fd.FieldID
                                left join  Workflow_BrowseType bt on fd.BrowseType=bt.BrowseTypeID
		                         WHERE m.ReportID=@KeyCol 
	                        ) t where GroupID is not null
	                        ORDER BY GroupID,DisplayOrder2
";
            DbParameter[] pramsGet = {
									   MakeInParam("@KeyCol",(DbType)SqlDbType.Int,4,Convert.ToInt32(KeyCol ))
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.Text, sql, pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }


        /// <summary>
        /// 返回自定义报表画面比较符下拉框
        /// </summary>
        /// <param name="DataTypeID"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_UserDefinedReportConditionDropDownListSourceByDataTypeID(int DataTypeID)
        {
            string strSql = string.Format("select CompareSymbol,SymbolName from Workflow_CompareSymbol where SymbolType{0}Flag=1 order by DisplayOrder ", DataTypeID);
            return ExecuteDataset(CommandType.Text, strSql).Tables[0];

        }

        /// <summary>
        /// 返回自定义报表查询结果框
        /// </summary>
        /// <param name="ReportID"></param>
        /// <param name="strCondition"></param>
        /// <param name="strColumns"></param>
        /// <param name="strOrderBy"></param>
        /// <returns></returns>
        public DataTable GetWorkflow_UserDefinedReportSearchResult(int ReportID, string strCondition, string strColumns, string strOrderBy)
        {

            DbParameter[] pramsGet = {
                                       MakeInParam("@Condition",(DbType)SqlDbType.VarChar,3000,strCondition ),
									   MakeInParam("@Columns",(DbType)SqlDbType.VarChar,2000,strColumns ),
									   MakeInParam("@ReportID",(DbType)SqlDbType.Int,4,ReportID ),
                                       MakeInParam("@OrderBy",(DbType)SqlDbType.VarChar,2000,strOrderBy )
                                             };
            DataTable dt = new DataTable();

            try
            {
                dt = ExecuteDataset(CommandType.StoredProcedure, "Workflow_UserDefinedReportSearch", pramsGet).Tables[0];
            }
            catch
            {
                dt = null;
            }

            finally
            {

            }
            return dt;
        }
        #endregion

        #region "表单生成"

        /// <summary>
        /// 获取多选框，下拉框或CheckBoxList等的数据选择项
        /// </summary>
        /// <param name="FieldID"></param>
        /// <param name="IsDynamic"></param>
        /// <param name="DataSetID"></param>
        /// <param name="ValueColumn"></param>
        /// <param name="TextColumn"></param>
        /// <returns></returns>
        public DataTable GetMultiSelectDataSource(int FieldID, int IsDynamic, int DataSetID, string ValueColumn, string TextColumn)
        {

            if (IsDynamic == 0)  //静态的数据源
            {
                DbParameter[] prams = {
                                          MakeInParam("@FieldID", (DbType)SqlDbType.Int   , 4, FieldID),
                                      };
                string sql = "sp_GetFieldStaticMultiSelectDataSource";
                return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
            }
            else
            {
                //根据数据集来抓数据


                Workflow_DataSetEntity DataSetEntity = GetWorkflow_DataSetEntityByKeyCol(DataSetID.ToString());
                int DataSoureID = DataSetEntity.DataSourceID;
                int DataSetType = DataSetEntity.DataSetType;
                DataTable dtParameter = GetWorkflow_DataSetParameterEntityByKeyCol(DataSetID.ToString());
                string QueryString = DataSetEntity.QuerySql;
                //再获得DataSource信息
                Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSoureID);
                DbParameter[] DbParms = new DbParameter[dtParameter.Rows.Count];
                for (int j = 0; j < dtParameter.Rows.Count; j++)
                {
                    DbParms[j] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(dtParameter.Rows[j]["ParameterName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtParameter.Rows[j]["ParameterType"].ToString(), true), GetSqlDbTypeLength(dtParameter.Rows[j]["ParameterType"].ToString()), dtParameter.Rows[j]["ParameterValue"].ToString());
                }
                if (DataSetType == 1)
                {
                    return GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.Text, QueryString, DbParms).Tables[0];
                }
                else
                {
                    return GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.StoredProcedure, QueryString, DbParms).Tables[0];
                }
            }
        }

        /// <summary>
        /// 获取工作流创建者的节点的NodeID
        /// </summary>
        /// <param name="WorkflowID">工作流编号</param>
        /// <returns>NodeID</returns>
        public int GetCreateNodeIDbyWorkflowID(int WorkflowID)
        {
            //int NodeID = 0;
            DbParameter[] prams = {
                                      MakeInParam("@WorkflowID", (DbType)SqlDbType.Int   , 4, WorkflowID),
                                      MakeOutParam("@NodeID", (DbType)SqlDbType.Int, 4),
                                   };
            string sql = "sp_GetWorkflowCreateNodeID";

            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            return Convert.ToInt32(prams[1].Value);
        }

        /// <summary>
        /// 获取工作流对应的表单的FormID
        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <returns></returns>
        public int GetWorkflowFromIDbyWorkflowID(int WorkflowID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@WorkflowID", (DbType)SqlDbType.Int   , 4, WorkflowID),
                                      MakeOutParam("@FormID", (DbType)SqlDbType.Int, 4),
                                   };
            string sql = "sp_GetWorkflowFormID";

            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            return Convert.ToInt32(prams[1].Value);
        }

        /// <summary>
        /// 抓取节点下的主字段的详细情况
        /// </summary>
        /// <param name="FormID">表单编号</param>
        /// <param name="NodeID">节点编号</param>
        /// <returns></returns>
        public DataTable sp_GetMainFieldInforByNodeIDAndFormID(int FormID, int NodeID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@FormID", (DbType)SqlDbType.Int   , 4, FormID),
                                      MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID), 
                                   };
            string sql = "sp_GetMainFieldInforByNodeIDAndFormID";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 抓取流程节点下的明细字段的详细情况
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public DataTable GetFormDetailFied(int FormID, int NodeID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@FormID", (DbType)SqlDbType.Int   , 4, FormID),
                                      MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                  };
            string sql = "sp_GetDetailFieldInforByNodeIDAndFormID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 抓取流程节点下的明细字段组的情况
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="NodeID"></param>
        /// <returns></returns>
        public DataTable GetDetailFieldGroup(int FormID, int NodeID)
        {
            DbParameter[] prams = {   MakeInParam("@FormID", (DbType)SqlDbType.Int   , 4, FormID),
                                       MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                       
                                       
                                   };
            string sql = "sp_GetDetailFieldGroupbyNodeIDAndFormID";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 抓取流程节点下明细字段组的字段的情况
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataTable GetDetailFieldInfor(int NodeID, int GroupID)
        {
            DbParameter[] prams = {   MakeInParam("@NodeID", (DbType)SqlDbType.Int   , 4, NodeID),
                                       MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                       
                                       
                                   };
            string sql = "sp_GetDetailFieldInforByNodeIDAndGroupID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 抓取流程节点下明细字段组的数据
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="RequestID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataTable GetDetailFieldValue(int FormID, int RequestID, int GroupID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@FormID", (DbType)SqlDbType.Int   , 4, FormID),
                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4,RequestID),
                                      MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),  
                                   };
            string sql = "sp_GetDetailFieldValueByRequestID";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 新增流程时，保存表单数据
        /// </summary>
        /// <param name="dtMainEdit"></param>
        /// <param name="detailGroups"></param>
        /// <param name="GroupIDs"></param>
        /// <param name="dtFieldGroups"></param>
        /// <param name="WorkflowID"></param>
        /// <param name="NodeID"></param>
        /// <param name="NodeType"></param>
        /// <param name="UserSerialID"></param>
        /// <param name="IPAddress"></param>
        /// <param name="FormID"></param>
        /// <param name="dtFilesUpLoad"></param>
        /// <param name="FilesUpLoadFieldID"></param>
        /// <param name="RequestName"></param>
        /// <param name="IsAgent"></param>
        /// <param name="BeAgentID"></param>
        /// <returns></returns>
        public int saveFormData(DataTable dtMainEdit, ArrayList detailGroups, Int32[] GroupIDs, ArrayList dtFieldGroups, int WorkflowID, int NodeID, int NodeType, int UserSerialID, string IPAddress, int FormID, ArrayList dtFilesUpLoad, Int32[] FilesUpLoadFieldID, string RequestName, int IsAgent, int BeAgentID)
        {
            try
            {
                string sqlTableColumn = "insert into workflow_form(";
                string sqlTableColumnValue = "(";
                //先保存主字段并取得RequestID
                DbParameter[] prams = new DbParameter[dtMainEdit.Rows.Count + 3];
                for (int i = 0; i < dtMainEdit.Rows.Count; i++)
                {
                    //检查日期，符点，整型数据为空（没有输入数据）时要付null值，不然会引起保存到数据库时，数据类型不对的错误 Add by Jerry Wang 2011-10-27
                    //if (dtMainEdit.Rows[i]["FieldValue"].ToString()==""&& dtMainEdit.Rows[i]["SqlDbType"].ToString().ToUpper()=="DATETIME")
                    //    prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), System.Data.SqlTypes.SqlDateTime.Null );
                    //else if (dtMainEdit.Rows[i]["FieldValue"].ToString()==""&& dtMainEdit.Rows[i]["SqlDbType"].ToString().ToUpper()=="FLOAT")
                    //    prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]),  System.Data.SqlTypes.SqlDecimal.Null );
                    //else if (dtMainEdit.Rows[i]["FieldValue"].ToString()==""&& dtMainEdit.Rows[i]["SqlDbType"].ToString().ToUpper()=="BIGINT")
                    //    prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), System.Data.SqlTypes.SqlInt32.Null );
                    //else if (dtMainEdit.Rows[i]["FieldValue"].ToString() == "" && dtMainEdit.Rows[i]["SqlDbType"].ToString().ToUpper() == "INT")
                    //    prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), System.Data.SqlTypes.SqlInt16.Null );
                    if (dtMainEdit.Rows[i]["FieldValue"].ToString() == "") //直接用DBNULL来表示各种数据类型的 null值  Modified by Jerry Wang 2011-10-27
                        prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]),DBNull.Value );
                    else
                        prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), dtMainEdit.Rows[i]["FieldValue"].ToString());
                    sqlTableColumn = sqlTableColumn + dtMainEdit.Rows[i]["FieldName"].ToString() + ",";
                    sqlTableColumnValue = sqlTableColumnValue + "@" + dtMainEdit.Rows[i]["FieldName"].ToString() + ",";
                }

                sqlTableColumn = sqlTableColumn + "WorkflowID,FormID,Creator,CreateDate) values ";
                sqlTableColumnValue = sqlTableColumnValue + "@WorkflowID,@FormID,@Creator,getdate())";
                prams[dtMainEdit.Rows.Count] = MakeInParam("@WorkflowID", (DbType)SqlDbType.Int, 4, WorkflowID);
                prams[dtMainEdit.Rows.Count + 1] = MakeInParam("@FormID", (DbType)SqlDbType.Int, 4, FormID);
                prams[dtMainEdit.Rows.Count + 2] = MakeInParam("@Creator", (DbType)SqlDbType.Int, 4, UserSerialID);

                string sqlInsert = sqlTableColumn + sqlTableColumnValue + "; select @@identity; ";
                int RequestID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sqlInsert, prams));
                //保存明细数据
                if (RequestID > 0)
                {
                    for (int j = 0; j < detailGroups.Count; j++)
                    {
                        int GroupID = GroupIDs[j];
                        DataTable dtValue = (DataTable)detailGroups[j];
                        DataTable dtField = (DataTable)dtFieldGroups[j];
                        for (int ii = 0; ii < dtValue.Rows.Count; ii++)
                        {
                            string sqlColumn = "insert into Workflow_FormDetail(RequestID,GroupID,";
                            string sqlColumnValue = "(" + RequestID.ToString() + "," + GroupID.ToString() + ",";
                            DbParameter[] SqlGroupParms = new DbParameter[dtValue.Columns.Count];
                            for (int jj = 0; jj < dtField.Rows.Count; jj++)
                            {
                                if ((dtField.Rows[jj]["IsEdit"].ToString() == "1" && dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString() == "") || (dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString() != "")) //可编辑并且编辑成空时候 或者有值的时候要保存
                                {
                                    SqlGroupParms[jj] = MakeInParam("@" + dtField.Rows[jj]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtField.Rows[jj]["SqlDbType"].ToString(), true), Convert.ToInt32(dtField.Rows[jj]["SqlDbLength"]), dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString());
                                    sqlColumn = sqlColumn + dtField.Rows[jj]["FieldName"].ToString() + ",";
                                    sqlColumnValue = sqlColumnValue + "@" + dtField.Rows[jj]["FieldName"].ToString() + ",";
                                }
                            }

                            sqlColumn = sqlColumn.Substring(0, sqlColumn.Length - 1) + ") values ";
                            sqlColumnValue = sqlColumnValue.Substring(0, sqlColumnValue.Length - 1) + ")";
                            string sqlInsertGroup = sqlColumn + sqlColumnValue + "; select @@identity; ";
                            ExecuteNonQuery(CommandType.Text, sqlInsertGroup, SqlGroupParms);
                        }
                    }

                    //保存上传的附件


                    for (int j = 0; j < dtFilesUpLoad.Count; j++)
                    {
                        int FieldID = FilesUpLoadFieldID[j];

                        DataTable dtValue = (DataTable)dtFilesUpLoad[j];

                        for (int jj = 0; jj < dtValue.Rows.Count; jj++)
                        {
                            DbParameter[] pramsFileUpload = {
                                                                MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                MakeInParam("@FieldID", (DbType)SqlDbType.Int, 4,FieldID),
                                                                MakeInParam("@FileServerName", (DbType)SqlDbType.VarChar ,2000,dtValue.Rows[jj]["FileServerName"].ToString()),
                                                                MakeInParam("@FileType", (DbType)SqlDbType.VarChar ,50,dtValue.Rows[jj]["FileType"].ToString()),
                                                                MakeInParam("@FileClientName", (DbType)SqlDbType.VarChar ,2000,dtValue.Rows[jj]["FileClientName"].ToString()),
                                                                MakeInParam("@FileSize", (DbType)SqlDbType.Float, 8,dtValue.Rows[jj]["FileSize"].ToString()),
                                                                MakeInParam("@Uploader", (DbType)SqlDbType.Int, 4,dtValue.Rows[jj]["Uploader"].ToString() ),
                                                                MakeInParam("@UploadDate", (DbType)SqlDbType.DateTime ,16,dtValue.Rows[jj]["UploadDate"].ToString() ),
                                                            };

                            string sqlFileGroup = "sp_SaveRequestFieldUploadFile";
                            ExecuteNonQuery(CommandType.StoredProcedure, sqlFileGroup, pramsFileUpload);
                        }
                    }

                    //保存workflow_RequestBase
                    DbParameter[] pramsRequestBase = {
                                                         MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                         MakeInParam("@WorkflowID", (DbType)SqlDbType.Int, 4,WorkflowID),
                                                         MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                                         MakeInParam("@NodeType", (DbType)SqlDbType.Int, 4,NodeType ),
                                                         MakeInParam("@OperatorID", (DbType)SqlDbType.Int, 4,UserSerialID ),
                                                         MakeInParam("@Creator", (DbType)SqlDbType.Int, 4,IsAgent==1?BeAgentID:UserSerialID ),
                                                         MakeInParam("@RequestName", (DbType)SqlDbType.VarChar, 2000,RequestName),
                                                     };
                    string sqlRequestBase = "sp_InsertIntoWorkflow_RequestBase";
                    ExecuteNonQuery(CommandType.StoredProcedure, sqlRequestBase, pramsRequestBase);

                    //如果是代理则要把代理的情况写入Workflow_RequestAgentDetail
                    if (IsAgent == 1)
                    {
                        DbParameter[] pramsRequestAgent = {
                                                              MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                              MakeInParam("@WorkflowID", (DbType)SqlDbType.Int, 4,WorkflowID),
                                                              MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                                              MakeInParam("@DeptLevel", (DbType)SqlDbType.Int, 4,0 ),
                                                              MakeInParam("@AgentID", (DbType)SqlDbType.Int, 4,UserSerialID ),
                                                              MakeInParam("@BeAgentID", (DbType)SqlDbType.Int, 4,BeAgentID  ),
                                                          };
                        string sqlRequestAgent = "sp_InsertWorkflowAgentDetail";
                        ExecuteNonQuery(CommandType.StoredProcedure, sqlRequestAgent, pramsRequestAgent);
                    }
                    return RequestID;
                }
                else
                {
                    throw new Exception("保存表单数据失败");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        private int GetSqlDbTypeLength(string FieldDbType)
        {
            FieldDbType = FieldDbType.ToUpper();
            if (FieldDbType == "INT")
                return 4;
            else if (FieldDbType == "FLOAT")
                return 8;
            else if (FieldDbType == "DATETIME")
                return 16;
            else
                return 2000;
        }

        /// <summary>
        /// 流程提交到下一个节点
        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <param name="RequestID"></param>
        /// <param name="NodeID"></param>
        /// <param name="UserSerialID"></param>
        /// <param name="ActionType"></param>
        /// <param name="OperateComment"></param>
        /// <param name="ClientIP"></param>
        /// <param name="IsAgent"></param>
        /// <param name="BeAgentID"></param>
        public void PromoteRequestToNextNode(int WorkflowID, int RequestID, int NodeID, int UserSerialID, int ActionType, string OperateComment, string ClientIP, int IsAgent, int BeAgentID)
        {
            try
            {

                DbParameter[] prams = {       
                                        MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                        MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                        MakeInParam("@NodeOperatorID", (DbType)SqlDbType.Int, 4,IsAgent==1?BeAgentID:UserSerialID),
                                        MakeInParam("@ActionType", (DbType)SqlDbType.Int, 4,ActionType ),
                                        MakeOutParam("@NextNodeLinkID", (DbType)SqlDbType.Int, 4),
                                        MakeOutParam("@NextNodeID", (DbType)SqlDbType.Int, 4),
                                        MakeOutParam("@NextNodeType", (DbType)SqlDbType.Int, 4 ),
                                        MakeOutParam("@NextTotalOperatorIDOptimize", (DbType)SqlDbType.VarChar, 2000 ),
                                        MakeOutParam("@NextRuleID", (DbType)SqlDbType.VarChar,2000 ),
                                        MakeOutParam("@NextRuleType", (DbType)SqlDbType.VarChar,2000 ),
                                        MakeOutParam("@NextDeptLevel", (DbType)SqlDbType.Int,4),
                                      };
                string sql = "sp_GetNextOperatorList";
                ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);

                //获得下一个操作节点的相关信息
                int NextNodeLinkID = Convert.ToInt32(prams[4].Value);
                int NextNodeID = Convert.ToInt32(prams[5].Value);
                int NextNodeTypeID = Convert.ToInt32(prams[6].Value);
                string NextOperatorID = Convert.ToString(prams[7].Value);
                string NextRuleID = Convert.ToString(prams[8].Value);
                string NextRuleType = Convert.ToString(prams[9].Value);
                int NextDeptLevel = Convert.ToInt32(prams[10].Value);
                //更新workflow_RequestBase
                DbParameter[] pramsRequestBase = {
                                                     MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4, RequestID),
                                                     MakeInParam("@NextNodeLinkID", (DbType)SqlDbType.Int, 4,NextNodeLinkID),
                                                     MakeInParam("@NextNodeID", (DbType)SqlDbType.Int, 4,NextNodeID),
                                                     MakeInParam("@NextNodeTypeID", (DbType)SqlDbType.Int , 4,NextNodeTypeID),
                                                     MakeInParam("@NextRuleID", (DbType)SqlDbType.VarChar, 2000,NextRuleID),
                                                     MakeInParam("@NextRuleType", (DbType)SqlDbType.VarChar, 2000,NextRuleType ),
                                                     MakeInParam("@NextOperatorID", (DbType)SqlDbType.VarChar, 2000,NextOperatorID ),
                                                     MakeInParam("@NextDeptLevel", (DbType)SqlDbType.Int, 4,NextDeptLevel ),

                                                 };
                string sqlRequestBase = "sp_UpdateWorkflow_RequestBase";
                ExecuteNonQuery(CommandType.StoredProcedure, sqlRequestBase, pramsRequestBase);

                //记录操作日志，插入Workflow_RequestLog
                DbParameter[] pramsRequestLog = {
                                                    MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4, RequestID),
                                                    MakeInParam("@WorkflowID", (DbType)SqlDbType.Int, 4,WorkflowID ),
                                                    MakeInParam("@NodeID", (DbType)SqlDbType.Int , 4,NodeID ),
                                                    MakeInParam("@OperatorID", (DbType)SqlDbType.Int , 4,IsAgent==1?BeAgentID:UserSerialID),
                                                    MakeInParam("@ClientIP", (DbType)SqlDbType.VarChar ,50,ClientIP),
                                                    MakeInParam("@TargetNodeID", (DbType)SqlDbType.Int , 4,NextNodeID ),
                                                    MakeInParam("@ReceivList", (DbType)SqlDbType.VarChar, 2000,NextOperatorID),
                                                    MakeInParam("@OperateType", (DbType)SqlDbType.VarChar, 2000,ActionType  ),
                                                    MakeInParam("@Creator", (DbType)SqlDbType.VarChar, 2000,IsAgent==1?BeAgentID:UserSerialID ),
                                                    MakeInParam("@AgentID",(DbType)SqlDbType.Int , 4,IsAgent==1?UserSerialID:0),
                                                    MakeInParam("@OperateComment", (DbType)SqlDbType.VarChar, 2000,OperateComment),
                                                };

                string sqlRequestLog = "sp_InsertIntoWorkflow_RequestLog";
                ExecuteNonQuery(CommandType.StoredProcedure, sqlRequestLog, pramsRequestLog);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 更新表单数据
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="dtMainEdit"></param>
        /// <param name="detailGroups"></param>
        /// <param name="GroupIDs"></param>
        /// <param name="dtFieldGroups"></param>
        /// <param name="dtFilesUpLoad"></param>
        /// <param name="FilesUpLoadFieldID"></param>
        /// <param name="RequestName"></param>
        public void updateFormData(int RequestID, DataTable dtMainEdit, ArrayList detailGroups, Int32[] GroupIDs, ArrayList dtFieldGroups, ArrayList dtFilesUpLoad, Int32[] FilesUpLoadFieldID, string RequestName)
        {
            try
            {
                string sqlUpdate = "update workflow_form set ";

                //先保存主字段并取得RequestID
                DbParameter[] prams = new DbParameter[dtMainEdit.Rows.Count + 2];
                for (int i = 0; i < dtMainEdit.Rows.Count; i++)
                {

                    
                    if (dtMainEdit.Rows[i]["FieldValue"].ToString() == "") //如果没有输入数据，直接用DBNULL来表示各种数据类型的 null值  Modified by Jerry Wang 2011-10-27
                        prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), DBNull.Value );
                    else
                     prams[i] = MakeInParam("@" + dtMainEdit.Rows[i]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtMainEdit.Rows[i]["SqlDbType"].ToString(), true), Convert.ToInt32(dtMainEdit.Rows[i]["SqlDbLength"]), dtMainEdit.Rows[i]["FieldValue"].ToString());
                    sqlUpdate = sqlUpdate + dtMainEdit.Rows[i]["FieldName"].ToString() + "=@" + dtMainEdit.Rows[i]["FieldName"].ToString() + ",";

                }
                if (dtMainEdit.Rows.Count > 0)
                {
                    sqlUpdate = sqlUpdate.Substring(0, sqlUpdate.Length - 1) + " where RequestID=@RequestID;";
                }
                else
                {
                    sqlUpdate = "";
                }
                sqlUpdate += " update Workflow_RequestBase set RequestName=@RequestName where RequestID=@RequestID ";

                prams[dtMainEdit.Rows.Count] = MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4, RequestID);
                prams[dtMainEdit.Rows.Count + 1] = MakeInParam("@RequestName", (DbType)SqlDbType.VarChar, 2000, RequestName);
                int rowCount = ExecuteNonQuery(CommandType.Text, sqlUpdate, prams);
                //保存明细数据
                if (rowCount > 0)
                {
                    for (int j = 0; j < detailGroups.Count; j++)
                    {
                        int GroupID = GroupIDs[j];
                        //先删除此明细组的数据
                        DbParameter[] pramsDelte = {
                                                        MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                        MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                                                    };
                        string sqlDelete = "delete from Workflow_FormDetail where RequestID=@RequestID and GroupID=@GroupID";
                        ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelte);

                        DataTable dtValue = (DataTable)detailGroups[j];
                        DataTable dtField = (DataTable)dtFieldGroups[j];
                        for (int ii = 0; ii < dtValue.Rows.Count; ii++)
                        {
                            string sqlColumn = "insert into Workflow_FormDetail(RequestID,GroupID,";
                            string sqlColumnValue = "(" + RequestID.ToString() + "," + GroupID.ToString() + ",";
                            DbParameter[] SqlGroupParms = new DbParameter[dtValue.Columns.Count];
                            for (int jj = 0; jj < dtField.Rows.Count; jj++)
                            {
                                if ((dtField.Rows[jj]["IsEdit"].ToString() == "1" && dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString() == "") || (dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString()!="")) //可编辑并且编辑成空时候 或者有值的时候要保存
                                {
                                    SqlGroupParms[jj] = MakeInParam("@" + dtField.Rows[jj]["FieldName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtField.Rows[jj]["SqlDbType"].ToString(), true), Convert.ToInt32(dtField.Rows[jj]["SqlDbLength"]), dtValue.Rows[ii][dtField.Rows[jj]["FieldName"].ToString()].ToString());
                                    sqlColumn = sqlColumn + dtField.Rows[jj]["FieldName"].ToString() + ",";
                                    sqlColumnValue = sqlColumnValue + "@" + dtField.Rows[jj]["FieldName"].ToString() + ",";
                                }
                            }

                            sqlColumn = sqlColumn.Substring(0, sqlColumn.Length - 1) + ") values ";
                            sqlColumnValue = sqlColumnValue.Substring(0, sqlColumnValue.Length - 1) + ")";
                            string sqlInsertGroup = sqlColumn + sqlColumnValue + "; select @@identity; ";
                            ExecuteNonQuery(CommandType.Text, sqlInsertGroup, SqlGroupParms);
                        }
                    }

                    //保存上传的附件

                    for (int j = 0; j < dtFilesUpLoad.Count; j++)
                    {
                        int FieldID = FilesUpLoadFieldID[j];


                        //先删除此明细组的数据
                        DbParameter[] pramsDelte = {
                                                        MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                        MakeInParam("@FieldID", (DbType)SqlDbType.Int   , 4, FieldID),
                                                    };
                        string sqlDelete = "delete from Workflow_RequestFileAttach where RequestID=@RequestID and FieldID=@FieldID";
                        ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelte);

                        DataTable dtValue = (DataTable)dtFilesUpLoad[j];
                        for (int jj = 0; jj < dtValue.Rows.Count; jj++)
                        {
                            DbParameter[] pramsFileUpload ={
                               MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                               MakeInParam("@FieldID", (DbType)SqlDbType.Int, 4,FieldID),
                               MakeInParam("@FileServerName", (DbType)SqlDbType.VarChar ,2000,dtValue.Rows[jj]["FileServerName"].ToString()),
                               MakeInParam("@FileType", (DbType)SqlDbType.VarChar ,50,dtValue.Rows[jj]["FileType"].ToString()),
                               MakeInParam("@FileClientName", (DbType)SqlDbType.VarChar ,2000,dtValue.Rows[jj]["FileClientName"].ToString()),
                               MakeInParam("@FileSize", (DbType)SqlDbType.Float, 8,dtValue.Rows[jj]["FileSize"].ToString()),
                               MakeInParam("@Uploader", (DbType)SqlDbType.Int, 4,dtValue.Rows[jj]["Uploader"].ToString() ),
                               MakeInParam("@UploadDate", (DbType)SqlDbType.DateTime ,16,dtValue.Rows[jj]["UploadDate"].ToString() ),

                            };
                            string sqlFileGroup = "sp_SaveRequestFieldUploadFile";
                            ExecuteNonQuery(CommandType.StoredProcedure, sqlFileGroup, pramsFileUpload);
                        }
                    }
                }
                else
                {
                    throw new Exception("保存表单数据失败");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// 工作流查看日志
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="WorkflowID"></param>
        /// <param name="NodeID"></param>
        /// <param name="UserSerialID"></param>
        /// <param name="IPAddress"></param>
        /// <param name="loginType"></param>
        public void DoRequestViewLog(int RequestID, int WorkflowID, int NodeID, int UserSerialID, string IPAddress, int loginType)
        {
            DbParameter[] prams ={
                                     MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                     MakeInParam("@WorkflowID", (DbType)SqlDbType.Int   , 4, WorkflowID),
                                     MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,NodeID),
                                     MakeInParam("@UserSerialID", (DbType)SqlDbType.Int  ,4,UserSerialID),
                                     MakeInParam("@ClientIP", (DbType)SqlDbType.VarChar ,50,IPAddress),
                                     MakeInParam("@loginType", (DbType)SqlDbType.Int  ,4,loginType),
                                 };
            string sql = "sp_InsertRequestViewLog";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
        }

        /// <summary>
        /// 获取流程的基本信息(包含对应的WorkflowID, FormID,当前的NodeID,NodeType)
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public ArrayList GetRequestBaseInforbyRequestID(int RequestID)
        {
            ArrayList arrayList = new ArrayList();
            DbParameter[] prams = {       
                                        MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                        MakeOutParam("@NodeID", (DbType)SqlDbType.Int, 4),
                                        MakeOutParam("@NodeType", (DbType)SqlDbType.Int, 4 ),
                                        MakeOutParam("@FormID", (DbType)SqlDbType.Int,4 ),
                                        MakeOutParam("@WorkflowID", (DbType)SqlDbType.Int,4 ),
                                        MakeOutParam("@DeptLevel", (DbType)SqlDbType.Int,4 ),
                                   };
            string sql = "sp_GetRequestBaseInforbyRequestID";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            arrayList.Add(Convert.ToInt32(prams[1].Value));
            arrayList.Add(Convert.ToInt32(prams[2].Value));
            arrayList.Add(Convert.ToInt32(prams[3].Value));
            arrayList.Add(Convert.ToInt32(prams[4].Value));
            arrayList.Add(Convert.ToInt32(prams[5].Value));
            return arrayList;
        }

        /// <summary>
        /// 检查当前用户对此流程实例的操作权限
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="UserSerialID"></param>
        /// <param name="IsAgent"></param>
        /// <param name="NodeID"></param>
        /// <param name="DeptLevel"></param>
        /// <returns>0表示没有权限 1 表示只读不是当前节点的操作者但在Allparticipator中 2表示是此流程的当前节点的操作</returns>
        public Int32[] GetRequestOperateRightType(int RequestID, int UserSerialID, int IsAgent, int NodeID, int DeptLevel)
        {
            ArrayList arrayList = new ArrayList();
            DbParameter[] prams = {
                                        MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                        MakeInParam("@OperatorID", (DbType)SqlDbType.Int   , 4, UserSerialID),
                                        MakeInParam("@IsAgent", (DbType)SqlDbType.Int   , 4, IsAgent),
                                        MakeInParam("@NodeID", (DbType)SqlDbType.Int   , 4, NodeID),
                                        MakeInParam("@DeptLevel", (DbType)SqlDbType.Int   , 4, DeptLevel),
                                        MakeOutParam("@RightType", (DbType)SqlDbType.Int,4 ),
                                        MakeOutParam("@BeAgentID", (DbType)SqlDbType.Int,4 ),
                                   };


            string sql = "sp_GetRequestOperateRightTypebyRequestID";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            Int32[] intReturn = new Int32[2];
            intReturn[0] = Convert.ToInt32(prams[5].Value);
            intReturn[1] = Convert.ToInt32(prams[6].Value);
            return intReturn;
        }


        /// <summary>
        /// 获取被代理的流程创建者
        /// </summary>
        /// <param name="WorkflowID"></param>
        /// <param name="AgentID"></param>
        /// <returns></returns>
        public DataTable GetBeAgentCreatePerson(int WorkflowID, int AgentID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@WorkflowID", (DbType)SqlDbType.Int   , 4, WorkflowID),
                                      MakeInParam("@AgentID", (DbType)SqlDbType.Int   , 4, AgentID),
                                  };

            string sql = "sp_GetBeAgentIDForAgentWorkflowCreate";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 获取流程实例的主字段数据
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public DataTable GetRequestMainFieldData(int RequestID)
        {
            ArrayList arrayList = new ArrayList();
            DbParameter[] prams = {
                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4, RequestID),
                                  };
            string sql = "sp_GetRequestMainFieldDataByRequestID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获取Browse字段的显示文字
        /// </summary>
        /// <param name="BrowseValueSql"></param>
        /// <param name="BrowseSqlParam"></param>
        /// <param name="FieldValue"></param>
        /// <returns></returns>
        public string GetBrowseFieldText(string BrowseValueSql, string BrowseSqlParam, string FieldValue)
        {
            //有可能是多个值 Add by Jerry  2010-12-24要做处理
            //ArrayList arrayList = new ArrayList();

            string returnValue = "";
            //检查fieldValue是否是多个值，用逗号割开的 Add by Jerry Wang 2011-10-21

            if (FieldValue.IndexOf(",") <= 0)
            {

                
                DbParameter[] prams = {
                                      MakeInParam(BrowseSqlParam, (DbType)SqlDbType.VarChar, 50, FieldValue),
                                  };
                string sql = BrowseValueSql;
                returnValue =  ExecuteScalar(CommandType.Text, sql, prams).ToString() ;

            }
            else
            {
                //处理多个职的情况
                //如果最后没有逗号，加个逗号
                if (FieldValue.Substring(FieldValue.Length - 1, 1) != ",")
                    FieldValue = FieldValue + ",";
                while (FieldValue!=""&&FieldValue.IndexOf(",") > 0)
                {
                    string parameterValue = FieldValue.Substring(0, FieldValue.IndexOf(","));
                    FieldValue = FieldValue.Substring(FieldValue.IndexOf(",") + 1, FieldValue.Length - FieldValue.IndexOf(",") - 1);
                    DbParameter[] prams = {
                                      MakeInParam(BrowseSqlParam, (DbType)SqlDbType.VarChar, 50, parameterValue),
                                  };
                    string sql = BrowseValueSql;
                    returnValue = returnValue + ExecuteScalar(CommandType.Text, sql, prams).ToString() + ",";
                }
            }
            return returnValue; 
        }


        /// <summary>
        /// 获取流程实例的附件
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        public DataTable GetRequestAttachFile(int RequestID, int FieldID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4,RequestID),
                                      MakeInParam("@FieldID", (DbType)SqlDbType.Int, 4,FieldID), 
                                  };
            string sql = "sp_GetRequestAttachFileByRequestID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 获取文件类型对应的图标Path
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public string GetFileImageTagPath(string fileType)
        {
            DbParameter[] prams = {
                                      MakeInParam("@FileType", (DbType)SqlDbType.VarChar, 50,fileType),                                        
                                  };
            string sql = "sp_GetFileImageTagPathByFileType";
            return ExecuteScalar(CommandType.StoredProcedure, sql, prams).ToString();
        }

        /// <summary>
        /// 获取明细字段相关的行列规则
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldName"></param>
        /// <returns></returns>
        public DataTable GetFormDetailRule(int FormID, int GroupID, string FieldName)
        {
            DbParameter[] prams = {
                                      MakeInParam("@FormID", (DbType)SqlDbType.Int, 4,FormID),
                                      MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                                      MakeInParam("@FieldName", (DbType)SqlDbType.VarChar,50,FieldName),
                                  };
            string sql = "sp_GetFormDetailRuleByFormIDGroupIDAndFieldName";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获取本字段为目标字段的行规则
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        public DataTable GetFormDetailRowRule(int FormID, int GroupID, int FieldID)
        {
            DbParameter[] prams = {
                                       MakeInParam("@FormID", (DbType)SqlDbType.Int, 4,FormID),
                                       MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                                       MakeInParam("@FieldID", (DbType)SqlDbType.Int, 4,FieldID), 
                                   };
            string sql = "sp_GetFormDetailRowRuleByFormIDGroupIDAndFieldID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获取明细组下的所有行规则
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataTable GetFormDetailRowRule(int FormID, int GroupID)
        {
            DbParameter[] prams = {   
                                       MakeInParam("@FormID", (DbType)SqlDbType.Int, 4,FormID),
                                       MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                                   };
            string sql = "sp_GetFormDetailRowRuleByFormIDAndGroupID";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 按行规则计算出目标值
        /// </summary>
        /// <param name="RuleFieldName"></param>
        /// <param name="FieldValue"></param>
        /// <param name="RuleDetail"></param>
        /// <param name="FieldDBType"></param>
        /// <returns></returns>
        public string GetDetailFormRowRuleValue(string RuleFieldName, string FieldValue, string RuleDetail, string FieldDBType)
        {
            string[] splitStr = new string[1];
            splitStr[0] = "|";
            string[] FieldNameArray = RuleFieldName.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            string[] FieldValueArray = FieldValue.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            string[] FieldDBTypeArray = FieldDBType.Split(splitStr, StringSplitOptions.RemoveEmptyEntries);
            DbParameter[] prams = new DbParameter[FieldNameArray.Length];
            for (int i = 0; i < FieldNameArray.Length; i++)
            {
                RuleDetail = RuleDetail.Replace(FieldNameArray[i], "@" + FieldNameArray[i]);
                prams[i] = MakeInParam("@" + FieldNameArray[i], (DbType)Enum.Parse(typeof(SqlDbType), FieldDBTypeArray[i], true), GetSqlDbTypeLength(FieldDBTypeArray[i]), FieldValueArray[i]);
            }
            string sql = "select " + Utils.UrlDecode(RuleDetail);
            return ExecuteScalar(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// 获取明细组下的所有列规则
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public DataTable GetFormDetailColumnRule(int FormID, int GroupID)
        {
            DbParameter[] prams = {   
                                       MakeInParam("@FormID", (DbType)SqlDbType.Int, 4,FormID),
                                       MakeInParam("@GroupID", (DbType)SqlDbType.Int, 4,GroupID),
                                   };
            string sql = "sp_GetFormDetailColumnRuleByFormIDAndGroupID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获得用户最后操作表单所属的节点编号
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="UserSerialID"></param>
        /// <returns></returns>
        public int GetRequestLastOperateNodeIDByUserSerialID(int RequestID, int UserSerialID)
        {
            DbParameter[] prams = {
                                       MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                       MakeInParam("@UserSerialID", (DbType)SqlDbType.Int , 4, UserSerialID),
                                       MakeOutParam("@NodeID", (DbType)SqlDbType.Int,4 ),
                                   };
            string sql = "sp_GetRequestLastOperateNodeIDbyUserSerialID";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            return Convert.ToInt32(prams[2].Value);
        }

        /// <summary>
        /// 获取节点前的附加操作Type0
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="OPTime"></param>
        /// <returns></returns>
        public DataTable GetNodeAddInOperateType0(int NodeID, int OPTime)
        {
            DbParameter[] prams = {
                                       MakeInParam("@NodeID", (DbType)SqlDbType.Int   , 4, NodeID),
                                       MakeInParam("@OPTime", (DbType)SqlDbType.Int   , 4, OPTime), 
                                   };
            string sql = "sp_GetNodeAddInOperateType0";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 附加动作0，覆值动作
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="CaculateType"></param>
        /// <param name="DataSourceID"></param>
        /// <param name="CaculateValue"></param>
        /// <param name="DataSetID"></param>
        /// <param name="ValueField"></param>
        /// <param name="SPName"></param>
        /// <param name="OutParameter"></param>
        /// <param name="OPCondition"></param>
        /// <param name="RequestID"></param>
        /// <param name="TargetFieldID"></param>
        /// <returns></returns>
        public string ProcessAddInOperateType0(int AddInOPID, int CaculateType, int DataSourceID, string CaculateValue, int DataSetID, string ValueField, string SPName, string OutParameter, string OPCondition, int RequestID, int TargetFieldID)
        {

            //先检查是否当前流程实例是否符合操作条件


            DbParameter[] pramsOperaCondition = {
                                                    MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                    MakeInParam("@Condition", (DbType)SqlDbType.VarChar   , 2000, OPCondition), 
                                                };
            if (ExecuteDataset(CommandType.StoredProcedure, "sp_CheckRequestFitConditon", pramsOperaCondition).Tables[0].Rows.Count > 0)
            {
                if (CaculateType == 1)  //计算公式
                {
                    DbParameter[] pramsFunction = {
                                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID), 
                                                      MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   ,200, CaculateValue ),
                                                      MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   ,200 ),
                                                  };

                    //string sqlFunction = "select dbo.Fun_CaculateValueByFieldFunction(@RequestID," + CaculateValue + ")";
                    string sqlFunction = "sp_CaculateValueByFieldFunction";
                    ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                    return pramsFunction[2].Value.ToString();
                }
                else if (CaculateType == 2)  //浏览画面
                {
                    //Workflow_FieldDictEntity targetFieldEntity = GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID.ToString());
                    //int BrowseType = targetFieldEntity.BrowseType;
                    //Workflow_BrowseTypeEntity browseTypeEntity = GetWorkflow_BrowseTypeEntityByKeyCol(BrowseType.ToString());
                    //string BrowseSql = browseTypeEntity.BrowseValueSql;
                    //string BrowseSqlParam = browseTypeEntity.BrowseSqlParam;
                    //string  GetBrowseFieldText(BrowseValueSql, BrowseSqlParam, CaculateValue );
                    return CaculateValue;
                }
                else if (CaculateType == 3)  //数据集
                {
                    return "";
                }
                else if (CaculateType == 4) //调用数据源存储过程
                {

                    Workflow_DataSourceEntity DataSourceEntity = GetDataSourceByID(DataSourceID);
                    string DBType = DataSourceEntity.DataSourceDBType;
                    string ConnectString = DataSourceEntity.ConnectString;

                    //获取参数列表
                    DbParameter[] pramsFunction = {
                                                      MakeInParam("@AddInOPID", (DbType)SqlDbType.Int, 4, AddInOPID),
                                                  };
                    string DbParameter = "select * from Workflow_NodeAddInOperation_Type0_SpParameter where AddInOPID=@AddInOPID";
                    DataTable dtParameter = ExecuteDataset(CommandType.Text, DbParameter, pramsFunction).Tables[0];
                    int OutParameterIndex = 0;
                    DbParameter[] DbParms = new DbParameter[dtParameter.Rows.Count];
                    for (int j = 0; j < dtParameter.Rows.Count; j++)
                    {
                        string sParameterDirection = dtParameter.Rows[j]["ParameterDirection"].ToString();
                        string SpParameter = dtParameter.Rows[j]["SpParameter"].ToString();
                        string ParameterType = dtParameter.Rows[j]["ParameterType"].ToString();
                        int ParameterSize = Convert.ToInt32(dtParameter.Rows[j]["ParameterSize"].ToString());
                        if (sParameterDirection.ToUpper() == "INPUT" || sParameterDirection.ToUpper() == "INPUTOUTPUT")
                        {
                            string TartgetValue = dtParameter.Rows[j]["TartgetValue"].ToString();
                            DbParameter[] prams = {
                                                      GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                      GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, TartgetValue=="" ? "''" : TartgetValue),
                                                      GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 200),
                                                  };
                            string sqlFunction = "sp_CaculateValueByFieldFunction";
                            GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, prams).ToString();
                            string ParamValue = prams[2].Value.ToString();

                            ParameterDirection parameterDirection = (ParameterDirection)Enum.Parse(typeof(ParameterDirection), sParameterDirection);
                            if (DBType == "Oracle")
                            {
                                DbParms[j] = GetInstance(DBType, ConnectString).MakeParam(SpParameter, (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), ParameterType, true), ParameterSize, parameterDirection, ParamValue);
                            }
                            else
                            {
                                DbParms[j] = GetInstance(DBType, ConnectString).MakeParam(SpParameter, (DbType)Enum.Parse(typeof(SqlDbType), ParameterType, true), ParameterSize, parameterDirection, ParamValue);
                            }
                        }
                        else
                        {
                            if (DBType == "Oracle")
                            {
                                DbParms[j] = GetInstance(DBType, ConnectString).MakeOutParam(SpParameter, (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), ParameterType, true), ParameterSize);
                            }
                            else
                            {
                                DbParms[j] = GetInstance(DBType, ConnectString).MakeOutParam(SpParameter, (DbType)Enum.Parse(typeof(SqlDbType), ParameterType, true), ParameterSize);
                            }
                        }
                        if (dtParameter.Rows[j]["SpParameter"].ToString().ToUpper() == OutParameter.ToUpper())
                            OutParameterIndex = j;
                    }

                    GetInstance(DBType, ConnectString).ExecuteScalar(CommandType.StoredProcedure, SPName, DbParms);
                    return DbParms[OutParameterIndex].Value.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 更新表单的字段值
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="FieldName"></param>
        /// <param name="FieldDBType"></param>
        /// <param name="sResultField"></param>
        public void UpdateFormFieldValue(int RequestID, string FieldName, string FieldDBType, string sResultField)
        {

            DbParameter[] prams = {
                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                      MakeInParam("@FieldName", (DbType)SqlDbType.VarChar   , 2000, FieldName),
                                      MakeInParam("@FieldValue", (DbType)Enum.Parse(typeof(SqlDbType), FieldDBType, true)   ,GetSqlDbTypeLength(FieldDBType), sResultField),
                                  };

            string sqlUpdate = "update Workflow_Form set @FieldName=@FieldValue where RequestID=@RequestID";
            ExecuteNonQuery(CommandType.Text, sqlUpdate, prams);
        }

        /// <summary>
        /// 获取节点前的附加操作Type1
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="OPTime"></param>
        /// <returns></returns>
        public DataTable GetNodeAddInOperateType1(int NodeID, int OPTime)
        {
            DbParameter[] prams = {
                                       MakeInParam("@NodeID", (DbType)SqlDbType.Int   , 4, NodeID),
                                       MakeInParam("@OPTime", (DbType)SqlDbType.Int   , 4, OPTime),
                                   };
            string sql = "sp_GetNodeAddInOperateType1";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 节点附加动作1的处理
        /// </summary>
        /// <param name="AddInOPID"></param>
        /// <param name="CaculateType"></param>
        /// <param name="DataSourceID"></param>
        /// <param name="DataTableName"></param>
        /// <param name="GroupID"></param>
        /// <param name="selectRange"></param>
        /// <param name="OPCycleType"></param>
        /// <param name="OPCondition"></param>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public string ProcessAddInOperateType1(int AddInOPID, int CaculateType, int DataSourceID, string DataTableName, int GroupID, string selectRange, int OPCycleType, string OPCondition, int RequestID)
        {
            //先检查是否当前流程实例是否符合操作条件


            DbParameter[] pramsOperaCondition = {
                                                    MakeInParam("@RequestID", (DbType)SqlDbType.Int, 4, RequestID),
                                                    MakeInParam("@Condition", (DbType)SqlDbType.VarChar, 2000, OPCondition), 
                                                };
            if (ExecuteDataset(CommandType.StoredProcedure, "sp_CheckRequestFitConditon", pramsOperaCondition).Tables[0].Rows.Count > 0)
            {
                //获取数据源信息
                Workflow_DataSourceEntity DataSourceEntity = GetDataSourceByID(DataSourceID);
                //取字段对应
                DbParameter[] prams = {
                                          MakeInParam("@AddInOPID", (DbType)SqlDbType.Int   , 4, AddInOPID),
                                      };
                string sql = "sp_GetNodeFieldMapping_AddInOperateType1";
                DataTable dtFieldMap = ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];

                //取主字段值
                DbParameter[] pramsMain = {
                                              MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                          };
                string sqlMain = "sp_GetRequestMainFieldDataByRequestID";
                DataTable dtMain = ExecuteDataset(CommandType.StoredProcedure, sqlMain, pramsMain).Tables[0];

                DataTable dtGroup;
                if (GroupID > 0)
                {
                    //获取明细组的值(要加selectRange的限制 Mark by Jerry Wang 2010-5-17)
                    DbParameter[] pramsGroup = {
                                               MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                               MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                               MakeInParam("@SelectFilter", (DbType)SqlDbType.VarChar   , 2000, selectRange),
                                           };
                    string sqlGroup = "sp_GetRequestGroupValueByRequstIDAndGroupID";
                    dtGroup = ExecuteDataset(CommandType.StoredProcedure, sqlGroup, pramsGroup).Tables[0];
                }
                else
                {
                    dtGroup = dtMain.Clone();
                }

                //执行语句的条件
                DbParameter[] pramsCondition = {
                                                   MakeInParam("@AddInOPID", (DbType)SqlDbType.Int   , 4, AddInOPID),
                                               };
                string sqlCondition = "sp_GetFieldConditon_AddInOperateType1";
                DataTable dtConditon = ExecuteDataset(CommandType.StoredProcedure, sqlCondition, pramsCondition).Tables[0];

                //存储过程的参数对应
                //DbParameter[] pramsSP = {       
                //                           MakeInParam("@AddInOPID", (DbType)SqlDbType.Int   , 4, AddInOPID),
                                            
                //                          };
                //string sqlSP = "sp_GetFieldConditon_AddInOperateType1";
                //DataTable dtSPCondition = ExecuteDataset(CommandType.StoredProcedure, sqlSP, pramsSP).Tables[0];
                //return ProcessOperateDataSourceData(DataSourceEntity.DataSourceDBType, DataSourceEntity.ConnectString, RequestID, GroupID, CaculateType, dtFieldMap, dtConditon, dtMain, dtGroup, OPCycleType, dtSPCondition, DataTableName);
                //存储过程的参数对应
                DbParameter[] pramsSP = {
                                            MakeInParam("@AddInOPID",(DbType)SqlDbType.Int  , 4, AddInOPID),
                                         };
                string sqlSP = "sp_GetWorkflow_NodeAddInOperation_Type1_SpParameter";
                DataTable dtSP = ExecuteDataset( CommandType.StoredProcedure, sqlSP, pramsSP).Tables[0];

                return ProcessOperateDataSourceData(DataSourceEntity.DataSourceDBType, DataSourceEntity.ConnectString, RequestID, GroupID, CaculateType, dtFieldMap, dtConditon, dtMain, dtGroup, OPCycleType, dtSP, DataTableName);
            }
            else
            {
                return "0";
            }
        }


        /// <summary>
        /// 为数据集成，做字段Mapping时，基于字段的公式的值
        /// </summary>
        /// <param name="RequestID"></param>
        /// <param name="GroupID"></param>
        /// <param name="DetailID"></param>
        /// <param name="FieldFunction"></param>
        /// <param name="OPCycleType"></param>
        /// <param name="FieldTypeID"></param>
        /// <returns></returns>
        private string CaculateFieldFunctionValue(int RequestID, int GroupID, int DetailID, string FieldFunction, int OPCycleType, string FieldTypeID)
        {


            //计算TargetValue
            
            if (FieldTypeID == "1")
            {
                DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, FieldFunction), 
                                           MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                       };

                string sqlFunction = "[sp_CaculateValueByFieldFunction]";
                ExecuteNonQuery( CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                string ParamValue = pramsFunction[2].Value.ToString();
                return ParamValue;
            }
            else
            {
                if (OPCycleType == 0)
                {
                    DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, FieldFunction), 
                                           MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                       };

                    //string sqlFunction = "select dbo.Fun_CaculateValueByAllLineDetailFieldFunction(@RequestID,@GroupID,@FieldFunction)";
                    string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                    ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                    string ParamValue = pramsFunction[3].Value.ToString();
                    return ParamValue;
                }
                else
                {


                    DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID ),
                                           MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, FieldFunction), 
                                           MakeOutParam("@ReturnResult",(DbType)SqlDbType.VarChar   , 2000), 
                                           };

                    //string sqlFunction = "select dbo.Fun_CaculateValueByOneLineDetailFieldFunction(@RequestID,@GroupID,@DetailID,@FieldFunction)";
                    string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                    ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                    string ParamValue = pramsFunction[4].Value.ToString();
                    return ParamValue;



                }

            }



        }



        /// <summary>
        /// 对别的数据源的数据的操作
        /// </summary>
        /// <param name="DataSourceDBType"></param>
        /// <param name="ConnectString"></param>
        /// <param name="RequestID"></param>
        /// <param name="GroupID"></param>
        /// <param name="CaculateType"></param>
        /// <param name="dtFieldMap"></param>
        /// <param name="dtConditon"></param>
        /// <param name="dtMain"></param>
        /// <param name="dtGroup"></param>
        /// <param name="OPCycleType"></param>
        /// <param name="dtSPCondition"></param>
        /// <param name="DataTableName"></param>
        /// <returns></returns>
        public string ProcessOperateDataSourceData(string DataSourceDBType, string ConnectString, int RequestID, int GroupID, int CaculateType, DataTable dtFieldMap, DataTable dtConditon, DataTable dtMain, DataTable dtGroup, int OPCycleType, DataTable dtSP, string DataTableName)
        {
            try
            {
                int execTimes = 0;
                if (OPCycleType == 0)
                    execTimes = 1;
                else
                    execTimes = dtGroup.Rows.Count;

                #region "数据集成"
                if (CaculateType == 0)  //insert 将数据写入到数据源中
                {
                    for (int j = 0; j < execTimes; j++)
                    {
                        string sqlInsert = "insert into " + DataTableName + "(";
                        string sqlInsertValue = " values(";
                        DbParameter[] parmFieldValue = new DbParameter[dtFieldMap.Rows.Count];
                        for (int i = 0; i < dtFieldMap.Rows.Count; i++)
                        {
                            string ParamValue = "";
                            if (dtFieldMap.Rows[i]["FieldTypeID"].ToString() == "1") //目标值来自主字段
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[2].Value.ToString();
                            }
                            else if (OPCycleType == 0)
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000),
                                                              };

                                string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                                ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[3].Value.ToString();
                            }
                            else
                            {
                                int DetailID = Convert.ToInt32(dtGroup.Rows[j]["DetailID"].ToString());
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[4].Value.ToString();
                            }

                            parmFieldValue[i] = GetInstance(DataSourceDBType, ConnectString).MakeInParam("@" + dtFieldMap.Rows[i]["TargetFieldName"].ToString(), (DbType)SqlDbType.VarChar, 2000, ParamValue);
                            sqlInsert = sqlInsert + dtFieldMap.Rows[i]["TargetFieldName"].ToString() + ",";
                            sqlInsertValue = sqlInsertValue + "@" + dtFieldMap.Rows[i]["TargetFieldName"].ToString() + ",";
                        }
                        string sqlInsertFull = sqlInsert.Substring(0, sqlInsert.Length - 1) + ")" + sqlInsertValue.Substring(0, sqlInsertValue.Length - 1) + ")";
                        GetInstance(DataSourceDBType, ConnectString).ExecuteNonQuery(CommandType.Text, sqlInsertFull, parmFieldValue);
                    }
                }
                else if (CaculateType == 1) //update 更新数据源的数据
                {
                    for (int j = 0; j < execTimes; j++)
                    {
                        string sqlUpdate = "update " + DataTableName + "set ";
                        DbParameter[] parmFieldValueWhereConditon = new DbParameter[dtFieldMap.Rows.Count + dtConditon.Rows.Count];
                        for (int i = 0; i < dtFieldMap.Rows.Count; i++)
                        {
                            string ParamValue = "";
                            if (dtFieldMap.Rows[i]["FieldTypeID"].ToString() == "1") //目标值来自主字段
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[2].Value.ToString();
                            }
                            else if (OPCycleType == 0)
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000),
                                                              };

                                string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                                ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[3].Value.ToString();
                            }
                            else
                            {
                                int DetailID = Convert.ToInt32(dtGroup.Rows[i]["DetailID"].ToString());
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtFieldMap.Rows[i]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[4].Value.ToString();
                            }
                            parmFieldValueWhereConditon[i] = GetInstance(DataSourceDBType, ConnectString).MakeInParam("@" + dtFieldMap.Rows[i]["TargetFieldName"].ToString(), (DbType)SqlDbType.VarChar, 2000, ParamValue);
                            sqlUpdate = sqlUpdate + dtFieldMap.Rows[i]["TargetFieldName"].ToString() + "=" + "@" + dtFieldMap.Rows[i]["TargetFieldName"].ToString() + ",";
                        }
                        //生成where条件
                        string sqlUpDateCondition = "";
                        for (int ii = 0; ii < dtConditon.Rows.Count; ii++)
                        {
                            string ParamValue = "";
                            if (dtConditon.Rows[ii]["FieldTypeID"].ToString() == "1") //目标值来自主字段
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[2].Value.ToString();
                            }
                            else if (OPCycleType == 0)
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000),
                                                              };

                                string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                                ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[3].Value.ToString();
                            }
                            else
                            {
                                int DetailID = Convert.ToInt32(dtGroup.Rows[j]["DetailID"].ToString());
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[4].Value.ToString();
                            }

                            parmFieldValueWhereConditon[ii] = GetInstance(DataSourceDBType, ConnectString).MakeInParam("@" + dtConditon.Rows[ii]["TargetFieldName"].ToString(), (DbType)SqlDbType.VarChar, 2000, dtGroup.Rows[j][dtFieldMap.Rows[ii]["TartgetValue"].ToString()].ToString());
                            sqlUpDateCondition = sqlUpDateCondition + "(" + dtConditon.Rows[ii]["TargetFieldName"].ToString() + dtConditon.Rows[ii]["SymbolName"].ToString() + "@" + dtConditon.Rows[ii]["TargetFieldName"].ToString() + ")" + dtConditon.Rows[ii]["AndOr"].ToString();
                        }
                        string sqlUpdateFull = sqlUpdate.Substring(0, sqlUpdate.Length - 1) + " where " + sqlUpDateCondition;
                        GetInstance(DataSourceDBType, ConnectString).ExecuteNonQuery(CommandType.Text, sqlUpdateFull, parmFieldValueWhereConditon);
                    }
                }
                else if (CaculateType == 2) //delete 删除数据源头的数据
                {
                    for (int j = 0; j < execTimes; j++)
                    {
                        //生成where条件
                        string sqlDeleteCondition = "";
                        DbParameter[] parmWhereConditon = new DbParameter[dtConditon.Rows.Count];

                        for (int ii = 0; ii < dtConditon.Rows.Count; ii++)
                        {
                            string ParamValue = "";
                            if (dtConditon.Rows[ii]["FieldTypeID"].ToString() == "1") //目标值来自主字段
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[2].Value.ToString();
                            }
                            else if (OPCycleType == 0)
                            {
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000),
                                                              };

                                string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                                ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[3].Value.ToString();
                            }
                            else
                            {
                                int DetailID = Convert.ToInt32(dtGroup.Rows[j]["DetailID"].ToString());
                                DbParameter[] pramsFunction = {
                                                                  GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                                                                  GetInstance().MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                                                  GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtConditon.Rows[ii]["TartgetValue"]), 
                                                                  GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                              };
                                string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                                GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                ParamValue = pramsFunction[4].Value.ToString();
                            }
                            parmWhereConditon[ii] = GetInstance(DataSourceDBType, ConnectString).MakeInParam("@" + dtConditon.Rows[ii]["TargetFieldName"].ToString(), (DbType)SqlDbType.VarChar, 2000, ParamValue);
                            sqlDeleteCondition = sqlDeleteCondition + "(" + dtConditon.Rows[ii]["TargetFieldName"].ToString() + dtConditon.Rows[ii]["SymbolName"] + "@" + dtConditon.Rows[ii]["TargetFieldName"].ToString() + ")" + dtConditon.Rows[ii]["AndOr"].ToString();
                        }
                        string sqlDeleteFull = "delete from " + DataTableName + " where " + sqlDeleteCondition;
                        GetInstance(DataSourceDBType, ConnectString).ExecuteNonQuery(CommandType.Text, sqlDeleteFull, parmWhereConditon);
                    }
                }
                else if (CaculateType == 3)  //调用存储过程
                {
                    for (int j = 0; j < execTimes; j++)
                    {
                        //生成存储过程的参数
                        //DbParameter[] parmSp = new DbParameter[dtSPCondition.Rows.Count];
                        //for (int ii = 0; ii < dtSPCondition.Rows.Count; ii++)
                        //{
                        //    string ParamValue = "";
                        //    if (dtSPCondition.Rows[ii]["FieldTypeID"].ToString() == "1") //目标值来自主字段
                        //    {
                        //        DbParameter[] pramsFunction = {
                        //                                          GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                        //                                          GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtSPCondition.Rows[ii]["TartgetValue"]), 
                        //                                          GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                        //                                      };
                        //        string sqlFunction = "sp_CaculateValueByFieldFunction";
                        //        GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                        //        ParamValue = pramsFunction[2].Value.ToString();
                        //    }
                        //    else if (OPCycleType == 0)
                        //    {
                        //        DbParameter[] pramsFunction = {
                        //                                          GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                        //                                          GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                        //                                          GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtSPCondition.Rows[ii]["TartgetValue"]), 
                        //                                          GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000),
                        //                                      };

                        //        string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                        //        ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                        //        ParamValue = pramsFunction[3].Value.ToString();
                        //    }
                        //    else
                        //    {
                        //        int DetailID = Convert.ToInt32(dtGroup.Rows[j]["DetailID"].ToString());
                        //        DbParameter[] pramsFunction = {
                        //                                          GetInstance().MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                        //                                          GetInstance().MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, GroupID),
                        //                                          GetInstance().MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                        //                                          GetInstance().MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, dtSPCondition.Rows[ii]["TartgetValue"]), 
                        //                                          GetInstance().MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                        //                                      };
                        //        string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                        //        GetInstance().ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                        //        ParamValue = pramsFunction[4].Value.ToString();
                        //    }

                        //    parmSp[ii] = GetInstance(DataSourceDBType, ConnectString).MakeInParam(dtConditon.Rows[ii]["SpParameter"].ToString(), (DbType)SqlDbType.VarChar, 2000, dtGroup.Rows[j][dtFieldMap.Rows[ii]["TartgetValue"].ToString()].ToString());
                        //}
                        //string sqlSP = DataTableName;
                        //GetInstance(DataSourceDBType, ConnectString).ExecuteNonQuery(CommandType.StoredProcedure, sqlSP, parmSp);

                                              
                            //生成存储过程的参数
                        DbParameter[] parmSp = new DbParameter[dtSP.Rows.Count];
                            for (int ii = 0; ii < dtSP.Rows.Count; ii++)
                            {
                                if (dtSP.Rows[ii]["FieldTypeID"].ToString() == "1")
                                    parmSp[ii] = MakeInParam(dtSP.Rows[ii]["SpParameter"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtSP.Rows[ii]["ParameterType"].ToString()), Convert.ToInt32(dtSP.Rows[ii]["ParameterSize"].ToString()), CaculateFieldFunctionValue(RequestID, GroupID, 0, dtSP.Rows[ii]["TartgetValue"].ToString(), OPCycleType, "1"));
                                else
                                    parmSp[ii] = MakeInParam(dtSP.Rows[ii]["SpParameter"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtSP.Rows[ii]["ParameterType"].ToString()), Convert.ToInt32(dtSP.Rows[ii]["ParameterSize"].ToString()), CaculateFieldFunctionValue(RequestID, GroupID, Convert.ToInt32(dtGroup.Rows[j]["DetailID"].ToString()), dtSP.Rows[ii]["TartgetValue"].ToString(), OPCycleType, "2"));

                            }
                            string sqlSP = DataTableName;
                            GetInstance(DataSourceDBType, ConnectString).ExecuteNonQuery(CommandType.StoredProcedure, sqlSP, parmSp);
                        
                    }
                }
                else
                {
                    return "0";
                }
                #endregion

                return "0";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        /// <summary>
        /// 获得节点的触发流程
        /// </summary>
        /// <param name="NodeID"></param>
        /// <param name="OPTime"></param>
        /// <returns></returns>
        public DataTable GetNodeTriggerWF(int NodeID, int OPTime)
        {
            DbParameter[] prams = {
                                       MakeInParam("@NodeID", (DbType)SqlDbType.Int , 4, NodeID),
                                       MakeInParam("@OPTime", (DbType)SqlDbType.Int , 4, OPTime), 
                                   };

            string sql = "sp_GetNodeTriggerWF";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 流程触发的处理
        /// </summary>
        /// <param name="TriggerID"></param>
        /// <param name="OPCondition"></param>
        /// <param name="TriggerWFID"></param>
        /// <param name="TriggerWFCreatorType"></param>
        /// <param name="WFCreateNode"></param>
        /// <param name="WFCreateFieldID"></param>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public string ProcessTriggerWF(int TriggerID, string OPCondition, int TriggerWFID, int TriggerWFCreatorType, int WFCreateNode, int WFCreateFieldID, int RequestID)
        {
            //先检查是否当前流程实例是否符合触发流程条件


            int fitFlag = 0;  //条件是否满足 1表示满足
            if (OPCondition == "1==1")
                fitFlag = 1;
            else
            {
                DbParameter[] prams = {
                                          MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                          MakeInParam("@Condition", (DbType)SqlDbType.VarChar   , 2000, OPCondition),    
                                      };
                string sql = "sp_CheckRequestFitConditon";
                if (ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0].Rows.Count > 0)
                {
                    fitFlag = 1;
                }
            }

            if (fitFlag == 1)
            {
                //计算被触发的流程的创建者
                DbParameter[] parmsTriggerCreatorID = {
                                                          MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                          MakeInParam("@TriggerWFCreatorType", (DbType)SqlDbType.Int   , 4, TriggerWFCreatorType),
                                                          MakeInParam("@WFCreateNode", (DbType)SqlDbType.Int   , 4, WFCreateNode),
                                                          MakeInParam("@WFCreateFieldID", (DbType)SqlDbType.Int   , 4, WFCreateFieldID),
                                                          MakeOutParam("@TriggerWFCreatorID", (DbType)SqlDbType.Int,4),
                                                      };
                string sqlTriggerWFCreatorID = "sp_GetTriggerWFCreatorID";
                ExecuteNonQuery(CommandType.StoredProcedure, sqlTriggerWFCreatorID, parmsTriggerCreatorID);
                int WFTriggerCreatorID = Convert.ToInt32(parmsTriggerCreatorID[4].Value.ToString());

                ////取字段对应

                //获取字段复制的主数据
                try
                {
                    DbParameter[] pramsFieldMappingMain = {
                                                              MakeInParam("@TriggerID", (DbType)SqlDbType.Int   , 4, TriggerID),
                                                          };
                    string sqlMappingMain = "select * from Workflow_TriggerWFFieldMappingMain where TriggerID=@TriggerID";
                    DataTable dtMappingMain = ExecuteDataset(CommandType.Text, sqlMappingMain, pramsFieldMappingMain).Tables[0];
                    int TargetRequestID = 0;
                    for (int i = 0; i < dtMappingMain.Rows.Count; i++)
                    {
                        int MappingID = Convert.ToInt32(dtMappingMain.Rows[i]["MappingID"].ToString());
                        int TargetGroupID = Convert.ToInt32(dtMappingMain.Rows[i]["TargetGroupID"].ToString());
                        int OPCycleType = Convert.ToInt32(dtMappingMain.Rows[i]["OPCycleType"].ToString());

                        //根据MappingID和triggerID来取字段对应
                        DbParameter[] pramsFieldMappingDetail = {
                                           MakeInParam("@TriggerID", (DbType)SqlDbType.Int   , 4, TriggerID),
                                           MakeInParam("@MappingID", (DbType)SqlDbType.Int   , 4, TriggerID),
                                           MakeInParam("@TargetGroupID", (DbType)SqlDbType.Int   , 4, TargetGroupID),
                                       };

                        string sqlMappingDetail = "select * from Workflow_TriggerWFFieldMappingDetail where TriggerID=@TriggerID and MappingID=@MappingID and TargetGroupID=@TargetGroupID order by TargetGroupID ";

                        DataTable dtMappingDetail = ExecuteDataset(CommandType.Text, sqlMappingDetail, pramsFieldMappingDetail).Tables[0];

                        string sqlTableColumn = "";
                        string sqlTableColumnValue = "";
                        int SourceDetailGroupID = 0;
                        if (TargetGroupID == 0) //对主字段进行复制
                        {
                            OPCycleType = 0;
                            int FormID = GetWorkflowFromIDbyWorkflowID(TriggerWFID); //获得TargetWorkflow的FormID
                            sqlTableColumn = "insert into workflow_form(WorkflowID,FormID,Creator,CreateDate,";
                            sqlTableColumnValue = "(" + TriggerWFID.ToString() + "," + FormID.ToString() + "," + WFTriggerCreatorID.ToString() + ",getdate(),";
                        }
                        else  //对明细字段复制
                        {
                            sqlTableColumn = "insert into workflow_formDetail(RequestID,GroupID,";
                            sqlTableColumnValue = "(" + TargetRequestID.ToString() + "," + TargetGroupID.ToString() + ",";

                            if (OPCycleType == 1) //循环执行
                            {
                                DataRow[] rows = dtMappingDetail.Select("SourceGroupID>0");
                                if (rows.Length > 0)
                                {
                                    SourceDetailGroupID = Convert.ToInt32(rows[0]["SourceGroupID"].ToString());
                                    //获取Source 的行数
                                }
                            }
                        }

                        if (OPCycleType == 0)  //只执行一次
                        {
                            DbParameter[] parmTarget = new DbParameter[dtMappingDetail.Rows.Count];

                            for (int j = 0; j < dtMappingDetail.Rows.Count; j++)
                            {
                                int TargetFieldID = Convert.ToInt32(dtMappingDetail.Rows[j]["TargetFieldID"].ToString());
                                int SourceGroupID = Convert.ToInt32(dtMappingDetail.Rows[j]["SourceGroupID"].ToString());
                                string SourceFieldName = dtMappingDetail.Rows[j]["SourceFieldName"].ToString();
                                int SourceFieldTypeID = Convert.ToInt32(dtMappingDetail.Rows[j]["SourceFieldTypeID"].ToString());

                                Workflow_FieldDictEntity targetFieldEntity = GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID.ToString());

                                if (SourceFieldTypeID == 1) //目标值来自主字段
                                {
                                    DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, SourceFieldName), 
                                           MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                       };

                                    string sqlFunction = "[sp_CaculateValueByFieldFunction]";
                                    ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction);
                                    string ParamValue = pramsFunction[2].Value.ToString();
                                    parmTarget[j] = MakeInParam("@" + targetFieldEntity.FieldName, (DbType)Enum.Parse(typeof(SqlDbType), targetFieldEntity.FieldDBType, true), GetSqlDbTypeLength(targetFieldEntity.FieldDBType), ParamValue);
                                }
                                else
                                {
                                    DbParameter[] pramsFunction = {
                                                                      MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                      MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, SourceGroupID),
                                                                      MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, SourceFieldName), 
                                                                      MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                                                  };

                                    //string sqlFunction = "select dbo.Fun_CaculateValueByAllLineDetailFieldFunction(@RequestID,@GroupID,@FieldFunction)";
                                    string sqlFunction = "sp_CaculateValueByAllLineDetailFieldFunction";
                                    ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                    string ParamValue = pramsFunction[3].Value.ToString();
                                    parmTarget[j] = MakeInParam("@" + targetFieldEntity.FieldName, (DbType)Enum.Parse(typeof(SqlDbType), targetFieldEntity.FieldDBType, true), GetSqlDbTypeLength(targetFieldEntity.FieldDBType), ParamValue);

                                }
                                sqlTableColumnValue = sqlTableColumnValue + "@" + targetFieldEntity.FieldName + ",";
                                sqlTableColumn = sqlTableColumn + targetFieldEntity.FieldName + ",";
                            }

                            sqlTableColumn = sqlTableColumn.Substring(0, sqlTableColumn.Length - 1) + ") values ";
                            sqlTableColumnValue = sqlTableColumnValue.Substring(0, sqlTableColumnValue.Length - 1) + ")";
                            if (TargetGroupID == 0)
                            {
                                string sqlInsert = sqlTableColumn + sqlTableColumnValue + "; select @@identity; ";
                                TargetRequestID = Convert.ToInt32(ExecuteScalar(CommandType.Text, sqlInsert, parmTarget));
                            }
                            else
                            {
                                string sqlInsert = sqlTableColumn + sqlTableColumnValue;
                                ExecuteNonQuery(CommandType.Text, sqlInsert, parmTarget);
                            }
                        }
                        else
                        {
                            //获取SourceDetailGroup
                            DbParameter[] paramSourceDetail = {
                                                                  MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                                                  MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, SourceDetailGroupID),                    
                                                              };


                            string sqlSourceDetail = "select * from workflow_FormDetail where RequestID=@RequestID and GroupID=@GroupID";
                            DataTable dtSourceDetail = ExecuteDataset(CommandType.Text, sqlSourceDetail, paramSourceDetail).Tables[0];
                            for (int ii = 0; ii < dtSourceDetail.Rows.Count; ii++)
                            {
                                int DetailID = Convert.ToInt32(dtSourceDetail.Rows[ii]["DetailID"].ToString());

                                DbParameter[] parmTarget = new DbParameter[dtMappingDetail.Rows.Count];

                                for (int j = 0; j < dtMappingDetail.Rows.Count; j++)
                                {
                                    int TargetFieldID = Convert.ToInt32(dtMappingDetail.Rows[j]["TargetFieldID"].ToString());
                                    int SourceGroupID = Convert.ToInt32(dtMappingDetail.Rows[j]["SourceGroupID"].ToString());
                                    string SourceFieldName = dtMappingDetail.Rows[j]["SourceFieldName"].ToString();
                                    int SourceFieldTypeID = Convert.ToInt32(dtMappingDetail.Rows[j]["SourceFieldTypeID"].ToString());

                                    Workflow_FieldDictEntity targetFieldEntity = GetWorkflow_FieldDictEntityByKeyCol(TargetFieldID.ToString());

                                    if (SourceFieldTypeID == 1) //目标值来自主字段
                                    {
                                        DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, SourceFieldName), 
                                           MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                       };

                                        //string sqlFunction = "select dbo.Fun_CaculateValueByFieldFunction(@RequestID,@FieldFunction)";
                                        string sqlFunction = "sp_CaculateValueByFieldFunction";
                                        ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                        string ParamValue = pramsFunction[2].Value.ToString();
                                        parmTarget[j] = MakeInParam("@" + targetFieldEntity.FieldName, (DbType)Enum.Parse(typeof(SqlDbType), targetFieldEntity.FieldDBType, true), GetSqlDbTypeLength(targetFieldEntity.FieldDBType), ParamValue);
                                    }
                                    else
                                    {
                                        DbParameter[] pramsFunction = {
                                           MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, RequestID),
                                           MakeInParam("@GroupID", (DbType)SqlDbType.Int   , 4, SourceGroupID),
                                           MakeInParam("@DetailID", (DbType)SqlDbType.Int   , 4, DetailID),
                                           MakeInParam("@FieldFunction", (DbType)SqlDbType.VarChar   , 2000, SourceFieldName), 
                                           MakeOutParam("@ReturnResult", (DbType)SqlDbType.VarChar   , 2000), 
                                           };

                                        //string sqlFunction = "select dbo.Fun_CaculateValueByOneLineDetailFieldFunction(@RequestID,@GroupID,@DetailID,@FieldFunction)";
                                        string sqlFunction = "sp_CaculateValueByOneLineDetailFieldFunction";
                                        ExecuteNonQuery(CommandType.StoredProcedure, sqlFunction, pramsFunction).ToString();
                                        string ParamValue = pramsFunction[4].Value.ToString();
                                        parmTarget[j] = MakeInParam("@" + targetFieldEntity.FieldName, (DbType)Enum.Parse(typeof(SqlDbType), targetFieldEntity.FieldDBType, true), GetSqlDbTypeLength(targetFieldEntity.FieldDBType), ParamValue);
                                    }
                                    sqlTableColumnValue = sqlTableColumnValue + "@" + targetFieldEntity.FieldName + ",";
                                    sqlTableColumn = sqlTableColumn + targetFieldEntity.FieldName + ",";
                                }
                                sqlTableColumn = sqlTableColumn.Substring(0, sqlTableColumn.Length - 1) + ") values ";
                                sqlTableColumnValue = sqlTableColumnValue.Substring(0, sqlTableColumnValue.Length - 1) + ")";
                                string sqlInsert = sqlTableColumn + sqlTableColumnValue;
                                ExecuteNonQuery(CommandType.Text, sqlInsert, parmTarget);
                            }
                        }
                    }

                    //复制完数据，写RequestBase
                    int TriggerWFCreateNodeID = GetCreateNodeIDbyWorkflowID(TriggerWFID);
                    DbParameter[] pramsRequestBase = {
                                                         MakeInParam("@RequestID", (DbType)SqlDbType.Int   , 4, TargetRequestID),
                                                         MakeInParam("@WorkflowID", (DbType)SqlDbType.Int, 4,TriggerWFID ),
                                                         MakeInParam("@NodeID", (DbType)SqlDbType.Int, 4,TriggerWFCreateNodeID),
                                                         MakeInParam("@NodeType", (DbType)SqlDbType.Int, 4,1 ),
                                                         MakeInParam("@OperatorID", (DbType)SqlDbType.Int, 4,WFTriggerCreatorID ),
                                                         MakeInParam("@Creator", (DbType)SqlDbType.Int, 4,WFTriggerCreatorID ),
                                                       };
                    string sqlRequestBase = "sp_InsertIntoWorkflow_RequestBase";
                    ExecuteNonQuery(CommandType.StoredProcedure, sqlRequestBase, pramsRequestBase);
                }
                catch (Exception err)
                {
                    throw new Exception("触发流程发生错误" + err.Message);
                }
                return "0";
            }
            else
            {
                return "0";
            }
        }

        #endregion

        #region "表单基础"

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="RequestID"></param>
        /// <returns></returns>
        public Workflow_RequestBaseEntity GetWorkflow_RequestBaseEntityByKeyCol(string RequestID)
        {
            string sql = "select * from Workflow_RequestBase RequestID Where RequestID=@RequestID";
            DbParameter[] pramsGet = {
									   MakeInParam("@RequestID",(DbType)SqlDbType.Int,4,RequestID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_RequestBaseFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
        private Workflow_RequestBaseEntity GetWorkflow_RequestBaseFromIDataReader(DbDataReader dr)
        {
            Workflow_RequestBaseEntity dt = new Workflow_RequestBaseEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["RequestID"].ToString() != "" || dr["RequestID"] != null) dt.RequestID = Int32.Parse(dr["RequestID"].ToString());
                if (dr["WorkflowID"].ToString() != "" || dr["WorkflowID"] != null) dt.WorkflowID = Int32.Parse(dr["WorkflowID"].ToString());
                if (dr["LastNodeID"].ToString() != "" || dr["LastNodeID"] != null) dt.LastNodeID = Int32.Parse(dr["LastNodeID"].ToString());
                dt.LastRuleID = dr["LastRuleID"].ToString();
                dt.LastRuleType = dr["LastRuleType"].ToString();
                if (dr["LastNodeType"].ToString() != "" || dr["LastNodeType"] != null) dt.LastNodeType = Int32.Parse(dr["LastNodeType"].ToString());
                if (dr["LastDeptLevel"].ToString() != "" || dr["LastDeptLevel"] != null) dt.LastDeptLevel = Int32.Parse(dr["LastDeptLevel"].ToString());
                dt.LastOperatorID = dr["LastOperatorID"].ToString();
                if (dr["CurrentNodeID"].ToString() != "" || dr["CurrentNodeID"] != null) dt.CurrentNodeID = Int32.Parse(dr["CurrentNodeID"].ToString());
                dt.CurrentRuleID = dr["CurrentRuleID"].ToString();
                dt.CurrentRuleType = dr["CurrentRuleType"].ToString();
                if (dr["CurrentNodeType"].ToString() != "" || dr["CurrentNodeType"] != null) dt.CurrentNodeType = Int32.Parse(dr["CurrentNodeType"].ToString());
                if (dr["CurrentDeptLevel"].ToString() != "" || dr["CurrentDeptLevel"] != null) dt.CurrentDeptLevel = Int32.Parse(dr["CurrentDeptLevel"].ToString());
                dt.CurrentOperatorID = dr["CurrentOperatorID"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                if (dr["Creator"].ToString() != "" || dr["Creator"] != null) dt.Creator = Int32.Parse(dr["Creator"].ToString());
                dt.RequestStatus = dr["RequestStatus"].ToString();
                dt.RequestName = dr["RequestName"].ToString();
                if (dr["IsCancel"].ToString() != "" || dr["IsCancel"] != null) dt.IsCancel = Int32.Parse(dr["IsCancel"].ToString());
                dt.AllParticipator = dr["AllParticipator"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }


        #endregion

        #region "基本验证方式"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_Workflow_BasicValidTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddWorkflow_BasicValidType(Workflow_BasicValidTypeEntity _Workflow_BasicValidTypeEntity)
        {
            DbParameter[] pramsInsert = {
                                             MakeInParam("@ValidTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidTypeDesc ),
                                             MakeInParam("@ValidErrorMsg",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidErrorMsg ),
                                             MakeInParam("@ValidRule",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidRule )
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Workflow_BasicValidType]");
            sb.Append("(");
            sb.Append("[ValidTypeDesc]");
            sb.Append(",[ValidErrorMsg]");
            sb.Append(",[ValidRule]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@ValidTypeDesc,");
            sb.Append("@ValidErrorMsg,");
            sb.Append("@ValidRule);");
            sb.Append("select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_Workflow_BasicValidTypeEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateWorkflow_BasicValidType(Workflow_BasicValidTypeEntity _Workflow_BasicValidTypeEntity)
        {
            DbParameter[] pramsUpdate = {
                                            MakeInParam("@ValidTypeDesc",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidTypeDesc ),
                                            MakeInParam("@ValidErrorMsg",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidErrorMsg ),
                                            MakeInParam("@ValidRule",(DbType)SqlDbType.VarChar,200,_Workflow_BasicValidTypeEntity.ValidRule ),
                                            MakeInParam("@ValidTypeID",(DbType)SqlDbType.Int,4,_Workflow_BasicValidTypeEntity.ValidTypeID ) 
                                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[Workflow_BasicValidType]");
            sb.Append(" set ");
            sb.Append(" [ValidTypeDesc]=@ValidTypeDesc,");
            sb.Append(" [ValidErrorMsg]=@ValidErrorMsg,");
            sb.Append(" [ValidRule]=@ValidRule");
            sb.Append(" where [ValidTypeID]=@ValidTypeID;select @@rowcount");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>        /// 如果未被其他数据集引用,可以删除,成功返回0,不成功返回-1;如已被收用,返回数据集ID        /// </summary>
        /// <param name="ValidTypeID"> </param>
        /// <returns></returns>
        public int DeleteWorkflow_BasicValidType(int ValidTypeID)
        {
            //先判断是否已被引用,有,则不能删除;无,可删除


            DbParameter[] parms = { MakeInParam("@ValidTypeID", (DbType)SqlDbType.Int, 4, ValidTypeID) };
            StringBuilder strb = new StringBuilder();
            strb.Append(" DELETE FROM  [dbo].[Workflow_BasicValidType] ");
            strb.Append(" WHERE ValidTypeID=@ValidTypeID");
            return Convert.ToInt32(ExecuteScalar(CommandType.Text, strb.ToString(), parms));
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="ValidTypeID"></param>
        /// <returns></returns>
        public Workflow_BasicValidTypeEntity GetWorkflow_BasicValidTypeEntityByKeyCol(string ValidTypeID)
        {
            string sql = "select * from Workflow_BasicValidType Where ValidTypeID=@ValidTypeID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@ValidTypeID",(DbType)SqlDbType.Int,4,ValidTypeID) 
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetWorkflow_BasicValidTypeFromIDataReader(sr);
                }
                throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        private Workflow_BasicValidTypeEntity GetWorkflow_BasicValidTypeFromIDataReader(DbDataReader dr)
        {
            Workflow_BasicValidTypeEntity dt = new Workflow_BasicValidTypeEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["ValidTypeID"].ToString() != "" || dr["ValidTypeID"] != null) dt.ValidTypeID = Int32.Parse(dr["ValidTypeID"].ToString());
                dt.ValidTypeDesc = dr["ValidTypeDesc"].ToString();
                dt.ValidErrorMsg = dr["ValidErrorMsg"].ToString();
                dt.ValidRule = (dr["ValidRule"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        #endregion

        #region "流程申请"

        /// <summary>
        /// 根据参数列表来获取数据集
        /// </summary>
        /// <param name="DataSetID"></param>
        /// <param name="dtParameters"></param>
        /// <returns></returns>
        public DataTable GetDataSetTable(int DataSetID, DataTable dtParameter)
        {
            //根据数据集来抓数据
            Workflow_DataSetEntity DataSetEntity = GetWorkflow_DataSetEntityByKeyCol(DataSetID.ToString());
            int DataSoureID = DataSetEntity.DataSourceID;
            int DataSetType = DataSetEntity.DataSetType;
            string QueryString = DataSetEntity.QuerySql;
            //再获得DataSource信息
            Workflow_DataSourceEntity _DSE = GetDataSourceByID(DataSoureID);
            DbParameter[] prams = new DbParameter[dtParameter.Rows.Count];
            if (_DSE.DataSourceDBType == "Oracle")
            {
                for (int j = 0; j < dtParameter.Rows.Count; j++)
                {
                    if (dtParameter.Rows[j]["ParameterDirection"].ToString().ToUpper() == "OUTPUT")
                        prams[j] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeOutParam(dtParameter.Rows[j]["ParameterName"].ToString(), (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), dtParameter.Rows[j]["ParameterType"].ToString(), true), Convert.ToInt32(dtParameter.Rows[j]["ParameterSize"].ToString()));
                    else
                        prams[j] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(dtParameter.Rows[j]["ParameterName"].ToString(), (DbType)Enum.Parse(typeof(System.Data.OracleClient.OracleType), dtParameter.Rows[j]["ParameterType"].ToString(), true), Convert.ToInt32(dtParameter.Rows[j]["ParameterSize"].ToString()), dtParameter.Rows[j]["ParameterValue"].ToString());
                }
            }
            else
            {
                for (int j = 0; j < dtParameter.Rows.Count; j++)
                {
                    if (dtParameter.Rows[j]["ParameterDirection"].ToString().ToUpper() == "OUTPUT")
                        prams[j] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeOutParam(dtParameter.Rows[j]["ParameterName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtParameter.Rows[j]["ParameterType"].ToString(), true), Convert.ToInt32(dtParameter.Rows[j]["ParameterSize"].ToString()));
                    else
                        prams[j] = GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).MakeInParam(dtParameter.Rows[j]["ParameterName"].ToString(), (DbType)Enum.Parse(typeof(SqlDbType), dtParameter.Rows[j]["ParameterType"].ToString(), true), Convert.ToInt32(dtParameter.Rows[j]["ParameterSize"].ToString()), dtParameter.Rows[j]["ParameterValue"].ToString());
                }
            }
            if (DataSetType == 1)
            {
                return GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.Text, QueryString, prams).Tables[0];
            }
            else
            {
                return GetInstance(_DSE.DataSourceDBType, _DSE.ConnectString).ExecuteDataset(CommandType.StoredProcedure, QueryString, prams).Tables[0];
            }
        }

        /// <summary>
        /// 抓取明细字段与DataSet的字段对应
        /// </summary>
        /// <param name="FormID"></param>
        /// <param name="FieldID"></param>
        /// <returns></returns>
        public DataTable GetGroupLineFieldMap(int FormID, int FieldID)
        {
            DbParameter[] prams = {
                                       MakeInParam("@FormID", (DbType)SqlDbType.Int , 4, FormID),
                                       MakeInParam("@FieldID", (DbType)SqlDbType.Int , 4, FieldID),                                        
                                   };
            string sql = "sp_GetGroupLineFieldMapping";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        #endregion
    }
}
