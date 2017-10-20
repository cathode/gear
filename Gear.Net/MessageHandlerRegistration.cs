using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gear.Net
{
    /// <summary>
    /// Represents a runtime message handler registration associated with a <see cref="Channel"/>.
    /// </summary>
    public class MessageHandlerRegistration
    {
        /// <summary>
        /// Gets or sets the object that owns the action delegate.
        /// </summary>
        public object Owner { get; set; }

        /// <summary>
        /// Gets or sets the action delegate that will be invoked to handle the message.
        /// </summary>
        public Action<MessageEventArgs, IMessage> Action { get; set; }
    }
}
