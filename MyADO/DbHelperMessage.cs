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
        /// 返回实体内容
        /// </summary>
        /// <param name="MessageID"></param>
        /// <returns></returns>
        public MFPAJ100Entity GetMFPAJ100EntityByKeyCol(int MessageID)
        {
            string sql = "select * from MFPAJ100 Where MessageID=@MessageID";
            DbParameter[] pramsGet = {
                                         MakeInParam("@MessageID",(DbType)SqlDbType.Int,4,MessageID ),
                                     };
            DbDataReader sr = null;
            try
            {
                sr = ExecuteReader(CommandType.Text, sql, pramsGet);
                if (sr.Read())
                {
                    return GetMFPAJ100FromIDataReader(sr);
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
        private MFPAJ100Entity GetMFPAJ100FromIDataReader(DbDataReader dr)
        {
            MFPAJ100Entity dt = new MFPAJ100Entity();
            if (dr.FieldCount > 0)
            {
                if (dr["MessageID"].ToString() != "" || dr["MessageID"] != null) dt.MessageID = Int32.Parse(dr["MessageID"].ToString());
                dt.MessageType = dr["MessageType"].ToString();
                dt.MessageDetail = dr["MessageDetail"].ToString();
                if (dr["DisplayOrder"].ToString() != "" || dr["DisplayOrder"] != null) dt.DisplayOrder = Int32.Parse(dr["DisplayOrder"].ToString());
                if (dr["CreatorSID"].ToString() != "" || dr["CreatorSID"] != null) dt.CreatorSID = Int32.Parse(dr["CreatorSID"].ToString());
                dt.CreateDate = Convert.ToDateTime(dr["CreateDate"]);
                dr.Close();
                return dt;
            }
            dr.Close();
            return null;
        }
    }
}
