using System;
using System.Collections.Generic;
using System.Text;
namespace GPRP.Entity
{
    /// <summary>
    /// ΚµΜε
    /// </summary>
    public class UserRightEntity
    {

        private string m_userID;//
        private string m_menuID;//
        /// <summary>
        ///
        /// </summary>
        public string userID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        /// <summary>
        ///
        /// </summary>
        public string menuID
        {
            get { return m_menuID; }
            set { m_menuID = value; }
        }
    }
}