using API_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApplication
{
    public static class JobBoard
    {
        private static List<JobStruct> availableJobs;
        private static List<JobStruct> completedJobs;

        static JobBoard()
        {
            availableJobs = new List<JobStruct>();
            completedJobs = new List<JobStruct>();
        }

        public static int GetNumOfAvailableJobs()
        {
            return availableJobs.Count();
        }

        public static int GetNumOfCompletedJobs()
        {
            return completedJobs.Count();
        }

        public static JobStruct DownloadJob()
        {
            return availableJobs[0];
        }
        public static JobStruct FindJob(int jobID)
        {
            foreach(JobStruct job in availableJobs)
            {
                if (job.JobID == jobID)
                    return job;
            }

            throw new Exception();
        }

        public static void NewJob(JobStruct job)
        {
            availableJobs.Add(job);
        }

        public static void CompleteJob(int jobID, string solution)
        {
            JobStruct job = FindJob(jobID);
            availableJobs.Remove(job);
            completedJobs.Add(job);

            Console.WriteLine($"Solution uploaded for job {jobID} - {solution}");
        }
    }
}
