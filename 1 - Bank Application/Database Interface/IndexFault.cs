// Alex Starling - Distributed Computing - 2021
using System.Runtime.Serialization;

namespace Database_Interface
{
    // This is just a basic fault so you can get an idea on how to use them
    [DataContract]
    public class IndexFault
    {
        private string issue;

        [DataMember]
        public string Issue 
        {
            get { return issue; }
            set { issue = value; }
        }
    }
}