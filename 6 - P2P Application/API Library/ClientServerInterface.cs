using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace API_Library
{
    [ServiceContract]
    public interface ClientServerInterface
    {
        [OperationContract]
        int GetNumOfAvailableJobs();

        [OperationContract]
        JobStruct DownloadJob();

        [OperationContract]
        void UploadSolution(int jobID, string solution);
    }
}
