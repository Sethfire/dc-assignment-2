using DatabaseServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DatabaseServer.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/ProcessAllTransactions")]
        [HttpGet]
        public void ProcessAllTransactions()
        {
            Bank.ProcessAllTransactions();
        }

        [Route("api/SaveToDisk")]
        [HttpGet]
        public void SaveToDisk()
        {
            Bank.SaveToDisk();
        }
    }
}