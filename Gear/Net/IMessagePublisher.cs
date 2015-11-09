﻿/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2014 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
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

   
}
