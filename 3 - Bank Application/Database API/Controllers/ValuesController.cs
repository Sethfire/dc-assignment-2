using API_Classes;
using Database_API.Models;
using Database_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;

namespace Database_API.Controllers
{
    public class ValuesController : ApiController
    {
        [Route("api/Values")]
        [HttpGet]
        public HttpResponseMessage GetNumEntries()
        {
            int numEntries = DataModel.GetNumEntries();
            return Request.CreateResponse(HttpStatusCode.OK, numEntries);
        }

        [Route("api/Values/{index}")]
        [HttpGet]
        public HttpResponseMessage GetData(int index)
        {
            DataStruct data = new DataStruct();
            try
            {
                DataModel.GetValuesForEntry(index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
                data.acctNo = acctNo;
                data.pin = pin;
                data.bal = bal;
                data.fName = fName;
                data.lName = lName;
            }
            catch (FaultException<IndexFault> exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exception.Detail.Issue);
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Route("api/Values/Search/{searchTerm}")]
        [HttpGet]
        public HttpResponseMessage SearchData(string searchTerm)
        {
            DataStruct data = new DataStruct();
            try
            {
                DataModel.SearchForEntry(searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName);
                data.acctNo = acctNo;
                data.pin = pin;
                data.bal = bal;
                data.fName = fName;
                data.lName = lName;
            }
            catch (FaultException<SearchFault> exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, exception.Detail.Issue);
            }

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }
    }
}