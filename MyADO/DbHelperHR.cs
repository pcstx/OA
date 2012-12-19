using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;
using GPRP.GPRPComponents;
using GPRP.Entity;
using System.Data.SqlClient;
using GPRP.Entity.Basic;


namespace MyADO
{
    public partial class DbHelper
    {

        #region "获取员工基本信息 PEEBI"
        /// <summary>
        /// 获取表信息
        /// 在基础数据页，应该全部选出；
        /// 在其它页，无效数据不出来，且按序排序
        /// </summary>
        /// <param name="PageSource">只能用两个值，在基础数据页为"Basic" ;否则为"Other" 默认为 Other; </param>
        /// <param name="SearchSql">查询字符串,在基础数据页以外的页，该项基本不会用 </param>
        /// <returns></returns>
        public DataTable GetPEEBI(string PageSource, string SearchSql)
        {
            if (PageSource == "")
                PageSource = "Other";
            string sql = "select * from [PEEBI]";
            if (PageSource == "Basic")
            {
                if (SearchSql != "")
                    sql += SearchSql;
            }
            else
            {
                //sql += " Where [PBPOSUS]='1' Order By PBPOSOI";

            }

            return ExecuteDataset(CommandType.Text, sql).Tables[0];

        }

        public DataTable sp_GetPEEBINameAllList(ArrayList arylstQueryParamter, int PageSize, int PageIndex)
        {
            if (arylstQueryParamter[2].ToString() == "") arylstQueryParamter[2] = "1900-01-01";
          //  if (arylstQueryParamter[3].ToString() == "") arylstQueryParamter[3] = "1900-01-01";
            if (arylstQueryParamter[6].ToString() == "") arylstQueryParamter[6] = "1900-01-01";
          //  if (arylstQueryParamter[8].ToString() == "") arylstQueryParamter[8] = "1900-01-01";

            DbParameter[] prams = {   
                                       MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//员工编号
									   MakeInParam("@PEEBIEN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//员工姓名
                                       MakeInParam("@PEEBIBDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[2]) ),//员工出生日期
                                  //      MakeInParam("@PEEBIBDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[3].ToString()) ),//员工出生日期
                                       // MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//员工身份证编号
                                       // MakeInParam("@PEEBIEG",(DbType)SqlDbType.Char,1,arylstQueryParamter[5].ToString() ),//员工性别
                                       // MakeInParam("@PEEBIMI",(DbType)SqlDbType.Char,1,arylstQueryParamter[6].ToString() ),//婚姻状况
                                       //MakeInParam("@PEEBIENA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString()),//员工民族
                                       //MakeInParam("@PEEBINP",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[8].ToString() ),//员工籍贯
                                       //MakeInParam("@PEEBIRT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[9].ToString() ),//员工户口性质
									   MakeInParam("@PEEBIDEP",(DbType)SqlDbType.VarChar,500,arylstQueryParamter[3].ToString() ),//员工所属部门
                                       MakeInParam("@PEEBIPC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//岗位
                                        //MakeInParam("@PEEBIPT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[12].ToString() ),//岗位性质
                                        //MakeInParam("@PEEBIEL",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[13].ToString() ),//学历
                                        //MakeInParam("@PEEBILM",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[14].ToString() ),//所学专业
                                        //MakeInParam("@PEEBIGDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[15].ToString()) ),//毕业时间
                                        //MakeInParam("@PEEBIGDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[16].ToString()) ),//毕业时间
                                         MakeInParam("@PEEBIDT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[5].ToString() ),//职务
                                         MakeInParam("@PEEBIEDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[6].ToString()) ),//员工入厂时间
                                      //  MakeInParam("@PEEBIEDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[8].ToString()) ),//员工入厂时间
                                        //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[20].ToString() ),//员工工种类别
                                        // MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[21].ToString() ),//来源类别信息
                                        MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString() ),//状态类别信息
                                        // MakeInParam("@PEEBIVY",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[23].ToString() ),//员工身份证有效期限
                                        //MakeInParam("@PEEBIEA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[24].ToString() ),//员工进厂年龄
                                        // MakeInParam("@PEEBIIBDMONTH",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[25].ToString() ),//员工生日月份
                                         MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                       MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                       //MakeInParam("@PEEBIIP",(DbType)SqlDbType.smallDatetime,16,_PEEBIEntity.PEEBIIP ),
                                       //MakeInParam("@PEEBIVY",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIVY ),
                                       //MakeInParam("@PEEBICP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBICP ),
                                       //MakeInParam("@PEEBIECN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECN ),
                                       //MakeInParam("@PEEBIECP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECP ),
                                       //MakeInParam("@PEEBIGF",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIGF ),
                                       //MakeInParam("@PEEBIDN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDN ),
                                       //MakeInParam("@PEEBIDNC",(DbType)SqlDbType.smallDatetime,16,_PEEBIEntity.PEEBIDNC ),
                                       //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWT ),
                                       //MakeInParam("@PEEBIDL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDL ),
                                       //MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEST ),
                                       //MakeInParam("@PEEBIESN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIESN ),
                                       //MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIES ),
                                      // MakeInParam("@PEEBIRA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIRA ),
									  // MakeInParam("@PEEBISA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBISA ),
									 //MakeInParam("@PEEBIEEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEEN ),
									  //MakeInParam("@PEEBIETWN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIETWN ),
									  // MakeInParam("@PEEBIEA",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIEA ),
									  

                                       
								   };

            string sql = "sp_GetPEEBINameListTwoTable";


            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        
        }


        public DataTable sp_GetPEEBINameList(ArrayList arylstQueryParamter, int PageSize, int PageIndex)
        {
            //arylst.Add(txtqPEEBIEC.Text);//员工编号
            //arylst.Add(txtqPEEBIEN.Text);//员工姓名
            //arylst.Add(txtPEEBIBDStart.Text);	//员工出生日期
            //arylst.Add(txtPEEBIBDEnd.Text);	//员工出生日期
            //arylst.Add(txtqPEEBIIN.Text);//员工身份证编号
            //arylst.Add(dpqPEEBIEG.SelectedValue);//员工性别
            //arylst.Add(dpqPEEBIMI.SelectedValue);//婚姻状况
            //arylst.Add(dpqPEEBIENA.SelectedValue);//员工民族
            //arylst.Add(txtqPEEBINP.Text);//员工籍贯
            //arylst.Add(dpqPEEBIRT.SelectedValue);//员工户口性质
            //arylst.Add(dpqPEEBIDEP.SelectedValue);//员工所属部门
            //arylst.Add(dpqPEEBIPC.SelectedValue);//岗位
            //arylst.Add(dpqPEEBIPT.SelectedValue);//岗位性质
            //arylst.Add(dpqPEEBIEL.SelectedValue);//学历
            //arylst.Add(dpqPEEBILM.SelectedValue);//所学专业
            //arylst.Add(txtPEEBIGDStart.Text);//毕业时间
            //arylst.Add(txtPEEBIGDEnd.Text);//毕业时间
            //arylst.Add(dpqPEEBIDT.SelectedValue);//职务
            //arylst.Add(txtPEEBIEDStart.Text);//员工入厂时间
            //arylst.Add(txtPEEBIEDEnd.Text);//员工入厂时间
            //arylst.Add(txtqPEEBIWTCode.Value);//员工工种类别
            //arylst.Add(txtqPEEBIESTCode.Value);//来源类别信息
            //arylst.Add(txtqPEEBIESCode.Value);//状态类别信息
            //arylst.Add(dpqPEEBIVY.SelectedValue);//员工身份证有效期限 
            //arylst.Add(txtqPEEBIEA.Text.ToString().Trim());//员工进厂年龄
            if (arylstQueryParamter[2].ToString() == "") arylstQueryParamter[2] = "1900-01-01";
           // if (arylstQueryParamter[3].ToString() == "") arylstQueryParamter[3] = "1900-01-01";
            if (arylstQueryParamter[6].ToString() == "") arylstQueryParamter[6] = "1900-01-01";
           // if (arylstQueryParamter[8].ToString() == "") arylstQueryParamter[8] = "1900-01-01";

            DbParameter[] prams = {   
                                       MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//员工编号
									   MakeInParam("@PEEBIEN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//员工姓名
                                       MakeInParam("@PEEBIBDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[2]) ),//员工出生日期
                                      //  MakeInParam("@PEEBIBDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[3].ToString()) ),//员工出生日期
                                       // MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//员工身份证编号
                                       // MakeInParam("@PEEBIEG",(DbType)SqlDbType.Char,1,arylstQueryParamter[5].ToString() ),//员工性别
                                       // MakeInParam("@PEEBIMI",(DbType)SqlDbType.Char,1,arylstQueryParamter[6].ToString() ),//婚姻状况
                                       //MakeInParam("@PEEBIENA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString()),//员工民族
                                       //MakeInParam("@PEEBINP",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[8].ToString() ),//员工籍贯
                                       //MakeInParam("@PEEBIRT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[9].ToString() ),//员工户口性质
									   MakeInParam("@PEEBIDEP",(DbType)SqlDbType.VarChar,500,arylstQueryParamter[3].ToString() ),//员工所属部门
                                       MakeInParam("@PEEBIPC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//岗位
                                        //MakeInParam("@PEEBIPT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[12].ToString() ),//岗位性质
                                        //MakeInParam("@PEEBIEL",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[13].ToString() ),//学历
                                        //MakeInParam("@PEEBILM",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[14].ToString() ),//所学专业
                                        //MakeInParam("@PEEBIGDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[15].ToString()) ),//毕业时间
                                        //MakeInParam("@PEEBIGDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[16].ToString()) ),//毕业时间
                                         MakeInParam("@PEEBIDT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[5].ToString() ),//职务
                                         MakeInParam("@PEEBIEDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[6].ToString()) ),//员工入厂时间
                                       // MakeInParam("@PEEBIEDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[8].ToString()) ),//员工入厂时间
                                        //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[20].ToString() ),//员工工种类别
                                        // MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[21].ToString() ),//来源类别信息
                                        MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString() ),//状态类别信息
                                        // MakeInParam("@PEEBIVY",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[23].ToString() ),//员工身份证有效期限
                                        //MakeInParam("@PEEBIEA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[24].ToString() ),//员工进厂年龄
                                        // MakeInParam("@PEEBIIBDMONTH",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[25].ToString() ),//员工生日月份
                                         MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                       MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                       //MakeInParam("@PEEBIIP",(DbType)SqlDbType.smallDatetime,16,_PEEBIEntity.PEEBIIP ),
                                       //MakeInParam("@PEEBIVY",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIVY ),
                                       //MakeInParam("@PEEBICP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBICP ),
                                       //MakeInParam("@PEEBIECN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECN ),
                                       //MakeInParam("@PEEBIECP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECP ),
                                       //MakeInParam("@PEEBIGF",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIGF ),
                                       //MakeInParam("@PEEBIDN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDN ),
                                       //MakeInParam("@PEEBIDNC",(DbType)SqlDbType.smallDatetime,16,_PEEBIEntity.PEEBIDNC ),
                                       //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWT ),
                                       //MakeInParam("@PEEBIDL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDL ),
                                       //MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEST ),
                                       //MakeInParam("@PEEBIESN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIESN ),
                                       //MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIES ),
                                      // MakeInParam("@PEEBIRA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIRA ),
									  // MakeInParam("@PEEBISA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBISA ),
									 //MakeInParam("@PEEBIEEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEEN ),
									  //MakeInParam("@PEEBIETWN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIETWN ),
									  // MakeInParam("@PEEBIEA",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIEA ),
									  

                                       
								   };

            string sql = "sp_GetPEEBINameList";


            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }



        public DataSet sp_GetPEEBINameList(ArrayList arylstQueryParamter)
        {
            //arylst.Add(txtqPEEBIEC.Text);//员工编号
            //arylst.Add(txtqPEEBIEN.Text);//员工姓名
            //arylst.Add(txtPEEBIBDStart.Text);	//员工出生日期
            //arylst.Add(txtPEEBIBDEnd.Text);	//员工出生日期
            //arylst.Add(txtqPEEBIIN.Text);//员工身份证编号
            //arylst.Add(dpqPEEBIEG.SelectedValue);//员工性别
            //arylst.Add(dpqPEEBIMI.SelectedValue);//婚姻状况
            //arylst.Add(dpqPEEBIENA.SelectedValue);//员工民族
            //arylst.Add(txtqPEEBINP.Text);//员工籍贯
            //arylst.Add(dpqPEEBIRT.SelectedValue);//员工户口性质
            //arylst.Add(dpqPEEBIDEP.SelectedValue);//员工所属部门
            //arylst.Add(dpqPEEBIPC.SelectedValue);//岗位
            //arylst.Add(dpqPEEBIPT.SelectedValue);//岗位性质
            //arylst.Add(dpqPEEBIEL.SelectedValue);//学历
            //arylst.Add(dpqPEEBILM.SelectedValue);//所学专业
            //arylst.Add(txtPEEBIGDStart.Text);//毕业时间
            //arylst.Add(txtPEEBIGDEnd.Text);//毕业时间
            //arylst.Add(dpqPEEBIDT.SelectedValue);//职务
            //arylst.Add(txtPEEBIEDStart.Text);//员工入厂时间
            //arylst.Add(txtPEEBIEDEnd.Text);//员工入厂时间
            //arylst.Add(dpqPEEBIIBDMONTH.SelectedValue.ToString());//员工生日月份
            if (arylstQueryParamter[2].ToString() == "") arylstQueryParamter[2] = "1900-01-01";
           // if (arylstQueryParamter[3].ToString() == "") arylstQueryParamter[3] = "1900-01-01";
            if (arylstQueryParamter[6].ToString() == "") arylstQueryParamter[6] = "1900-01-01";
          //  if (arylstQueryParamter[8].ToString() == "") arylstQueryParamter[8] = "1900-01-01";

            DbParameter[] prams = {   
                                       MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[0].ToString() ),//员工编号
									   MakeInParam("@PEEBIEN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[1].ToString() ),//员工姓名
                                       MakeInParam("@PEEBIBDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[2]) ),//员工出生日期
                                  //      MakeInParam("@PEEBIBDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[3].ToString()) ),//员工出生日期
                                       // MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//员工身份证编号
                                       // MakeInParam("@PEEBIEG",(DbType)SqlDbType.Char,1,arylstQueryParamter[5].ToString() ),//员工性别
                                       // MakeInParam("@PEEBIMI",(DbType)SqlDbType.Char,1,arylstQueryParamter[6].ToString() ),//婚姻状况
                                       //MakeInParam("@PEEBIENA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString()),//员工民族
                                       //MakeInParam("@PEEBINP",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[8].ToString() ),//员工籍贯
                                       //MakeInParam("@PEEBIRT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[9].ToString() ),//员工户口性质
									   MakeInParam("@PEEBIDEP",(DbType)SqlDbType.VarChar,500,arylstQueryParamter[3].ToString() ),//员工所属部门
                                       MakeInParam("@PEEBIPC",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[4].ToString() ),//岗位
                                        //MakeInParam("@PEEBIPT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[12].ToString() ),//岗位性质
                                        //MakeInParam("@PEEBIEL",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[13].ToString() ),//学历
                                        //MakeInParam("@PEEBILM",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[14].ToString() ),//所学专业
                                        //MakeInParam("@PEEBIGDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[15].ToString()) ),//毕业时间
                                        //MakeInParam("@PEEBIGDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[16].ToString()) ),//毕业时间
                                         MakeInParam("@PEEBIDT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[5].ToString() ),//职务
                                         MakeInParam("@PEEBIEDStart",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[6].ToString()) ),//员工入厂时间
                                     //   MakeInParam("@PEEBIEDEnd",(DbType)SqlDbType.DateTime,8,Convert.ToDateTime(arylstQueryParamter[8].ToString()) ),//员工入厂时间
                                        //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[20].ToString() ),//员工工种类别
                                        // MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[21].ToString() ),//来源类别信息
                                        MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[7].ToString() ),//状态类别信息
                                        // MakeInParam("@PEEBIVY",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[23].ToString() ),//员工身份证有效期限
                                        //MakeInParam("@PEEBIEA",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[24].ToString() ),//员工进厂年龄
                                        //MakeInParam("@PEEBIIBDMONTH",(DbType)SqlDbType.VarChar,50,arylstQueryParamter[25].ToString() ),//员工生日月份
								   };

            string sql = "sp_GetPEEBINameList_NoPage";


            return ExecuteDataset(CommandType.StoredProcedure, sql, prams);
        }

        public DataTable GetNameAllList(string szTableName)
        {
            string sql;
            sql = string.Format("select ColName,ColType,ColDescriptionCN from sys_TableColumn where TableName='{0}'", szTableName);
            return ExecuteDataset(CommandType.Text,sql).Tables[0];
        
        }

        /// <summary>
        /// 返回获取员工基本信息 实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public PEEBIEntity GetPEEBIEntityByKeyCol(string KeyCol)
        {
            string sql = "[sp_GetPEEBIByCode]";
            DbParameter[] pramsGet = {
									   MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,KeyCol ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.StoredProcedure, sql, pramsGet);
                if (sr.Read())
                {
                    return GetPEEBIFromIDataReader(sr);
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
        private PEEBIEntity GetPEEBIFromIDataReader(DbDataReader dr)
        {
            PEEBIEntity dt = new PEEBIEntity();
            if (dr.FieldCount > 0)
            {
                //dt.PBPRTTN = dr["PBPRTTN"].ToString();
                //dt.PEEBIENUP = dr["PEEBIENUP"].ToString();
                dt.PEEBIEC = dr["PEEBIEC"].ToString();
                dt.PEEBIEN = dr["PEEBIEN"].ToString();
                //dt.PEEBIEEN = dr["PEEBIEEN"].ToString();
                //dt.PEEBIETWN = dr["PEEBIETWN"].ToString();
                //dt.PEEBIEG = dr["PEEBIEG"].ToString();
                //dt.PBSEXCN = dr["PBSEXCN"].ToString();
                if (dr["PEEBIBD"].ToString() != "") dt.PEEBIBD = Convert.ToDateTime(dr["PEEBIBD"].ToString());
               // if (dr["PEEBIEA"].ToString() != "" || dr["PEEBIEA"] != null) dt.PEEBIEA = Int32.Parse(dr["PEEBIEA"].ToString());
                dt.PEEBIDEP = dr["PEEBIDEP"].ToString();
                //dt.PBDEPDN = dr["PBDEPDN"].ToString();
                //dt.PEEBIDEPLink = dr["PEEBIDEPLink"].ToString();
                //dt.PBDUTDN = dr["PBDUTDN"].ToString();
                dt.PEEBIDT = dr["PEEBIDT"].ToString();
                dt.PEEBIPC = dr["PEEBIPC"].ToString();
                //dt.PBPOSPN = dr["PBPOSPN"].ToString();
                //dt.PBEWTTN = dr["PBEWTTN"].ToString();
                //dt.PEEBIWT = dr["PEEBIWT"].ToString();
                //dt.PEEBIPT = dr["PEEBIPT"].ToString();
                //dt.PBPOTTN = dr["PBPOTTN"].ToString();
                //dt.PBPOCCC = dr["PBPOCCC"].ToString();
                //dt.PBPOCCN = dr["PBPOCCN"].ToString();
                //dt.PEEBIWP = dr["PEEBIWP"].ToString();
                //dt.PEEBIWPN = dr["PEEBIWPN"].ToString();
                //dt.PEEBIET = dr["PEEBIET"].ToString();
                //dt.PEEBIETN = dr["PEEBIETN"].ToString();
                dt.PEEBIDL = dr["PEEBIDL"].ToString();
                //dt.PEEBIDN = dr["PEEBIDN"].ToString();
                //dt.PEEBIDNC = dr["PEEBIDNC"].ToString();
                //if (dr["PEEBIDNC"].ToString() != "") dt.PEEBIDNC = Convert.ToDateTime(dr["PEEBIDNC"].ToString());
                //if (dr["PEEBIWL"].ToString() != "") dt.PEEBIWL = Convert.ToDouble(dr["PEEBIWL"].ToString());
                dt.PEEBIED = (DateTime)dr["PEEBIED"];
                if (dr["PEEBIED"].ToString() != "") dt.PEEBIED = Convert.ToDateTime(dr["PEEBIED"].ToString());
                //dt.PEEBIGF = dr["PEEBIGF"].ToString();
                //dt.PEEBIEL = dr["PEEBIEL"].ToString();
                //dt.PBEDULN = dr["PBEDULN"].ToString();
                //dt.PEEBILM = dr["PEEBILM"].ToString();
                //dt.PEEBIGD = dr["PEEBIGD"].ToString();
                //if (dr["PEEBIGD"].ToString() != "") dt.PEEBIGD = Convert.ToDateTime(dr["PEEBIGD"].ToString());
                //dt.PEEBIIN = dr["PEEBIIN"].ToString();
                //dt.PEEBIIP = dr["PEEBIIP"].ToString();
                //if (dr["PEEBIIP"].ToString() != "") dt.PEEBIIP = Convert.ToDateTime(dr["PEEBIIP"].ToString());
               //// if (dr["PEEBIVY"].ToString() != "" || dr["PEEBIVY"] != null) dt.PEEBIVY = Int32.Parse(dr["PEEBIVY"].ToString());
               // dt.PEEBIRT = dr["PEEBIRT"].ToString();
               // dt.PBRETTN = dr["PBRETTN"].ToString();
               // dt.PEEBIENA = dr["PEEBIENA"].ToString();
               // dt.PBNATNN = dr["PBNATNN"].ToString();
               // dt.PBMARCN = dr["PBMARCN"].ToString();
               // dt.PEEBIMI = dr["PEEBIMI"].ToString();
               // dt.PEEBINP = dr["PEEBINP"].ToString();
               // dt.PEEBINPFullName = dr["PEEBINPFullName"].ToString();
               // dt.PEEBIRA = dr["PEEBIRA"].ToString();
               // dt.PEEBISA = dr["PEEBISA"].ToString();
               // dt.PEEBICP = dr["PEEBICP"].ToString();
               // dt.PEEBIECN = dr["PEEBIECN"].ToString();
               // dt.PEEBIECP = dr["PEEBIECP"].ToString();
                dt.PEEBIES = dr["PEEBIES"].ToString();
                dt.PEEBIID = Int32.Parse(dr["PEEBIID"].ToString());
    
                //dt.PBESTTN = dr["PBESTTN"].ToString();
                //dt.PBESETN = dr["PBESETN"].ToString();
                //dt.PEEBIEST = dr["PEEBIEST"].ToString();
                //dt.PEEBIESN = dr["PEEBIESN"].ToString();
                //if (dr["PEETNTD"].ToString() != "") dt.PEETNTD = Convert.ToDateTime(dr["PEETNTD"].ToString());
                //dt.PEEBIML = dr["PEEBIML"].ToString();
                //dt.PEEBIOAAU = dr["PEEBIOAAU"].ToString();
                if (Convert.IsDBNull(dr["PEEBIEP"]))
                {
                }
                else
                {
                   // if (((byte[])dr["PEEBIEP"]).Length > 0)
                       // dt.PEEBIEP = (byte[])dr["PEEBIEP"];
                }
                //dt.PEEBIEPHORN = dr["PEEBIEPHORN"].ToString();


                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        public byte[] GetPEEBIEPByKeyCol(string KeyCol)
        {
            string sql = "Select PEEBIEP from PEEBI Where PEEBIEC=@PEEBIEC";
            DbParameter[] pramsGet = {
									   MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,KeyCol ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return (byte[])sr["PEEBIEP"];
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
        /// 新增信息
        /// </summary>
        /// <param name="_PEEBIEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddPEEBI(PEEBIEntity _PEEBIEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = {    MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEC ),
                                     };
            string sql = "select * from PEEBI where PEEBIEC=@PEEBIEC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                //if (_PEEBIEntity.PEEBIEP == null)
                //{
                //    _PEEBIEntity.PEEBIEPHORN = "无";
                //}
                //else if (_PEEBIEntity.PEEBIEP.Length == 0)
                //{
                //    _PEEBIEntity.PEEBIEPHORN = "无";
                //}
               //add RIo  _PEEBIEntity.PEEBIEC = px_Sequence("PEEBICODE", "1");
                DbParameter[] pramsInsert = {
                                       //MakeInParam("@PEEBIEPHORN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEPHORN ),
									   MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEC ),
									   MakeInParam("@PEEBIEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEN ),
                                       //MakeInParam("@PEEBIEEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEEN ),
                                       //MakeInParam("@PEEBIETWN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIETWN ),
                                       //MakeInParam("@PEEBIEG",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIEG ),
									   MakeInParam("@PEEBIBD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIBD ),
                                       //MakeInParam("@PEEBIEA",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIEA ),
									   MakeInParam("@PEEBIDEP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDEP ),
									   MakeInParam("@PEEBIDT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDT ),
									   MakeInParam("@PEEBIPC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIPC ),
                                       //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWT ),
                                       //MakeInParam("@PEEBIPT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIPT ),
                                       //MakeInParam("@PBPOCCC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBPOCCC ),
                                       //MakeInParam("@PEEBIWP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWP ),
                                       //MakeInParam("@PEEBIET",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIET ),
									   MakeInParam("@PEEBIDL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDL ),
                                       //MakeInParam("@PEEBIDN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDN ),
                                       //MakeInParam("@PBPRTTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBPRTTN ),
                                       //MakeInParam("@PEEBIDNC",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIDNC ),
                                       //MakeInParam("@PEEBIWL",(DbType)SqlDbType.Float,53,_PEEBIEntity.PEEBIWL ),
									   MakeInParam("@PEEBIED",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIED ),
                                       //MakeInParam("@PEEBIGF",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIGF ),
                                       //MakeInParam("@PEEBIEL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEL ),
                                       //MakeInParam("@PBEDULN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBEDULN ),
                                       //MakeInParam("@PEEBILM",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBILM ),
                                       //MakeInParam("@PEEBIGD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIGD ),
                                       //MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIIN ),
                                       //MakeInParam("@PEEBIIP",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIIP ),
                                       //MakeInParam("@PEEBIVY",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIVY ),
                                       //MakeInParam("@PEEBIRT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIRT ),
                                       //MakeInParam("@PBRETTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBRETTN ),
                                       //MakeInParam("@PEEBIENA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIENA ),
                                       //MakeInParam("@PEEBIMI",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIMI ),
                                       //MakeInParam("@PEEBINP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBINP ),
                                       //MakeInParam("@PEEBIRA",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBIRA ),
                                       //MakeInParam("@PEEBISA",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBISA ),
									   MakeInParam("@PEEBICP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBICP ),
                                       //MakeInParam("@PEEBIECN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECN ),
                                       //MakeInParam("@PEEBIECP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECP ),
									   MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIES ),
                                    
                                       MakeInParam("@PEEBIID",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIID ),
                                       //MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBESTTN ),
                                       //MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEST ),
                                       //MakeInParam("@PEEBIESN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIESN ),
                                       //MakeInParam("@PEEBIEP",(DbType)SqlDbType.Image,_PEEBIEntity.PEEBIEP.Length,_PEEBIEntity.PEEBIEP ),
                                       //MakeInParam("@PEETNTD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEETNTD ),
                                       //MakeInParam("@PEEBIML",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBIML ),
                                       //MakeInParam("@PEEBIOAAU",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIOAAU ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append(" declare @PEEBIDEPID int; select @PEEBIDEPID=PBDEPID from PBDEP where PBDEPDC=@PEEBIDEP; ");
                sb.Append("INSERT INTO [dbo].[PEEBI]");
                sb.Append("(");
                //sb.Append("[PEEBIEPHORN]");
                sb.Append("[PEEBIEC]");
                sb.Append(",[PEEBIEN]");
                //sb.Append(",[PEEBIEEN]");
                //sb.Append(",[PEEBIETWN]");
                //sb.Append(",[PEEBIEG]");
                sb.Append(",[PEEBIBD]");
                //sb.Append(",[PEEBIEA]");
                sb.Append(",[PEEBIDEPID]");
                sb.Append(",[PEEBIDEP]");
                sb.Append(",[PEEBIDT]");
                sb.Append(",[PEEBIPC]");
                //sb.Append(",[PEEBIWT]");
                //sb.Append(",[PEEBIPT]");
                //sb.Append(",[PBPOCCC]");
                //sb.Append(",[PEEBIWP]");
                //sb.Append(",[PEEBIET]");
                sb.Append(",[PEEBIDL]");
                //sb.Append(",[PEEBIDN]");
                //sb.Append(",[PEEBIDNC]");
                //sb.Append(",[PEEBIWL]");
                sb.Append(",[PEEBIED]");
                //sb.Append(",[PEEBIGF]");
                //sb.Append(",[PEEBIEL]");
                //sb.Append(",[PEEBILM]");
                //sb.Append(",[PEEBIGD]");
                //sb.Append(",[PEEBIIN]");
                //sb.Append(",[PEEBIIP]");
                //sb.Append(",[PEEBIVY]");
                //sb.Append(",[PEEBIRT]");
                //sb.Append(",[PEEBIENA]");
                //sb.Append(",[PEEBIMI]");
                //sb.Append(",[PEEBINP]");
                //sb.Append(",[PEEBIRA]");
                //sb.Append(",[PEEBISA]");
                sb.Append(",[PEEBICP]");
                //sb.Append(",[PEEBIECN]");
                //sb.Append(",[PEEBIECP]");
                sb.Append(",[PEEBIES]");
                sb.Append(",[PEEBIID]");
                //sb.Append(",[PEEBIEST]");
                //sb.Append(",[PEEBIESN]");
                //sb.Append(",[PEEBIEP]");
                //sb.Append(",[PEETNTD]");
                //sb.Append(",[PEEBIML]");
                //sb.Append(",[PEEBIOAAU]");
                sb.Append(") ");
                sb.Append(" VALUES (");
               // sb.Append("@PEEBIEPHORN,");
                sb.Append("@PEEBIEC,");
                sb.Append("@PEEBIEN,");
                //sb.Append("@PEEBIEEN,");
                //sb.Append("@PEEBIETWN,");
                //sb.Append("@PEEBIEG,");
                sb.Append("@PEEBIBD,");
               // sb.Append("@PEEBIEA,");
                sb.Append("@PEEBIDEPID,");
                sb.Append("@PEEBIDEP,");
                sb.Append("@PEEBIDT,");
                sb.Append("@PEEBIPC,");
                //sb.Append("@PEEBIWT,");
                //sb.Append("@PEEBIPT,");
                //sb.Append("@PBPOCCC,");
                //sb.Append("@PEEBIWP,");
                //sb.Append("@PEEBIET,");
                sb.Append("@PEEBIDL,");
                //sb.Append("@PEEBIDN,");
                //sb.Append("@PEEBIDNC,");
                //sb.Append("@PEEBIWL,");
                sb.Append("@PEEBIED,");
                //sb.Append("@PEEBIGF,");
                //sb.Append("@PEEBIEL,");
                //sb.Append("@PEEBILM,");
                //sb.Append("@PEEBIGD,");
                //sb.Append("@PEEBIIN,");
                //sb.Append("@PEEBIIP,");
                //sb.Append("@PEEBIVY,");
                //sb.Append("@PEEBIRT,");
                //sb.Append("@PEEBIENA,");
                //sb.Append("@PEEBIMI,");
                //sb.Append("@PEEBINP,");
                //sb.Append("@PEEBIRA,");
                //sb.Append("@PEEBISA,");
                sb.Append("@PEEBICP,");
                //sb.Append("@PEEBIECN,");
                //sb.Append("@PEEBIECP,");
                sb.Append("@PEEBIES,");
                sb.Append("@PEEBIID");
                //sb.Append("@PEEBIEST,");
                //sb.Append("@PEEBIESN,");
                //sb.Append("@PEEBIEP,");
                //sb.Append("@PEETNTD,");
                //sb.Append("@PEEBIML,");
                //sb.Append("@PEEBIOAAU)");
                sb.Append(") ");
               // sb.Append(" if (Datediff(day,'1900-01-01',@PEEBIDNC)=0) begin upDate PEEBI set PEEBIDNC=null where [PEEBIEC]=@PEEBIEC end");
               // sb.Append(" if (Datediff(day,'1900-01-01',@PEETNTD)=0) begin upDate PEEBI set PEETNTD=null where [PEEBIEC]=@PEEBIEC end");
                sb.Append(" select @PEEBIEC;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_PEEBIEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpDatePEEBI(PEEBIEntity _PEEBIEntity)
        {
            //if (_PEEBIEntity.PEEBIEP == null)
            //{
            //    _PEEBIEntity.PEEBIEPHORN = "无";
            //}
            //else if (_PEEBIEntity.PEEBIEP.Length == 0)
            //{
            //    _PEEBIEntity.PEEBIEPHORN = "无";
            //}
            DbParameter[] pramsUpDate = {
                                      // MakeInParam("@PEEBIEPHORN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEPHORN ),
									   MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEC ),
									   MakeInParam("@PEEBIEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEN ),
                                       //MakeInParam("@PEEBIEEN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEEN ),
                                       //MakeInParam("@PEEBIETWN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIETWN ),
                                       //MakeInParam("@PEEBIEG",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIEG ),
									   MakeInParam("@PEEBIBD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIBD ),
									   //MakeInParam("@PEEBIEA",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIEA ),
									   MakeInParam("@PEEBIDEP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDEP ),
									   MakeInParam("@PEEBIDT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDT ),
									   MakeInParam("@PEEBIPC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIPC ),
                                       //MakeInParam("@PEEBIWT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWT ),
                                       //MakeInParam("@PEEBIPT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIPT ),
                                       //MakeInParam("@PBPOCCC",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBPOCCC ),
                                       //MakeInParam("@PEEBIWP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIWP ),
                                       //MakeInParam("@PEEBIET",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIET ),
									   MakeInParam("@PEEBIDL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDL ),
                                       //MakeInParam("@PEEBIDN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIDN ),
                                       //MakeInParam("@PBPRTTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBPRTTN ),
                                       //MakeInParam("@PEEBIDNC",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIDNC ),
                                       //MakeInParam("@PEEBIWL",(DbType)SqlDbType.Float,53,_PEEBIEntity.PEEBIWL ),
									   MakeInParam("@PEEBIED",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIED ),
                                       //MakeInParam("@PEEBIGF",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIGF ),
                                       //MakeInParam("@PEEBIEL",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEL ),
                                       //MakeInParam("@PBEDULN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBEDULN ),
                                       //MakeInParam("@PEEBILM",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBILM ),
                                       //MakeInParam("@PEEBIGD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIGD ),
                                       //MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIIN ),
                                       //MakeInParam("@PEEBIIP",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEEBIIP ),
                                       //MakeInParam("@PEEBIVY",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIVY ),
                                       //MakeInParam("@PEEBIRT",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIRT ),
                                       //MakeInParam("@PBRETTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBRETTN ),
                                       //MakeInParam("@PEEBIENA",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIENA ),
                                       //MakeInParam("@PEEBIMI",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIMI ),
                                       //MakeInParam("@PEEBINP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBINP ),
                                       //MakeInParam("@PEEBIRA",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBIRA ),
                                       //MakeInParam("@PEEBISA",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBISA ),
									   MakeInParam("@PEEBICP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBICP ),
                                       //MakeInParam("@PEEBIECN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECN ),
                                       //MakeInParam("@PEEBIECP",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIECP ),
									   MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIES ),

                                       MakeInParam("@PEEBIID",(DbType)SqlDbType.Int,4,_PEEBIEntity.PEEBIID ),
                                       //MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PBESTTN ),
                                       //MakeInParam("@PEEBIEST",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIEST ),
                                       //MakeInParam("@PEEBIESN",(DbType)SqlDbType.VarChar,50,_PEEBIEntity.PEEBIESN ),
                                       //MakeInParam("@PEEBIEP",(DbType)SqlDbType.Image,(_PEEBIEntity.PEEBIEP ==null)?0:_PEEBIEntity.PEEBIEP.Length,_PEEBIEntity.PEEBIEP ),
                                       //MakeInParam("@PEETNTD",(DbType)SqlDbType.DateTime,16,_PEEBIEntity.PEETNTD ),
                                       //MakeInParam("@PEEBIML",(DbType)SqlDbType.VarChar,100,_PEEBIEntity.PEEBIML ),
                                       //MakeInParam("@PEEBIOAAU",(DbType)SqlDbType.Char,1,_PEEBIEntity.PEEBIOAAU ),
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[PEEBI]");
            sb.Append(" set ");
            //sb.Append(" [PEEBIEPHORN]=@PEEBIEPHORN,");
            sb.Append(" [PEEBIEN]=@PEEBIEN,");
            //sb.Append(" [PEEBIEEN]=@PEEBIEEN,");
            //sb.Append(" [PEEBIETWN]=@PEEBIETWN,");
            //sb.Append(" [PEEBIEG]=@PEEBIEG,");
            sb.Append(" [PEEBIBD]=@PEEBIBD,");
            //sb.Append(" [PEEBIEA]=@PEEBIEA,");
            sb.Append(" [PEEBIDEP]=@PEEBIDEP,");
            sb.Append(" [PEEBIDT]=@PEEBIDT,");
            sb.Append(" [PEEBIPC]=@PEEBIPC,");
            //sb.Append(" [PEEBIWT]=@PEEBIWT,");
            //sb.Append(" [PEEBIPT]=@PEEBIPT,");
            //sb.Append(" [PBPOCCC]=@PBPOCCC,");
            //sb.Append(" [PEEBIWP]=@PEEBIWP,");
            //sb.Append(" [PEEBIET]=@PEEBIET,");
            //sb.Append(" [PEEBIDL]=@PEEBIDL,");
            //sb.Append(" [PEEBIDN]=@PEEBIDN,");
            //sb.Append(" [PEEBIDNC]=@PEEBIDNC,");
            //sb.Append(" [PEEBIWL]=@PEEBIWL,");
            //sb.Append(" [PEEBIED]=@PEEBIED,");
            //sb.Append(" [PEEBIGF]=@PEEBIGF,");
            //sb.Append(" [PEEBIEL]=@PEEBIEL,");
            //sb.Append(" [PEEBILM]=@PEEBILM,");
            //sb.Append(" [PEEBIGD]=@PEEBIGD,");
            //sb.Append(" [PEEBIIN]=@PEEBIIN,");
            //sb.Append(" [PEEBIIP]=@PEEBIIP,");
            //sb.Append(" [PEEBIVY]=@PEEBIVY,");
            //sb.Append(" [PEEBIRT]=@PEEBIRT,");
            //sb.Append(" [PBRETTN]=@PBRETTN,");
            //sb.Append(" [PEEBIENA]=@PEEBIENA,");
            //sb.Append(" [PEEBIMI]=@PEEBIMI,");
            //sb.Append(" [PEEBINP]=@PEEBINP,");
            //sb.Append(" [PEEBIRA]=@PEEBIRA,");
            //sb.Append(" [PEEBISA]=@PEEBISA,");
            sb.Append(" [PEEBICP]=@PEEBICP,");
            sb.Append(" [PEEBIID]=@PEEBIID,");
            //sb.Append(" [PEEBIECN]=@PEEBIECN,");
            //sb.Append(" [PEEBIECP]=@PEEBIECP,");
            sb.Append(" [PEEBIES]=@PEEBIES");
            //sb.Append(" [PEEBIEST]=@PEEBIEST,");
            //sb.Append(" [PEEBIESN]=@PEEBIESN,");
            //sb.Append(" [PEEBIEP]=@PEEBIEP, ");
            //sb.Append(" [PEETNTD]=@PEETNTD, ");
            //sb.Append(" [PEEBIML]=@PEEBIML, ");
            //sb.Append(" [PEEBIOAAU]=@PEEBIOAAU ");
            sb.Append(" where [PEEBIEC]=@PEEBIEC ");
          //  sb.Append(" if (Datediff(day,'1900-01-01',@PEEBIDNC)=0) begin upDate PEEBI set PEEBIDNC=null where [PEEBIEC]=@PEEBIEC end");
           // sb.Append(" if (Datediff(day,'1900-01-01',@PEETNTD)=0) begin upDate PEEBI set PEETNTD=null where [PEEBIEC]=@PEEBIEC end");
            sb.Append(" select @PEEBIEC ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }

        public DataTable sp_GetPEEBINameListByPEEBIIN(string PEEBIIN, string PEEBIES)
        {
            //arylst.Add(txtqPEEBIIN.Text);//员工身份证编号
            DbParameter[] prams = {   
                                        MakeInParam("@PEEBIIN",(DbType)SqlDbType.VarChar,50,PEEBIIN ),//员工身份证编号
                                        MakeInParam("@PEEBIES",(DbType)SqlDbType.VarChar,50,PEEBIES ),//员工状态
								   };

            string sql = "sp_GetPEEBINameListByPEEBIIN";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }

        /// <summary>
        /// 删除员工基本信息，以员工编号为参数
        /// </summary>
        /// <param name="WorkerTypeCode"></param>
        /// <returns></returns>
        public string DeletePEEBI(string PEEBIEC)
        {

            DbParameter[] prams = {
                                    MakeInParam("@PEEBIEC",(DbType)SqlDbType.VarChar,50,PEEBIEC),
                                   };
            string sql = "";

            sql = "delete from [PEEBI] where  PEEBIEC = @PEEBIEC ";

            ExecuteNonQuery(CommandType.Text, sql, prams);

            return "0";

        }


        public string DeletePEEBITEMP(string PEEBITEMPID)
        {

            DbParameter[] prams = {
                                    MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,PEEBITEMPID),
                                   };
            string sql = "";

            sql = "delete from [PEEBITEMP] where  PEEBITEMPID = @PEEBITEMPID ";

            ExecuteNonQuery(CommandType.Text, sql, prams);

            return "0";

        }


        public DataTable GetPEEBIInfoById(string szId)
        {
            string sql;
            sql = string.Format("select * from PEEBI where PEEBIEC='{0}'", szId);
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }


        public DataTable GetLastEmplyeeId()
        {
            string sql;
            sql = "select top 1 PEEBIEC from PEEBI  order by PEEBIEC desc";
            return ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public string AddPEEBITEMP(string szId)
        {
            DbParameter[] pramsInsert = {
									   MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szId ),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[PEEBITEMP]");
            sb.Append("(");
            sb.Append("[PEEBITEMPID]");
            sb.Append(") ");
            sb.Append(" VALUES (");
            sb.Append("@PEEBITEMPID");
            sb.Append(") ");
            sb.Append(" select @PEEBITEMPID;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
 
        }
        #endregion

        #region "附加员工信息 PEEBITEMP"

        //add Rio
        //往sys_TableColumn写入新添部门表栏位信息
        public string DoInsertSysTable(string szTableName, string szaddItemFlagName, string sztype, string szMsgValue)
        {

             DbParameter[] prams = {
                                           MakeInParam("@ColName",(DbType)SqlDbType.VarChar,50,szaddItemFlagName),
                                          
                                     };
             string sql = "select * from [sys_TableColumn] where   ColName=@ColName ";
             if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count == 0)
             {
                 DbParameter[] pramsInsert = {
                                       MakeInParam("@TableName",(DbType)SqlDbType.VarChar,50,szTableName),
									   MakeInParam("@ColName",(DbType)SqlDbType.VarChar,50,szaddItemFlagName),
									   MakeInParam("@ColType",(DbType)SqlDbType.VarChar,50,sztype),
                                       MakeInParam("@ColDescriptionCN",(DbType)SqlDbType.VarChar,50,szMsgValue),
                                        MakeInParam("@ColIsShow",(DbType)SqlDbType.Char,1,'0'),
                                        MakeInParam("@ColWidth",(DbType)SqlDbType.Int,4,100),

                                             };
                 StringBuilder sb = new StringBuilder();
                 sb.Append("INSERT INTO [dbo].[sys_TableColumn]");
                 sb.Append("(");
                 sb.Append("[TableName],");
                 sb.Append("[ColName],");
                 sb.Append("[ColType],");
                 sb.Append("[ColDescriptionCN],");
                 sb.Append("[ColIsShow],");
                 sb.Append("[ColWidth]");
                 sb.Append(")");
                 sb.Append("VALUES");
                 sb.Append("(");
                 sb.Append("@TableName,");
                 sb.Append("@ColName,");
                 sb.Append("@ColType,");
                 sb.Append("@ColDescriptionCN,");
                 sb.Append("@ColIsShow,");
                 sb.Append("@ColWidth");
                 sb.Append(")");
                 sb.Append(" select @@identity;");
                 return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
             }
             else
                 return "";
        }

        //add Rio
        //根据ColName删除sys_TableColumn中的信息
        public string DeleteSysTableColumnInfor(string szColname)
        {
            string sql;

            sql = string.Format("delete from sys_TableColumn where ColName='{0}'", szColname);

            return ExecuteNonQuery(sql).ToString();
        }

        //add Rio
        //往PEEBITEMP表中添加新增加的员工信息
        public string AddTempDeptInforTemp(string szPEEBIID, string szItemName, string szItemValue)
        {


            DbParameter[] prams = {
                                           MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szPEEBIID),
                                          
                                     };
            string sql = "select * from [PEEBITEMP] where   PEEBITEMPID=@PEEBITEMPID ";
            StringBuilder sb = new StringBuilder();
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0) //判断主键是否有值
            {
                string strTemp = "@" + szItemName;
                DbParameter[] pramsInsert = {
                                       MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szPEEBIID),
									   MakeInParam(strTemp,(DbType)SqlDbType.VarChar,50,szItemValue),
                                    
                                             };

                sb.Append("upDate [dbo].[PEEBITEMP] set ");
                sb.Append(szItemName);
                sb.Append("=");
                sb.Append(strTemp);
                sb.Append(" where  PEEBITEMPID=@PEEBITEMPID");


                return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
            else
            {
                string strTemp = "@" + szItemName;
                DbParameter[] pramsInsert = {
                                       MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szPEEBIID),
									   MakeInParam(strTemp,(DbType)SqlDbType.VarChar,50,szItemValue),
                                    
                                             };

                sb.Append("INSERT INTO [dbo].[PEEBITEMP]");
                sb.Append("(");
                sb.Append("[PEEBITEMPID],");
                sb.Append(szItemName);
                sb.Append(")");
                sb.Append("VALUES");
                sb.Append("(");
                sb.Append("@PEEBITEMPID,");
                sb.Append(strTemp);
                sb.Append(")");
                sb.Append(" select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

            }

        }

        //add Rio
        //更新sys_TableColumn表中的ColIsShow 
        public string UpDateSysColumShow(string szTableName, string szItemName)
        {
            string strTemp = "@" + szItemName;
            DbParameter[] pramsInsert = {
                                       MakeInParam("@TableName",(DbType)SqlDbType.VarChar,50,szTableName),
									   MakeInParam("@ColName",(DbType)SqlDbType.VarChar,50,szItemName),
                                       MakeInParam("@ColIsShow",(DbType)SqlDbType.Char,1,'1'),
                                    
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("upDate [dbo].[sys_TableColumn] set ");
            sb.Append("[ColIsShow]=@ColIsShow");
            sb.Append(" where  TableName=@TableName  and ColName=@ColName");
            return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsInsert).ToString();

        
        }


        //add Rio
        //更新PEEBITEMP表
        public string UpDateTempDeptInforTemp(string szPEEBIID, string szItemName, string szItemValue)
        {
             string strTemp = "@" + szItemName;
                DbParameter[] pramsInsert = {
                                       MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szPEEBIID),
									   MakeInParam(strTemp,(DbType)SqlDbType.VarChar,50,szItemValue),
                                    
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("upDate [dbo].[PEEBITEMP] set ");
                sb.Append(szItemName);
                sb.Append("=");
                sb.Append(strTemp);
                sb.Append(" where  PEEBITEMPID=@PEEBITEMPID");


                return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsInsert).ToString();

        }

        //add Rio
        //通过szPEEBIID删除PEEBITEMP表中的数据
        public string DeleteDeptTempInfor(string szPEEBIID)
        {
            string sql;

            sql = string.Format("delete from PEEBITEMP where PEEBITEMPID='{0}'", szPEEBIID);

            return ExecuteNonQuery(sql).ToString();
        }


        //add Rio
        //根据ID获取PEEBITEMP表中的数据
        public DataTable DoGetAddItem(string szid)
        {
            DbParameter[] prams = {
                                           MakeInParam("@PEEBITEMPID",(DbType)SqlDbType.VarChar,50,szid),
                                          
                                     };
            string sql = "select * from [PEEBITEMP] where   PEEBITEMPID=@PEEBITEMPID ";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }


        //add Rio
        //往PEEBITEMP表中添加新的字段
        public int DoInsertItem(string name, string type)
        {

            DbParameter[] prams = {   
                                        MakeInParam("@ProductTempName",(DbType)SqlDbType.VarChar,50,name ),
                                        MakeInParam("@ProductTempType",(DbType)SqlDbType.VarChar,50,type ),
								   };

            string sql = "ProductTemp";
            return ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);

        }

        //add Rio 
        //删除PEEBITEMP或PEEBI表中的字段
        public int DeletePEEBIITEM(string name)
        {
            DbParameter[] prams = {   
                                        MakeInParam("@ProductTempName",(DbType)SqlDbType.VarChar,50,name ),
                                       
								   };

            string sql = "DeleteItem";
            return ExecuteNonQuery(CommandType.StoredProcedure, sql, prams);
        }


        #endregion


        #region "部门信息 PBDEP"


        /// <summary>
        /// 新增部门信息
        /// </summary>
        /// <param name="_PBDEPEntity"></param>
        /// <returns>返回string "-1"表示部门名称已经存在，否则表示新增成功</returns>
        public string AddDeptInfor(PBDEPEntity _PBDEPEntity)
        {

            //判断是否存在该部门名称
            DbParameter[] prams = { MakeInParam("@DeptName",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptName),
                                    MakeInParam("@ParentDeptCode",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.ParentDeptCode),
                                     };
            string sql = "select * from [PBDEP] where  PBDEPDN = @DeptName and PBDEPPDC=@ParentDeptCode ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {

                DbParameter[] pramsInsert = {
									   MakeInParam("@PBDEPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptCode),
									   MakeInParam("@PBDEPDN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptName),
                                       MakeInParam("@PBDEPDEN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptEName),
                                       MakeInParam("@PBDEPDTWN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptTWName),
									   MakeInParam("@PBDEPPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.ParentDeptCode),
									   MakeInParam("@PBDEPUS",(DbType)SqlDbType.Char,1,_PBDEPEntity.DeptIsValid),
                                       MakeInParam("@PBDEPOI",(DbType)SqlDbType.Int,4,_PBDEPEntity.DeptOrderItem ),
                                       MakeInParam("@PBDEPPID",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.ParentDeptID ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PBDEP]");
                sb.Append("([PBDEPDC]");
                sb.Append(",[PBDEPDN]");
                sb.Append(",[PBDEPDEN]");
                sb.Append(",[PBDEPDTWN]");
                sb.Append(",[PBDEPPDC]");
                sb.Append(",[PBDEPPID]");
                sb.Append(",[PBDEPOI]");
                sb.Append(",[PBDEPUS])");
                sb.Append("VALUES");
                sb.Append("(@PBDEPDC");
                sb.Append(",@PBDEPDN");
                sb.Append(",@PBDEPDEN");
                sb.Append(",@PBDEPDTWN,");
                sb.Append("@PBDEPPDC, ");
                sb.Append("@PBDEPPID, ");
                sb.Append("@PBDEPOI, ");
                sb.Append("@PBDEPUS)");
                sb.Append(" select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

            }
        }




        public DataTable GetDeptInfor()
        {
            string sql = "";
            sql = "select * from [PBDEP]";
            return ExecuteDataset(sql).Tables[0];
        }


          
        /// <summary>
        /// 根据部门名称获得部门信息,当参数"是否启用PBDEPUS"的值为空的话，则抓全部部门资料
        /// </summary>
        /// <param name="PBDEPUS"></param>
        /// <param name="PBDEPDN"></param>
        /// <returns></returns>
        public DataTable GetDeptInforbyDeptName(string PBDEPUS, string PBDEPDN)
        {

            DbParameter[] prams = {
									   MakeInParam("@PBDEPUS", (DbType)SqlDbType.VarChar, 1, PBDEPUS),
									   MakeInParam("@PBDEPDN", (DbType)SqlDbType.VarChar, 50, PBDEPDN)
								   };
            string sql = "";
            if (PBDEPUS == "")
                sql = "select * from [PBDEP] Order by PBDEPDN ";
            else
                sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPDN like @PBDEPDN +'%' Order by PBDEPDN";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];



        }
        /// <summary>
        /// 根据部门代码获得部门信息,当参数"是否启用PBDEPUS"的值为空的话，则抓全部部门资料
        /// </summary>
        /// <param name="PBDEPUS"></param>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetDeptInforbyDeptCode(string PBDEPUS, string PBDEPDC)
        {
            DbParameter[] prams = {
									   MakeInParam("@PBDEPUS", (DbType)SqlDbType.VarChar, 1, PBDEPUS),
									   MakeInParam("@PBDEPDC", (DbType)SqlDbType.VarChar, 50, PBDEPDC)
								   };
            string sql = "";
            if (PBDEPUS == "")
                sql = "select * from [PBDEP] Order by PBDEPDN ";
            else
                sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPDC like @PBDEPDC +'%' Order by PBDEPDN";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }


        /// <summary>
        /// 根据部门ID获得部门信息,当参数"是否启用PBDEPUS"的值为空的话，则抓全部部门资料
        /// </summary>
        /// <param name="PBDEPUS"></param>
        /// <param name="PBDEPID"></param>
        /// <returns></returns>
        //public DataTable GetDeptInforbyDeptID(string PBDEPUS, string PBDEPID)
        //{
        //    DbParameter[] prams = {
        //                               MakeInParam("@PBDEPUS", (DbType)SqlDbType.VarChar, 1, PBDEPUS),
        //                               MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar, 50, PBDEPID)
        //                           };
        //    string sql = "";
        //    if (PBDEPUS == "")
        //        sql = "select * from [PBDEP] where PBDEPID=@PBDEPID Order by PBDEPDN ";
        //    else
        //        sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPID=@PBDEPID  Order by PBDEPDN";

        //    return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        //}

        /// <summary>
        /// 根据部门代码获得上级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetParentDeptbyDeptCode(string PBDEPDC)
        {
            DbParameter[] prams = {
									   
									   MakeInParam("@PBDEPDC", (DbType)SqlDbType.VarChar, 50, PBDEPDC),
								   };
            string sql = "";
            //sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPDC like @PBDEPDC +'%' Order by PBDEPDN";
            sql = "select b.* from [PBDEP] a,[PBDEP] b where  a.PBDEPDC=@PBDEPDC and b.PBDEPDC=a.PBDEPPDC Order by b.PBDEPDN";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }


        /// <summary>
        /// 根据部门ID获得上级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetParentDeptbyDeptID(string PBDEPID)
        {
            DbParameter[] prams = {
									   
									   MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar, 50, PBDEPID),
								   };
            string sql = "";
            //sql = "select * from [PBDEP] where [PBDEPUS] =@PBDEPUS and PBDEPDC like @PBDEPDC +'%' Order by PBDEPDN";
            sql = "select b.* from [PBDEP] a,[PBDEP] b where  a.PBDEPID=@PBDEPID and b.PBDEPID=a.PBDEPPID Order by b.PBDEPDN";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }


        /// <summary>
        /// 根据部门代码获得下级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetChildDeptbyDeptCode(string PBDEPDC)
        {
            DbParameter[] prams = { MakeInParam("@PBDEPDC", (DbType)SqlDbType.VarChar, 50, PBDEPDC),
								   };
            string sql = "";
            sql = "select * from [PBDEP] where [PBDEPPDC] =@PBDEPDC Order by PBDEPDN";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }


        /// <summary>
        /// 根据部门ID获得下级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        //public DataTable GetChildDeptbyDeptID(string PBDEPID)
        //{
        //    DbParameter[] prams = { MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar, 50, PBDEPID),
        //                           };
        //    string sql = "";
        //    sql = "select * from [PBDEP] where [PBDEPPID] =@PBDEPID Order by PBDEPDN";
        //    return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        //}

        /// <summary>
        /// 根据部门代码获得下级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetChildDeptbyDeptCode(string PBDEPDC, string NotUseToView)
        {
            DbParameter[] prams = {
                MakeInParam("@PBDEPDC", (DbType)SqlDbType.VarChar, 50, PBDEPDC),
            };
            string sql = "";
            if (NotUseToView.Equals("1"))
            {
                sql = "select * from [PBDEP] where [PBDEPPDC] =@PBDEPDC Order by PBDEPDN";
            }
            else
            {
                sql = "select * from [PBDEP] where [PBDEPPDC] =@PBDEPDC and PBDEPUS='1' Order by PBDEPDN";
            }
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }


        /// <summary>
        /// 根据部门ID获得下级部门信息
        /// </summary>
        /// <param name="PBDEPDC"></param>
        /// <returns></returns>
        public DataTable GetChildDeptbyDeptID(string PBDEPID, string NotUseToView)
        {
            DbParameter[] prams = {
                MakeInParam("@PBDEPID", (DbType)SqlDbType.VarChar,50, PBDEPID),
            };
            string sql = "";
            if (NotUseToView.Equals("1"))
            {
                sql = "select * from [PBDEP] where [PBDEPPID] =@PBDEPID Order by PBDEPDN";
            }
            else
            {
                sql = "select * from [PBDEP] where [PBDEPPID] =@PBDEPID and PBDEPUS='1' Order by PBDEPDN";
            }
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 根据DataRow获取部门实体
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        //public PBDEPEntity GetDeptEntity(DataRow dataRow)
        //{

        //    PBDEPEntity _PBDEPEntity = new PBDEPEntity();
        //    _PBDEPEntity.DeptID = dataRow["PBDEPID"].ToString();
        //    _PBDEPEntity.DeptCode = dataRow["PBDEPDC"].ToString();
        //    _PBDEPEntity.DeptName = dataRow["PBDEPDN"].ToString();
        //    _PBDEPEntity.DeptEName = dataRow["PBDEPDEN"].ToString();
        //    _PBDEPEntity.DeptTWName = dataRow["PBDEPDTWN"].ToString();
        //    _PBDEPEntity.ParentDeptID = dataRow["PBDEPPID"].ToString();
        //    _PBDEPEntity.ParentDeptCode = dataRow["PBDEPPDC"].ToString();
        //    _PBDEPEntity.DeptOrderItem = Convert.ToInt32(dataRow["PBDEPOI"].ToString());
        //    _PBDEPEntity.DeptIsValid = dataRow["PBDEPUS"].ToString();

        //    return _PBDEPEntity;

        //}


        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public PBDEPEntity GetPBDEPEntityByKeyCol(string PBDEPDC)
        {
            string sql = "select * from PBDEP Where PBDEPDC=@PBDEPDC";
            DbParameter[] pramsGet = {
									   MakeInParam("@PBDEPDC",(DbType)SqlDbType.VarChar,50,PBDEPDC ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetPBDEPFromIDataReader(sr);
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
        private PBDEPEntity GetPBDEPFromIDataReader(DbDataReader dr)
        {
            PBDEPEntity dt = new PBDEPEntity();
            if (dr.FieldCount > 0)
            {
                dt.DeptID = dr["PBDEPID"].ToString();
                dt.DeptCode = dr["PBDEPDC"].ToString();
                dt.DeptName = dr["PBDEPDN"].ToString();
                dt.DeptEName = dr["PBDEPDEN"].ToString();
                dt.DeptTWName = dr["PBDEPDTWN"].ToString();
                dt.ParentDeptID = dr["PBDEPPID"].ToString();
                dt.ParentDeptCode = dr["PBDEPPDC"].ToString();
                dt.DeptIsValid = dr["PBDEPUS"].ToString();


                if (dr["PBDEPOI"].ToString() != "" || dr["PBDEPOI"] != null) dt.DeptOrderItem = Int32.Parse(dr["PBDEPOI"].ToString());
                else
                    dt.DeptOrderItem = 0;
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 根据DataTable获取部门实体的数组
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        //public PBDEPEntity[] GetDeptEntityArray(DataTable dataTable)
        //{
        //    int arrayItems = dataTable.Rows.Count;

        //    PBDEPEntity[] deptArray = new PBDEPEntity[arrayItems];
        //    for (int i = 0; i < arrayItems; i++)
        //    {
        //        deptArray[i] = GetDeptEntity(dataTable.Rows[i]);

        //    }
        //    return deptArray;


        //}
        /// <summary>
        /// 修改部门信息
        /// </summary>
        /// <param name="_PBDEPEntity"></param>
        /// <returns>返回string "-1"表示部门名称已经存在，否则表示修改成功</returns>

        public string UpDateDeptInfor(PBDEPEntity _PBDEPEntity)
        {

            if (!_PBDEPEntity.DeptIsValid.Equals("1"))
            {
                string sqlCount = "select count(*) from PEEBI where PEEBIES != '02' and PEEBIDEP like '" + _PBDEPEntity.DeptCode + "%'";
                if (Convert.ToInt32(ExecuteScalar(CommandType.Text, sqlCount)) > 0)
                {
                    return "-2";
                }
            }
            //判断是否存在该部门名称
            DbParameter[] prams = {
                MakeInParam("@DeptName",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptName),
                MakeInParam("@DeptCode",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptCode),
                MakeInParam("@PBDEPPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.ParentDeptCode),
            };
            string sql = "select * from [PBDEP] where [PBDEPDN] = @DeptName and [PBDEPDC]!=@DeptCode and [PBDEPPDC]=@PBDEPPDC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {
                DbParameter[] pramsUpDate = {
									   MakeInParam("@PBDEPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptCode),
									   MakeInParam("@PBDEPDN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptName),
                                       MakeInParam("@PBDEPDEN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptEName),
                                       MakeInParam("@PBDEPDTWN",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptTWName),
									   MakeInParam("@PBDEPPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.ParentDeptCode),
									   MakeInParam("@PBDEPOI",(DbType)SqlDbType.Int,4,_PBDEPEntity.DeptOrderItem),
                                       MakeInParam("@PBDEPUS",(DbType)SqlDbType.Char,1,_PBDEPEntity.DeptIsValid)
                                        };
                StringBuilder sb = new StringBuilder();
                sb.Append("UpDate [dbo].[PBDEP] set ");
                sb.Append("[PBDEPDN]=@PBDEPDN");
                sb.Append(",[PBDEPDEN]=@PBDEPDEN");
                sb.Append(",[PBDEPDTWN]=@PBDEPDTWN");
                sb.Append(",[PBDEPPDC]=@PBDEPPDC");
                sb.Append(",[PBDEPOI]=@PBDEPOI");
                sb.Append(",[PBDEPUS]=@PBDEPUS");
                sb.Append(" where [PBDEPDC]=@PBDEPDC");
                sb.Append(" select @PBDEPDC;");

                //return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
                return ExecuteNonQuery(CommandType.Text, sb.ToString(), pramsUpDate).ToString();

            }

        }


        public string UpDateDeptInfor(PBDEPEntity _PBDEPEntity, string NewPBDEPPID)
        {


            string connString = ConnectionString;
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand();
            SqlTransaction transaction = null;
            cmd.Connection = conn;
            try
            {
                conn.Open();
                transaction = conn.BeginTransaction();
                cmd.Transaction = transaction;

                string PBDEPID = _PBDEPEntity.DeptID;//部门ID  

                //先获得新上级部门的部门代码
                string NewPBDEPPDC = "";
                string sql = "select * from PBDEP where PBDEPID='" + NewPBDEPPID + "'";
                cmd.CommandText = sql;
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read() || NewPBDEPPID == "0")   //判断NewPBDEPPID==0， 是更改为一级部门的情况
                {
                    if (NewPBDEPPID == "0")
                        NewPBDEPPDC = "";
                    else
                        NewPBDEPPDC = rd["PBDEPDC"].ToString();

                    rd.Close();
                    //获得变更后的部门代码
                    //
                    string NewPBDEPDC = GetPBDEPDCbyPBDEPPDC(NewPBDEPPDC, cmd);
                    //做更新
                    string sqlUpDate = "upDate PBDEP set PBDEPDC='" + NewPBDEPDC + "',PBDEPDN='" + _PBDEPEntity.DeptName + "',PBDEPDEN='" + _PBDEPEntity.DeptEName + "',PBDEPDTWN='" + _PBDEPEntity.DeptTWName + "',PBDEPPDC='" + NewPBDEPPDC + "',PBDEPUS='" + _PBDEPEntity.DeptIsValid + "',PBDEPPID='" + NewPBDEPPID + "' where PBDEPID='" + PBDEPID + "'";
                    cmd.CommandText = sqlUpDate;
                    int i = cmd.ExecuteNonQuery();

                    if (i == 1)
                    {
                        //看此部门代码下是否有子级代码
                        //DataTable dt = GetChildDeptbyDeptID(PBDEPID);
                        string sqldt = "select * from [PBDEP] where [PBDEPPID] ='" + PBDEPID + "'";
                        cmd.CommandText = sqldt;
                        SqlDataAdapter ad = new SqlDataAdapter();
                        ad.SelectCommand = cmd;
                        DataSet ds = new DataSet();
                        ad.Fill(ds);
                        DataTable dt = ds.Tables[0];

                        //有的话要改其部门代码，上级部门代码
                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                string childResult = UpDateChildDept(dt.Rows[j]["PBDEPID"].ToString(), NewPBDEPDC, cmd);
                                if (childResult == "-1")
                                {
                                    transaction.Rollback();
                                    conn.Close();
                                    return "-1";

                                }
                            }
                        }

                        //1。更新人事基本资料中的部门代码
                        string sqlUpDatePEEBI = "upDate PEEBI set PEEBIDEP=b.PBDEPDC from PEEBI a,PBDEP b where a.PEEBIDEPID=b.PBDEPID";
                        cmd.CommandText = sqlUpDatePEEBI;
                        cmd.ExecuteNonQuery();

                        //2。更新部门异动信息
                        string sqlUpDatePEDEC = @"
 upDate PEDEC set PEDECPR=PBDEPDC from PBDEP where PEDECPRID=PBDEPID;
 upDate PEDEC set PEDECNE=PBDEPDC from PBDEP where PEDECNEID=PBDEPID;
";
                        cmd.CommandText = sqlUpDatePEDEC;
                        cmd.ExecuteNonQuery();

                        string sqlUpDatePKDSI = " upDate PKDSI set PKDSIDC=PBDEPDC from PBDEP where PKDSIDID=PBDEPID; ";
                        cmd.CommandText = sqlUpDatePKDSI;
                        cmd.ExecuteNonQuery();


                        string sqlUpDatePKTPT = " upDate PKTPT set PKTPTDC=PBDEPDC from PBDEP where PKTPTDID=PBDEPID; ";
                        cmd.CommandText = sqlUpDatePKTPT;
                        cmd.ExecuteNonQuery();

                        transaction.Commit();
                        conn.Close();
                        return "1";


                    }
                    else
                    {
                        transaction.Rollback();
                        conn.Close();
                        return "-1";
                    }



                }
                else
                {
                    transaction.Rollback();
                    conn.Close();
                    return "-1";
                }
            }
            catch
            {
                transaction.Rollback();
                conn.Close();
                return "-1";
            }
        }

        private string UpDateChildDept(string PBDEPID, string NewPBDEPPDC, SqlCommand cmd)
        {
            try
            {
                //获得变更后的部门代码
                string NewPBDEPDC = GetPBDEPDCbyPBDEPPDC(NewPBDEPPDC, cmd);
                //做更新
                string sqlUpDate = "upDate PBDEP set PBDEPDC='" + NewPBDEPDC + "',PBDEPPDC='" + NewPBDEPPDC + "' where PBDEPID='" + PBDEPID + "'";
                cmd.CommandText = sqlUpDate;
                int i = cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    //看此部门代码下是否有子级代码
                    //DataTable dt = GetChildDeptbyDeptID(PBDEPID);
                    string sqldt = "select * from [PBDEP] where [PBDEPPID] ='" + PBDEPID + "'";
                    cmd.CommandText = sqldt;
                    SqlDataAdapter ad = new SqlDataAdapter();
                    ad.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    ad.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    //有的话要改其部门代码，上级部门代码
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            string childResult = UpDateChildDept(dt.Rows[j]["PBDEPID"].ToString(), NewPBDEPDC, cmd);
                            if (childResult == "-1")
                            {
                                return "-1";
                            }
                        }
                    }
                    return "0";
                }
                else
                {
                    return "-1";
                }
            }
            catch
            {
                return "-1";
            }
        }


        private string GetPBDEPDCbyPBDEPPDC(string PBDEPPDC, SqlCommand cmd)
        {

            string result = "-1";

            string sql = "";

            sql = "select PBDEPDC from PBDEP where PBDEPPDC='" + PBDEPPDC + "'";
            cmd.CommandText = sql;
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    if (PBDEPPDC == "")
                    {
                        result = PBDEPPDC + "10";

                    }
                    else
                    {
                        result = PBDEPPDC + "01";
                    }
                    dr.Close();
                }
                else
                {
                    dr.Close();
                    sql = "select max(PBDEPDC) as PBDEPDC from PBDEP where PBDEPPDC='" + PBDEPPDC + "'";
                    cmd.CommandText = sql;
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string currentDeptCode = dr["PBDEPDC"].ToString();
                        result = Convert.ToString((Convert.ToInt64(currentDeptCode) + 1));
                        dr.Close();
                        //return GetPBDEPFromIDataReader(sr);
                    }
                    //throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
                }
            }
            catch
            {
                dr.Close();
                result = "-1";
                //throw exp;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }

            return result;






        }

        /// <summary>
        /// 修改多个部门信息
        /// </summary>
        /// <param name="_PBDEPEntity"></param>
        /// <returns>返回string "0"表示全部修改成功，否则返回已经存在部门名称</returns>
        public string UpDateDeptInfor(PBDEPEntity[] _PBDEPEntity)
        {

            for (int i = 0; i < _PBDEPEntity.Length; i++)
            {
                string upDateResult = UpDateDeptInfor(_PBDEPEntity[i]);
                if (upDateResult == "-1")
                {
                    return _PBDEPEntity[i].DeptName;
                }

            }
            return "0";


        }
        /// <summary>
        /// 删除部门信息，以部门代码为参数
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public string DeleteDeptInfor(string DeptCode)
        {

            DbParameter[] prams = {
                MakeInParam("@PBDEPDC",(DbType)SqlDbType.VarChar,50,DeptCode),
            };

            int employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES != '02' and PEEBIDEP=@PBDEPDC", prams));
            if (employeeCount > 0)
            {
                return "-3";
            }

            employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES = '02' and PEEBIDEP=@PBDEPDC", prams));
            if (employeeCount > 0)
            {
                return "-2";
            }
            string sql = "if not exists (select * from PEEBI where PEEBIDEP=@PBDEPDC) delete from [PBDEP] where  PBDEPDC = @PBDEPDC ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }


        /// <summary>
        /// 删除部门信息，以部门ID为参数
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public string DeleteDeptInforbyID(string DeptID)
        {

            DbParameter[] prams = {
                MakeInParam("@PBDEPID",(DbType)SqlDbType.VarChar,50,DeptID),
            };

            int employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES != '02' and PEEBIDEPID=@PBDEPID", prams));
            if (employeeCount > 0)
            {
                return "-3";
            }

            employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES = '02' and PEEBIDEPID=@PBDEPID", prams));
            if (employeeCount > 0)
            {
                return "-2";
            }
            string sql = "if not exists (select * from PEEBI where PEEBIDEPID=@PBDEPID) delete from [PBDEP] where  PBDEPID = @PBDEPID ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }

        /// <summary>
        /// 删除部门信息，以部门实体为参数
        /// </summary>
        /// <param name="_PBDEPEntity"></param>
        /// <returns></returns>
        public string DeleteDeptInfor(PBDEPEntity _PBDEPEntity)
        {

            DbParameter[] prams = {
                                    MakeInParam("@PBDEPDC",(DbType)SqlDbType.VarChar,50,_PBDEPEntity.DeptCode),
                                   };
            string sql = "";

            sql = "delete from [PBDEP] where  PBDEPDC = @PBDEPDC ";

            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }


        /// <summary>
        /// 删除部门信息，以部门实体数组为参数
        /// </summary>
        /// <param name="_PBDEPEntity"></param>
        /// <returns></returns>
        public string DeleteDeptInfor(PBDEPEntity[] _PBDEPEntity)
        {
            for (int i = 0; i < _PBDEPEntity.Length; i++)
            {
                DeleteDeptInfor(_PBDEPEntity[i]);
            }
            return "0";
        }

        /// <summary>
        /// 获得下级部门的最大部门代码，返回一个新下级代码，此处以后要将其写的严谨些，比如要考虑每级部门的代码长度等
        /// </summary>
        /// <param name="PBDEPPDC">上级部门代码</param>
        /// <returns>-1表示抓取失败</returns>
        public string GetPBDEPDCbyPBDEPPDC(string PBDEPPDC)
        {
            string result = "-1";
            DbParameter[] prams = {
                                    MakeInParam("@PBDEPPDC",(DbType)SqlDbType.VarChar,50,PBDEPPDC),
                                   };
            string sql = "";

            sql = "select PBDEPDC from PBDEP where PBDEPPDC=@PBDEPPDC";

            DbDataReader dr = null;
            try
            {
                dr = ExecuteReader(CommandType.Text, sql, prams);
                if (!dr.HasRows)
                {
                    if (PBDEPPDC == "")
                    {
                        result = PBDEPPDC + "10";

                    }
                    else
                    {
                        result = PBDEPPDC + "01";
                    }
                    dr.Close();
                }
                else
                {
                    sql = "select max(PBDEPDC) as PBDEPDC from PBDEP where PBDEPPDC=@PBDEPPDC";
                    dr = ExecuteReader(CommandType.Text, sql, prams);
                    if (dr.Read())
                    {
                        string currentDeptCode = dr["PBDEPDC"].ToString();
                        result = Convert.ToString((Convert.ToInt64(currentDeptCode) + 1));
                        dr.Close();
                        //return GetPBDEPFromIDataReader(sr);
                    }
                    //throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
                }
            }
            catch
            {
                dr.Close();
                result = "-1";
                //throw exp;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }

            return result;
            //return "0";

        }


        #endregion


        #region "岗位信息  PBPOS"

        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="_PBPOSEntity"></param>
        /// <returns>返回string "-1"所属部门的岗位的名称已经存在,否则新增成功 </returns>
        public string AddPBPOS(PBPOSEntity _PBPOSEntity)
        {
            //判断该记录是否已经存在
            //判断该岗位名称是否已经存在
            DbParameter[] prams = {
                                      MakeInParam("@PBPOSPN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPN),
                                      MakeInParam("@PBDUTDC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBDUTDC ),
                                      MakeInParam("@PBPOTTC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOTTC ),
                                     };
            string sql = "select * from [PBPOS] where  PBPOSPN=@PBPOSPN and PBDUTDC=@PBDUTDC and PBPOTTC=@PBPOTTC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                _PBPOSEntity.PBPOSPC = px_Sequence("PBPOSCODE", "1");
                DbParameter[] pramsInsert = {
                                        MakeInParam("@PBPOSPC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPC ),
                                        MakeInParam("@PBPOSPN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPN ),
                                        MakeInParam("@PBPOSPEN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPEN ),
                                        MakeInParam("@PBPOSPTWN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPTWN ),
                                        MakeInParam("@PBPOSBD",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSBD ),
                                        MakeInParam("@PBPOSPD",(DbType)SqlDbType.VarChar,200,_PBPOSEntity.PBPOSPD ),
                                        MakeInParam("@PBPOSRB",(DbType)SqlDbType.VarChar,100,_PBPOSEntity.PBPOSRB ),
                                        MakeInParam("@PBPOSRBN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSRBN ),
                                        MakeInParam("@PBPOSUS",(DbType)SqlDbType.Char,1,_PBPOSEntity.PBPOSUS ),
                                        MakeInParam("@PBPOSOI",(DbType)SqlDbType.Int,4,_PBPOSEntity.PBPOSOI ),
                                        MakeInParam("@PBPOTTC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOTTC ),
                                        MakeInParam("@PBDUTDC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBDUTDC ),
                                        MakeInParam("@PBPOCCC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOCCC ),
                                        MakeInParam("@PBPOSWP",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSWP ),
                                        MakeInParam("@PBPOSET",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSET ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PBPOS]");
                sb.Append("(");
                sb.Append("[PBPOSPC]");
                sb.Append(",[PBPOSPN]");
                sb.Append(",[PBPOSPEN]");
                sb.Append(",[PBPOSPTWN]");
                sb.Append(",[PBPOSBD]");
                sb.Append(",[PBPOSPD]");
                sb.Append(",[PBPOSRB]");
                sb.Append(",[PBPOSRBN]");
                sb.Append(",[PBPOSUS]");
                sb.Append(",[PBPOSOI]");
                sb.Append(",[PBPOTTC]");
                sb.Append(",[PBDUTDC]");
                sb.Append(",[PBPOCCC]");
                sb.Append(",[PBPOSWP]");
                sb.Append(",[PBPOSET]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@PBPOSPC,");
                sb.Append("@PBPOSPN,");
                sb.Append("@PBPOSPEN,");
                sb.Append("@PBPOSPTWN,");
                sb.Append("@PBPOSBD,");
                sb.Append("@PBPOSPD,");
                sb.Append("@PBPOSRB,");
                sb.Append("@PBPOSRBN,");
                sb.Append("@PBPOSUS,");
                sb.Append("@PBPOSOI,");
                sb.Append("@PBPOTTC,");
                sb.Append("@PBDUTDC,");
                sb.Append("@PBPOCCC,");
                sb.Append("@PBPOSWP,");
                sb.Append("@PBPOSET )");
                sb.Append(" select @PBPOSPC;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }
        }

        /// <summary>
        /// 根据岗位代码获得岗位信息,当参数"是否启用PBPOSUS"的值为空的话，则抓全部岗位资料
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public PBPOSEntity GetPBPOSEntityByKeyCol(string PBPOSUS, string PBPOSPC)
        {
            string sql = "";
            sql = "select * from [PBPOS] where [PBPOSPC] =@PBPOSPC ";
            DbParameter[] pramsGet = {
									   MakeInParam("@PBPOSUS", (DbType)SqlDbType.Char , 1, PBPOSUS),
									   MakeInParam("@PBPOSPC", (DbType)SqlDbType.VarChar, 50, PBPOSPC)
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetPBPOSFromIDataReader(sr);
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
        private PBPOSEntity GetPBPOSFromIDataReader(DbDataReader dr)
        {
            PBPOSEntity dt = new PBPOSEntity();
            if (dr.FieldCount > 0)
            {
                dt.PBPOSPC = dr["PBPOSPC"].ToString();
                dt.PBPOSPN = dr["PBPOSPN"].ToString();
                dt.PBPOSPEN = dr["PBPOSPEN"].ToString();
                dt.PBPOSPTWN = dr["PBPOSPTWN"].ToString();
                dt.PBPOSBD = dr["PBPOSBD"].ToString();
                dt.PBPOSPD = dr["PBPOSPD"].ToString();
                dt.PBPOSRB = dr["PBPOSRB"].ToString();
                dt.PBPOSRBN = dr["PBPOSRBN"].ToString();
                dt.PBPOSUS = dr["PBPOSUS"].ToString();
                dt.PBPOTTC = dr["PBPOTTC"].ToString();
                dt.PBDUTDC = dr["PBDUTDC"].ToString();
                dt.PBPOCCC = dr["PBPOCCC"].ToString();
                dt.PBPOSWP = dr["PBPOSWP"].ToString();
                dt.PBPOSET = dr["PBPOSET"].ToString();
                if (dr["PBPOSOI"].ToString() != "" || dr["PBPOSOI"] != null) dt.PBPOSOI = Int32.Parse(dr["PBPOSOI"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 获取表信息
        /// 在基础数据页，应该全部选出；
        /// 在其它页，无效数据不出来，且按序排序
        /// </summary>
        /// <param name="PageSource">只能用两个值，在基础数据页为"Basic" ;否则为"Other" 默认为 Other; </param>
        /// <param name="SearchSql">查询字符串,在基础数据页以外的页，该项基本不会用 </param>
        /// <returns></returns>
        public DataTable GetPBPOS(string PageSource, string SearchSql)
        {
            if (PageSource == "")
                PageSource = "Other";
            string sql = "select * from [PBPOS]";
            if (PageSource == "Basic")
            {
                if (SearchSql != "")
                    sql += SearchSql;
            }
            else
            {
                sql += " Where [PBPOSUS]='1' Order By PBPOSOI";

            }

            return ExecuteDataset(CommandType.Text, sql).Tables[0];

        }
        /// <summary>
        /// 修改岗位信息
        /// </summary>
        /// <param name="_PBPOSEntity"></param>
        /// <returns>返回string "-1"表示所属部门下的岗位名称已经存在</returns>
        public string UpDatePBPOS(PBPOSEntity _PBPOSEntity)
        {
            if (!_PBPOSEntity.PBPOSUS.Equals("1"))
            {
                string sqlCount = "select count(*) from PEEBI where PEEBIES != '02' and PEEBIPC = '" + _PBPOSEntity.PBPOSPC + "'";
                if (Convert.ToInt32(ExecuteScalar(CommandType.Text, sqlCount)) > 0)
                {
                    return "-2";
                }
            }
            DbParameter[] prams = {
                                      MakeInParam("@PBPOSPN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPN ),
                                      MakeInParam("@PBPOSPC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPC ),
                                      MakeInParam("@PBPOTTC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOTTC ),
                                      MakeInParam("@PBDUTDC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBDUTDC ),
                                  };
            string sql = "select * from [PBPOS] where PBPOSPN = @PBPOSPN and PBDUTDC=@PBDUTDC and PBPOTTC=@PBPOTTC and PBPOSPC<>@PBPOSPC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {
                DbParameter[] pramsUpDate = {
                                        MakeInParam("@PBPOSPC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPC ),
                                        MakeInParam("@PBPOSPN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPN ),
                                        MakeInParam("@PBPOSPEN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPEN ),
                                        MakeInParam("@PBPOSPTWN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSPTWN ),
                                        MakeInParam("@PBPOSBD",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSBD ),
                                        MakeInParam("@PBPOSPD",(DbType)SqlDbType.VarChar,200,_PBPOSEntity.PBPOSPD ),
                                        MakeInParam("@PBPOSRB",(DbType)SqlDbType.VarChar,100,_PBPOSEntity.PBPOSRB ),
                                        MakeInParam("@PBPOSRBN",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSRBN ),
                                        MakeInParam("@PBPOSUS",(DbType)SqlDbType.Char,1,_PBPOSEntity.PBPOSUS ),
                                        MakeInParam("@PBPOSOI",(DbType)SqlDbType.Int,4,_PBPOSEntity.PBPOSOI ),
                                        MakeInParam("@PBPOTTC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOTTC ),
                                        MakeInParam("@PBDUTDC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBDUTDC ),
                                        MakeInParam("@PBPOCCC",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOCCC ),
                                        MakeInParam("@PBPOSWP",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSWP ),
                                        MakeInParam("@PBPOSET",(DbType)SqlDbType.VarChar,50,_PBPOSEntity.PBPOSET ),
                                            };

                StringBuilder sb = new StringBuilder();
                sb.Append("UpDate [dbo].[PBPOS]");
                sb.Append(" set ");
                sb.Append(" [PBPOSPN]=@PBPOSPN,");
                sb.Append(" [PBPOSPEN]=@PBPOSPEN,");
                sb.Append(" [PBPOSPTWN]=@PBPOSPTWN,");
                sb.Append(" [PBPOSBD]=@PBPOSBD,");
                sb.Append(" [PBPOSPD]=@PBPOSPD,");
                sb.Append(" [PBPOSRB]=@PBPOSRB,");
                sb.Append(" [PBPOSRBN]=@PBPOSRBN,");
                sb.Append(" [PBPOSUS]=@PBPOSUS,");
                sb.Append(" [PBPOSOI]=@PBPOSOI,");
                sb.Append(" [PBPOTTC]=@PBPOTTC,");
                sb.Append(" [PBDUTDC]=@PBDUTDC,");
                sb.Append(" [PBPOCCC]=@PBPOCCC,");
                sb.Append(" [PBPOSWP]=@PBPOSWP,");
                sb.Append(" [PBPOSET]=@PBPOSET");
                sb.Append(" where [PBPOSPC]=@PBPOSPC ");
                sb.Append(" select @PBPOSPC;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
            }
        }

        /// <summary>
        /// 修改岗位信息
        /// </summary>
        /// <param name="_PBPOSEntity"></param>
        /// <returns>返回string "0"表示全部修改成功，否则返回第一个已经存在工种名称</returns>
        public string UpDatePBPOS(PBPOSEntity[] _PBPOSEntity)
        {

            for (int i = 0; i < _PBPOSEntity.Length; i++)
            {
                string upDateResult = UpDatePBPOS(_PBPOSEntity[i]);
                if (upDateResult == "-1")
                {
                    return _PBPOSEntity[i].PBPOSPN;
                }

            }
            return "0";


        }



        /// <summary>
        /// 删除岗位信息，以岗位代码为参数
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public string DeletePBPOS(string PositionCode)
        {

            DbParameter[] prams = {
                                    MakeInParam("@PBPOSPC",(DbType)SqlDbType.VarChar,50,PositionCode),
                                   };
            string sql = "";

            sql = "delete from [PBPOS] where  PBPOSPC = @PBPOSPC ";

            ExecuteNonQuery(CommandType.Text, sql, prams);

            return "0";

        }

        #endregion


        #region "员工状态类别信息 PBEST"


        public DataTable GetPBESTInfo()
        {
            string sql = "";
            sql = "select * from [PBEST]";
            return ExecuteDataset(sql).Tables[0];
        }
        /// <summary>
        /// 新增员工状态信息
        /// </summary>
        /// <param name="_PBESTEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string AddPBEST(PBESTEntity _PBESTEntity)
        {
            //判断该记录是否已经存在
            DbParameter[] prams = { MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTN),
                                     };
            string sql = "select * from PBEST where PBESTTN=@PBESTTN ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该记录已经存在
            }
            else
            {
                _PBESTEntity.PBESTTC = px_Sequence("PBESTCODE", "1");
                DbParameter[] pramsInsert = {
									   MakeInParam("@PBESTTC",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTC ),
									   MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTN ),
									   MakeInParam("@PBESTTEN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTEN ),
									   MakeInParam("@PBESTTTWN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTTWN ),
									   MakeInParam("@PBESTUS",(DbType)SqlDbType.Char,1,_PBESTEntity.PBESTUS ),
									   MakeInParam("@PBESTOI",(DbType)SqlDbType.Int,4,_PBESTEntity.PBESTOI ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PBEST]");
                sb.Append("(");
                sb.Append("[PBESTTC]");
                sb.Append(",[PBESTTN]");
                sb.Append(",[PBESTTEN]");
                sb.Append(",[PBESTTTWN]");
                sb.Append(",[PBESTUS]");
                sb.Append(",[PBESTOI]");
                sb.Append(") ");
                sb.Append(" VALUES (");
                sb.Append("@PBESTTC,");
                sb.Append("@PBESTTN,");
                sb.Append("@PBESTTEN,");
                sb.Append("@PBESTTTWN,");
                sb.Append("@PBESTUS,");
                sb.Append("@PBESTOI)");
                sb.Append(" select @@identity;");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }

        }

        /// <summary>
        /// 修改信息
        /// </summary>
        /// <param name="_PBESTEntity"></param>
        /// <returns>返回string "-1"表示该已经存在，否则成功 </returns>
        public string UpDatePBEST(PBESTEntity _PBESTEntity)
        {
            //判断是否存在该工种名称
            DbParameter[] prams = { MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTN),
                                    MakeInParam("@PBESTTC",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTC )
                                     };
            string sql = "select * from [PBEST] where  PBESTTN = @PBESTTN and PBESTTC<>@PBESTTC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {
                DbParameter[] pramsUpDate = {
									   MakeInParam("@PBESTTC",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTC ),
									   MakeInParam("@PBESTTN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTN ),
									   MakeInParam("@PBESTTEN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTEN ),
									   MakeInParam("@PBESTTTWN",(DbType)SqlDbType.VarChar,50,_PBESTEntity.PBESTTTWN ),
									   MakeInParam("@PBESTUS",(DbType)SqlDbType.Char,1,_PBESTEntity.PBESTUS ),
									   MakeInParam("@PBESTOI",(DbType)SqlDbType.Int,4,_PBESTEntity.PBESTOI ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("UpDate [dbo].[PBEST]");
                sb.Append(" set ");
                sb.Append(" [PBESTTN]=@PBESTTN,");
                sb.Append(" [PBESTTEN]=@PBESTTEN,");
                sb.Append(" [PBESTTTWN]=@PBESTTTWN,");
                sb.Append(" [PBESTUS]=@PBESTUS,");
                sb.Append(" [PBESTOI]=@PBESTOI");
                sb.Append(" where [PBESTTC]=@PBESTTC");
                sb.Append(" select @PBESTTC");
                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
            }
        }

        /// <summary>
        /// 根据员工状态信息,当参数"是否启用PBDEPUS"的值为空的话，则抓员工状态资料
        /// </summary>
        /// <param name="PBDEPUS"></param>
        /// <param name="sqlWhere">where </param>
        /// <returns></returns>
        public DataTable GetPBESTEntityByKeyCol(string PBESTUS, string sqlWhere)
        {
            DbParameter[] prams = {
									   MakeInParam("@PBESTUS", (DbType)SqlDbType.Char , 1, PBESTUS),
								   };
            string sql = "";
            if (sqlWhere == "")
            {
                if (PBESTUS != "")
                    sql = "where [PBESTTC] =@PBESTUS";
            }
            else
            {
                if (PBESTUS != "")
                    sql = sqlWhere + " and [PBESTTC] =@PBESTUS";
            }
            sql = "select * from [PBEST] " + sql + " Order by PBESTOI,PBESTTN";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }

        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="PBESTTC"></param>
        /// <returns></returns>
        public PBESTEntity GetPBESTEntityByKeyCol(string PBESTTC)
        {
            string sql = "select * from PBEST Where PBESTTC=@PBESTTC";
            DbParameter[] pramsGet = {
									   MakeInParam("@PBESTTC",(DbType)SqlDbType.VarChar,50,PBESTTC ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetPBESTFromIDataReader(sr);
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
        private PBESTEntity GetPBESTFromIDataReader(DbDataReader dr)
        {
            PBESTEntity dt = new PBESTEntity();
            if (dr.FieldCount > 0)
            {
                dt.PBESTTC = dr["PBESTTC"].ToString();
                dt.PBESTTN = dr["PBESTTN"].ToString();
                dt.PBESTTEN = dr["PBESTTEN"].ToString();
                dt.PBESTTTWN = dr["PBESTTTWN"].ToString();
                dt.PBESTUS = dr["PBESTUS"].ToString();
                if (dr["PBESTOI"].ToString() != "" || dr["PBESTOI"] != null) dt.PBESTOI = Int32.Parse(dr["PBESTOI"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }


        /// <summary>
        /// 获取员工状态类别信息
        /// 在基础数据页，应该全部选出；
        /// 在其它页，无效数据不出来，且按序排序
        /// </summary>
        /// <param name="PageSource">只能用两个值，在基础数据页为"Basic" ;否则为"Other" 默认为 Other; </param>
        /// <param name="SearchSql">查询字符串,在基础数据页以外的页，该项基本不会用 </param>
        /// <returns></returns>
        public DataTable GetPBEST(string PageSource, string SearchSql)
        {
            if (PageSource == "")
                PageSource = "Other";
            string sql = "select * from [PBEST]";
            if (PageSource == "Basic")
            {
                if (SearchSql != "")
                    sql += SearchSql;
            }
            else
            {
                sql += " Where [PBESTUS]='1' Order By PBESTOI";

            }

            return ExecuteDataset(CommandType.Text, sql).Tables[0];

        }
        #endregion


        #region "职位基础信息 PCPOS"

        public DataTable GetPCPOS()
        {
            string sql = "";
            sql = "select * from  [PCPOS]";
            return ExecuteDataset(sql).Tables[0];

        }



        /// <summary>
        /// 新增职位信息
        /// </summary>
        /// <param name="_PCPOSEntity"></param>
        /// <returns>返回string "-1"表示职位名称已经存在，否则表示新增成功</returns>
        public string AddPCPOS(PCPOSEntity _PCPOSEntity)
        {

            //判断是否存在该职位名称
            DbParameter[] prams = { MakeInParam("@DeptName",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptName),
                                    MakeInParam("@ParentDeptCode",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.ParentDeptCode),
                                     };
            string sql = "select * from [PCPOS] where  PCPOSDN = @DeptName and PCPOSPDC=@ParentDeptCode ";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {

                DbParameter[] pramsInsert = {
									   MakeInParam("@PCPOSDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptCode),
									   MakeInParam("@PCPOSDN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptName),
                                       MakeInParam("@PCPOSDEN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptEName),
                                       MakeInParam("@PCPOSDTWN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptTWName),
									   MakeInParam("@PCPOSPDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.ParentDeptCode),
									   MakeInParam("@PCPOSUS",(DbType)SqlDbType.Char,1,_PCPOSEntity.DeptIsValid),
                                       MakeInParam("@PCPOSOI",(DbType)SqlDbType.Int,4,_PCPOSEntity.DeptOrderItem ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[PCPOS]");
                sb.Append("([PCPOSDC]");
                sb.Append(",[PCPOSDN]");
                sb.Append(",[PCPOSDEN]");
                sb.Append(",[PCPOSDTWN]");
                sb.Append(",[PCPOSPDC]");
                sb.Append(",[PCPOSOI]");
                sb.Append(",[PCPOSUS])");
                sb.Append("VALUES");
                sb.Append("(@PCPOSDC");
                sb.Append(",@PCPOSDN");
                sb.Append(",@PCPOSDEN");
                sb.Append(",@PCPOSDTWN,");
                sb.Append("@PCPOSPDC, ");
                sb.Append("@PCPOSOI, ");
                sb.Append("@PCPOSUS)");
                sb.Append(" select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();

            }


        }
        /// <summary>
        /// 根据职位名称获得职位信息,当参数"是否启用PCPOSUS"的值为空的话，则抓全部职位资料
        /// </summary>
        /// <param name="PCPOSUS"></param>
        /// <param name="PCPOSDN"></param>
        /// <returns></returns>
        public DataTable GetPCPOSbyDeptName(string PCPOSUS, string PCPOSDN)
        {

            DbParameter[] prams = {
									   MakeInParam("@PCPOSUS", (DbType)SqlDbType.VarChar, 1, PCPOSUS),
									   MakeInParam("@PCPOSDN", (DbType)SqlDbType.VarChar, 50, PCPOSDN)
								   };
            string sql = "";
            if (PCPOSUS == "")
                sql = "select * from [PCPOS] Order by PCPOSOI ";
            else
                sql = "select * from [PCPOS] where [PCPOSUS] =@PCPOSUS and PCPOSDN like @PCPOSDN +'%' Order by PCPOSOI";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];



        }
        /// <summary>
        /// 根据职位代码获得职位信息,当参数"是否启用PCPOSUS"的值为空的话，则抓全部职位资料
        /// </summary>
        /// <param name="PCPOSUS"></param>
        /// <param name="PCPOSDC"></param>
        /// <returns></returns>
        public DataTable GetPCPOSbyDeptCode(string PCPOSUS, string PCPOSDC)
        {
            DbParameter[] prams = {
									   MakeInParam("@PCPOSUS", (DbType)SqlDbType.VarChar, 1, PCPOSUS),
									   MakeInParam("@PCPOSDC", (DbType)SqlDbType.VarChar, 50, PCPOSDC)
								   };
            string sql = "";
            if (PCPOSUS == "")
                sql = "select * from [PCPOS] Order by PCPOSOI ";
            else
                sql = "select * from [PCPOS] where [PCPOSUS] =@PCPOSUS and PCPOSDC like @PCPOSDC +'%' Order by PCPOSOI";

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }

        /// <summary>
        /// 根据职位代码获得上级职位信息
        /// </summary>
        /// <param name="PCPOSDC"></param>
        /// <returns></returns>
        public DataTable GetParentPCPOSbyDeptCode(string PCPOSDC)
        {
            DbParameter[] prams = {
									   
									   MakeInParam("@PCPOSDC", (DbType)SqlDbType.VarChar, 50, PCPOSDC),
								   };
            string sql = "";
            //sql = "select * from [PCPOS] where [PCPOSUS] =@PCPOSUS and PCPOSDC like @PCPOSDC +'%' Order by PCPOSOI";
            sql = "select b.* from [PCPOS] a,[PCPOS] b where  a.PCPOSDC=@PCPOSDC and b.PCPOSDC=a.PCPOSPDC Order by b.PCPOSDN";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }
        /// <summary>
        /// 根据职位代码获得下级职位信息
        /// </summary>
        /// <param name="PCPOSDC"></param>
        /// <returns></returns>
        public DataTable GetChildPCPOSbyDeptCode(string PCPOSDC)
        {
            DbParameter[] prams = { MakeInParam("@PCPOSDC", (DbType)SqlDbType.VarChar, 50, PCPOSDC),
								   };
            string sql = "";
            sql = "select * from [PCPOS] where [PCPOSPDC] =@PCPOSDC Order by PCPOSOI";
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }

        /// <summary>
        /// 根据职位代码获得下级职位信息
        /// </summary>
        /// <param name="PCPOSDC"></param>
        /// <returns></returns>
        public DataTable GetChildPCPOSbyDeptCode(string PCPOSDC, string NotUseToView)
        {
            DbParameter[] prams = {
                MakeInParam("@PCPOSDC", (DbType)SqlDbType.VarChar, 50, PCPOSDC),
            };
            string sql = "";
            if (NotUseToView.Equals("1"))
            {
                sql = "select * from [PCPOS] where [PCPOSPDC] =@PCPOSDC Order by PCPOSOI";
            }
            else
            {
                sql = "select * from [PCPOS] where [PCPOSPDC] =@PCPOSDC and PCPOSUS='1' Order by PCPOSOI";
            }
            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        /// <summary>
        /// 根据DataRow获取职位实体
        /// </summary>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        public PCPOSEntity GetPCPOSEntity(DataRow dataRow)
        {

            PCPOSEntity _PCPOSEntity = new PCPOSEntity();
            _PCPOSEntity.DeptCode = dataRow["PCPOSDC"].ToString();
            _PCPOSEntity.DeptName = dataRow["PCPOSDN"].ToString();
            _PCPOSEntity.DeptEName = dataRow["PCPOSDEN"].ToString();
            _PCPOSEntity.DeptTWName = dataRow["PCPOSDTWN"].ToString();
            _PCPOSEntity.ParentDeptCode = dataRow["PCPOSPDC"].ToString();
            _PCPOSEntity.DeptOrderItem = Convert.ToInt16(dataRow["PCPOSOI"].ToString());
            _PCPOSEntity.DeptIsValid = dataRow["PCPOSUS"].ToString();

            return _PCPOSEntity;

        }


        /// <summary>
        /// 返回实体内容
        /// </summary>
        /// <param name="KeyCol"></param>
        /// <returns></returns>
        public PCPOSEntity GetPCPOSEntityByKeyCol(string PCPOSDC)
        {
            string sql = "select * from PCPOS Where PCPOSDC=@PCPOSDC";
            DbParameter[] pramsGet = {
									   MakeInParam("@PCPOSDC",(DbType)SqlDbType.VarChar,50,PCPOSDC ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetPCPOSFromIDataReader(sr);
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
        private PCPOSEntity GetPCPOSFromIDataReader(DbDataReader dr)
        {
            PCPOSEntity dt = new PCPOSEntity();
            if (dr.FieldCount > 0)
            {
                dt.DeptCode = dr["PCPOSDC"].ToString();
                dt.DeptName = dr["PCPOSDN"].ToString();
                dt.DeptEName = dr["PCPOSDEN"].ToString();
                dt.DeptTWName = dr["PCPOSDTWN"].ToString();
                dt.ParentDeptCode = dr["PCPOSPDC"].ToString();
                dt.DeptIsValid = dr["PCPOSUS"].ToString();


                if (dr["PCPOSOI"].ToString() != "" || dr["PCPOSOI"] != null) dt.DeptOrderItem = Int32.Parse(dr["PCPOSOI"].ToString());
                else
                    dt.DeptOrderItem = 0;
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        /// <summary>
        /// 根据DataTable获取职位实体的数组
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public PCPOSEntity[] GetPCPOSEntityArray(DataTable dataTable)
        {
            int arrayItems = dataTable.Rows.Count;

            PCPOSEntity[] deptArray = new PCPOSEntity[arrayItems];
            for (int i = 0; i < arrayItems; i++)
            {
                deptArray[i] = GetPCPOSEntity(dataTable.Rows[i]);

            }
            return deptArray;


        }
        /// <summary>
        /// 修改职位信息
        /// </summary>
        /// <param name="_PCPOSEntity"></param>
        /// <returns>返回string "-1"表示职位名称已经存在，否则表示修改成功</returns>

        public string UpDatePCPOS(PCPOSEntity _PCPOSEntity)
        {

            if (!_PCPOSEntity.DeptIsValid.Equals("1"))
            {
                string sqlCount = "select count(*) from PEEBI where PEEBIES != '02' and PEEBIDEP like '" + _PCPOSEntity.DeptCode + "%'";
                if (Convert.ToInt32(ExecuteScalar(CommandType.Text, sqlCount)) > 0)
                {
                    return "-2";
                }
            }
            //判断是否存在该职位名称
            DbParameter[] prams = {
                MakeInParam("@DeptName",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptName),
                MakeInParam("@DeptCode",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptCode),
                MakeInParam("@PCPOSPDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.ParentDeptCode),
            };
            string sql = "select * from [PCPOS] where [PCPOSDN] = @DeptName and [PCPOSDC]!=@DeptCode and [PCPOSPDC]=@PCPOSPDC";
            if (ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
            {
                return "-1";//该名称已经存在
            }
            else
            {
                DbParameter[] pramsUpDate = {
									   MakeInParam("@PCPOSDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptCode),
									   MakeInParam("@PCPOSDN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptName),
                                       MakeInParam("@PCPOSDEN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptEName),
                                       MakeInParam("@PCPOSDTWN",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptTWName),
									   MakeInParam("@PCPOSPDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.ParentDeptCode),
									   MakeInParam("@PCPOSOI",(DbType)SqlDbType.Int,4,_PCPOSEntity.DeptOrderItem),
                                       MakeInParam("@PCPOSUS",(DbType)SqlDbType.Char,1,_PCPOSEntity.DeptIsValid)
                                        };
                StringBuilder sb = new StringBuilder();
                sb.Append("UpDate [dbo].[PCPOS] set ");
                sb.Append("[PCPOSDN]=@PCPOSDN");
                sb.Append(",[PCPOSDEN]=@PCPOSDEN");
                sb.Append(",[PCPOSDTWN]=@PCPOSDTWN");
                sb.Append(",[PCPOSPDC]=@PCPOSPDC");
                sb.Append(",[PCPOSOI]=@PCPOSOI");
                sb.Append(",[PCPOSUS]=@PCPOSUS");
                sb.Append(" where [PCPOSDC]=@PCPOSDC");
                sb.Append(" select @PCPOSDC;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();

            }

        }

        /// <summary>
        /// 修改多个职位信息
        /// </summary>
        /// <param name="_PCPOSEntity"></param>
        /// <returns>返回string "0"表示全部修改成功，否则返回已经存在职位名称</returns>
        public string UpDatePCPOS(PCPOSEntity[] _PCPOSEntity)
        {

            for (int i = 0; i < _PCPOSEntity.Length; i++)
            {
                string upDateResult = UpDatePCPOS(_PCPOSEntity[i]);
                if (upDateResult == "-1")
                {
                    return _PCPOSEntity[i].DeptName;
                }

            }
            return "0";


        }
        /// <summary>
        /// 删除职位信息，以职位代码为参数
        /// </summary>
        /// <param name="DeptCode"></param>
        /// <returns></returns>
        public string DeletePCPOS(string DeptCode)
        {

            DbParameter[] prams = {
                MakeInParam("@PCPOSDC",(DbType)SqlDbType.VarChar,50,DeptCode),
            };

            int employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES != '02' and PEEBIDEP=@PCPOSDC", prams));
            if (employeeCount > 0)
            {
                return "-3";
            }

            employeeCount = Convert.ToInt32(ExecuteScalar(CommandType.Text, "select count(*) from PEEBI where PEEBIES = '02' and PEEBIDEP=@PCPOSDC", prams));
            if (employeeCount > 0)
            {
                return "-2";
            }
            string sql = "if not exists (select * from PEEBI where PEEBIDEP=@PCPOSDC) delete from [PCPOS] where  PCPOSDC = @PCPOSDC ";
            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }
        /// <summary>
        /// 删除职位信息，以职位实体为参数
        /// </summary>
        /// <param name="_PCPOSEntity"></param>
        /// <returns></returns>
        public string DeletePCPOS(PCPOSEntity _PCPOSEntity)
        {

            DbParameter[] prams = {
                                    MakeInParam("@PCPOSDC",(DbType)SqlDbType.VarChar,50,_PCPOSEntity.DeptCode),
                                   };
            string sql = "";

            sql = "delete from [PCPOS] where  PCPOSDC = @PCPOSDC ";

            return ExecuteNonQuery(CommandType.Text, sql, prams).ToString();
        }


        /// <summary>
        /// 删除职位信息，以职位实体数组为参数
        /// </summary>
        /// <param name="_PCPOSEntity"></param>
        /// <returns></returns>
        public string DeletePCPOS(PCPOSEntity[] _PCPOSEntity)
        {
            for (int i = 0; i < _PCPOSEntity.Length; i++)
            {
                DeletePCPOS(_PCPOSEntity[i]);
            }
            return "0";
        }

        /// <summary>
        /// 获得下级职位的最大职位代码，返回一个新下级代码，此处以后要将其写的严谨些，比如要考虑每级职位的代码长度等
        /// </summary>
        /// <param name="PCPOSPDC">上级职位代码</param>
        /// <returns>-1表示抓取失败</returns>
        public string GetPCPOSDCbyPCPOSPDC(string PCPOSPDC)
        {
            string result = "-1";
            DbParameter[] prams = {
                                    MakeInParam("@PCPOSPDC",(DbType)SqlDbType.VarChar,50,PCPOSPDC),
                                   };
            string sql = "";

            sql = "select PCPOSDC from PCPOS where PCPOSPDC=@PCPOSPDC";

            DbDataReader dr = null;
            try
            {
                dr = ExecuteReader(CommandType.Text, sql, prams);
                if (!dr.HasRows)
                {
                    if (PCPOSPDC == "")
                    {
                        result = PCPOSPDC + "10";

                    }
                    else
                    {
                        result = PCPOSPDC + "01";
                    }
                    dr.Close();
                }
                else
                {
                    sql = "select max(PCPOSDC) as PCPOSDC from PCPOS where PCPOSPDC=@PCPOSPDC";
                    dr = ExecuteReader(CommandType.Text, sql, prams);
                    if (dr.Read())
                    {
                        string currentDeptCode = dr["PCPOSDC"].ToString();
                        result = Convert.ToString((Convert.ToInt32(currentDeptCode) + 1));

                        //return GetPCPOSFromIDataReader(sr);
                    }
                    //throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
                }
            }
            catch
            {
                result = "-1";
                //throw exp;
            }
            finally
            {
                if (dr != null)
                    dr.Close();
            }
            return result;
            //return "0";

        }


        #endregion


        #region  "模块GridView数据获取"
        /// <summary>
        /// 根据模块号,分页参数,查询条件来获得DataTable
        /// </summary>
        /// <param name="ModuleCode"></param>
        /// <param name="WhereCondition"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <returns>获得DataTable</returns>
        public DataTable GetDBRecord(string ModuleCode, string columnList, string tableList, string WhereCondition, string orderby, int PageSize, int PageIndex)
        {
            DbParameter[] prams = {
                                      MakeInParam("@columnList", (DbType)SqlDbType.VarChar  , 2000, columnList),
									  MakeInParam("@tableList", (DbType)SqlDbType.VarChar , 2000, tableList),
									  MakeInParam("@WhereCondition", (DbType)SqlDbType.VarChar, 2000, WhereCondition),
                                      MakeInParam("@orderby", (DbType)SqlDbType.VarChar, 50, orderby),
                                      MakeInParam("@pagesize", (DbType)SqlDbType.Int , 4, PageSize),
                                      MakeInParam("@pageindex", (DbType)SqlDbType.Int, 4, PageIndex),
                                  };

            string sql = "GetRecordSetbyPage";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];

        }
        /// <summary>
        /// 抓出符合条件的资料.没条件的话，传 1=1为条件
        /// </summary>
        /// <param name="columnList"></param>
        /// <param name="tableList"></param>
        /// <param name="WhereCondition"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public DataTable GetDBRecord(string columnList, string tableList, string WhereCondition, string orderby)
        {

            DbParameter[] prams = {    MakeInParam("@columnList", (DbType)SqlDbType.VarChar  , 2000, columnList),
                                       MakeInParam("@tableList", (DbType)SqlDbType.VarChar , 2000, tableList),
                                       MakeInParam("@WhereCondition", (DbType)SqlDbType.VarChar, 2000, WhereCondition),
                                       MakeInParam("@orderby", (DbType)SqlDbType.VarChar, 50, orderby),
                                      

                                       
                                   };
            string sql = "";
            if (orderby != "")
                sql = "select " + columnList + " from " + tableList + " where " + WhereCondition + " order by " + orderby;
            else
                sql = "select " + columnList + " from " + tableList + " where " + WhereCondition;

            return ExecuteDataset(CommandType.Text, sql, prams).Tables[0];

        }

        #endregion

        #region "内部邮件 UserEMail"
        public DataTable GetEmailInfoByID(int UserSerialID)
        {
            string sql = "";

            sql = string.Format("select * from UserEmail where UserSerialID={0}", UserSerialID);

            return ExecuteDataset(sql).Tables[0];


        }



        //add Rio
        public string AddEmailInfor(EmailEntity _EmailEntity)
        {


                DbParameter[] pramsInsert = {
									   MakeInParam("@UserID",(DbType)SqlDbType.VarChar,50,_EmailEntity.UserID),
									   MakeInParam("@SenderID",(DbType)SqlDbType.VarChar,2000,_EmailEntity.SenderID),
                                       MakeInParam("@ReceiverID",(DbType)SqlDbType.VarChar,2000,_EmailEntity.ReceiverID),
                                       MakeInParam("@MailTitle",(DbType)SqlDbType.VarChar,50,_EmailEntity.MailTitle),
									   MakeInParam("@MailContent",(DbType)SqlDbType.VarChar,20000,_EmailEntity.MailContent),
									   MakeInParam("@AttachmentID",(DbType)SqlDbType.VarChar,50,_EmailEntity.AttachID),
                                       MakeInParam("@ReceiveTime",(DbType)SqlDbType.DateTime,20,_EmailEntity.ReceiveTime),
                                       MakeInParam("@SendTime",(DbType)SqlDbType.DateTime,20,_EmailEntity.SendTime),
                                       MakeInParam("@ISRead",(DbType)SqlDbType.Int,4,_EmailEntity.ISRead ),
                                       MakeInParam("@UserMasterID",(DbType)SqlDbType.Int,4,_EmailEntity.UserMasterID ),
                                       MakeInParam("@SecretSenderID",(DbType)SqlDbType.VarChar,2000,_EmailEntity.SecretSenderID ),
                                       MakeInParam("@ISSecret",(DbType)SqlDbType.Int,4,_EmailEntity.IsScret ),

                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[UserEmail]");
                sb.Append("([UserID]");
                sb.Append(",[SenderID]");
                sb.Append(",[ReceiverID]");
                sb.Append(",[MailTitle]");
                sb.Append(",[MailContent]");
                sb.Append(",[AttachmentID]");
                sb.Append(",[ReceiveTime]");
                sb.Append(",[ISRead]");
                sb.Append(",[SendTime]");
                sb.Append(",[UserMasterID]");
                sb.Append(",[SecretSenderID]");
                sb.Append(",[ISSecret]");
                sb.Append(")");
                sb.Append("VALUES");
                sb.Append("(@UserID");
                sb.Append(",@SenderID");
                sb.Append(",@ReceiverID");
                sb.Append(",@MailTitle");
                sb.Append(",@MailContent");
                sb.Append(",@AttachmentID");
                sb.Append(",@ReceiveTime");
                sb.Append(",@ISRead");
                sb.Append(",@SendTime");
                sb.Append(",@UserMasterID");
                sb.Append(",@SecretSenderID");
                sb.Append(",@ISSecret");
                sb.Append(")");
                sb.Append(" select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
           
            
        }

        //获取删除邮件
        public DataTable GetDeleteEmailInfoById(int id)
        {
            string sql = "";
            sql = string.Format("select MailContent=e.MailContent,case e.ReceiverID when '' then e.UserID  else e.ReceiverID end as RecvAdd ,RecvName=l.UserName,MailTitle=e.MailTitle,SendAdd=e.SenderID,Time=e.ReceiveTime,e.UserSerialID,e.UserMasterID from UserEmail e left join UserList l on ( case e.ReceiverID when '' then e.UserID else e.ReceiverID end )= l.UserID  where e.UserSerialID ={0} ", id);
            return ExecuteDataset(sql).Tables[0];
        }

        // add Rio
        //获取收件箱邮件
        public DataTable GetRecvEmailInfoById(int id)
        {
            string sql = "";
            sql = string.Format("select MailContent=e.MailContent,RecvAdd=e.ReceiverID ,RecvName=l.UserName,MailTitle=e.MailTitle,Time=e.ReceiveTime,e.UserSerialID,e.UserMasterID from UserEmail e left join UserList l on e.ReceiverID = l.UserID  where e.UserSerialID ={0} and ReceiverID!=''", id);
            return ExecuteDataset(sql).Tables[0];
        }

        //获取已发送邮件
        public DataTable GetSendEmailInfoById(int id)
        {
            string sql = "";
            sql = string.Format("select MailContent=e.MailContent,SendAdd=e.SenderID ,SendName=l.UserName,MailTitle=e.MailTitle,Time=e.SendTime,e.UserSerialID,IsScret=e.ISSecret from UserEmail e left join UserList l on e.SenderID = l.UserID  where e.UserSerialID ={0} ", id);
            return ExecuteDataset(sql).Tables[0];
        }

        //通过UserMasterID获取有哪些人收到邮件
        public DataTable GetRecvEmailInfoByUserMasterID(int id)
        {
            string sql = "";
            sql = string.Format("select UserID=e.UserID ,UserName=l.UserName,e.UserSerialID,e.UserMasterID,IsScret=e.ISSecret from UserEmail e left join UserList l on e.UserID = l.UserID  where e.UserMasterID ={0} ", id);
            return ExecuteDataset(sql).Tables[0];

        }


        //通过UserSerialID更新ISRead  邮件是否被读取
        public int UpDateISReadByID(int ID,int iIsRead)
        {
            string sql = "";

            sql = string.Format("upDate UserEmail set ISRead={0} where UserSerialID ={1}",iIsRead,ID);

            return ExecuteNonQuery(sql);
        }

        //通过UserSerialID删除记录
        public int DeleteEmailByUserSerialID(int ID)
        {
            string sql = "";

            sql = string.Format("delete from UserEmail where UserSerialID = {0}", ID);

            return ExecuteNonQuery(sql);
        }

        #endregion

        #region "邮件附件 UserEMailAttachment"

        public DataTable GetEmailAttachFile(int EmailID)
        {
            DbParameter[] prams = {
                                      MakeInParam("@EmailSerialID", (DbType)SqlDbType.Int, 4,EmailID),
                                     
                                  };
            string sql = "sp_GetEmailAttachFileByEmailID";
            return ExecuteDataset(CommandType.StoredProcedure, sql, prams).Tables[0];
        }



        // add Rio
        public DataTable GetEmailAttachInfoById(int id)
        {
            string sql = "";
            sql = string.Format("select * from [UserEmailAttachment] where EmailSerialID ={0}", id);
            return ExecuteDataset(sql).Tables[0];
        }


        //add Rio
        public string AddEmailAttachmentInfor(EmailAttachEntity _EmailAttachEntity)
        {

         
                DbParameter[] pramsInsert = {
									   
									   MakeInParam("@AttachmentName",(DbType)SqlDbType.VarChar,50,_EmailAttachEntity.AttachmentName),
                                       MakeInParam("@AttachmentUrl",(DbType)SqlDbType.VarChar,2000,_EmailAttachEntity.AttachmentUrl),
                                        MakeInParam("@EmailSerialID",(DbType)SqlDbType.Int,4,_EmailAttachEntity.EmailSerialID),

                                        MakeInParam("@AttachClientName",(DbType)SqlDbType.VarChar,2000,_EmailAttachEntity.AttachClientName),
                                       MakeInParam("@AttachSize",(DbType)SqlDbType.Float,32,_EmailAttachEntity.AttachSize),
                                        MakeInParam("@AttachType",(DbType)SqlDbType.VarChar,50,_EmailAttachEntity.AttachType),
									 
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[UserEmailAttachment]");
                sb.Append("(");
                sb.Append(" [AttachmentName]");
                sb.Append(",[AttachmentUrl]");
                sb.Append(",[EmailSerialID]");
                sb.Append(",[AttachClientName]");
                sb.Append(",[AttachSize]");
                sb.Append(",[AttachType]");
                sb.Append(")"); 
                sb.Append("VALUES");
                sb.Append("(");
                sb.Append(" @AttachmentName");
                sb.Append(",@AttachmentUrl");
                sb.Append(",@EmailSerialID");
                sb.Append(",@AttachClientName");
                sb.Append(",@AttachSize");
                sb.Append(",@AttachType");
                sb.Append(")");
                sb.Append(" select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
         
        }


        public string GetImagePath(string FileType)
        {
            string sql = "";

            sql = string.Format("select ImagePath from [Workflow_FileImageTag] where FileType='{0}'", FileType);

            return ExecuteDataset(sql).Tables[0].Rows[0]["ImagePath"].ToString();
        }
        #endregion

        #region "信息维护 NewsList"

        //add Rio
        public string AddNewsListInfor(NewsListEntity _NewsListEntity)
        {

                DbParameter[] pramsInsert = {
									   MakeInParam("@NewsTitle",(DbType)SqlDbType.VarChar,200,_NewsListEntity.NewsTitle),
									   MakeInParam("@NewsBody",(DbType)SqlDbType.VarChar,2000,_NewsListEntity.NewsBody),
                                       MakeInParam("@NewsTypeID",(DbType)SqlDbType.Int,4,_NewsListEntity.NewsTypeID),
                                       MakeInParam("@IsPublish",(DbType)SqlDbType.Char,1,_NewsListEntity.IsPublish),
									   MakeInParam("@ExpiredDate",(DbType)SqlDbType.DateTime,20,_NewsListEntity.ExpireDate),
									   MakeInParam("@CreateDate",(DbType)SqlDbType.DateTime,20,_NewsListEntity.CreateDate),
                                       MakeInParam("@Creator",(DbType)SqlDbType.VarChar,50,_NewsListEntity.Creator),
                                       MakeInParam("@PublishDate",(DbType)SqlDbType.DateTime,20,_NewsListEntity.PublishDate),
                                       MakeInParam("@LastModifier",(DbType)SqlDbType.VarChar,50,_NewsListEntity.LastModifier ),
                                       MakeInParam("@LastModifyDate",(DbType)SqlDbType.DateTime,20,_NewsListEntity.LastModifyDate ),
                                             };
                StringBuilder sb = new StringBuilder();
                sb.Append("INSERT INTO [dbo].[News_NewsList]");
                sb.Append("([NewsTitle]");
                sb.Append(",[NewsBody]");
                sb.Append(",[NewsTypeID]");
                sb.Append(",[IsPublish]");
                sb.Append(",[ExpiredDate]");
                sb.Append(",[CreateDate]");
                sb.Append(",[Creator]");
                sb.Append(",[PublishDate]");
                sb.Append(",[LastModifier]");
                sb.Append(",[LastModifyDate]");
                sb.Append(")");
                sb.Append("VALUES");
                sb.Append("(@NewsTitle");
                sb.Append(",@NewsBody");
                sb.Append(",@NewsTypeID");
                sb.Append(",@IsPublish");
                sb.Append(",@ExpiredDate ");
                sb.Append(",@CreateDate");
                sb.Append(",@Creator");
                sb.Append(",@PublishDate");
                sb.Append(",@LastModifier");
                sb.Append(",@LastModifyDate");
                sb.Append(")");
                sb.Append(" select @@identity;");

                return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
            }


        public string DeleteNewsById(int iNewsId)
        {
            string sql;

            sql = string.Format("delete from News_NewsList where NewsID={0}", iNewsId);

            return ExecuteNonQuery(sql).ToString();
 
        }

        public DataTable GetNewsType()
        {
            string sql = "";

            sql = "select * from [News_NewsType]";

            return  ExecuteDataset(sql).Tables[0];
        }

        public DataTable GetNewsByID(int id)
        {
            string sql = "";

            sql = string.Format("select * from [News_NewsList] where NewsID={0}", id);

            return ExecuteDataset(sql).Tables[0];
        }
        #endregion

        #region "信息使用者 NewsUser"

        public string AddNewsUser(NewsUser _NewsUser)
        {

            DbParameter[] pramsInsert = {
									   MakeInParam("@NewsID",(DbType)SqlDbType.Int,4,_NewsUser.NewsID),
									   MakeInParam("@NewsTypeDetailName",(DbType)SqlDbType.VarChar,50,_NewsUser.NewsTypeDetailName),
                                       MakeInParam("@NewsTypeUserName",(DbType)SqlDbType.VarChar,50,_NewsUser.NewsTypeuserName),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[News_NewsUser]");
            sb.Append("([NewsID]");
            sb.Append(",[NewsTypeDetailName]");
            sb.Append(",[NewsTypeUserName]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@NewsID");
            sb.Append(",@NewsTypeDetailName");
            sb.Append(",@NewsTypeUserName");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        #endregion


        #region "文件夹"

        public int AddNewsFolder(DocFolderRight _DocFolder)
        {

            DbParameter[] pramsInsert = {
									
									   MakeInParam("@FolderName",(DbType)SqlDbType.VarChar,50,_DocFolder.FolderName),
                                         MakeInParam("@FolderFullName",(DbType)SqlDbType.VarChar,50,_DocFolder.FullFolderName),
                                           MakeInParam("@FatherID",(DbType)SqlDbType.Int,4,_DocFolder.FatherID),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_FolderRight]");
            sb.Append("([FolderName]");
            sb.Append(",[FolderFullName]");
            sb.Append(",[FatherID]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@FolderName");
            sb.Append(",@FolderFullName");
            sb.Append(",@FatherID");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return Int16.Parse(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());
        }


        public DataTable GetFolderFromName(string FolderFullName)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_FolderRight] where FolderFullName='{0}'", FolderFullName);

            return ExecuteDataset(sql).Tables[0];
      
        }

        public DataTable GetFolderInfoByID(int FolderID)
        {
            string sql = "";

            sql = string.Format(" select * from Doc_FolderRight where FolderSerialID={0} ", FolderID);

            return ExecuteDataset(sql).Tables[0];

        }


        #endregion

        #region "文件夹用户权限"

        public string AddNewsFolderPermission(DocUserRight _DocUserRight)
        {

            DbParameter[] pramsInsert = {
									
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,_DocUserRight.UserID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocUserRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocUserRight.Permission),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_UserRight]");
            sb.Append("([UserSerialID]");
            sb.Append(",[FolderID]");
            sb.Append(",[Permission]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@UserSerialID");
            sb.Append(",@FolderID");
            sb.Append(",@Permission");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }


        public string UpDateFolderPermission(DocUserRight _DocUserRight)
        {

            DbParameter[] pramsUpDate = {
									   MakeInParam("@UserSerialID",(DbType)SqlDbType.Int,4,_DocUserRight.UserID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocUserRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocUserRight.Permission),
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Doc_UserRight]");
            sb.Append(" set ");
            sb.Append(" [UserSerialID]=@UserSerialID,");
            sb.Append(" [FolderID]=@FolderID,");
            sb.Append(" [Permission]=@Permission");
            sb.Append(" where [UserSerialID]=@UserSerialID  and [FolderID]=@FolderID");
            sb.Append(" select @UserSerialID ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }



        public DataTable GetFolderPermissFromID(int UserSerialID, int FolderId)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_UserRight] where UserSerialID={0} and FolderID={1}", UserSerialID, FolderId);

            return ExecuteDataset(sql).Tables[0];

        }


        public DataTable GetFolderByPermiss(int UserSerialID)
        { 

                string sql = "";

                sql = string.Format(" select * from Doc_UserRight where UserSerialID={0}  ", UserSerialID);

            return ExecuteDataset(sql).Tables[0];
        }


       
        #endregion


        #region "文件夹角色权限"

        public string AddNewFolderPermissionBySysRole(DocSysRoleRight _DocSysRoleRight)
        {

            DbParameter[] pramsInsert = {
									
									   MakeInParam("@SysRoleID",(DbType)SqlDbType.Int,4,_DocSysRoleRight.SysRoldID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocSysRoleRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocSysRoleRight.Permission),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_SysRoleRight]");
            sb.Append("([SysRoleID]");
            sb.Append(",[FolderID]");
            sb.Append(",[Permission]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@SysRoleID");
            sb.Append(",@FolderID");
            sb.Append(",@Permission");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        public string UpDateFolderPermissionBySysRole(DocSysRoleRight _DocSysRoleRight)
        {

            DbParameter[] pramsUpDate = {
									    MakeInParam("@SysRoleID",(DbType)SqlDbType.Int,4,_DocSysRoleRight.SysRoldID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocSysRoleRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocSysRoleRight.Permission),
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Doc_SysRoleRight]");
            sb.Append(" set ");
            sb.Append(" [SysRoleID]=@SysRoleID,");
            sb.Append(" [FolderID]=@FolderID,");
            sb.Append(" [Permission]=@Permission");
            sb.Append(" where [SysRoleID]=@SysRoleID  and [FolderID]=@FolderID");
            sb.Append(" select @SysRoleID ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }


        public DataTable GetFolderPermissFromSysRole(int SysRoleID, int FolderId)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_SysRoleRight] where SysRoleID={0} and FolderID={1}", SysRoleID, FolderId);

            return ExecuteDataset(sql).Tables[0];

        }

      
        #endregion

        #region "文件夹部门权限"

        public string AddNewFolderPermissionByDepart(DocDepartRight _DocDepartRight)
        {

            DbParameter[] pramsInsert = {
									
									   MakeInParam("@DepartmentID",(DbType)SqlDbType.Int,4,_DocDepartRight.DepartMentID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocDepartRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocDepartRight.Permission),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_DepartRight]");
            sb.Append("([DepartmentID]");
            sb.Append(",[FolderID]");
            sb.Append(",[Permission]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@DepartmentID");
            sb.Append(",@FolderID");
            sb.Append(",@Permission");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }


        public string UpDateFolderPermissionByDepart(DocDepartRight _DocDepartRight)
        {

            DbParameter[] pramsUpDate = {
									    MakeInParam("@DepartmentID",(DbType)SqlDbType.Int,4,_DocDepartRight.DepartMentID),
                                         MakeInParam("@FolderID",(DbType)SqlDbType.Int,4,_DocDepartRight.FolderID),
                                           MakeInParam("@Permission",(DbType)SqlDbType.VarChar,50,_DocDepartRight.Permission),
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Doc_DepartRight]");
            sb.Append(" set ");
            sb.Append(" [DepartmentID]=@DepartmentID,");
            sb.Append(" [FolderID]=@FolderID,");
            sb.Append(" [Permission]=@Permission");
            sb.Append(" where [DepartmentID]=@DepartmentID  and [FolderID]=@FolderID");
            sb.Append(" select @DepartmentID ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }


        public DataTable GetFolderPermissFromDepart(int DepartMentID, int FolderId)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_DepartRight] where DepartmentID={0} and FolderID={1}", DepartMentID, FolderId);

            return ExecuteDataset(sql).Tables[0];

        }

      
        #endregion

        #region "文件信息"


        public DataTable GetFileInfoByName(string fileName, string foldName)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_FileInfo] where FileName='{0}' and FileFoldName='{1}'", fileName, foldName);

            return ExecuteDataset(sql).Tables[0];

        }

        public string AddNewsFileInfo(DocFileInfo _DocFileInfo)
        {

            DbParameter[] pramsInsert = {
									
									     MakeInParam("@FileName",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileName),
                                         MakeInParam("@FileFoldName",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileFolderName),
                                         MakeInParam("@FileES",(DbType)SqlDbType.Int,4,_DocFileInfo.FileES),
                                         MakeInParam("@FileEdition",(DbType)SqlDbType.Int,4,_DocFileInfo.FileEdition),
                                         MakeInParam("@FileModifyDate",(DbType)SqlDbType.DateTime,10,_DocFileInfo.FileModifyDate),
                                         MakeInParam("@FileModifyUserID",(DbType)SqlDbType.Int,4,_DocFileInfo.FileModifyUserId),
                                         MakeInParam("@FileSize",(DbType)SqlDbType.Int,4,_DocFileInfo.FileSize),
                                         MakeInParam("@FileNote",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileNote),
                                         MakeInParam("@FileValidPeriod",(DbType)SqlDbType.DateTime,10,_DocFileInfo.FileValidPeriod),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_FileInfo]");
            sb.Append("([FileName]");
            sb.Append(",[FileFoldName]");
            sb.Append(",[FileES]");
            sb.Append(",[FileEdition]");
            sb.Append(",[FileModifyDate]");
            sb.Append(",[FileModifyUserID]");
            sb.Append(",[FileSize]");
            sb.Append(",[FileNote]");
            sb.Append(",[FileValidPeriod]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@FileName");
            sb.Append(",@FileFoldName");
            sb.Append(",@FileES");
            sb.Append(",@FileEdition");
            sb.Append(",@FileModifyDate");
            sb.Append(",@FileModifyUserID");
            sb.Append(",@FileSize");
            sb.Append(",@FileNote");
            sb.Append(",@FileValidPeriod");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        public string UpDateFileInfoByFileName(DocFileInfo _DocFileInfo)
        {

            DbParameter[] pramsUpDate = {
									     MakeInParam("@FileName",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileName),
                                         MakeInParam("@FileFoldName",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileFolderName),
                                         MakeInParam("@FileES",(DbType)SqlDbType.Int,4,_DocFileInfo.FileES),
                                         MakeInParam("@FileEdition",(DbType)SqlDbType.Int,4,_DocFileInfo.FileEdition),
                                         MakeInParam("@FileModifyDate",(DbType)SqlDbType.DateTime,10,_DocFileInfo.FileModifyDate),
                                         MakeInParam("@FileModifyUserID",(DbType)SqlDbType.Int,4,_DocFileInfo.FileModifyUserId),
                                         MakeInParam("@FileSize",(DbType)SqlDbType.Int,4,_DocFileInfo.FileSize),
                                         MakeInParam("@FileNote",(DbType)SqlDbType.VarChar,50,_DocFileInfo.FileNote),
                                         MakeInParam("@FileValidPeriod",(DbType)SqlDbType.DateTime,10,_DocFileInfo.FileValidPeriod),
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Doc_FileInfo]");
            sb.Append(" set ");
            sb.Append(" [FileName]=@FileName,");
            sb.Append(" [FileFoldName]=@FileFoldName,");
            sb.Append(" [FileES]=@FileES,");
            sb.Append(" [FileEdition]=@FileEdition,");
            sb.Append(" [FileModifyDate]=@FileModifyDate,");
            sb.Append(" [FileModifyUserID]=@FileModifyUserID,");
            sb.Append(" [FileSize]=@FileSize,");
            sb.Append(" [FileNote]=@FileNote,");
            sb.Append(" [FileValidPeriod]=@FileValidPeriod");
            sb.Append(" where [FileName]=@FileName  and [FileFoldName]=@FileFoldName");
            sb.Append(" select @@identity;");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }

        public void UpDateFileInfo()
        {
             
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Doc_FileInfo]");
            sb.Append(" set ");
            sb.Append(" [FileEdition]=0");
            sb.Append(" select @@identity;");
            ExecuteScalar(CommandType.Text, sb.ToString());
 
        }

        public DocFileInfo GetFileInfoEntityByFileName(string fileName, string foldName)
        {
            string sql = "select * from Doc_FileInfo Where FileName=@fileName and  FileFoldName=@foldName";
            DbParameter[] pramsGet = {
									   MakeInParam("@fileName",(DbType)SqlDbType.VarChar,50,fileName ),
                                       MakeInParam("@foldName",(DbType)SqlDbType.VarChar,50,foldName ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetFileInfoFromIDataReader(sr);
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

        private DocFileInfo GetFileInfoFromIDataReader(DbDataReader dr)
        {
            DocFileInfo dt = new DocFileInfo();
            if (dr.FieldCount > 0)
            {
                dt.FileSerialID = Int32.Parse(dr["FileSerial"].ToString());
                dt.FileEdition =Int32.Parse(dr["FileEdition"].ToString());
                dt.FileES = Int32.Parse(dr["FileES"].ToString());
                dt.FileFolderName = dr["FileFoldName"].ToString();
                dt.FileModifyDate =DateTime.Parse(dr["FileModifyDate"].ToString());
                dt.FileModifyUserId =Int32.Parse(dr["FileModifyUserID"].ToString());
                dt.FileName = dr["FileName"].ToString();
                dt.FileNote = dr["FileNote"].ToString();
                dt.FileSize =Int32.Parse(dr["FileSize"].ToString());
                dt.FileValidPeriod =DateTime.Parse(dr["FileValidPeriod"].ToString());
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }

        public DataTable GetFileByFileName(string fileName)
        {
            string sql = "";

            sql = string.Format("select * from Doc_FileInfo where FileName like '%{0}%' ", fileName);

            return ExecuteDataset(sql).Tables[0];
        }

        #endregion

        #region "文件版本信息"

        public DataTable GetFileEditionInfoByID(int ID)
        {
            string sql = "";

            sql = string.Format("select * from [Doc_FileEdition] where FileID={0} ", ID);

            return ExecuteDataset(sql).Tables[0];

        }

        public string AddFileEditionInfo(DocFileEdition _DocFileEdition)
        {

            DbParameter[] pramsInsert = {
									
									     MakeInParam("@FileID",(DbType)SqlDbType.Int,4,_DocFileEdition.FileID),
                          
                                      
                                         MakeInParam("@FileEdition",(DbType)SqlDbType.VarChar,50,_DocFileEdition.FileEdition),
                                         MakeInParam("@ModifyDate",(DbType)SqlDbType.DateTime,10,_DocFileEdition.ModifyDate),
                                         MakeInParam("@ModifyUser",(DbType)SqlDbType.VarChar,50,_DocFileEdition.ModifyUserName),
                                        
                                         MakeInParam("@FileNote",(DbType)SqlDbType.VarChar,50,_DocFileEdition.FileNote),
                                          MakeInParam("@FileUrl",(DbType)SqlDbType.VarChar,50,_DocFileEdition.FileUrl),
                                         MakeInParam("@FileName",(DbType)SqlDbType.VarChar,50,_DocFileEdition.FileName),
                                        
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Doc_FileEdition]");
            sb.Append("([FileID]");
            sb.Append(",[FileEdition]");
            sb.Append(",[ModifyUser]");
            sb.Append(",[ModifyDate]");
            sb.Append(",[FileNote]");
            sb.Append(",[FileUrl]");
            sb.Append(",[FileName]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@FileID");
            sb.Append(",@FileEdition");
            sb.Append(",@ModifyUser");
            sb.Append(",@ModifyDate");
            sb.Append(",@FileNote");
            sb.Append(",@FileUrl");
            sb.Append(",@FileName");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

      


        #endregion

        #region "获取用户角色和部门"

        public DataTable GetSysRoleIDByUserID(int UserSerialID)
        {
            string sql = "";

            sql = string.Format("select * from SysRoleUser where UserSerialID={0} ", UserSerialID);

            return ExecuteDataset(sql).Tables[0];
        }

        public DataTable GetDepartIDByUserID(int UserSerialID)
        {
            string sql = "";

            sql = string.Format("select * from PEEBI where PEEBIID={0} ", UserSerialID);

            return ExecuteDataset(sql).Tables[0];
        }

        #endregion

        #region "日历"

        public string AddNewsCalender(MyCalendar _MyCalendar)
        {

            DbParameter[] pramsInsert = {
									
									       MakeInParam("@ID",(DbType)SqlDbType.Int,4,_MyCalendar.ID),
                                           MakeInParam("@UID",(DbType)SqlDbType.VarChar,50,_MyCalendar.UID),
                                           MakeInParam("@EID",(DbType)SqlDbType.VarChar,50,_MyCalendar.EID),
                                           MakeInParam("@EName",(DbType)SqlDbType.VarChar,50,_MyCalendar.EName),
                                           MakeInParam("@STime",(DbType)SqlDbType.VarChar,50,_MyCalendar.STime),
                                           MakeInParam("@ETime",(DbType)SqlDbType.VarChar,50,_MyCalendar.ETime),
                                           MakeInParam("@CTime",(DbType)SqlDbType.VarChar,50,_MyCalendar.CTime),
                                           MakeInParam("@MEMO",(DbType)SqlDbType.VarChar,50,_MyCalendar.MEMO),
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[Calendar]");
            sb.Append("([ID]");
            sb.Append(",[UID]");
            sb.Append(",[EID]");
            sb.Append(",[EName]");
            sb.Append(",[STime]");
            sb.Append(",[ETime]");
            sb.Append(",[CTime]");
            sb.Append(",[MEMO]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@ID");
            sb.Append(",@UID");
            sb.Append(",@EID");
            sb.Append(",@EName");
            sb.Append(",@STime");
            sb.Append(",@ETime");
            sb.Append(",@CTime");
            sb.Append(",@MEMO");
            sb.Append(")");
            sb.Append(" select @ID;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        public string UpDateCalendar(MyCalendar _MyCalendar)
        {

            DbParameter[] pramsUpDate = {
									       MakeInParam("@ID",(DbType)SqlDbType.Int,4,_MyCalendar.ID),
                                           MakeInParam("@UID",(DbType)SqlDbType.VarChar,50,_MyCalendar.UID),
                                           MakeInParam("@EID",(DbType)SqlDbType.VarChar,50,_MyCalendar.EID),
                                           MakeInParam("@EName",(DbType)SqlDbType.VarChar,50,_MyCalendar.EName),
                                           MakeInParam("@STime",(DbType)SqlDbType.VarChar,50,_MyCalendar.STime),
                                           MakeInParam("@ETime",(DbType)SqlDbType.VarChar,50,_MyCalendar.ETime),
                                           MakeInParam("@CTime",(DbType)SqlDbType.VarChar,50,_MyCalendar.CTime),
                                           MakeInParam("@MEMO",(DbType)SqlDbType.VarChar,50,_MyCalendar.MEMO),
                                             };

            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[Calendar]");
            sb.Append(" set ");
            sb.Append(" [ID]=@ID,");
            sb.Append(" [UID]=@UID,");
            sb.Append(" [EID]=@EID,");
            sb.Append(" [EName]=@EName,");
            sb.Append(" [STime]=@STime,");
            sb.Append(" [ETime]=@ETime,");
            sb.Append(" [CTime]=@CTime,");
            sb.Append(" [MEMO]=@MEMO");
            sb.Append(" where [UID]=@UID  and [EID]=@EID");
            sb.Append(" select @ID ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }


        public DataTable FindCalendarByUEID(string UID , string EID)
        {
            string sql = "";

            sql = string.Format(" select * from Calendar where UID='{0}' and EID='{1}' ", UID,EID);

            return ExecuteDataset(sql).Tables[0];

        }


        public DataTable FindCalendarByUID(string UID)
        {
            string sql = "";

            sql = string.Format(" select * from Calendar where UID='{0}'  ", UID);

            return ExecuteDataset(sql).Tables[0];

        }

        public MyCalendar GetCalendarEntityByUEID(string UID, string EID)
        {
            string sql = "select * from Calendar Where UID=@UID and  EID=@EID";
            DbParameter[] pramsGet = {
									   MakeInParam("@UID",(DbType)SqlDbType.VarChar,50,UID ),
                                       MakeInParam("@EID",(DbType)SqlDbType.VarChar,50,EID ),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetCalendarFromIDataReader(sr);
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

        private MyCalendar GetCalendarFromIDataReader(DbDataReader dr)
        {
            MyCalendar dt = new MyCalendar();
            if (dr.FieldCount > 0)
            {
                dt.CTime = dr["CTime"].ToString();
                dt.EID = dr["EID"].ToString();
                dt.EName = dr["EName"].ToString();
                dt.ETime = dr["ETime"].ToString();
                dt.ID = Int32.Parse(dr["ID"].ToString());
                dt.MEMO = dr["MEMO"].ToString();
                dt.STime = dr["STime"].ToString();
                dt.UID = dr["UID"].ToString();
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }



        #endregion

        #region "日历权限"

        public string AddNewsCalenderPermission(CalendarPermission _CalendarEventPermission)
        {

            DbParameter[] pramsInsert = {
									
									       MakeInParam("@CalendarID",(DbType)SqlDbType.Int,4,_CalendarEventPermission.CalendarID),
                                         
                                           MakeInParam("@CalendarPermission",(DbType)SqlDbType.VarChar,50,_CalendarEventPermission.CalendarPer),
                                           MakeInParam("@CalendarPermissionUserID",(DbType)SqlDbType.Int,4,_CalendarEventPermission.CalendarPermissionUserID),
             
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[CalendarPermission]");
            sb.Append("([CalendarID]");

            sb.Append(",[CalendarPermission]");
            sb.Append(",[CalendarPermissionUserID]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@CalendarID");

            sb.Append(",@CalendarPermission");
            sb.Append(",@CalendarPermissionUserID");

            sb.Append(")");
            sb.Append(" select @@identity;");

            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString();
        }

        public string UpDateCalendarPermission(CalendarPermission _CalendarEventPermission)
        {

            DbParameter[] pramsUpDate = {
									          MakeInParam("@CalendarID",(DbType)SqlDbType.Int,4,_CalendarEventPermission.CalendarID),
                         
                                           MakeInParam("@CalendarPermission",(DbType)SqlDbType.VarChar,50,_CalendarEventPermission.CalendarPer),
                                           MakeInParam("@CalendarPermissionUserID",(DbType)SqlDbType.Int,4,_CalendarEventPermission.CalendarPermissionUserID),
                                             };

            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[CalendarPermission]");
            sb.Append(" set ");
            sb.Append(" [CalendarID]=@CalendarID,");

            sb.Append(" [CalendarPermission]=@CalendarPermission,");
            sb.Append(" [CalendarPermissionUserID]=@CalendarPermissionUserID");
            sb.Append(" where [CalendarPermissionUserID]=@CalendarPermissionUserID  and [CalendarID]=@CalendarID");
            sb.Append(" select @@identity ");
            return ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString();
        }


        public DataTable FindCalendarPermissionByID(int CalendarID)
        {
            string sql = "";

            sql = string.Format(" select * from CalendarPermission where CalendarID={0}  ", CalendarID);

            return ExecuteDataset(sql).Tables[0];

        }


        public CalendarPermission GetCalendarPermissionEntityByID(int CalendarID, int PermissionUserID)
        {
            string sql = "select * from CalendarPermission Where CalendarID=@CalendarID and  CalendarPermissionUserID=@CalendarPermissionUserID";
            DbParameter[] pramsGet = {
									   MakeInParam("@CalendarID",(DbType)SqlDbType.Int,4,CalendarID ),
                                       MakeInParam("@CalendarPermissionUserID",(DbType)SqlDbType.Int,4,PermissionUserID),
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetCalendarPermissionFromIDataReader(sr);
                }
                else
                    return null;
                // throw new Exception(ResourceManager.GetString("CANNOT_FIND_RECORD"));
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

        private CalendarPermission GetCalendarPermissionFromIDataReader(DbDataReader dr)
        {
            CalendarPermission dt = new CalendarPermission();
            if (dr.FieldCount > 0)
            {
                dt.CalendarID = Int32.Parse(dr["CalendarID"].ToString());
                dt.CalendarPer = dr["CalendarPermission"].ToString();
                dt.CalendarPermissionSerialID = Int32.Parse(dr["CalendarPermissionSerialID"].ToString());
                dt.CalendarPermissionUserID = Int32.Parse(dr["CalendarPermissionUserID"].ToString());
                return dt;
            }
            dr.Close();
            return null;
        }



        #endregion

        #region "日历事件"

        public int AddNewsCalenderEvent(CalendarEventEntity _CalendarEvent)
        {

            DbParameter[] pramsInsert = {
                                           MakeInParam("@CalendarEventTitle",(DbType)SqlDbType.VarChar,50,_CalendarEvent.Title),
                                           MakeInParam("@CalendarEventContent",(DbType)SqlDbType.VarChar,100,_CalendarEvent.Content),
                                           MakeInParam("@CalendarEventType",(DbType)SqlDbType.Int,4,_CalendarEvent.Type),
                                           MakeInParam("@CalendarEventStartTime",(DbType)SqlDbType.DateTime,50,_CalendarEvent.StartTime),
                                           MakeInParam("@CalendarEventEndTime",(DbType)SqlDbType.DateTime,50,_CalendarEvent.EndTime),
                                           MakeInParam("@CalendarEventInvite",(DbType)SqlDbType.VarChar,1000,_CalendarEvent.Invite),
                                           MakeInParam("@CalendarEventEmailNote",(DbType)SqlDbType.VarChar,50,_CalendarEvent.EmailNote),
                                           MakeInParam("@CalendarEventNote",(DbType)SqlDbType.VarChar,50,_CalendarEvent.Note),
                                           MakeInParam("@CalendarEventNoteBefore",(DbType)SqlDbType.Int ,4,_CalendarEvent.NoteBefore),
                                           MakeInParam("@CalendarEventRepeat",(DbType)SqlDbType.VarChar,50,_CalendarEvent.Repeat),
                                           MakeInParam("@CalendarEventRepeatRate",(DbType)SqlDbType.Int,4,_CalendarEvent.RepeatRate),
                                           MakeInParam("@CalendarEventUserID",(DbType)SqlDbType.Int,4,_CalendarEvent.UserID),   
                                             };
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO [dbo].[CalendarEvent]");
            sb.Append("([CalendarEventTitle]");
            sb.Append(",[CalendarEventContent]");
            sb.Append(",[CalendarEventType]");
            sb.Append(",[CalendarEventStartTime]");
            sb.Append(",[CalendarEventEndTime]");
            sb.Append(",[CalendarEventInvite]");
            sb.Append(",[CalendarEventEmailNote]");
            sb.Append(",[CalendarEventNote]");
            sb.Append(",[CalendarEventNoteBefore]");
            sb.Append(",[CalendarEventRepeat]");
            sb.Append(",[CalendarEventRepeatRate]");
            sb.Append(",[CalendarEventUserID]");
            sb.Append(")");
            sb.Append("VALUES");
            sb.Append("(@CalendarEventTitle");
            sb.Append(",@CalendarEventContent");
            sb.Append(",@CalendarEventType");
            sb.Append(",@CalendarEventStartTime");
            sb.Append(",@CalendarEventEndTime");
            sb.Append(",@CalendarEventInvite");
            sb.Append(",@CalendarEventEmailNote");
            sb.Append(",@CalendarEventNote");
            sb.Append(",@CalendarEventNoteBefore");
            sb.Append(",@CalendarEventRepeat");
            sb.Append(",@CalendarEventRepeatRate");
            sb.Append(",@CalendarEventUserID");
            sb.Append(")");
            sb.Append(" select @@identity;");

            return Int32.Parse(ExecuteScalar(CommandType.Text, sb.ToString(), pramsInsert).ToString());
        }


        public DataTable GetCalenderEventByID(int id)
        {
            string sql = "";
            sql = string.Format("select * from [CalendarEvent] where  CalendarEventUserial={0}", id);
            return ExecuteDataset(sql).Tables[0];
        }

        public DataTable GetCalenderEventListByID(int CalendarEventUserID)
        {
            string sql = "";
            sql = string.Format("select * from [CalendarEvent] where CalendarEventUserID={0} ", CalendarEventUserID);
            return ExecuteDataset(sql).Tables[0];
        }

        public int UpDateCalendarEvnetByID(CalendarEventEntity _CalendarEventEntity)
        {

            DbParameter[] pramsUpDate = {
									       MakeInParam("@CalendarEventTitle",(DbType)SqlDbType.VarChar,50,_CalendarEventEntity.Title),
                                           MakeInParam("@CalendarEventContent",(DbType)SqlDbType.VarChar,100,_CalendarEventEntity.Content),
                                           MakeInParam("@CalendarEventType",(DbType)SqlDbType.Int,4,_CalendarEventEntity.Type),
                                           MakeInParam("@CalendarEventStartTime",(DbType)SqlDbType.DateTime,50,_CalendarEventEntity.StartTime),
                                           MakeInParam("@CalendarEventEndTime",(DbType)SqlDbType.DateTime,50,_CalendarEventEntity.EndTime),
                                           MakeInParam("@CalendarEventInvite",(DbType)SqlDbType.VarChar,1000,_CalendarEventEntity.Invite),
                                           MakeInParam("@CalendarEventEmailNote",(DbType)SqlDbType.VarChar,50,_CalendarEventEntity.EmailNote),
                                           MakeInParam("@CalendarEventNote",(DbType)SqlDbType.VarChar,50,_CalendarEventEntity.Note),
                                           MakeInParam("@CalendarEventNoteBefore",(DbType)SqlDbType.Int ,4,_CalendarEventEntity.NoteBefore),
                                           MakeInParam("@CalendarEventRepeat",(DbType)SqlDbType.VarChar,50,_CalendarEventEntity.Repeat),
                                           MakeInParam("@CalendarEventRepeatRate",(DbType)SqlDbType.Int,4,_CalendarEventEntity.RepeatRate),
                                           MakeInParam("@CalendarEventUserID",(DbType)SqlDbType.Int,4,_CalendarEventEntity.UserID),   
                                          MakeInParam("@CalendarEventUserial",(DbType)SqlDbType.Int,4,_CalendarEventEntity.Userial),   
                        };
            StringBuilder sb = new StringBuilder();
            sb.Append("UpDate [dbo].[CalendarEvent]");
            sb.Append(" set ");
            sb.Append(" [CalendarEventUserial]=@CalendarEventUserial,");
            sb.Append(" [CalendarEventUserID]=@CalendarEventUserID,");
            sb.Append(" [CalendarEventTitle]=@CalendarEventTitle,");
            sb.Append(" [CalendarEventContent]=@CalendarEventContent,");
            sb.Append(" [CalendarEventType]=@CalendarEventType,");
            sb.Append(" [CalendarEventStartTime]=@CalendarEventStartTime,");
            sb.Append(" [CalendarEventEndTime]=@CalendarEventEndTime,");
            sb.Append(" [CalendarEventInvite]=@CalendarEventInvite,");
            sb.Append(" [CalendarEventEmailNote]=@CalendarEventEmailNote,");
            sb.Append(" [CalendarEventNote]=@CalendarEventNote,");
            sb.Append(" [CalendarEventNoteBefore]=@CalendarEventNoteBefore,");
            sb.Append(" [CalendarEventRepeat]=@CalendarEventRepeat,");
            sb.Append(" [CalendarEventRepeatRate]=@CalendarEventRepeatRate");
            sb.Append(" where [CalendarEventUserial]=@CalendarEventUserial  and [CalendarEventUserID]=@CalendarEventUserID");
            sb.Append(" select @@identity;");
            return Int32.Parse(ExecuteScalar(CommandType.Text, sb.ToString(), pramsUpDate).ToString());
        }

        public DataTable GetCalendarByDate(DateTime start, DateTime end, int UserID)
        {
            string sql = "";
            sql = string.Format("select * from [CalendarEvent] where DATEDIFF ([second], CalendarEventStartTime, '{0}')  < 0  and  DATEDIFF ([second], CalendarEventEndTime, '{1}')  >0 and CalendarEventUserID={2} ", start, end, UserID);
            return ExecuteDataset(sql).Tables[0];

        }

        public CalendarEventEntity GetCalendarEntityByID(int id)
        {
            string sql = "select * from CalendarEvent Where CalendarEventUserial=@CalendarEventUserial";
            DbParameter[] pramsGet = {
									   MakeInParam("@CalendarEventUserial",(DbType)SqlDbType.Int,4,id ),
                     
                                             };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetCalendarEntityFromIDataReader(sr);
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

        private CalendarEventEntity GetCalendarEntityFromIDataReader(DbDataReader dr)
        {
            CalendarEventEntity dt = new CalendarEventEntity();
            if (dr.FieldCount > 0)
            {
                dt.Content = dr["CalendarEventContent"].ToString();
                dt.EmailNote = dr["CalendarEventEmailNote"].ToString();
                dt.EndTime = DateTime.Parse(dr["CalendarEventEndTime"].ToString());
                dt.Invite = dr["CalendarEventInvite"].ToString();
                dt.Note = dr["CalendarEventNote"].ToString();
                dt.NoteBefore = Int32.Parse(dr["CalendarEventNoteBefore"].ToString());
                dt.Repeat = dr["CalendarEventRepeat"].ToString();
                dt.RepeatRate = Int32.Parse(dr["CalendarEventRepeatRate"].ToString());

                dt.StartTime = DateTime.Parse(dr["CalendarEventStartTime"].ToString());
                dt.Title = dr["CalendarEventTitle"].ToString();
                dt.Type = Int32.Parse(dr["CalendarEventType"].ToString());
                dt.Userial = Int32.Parse(dr["CalendarEventUserial"].ToString());
                dt.UserID = Int32.Parse(dr["CalendarEventUserID"].ToString());

                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }


        #endregion

    }
}
