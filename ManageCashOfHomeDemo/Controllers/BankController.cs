using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using myApp.Models;
using myApp.Models.Reopsitory;
using System.Net.Http;

namespace ManageCashOfHomeDemo.Controllers
{
    public class BankController : Controller
    {

        HttpClient client = new HttpClient();

        BankRepository repository = null;

        public BankController()
        {
            repository = new BankRepository();
        }

        public ActionResult FirstPage()
        {
            return View();
        }

        public ActionResult AddBranch()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddBranch(BranchModel model)
        {
            BranchModel data = repository.addBranch(model);

            if (data != null)
            {
                ModelState.Clear();
                ViewBag.isSuceess = "Branch Added Successfully....";
            }
            return RedirectToAction("BranchList");
        }

        public ActionResult AddAccount()
        {
            AccountModel model = new AccountModel();

            model.UserList = repository.UserNameDropDown();
            model.BranchList = repository.BranchNameDropDown();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddAccount(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                AccountModel data = repository.addAccount(model);

                if (data != null)
                {
                    ModelState.Clear();
                    ViewBag.success = "User Account Is Added.....";
                }
            }
            return RedirectToAction("AccountList");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                //int id = repository.RegisterNewUser(model);
                //if (id > 0)

                client.BaseAddress = new Uri("https://localhost:44324/api/BankApi/insertImage/");
                var responce = client.PostAsJsonAsync<UserModel>("BankApi", model);
                responce.Wait();

                var test = responce.Result;

                if (test.IsSuccessStatusCode)
                {
                    ModelState.Clear();
                    ViewBag.isSuceess = "User Successfully Registered.....!!";
                }

                UserModel data = repository.RegisterNewUser(model);

                if (data != null)
                {
                    ModelState.Clear();
                    ViewBag.isSuceess = "User Successfully Registered.....!!";
                }

            }
            return View();
        }

        public ActionResult Login()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {

            UserModel data = repository.UserLogin(model.email, model.password);

            if (ModelState.IsValid)
            {
                if (data.id != 0)
                {
                    FormsAuthentication.SetAuthCookie(model.email, false);
                    return RedirectToAction("FirstPage");
                }
                else
                {
                    ViewBag.isFailure = "Login Failed.....!!";
                    //ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        //public ActionResult BranchList()
        //{
        //    List<BranchModel> result = repository.BranchDetailsList();
        //    return Json(result);
        //}
        [HttpGet]
        public ActionResult BranchList()
        {
            //List<BranchModel> result = repository.BranchDetailsList();
            //return Json(result);
            return View();
        }

        [HttpPost]
        public JsonResult BranchList(BranchModel model)
        {
            var result = repository.BranchDetailsList();
            return Json(result);
        }

        public ActionResult EditBranchDetails(int id)
        {
            BranchModel objBranchModel = repository.getBranchById(id);
            return View(objBranchModel);
        }

        [HttpPost]
        public ActionResult EditBranchDetails(BranchModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateBranchDetails(model.branchid, model);
                return RedirectToAction("BranchList");
            }
            return View();
        }

        public ActionResult DeleteBranch(int id)
        {
            repository.deleteBranch(id);
            return RedirectToAction("BranchList");
        }
        
        public ActionResult AccountList()
        {
            List<AccountModel> list = repository.accountDetailsList();
            return View(list);
        }

        public ActionResult EditAccountDetails(int id)
        {
            AccountModel objAccountModel = repository.getAccountById(id);
            return View(objAccountModel);
        }

        [HttpPost]
        public ActionResult EditAccountDetails(AccountModel model)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateAccountDetails(model.id, model);
                return RedirectToAction("AccountList");
            }
            return View();
        }

        public ActionResult DeleteAccount(int id)
        {
            repository.deleteAccount(id);
            return RedirectToAction("AccountList");
        }
    }
}