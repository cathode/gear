using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    public interface IMessagePublisher
    {
        /// <summary>
        /// Raised when the publisher is making a message available to subscribers.
        /// </summary>
        event EventHandler<MessageEventArgs> MessageAvailable;

        /// <summary>
        /// Raised when the publisher is ceasing operation.
        /// </summary>
        event EventHandler ShuttingDown;
    }

    public class MessageEventArgs : EventArgs
    {
        public object Message { get; set; }
    }
}
