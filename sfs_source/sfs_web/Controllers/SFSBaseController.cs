using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Globalization;
using System.IO;
using sfs_model;
using scs_datarepository;

namespace sfs_web.Controllers
{

    public class SFSBaseController : Controller
    {
        DP_Partner _DP_Partner = null;
        DP_Plan _DP_Plan = null;
        Plan _Plan = null;
        List<Plan> lstPlan = null;
        public const string ErrorMessage = "Unable to process, contact IT department";
        public const string NewTransaction = "Records created Successfully";
        public const string UpdatedMessage = "Records Updated Successfully";
        public const string TransFailed = "Transaction Failed";
        public const string EmptyRecordSet = "No Record Found";
        protected string strnavgationurl = string.Empty;
        
        //getting log file path
        //public static string GetLogPath()
        //{
        //    return HttpContext.Current.Server.MapPath(Convert.ToString(ConfigurationManager.AppSettings["logpath"]));
        //}
           
        protected string ConvertToDateString(string strDate)
        {
            DateTime date = DateTime.ParseExact(strDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return date.ToString("yyyy/MM/dd");
        }

        public List<PartnerType> GetPartnerType()
        {
            List<PartnerType> lstPartnerType = new List<PartnerType>();
            try
            {
                string strPartnerType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PartnerType"]);
                //string[] arrPartnerType = strPartnerType.Split(',');  //1-Manager   2-Partner
                foreach (string strEachType in strPartnerType.Split(','))
                {
                    PartnerType _PartnerType = new PartnerType();
                    _PartnerType.Id = Convert.ToInt16(strEachType.Split('-')[0]);
                    _PartnerType.Title = strEachType.Split('-')[1];
                    lstPartnerType.Add(_PartnerType);
                }
            }
            catch(Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/GetPartnerType", "SFS");
            }
            return lstPartnerType;
        }

        public List<PlanType> GetPlanType()
        {
            List<PlanType> lstPlanType = new List<PlanType>();
            try
            {
                string strPlanType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PlanType"]);
                foreach (string strEachType in strPlanType.Split(','))
                {
                    PlanType _PlanType = new PlanType();
                    _PlanType.Id = Convert.ToInt16(strEachType.Split('-')[0]);
                    _PlanType.Title = strEachType.Split('-')[1];
                    lstPlanType.Add(_PlanType);
                }
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/GetPlanType", "SFS");
            }
            return lstPlanType;
        }

        public List<PaymentType> GetPaymentType()
        {
            List<PaymentType> lstPaymentType = new List<PaymentType>();
            try
            {
                string strPaymentType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["PaymentType"]);
                foreach (string strEachType in strPaymentType.Split(','))
                {
                    PaymentType _PaymentType = new PaymentType();
                    _PaymentType.Id = Convert.ToInt16(strEachType.Split('-')[0]);
                    _PaymentType.Title = strEachType.Split('-')[1];
                    lstPaymentType.Add(_PaymentType);
                }
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/GetPlanType", "SFS");
            }
            return lstPaymentType;
        }

        public List<GenderType> GetGenderType()
        {
            List<GenderType> lstGenderType = new List<GenderType>();
            try
            {
                string strGenderType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["GenderType"]);
                foreach (string strEachType in strGenderType.Split(','))
                {
                    GenderType _GenderType = new GenderType();
                    _GenderType.Id = strEachType.Split('-')[0];
                    _GenderType.Title = strEachType.Split('-')[1];
                    lstGenderType.Add(_GenderType);
                }
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/GetGenderType", "SFS");
            }
            return lstGenderType;
        }

        public List<StatusType> GetStatusType()
        {
            List<StatusType> lstStatusType = new List<StatusType>();
            try
            {
                string strStatusType = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["StatusType"]);
                foreach (string strEachType in strStatusType.Split(','))
                {
                    StatusType _StatusType = new StatusType();
                    _StatusType.Id = Convert.ToInt16(strEachType.Split('-')[0]);
                    _StatusType.Title = strEachType.Split('-')[1];
                    lstStatusType.Add(_StatusType);
                }
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/GetStatusType", "SFS");
            }
            return lstStatusType;
        }

        public List<PartnerData> GetPartnerData()
        {
            List<PartnerData> lstPartnerdata = new List<PartnerData>();
            PartnerData _Partnerdata = new PartnerData();
            try
            {
                _Partnerdata.PartnerCode = (int)Session["PartnerCode"];
                _Partnerdata.PartnerName = (string)Session["PartnerName"];
                lstPartnerdata.Add(_Partnerdata);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPartnerdata;
        }
        
        public List<Plan> GetPlanData()
        {
            lstPlan = new List<Plan>();
            _Plan = new Plan();
            try
            {
                _Plan.PlanCode = (int)Session["PlanCode"];
                _Plan.Title = (string)Session["Title"];
                lstPlan.Add(_Plan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlan;
        }
        
        public List<Plan> GetPlanBasicList()
        {

            lstPlan = new List<Plan>();
            _DP_Plan = new DP_Plan();
            try
            {
                lstPlan = _DP_Plan.GetPlanBasicList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPlan;
        }
        public List<PartnerType> GetPartnerBasicList()
        {
            List<PartnerType> lstPartnertype = new List<PartnerType>();
            _DP_Partner = new DP_Partner();
            try
            {
                lstPartnertype = _DP_Partner.GetPartnerBasicList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstPartnertype;
        }


        public void MailSending(Partner _Partner)
        {
            string message;
            DP_User _DP_User = new DP_User();
            try
            {
                string strMailId = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["MailId"]);
                string strPassword = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Password"]);
                message = "Hello, " + _Partner.FirstName.ToUpper() + Environment.NewLine + "You were Regitered into SMART FINANCE SOLUTIONS Successfully" + Environment.NewLine + "Your" + Environment.NewLine + "Partner Id: " + _Partner.PartnerId + Environment.NewLine + "Password:" + _Partner.Password;
                _DP_User.SendMail(strMailId, strPassword, _Partner.MailId, "Confirmation Message", message, 'T');
            }
            catch(Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "SFSBaseController/MailSending", "SFS");
            }
        }
    }


    #region Exception log
    public class EXCEPTION_UTILITY
    {
        /// <summary>
        /// if file size exceeds 30 mb file content will be cleared
        /// </summary>
        /// <param name="strLogPath"></param>
        private static void FileExceedSize(string strLogPath)
        {
            FileInfo _file = new FileInfo(strLogPath);
            if (_file.Length / 1048576 > 30)
            {
                //File.WriteAllText(strLogPath, String.Empty);
            }
        }

        private static bool IfThreadAbort(Exception ex)
        {
            if (ex.Message == "Thread was being aborted.")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Writing exception to log file  along with user id including form name in which the exception occured
        /// </summary>
        /// <param name="ex">exception</param>
        /// <param name="LogFilePath">File path</param>
        /// <param name="UserID">User ID</param>
        /// <param name="FormName">Form Name</param>
        /// <param name="ProjectName">Project Name</param>
        /// <param name="MethodName">Method Name</param>
        public static void WriteToLog(Exception ex, string UserID, string IterationPath, string ProjectName)
        {
            string strFilePath = Convert.ToString(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["ExceptionLogPath"]));
            EXCEPTION_UTILITY.FileExceedSize(strFilePath);
            if (IfThreadAbort(ex))
            {
                StreamWriter _sw = new StreamWriter(strFilePath, true);
                Random rand = new Random();
                string ID = Convert.ToString(rand.Next()) + "-" + string.Format("{0:ddMMyyyyHHmmss}", System.DateTime.Now);
                _sw.WriteLine("Id: {0}", ID);
                _sw.WriteLine("UserID: {0}", UserID);
                _sw.WriteLine("ControllerName: {0}", IterationPath.Split('/')[0]);
                _sw.WriteLine("Action Name: {0}", IterationPath.Split('/')[1]);
                _sw.WriteLine("project Name: {0}", ProjectName);
                _sw.WriteLine("Type: {0}", ex.GetType());
                _sw.WriteLine("Source: {0}", ex.Source);
                _sw.WriteLine("Message: {0}", ex.Message);
                _sw.WriteLine("------------------------------------------------------------------------------------");
                _sw.WriteLine();
                _sw.Close();
            }
        }
    }
    #endregion
}
