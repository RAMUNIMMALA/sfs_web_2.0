using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sfs_model;

namespace scs_datarepository
{
   public class DP_Manager
    {
       Partner _Partner = null;
       TotalCount _TotalCount = null; 
       public TotalCount GetTotalCounts()
       {
           _TotalCount = new TotalCount();
           DataTable dtCount = null;
           try
           {
               IDbDataParameter[] ArrParameter = new IDbDataParameter[] { };
               dtCount = DB_UTILITY.RunSP("usp_TotalCounts", ArrParameter);
               if (dtCount != null && dtCount.Rows.Count > 0)
               {
                   _TotalCount = OBJECT_UTILITY.GetConvert<TotalCount>(dtCount);
               }
            }
           catch (Exception ex)
            {
                throw ex;
            }
            return _TotalCount;
       }

       public Partner RegisterUser(Partner _PartnerData)
       {
           _Partner = new Partner();
           DataTable dtPartner = null;
           try
           {
               IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iFirstName", DbType.String, ParameterDirection.Input,_PartnerData.FirstName),
                    DB_UTILITY.CreateParameter("@iLastName", DbType.String, ParameterDirection.Input,_PartnerData.LastName),
                    DB_UTILITY.CreateParameter("@iGender", DbType.String, ParameterDirection.Input,_PartnerData.GenderType),
                    DB_UTILITY.CreateParameter("@iOccupation", DbType.String, ParameterDirection.Input,_PartnerData.Occupation),
                    DB_UTILITY.CreateParameter("@iContactNo", DbType.String, ParameterDirection.Input,_PartnerData.ContactNo),
                    DB_UTILITY.CreateParameter("@iMailId", DbType.String, ParameterDirection.Input,_PartnerData.MailId),
                    DB_UTILITY.CreateParameter("@iRefName", DbType.String, ParameterDirection.Input,_PartnerData.RefName),
                    DB_UTILITY.CreateParameter("@iImage", DbType.String, ParameterDirection.Input,_PartnerData.Image),
                    DB_UTILITY.CreateParameter("@iType", DbType.Int32,ParameterDirection.Input,_PartnerData.PartnerType),
                    DB_UTILITY.CreateParameter("@iStatus", DbType.Int32,ParameterDirection.Input,_PartnerData.StatusType),
                 };
               dtPartner = DB_UTILITY.RunSP("usp_insertPartner", ArrParameter);
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

    }
}
    
