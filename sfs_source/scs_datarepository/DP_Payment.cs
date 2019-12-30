using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using sfs_model;

namespace scs_datarepository
{
    public class DP_Payment
    {
        Payment _Payment = null;
        List<Payment> lstPayment = null;
        public Payment AddPayment(Payment _PaymentData)
        {
            _Payment = new Payment();
            DataTable dtPayment = null;
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                    DB_UTILITY.CreateParameter("@iPlanMemberMappingId", DbType.Int32, ParameterDirection.Input,_PaymentData.PlanMemberMappingId),
                    DB_UTILITY.CreateParameter("@iTitle", DbType.String, ParameterDirection.Input,_PaymentData.Title),
                    DB_UTILITY.CreateParameter("@iAmount", DbType.Int32, ParameterDirection.Input,_PaymentData.Amount),
                    DB_UTILITY.CreateParameter("@iPaymentMode", DbType.Int32, ParameterDirection.Input,_PaymentData.PaymentType),
                    DB_UTILITY.CreateParameter("@iComments", DbType.String, ParameterDirection.Input,_PaymentData.Comments),
                    DB_UTILITY.CreateParameter("@iAttachment", DbType.String, ParameterDirection.Input,_PaymentData.Attachment),
                    DB_UTILITY.CreateParameter("@iPaidDate", DbType.Date, ParameterDirection.Input,_PaymentData.PaidDate),
                 };
                dtPayment = DB_UTILITY.RunSP("usp_insertPayment", ArrParameter);
                if (dtPayment != null && dtPayment.Rows.Count > 0)
                {
                    _Payment = OBJECT_UTILITY.GetConvert<Payment>(dtPayment);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return _Payment;
        }
        
        public List<Payment> TotalPayments()
        {
            lstPayment = new List<Payment>();
            DataTable dtPayments = null;
            try
            {
                IDbDataParameter[] ArrParameter = new IDbDataParameter[]
                {
                };
                dtPayments = DB_UTILITY.RunSP("usp_fetchPaymentList", ArrParameter);
                if (dtPayments != null && dtPayments.Rows.Count > 0)
                {
                    lstPayment = OBJECT_UTILITY.GetConvertCollection<Payment>(dtPayments);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return lstPayment;
        }

    }
}
