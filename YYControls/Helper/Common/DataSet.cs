using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;
using System.Data;

namespace YYControls.Helper
{
    /// <summary>
    /// 常用Helper
    /// </summary>
    public partial class Common
    {
        /// <summary>
        /// 根据数据列的列名取数据列的列索引
        /// </summary>
        /// <param name="dcc">数据列集合</param>
        /// <param name="columnName">数据列的列名</param>
        /// <returns></returns>
        public static int GetColumnIndexByColumnName(DataColumnCollection dcc, string columnName)
        {
            int result = -1;

            for (int i = 0; i < dcc.Count; i++)
            {
                if (dcc[i].ColumnName.ToLower() == columnName.ToLower())
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
