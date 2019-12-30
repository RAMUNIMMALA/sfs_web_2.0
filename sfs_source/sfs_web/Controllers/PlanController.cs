using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sfs_model;
using scs_datarepository;

namespace sfs_web.Controllers
{
    public class PlanController : SFSBaseController
    {
        DP_Plan _DP_Plan = null;
        DP_Partner _DP_Partner = null;
        Partner _Partner = null;
        Plan _Plan = null;
        List<Plan> lstPlan = null;
        PartnerPlanMapping _PartnerPlanMapping = null;
        
        public ActionResult PlanList()
        {
            _DP_Plan = new DP_Plan();
            lstPlan = new List<Plan>();
            try
            {
                lstPlan = _DP_Plan.TotalPlans();
            }
            catch(Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Plan/PlanList", "SFS");
            }
            return View(lstPlan);
        }

        public ActionResult NewPlan()
        {
            _Plan = new Plan();
            try
            {
                _Plan.PType = GetPlanType();
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Plan/NewPlan", "SFS");
            }
            return View(_Plan);
        }

        public ActionResult AddNewPlan(Plan _PlanData)
        {
            _DP_Plan = new DP_Plan();
            _Plan = new Plan();
            lstPlan = new List<Plan>();
            bool isSuccess = false;
            try
            {
                _Plan = _DP_Plan.AddPlan(_PlanData);
                if (_Plan.Result == 1) // if plan added
                {
                    lstPlan = _DP_Plan.TotalPlans();
                    isSuccess = true;
                    ViewBag.SuccessMessage = "Plan added successfully";
                }
                else if (_Plan.Result == 2) // if plan title already exists
                {
                    ViewBag.AlertMessage = "Plan title already exists";
                }
                else
                {
                    ViewBag.AlertMessage = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Plan/AddNewPlan", "SFS");
            }
            if(isSuccess)
            {
                return View("PlanList", lstPlan);
            }
            else
            {
                _Plan.PType = GetPlanType();
                return View("NewPlan", _Plan);
            }
        }

        public ActionResult GetPlanPartnerDetails(int PlanCode)
        {
            _PartnerPlanMapping = new PartnerPlanMapping();
            _PartnerPlanMapping = GetPlanData(PlanCode);
            if (_PartnerPlanMapping.PlanData != null)
            {
                return View("PlanDetails", _PartnerPlanMapping);
            }
            else
            {
                return View("Error");
            }
        }

        public ActionResult PartnerMapping(int PlanCode, string PlanTitle)
        {
            _Partner = new Partner();
            Session["PlanCode"] = PlanCode;
            Session["Title"] = PlanTitle;
            try
            {
                _Partner.PMapping = GetPlanData();
                _Partner.PType = GetPartnerBasicList();
            }
            catch (Exception ex)
            {
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/PlanMapping", "SFS");
            }
            if (_Partner.PType != null)
            {
                return View("PartnerMapping", _Partner);
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
                _Plan = _DP_Partner.MemberMapping(_PlanData);
                if (_Plan.Result == 1)
                {
                    _PartnerPlanMapping = GetPlanData(_PlanData.PlanCode);
                    ViewBag.SuccessMessage = "Partner mapped to plan successfully";
                }
                else
                {
                    _PartnerPlanMapping = GetPlanData(_PlanData.PlanCode);
                    ViewBag.SuccessMessage = "Something Went Wrong";
                }
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = "Unable to process, contact IT department";
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Plan/AddPlanMember", "SFS");
            }
            if (_PartnerPlanMapping.PlanData != null)
            {
                return View("PlanDetails", _PartnerPlanMapping);
            }
            else
            {
                return View("Error");
            }
        }



        #region Methods
        
        [NonAction]
        private PartnerPlanMapping GetPlanData(int PlanCode)
        {
            _DP_Partner = new DP_Partner();
            _DP_Plan = new DP_Plan();
            _PartnerPlanMapping = new PartnerPlanMapping();
            try
            {
                _PartnerPlanMapping.PartnerList = _DP_Partner.FetchPlanPartnerList(PlanCode);
                _PartnerPlanMapping.PlanData = _DP_Plan.FetchPlanDetails(PlanCode);
                _PartnerPlanMapping.PlanData.SType = GetStatusType();
            
            }
            catch (Exception ex)
            {
                ViewBag.AlertMessage = ErrorMessage;
                EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Partner/GetPartnerData", "SFS");
            }
            return _PartnerPlanMapping;
        }

        #endregion
        

    }
}
