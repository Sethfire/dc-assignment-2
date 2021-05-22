using System.Runtime.Serialization;

namespace Database_Interface
{
    [DataContract]
    public class SearchFault
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
