using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gear.Net;

namespace Gear.Net.Messages
{
    /// <summary>
    /// Represents a receipt sent back to the sender of a file after the receiver confirms all chunks of the file.
    /// </summary>
    public class TransferFileReceiptMessage : IMessage
    {
        public int DispatchId
        {
            get
            {
                return Ids.TransferFileReceipt;
            }
        }

        
        public long FileId { get; set; }
    }
}
