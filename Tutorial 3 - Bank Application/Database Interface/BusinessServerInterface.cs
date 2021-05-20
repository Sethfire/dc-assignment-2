using System.Drawing;
using System.ServiceModel;

namespace Database_Interface
{
    [ServiceContract]
    public interface BusinessServerInterface
    {
        [OperationContract]
        int GetNumEntries();

        [OperationContract]
        [FaultContract(typeof(IndexFault))]
        void GetValuesForEntry(int index, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);

        [OperationContract]
        void SearchForEntry(string searchTerm, out uint acctNo, out uint pin, out int bal, out string fName, out string lName, out Bitmap icon);
    }
}