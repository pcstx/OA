//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web;
using GPRP.GPRPEnumerations;

namespace GPRP.GPRPComponents
{

	/// <summary>
	/// Summary description for AuditSummary.
	/// </summary>
	public class AuditSummary {
		Hashtable collection = null;
        int total = 0;

        public AuditSummary () {
            collection = new Hashtable();
		}

        #region Properties
        public Hashtable Collection { 
            get { 
                return collection;
            }
            set { 
                collection = value;
            }
        }

        public int this [ModeratorActions key] { 
            get {
                try {
                    return (int) collection[key];
                } 
                catch { 
                    return 0;
                }
            }
            set {
                collection[key] = value;
                total += value;
            }
        }

        public int UpdatedTotal { 
            get { 
                int updatedTotal = 0;

                if (collection != null) { 
                    IDictionaryEnumerator iterator = collection.GetEnumerator();

                    while (iterator.MoveNext()) { 
                        if (iterator.Value != null)
                            updatedTotal += (int) iterator.Value;
                    }
                }

                return updatedTotal;
            }
        }
        
        public int Total {
            get {
                return total;
            }
        }

        public bool HasActions {
            get { return (this.Total > 0 ? true : false); }
        }
        #endregion
	}
}
