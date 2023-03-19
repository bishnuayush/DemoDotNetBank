using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using myApp.Models.Reopsitory;
using myApp.Models;
using System.Web.Security;
using System.Net.Http.Formatting;
using ManageCashOfHomeDemo.Models;
using System.IO;
using System.Web;
using BankApp.Db;

namespace ManageCashOfHomeDemo.APIs.Restful
{
    public class BankApiController : ApiController
    {
        BankRepository repository = null;

        public BankApiController()
        {
            repository = new BankRepository();
        }

        [HttpGet]
        public IHttpActionResult apiTest()
        {
            return Ok("api working");
        }

        [HttpPost]
        public HttpResponseMessage ImgUpload_register()
        {
            string ImageName = null;
            var httpRequest = HttpContext.Current.Request;

            var postedFile = httpRequest.Files["Image"];

            if(postedFile != null)
            {
                ImageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                ImageName = ImageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                //var filePath = HttpContext.Current.Server.MapPath("~/Image/" + ImageName);
                var filePath = HttpContext.Current.Server.MapPath("~/Image/" + ImageName);
                postedFile.SaveAs(filePath);
            }


            UserModel model = new UserModel()
            {
                fName = httpRequest["firstName"],
                lName = httpRequest["lastName"],
                mobileNo = httpRequest["mobile"],
                address = httpRequest["address"],
                email = httpRequest["email"],
                password = httpRequest["password"],
                
            };
            if(postedFile != null)
            {
                model.image = "image/" + ImageName;
            }

            ResponseModel response = new ResponseModel();

            repository.RegisterNewUser(model);

            if (model.id != 0)
            {
                response.Status = "success";
                response.Message = "User Successfully Registered";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                response.Status = "fail";
                response.Message = "User Registraton Failed";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            //return Request.CreateResponse(HttpStatusCode.Created);
        }

        //[HttpPost]
        //public IHttpActionResult RegisterUser(UserModel model)
        //{

        //    string createFolder = HttpContext.Current.Server.MapPath("~/myFiles");
        //    if (!Directory.Exists(createFolder))
        //    {
        //        Directory.CreateDirectory(createFolder);
        //    }//created the images folder dynamically.


        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        var uploadfile = HttpContext.Current.Request.Files["Filesave"];
        //        if (uploadfile != null)
        //        {
        //            var saveimage = Path.Combine(HttpContext.Current.Server.MapPath("~/myFiles/"), uploadfile.FileName);
        //            uploadfile.SaveAs(saveimage);

        //            model.image = saveimage;
        //            //FileDemoEntities db = new FileDemoEntities();
        //            //db.insertimgdetails(uploadfile.FileName, savepdf);
        //            //db.SaveChanges();
        //            repository.RegisterNewUser(model);

        //        }
        //    }

        //    //repository.RegisterNewUser(model);

        //    return Ok("success");
        //}

        [HttpGet]
        public IHttpActionResult getUserDataById(int id)
        {
            var response = repository.getUserById(id);
            return Ok(response);
        }


        [HttpPost]
        public IHttpActionResult LoginUser(LoginModel model)
        {
            ResponseModel response = new ResponseModel();

            try
            {
                if (model.email == "" && model.password == "")
                {
                    //Dictionary<string, object> success = new Dictionary<string, object>() { { "Message", "empty" } };
                    response.Message = "you need to fill both username and password";
                    response.Status = "empty";
                    return Ok(response);
                }

                UserModel data = repository.UserLogin(model.email, model.password);
                if (data.id != 0)
                {
                    //response.Message = "empty";
                    response.Status = "success";
                    response.Data = data;

                    return Ok(response);
                }
                else
                {
                    response.Message = "Wrong UserName & Password";
                    response.Status = "fail";
                    return Ok(response);
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(internalServerError: (int)HttpStatusCode.InternalServerError, message: ex.Message);
            }
        }

        private IHttpActionResult StatusCode(int internalServerError, string message)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IHttpActionResult BranchListApi()
        {
            List<BranchModel> result = repository.BranchDetailsList();
            //return db.Candidates.OrderByDescending(x => x.Id).ToList();
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult AddBranchApi(BranchModel model)
        {
            ResponseModel response = new ResponseModel();
            if (model.branchNum == null && model.branchNum == null)
            {
                //Dictionary<string, object> success = new Dictionary<string, object>() { { "Message", "empty" } };
                response.Message = "you need to fill both Branch Number and Name";
                response.Status = "empty";
                return Ok(response);
            }

            BranchModel data = repository.addBranch(model);

            //BranchModel data =
            if (data.branchid != 0)
            {
                response.Message = "Branch Added Successfully";
                response.Status = "success";
                response.Data = data;
                return Ok(response);
            }
            return Ok("fail");
        }

        [HttpPut]
        public IHttpActionResult BranchUpdateApi(BranchModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.UpdateBranchDetails(model.branchid, model);

            response.Message = "Branch Update Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }

        [HttpPost]
        public IHttpActionResult delBranchApi(BranchModel model)
        {
            ResponseModel response = new ResponseModel();

            repository.deleteBranch(model.branchid);

            response.Message = "Branch Deleted Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult AccountListApi()
        {
            List<AccountModel> result = repository.accountDetailsList();
            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult UserNameDropDownApi()
        {
            List<UserModel> result = repository.UserNameDropDown();
            return Ok(result);

        }

        [HttpPost]
        public IHttpActionResult AddAccountApi(AccountModel model)
        {
            ResponseModel response = new ResponseModel();
            //if (model.accountNumber == null && model.minBalance == null && model.withdrawLimit == null && model.balance == null && model.userId == null && model.branchId == null)
            //{
            //    //Dictionary<string, object> success = new Dictionary<string, object>() { { "Message", "empty" } };
            //    response.Message = "you need to fill all the fields";
            //    response.Status = "empty";
            //    return Ok(response);
            //}

            if (model.accountNumber == "" || model.accountNumber == null && model.minBalance == null && model.withdrawLimit == null)
            {
                response.Message = "you need to fill the fields";
                response.Status = "empty";
                return Ok(response);
            }

            AccountModel data = repository.addAccount(model);

            //BranchModel data =
            if (data.id != 0)
            {
                response.Message = "Account Added Successfully";
                response.Status = "success";
                response.Data = data;
                return Ok(response);
            }
            return Ok("fail");
        }

        [HttpPut]
        public IHttpActionResult AccountUpdateApi(AccountModel model)
        {
            ResponseModel response = new ResponseModel();
            repository.UpdateAccountDetails(model.id, model);

            response.Message = "Account Update Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }

        //[HttpPut]
        //public IHttpActionResult UpdateUserImage(int id, string image)
        //{
        //    ResponseModel response = new ResponseModel();

        //    var data = repository.UpdateUserImage(id, image);
        //    if(data == true)
        //    {
        //        response.Message = "Image Update Successfully";
        //        response.Status = "success";
        //        return Ok(response);
        //    }
        //    response.Status = "fail";
        //    return Ok(response);
        //}

        [HttpPost]
        public IHttpActionResult delAccountApi(AccountModel model)
        {
           
            ResponseModel response = new ResponseModel();

            repository.deleteAccount(model.id);

            response.Message = "Account Deleted Successfully";
            response.Status = "success";
            response.Data = model;

            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult TransactionListApi()
        {
            List<TransactionModel> result = repository.transactionDetailsList();
            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult TransactionApi(TransactionModel model)
        {
            ResponseModel response = new ResponseModel();

            if (ModelState.IsValid)
            {
                model.CashTransaction.Count = repository.CashNoteCount(
                   model.CashTransaction.C100,
                   model.CashTransaction.C200,
                   model.CashTransaction.C500,
                   model.CashTransaction.C2000
                   );

                bool compareAmount = repository.CashNoteCount(
                    model.CashTransaction.C100,
                    model.CashTransaction.C200,
                    model.CashTransaction.C500,
                    model.CashTransaction.C2000,
                    model.amount.Value
                    );

                if (compareAmount == true)
                {
                    model.Account = repository.getAccountById(model.accountId.Value);

                    if (model.amount >= model.Account.withdrawLimit)
                    {
                        response.Status = "TooMuch";
                        response.Message = "You Can Not Withdrawl This Much Money";
                        return Ok(response);
                    }

                    if (model.Account.balance < model.Account.minBalance)
                    {
                        response.Status = "lowBal";
                        response.Message = "Warning.. Your Account Balnance is Very Less";
                        return Ok(response);
                        
                    }

                    TransactionModel data = repository.transaction(model);
                    response.Message = "Transaction Successful";
                    response.Status = "success";
                    response.Data = model;
                    return Ok(response);
                }
                else
                {
                    response.Status = "NoMatch";
                    response.Message = "Your Amount Does Not Matches With The Note Count";
                    return Ok(response);
                }
            }
            response.Status = "failed";
            response.Message = "faied";
            //response.Data = model;
            return Ok(response);
        }

        //[HttpPost]
        //public IHttpActionResult insertImage(UserModel model)
        //{
        //    string createFolder = HttpContext.Current.Server.MapPath("~/myFiles");
        //    if (!Directory.Exists(createFolder))
        //    {
        //        Directory.CreateDirectory(createFolder);
        //    }//created the images folder dynamically.


        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        var uploadfile = HttpContext.Current.Request.Files["Filesave"];
        //        if (uploadfile != null)
        //        {
        //            var saveimage = Path.Combine(HttpContext.Current.Server.MapPath("~/myFiles/"), uploadfile.FileName);
        //            uploadfile.SaveAs(saveimage);

        //            //FileDemoEntities db = new FileDemoEntities();
        //            //db.insertimgdetails(uploadfile.FileName, savepdf);
        //            //db.SaveChanges();
        //            repository.RegisterNewUser(model);

        //        }
        //    }
        //    return Ok();
        //}


        [HttpPost]
        public HttpResponseMessage ImgUpdate()
        {
            string ImageName = null;
            var httpRequest = HttpContext.Current.Request;

            var postedFile = httpRequest.Files["Image"];

            ImageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            ImageName = ImageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
            var filePath = HttpContext.Current.Server.MapPath("~/Image/" + ImageName);
            //var filePath = HttpContext.Current.Server.MapPath("~/Image/" + ImageName);
            postedFile.SaveAs(filePath);

            ResponseModel response = new ResponseModel();

            UserModel model = new UserModel()
            {
                id = Convert.ToInt32(httpRequest["id"]),
                image = "image/" + ImageName
            };

            string data = repository.updateImage(model);

            if(data == "success")
            {
                response.Status = "success";
                response.Message = "Image Updated Successfully";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            else
            {
                response.Status = "fail";
                response.Message = "something went wrong please try after some time";
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
    }
}