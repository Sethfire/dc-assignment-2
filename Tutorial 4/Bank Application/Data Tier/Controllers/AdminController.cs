using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data_Tier.Model;

namespace Data_Tier.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/ProcessAllTransactions")]
        [HttpGet]
        public void ProcessAllTransactions()
        {
            Bank bank = Bank.GetInstance();
            bank.ProcessAllTransactions();
        }

        [Route("api/SaveToDisk")]
        [HttpGet]
        public void SaveToDisk()
        {
            Bank bank = Bank.GetInstance();
            bank.SaveToDisk();
        }
    }
}