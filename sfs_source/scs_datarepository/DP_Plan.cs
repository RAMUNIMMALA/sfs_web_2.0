using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using sfs_model;

namespace scs_datarepository
{
    public class DP_Plan
    {
        public Plan AddPlan(Plan _Plan)
        {
            DataTable dtPlan = null;
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iTitle", DbType.String, ParameterDirection.Input, _Plan.Title),
                    DB_UTILITY.CreateParameter("@iDetails", DbType.String, ParameterDirection.Input,_Plan.Details),                
                    DB_UTILITY.CreateParameter("@iStartDate", DbType.DateTime, ParameterDirection.Input,_Plan.StartDate),
                    DB_UTILITY.CreateParameter("@iEndDate", DbType.DateTime, ParameterDirection.Input,_Plan.EndDate),
                    DB_UTILITY.CreateParameter("@iNoofMonths", DbType.Int32, ParameterDirection.Input, _Plan.NoofMonths),
                    DB_UTILITY.CreateParameter("@iNoofPartners", DbType.Int32, ParameterDirection.Input,_Plan.NoofPartners),                
                    DB_UTILITY.CreateParameter("@iType", DbType.Int32, ParameterDirection.Input,_Plan.PlanType),
                    DB_UTILITY.CreateParameter("@iCommission", DbType.Int32, ParameterDirection.Input,_Plan.Commission),
                    DB_UTILITY.CreateParameter("@iPlanValue", DbType.Int32, ParameterDirection.Input, _Plan.PlanValue),
                    DB_UTILITY.CreateParameter("@iMonthlyAmount", DbType.Int32, ParameterDirection.Input,_Plan.MonthlyAmount),                
                    DB_UTILITY.CreateParameter("@iAuctionBaseAmount", DbType.Int32, ParameterDirection.Input,_Plan.AuctionBaseAmount),
                
                };
                dtPlan = DB_UTILITY.RunSP("usp_insertPlan", ArrParameter);
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

        public Plan FetchPlanDetails(int PlanCode)
        {
            DataTable dtPlan = null;
            Plan _Plan = new Plan();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] 
                {
                    DB_UTILITY.CreateParameter("@iplancode", DbType.Int32, ParameterDirection.Input, PlanCode),
                };
                dtPlan = DB_UTILITY.RunSP("usp_fetchPlanDetails", ArrParameter);
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

        public List<Plan> TotalPlans()
        {
            DataTable dtPlan = null;
            List<Plan> lstPlan = new List<Plan>(); 
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                };
                dtPlan = DB_UTILITY.RunSP("usp_fetchPlanList", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    lstPlan = OBJECT_UTILITY.GetConvertCollection<Plan>(dtPlan);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlan;
        }

        public List<Plan> FetchPartnerPlanList(int PartnerCode)
        {
            DataTable dtPlan = null;
            List<Plan> lstPlan = new List<Plan>();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                        DB_UTILITY.CreateParameter("@iPartnerCode", DbType.Int16, ParameterDirection.Input, PartnerCode),
                };
                dtPlan = DB_UTILITY.RunSP("usp_fetchPartnerPlanList", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    lstPlan = OBJECT_UTILITY.GetConvertCollection<Plan>(dtPlan);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlan;
        }

        //Name Nallapati sivakoteswarao
        //Description : to get the planrtitle and plancode
        public List<Plan> GetPlanBasicList()
        {
            DataTable dtPlan = null;
            List<Plan> lstPlan = new List<Plan>();
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[] { };
                dtPlan = DB_UTILITY.RunSP("usp_fetchPlanBasicList", ArrParameter);
                if (dtPlan != null && dtPlan.Rows.Count > 0)
                {
                    lstPlan = OBJECT_UTILITY.GetConvertCollection<Plan>(dtPlan);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlan;
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
