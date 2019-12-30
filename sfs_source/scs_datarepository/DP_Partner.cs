using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sfs_model;

namespace scs_datarepository
{
    public class DP_Partner
    {
        Partner _Partner = null;
        List<Partner> lstPartner = null;
        
        public Partner ChangeCurrentPassword(string strPartnerId, string strPassword)
        {
            DataTable dtPartner = null;
            _Partner = new Partner();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] 
                {
                        DB_UTILITY.CreateParameter("@iPartnerId", DbType.String, ParameterDirection.Input, strPartnerId),
                        DB_UTILITY.CreateParameter("@iPassword", DbType.String, ParameterDirection.Input, strPassword),
                };
                dtPartner = DB_UTILITY.RunSP("usp_changePassword", ArrParameter);
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

        public List<Partner> FetchPlanPartnerList(int PlanCode)
        {
            DataTable dtPartnerList = null;
            lstPartner = new List<Partner>();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iPlanCode", DbType.Int16, ParameterDirection.Input, PlanCode),
                };
                dtPartnerList = DB_UTILITY.RunSP("usp_fetchPlanPartnerList", ArrParameter);
                if (dtPartnerList != null && dtPartnerList.Rows.Count > 0)
                {
                    lstPartner = OBJECT_UTILITY.GetConvertCollection<Partner>(dtPartnerList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPartner;
        }

        public List<Partner> TotalPartners()
        {
            DataTable dtPartnerList = null;
            lstPartner = new List<Partner>();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] { };
                dtPartnerList = DB_UTILITY.RunSP("usp_fetchPartnerList", ArrParameter);
                if (dtPartnerList != null && dtPartnerList.Rows.Count > 0)
                {
                    lstPartner = OBJECT_UTILITY.GetConvertCollection<Partner>(dtPartnerList);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return lstPartner;
        }

        public Partner FetchPartnerDetails(int PartnerCode)
        {
            DataTable dtPartner = null;
            _Partner = new Partner();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] 
                {
                    DB_UTILITY.CreateParameter("@iPartnerCode", DbType.Int32, ParameterDirection.Input, PartnerCode),
                };
                dtPartner = DB_UTILITY.RunSP("usp_fetchPartnerDetails", ArrParameter);
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

        public Partner UpdatePartnerDetails(Partner _PartnerData)
        {
            DataTable dtPartner = null;
            _Partner = new Partner();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] 
                {
                    DB_UTILITY.CreateParameter("@iPartnerCode", DbType.String, ParameterDirection.Input,_PartnerData.PartnerCode),
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
                dtPartner = DB_UTILITY.RunSP("usp_updatePartnerDetails", ArrParameter);
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
        
        public List<PartnerType> GetPartnerBasicList()
        {
            DataTable dtPlan = null;
            List<PartnerType> lstPartner = new List<PartnerType>();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] { };
                dtPlan = DB_UTILITY.RunSP("usp_fetchPartnerBasicList", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    lstPartner = OBJECT_UTILITY.GetConvertCollection<PartnerType>(dtPlan);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPartner;
        }
        
        //Name :Nallapati Siva KoteswaRarao
        //Description:Member mapped to the plan
        public Plan AddMemberMapping(Plan _Plan)
        {
            DataTable dtPlan = null;
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iPlanCode", DbType.Int32, ParameterDirection.Input,_Plan.PlanCode),                
                    DB_UTILITY.CreateParameter("@iPartnerCode", DbType.Int32, ParameterDirection.Input,_Plan.PartnerCode),
                };
                dtPlan = DB_UTILITY.RunSP("usp_insertMemberMapping", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    _Plan = OBJECT_UTILITY.GetConvert<Plan>(dtPlan);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return _Plan;
        }
        
        public Plan MemberMapping(Plan _PlanData)
        {
            DataTable dtPlan = null;
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iPlanCode", DbType.Int32, ParameterDirection.Input,_PlanData.PlanCode),                
                    DB_UTILITY.CreateParameter("@iPartnerCode", DbType.Int32, ParameterDirection.Input,_PlanData.PartnerCode),
                };
                dtPlan = DB_UTILITY.RunSP("usp_insertPlanMapping", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    _PlanData = OBJECT_UTILITY.GetConvert<Plan>(dtPlan);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return _PlanData;
        }
        
        public Partner GetPartnerPlanMapped(Partner _PartnerData)
        {
            DataTable dtPartner = null;
            Partner _Partner = new Partner();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] 
                {
                    DB_UTILITY.CreateParameter("@iPlanCode", DbType.Int32, ParameterDirection.Input, _PartnerData.PlanCode),
                    DB_UTILITY.CreateParameter("@iPartnerCode", DbType.Int32, ParameterDirection.Input,_PartnerData.PartnerCode),
                };
                dtPartner = DB_UTILITY.RunSP("usp_PartnerPlanMapped", ArrParameter);
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
