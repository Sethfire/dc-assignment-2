using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client_Application
{
    [ServiceContract]
    public interface ClientServerInterface
    {
        //Might be ServiceContract as opposed to OperationContract
        [OperationContract]
        void AvailableJobs();

        [OperationContract]
        void DownloadJob();

        [OperationContract]
        void SubmitJob();
    }
}
