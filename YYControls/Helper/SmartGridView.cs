using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;
using System.Web.UI;

namespace YYControls.Helper
{
    /// <summary>
    /// SmartGridView的Helper
    /// </summary>
    public class SmartGridView
    {
        /// <summary>
        /// 获取GridView中通过复选框选中的行的DataKey集合
        /// </summary>
        /// <param name="gv">GridView</param>
        /// <param name="columnIndex">CheckBox在GridView中的列索引</param>
        /// <returns></returns>
        public static List<DataKey> GetCheckedDataKey(GridView gv, int columnIndex)
        {
            if (gv.DataKeyNames.Length == 0)
            {
                throw new ArgumentNullException("DataKeys", "未设置GridView的DataKeyNames");
            }

            List<DataKey> list = new List<DataKey>();

            int i = 0;
            foreach (GridViewRow gvr in gv.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    foreach (Control c in gvr.Cells[columnIndex].Controls)
                    {
                        if (c is CheckBox && ((CheckBox)c).Checked)
                        {
                            list.Add(gv.DataKeys[i]);
                            break;
                        }
                    }

                    i++;
                }
            }

            return list;
        }

        /// <summary>
        /// 获取GridView中通过复选框选中的行的DataKey集合
        /// </summary>
        /// <param name="gv">GridView</param>
        /// <param name="checkboxId">CheckBox的ID</param>
        /// <returns></returns>
        public static List<DataKey> GetCheckedDataKey(GridView gv, string checkboxId)
        {
            return GetCheckedDataKey(gv, GetColumnIndex(gv, checkboxId));
        }

        /// <summary>
        /// 获取列索引        /// </summary>
        /// <param name="gv">GridView</param>
        /// <param name="controlId">控件ID</param>
        /// <returns></returns>
        public static int GetColumnIndex(GridView gv, string controlId)
        {
            foreach (GridViewRow gvr in gv.Rows)
            {
                for (int i = 0; i < gvr.Cells.Count; i++)
                {
                    foreach (Control c in gvr.Cells[i].Controls)
                    {
                        if (c.ID == controlId)
                        {
                            return i;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// 合并指定列的相邻且内容相同的单元格        /// </summary>
        /// <param name="gv">GridView</param>
        /// <param name="columnIndices">需要合并单元格的列的索引（用逗号“,”分隔）</param>
        public static void MergeCells(GridView gv, int[] columnIndices)
        {
            // 指定的列中需要设置RowSpan的单元格的行索引
            int[] aryInt = new int[columnIndices.Length];
            // 是否重新指定aryInt的相关元素的值
            // aryInt中的元素与aryBln中的元素一一对应
            bool[] aryBln = new bool[columnIndices.Length];
            // aryInt初值均为0
            for (int i = 0; i < aryInt.Length; i++)
            {
                aryInt[i] = 0;
            }
            // aryBln初值均为true
            for (int i = 0; i < aryBln.Length; i++)
            {
                aryBln[i] = true;
            }

            for (int i = 1; i < gv.Rows.Count; i++)
            {
                // 本行和上一行均为DataControlRowType.DataRow
                if (gv.Rows[i].RowType == DataControlRowType.DataRow && gv.Rows[i - 1].RowType == DataControlRowType.DataRow)
                {
                    // 遍历指定的列索引
                    for (int j = 0; j < columnIndices.Length; j++)
                    {
                        // 列索引超出范围则不处理
                        if (columnIndices[j] < 0 || columnIndices[j] > gv.Columns.Count - 1) continue;

                        // 相邻单元格的内容相同
                        if (gv.Rows[i].Cells[columnIndices[j]].Text == gv.Rows[i - 1].Cells[columnIndices[j]].Text)
                        {
                            if (aryBln[j])
                                aryInt[j] = i - 1;

                            if (gv.Rows[aryInt[j]].Cells[columnIndices[j]].RowSpan == 0)
                                gv.Rows[aryInt[j]].Cells[columnIndices[j]].RowSpan = 1;

                            gv.Rows[aryInt[j]].Cells[columnIndices[j]].RowSpan++;
                            gv.Rows[i].Cells[columnIndices[j]].Visible = false;

                            aryBln[j] = false;
                        }
                        else
                        {
                            aryBln[j] = true;
                        }
                    }
                }
            }
        }
    }
}
