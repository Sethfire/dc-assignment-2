using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainLibrary
{
    [ServiceContract]
    public interface ClientServerInterface
    {
        [OperationContract]
        List<Block> GetCurrentBlockchain();

        [OperationContract]
        Block GetCurrentBlock();

        [OperationContract]
        void ReceiveNewTransaction(Transaction transaction);
    }
}
