using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineBankingSystem.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult Credit(int? id)
        {
            int usrId = Convert.ToInt32(Session["AccountID"]);
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            return View(objContext.Transactions);
        }
        public ActionResult Debit()
        {
            int usrId = Convert.ToInt32(Session["AccountID"]);
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            var items = objContext.Transactions.Where(u => u.SenderAccountID == usrId).ToList();
            return View(items);
        }
        public ActionResult PerformTransaction()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PerformTransaction(FormCollection objData)
        {

            //int usrId = Convert.ToInt32(Session["AccountID"]);
            int SenderAccountNo = Convert.ToInt32(objData[1]);
            int ReceiverAccountNo = Convert.ToInt32(objData[2]);
            string MobileNo = objData[3];
            int Amount = Convert.ToInt32(objData[4]);
            string TransactionType = objData[5];
            string Remark = objData[6];

            
        
            Models.OnlineBankingDBEntities2 objContext = new Models.OnlineBankingDBEntities2();
            var Receiver = objContext.Accounts.SingleOrDefault(u => u.AccountNo == ReceiverAccountNo );
            var Sender = objContext.Accounts.SingleOrDefault(u => u.AccountNo == SenderAccountNo);
            Models.Transaction objTransaction = new Models.Transaction()
            {
                SenderAccountID = Sender.AccountID,
                ReceiverAccountID = Receiver.AccountID,
                MobileNo = MobileNo,
                Amount = Amount,
                TransactionType = "DR",
                Remark = Remark,
            };
            if (Receiver != null && Sender!=null)
            {
                Receiver.Amount += Amount;
                Sender.Amount -= Amount;
                objContext.Entry(Receiver).State = System.Data.Entity.EntityState.Modified;
                objContext.Entry(Sender).State = System.Data.Entity.EntityState.Modified;
                
                objContext.Transactions.Add(objTransaction);
                objContext.SaveChanges();
                ViewBag.Message = "Amount Sent Successfullly";
            }
            else
            {
                ViewBag.Message = "Wrong Reciever's Or Senders Address Account Number";
            }
            return View();
        }
    }
}