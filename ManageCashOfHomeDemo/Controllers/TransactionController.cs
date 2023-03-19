using myApp.Models;
using myApp.Models.Reopsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManageCashOfHomeDemo.Controllers
{
    public class TransactionController : Controller
    {
        BankRepository repository = null;

        public TransactionController()
        {
            repository = new BankRepository();
        }

        public ActionResult Index()
        {
            TransactionModel model = new TransactionModel();
            model.accountList = repository.accountNumberDropDown();

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(TransactionModel model)
        {

            if (ModelState.IsValid)
            {
                 model.CashTransaction.Count = CashNoteCount(
                    model.CashTransaction.C100,
                    model.CashTransaction.C200,
                    model.CashTransaction.C500,
                    model.CashTransaction.C2000
                    );

                bool compareAmount = CashNoteCount(
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
                        ViewBag.limitError = "You Can Not Withdrawl This Much Money";
                        model.accountList = repository.accountNumberDropDown();
                        return View(model);
                    }

                    if (model.Account.balance < model.Account.minBalance) {
                        ViewBag.minBalamceErr = "Warning.. Your Account Balnance is Very Less";
                        model.accountList = repository.accountNumberDropDown();
                        return View(model);
                    }

                    TransactionModel data = repository.transaction(model);
                    return RedirectToAction("TransactionList");
                }
                else
                {
                    ViewBag.message = "Your Amount Does Not Matches With The Note Count";
                    model.accountList = repository.accountNumberDropDown();
                }
            }

            return View(model);
        }


        [NonAction]
        public int CashNoteCount(int C100, int C200, int C500, int C2000)
        {
            return C100 + C200 + C500 + C2000;
        }   

        [NonAction]
        public bool CashNoteCount(int C100, int C200, int C500, int C2000,int amount)
        {
            int TotalAmount = ((C100 * 100) + (C200 * 200) + (C500 * 500) + (C2000 * 2000));
            if(TotalAmount == amount)
            {
                return true;
            }
            return false;
        }

        public ActionResult TransactionList()
        {

            List<TransactionModel> list = repository.transactionDetailsList();

            return View(list);

        }

    }
}