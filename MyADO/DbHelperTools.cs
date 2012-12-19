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
        #region "工具信息"

        /// <summary>
        /// 通过代码返回该表当前编号
        /// 使用方法：在存储过程中使用，
        ///		declare @ReturnCode varchar(50)
        ///		exec px_Sequence @ReturnCode output, 'PEEBICODE'
        ///		print @ReturnCode
        ///           在程序中使用，
        ///		string px_Sequence(string CODE);
        /// 逻辑:
        ///     前缀+日期类型+编号长度，不足补0
        /// </summary>
        /// <param name="CODE">PSACN 表中PSACNCO中的值</param>
        /// <param name="Type">Type 是返回前部分还是一个ID号 1 是真实的ID号，0为前缀 2为真实ID，</param>
        /// <returns>返回编号/前缀的一部分</returns>
        public string px_Sequence(string CODE, string Type)
        {

            DbParameter[] prams = {
									  MakeInParam("@CODE", (DbType)SqlDbType.VarChar, 50, CODE),
									  MakeOutParam("@ReturnCode", (DbType)SqlDbType.VarChar, 50),
                                      MakeInParam("@Type", (DbType)SqlDbType.Char,1, Type),
                                  };
            string sql = "px_Sequence";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
            string strResult = prams[1].Value.ToString();
            return strResult;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PSSMEUS">是否有效</param>
        /// <param name="PSSMEPMC">上级模块编号</param>
        /// <returns></returns>
        public DataTable GetPSSMEInfo(string PSSMEUS, string PSSMEPMC)
        {

            DbParameter[] prams = {
									  MakeInParam("@PSSMEUS", (DbType)SqlDbType.VarChar, 50, PSSMEUS),
									  MakeInParam("@PSSMEPMC", (DbType)SqlDbType.VarChar, 50, PSSMEPMC)
								   };
            string sql = "";
            if (PSSMEUS == "")
                sql = "select * from [PSSME] Order by PSSMEMC,PSSMEOR ";
            else
                sql = "select * from [PSSME] where [PSSMEUS] =@PSSMEUS and PSSMEPMC like @PSSMEPMC +'%' Order by PSSMEMC,PSSMEOR";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PSSMEUS">是否有效</param>
        /// <param name="PSSMEPMC">上级模块编号</param>
        /// <returns></returns>
        public DataTable GetPSSMEInfoOnlyNext(string PSSMEUS, string PSSMEPMC)
        {

            DbParameter[] prams = {
									  MakeInParam("@PSSMEUS", (DbType)SqlDbType.VarChar, 50, PSSMEUS),
									  MakeInParam("@PSSMEPMC", (DbType)SqlDbType.VarChar, 50, PSSMEPMC)
								   };
            string sql = "";
            if (PSSMEUS == "")
                sql = "select * from [PSSME] Order by PSSMEMC,PSSMEOR ";
            else
                sql = "select * from [PSSME] where [PSSMEUS] =@PSSMEUS and PSSMEPMC like @PSSMEPMC Order by PSSMEMC,PSSMEOR";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 通过员工账号去判断用户权限,然后得到有节点权限的表;
        /// 存储过程:GetPSSMEInfoByPBACCID
        /// add by :Viliant.luo
        /// </summary>
        /// <param name="PBACCID">员工编号</param>
        /// <param name="PSSMEPMC">上级模块编号,根级为空</param>
        /// <returns></returns>
        public DataTable GetPSSMEInfoByPBACCID(string PBACCID, string PSSMEPMC)
        {

            DbParameter[] prams = {
									  MakeInParam("@PBACCID", (DbType)SqlDbType.VarChar, 50, PBACCID),
									  MakeInParam("@PSSMEPMC", (DbType)SqlDbType.VarChar, 50, PSSMEPMC)
								   };
            string sql = "GetPSSMEInfoByPBACCID";

            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获得单个菜单节点的信息
        /// </summary>
        /// <param name="PSSMEPMC">模块号</param>
        /// <returns></returns>
        public PSSMETable GetPSSMEInfo(string PSSMEPMC)
        {

            DbParameter[] prams = {
                                       MakeInParam("@PSSMEMC", (DbType)SqlDbType.VarChar, 50, PSSMEPMC),
								   };
            string sql = "select * from [PSSME] where [PSSMEMC] =@PSSMEMC ";

            return GetPSSMEInfoFromIDataReader(ExecuteReader(CommandType.Text, sql, prams));
        }

        private PSSMETable GetPSSMEInfoFromIDataReader(IDataReader dr)
        {
            PSSMETable dt = new PSSMETable();
            if (dr.Read())
            {
                dt.MenuDescription = dr["PSSMEMD"].ToString();
                dt.MenuImageUrl = dr["PSSMEUIP"].ToString();
                dt.MenuIsValid = dr["PSSMEUS"].ToString();
                dt.MenuLink = dr["PSSMEMP"].ToString();
                dt.MenuLinkTarget = dr["PSSMEOWT"].ToString();
                dt.MenuMouseDownCss = dr["PSSMEMSS"].ToString();
                dt.MenuMouseLeaveCss = dr["PSSMEMOS"].ToString();
                dt.MenuMouseOverCss = dr["PSSMEMVS"].ToString();
                dt.MenuName = dr["PSSMEMN"].ToString();
                dt.ModleCode = dr["PSSMEMC"].ToString();
                dt.ModleCodeList = dr["PSSMEMLST"].ToString();
                dt.ModleID = Int32.Parse(dr["PSSMEID"].ToString());
                dt.ModleParentCode = dr["PSSMEPMC"].ToString();
                dt.MouldIDList = dr["PSSMEILST"].ToString();
                dt.OrderBy = Int32.Parse(dr["PSSMEOR"].ToString());
                dt.MouldNameEn = dr["PSSMEMNEN"].ToString();
                dt.MouldNameTW = dr["PSSMEMNTW"].ToString();
                dt.IsProcess = dr["PSSMEPRO"].ToString();
                if (dr["PSSMESOID"].ToString() != "") dt.AllOrderBy = Int32.Parse(dr["PSSMESOID"].ToString());
                dt.AllOrderByList = dr["PSSMEOLST"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }
        /// <summary>
        /// 增加节点菜单
        /// </summary>
        /// <param name="_PSSMETable"></param>
        /// <returns>-1;//该节点代码已经存在;</returns>
        public int CreatePSSME(PSSMETable _PSSMETable)
        {
            //判断是否存在该节点代码
            DbParameter[] prams = {
		                           MakeInParam("@PSSMEPMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleCode),
								   };
            string sql = "";

            sql = "select * from [PSSME] where  PSSMEPMC = @PSSMEPMC ";

            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return -1;//该节点代码已经存在
            }
            else
            {
                DbParameter[] prams2 = {
                                   MakeInParam("@PSSMEMD",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuDescription),
                                   MakeInParam("@PSSMEUIP",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuImageUrl),
                                   MakeInParam("@PSSMEUS",(DbType)SqlDbType.Char,1,_PSSMETable.MenuIsValid),
                                   MakeInParam("@PSSMEMP",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuLink),
                                   MakeInParam("@PSSMEOWT",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuLinkTarget),
                                   MakeInParam("@PSSMEMSS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseDownCss),
                                   MakeInParam("@PSSMEMOS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseLeaveCss),
                                   MakeInParam("@PSSMEMVS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseOverCss),
                                   MakeInParam("@PSSMEMN",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuName),
                                   MakeInParam("@PSSMEMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleCode),
                                   MakeInParam("@PSSMEMLST",(DbType)SqlDbType.VarChar,200,_PSSMETable.ModleCodeList),
                                   MakeInParam("@PSSMEID",(DbType)SqlDbType.Int,4,_PSSMETable.ModleID),
                                   MakeInParam("@PSSMEPMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleParentCode),
                                   MakeInParam("@PSSMEILST",(DbType)SqlDbType.VarChar,100,_PSSMETable.MouldIDList),
                                   MakeInParam("@PSSMEOR",(DbType)SqlDbType.Int,4,_PSSMETable.OrderBy),
                                   MakeInParam("@PSSMEMNEN",(DbType)SqlDbType.VarChar,100,_PSSMETable.MouldNameEn),
                                   MakeInParam("@PSSMEMNTW",(DbType)SqlDbType.VarChar,50,_PSSMETable.MouldNameTW),
                                   MakeInParam("@PSSMEPRO",(DbType)SqlDbType.Char,1,_PSSMETable.IsProcess),
                                       };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PSSME]");
                sb.Append("([PSSMEMC]");
                sb.Append(",[PSSMEMN]");
                sb.Append(",[PSSMEMD]");
                sb.Append(",[PSSMEMP]");
                sb.Append(",[PSSMEPMC]");
                sb.Append(",[PSSMEUS]");
                sb.Append(",[PSSMEMVS]");
                sb.Append(",[PSSMEMOS]");
                sb.Append(",[PSSMEMSS]");
                sb.Append(",[PSSMEOWT]");
                sb.Append(",[PSSMEUIP]");
                sb.Append(",[PSSMEILST]");
                sb.Append(",[PSSMEOR]");
                sb.Append(",[PSSMEMNEN]");
                sb.Append(",[PSSMEMNTW]");
                sb.Append(",[PSSMEPRO]");
                sb.Append(",[PSSMEMLST])");
                sb.Append("VALUES");
                sb.Append("(@PSSMEMC,");
                sb.Append("@PSSMEMN,");
                sb.Append("@PSSMEMD, ");
                sb.Append("@PSSMEMP,");
                sb.Append("@PSSMEPMC,");
                sb.Append("@PSSMEUS, ");
                sb.Append("@PSSMEMVS,");
                sb.Append("@PSSMEMOS,");
                sb.Append("@PSSMEMSS,");
                sb.Append("@PSSMEOWT,");
                sb.Append("@PSSMEUIP,");
                sb.Append("@PSSMEILST, ");
                sb.Append("@PSSMEOR, ");
                sb.Append("@PSSMEMNEN,");
                sb.Append("@PSSMEMNTW, ");
                sb.Append("@PSSMEPRO, ");
                sb.Append("@PSSMEMLST);");
                sb.Append("select @@identity;");

                int MoudleID = Utils.StrToInt(ExecuteScalar(CommandType.Text, sb.ToString(), prams2), -1);
                //插入成功后，要把该节点code追加到其上级菜单的记录数组当中去

                if (_PSSMETable.ModleParentCode != "")//非第一级菜单,,
                {
                    DbParameter[] prams3 = {
									  MakeInParam("@PSSMEILST",(DbType)SqlDbType.VarChar,10,MoudleID.ToString()),
                                      MakeInParam("@PSSMEMLST",(DbType)SqlDbType.VarChar,10,_PSSMETable.ModleCode.ToString()),
                                      MakeInParam("@PSSMEMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleParentCode),
                        };
                    sql = "update [dbo].[PSSME] set PSSMEILST=isnull(PSSMEILST,'')+RTRIM(@PSSMEILST)+',',PSSMEMLST=isnull(PSSMEMLST,'')+RTRIM(@PSSMEMLST)+',' where PSSMEMC=@PSSMEMC";

                    ExecuteNonQuery(CommandType.Text, sql, prams3);
                    string LENPSSMEPMC = "";
                    //获取同级数据且按先代码排序/然后顺序排序.  然后把所有同级记录进行更新一次，也就是为了配合界面上的CSS显示问题
                    for (int i = 0; i < _PSSMETable.ModleCode.ToString().Trim().Length; i++)
                    {
                        LENPSSMEPMC = LENPSSMEPMC + "_";
                    }
                    sql = "select * from [PSSME] where  PSSMEMC like '" + LENPSSMEPMC + "'";

                    IDataReader dr = ExecuteReader(CommandType.Text, sql);
                    int j = 1;
                    while (dr.Read())
                    {
                        sql = "update [PSSME] set PSSMESOID=" + j + " where PSSMEMC = '" + dr["PSSMEMC"].ToString() + "'";
                        ExecuteNonQuery(CommandType.Text, sql);
                        j++;
                    }
                    //然后还要把该更新后的顺序号告诉给其上级，获取上级数据,
                    LENPSSMEPMC = "";
                    for (int i = 0; i < _PSSMETable.ModleParentCode.ToString().Trim().Length; i++)
                    {
                        LENPSSMEPMC = LENPSSMEPMC + "_";
                    }
                    sql = "select * from [PSSME] where  PSSMEMC like '" + LENPSSMEPMC + "'";
                    dr = ExecuteReader(CommandType.Text, sql);
                    while (dr.Read())
                    {
                        //获取到的上级，然后又去得到它的所有下级目录。
                        sql = "select * from [PSSME] where  PSSMEPMC like '" + dr["PSSMEMC"].ToString() + "' order by PSSMESOID";
                        IDataReader dr2 = ExecuteReader(CommandType.Text, sql);
                        string strPSSMEILST = "";
                        while (dr2.Read())
                        {
                            strPSSMEILST = strPSSMEILST + dr2["PSSMESOID"].ToString().Trim() + ",";
                        }
                        dr2.Close();
                        //再循环地把他们的CSS使用的ID给自己的ID集合 
                        if (strPSSMEILST.Length > 0)
                        {
                            sql = "update [PSSME] set PSSMEOLST='" + strPSSMEILST.Substring(0, strPSSMEILST.Length - 1) + "' where PSSMEMC = '" + dr["PSSMEMC"].ToString() + "'";
                            ExecuteNonQuery(CommandType.Text, sql);
                        }
                    }
                    dr.Close();
                }
                return MoudleID;
            }
        }

        /// <summary>
        /// 修改节点菜单
        /// </summary>
        /// <param name="_PSSMETable"></param>
        /// <returns>-1;//该节点代码已经存在;</returns>
        public int UpdatePSSME(PSSMETable _PSSMETable)
        {
            DbParameter[] prams2 = {
                               MakeInParam("@PSSMEMD",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuDescription),
                               MakeInParam("@PSSMEUIP",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuImageUrl),
                               MakeInParam("@PSSMEUS",(DbType)SqlDbType.Char,1,_PSSMETable.MenuIsValid),
                               MakeInParam("@PSSMEMP",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuLink),
                               MakeInParam("@PSSMEOWT",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuLinkTarget),
                               MakeInParam("@PSSMEMSS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseDownCss),
                               MakeInParam("@PSSMEMOS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseLeaveCss),
                               MakeInParam("@PSSMEMVS",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuMouseOverCss),
                               MakeInParam("@PSSMEMN",(DbType)SqlDbType.VarChar,50,_PSSMETable.MenuName),
                               MakeInParam("@PSSMEMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleCode),
                               MakeInParam("@PSSMEMLST",(DbType)SqlDbType.VarChar,200,_PSSMETable.ModleCodeList),
                               MakeInParam("@PSSMEID",(DbType)SqlDbType.Int,4,_PSSMETable.ModleID),
                               MakeInParam("@PSSMEPMC",(DbType)SqlDbType.VarChar,50,_PSSMETable.ModleParentCode),
                               MakeInParam("@PSSMEILST",(DbType)SqlDbType.VarChar,100,_PSSMETable.MouldIDList),
                               MakeInParam("@PSSMEOR",(DbType)SqlDbType.Int,4,_PSSMETable.OrderBy),
                               MakeInParam("@PSSMEMNEN",(DbType)SqlDbType.VarChar,100,_PSSMETable.MouldNameEn),
                               MakeInParam("@PSSMEMNTW",(DbType)SqlDbType.VarChar,50,_PSSMETable.MouldNameTW),
                               MakeInParam("@PSSMEPRO",(DbType)SqlDbType.Char,1,_PSSMETable.IsProcess),
                                       };
            string sql = "UpdatePSSME";
            int MoudleID = Utils.StrToInt(ExecuteScalar(CommandType.StoredProcedure, sql, prams2), -1);
            return MoudleID;
        }

        /// <summary>
        /// 为UI显示服务，系统设置中设置每个表中列是否可见。
        /// UserID是预留，如果要求可以为每一个人设置不同的界面时使用
        /// </summary>
        /// <param name="TableName">真实的数据库表名称</param>
        /// <param name="UserID">用户表中用户关键字,ID</param>
        /// <returns></returns>
        public DataTable GetSysTableColumnByTableName(string TableName, string UserID , string flag)
        {

            DbParameter[] prams = {
									  MakeInParam("@TableName", (DbType)SqlDbType.VarChar, 50, TableName),
                                      MakeInParam("@UserID", (DbType)SqlDbType.VarChar, 50, UserID),
                                      MakeInParam("@ColIsShow", (DbType)SqlDbType.Char, 2, flag),
                                  };
            string sql = "GetSysTableColumnByTableName";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }




        /// <summary>
        /// 增加节点菜单
        /// </summary>
        /// <param name="_PSSMECO"></param>
        /// <returns>-1;//该节点代码已经存在;</returns>
        public int DeletePSSME(string _PSSMECO)
        {

            string sql = "";
            sql = "select * from [PSSME] where  PSSMEMC like @PSSMECO + '__'  ";
            DbParameter[] prams = {
                                     MakeInParam("@PSSMECO",(DbType)SqlDbType.VarChar,50,_PSSMECO ),
                                  };
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return -1;//该节点代码已经存在
            }
            else
            {
                sql = "delete from PSSME where PSSMEMC =@PSSMECO";
                DbParameter[] pramsDelete = {
                                               MakeInParam("@PSSMECO",(DbType)SqlDbType.VarChar,50,_PSSMECO ),
                                            };
                ExecuteNonQuery(CommandType.Text, sql, pramsDelete);


                return Convert.ToInt32(_PSSMECO);
            }
        }

        ///<summary>
        /// 返回系统中所有表名
        /// </summary>
        /// <param name="mMeno">备注，还没有实际用</param>
        /// <returns></returns>
        public DataTable GetSysTable(string mMeno)
        {
            string sql = "select * from sys_TableColumn Order by TableName";
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        // add Rio
        //根据tablename返回表名
        public DataTable GetSysTableByTableName(string mMeno)
        {
            string sql = string.Format("select * from sys_TableColumn where TableName='{0}'",mMeno);
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        
        }

        public DataTable GetSysTable()
        {
            string sql = "select * from sys_TableColumn Order by TableName";
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
        /// <summary>
        /// SysTableColumn表记录的插入。
        /// 列名和表名如果存在，则修改
        /// 否则新加
        /// </summary>
        /// <param name="sys"></param>
        public void InsertUpdateSysTable(SysTableColumnEntity sys)
        {
            DbParameter[] prams = {
                           MakeInParam("@ColDescriptionCN",(DbType)SqlDbType.VarChar,50,sys.ColDescriptionCN),
                           MakeInParam("@ColDescriptionEN",(DbType)SqlDbType.VarChar,50,sys.ColDescriptionEN),
                           MakeInParam("@ColDescriptionTW",(DbType)SqlDbType.VarChar,50,sys.ColDescriptionTW),
                           MakeInParam("@ColIsShow",(DbType)SqlDbType.Char,1,sys.ColIsShow),
                           MakeInParam("@ColMatchTable",(DbType)SqlDbType.VarChar,50,sys.ColMatchTable),
                           MakeInParam("@ColName",(DbType)SqlDbType.VarChar,50,sys.ColName),
                           MakeInParam("@ColShowOrder",(DbType)SqlDbType.Int,4,sys.ColShowOrder),
                           MakeInParam("@ColType",(DbType)SqlDbType.VarChar,50,sys.ColType),
                           MakeInParam("@ColWidth",(DbType)SqlDbType.Int,4,sys.ColWidth),
                           MakeInParam("@IsKey",(DbType)SqlDbType.Char,1,sys.IsKey),
                           MakeInParam("@IsSearchCondi",(DbType)SqlDbType.Char,1,sys.IsSearchCondi),
                           MakeInParam("@MatchTableColTextCN",(DbType)SqlDbType.VarChar,50,sys.MatchTableColTextCN),
                           MakeInParam("@MatchTableColTextEN",(DbType)SqlDbType.VarChar,50,sys.MatchTableColTextEN),
                           MakeInParam("@MatchTableColTextTW",(DbType)SqlDbType.VarChar,100,sys.MatchTableColTextTW),
                           MakeInParam("@MatchTableColValue",(DbType)SqlDbType.VarChar,50,sys.MatchTableColValue),
                           MakeInParam("@MatchTableSqlText",(DbType)SqlDbType.VarChar,100,sys.MatchTableSqlText),
                           MakeInParam("@MatchTableTree",(DbType)SqlDbType.VarChar,50,sys.MatchTableTree),
                           MakeInParam("@TableName",(DbType)SqlDbType.VarChar,50,sys.TableName),
                           MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,sys.UserID),
                                  };
            string sql = "InsUpd_sys_TableColumn";
            ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
        }

        #region 自动编号
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_PSACNEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddPSACN(PSACNEntity _PSACNEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = {MakeInParam("@PSACNCO",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNCO),
                                     };
            string sql = "select * from PSACN where PSACNCO=@PSACNCO";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                DbParameter[] pramsInsert = {
									  MakeInParam("@PSACNCO",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNCO ),
									  MakeInParam("@PSACNNCN",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNCN ),
									  MakeInParam("@PSACNNEN",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNEN ),
									  MakeInParam("@PSACNNTW",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNTW ),
									  MakeInParam("@PSACNPRE",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNPRE ),
									  MakeInParam("@PSACNDAT",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNDAT ),
									  MakeInParam("@PSACNLEN",(DbType)SqlDbType.Int,4,_PSACNEntity.PSACNLEN ),
									  MakeInParam("@PSACNCUR",(DbType)SqlDbType.Int,4,_PSACNEntity.PSACNCUR ),
									  MakeInParam("@PSACNPDA",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNPDA ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PSACN]");
                sb.Append("(");
                sb.Append("[PSACNCO]");
                sb.Append(",[PSACNNCN]");
                sb.Append(",[PSACNNEN]");
                sb.Append(",[PSACNNTW]");
                sb.Append(",[PSACNPRE]");
                sb.Append(",[PSACNDAT]");
                sb.Append(",[PSACNLEN]");
                sb.Append(",[PSACNCUR]");
                sb.Append(",[PSACNPDA]");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("(@PSACNCO,");
                sb.Append("@PSACNNCN,");
                sb.Append("@PSACNNEN,");
                sb.Append("@PSACNNTW,");
                sb.Append("@PSACNPRE,");
                sb.Append("@PSACNDAT,");
                sb.Append("@PSACNLEN,");
                sb.Append("@PSACNCUR,");
                sb.Append("@PSACNPDA)");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }
        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_PSACNEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdatePSACN(PSACNEntity _PSACNEntity)
        {
            DbParameter[] pramsUpdate = {
									  MakeInParam("@PSACNCO",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNCO ),
									  MakeInParam("@PSACNNCN",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNCN ),
									  MakeInParam("@PSACNNEN",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNEN ),
									  MakeInParam("@PSACNNTW",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNNTW ),
									  MakeInParam("@PSACNPRE",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNPRE ),
									  MakeInParam("@PSACNDAT",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNDAT ),
									  MakeInParam("@PSACNLEN",(DbType)SqlDbType.Int,4,_PSACNEntity.PSACNLEN ),
									  MakeInParam("@PSACNCUR",(DbType)SqlDbType.Int,4,_PSACNEntity.PSACNCUR ),
									  MakeInParam("@PSACNPDA",(DbType)SqlDbType.VarChar,50,_PSACNEntity.PSACNPDA ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[PSACN]");
            sb.Append(" set ");
            sb.Append(" [PSACNNCN]=@PSACNNCN,");
            sb.Append(" [PSACNNEN]=@PSACNNEN,");
            sb.Append(" [PSACNNTW]=@PSACNNTW,");
            sb.Append(" [PSACNPRE]=@PSACNPRE,");
            sb.Append(" [PSACNDAT]=@PSACNDAT,");
            sb.Append(" [PSACNLEN]=@PSACNLEN,");
            sb.Append(" [PSACNCUR]=@PSACNCUR,");
            sb.Append(" [PSACNPDA]=@PSACNPDA");
            sb.Append(" where [PSACNCO]=@PSACNCO");
            sb.Append(" select @PSACNCO");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate).ToString();

        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public PSACNEntity GetPSACNEntityByKeyCol(string KeyCol)
        {
            string sql = "select * from PSACN Where PSACNCO=@KeyCol";

            DbParameter[] pramsGet = {
                                         MakeInParam("@KeyCol",(DbType)SqlDbType.VarChar,50,KeyCol ),
                                     };
            DbDataReader sr = null;

            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);

                if (sr.Read())
                {
                    return GetPSACNFromIDataReader(sr);
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

        private PSACNEntity GetPSACNFromIDataReader(DbDataReader dr)
        {
            PSACNEntity dt = new PSACNEntity();
            if (dr.FieldCount > 0)
            {
                dt.PSACNCO = dr["PSACNCO"].ToString();
                dt.PSACNNCN = dr["PSACNNCN"].ToString();
                dt.PSACNNEN = dr["PSACNNEN"].ToString();
                dt.PSACNNTW = dr["PSACNNTW"].ToString();
                dt.PSACNPRE = dr["PSACNPRE"].ToString();
                dt.PSACNDAT = dr["PSACNDAT"].ToString();
                if (dr["PSACNLEN"].ToString() != "" || dr["PSACNLEN"] != null) dt.PSACNLEN = Int32.Parse(dr["PSACNLEN"].ToString());
                if (dr["PSACNCUR"].ToString() != "" || dr["PSACNCUR"] != null) dt.PSACNCUR = Int32.Parse(dr["PSACNCUR"].ToString());
                dt.PSACNPDA = dr["PSACNPDA"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PSACNCOCollection"></param>
        /// <returns></returns>
        public string DeletePSACN(string PSACNCOCollection)
        {
            string sql = "delete from PASCN where PSACNCO in (@PSACNCOCollection)";
            DbParameter[] pramsDelete = {
									  MakeInParam("@PSACNCOCollection",(DbType)SqlDbType.VarChar,1000,PSACNCOCollection ),
                                         };
            ExecuteNonQuery(CommandType.Text, sql, pramsDelete);
            return "0";
        }
        #endregion

        #endregion

        #region "保存中国银行汇率"

        public string AddExchangeRate(string BasicCurrency,string ExchangeCurrency,string ExchangeRate)
        {
           
                DbParameter[] pramsInsert = {
									   MakeInParam("@BasicCurrency",(DbType)SqlDbType.VarChar,50,BasicCurrency ),
									   MakeInParam("@ExchangeCurrency",(DbType)SqlDbType.VarChar,50,ExchangeCurrency ),
									   MakeInParam("@ExchangeRate",(DbType)SqlDbType.Float ,38,ExchangeRate ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime  ,23,System.DateTime.Now ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[ChinaBankExchangeRate]");
                sb.Append("(");
                sb.Append("[BasicCurrency]");
                sb.Append(",[ExchangeCurrency]");
                sb.Append(",[ExchangeRate]");
                sb.Append(",[CreateDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@BasicCurrency,");
                sb.Append("@ExchangeCurrency,");
                sb.Append("@ExchangeRate,");
                sb.Append("@CreateDate");
                sb.Append("); ");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            
        }


        #endregion

      
    }
}
