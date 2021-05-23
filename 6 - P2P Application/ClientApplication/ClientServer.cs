using API_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class ClientServer : ClientServerInterface
    {
        //Get Number of Available Jobs
        public int GetNumOfAvailableJobs()
        {
            return JobBoard.GetNumOfAvailableJobs();
        }

        //Download a Job
        public JobStruct DownloadJob()
        {
            if (JobBoard.GetNumOfAvailableJobs() < 0)
                throw new Exception();

            return JobBoard.DownloadJob();
        }

        //Uploads a solution for a job with the given Job ID
        public void UploadSolution(int jobID, string solution)
        {
            JobBoard.CompleteJob(jobID, solution);
        }
    }
}
