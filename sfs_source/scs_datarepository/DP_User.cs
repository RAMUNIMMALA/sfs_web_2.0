using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using sfs_model;

namespace scs_datarepository
{
    public class DP_User
    {
        COMMON_UTILITY _CU = new COMMON_UTILITY();
        Partner _Partner = null;
        
        public Partner CheckUserInfo(string strUserID, string strPassword)
        {
            DataTable dtPartner = null;
            _Partner = new Partner();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iUserId", DbType.String, ParameterDirection.Input, strUserID),
                    DB_UTILITY.CreateParameter("@iPassword", DbType.String, ParameterDirection.Input,strPassword)                
                };
                dtPartner = DB_UTILITY.RunSP("usp_fetchUserInfo", ArrParameter);
                if (dtPartner != null && dtPartner.Rows.Count > 0)
                {
                    _Partner = OBJECT_UTILITY.GetConvert<Partner>(dtPartner);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _Partner;
        }

        public void SendMail(string FromID, string FromPWD, string DestinationID, string Subject, string Details, Char Mode)
        {
            try
            {
                _CU.SendMail(FromID, FromPWD, DestinationID, Subject, Details, Mode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
