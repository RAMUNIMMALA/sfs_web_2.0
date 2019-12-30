using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sfs_model;
using scs_datarepository;

namespace sfs_web.Controllers
{
    public class PartnerController : SFSBaseController
    {
        DP_Partner _DP_Partner = null;
        DP_Plan _DP_Plan = null;
        DP_Manager _DP_Manager = null;
        Partner _Partner = null;
        Plan _Plan = null;
        TotalCount _TotalCount = null;
        List<Partner> lstPartner = null;
        PartnerPlanMapping _PartnerPlanMapping = null;

        public ActionResult Index()
        {
            _DP_Manager = new DP_Manager();
            _TotalCount = new TotalCount();
            bool isSuccess = false;
            try
            {
                _TotalCount = _DP_Manager.GetTotalCounts();
                if (_TotalCount != null) // if fetching success 
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
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/Index", "SFS");
            }
            if (isSuccess)
            {
                return View(_TotalCount);
            }
            else
            {
                return View(_TotalCount);
            }
        }

        public ActionResult NewPartner()
        {
            _Partner = new Partner();
            try
            {
                _Partner.PType = GetPartnerType();
                _Partner.SType = GetStatusType();
                _Partner.GType = GetGenderType();
            }
            catch(Exception ex)
            {
                ViewBag.AlertMessage = "Something went wrong";
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/AddPartner", "SFS");
            }
            return View(_Partner);
        }

        public ActionResult ResetPassword()
        {
            _Partner = new Partner();
            return View(_Partner);
        }

        public ActionResult ChangePassword(Partner _PartnerData)
        {
            _DP_Partner = new DP_Partner();
            _DP_Manager = new DP_Manager();
            _Partner = new Partner();
            _TotalCount = new TotalCount();
            bool isSuccess = false;
            try
            {
                if (_PartnerData.Password == _PartnerData.ConfirmPassword) // if passwords are matching
                {
                    _Partner = _DP_Partner.ChangeCurrentPassword(Convert.ToString(Session["UserId"]), _PartnerData.Password);
                    if (_Partner.Result == 1) // if password changed successfully
                    {
                        ViewBag.SuccessMessage = "Password changed successfully";
                        _TotalCount = _DP_Manager.GetTotalCounts();
                        isSuccess = true;
                    }
                    else // if fail
                    {
                        ViewBag.AlertMessage = "Something went wrong";
                    }
                }
                else // if passwords are not matching
                {
                    ViewBag.AlertMessage = "Passwords are not matching";
                }

            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PartnerData", "SFS");
            }
            if (isSuccess)
            {
                return View("../Manager/Index", _TotalCount);
            }
            else
            {
                return View("ResetPassword", _Partner);
            }
        }

        public ActionResult GetProfile(int PartnerCode)
        {
            _DP_Partner = new DP_Partner();
            _Partner = new Partner();
            lstPartner = new List<Partner>();
            bool isSuccess = false;
            Session["UpdateCode"] = PartnerCode;
            try
            {
                _Partner = _DP_Partner.FetchPartnerDetails(PartnerCode);
                if (_Partner.FirstName != null) // if profile fetched successfully
                {
                    _Partner.PType = GetPartnerType();
                    _Partner.SType = GetStatusType();
                    _Partner.GType = GetGenderType();
                    isSuccess = true;
                }
                else // if fail
                {
                    lstPartner = _DP_Partner.TotalPartners();
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PartnerData", "SFS");
            }
            if (isSuccess)
            {
                return View("UpdateProfile", _Partner);
            }
            else
            {
                return View("PartnerList", lstPartner);
            }
        }
        public ActionResult UpdateProfile(Partner _PartnerData)
        {
            _DP_Partner = new DP_Partner();
            _Partner = new Partner();
            lstPartner = new List<Partner>();
            _PartnerData.PartnerCode = (int)Session["UpdateCode"];
            bool isSuccess = false;
            try
            {
                _Partner = _DP_Partner.UpdatePartnerDetails(_PartnerData);
                if (_Partner.Result == 1) // if profile updated successfully
                {
                    ViewBag.SuccessMessage = "Profile updated successfully";
                    lstPartner = _DP_Partner.TotalPartners();
                    isSuccess = true;
                }
                else if (_Partner.Result == 2) // if contact number already exists
                {
                    ViewBag.AlertMessage = "Contact number already exists";
                }
                else // if fail
                {
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PartnerData", "SFS");
            }
            if (isSuccess)
            {
                return View("PartnerList", lstPartner);
            }
            else
            {
                _PartnerData.PType = GetPartnerType();
                _PartnerData.SType = GetStatusType();
                _PartnerData.GType = GetGenderType();
                return View(_PartnerData);
            }
        }

        public ActionResult GetPartnerPlanDetails(int PartnerCode)
        {
            _PartnerPlanMapping = new PartnerPlanMapping();
            _PartnerPlanMapping = GetPartnerData(PartnerCode);
            if (_PartnerPlanMapping.PartnerData != null)
            {
                return View("PartnerDetails", _PartnerPlanMapping);
            }
            else
            {
                return View("Error");
            }
        }
        
        public ActionResult PartnerList()
        {
            _DP_Partner = new DP_Partner();
            lstPartner = new List<Partner>();
            try
            {
                lstPartner = _DP_Partner.TotalPartners();
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PartnerData", "SFS");
            }
            return View(lstPartner);
        }

        public ActionResult PlanMapping(int PartnerCode, string PartnerName)
        {
            _Partner = new Partner();
            Session["PartnerCode"] = PartnerCode;
            Session["PartnerName"] = PartnerName;
            try
            {
                _Partner.PMapping = GetPlanBasicList();
                _Partner.PartnerData = GetPartnerData();
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PlanMapping", "SFS");
            }
            if (_Partner.PMapping != null)
            {
                return View("PlanMapping", _Partner);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult AddPartnerPlan(Plan _PlanData)
        {
            _DP_Plan = new DP_Plan();
            _DP_Partner = new DP_Partner();
            _Plan = new Plan();
            _PartnerPlanMapping = new PartnerPlanMapping();
            try
            {
                _Plan = _DP_Partner.AddMemberMapping(_PlanData);
                if (_Plan.Result == 1)
                {
                    lstPartner = new List<Partner>();
                    _PartnerPlanMapping = GetPartnerData(_PlanData.PartnerCode);
                    ViewBag.SuccessMessage = "Plan mapped to partner successfully";
                }
                else if (_Plan.Result == 2)
                {
                    _PartnerPlanMapping = GetPartnerData(_PlanData.PartnerCode);
                    ViewBag.SuccessMessage = "Plan has no limit to add partner";
                }
                else
                {
                    _PartnerPlanMapping = GetPartnerData(_PlanData.PartnerCode);
                    ViewBag.SuccessMessage = "Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "Unable to process, contact IT department";
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Plan/AddPlanMember", "SFS");
            }
            if (_PartnerPlanMapping.PartnerData != null)
            {
                return View("PartnerDetails", _PartnerPlanMapping);
            }
            else
            {
                return View("Error");
            }
        }

        public JsonResult AjaxPartnerPlanDetails(Partner _PartnerData)
        {
            Partner _partner = new Partner();
            _DP_Partner = new DP_Partner();
            try
            {
                _partner = _DP_Partner.GetPartnerPlanMapped(_PartnerData);
                if (_partner.Result == 1 || _partner.Result == 2)
                {
                    ViewBag.Message = "";
                }
                else
                {
                    ViewBag.Message = "Sometnig Went Wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Contact It Department";
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/AjaxPartnerPlanDetails", "SFS");
            }
            return Json(_partner);
        }

    #region Methods

        [NonAction]
        private PartnerPlanMapping GetPartnerData(int PartnerCode)
        {
            _DP_Partner = new DP_Partner();
            _DP_Plan = new DP_Plan();
            _PartnerPlanMapping = new PartnerPlanMapping();
            try
            {
                _PartnerPlanMapping.PlanList = _DP_Plan.FetchPartnerPlanList(PartnerCode);
                _PartnerPlanMapping.PartnerData = _DP_Partner.FetchPartnerDetails(PartnerCode);
                _PartnerPlanMapping.PartnerData.SType = GetStatusType();
            }
            catch(Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/GetPartnerData", "SFS");
            }
            return _PartnerPlanMapping;
        }

        #endregion
    }
}

