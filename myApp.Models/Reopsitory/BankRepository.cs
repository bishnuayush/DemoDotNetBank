using BankApp.Db;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace myApp.Models.Reopsitory
{
    public class BankRepository
    {
        BankDBEntities1 database = new BankDBEntities1();

        public BranchModel addBranch(BranchModel model)
        {
            Branch branchTable = new Branch()
            {
                BanchNumber = model.branchNum,
                BranchName = model.branchName
            };

            database.Branches.Add(branchTable);
            database.SaveChanges();

            model.branchid = branchTable.Id;

            return model;
        }

        public UserModel RegisterNewUser(UserModel model)
        {
            using (var db = new BankDBEntities1())
            {
                User UserTable = new User();

                UserTable.FirstName = model.fName;
                UserTable.LastName = model.lName;
                UserTable.MobileNumber = model.mobileNo;
                UserTable.Address = model.address;
                UserTable.Email = model.email;
                UserTable.Password = model.password;

                UserTable.Image = model.image;

                db.Users.Add(UserTable);
                db.SaveChanges();

                model.id = UserTable.Id;

                return model;

            }
        }

        public UserModel UserLogin(string email, string password)
        {
            var objUser = database.Users.Where(x=> x.Email == email && x.Password == password).FirstOrDefault();

            UserModel objUserModel = new UserModel();

            if (objUser != null)
            {
                objUserModel.id = objUser.Id;
                objUserModel.email = objUser.Email;
                objUserModel.fName = objUser.FirstName;
                objUserModel.lName = objUser.LastName;
                objUserModel.mobileNo = objUser.MobileNumber;
                objUserModel.address = objUser.Address;
                objUserModel.image = objUser.Image;
            }
            
            return objUserModel;
            //throw new UnauthorizedAccessException("Invalid User Details");
            //return email;
        }

        public List<UserModel> UserNameDropDown()
        {
            List<UserModel> result = new List<UserModel>();

            var list = database.Users.Select(m => m).ToList();

            if (list != null && list.Count() > 0)
            {
                foreach (var data in list)
                {
                    UserModel model = new UserModel()
                    {
                        fName = data.FirstName,
                        id = data.Id,
                        address = data.Address,
                        email = data.Email,
                        lName = data.LastName,
                        mobileNo = data.MobileNumber,
                        
                    };
                    result.Add(model);
                }
            }
            return result;
        }

        public List<BranchModel> BranchNameDropDown()
        {
            List<BranchModel> result = new List<BranchModel>();

            var list = database.Branches.Select(m => m).ToList();

            if (list != null && list.Count() > 0)
            {
                foreach (var data in list)
                {
                    BranchModel model = new BranchModel()
                    {
                        branchid = data.Id,
                        branchName = data.BranchName,
                        branchNum = data.BanchNumber
                    };
                    result.Add(model);
                }
            }
            return result;
        }

        public List<AccountModel> accountNumberDropDown()
        {
            List<AccountModel> result = new List<AccountModel>();

            var list = database.Accounts.Select(m => m).ToList();

            if (list != null && list.Count() > 0)
            {
                foreach (var data in list)
                {
                    AccountModel model = new AccountModel()
                    {
                        id = data.Id,
                        accountNumber = data.AccountNumber,
                        minBalance = data.MinBalance,
                        withdrawLimit = data.WithdrawLimit,
                        balance = data.Balance,
                        userId = data.UserId
                    };
                    result.Add(model);
                }
            }
            return result;
        }

        public AccountModel addAccount(AccountModel model)
        {
            //User data = new User();
            Account table = new Account()
            {
                BranchId = model.branchId,
                UserId = model.userId,
                //BranchName = model.branchName,
                AccountNumber = model.accountNumber,
                MinBalance = model.minBalance,
                WithdrawLimit = model.withdrawLimit,
                Balance = model.balance
            };

            database.Accounts.Add(table);
            database.SaveChanges();

            model.id = table.Id;

            return model;
        }

        public List<BranchModel> BranchDetailsList()
        {
            using (var context = new BankDBEntities1())
            {
                var result = context.Branches
                    .Select(x => new BranchModel()
                    {
                        branchid = x.Id,
                        branchName = x.BranchName,
                        branchNum = x.BanchNumber,
                    }).ToList();
                return result;
            }
        }

        public BranchModel getBranchById(int id)
        {
            using (var db = new BankDBEntities1())
            {
                var result = db.Branches
                    .Where(x => x.Id == id)
                    .Select(x => new BranchModel()
                    {
                        branchid = x.Id,
                        branchName = x.BranchName,
                        branchNum = x.BanchNumber
                    }).FirstOrDefault();
                return result;
            }
        }

        public string updateImage(UserModel model)
        {
            var usr_data = database.Users.Where(x => x.Id == model.id).Select(x => new UserModel()
            {
                fName = x.FirstName,
                lName = x.LastName,
                mobileNo = x.MobileNumber,
                address = x.Address,
                email = x.Email,
                password = x.Password

            }).FirstOrDefault();

            User table = new User()
            {
                Id = model.id,
                FirstName = usr_data.fName,
                LastName = usr_data.lName,
                MobileNumber = usr_data.mobileNo,
                Address = usr_data.address,
                Email = usr_data.email,
                Password = usr_data.password,
                Image = model.image
            };

            database.Entry(table).State = EntityState.Modified;
            database.SaveChanges();

            return "success";
        }

        public bool UpdateBranchDetails(int id, BranchModel model)
        {
            using (BankDBEntities1 db = new BankDBEntities1())
            {
                Branch table = new Branch()
                {
                    Id = model.branchid,
                    BanchNumber = model.branchNum,
                    BranchName = model.branchName
                };

                db.Entry(table).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        public bool deleteBranch(int id)
        {
            //CashTransaction transaction_id = database.CashTransactions.FirstOrDefault(x => x.TransactionId == id);

            //List<Transaction> lstCashTransaction = database.CashTransactions.Where(x => x.TransactionId == id).ToList();
            //database.CashTransactions.RemoveRange(lstCashTransaction);
            //database.SaveChanges();
            //remove foreign key which is related to the table before delete main data otherwise it will throw error.

            //List <Transaction> lstTransaction = database.Transactions.Where(x => x.AccountId == id).ToList();
            //database.Transactions.RemoveRange(lstTransaction);
            //database.SaveChanges();

            //from account table get accountid from account table given branch id 
            //from transaction table get transactionid from accountid
            //from that transactionid get cash transaction id from cash transacton table

            var act_id = database.Accounts
                .Where(x => x.BranchId == id)
                .Select(x => new AccountModel()
                {
                    id = x.Id,
                }).FirstOrDefault();

            if(act_id != null)
            {

                var transaction_id = database.Transactions
                    .Where(x => x.AccountId == act_id.id).FirstOrDefault();

                if(transaction_id != null)
                {
                    var cash_trans_Id = database.CashTransactions
                    .Where(x => x.TransactionId == transaction_id.Id).FirstOrDefault();
                }

            }
    

            List<Account> delAccount = database.Accounts.Where(x => x.BranchId == id).ToList();
            database.Accounts.RemoveRange(delAccount);
            database.SaveChanges();

            using (BankDBEntities1 db = new BankDBEntities1())
            {

                Branch table = new Branch()
                {
                    Id = id
                };

                db.Entry(table).State = EntityState.Deleted;
                db.SaveChanges();

                return false;
            }

            //int a = act_id[1].id;

            //foreach (var allActId in act_id)
            //{ //where var can be your Class maybe Email
            //    //SendSimpleMessage(currentEmail);
            //    Console.WriteLine(allActId);
            //}

            //TransactionModel trans_id = database.Transactions
            //    .Where(x => x.AccountId == a)
            //    .Select(x => new TransactionModel()
            //    {
            //        id = x.Id,

            //    }).FirstOrDefault();

            //var data = database.CashTransactions
            //        .Select(x => new CashTransactionModel()
            //        {
            //            transactionId = x.TransactionId.Value
            //        }).FirstOrDefault();


        }


        //public bool deleteBranch(int id)
        //{

        //    Branch DelBranch = database.Branches.FirstOrDefault(x => x.Id == id);

        //    if (DelBranch != null)
        //    {
        //        database.Branches.Remove(DelBranch);
        //        database.SaveChanges();
        //        return true;

        //    }
        //    return false;
        //}

        public List<AccountModel> accountDetailsList()
        {
            using (var context = new BankDBEntities1())
            {
                var result = context.Accounts
                    .Select(x => new AccountModel()
                    {
                        id = x.Id,
                        userId = x.UserId,
                        accountNumber = x.AccountNumber,
                        minBalance = x.MinBalance,
                        withdrawLimit = x.WithdrawLimit,
                        balance = x.Balance,
                        branchId = x.BranchId,
                        Branch = context.Branches.Where(b => b.Id == x.BranchId).Select(br=> new BranchModel() {branchid = br.Id,branchName=br.BranchName,branchNum = br.BanchNumber }).FirstOrDefault(),
                        User = context.Users.Where(u=> u.Id == x.UserId).Select(ur=> new UserModel() { id = ur.Id,fName = ur.FirstName,address=ur.Address,email = ur.Email,mobileNo = ur.MobileNumber,lName = ur.LastName}).FirstOrDefault()
                    }).ToList();
                return result;
            }
        }

        public UserModel getUserById(int id)
        {
            using (var db = new BankDBEntities1())
            {
                var result = db.Users
                    .Where(x => x.Id == id)
                    .Select(x => new UserModel()
                    {
                        id = x.Id,
                        fName = x.FirstName,
                        lName = x.LastName,
                        mobileNo = x.MobileNumber,
                        address = x.Address,
                        email = x.Email,
                        image = x.Image
                    }).FirstOrDefault();
                return result;
            }
        }

        public AccountModel getAccountById(int id)
        {
            var result = database.Accounts
                .Where(x => x.Id == id)
                .Select(x => new AccountModel()
                {
                    id = x.Id,
                    accountNumber = x.AccountNumber,
                    minBalance = x.MinBalance,
                    withdrawLimit = x.WithdrawLimit,
                    balance = x.Balance,
                    userId = x.UserId,
                    branchId = x.BranchId,
                    Branch = database.Branches.Where(b => b.Id == x.BranchId).Select(br => new BranchModel() { branchid = br.Id, branchName = br.BranchName, branchNum = br.BanchNumber }).FirstOrDefault(),
                    User = database.Users.Where(u => u.Id == x.UserId).Select(ur => new UserModel() { id = ur.Id, fName = ur.FirstName, address = ur.Address, email = ur.Email, mobileNo = ur.MobileNumber, lName = ur.LastName }).FirstOrDefault(),
                    
                }).FirstOrDefault();
            return result;
        }

        public bool UpdateAccountDetails(int id, AccountModel model)
        {
            Account table = new Account();

            table.Id = id;
            table.AccountNumber = model.accountNumber;
            table.MinBalance = model.minBalance;
            table.WithdrawLimit = model.withdrawLimit;
            table.Balance = model.balance;
            table.UserId = model.userId;
            table.BranchId = model.branchId;

            database.Entry(table).State = EntityState.Modified;

            database.SaveChanges();
            return true;
        }

        public bool deleteAccount(int id)
        {
            List<CashTransaction> lstCashTransaction = database.CashTransactions.Where(x => x.Transaction.AccountId == id).ToList();
            database.CashTransactions.RemoveRange(lstCashTransaction);
            database.SaveChanges();

            //remove foreign key which is related to the table before delete main data otherwise it will throw error.

            List<Transaction> lstTransaction = database.Transactions.Where(x => x.AccountId == id).ToList();
            database.Transactions.RemoveRange(lstTransaction);
            database.SaveChanges();

            //Account act_id = database.Accounts.FirstOrDefault(x => x.Id == id);
            //if(act_id != null)
            //{
            //    database.Entry(act_id).State = EntityState.Deleted;
            //    database.SaveChanges();
            //}
            //if (act_id != null)
            //{
            //    database.Accounts.Remove(act_id);
            //    database.SaveChanges();
            //}

            using (BankDBEntities1 db = new BankDBEntities1())
            {
                Account accountTable = new Account()
                {
                    Id = id
                };
                db.Entry(accountTable).State = EntityState.Deleted;
                db.SaveChanges();

                return false;
            }
        }


        public List<TransactionModel> transactionDetailsList()
        {
            using (var context = new BankDBEntities1())
            {
                var result = context.Transactions
                    .Select(x => new TransactionModel()
                    {
                        id = x.Id,
                        userId = x.UserId.Value,
                        accountId = x.AccountId.Value,
                        amount = x.Amount.Value,
                        date = x.Date,
                        transactionType = x.TransactionType,

                        //Account = new AccountModel()
                        //{

                        //},

                        //Account = new AccountModel() { 
                        //    id = x.Account.Id,
                        //    accountNumber = x.Account.AccountNumber,
                        //    userId = x.Account.UserId,
                        //    minBalance = x.Account.MinBalance,
                        //    withdrawLimit = x.Account.WithdrawLimit,
                        //    balance = x.Account.Balance,
                        //    branchId = x.Account.BranchId
                        //},
                        Account = new AccountModel
                        {
                            accountNumber = x.Account.AccountNumber,
                            balance = x.Account.Balance,
                        },

                        User = new UserModel() { id = x.User.Id,fName = x.User.FirstName, lName = x.User.LastName,mobileNo = x.User.MobileNumber, address = x.User.Address, email = x.User.Email}

                        //Branch = context.Branches.Where(b => b.Id == x.BranchId).Select(br => new BranchModel() { branchid = br.Id, branchName = br.BranchName, branchNum = br.BanchNumber }).FirstOrDefault(),
                        //User = context.Users.Where(u => u.Id == x.UserId).Select(ur => new UserModel() { id = ur.Id, fName = ur.FirstName, address = ur.Address, email = ur.Email, mobileNo = ur.MobileNumber, lName = ur.LastName }).FirstOrDefault()
                    }).ToList();

                
                return result;
            }
        }


        public TransactionModel transaction(TransactionModel model)
        {

            Transaction table = new Transaction()
            {
                AccountId = model.accountId,
                Amount = model.amount,
                TransactionType = model.transactionType,
                Date = DateTime.Now,
                UserId = model.Account.userId,
                Withdrawl = model.transactionType == "W" ? 1 : 0,
                Deposit = model.transactionType == "D" ? 1 : 0
                
            };

            table.CashTransactions.Add(new CashTransaction() { 
                C100 = model.CashTransaction.C100,
                C200 = model.CashTransaction.C200,
                C500 = model.CashTransaction.C500,
                C2000 = model.CashTransaction.C2000,
                Transaction = table,
                cash=Convert.ToString(table.Amount),
                Count = model.CashTransaction.Count 
            });

            Account obj = new Account()
            {
                Balance = model.transactionType == "D" ? (model.Account.balance + model.amount) : (model.Account.balance - model.amount),
                Id = model.Account.id,
                BranchId = model.Account.branchId,
                UserId = model.Account.User.id,
                AccountNumber = model.Account.accountNumber,
                MinBalance = model.Account.minBalance,
                WithdrawLimit = model.Account.withdrawLimit
            };

            database.Transactions.Add(table);

            database.Entry(obj).State = EntityState.Modified;

            database.SaveChanges();

            return model;
    
        }

        public int CashNoteCount(int C100, int C200, int C500, int C2000)
        {
            return C100 + C200 + C500 + C2000;
        }

        public bool CashNoteCount(int C100, int C200, int C500, int C2000, int amount)
        {
            int TotalAmount = ((C100 * 100) + (C200 * 200) + (C500 * 500) + (C2000 * 2000));
            if (TotalAmount == amount)
            {
                return true;
            }
            return false;
        }

        public bool UpdateUserImage(int id, string image)
        {
            User table = new User()
            {
                Id = id,
                Image = image
            };

            database.Entry(table).State = EntityState.Modified;

            database.SaveChanges();

            return true;
        }

    }

}
