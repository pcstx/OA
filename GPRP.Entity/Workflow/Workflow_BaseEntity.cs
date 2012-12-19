using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// 实体
    /// </summary>
    public class Workflow_BaseEntity
    {
        private int m_WorkflowID;//流程编号
        private string m_WorkflowName;//流程名称
        private string m_WorkflowDesc;//流程描述
        private int m_FlowTypeID;//流程类型
        private int m_FormID;//对应表单编号
        private int m_IsValid;//是否启用
        private int m_IsMailNotice;//是否邮件提醒
        private int m_IsMsgNotice;//是否短信提醒
        private int m_IsTransfer;//是否允许转发
        private int m_AttachDocPath;//上传附件的目录
        private int m_HelpDocPath;//帮助文档
        private int m_DisplayOrder;//显示顺序
        /// <summary>
        ///流程编号
        /// </summary>
        public int WorkflowID
        {
            get { return m_WorkflowID; }
            set { m_WorkflowID = value; }
        }
        /// <summary>
        ///流程名称
        /// </summary>
        public string WorkflowName
        {
            get { return m_WorkflowName; }
            set { m_WorkflowName = value; }
        }
        /// <summary>
        ///流程描述
        /// </summary>
        public string WorkflowDesc
        {
            get { return m_WorkflowDesc; }
            set { m_WorkflowDesc = value; }
        }
        /// <summary>
        ///流程类型
        /// </summary>
        public int FlowTypeID
        {
            get { return m_FlowTypeID; }
            set { m_FlowTypeID = value; }
        }
        /// <summary>
        ///对应表单编号
        /// </summary>
        public int FormID
        {
            get { return m_FormID; }
            set { m_FormID = value; }
        }
        /// <summary>
        ///是否启用
        /// </summary>
        public int IsValid
        {
            get { return m_IsValid; }
            set { m_IsValid = value; }
        }
        /// <summary>
        ///是否邮件提醒
        /// </summary>
        public int IsMailNotice
        {
            get { return m_IsMailNotice; }
            set { m_IsMailNotice = value; }
        }
        /// <summary>
        ///是否短信提醒
        /// </summary>
        public int IsMsgNotice
        {
            get { return m_IsMsgNotice; }
            set { m_IsMsgNotice = value; }
        }
        /// <summary>
        ///是否允许转发
        /// </summary>
        public int IsTransfer
        {
            get { return m_IsTransfer; }
            set { m_IsTransfer = value; }
        }
        /// <summary>
        ///上传附件的目录        /// </summary>
        public int AttachDocPath
        {
            get { return m_AttachDocPath; }
            set { m_AttachDocPath = value; }
        }
        /// <summary>
        ///帮助文档
        /// </summary>
        public int HelpDocPath
        {
            get { return m_HelpDocPath; }
            set { m_HelpDocPath = value; }
        }
        /// <summary>
        ///显示顺序
        /// </summary>
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set { m_DisplayOrder = value; }
        }
    }
}