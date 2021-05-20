using API_Classes;
using Database_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Database_API.Controllers
{
    public class ValuesController : ApiController
    {
        [Route("api/values")]
        [HttpGet]
        public int GetNumEntries()
        {
            return DataModel.GetNumEntries();
        }

        [Route("api/values/{index}")]
        [HttpGet]
        public DataStruct GetData(int index)
        {
            DataModel.GetValuesForEntry(index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
            DataStruct data = new DataStruct();
            data.acctNo = acctNo;
            data.pin = pin;
            data.bal = bal;
            data.fName = fName;
            data.lName = lName;

            return data;
        }

        [Route("api/values/search/{searchTerm}")]
        [HttpGet]
        public DataStruct SearchData(string searchTerm)
        {
            DataModel.SearchForEntry(searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
            DataStruct data = new DataStruct();
            data.acctNo = acctNo;
            data.pin = pin;
            data.bal = bal;
            data.fName = fName;
            data.lName = lName;

            return data;
        }
    }
}