using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace OnlineBankingSystem.Controllers
{
    public class AccountController : Controller
    {
        //public int accountid { get; set; }
        
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection objData)
        {
            try
            {

                string strMessage = string.Empty;
                Models.Customer cus = new Models.Customer();

                cus.Password = objData[1];
                if (objData[0] == "Admin" && objData[1] == "Admin")
                {
                    Session["Name"] = "Admin";
                    return RedirectToAction("AdminHome");
                }
                else
                {
                    cus.AccountNo = Convert.ToInt32(objData[0]);
                    using (Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2())
                    {
                        var usr = objContext.Accounts.SingleOrDefault(u => u.AccountNo == cus.AccountNo && u.Password == cus.Password);
                        if (usr != null)
                        {

                            Session["AccountID"] = usr.AccountID.ToString();
                            Session["Name"] = usr.Name;
                            Session["Balance"] = usr.Amount;
                            return Redirect("/Transaction/HomePage");

                        }
                        else
                        {
                            strMessage = "*Invaild AccountNo Or Password";

                        }
                        ViewBag.Message = strMessage;
                    }
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Registration()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Registration(Models.Account objData )
        {
            try
            {
                //int strAccountNo = Convert.ToInt32(objData["AccountNo"]);
                //string strAccountType = objData["AccountType"];
                //string strName = objData["Name"];
                //string strGender = objData["Gender"];
                //string strEmail = objData["Email"];
                //string strMobileNo = objData["MobileNo"];
                //int strAmount = int.Parse(objData["Amount"]);
                //string strPassword = objData["Password"];
                //string strAddress = objData["Address"];
                //DateTime strDate = DateTime.Now.Date;
                if (ModelState.IsValid)
                {
                    Models.Account objCustomer = new Models.Account()
                    {
                        AccountNo = objData.AccountNo,
                        AccountType = objData.AccountType,
                        Name = objData.Name,
                        Gender = objData.Gender,
                        Email = objData.Email,
                        MobileNo = objData.Email,
                        Amount = objData.Amount,
                        Password = objData.Password,
                        Address = objData.Address,
                        DateOfOpening = objData.DateOfOpening,

                        //AccountNo = strAccountNo,
                        //AccountType = strAccountType,
                        //Name = strName,
                        //Gender = strGender,
                        //Email = strEmail,
                        //MobileNo = strMobileNo,
                        //Amount = strAmount,
                        //Password = strPassword,
                        //Address = strAddress,
                        //DateOfOpening = strDate,

                    };

                    Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
                    objContext.Accounts.Add(objCustomer);
                    objContext.SaveChanges();

                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
                return View();
            }
            catch (Exception ex)
            {
                
                return View("Error");
            }
            
            
        }
        
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(Models.ResetPwd obj)
        {
            
            int usrId = Convert.ToInt32(Session["AccountID"]);
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            var usr = objContext.Accounts.SingleOrDefault(u => u.AccountID == usrId && u.Password == obj.CurrentPassword);
            if (usr != null)
            {
                usr.Password = obj.NewPassword;
                objContext.Entry(usr).State = System.Data.Entity.EntityState.Modified;
                objContext.SaveChanges();
                ViewBag.Message = "Your Passsword is updated Sucessfully";
            }
            else
            {
                ViewBag.Message = "Invaild current password";
            }
            
            return RedirectToAction("Login");
        }


        public ActionResult UserList()
        {
            Models.OnlineBankingDBEntities2 objContext =new Models.OnlineBankingDBEntities2();
            return View(objContext.Accounts.AsEnumerable());
        }
        public ActionResult Edit(int? id)
        {
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            return View(objContext.Accounts.Find(id));
        }
        [HttpPost]
        public ActionResult Edit(Models.Account obj)
        {
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            objContext.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            objContext.SaveChanges();
            //ViewBag.Message = "Your Passsword is updated Sucessfully";
            return View();
        }
        public ActionResult Delete(int? id)
        {
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            return View(objContext.Accounts.Find(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            Models.Account obj = objContext.Accounts.Find(id);
            objContext.Accounts.Remove(obj);
            objContext.SaveChanges();
            return RedirectToAction("UserList");

        }
        public ActionResult Details(int? id)
        {
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            return View(objContext.Accounts.Find(id));
        }
        
        public ActionResult AdminHome()
        {
            return View();
        }
    }
}