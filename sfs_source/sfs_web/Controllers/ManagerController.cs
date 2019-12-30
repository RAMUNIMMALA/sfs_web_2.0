using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sfs_model;
using scs_datarepository;

namespace sfs_web.Controllers
{
    public class ManagerController : SFSBaseController
    {
        DP_Manager _DP_Manager = null;
        DP_Partner _DP_Partner = null;
        Partner _Partner = null;
        TotalCount _TotalCount = null; 
        
        public ActionResult Index()
        {
            _DP_Manager = new DP_Manager();
            _TotalCount = new TotalCount();
            bool isSuccess = false;
            try
            {
                _TotalCount = _DP_Manager.GetTotalCounts();
                if(_TotalCount != null) // if fetching success 
                {
                    isSuccess = true;
                }
                else // if fail
                {
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Manager/Index", "SFS");
            }
            if(isSuccess)
            {
                return View(_TotalCount);
            }
            else
            {
                return View(_TotalCount);
            }
        }

        //public JsonResult Registration(Partner _PartnerData)
        //{
        //    List<Partner> lstPartner = new List<Partner>();
        //    _Partner = new Partner();
        //    try
        //    {
        //        _Partner = _DP_Manager.RegisterUser(_PartnerData);
        //        if (_Partner.Result == "Registered Successfully")
        //        {
        //            MailSending(_Partner);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Manager/Registration", "SFS");
        //    }
        //    return Json(_Partner);
        //}

        public ActionResult Registration(Partner _PartnerData)
        {
            _DP_Manager = new DP_Manager();
            _DP_Partner = new DP_Partner();
            _Partner = new Partner();
            List<Partner> lstPartner = new List<Partner>();
            bool isSuccess = false;
            _PartnerData.PartnerType = 2;
            _PartnerData.StatusType = 1;
            try
            {
                _Partner = _DP_Manager.RegisterUser(_PartnerData);
                if (_Partner.Result == 1) // if registration success
                {
                    MailSending(_Partner);
                    lstPartner = _DP_Partner.TotalPartners();
                    ViewBag.SuccessMessage = "Partner registered successfully";
                    isSuccess = true;
                }
                else if(_Partner.Result == 2)// if contact number exists
                {
                    ViewBag.AlertMessage = "Contact number already exists";
                }
                else // if registration fail
                {
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Manager/Registration", "SFS");
            }
            if (isSuccess)
            {
                return View("../Partner/PartnerList", lstPartner);
            }
            else
            {
                _Partner.PType = GetPartnerType();
                _Partner.SType = GetStatusType();
                _Partner.GType = GetGenderType();
                return View("../Partner/NewPartner", _Partner);
            }
        }

    }
}
