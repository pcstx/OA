using System;
using System.Collections.Generic;
using System.Text;

using System.Web.UI.WebControls;

namespace YYControls.SmartGridViewFunction
{
    /// <summary>
    /// 扩展功能：合并指定列的相邻且内容相同的单元格
    /// </summary>
    public class MergeCellsFunction : ExtendFunction
    {
        /// <summary>
        /// 构造函数        /// </summary>
        public MergeCellsFunction()
            : base()
        {

        }

        /// <summary>
        /// 构造函数        /// </summary>
        /// <param name="sgv">SmartGridView对象</param>
        public MergeCellsFunction(SmartGridView sgv)
            : base(sgv)
        {
    
        }

        /// <summary>
        /// 扩展功能的实现        /// </summary>
        protected override void Execute()
        {
            this._sgv.DataBound += new EventHandler(_sgv_DataBound);
        }

        /// <summary>
        /// SmartGridView的DataBound事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _sgv_DataBound(object sender, EventArgs e)
        {
            string[] ary = this._sgv.MergeCells.Split(',');
            int[] columnIndices = new int[ary.Length];

            // 将字符串数组转为整型数组
            for (int i = 0; i < columnIndices.Length; i++)
            {
                int j;
                if (!Int32.TryParse(ary[i], out j))
                {
                    // 转整型失败则赋值为-1，“合并指定列的相邻且内容相同的单元格”则不会处理
                    j = -1;
                }

                columnIndices[i] = j;
            }

            YYControls.Helper.SmartGridView.MergeCells(this._sgv, columnIndices);
        }
    }
}
