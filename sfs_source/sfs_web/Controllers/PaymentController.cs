using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sfs_model;
using scs_datarepository;

namespace sfs_web.Controllers
{
    public class PaymentController : SFSBaseController
    {
        //
        // GET: /Payment/
        Payment _Payment = null;
        List<Payment> lstPayment = null;
        DP_Payment _DP_Payment = new DP_Payment();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewPayment(int MappingId)
        {
            Session["MappingId"] = MappingId;
            _Payment = new Payment();
            try
            {
                _Payment.PType = GetPaymentType();
                _Payment.PlanMemberMappingId = MappingId;
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Payment/NewPayment", "SFS");
            }
            
            return View(_Payment);
        }

        public ActionResult AddNewPayment(Payment _PaymentData)
        {
            _Payment = new Payment();
            _PaymentData.PlanMemberMappingId = (int)Session["MappingId"];
            lstPayment = new List<Payment>();
            bool isSuccess = false;
            try
            {
                _Payment = _DP_Payment.AddPayment(_PaymentData);
                if(_Payment.Result == 1)
                {
                    lstPayment = _DP_Payment.TotalPayments();
                    isSuccess = true;
                    ViewBag.SuccessMessage = "Payment added successfully";
                }
                else
                {
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Payment/AddNewPayment", "SFS");
            }
            if(isSuccess)
            {
                return View("PaymentList", lstPayment);
            }
            else
            {
                _Payment.PType = GetPaymentType();
                _Payment.PlanMemberMappingId = (int)Session["MappingId"];
                return View("NewPayment", _Payment);
            }
        }
        
        public ActionResult PaymentList()
        {
            lstPayment = new List<Payment>();
            try
            {
                lstPayment = _DP_Payment.TotalPayments();
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Payment/AddNewPayment", "SFS");
            }
            return View(lstPayment);
        }

        


    }
}
