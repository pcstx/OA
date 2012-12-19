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
        #region 用户管理

        public DataTable GetUserList()
        {
            string sql = "";

            sql = "select * from [UserList]";

            return ExecuteDataset(sql).Tables[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arylstQueryParamter"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable sp_userList_1(ArrayList arylstQueryParamter, int PageSize, int PageIndex)
        {
            DbParameter[] prams = {
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//序号
                                MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//用户ID
                                MakeInParam("@UserName",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[2].ToString() ),//用户姓名
                                MakeInParam("@UserType",(DbType)SqlDbType.VarChar,1,arylstQueryParamter[3].ToString() ),//用户类型
                                MakeInParam("@UserCode",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//员工编号
                                MakeInParam("@LoginAllowFlag",(DbType)SqlDbType.VarChar,1,arylstQueryParamter[5].ToString() ),//仅有权限登陆用户
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int, 4, PageSize),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                  };

            string sql = "[sp_GetUserList]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arylstQueryParamter"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable sp_userList_byDeptID(ArrayList arylstQueryParamter, int PageSize, int PageIndex)
        {
            DbParameter[] prams = {
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//序号
                                MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//用户ID
                                MakeInParam("@UserName",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[2].ToString() ),//用户姓名
                                MakeInParam("@DeptID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[3].ToString() ),//部门ID
                                MakeInParam("@UserCode",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//员工编号
                                MakeInParam("@LoginAllowFlag",(DbType)SqlDbType.VarChar,1,arylstQueryParamter[5].ToString() ),//仅有权限登陆用户
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                  };

            string sql = "[sp_userList_2]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_UserListEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddUserList(UserListEntity _UserListEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = {
                                       MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserID ),
                                      MakeInParam("@UserCode",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserCode ),
                                      MakeInParam("@UserType",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserType ),
                                  };
            string sql = "select * from UserList where (len(@UserID)>0 and UserID=@UserID) or (@UserType='0' and UserCode=@UserCode) ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-2";//该记录已经存在
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserID ),
									   MakeInParam("@PassWord",(DbType)SqlDbType.VarChar,50,_UserListEntity.PassWord ),
									   MakeInParam("@UserName",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserName ),
									   MakeInParam("@UserType",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserType ),
									   MakeInParam("@UserEmail",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserEmail ),
									   MakeInParam("@UserCode",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserCode ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.VarChar,1,_UserListEntity.UseFlag ),
									   MakeInParam("@CreateUser",(DbType)SqlDbType.VarChar,50,_UserListEntity.CreateUser ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,16,_UserListEntity.CreateDate ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_UserListEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,16,_UserListEntity.LastModifyDate ),
                                             };

                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[UserList]");
                sb.Append("(");
                sb.Append(" [UserID]");
                sb.Append(",[PassWord]");
                sb.Append(",[UserName]");
                sb.Append(",[UserType]");
                sb.Append(",[UserEmail]");
                sb.Append(",[UserCode]");
                sb.Append(",[UseFlag]");
                sb.Append(",[CreateUser]");
                sb.Append(",[CreateDate]");
                sb.Append(",[LastModifier]");
                sb.Append(",[LastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@UserID,");
                sb.Append("@PassWord,");
                sb.Append("@UserName,");
                sb.Append("@UserType,");
                sb.Append("@UserEmail,");
                sb.Append("@UserCode,");
                sb.Append("@UseFlag,");
                sb.Append("@CreateUser,");
                sb.Append("@CreateDate,");
                sb.Append("@LastModifier,");
                sb.Append("@LastModifyDate )");
                sb.Append("select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_UserListEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateUserList(UserListEntity _UserListEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,_UserListEntity.UserSerialID ),
									   MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserID ),
									   MakeInParam("@PassWord",(DbType)SqlDbType.VarChar,50,_UserListEntity.PassWord ),
									   MakeInParam("@UserName",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserName ),
									   MakeInParam("@UserType",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserType ),
									   MakeInParam("@UserEmail",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserEmail ),
									   MakeInParam("@UserCode",(DbType)SqlDbType.VarChar,50,_UserListEntity.UserCode ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.VarChar,1,_UserListEntity.UseFlag ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_UserListEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,16,_UserListEntity.LastModifyDate ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[UserList]");
            sb.Append(" set ");
            sb.Append(" [UserID]=@UserID,");
            sb.Append(" [PassWord]=@PassWord,");
            sb.Append(" [UserName]=@UserName,");
            sb.Append(" [UserType]=@UserType,");
            sb.Append(" [UserEmail]=@UserEmail,");
            sb.Append(" [UserCode]=@UserCode,");
            sb.Append(" [UseFlag]=@UseFlag,");
            sb.Append(" [LastModifier]=@LastModifier,");
            sb.Append(" [LastModifyDate]=@LastModifyDate ");
            sb.Append(" where [UserSerialID]=@UserSerialID ");

            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="UserSerialID"></param>
        /// <returns></returns>
        public UserListEntity GetUserListEntityByKeyCol(string UserSerialID)
        {
            try
            {
                ArrayList arylst = new ArrayList();
                arylst.Add(UserSerialID);//序号
                arylst.Add("");//用户ID
                arylst.Add("");//用户姓名
                arylst.Add("");//用户类型
                arylst.Add(""); //员工编号
                arylst.Add("0");
                DataTable dt = sp_userList_1(arylst, 1, 1);
                if (dt.Rows.Count == 0)
                    return null;
                else
                    return GetUserListFromDataRow(dt.Rows[0]);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public UserListEntity GetUserListEntityByUserID(string UserID)
        {
            try
            {
                ArrayList arylst = new ArrayList();
                arylst.Add("");//序号
                arylst.Add(UserID);//用户ID
                arylst.Add("");//用户姓名
                arylst.Add("");//用户类型
                arylst.Add(""); //员工编号
                arylst.Add("0");
                DataTable dt = sp_userList_1(arylst, 1, 1);
                if (dt.Rows.Count == 0)
                    //throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
                    return null;
                else
                    return GetUserListFromDataRow(dt.Rows[0]);
            }
            catch
            {
           // return null;
            }

            return null;
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public UserListEntity GetUserListEntityByUserCode(string userCode)
        {
            try
            {
                ArrayList arylst = new ArrayList();
                arylst.Add("");//序号
                arylst.Add("");//用户ID
                arylst.Add("");//用户姓名
                arylst.Add("");//用户类型
                arylst.Add(userCode); //员工编号
                arylst.Add("0");
                DataTable dt = sp_userList_1(arylst, 1, 1);
                if (dt.Rows.Count == 0)
                    throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
                else
                    return GetUserListFromDataRow(dt.Rows[0]);
            }
            catch
            {
            }
            return null;
        }

        private UserListEntity GetUserListFromDataRow(DataRow dr)
        {
            UserListEntity dt = new UserListEntity();

            if (dr["UserSerialID"].ToString() != "" || dr["UserSerialID"] != null) dt.UserSerialID = Int32.Parse(dr["UserSerialID"].ToString());
            dt.UserID = dr["UserID"].ToString();
            dt.PassWord = dr["PassWord"].ToString();
            dt.UserName = dr["UserName"].ToString();
            dt.UserType = dr["UserType"].ToString();
            dt.UserTypeN = dr["UserTypeN"].ToString();
            dt.UserEmail = dr["UserEmail"].ToString();
            dt.UserCode = dr["UserCode"].ToString();
            dt.DeptName = dr["DeptName"].ToString();
            dt.UseFlag = dr["UseFlag"].ToString();
            dt.CreateUser = dr["CreateUser"].ToString();
            dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
            dt.LastModifier = dr["LastModifier"].ToString();
            dt.LastModifyDate = Convert.ToDateTime(dr["LastModifyDate"]);

            return dt;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserSerialID"></param>
        /// <returns></returns>
        public string Delete_UserList(string UserSerialID)
        {
            DbParameter[] prams = {
                                    MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,UserSerialID),
                                  };
            string sql = "DELETE from [UserList] where UserSerialID = @UserSerialID; Delete from UserRight where UserSerialID = @UserSerialID;";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }
        #endregion

        #region 用户权限
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="UserSerialID"></param>
        /// <param name="ArlMenuID"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddUserRight(string UserSerialID, ArrayList ArlMenuID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,UserSerialID),
                                         };
            string sqlDelete = "Delete from [dbo].[UserRight] where UserSerialID=@UserSerialID ";
            ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelete);

            for (int i = 0; i < ArlMenuID.Count; i++)
            {
                DbParameter[] pramsInsert = {
                                                 MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,UserSerialID),
                                                 MakeInParam("@MenuID",(DbType)SqlDbType.VarChar,50,ArlMenuID[i]),
                                             };
                string sqlInsert = "insert into [dbo].[UserRight](UserSerialID,MenuID)values(@UserSerialID,@MenuID)";
                ExecuteNonQuery(CommandType.Text, sqlInsert, pramsInsert);
            }
            return "0";
        }

        /// <summary>
        /// 获得用户的有效节点
        /// </summary>
        /// <param name="UserSerialID">用户SerialID</param>
        /// <param name="PSSMEPMC">上级模块编号</param>
        /// <returns></returns>
        public DataTable GetPSSMEInfoOfUser(string UserSerialID, string PSSMEPMC)
        {
            DbParameter[] prams = {
									   MakeInParam("@UserSerialID", (DbType)SqlDbType.Int, 4, UserSerialID),
									   MakeInParam("@PSSMEPMC", (DbType)SqlDbType.VarChar, 50, PSSMEPMC)
								   };
            string sql = @"
select a.* from
PSSME a,
(select distinct MenuID
from(
select bb.MenuID
from UserRight bb
where UserSerialID=@UserSerialID
Union
select bb.MenuID
from
(
select a.GroupID from SysGroup a where GroupType=9
union
select a.GroupID from SysGroup a,SysGroupUser b
where a.GroupID=b.GroupID and b.UserSerialID=@UserSerialID
) aa,
GroupRight bb
where aa.GroupID=bb.GroupID
) aaa)b
where a.PSSMEMC=b.MenuID and PSSMEPMC like @PSSMEPMC +'%' ";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 获得用户的权限

        /// </summary>
        public DataTable GetSpUserRight(string menuID, string userID)
        {
            DbParameter[] prams = {   
                                   MakeInParam("@menuID",(DbType)SqlDbType.VarChar,50,menuID),//菜单编号
                                   MakeInParam("@userID",(DbType)SqlDbType.VarChar,50,userID),//用户帐号
                                  };
            string sql = "[GetSpUserRight]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }


        //add Rio
        //根据userid获取sys groupid
        public DataTable GetUserSysGroup(string UserSerialID)
        {
            DbParameter[] prams = {
									   MakeInParam("@UserSerialID", (DbType)SqlDbType.Int, 4, UserSerialID),
									
								   };
            string sql = @"
select a.GroupID from SysGroup a,SysGroupUser b
where a.GroupID=b.GroupID and b.UserSerialID=@UserSerialID";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }


        #endregion

        #region 部门信息

        /// <summary>
        /// 根据部门ID获得下级部门信息
        /// </summary>
        /// <param name="PBDEPID"></param>
        /// <returns></returns>
        public DataTable GetChildDeptbyDeptID(string PBDEPID)
        {
            DbParameter[] prams = { MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar, 50, PBDEPID),
								   };
            string sql = "select * from [PBDEP] where (isnull([PBDEPPID],0)=@PBDEPID or (len(PBDEPPID)=0 and @PBDEPID=0)) and PBDEPDC<>'20' and PBDEPDC<>'22' and PBDEPUS='1' Order by PBDEPDN";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 根据DataTable获取部门实体的数组
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public PBDEPEntity[] GetDeptEntityArray(DataTable dataTable)
        {
            int arrayItems = dataTable.Rows.Count;

            PBDEPEntity[] deptArray = new PBDEPEntity[arrayItems];
            for (int i = 0; i < arrayItems; i++)
            {
                deptArray[i] = GetDeptEntity(dataTable.Rows[i]);
            }
            return deptArray;
        }

        /// <summary>
        /// 根据DataRow获取部门实体
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public PBDEPEntity GetDeptEntity(DataRow dataRow)
        {
            PBDEPEntity _PBDEPEntity = new PBDEPEntity();
            _PBDEPEntity.DeptID = dataRow["PBDEPID"].ToString();
            _PBDEPEntity.DeptCode = dataRow["PBDEPDC"].ToString();
            _PBDEPEntity.DeptName = dataRow["PBDEPDN"].ToString();
            _PBDEPEntity.DeptEName = dataRow["PBDEPDEN"].ToString();
            _PBDEPEntity.DeptTWName = dataRow["PBDEPDTWN"].ToString();
            _PBDEPEntity.ParentDeptID = dataRow["PBDEPPID"].ToString();
            _PBDEPEntity.ParentDeptCode = dataRow["PBDEPPDC"].ToString();
            _PBDEPEntity.DeptOrderItem = Convert.ToInt32(dataRow["PBDEPOI"].ToString());
            _PBDEPEntity.DeptIsValid = dataRow["PBDEPUS"].ToString();

            return _PBDEPEntity;
        }
        #endregion

        #region "部门资料"
        /// <summary>
        /// 根据部门ID获得部门信息,当参数"是否启用PBDEPUS"的值为空的话，则抓全部部门资料
        /// </summary>
        /// <param name="PBDEPUS"></param>
        /// <param name="PBDEPID"></param>
        /// <returns></returns>
        public DataTable GetDeptInforbyDeptID(string PBDEPUS, string PBDEPID)
        {
            DbParameter[] prams = {
									   MakeInParam("@PBDEPUS", (DbType)SqlDbType.VarChar, 1, PBDEPUS),
									   MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar, 50, PBDEPID)
								   };
            string sql = "";
            if (PBDEPUS == "")
                sql = "select * from [PBDEP] where PBDEPID=@PBDEPID Order by PBDEPDN ";
            else
                sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPID=@PBDEPID  Order by PBDEPDN";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }
        #endregion

        #region "部门负责人"
        /// <summary>
        /// 获取信息
        /// </summary>
        /// <param name="arylstQueryParamter"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public DataTable sp_GetDepartLeaderList(ArrayList arylstQueryParamter, int PageSize, int PageIndex)
        {
            DbParameter[] prams = {
                                MakeInParam("@RequestUserSerialID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//序号
                                MakeInParam("@RequestUserID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//用户ID
                                MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                  };

            string sql = "[sp_GetDepartLeaderList]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_DepartmentLeaderEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateDepartmentLeader(DepartmentLeaderEntity _DepartmentLeaderEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = {
                                      MakeInParam("@DeptID",(DbType)SqlDbType.Int,4,_DepartmentLeaderEntity.DeptID),
                                  };
            string sql = "select * from DepartmentLeader where DeptID=@DeptID ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                DbParameter[] pramsUpdate = {
									   MakeInParam("@DeptID",(DbType)SqlDbType.Int,4,_DepartmentLeaderEntity.DeptID ),
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,_DepartmentLeaderEntity.UserSerialID ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_DepartmentLeaderEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_DepartmentLeaderEntity.lastModifyDate ),
                                             };

                StringBuilder sb = new StringBuilder();
                sb.Append("Update [dbo].[DepartmentLeader]");
                sb.Append(" set ");
                sb.Append(" [UserSerialID]=@UserSerialID,");
                sb.Append(" [lastModifier]=@lastModifier,");
                sb.Append(" [lastModifyDate]=@lastModifyDate ");
                sb.Append(" where [DeptID]=@DeptID");
                return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpdate).ToString();
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@DeptID",(DbType)SqlDbType.Int,4,_DepartmentLeaderEntity.DeptID ),
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,_DepartmentLeaderEntity.UserSerialID ),
									   MakeInParam("@lastModifier",(DbType)SqlDbType.VarChar,50,_DepartmentLeaderEntity.lastModifier ),
									   MakeInParam("@lastModifyDate",(DbType)SqlDbType.DateTime,23,_DepartmentLeaderEntity.lastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[DepartmentLeader]");
                sb.Append("(");
                sb.Append(" [DeptID]");
                sb.Append(",[UserSerialID]");
                sb.Append(",[lastModifier]");
                sb.Append(",[lastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@DeptID,");
                sb.Append("@UserSerialID,");
                sb.Append("@lastModifier,");
                sb.Append("@lastModifyDate )");
                sb.Append("select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        #endregion

        #region "部门全称"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PBDEPID"></param>
        /// <returns></returns>
        public string fp_getDepartFullNameByID(string PBDEPID)
        {
            DbParameter[] prams = {
                MakeInParam("@PBDEPID",(DbType)SqlDbType.Int,4,PBDEPID),//部门ID
                                  };

            string sql = "select [dbo].[fp_getDepartFullNameByID](@PBDEPID)";
            object o = ExecuteScalar(CommandType.Text, sql, prams);
            return Convert.IsDBNull(o) || o == null ? string.Empty : o.ToString();
        }
        #endregion

        #region "角色管理"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_SysRoleEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddSysRole(SysRoleEntity _SysRoleEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = { MakeInParam("@RoleName",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.RoleName),
                                     };
            string sql = "select * from [dbo].[SysRole] where RoleName=@RoleName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@RoleName",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.RoleName ),
									   MakeInParam("@RoleDesc",(DbType)SqlDbType.VarChar,2000,_SysRoleEntity.RoleDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_SysRoleEntity.DisplayOrder ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.VarChar,1,_SysRoleEntity.UseFlag ),
									   MakeInParam("@CreateUser",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.CreateUser ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_SysRoleEntity.CreateDate ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,23,_SysRoleEntity.LastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[SysRole]");
                sb.Append("(");
                sb.Append(" [RoleName]");
                sb.Append(",[RoleDesc]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[UseFlag]");
                sb.Append(",[CreateUser]");
                sb.Append(",[CreateDate]");
                sb.Append(",[LastModifier]");
                sb.Append(",[LastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@RoleName,");
                sb.Append("@RoleDesc,");
                sb.Append("@DisplayOrder,");
                sb.Append("@UseFlag,");
                sb.Append("@CreateUser,");
                sb.Append("@CreateDate,");
                sb.Append("@LastModifier,");
                sb.Append("@LastModifyDate )");
                sb.Append("select @@identity;");
                string RoleID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                UpdateSysRoleUser(RoleID, _SysRoleEntity.dtUserList);
                return "0";
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_SysRoleEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateSysRole(SysRoleEntity _SysRoleEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@RoleID",(DbType)SqlDbType.Int,4,_SysRoleEntity.RoleID ),
									   MakeInParam("@RoleName",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.RoleName ),
									   MakeInParam("@RoleDesc",(DbType)SqlDbType.VarChar,2000,_SysRoleEntity.RoleDesc ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_SysRoleEntity.DisplayOrder ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.VarChar,1,_SysRoleEntity.UseFlag ),
									   MakeInParam("@CreateUser",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.CreateUser ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_SysRoleEntity.CreateDate ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_SysRoleEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,23,_SysRoleEntity.LastModifyDate ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[SysRole]");
            sb.Append(" set ");
            sb.Append(" [RoleName]=@RoleName,");
            sb.Append(" [RoleDesc]=@RoleDesc,");
            sb.Append(" [DisplayOrder]=@DisplayOrder,");
            sb.Append(" [UseFlag]=@UseFlag,");
            sb.Append(" [CreateUser]=@CreateUser,");
            sb.Append(" [CreateDate]=@CreateDate,");
            sb.Append(" [LastModifier]=@LastModifier,");
            sb.Append(" [LastModifyDate]=@LastModifyDate ");
            sb.Append(" where [RoleID]=@RoleID ");
            ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate);
            UpdateSysRoleUser(_SysRoleEntity.RoleID.ToString(), _SysRoleEntity.dtUserList);
            return "0";
        }

        private void UpdateSysRoleUser(string RoleID, DataTable dtUserList)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@RoleID",(DbType)SqlDbType.Int,4,RoleID),
                                         };
            string sqlDelete = "delete from [dbo].[SysRoleUser] where RoleID=@RoleID ";
            ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelete);
            for (int i = 0; i < dtUserList.Rows.Count; i++)
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@RoleID",(DbType)SqlDbType.Int,4,RoleID ),
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,dtUserList.Rows[i]["UserSerialID"] ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[SysRoleUser]");
                sb.Append("(");
                sb.Append(" [RoleID]");
                sb.Append(",[UserSerialID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@RoleID,");
                sb.Append("@UserSerialID )");
                ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsInsert);

            }
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public SysRoleEntity GetSysRoleEntityByKeyCol(string RoleID)
        {
            string sql = "select * from [dbo].[SysRole] Where RoleID=@RoleID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@RoleID",(DbType)SqlDbType.VarChar,50,RoleID ),
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetSysRoleFromIDataReader(sr);
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
        private SysRoleEntity GetSysRoleFromIDataReader(DbDataReader dr)
        {
            SysRoleEntity dt = new SysRoleEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["RoleID"].ToString() != "" || dr["RoleID"] != null) dt.RoleID = Int32.Parse(dr["RoleID"].ToString());
                dt.RoleName = dr["RoleName"].ToString();
                dt.RoleDesc = dr["RoleDesc"].ToString();
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dt.UseFlag = dr["UseFlag"].ToString();
                dt.CreateUser = dr["CreateUser"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dt.LastModifier = dr["LastModifier"].ToString();
                dt.LastModifyDate = Convert.ToDateTime(dr["LastModifyDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arylstQueryParamter"></param>
        /// <returns></returns>
        public DataTable sp_GetSysRoleUser(ArrayList arylstQueryParamter)
        {
            DbParameter[] prams = {
                                MakeInParam("@RoleID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//用户编号
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//用户编号
                                MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[2].ToString() ),//用户ID
                                   };

            string sql = "[sp_GetSysRoleUser]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        public DataTable GetSysRole()
        {
            string sql = "select * from [SysRole] ";
            return ExecuteDataset(sql).Tables[0];
        }

        #endregion

        #region "用户组管理"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_SysGroupEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddSysGroup(SysGroupEntity _SysGroupEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = { MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.GroupName),
                                     };
            string sql = "select * from [dbo].[SysGroup] where GroupName=@GroupName ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.GroupName ),
									   MakeInParam("@GroupDesc",(DbType)SqlDbType.VarChar,2000,_SysGroupEntity.GroupDesc ),
                                       MakeInParam("@GroupType",(DbType)SqlDbType.Int,4,_SysGroupEntity.GroupType ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_SysGroupEntity.DisplayOrder ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.VarChar,1,_SysGroupEntity.UseFlag ),
									   MakeInParam("@CreateUser",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.CreateUser ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_SysGroupEntity.CreateDate ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,23,_SysGroupEntity.LastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[SysGroup]");
                sb.Append("(");
                sb.Append(" [GroupName]");
                sb.Append(",[GroupDesc]");
                sb.Append(",[GroupType]");
                sb.Append(",[DisplayOrder]");
                sb.Append(",[UseFlag]");
                sb.Append(",[CreateUser]");
                sb.Append(",[CreateDate]");
                sb.Append(",[LastModifier]");
                sb.Append(",[LastModifyDate]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@GroupName,");
                sb.Append("@GroupDesc,");
                sb.Append("@GroupType,");
                sb.Append("@DisplayOrder,");
                sb.Append("@UseFlag,");
                sb.Append("@CreateUser,");
                sb.Append("@CreateDate,");
                sb.Append("@LastModifier,");
                sb.Append("@LastModifyDate )");
                sb.Append("select @@identity;");
                string GroupID = ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
                UpdateSysGroupUser(GroupID, _SysGroupEntity.dtUserList);
                return "0";
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_SysGroupEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpdateSysGroup(SysGroupEntity _SysGroupEntity)
        {
            DbParameter[] pramsUpdate = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,_SysGroupEntity.GroupID ),
									   MakeInParam("@GroupName",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.GroupName ),
									   MakeInParam("@GroupDesc",(DbType)SqlDbType.VarChar,2000,_SysGroupEntity.GroupDesc ),
									   MakeInParam("@GroupType",(DbType)SqlDbType.Int,4,_SysGroupEntity.GroupType ),
									   MakeInParam("@DisplayOrder",(DbType)SqlDbType.Int,4,_SysGroupEntity.DisplayOrder ),
									   MakeInParam("@UseFlag",(DbType)SqlDbType.Char,1,_SysGroupEntity.UseFlag ),
									   MakeInParam("@CreateUser",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.CreateUser ),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,23,_SysGroupEntity.CreateDate ),
									   MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_SysGroupEntity.LastModifier ),
									   MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,23,_SysGroupEntity.LastModifyDate ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("Update [dbo].[SysGroup]");
            sb.Append(" set ");
            sb.Append(" [GroupName]=@GroupName,");
            sb.Append(" [GroupDesc]=@GroupDesc,");
            sb.Append(" [GroupType]=@GroupType,");
            sb.Append(" [DisplayOrder]=@DisplayOrder,");
            sb.Append(" [UseFlag]=@UseFlag,");
            sb.Append(" [LastModifier]=@LastModifier,");
            sb.Append(" [LastModifyDate]=@LastModifyDate ");
            sb.Append(" where [GroupID]=@GroupID ");
            ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpdate);
            UpdateSysGroupUser(_SysGroupEntity.GroupID.ToString(), _SysGroupEntity.dtUserList);
            return "0";
        }

        private void UpdateSysGroupUser(string GroupID, DataTable dtUserList)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID),
                                         };
            string sqlDelete = "delete from [dbo].[SysGroupUser] where GroupID=@GroupID ";
            ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelete);
            for (int i = 0; i < dtUserList.Rows.Count; i++)
            {
                DbParameter[] pramsInsert = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID ),
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,dtUserList.Rows[i]["UserSerialID"] ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[SysGroupUser]");
                sb.Append("(");
                sb.Append(" [GroupID]");
                sb.Append(",[UserSerialID]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@GroupID,");
                sb.Append("@UserSerialID )");
                ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsInsert);

            }
        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="GroupID"></param>
        /// <returns></returns>
        public SysGroupEntity GetSysGroupEntityByKeyCol(string GroupID)
        {
            string sql = "select * from [dbo].[SysGroup] Where GroupID=@GroupID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@GroupID",(DbType)SqlDbType.VarChar,50,GroupID ),
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetSysGroupFromIDataReader(sr);
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
        private SysGroupEntity GetSysGroupFromIDataReader(DbDataReader dr)
        {
            SysGroupEntity dt = new SysGroupEntity();
            if (dr.FieldCount > 0)
            {
                if (dr["GroupID"].ToString() != "" || dr["GroupID"] != null) dt.GroupID = Int32.Parse(dr["GroupID"].ToString());
                dt.GroupName = dr["GroupName"].ToString();
                dt.GroupDesc = dr["GroupDesc"].ToString();
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                dt.UseFlag = dr["UseFlag"].ToString();
                dt.CreateUser = dr["CreateUser"].ToString();
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dt.LastModifier = dr["LastModifier"].ToString();
                dt.LastModifyDate = Convert.ToDateTime(dr["LastModifyDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 获取用户组中的用户一览
        /// </summary>
        /// <param name="arylstQueryParamter"></param>
        /// <returns></returns>
        public DataTable sp_GetSysGroupUser(ArrayList arylstQueryParamter)
        {
            DbParameter[] prams = {
                                MakeInParam("@GroupID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//用户编号
                                MakeInParam("@UserSerialID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//用户编号
                                MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[2].ToString() ),//用户ID
                                   };

            string sql = "[sp_GetSysGroupUser]";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="GroupID"></param>
        public string DeleteSysGroup(string GroupID)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID),
                                         };
            string sqlDelete = "delete from [dbo].[SysGroup] where GroupID=@GroupID and GroupType=1;  delete from SysGroupUser where GroupID not in (select GroupID from SysGroup);";
            return ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelete).ToString();
        }

        #endregion

        #region "用户组权限"   

        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="GroupID"></param>
        /// <param name="ArlMenu"></param>
        /// <returns></returns>
        public string AddGroupRight(string GroupID, ArrayList ArlMenu)
        {
            DbParameter[] pramsDelete = {
									   MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID),
                                         };
            string sqlDelete = "delete from [dbo].[GroupRight] where GroupID=@GroupID ";
            ExecuteNonQuery(CommandType.Text, sqlDelete, pramsDelete);

            for (int i = 0; i < ArlMenu.Count; i++)
            {
                DbParameter[] pramsInsert = {
                                                 MakeInParam("@GroupID",(DbType)SqlDbType.Int,4,GroupID),
                                                 MakeInParam("@MenuID",(DbType)SqlDbType.VarChar,50,ArlMenu[i]),
                                             };
                string sqlInsert = "insert into [dbo].[GroupRight](GroupID,MenuID)values(@GroupID,@MenuID)";
                ExecuteNonQuery(CommandType.Text, sqlInsert, pramsInsert);
            }
            return "0";
        }


        #endregion


       
    }
}
