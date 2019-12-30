using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using scs_datarepository;
using sfs_model;

namespace sfs_web.Controllers
{
    public class HomeController : SFSBaseController
    {
       DP_User _DP_User = null;
       DP_Manager _DP_Manager = null;
       Partner _Partner = null;
       TotalCount _TotalCount = null;
       public ActionResult Index()
       {
           return View(_Partner);
       }

       public ActionResult CheckUserLogin(Partner _PartnerData)
       {
           _DP_User = new DP_User();
           _DP_Manager = new DP_Manager();
           _Partner = new Partner();
           _TotalCount = new TotalCount();
           bool isSuccess = false;
           try
           {
               _Partner = _DP_User.CheckUserInfo(_PartnerData.PartnerId, _PartnerData.Password);
               _TotalCount = _DP_Manager.GetTotalCounts();
               if (_Partner.FirstName != null)
               {
                   Session["Userdata"] = _Partner;
                   Session["UserId"] = _Partner.PartnerId;
                   Session["LogStatus"] = "Logout";
                   isSuccess = true;
                   ViewBag.Login = null;
                   switch (_Partner.RoleCode) // if login success 
                   {
                       case 1: strnavgationurl = "../Manager/Index";
                           break;
                       case 2: strnavgationurl = "../Partner/Index";
                           break;
                       default: strnavgationurl = "Index";
                           break;
                   }
               }
               else // if login fail
               {
                   ViewBag.Login = "Invalid Credentials";
               }
           }
           catch (Exception ex)
           {
               ViewBag.AlertMessage = ErrorMessage;
               EXCEPTION_UTILITY.WriteToLog(ex, Convert.ToString(Session["UserId"]), "Home/CheckUserLogin", "SFS");
           }
           if (isSuccess)
           {
               return View(strnavgationurl, _TotalCount);
           }
           else
           {
               return View("Index", _Partner);
           }
       }
       
       public ActionResult About()
       {
           return View();
       }

       public ActionResult Contact()
       {
           return View();
       }
       
       public ActionResult Error()
       {
           return View();
       }
    
    }
}


