/******************************************************************************
 * Gear: An open-world sandbox game for creative people.                      *
 * http://github.com/cathode/gear/                                            *
 * Copyright © 2009-2016 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the MIT        *
 * license. See the included LICENSE file for details.                        *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Modeling
{
    /// <summary>
    ///  Represent how a selection is merged with another selection.
    /// </summary>
    public enum SelectionAction
    {
        /// <summary>
        /// Indicates the selection is cleared of all items.
        /// </summary>
        Clear = 0,

        /// <summary>
        /// Indicates the selection is assigned to a set of items.
        /// </summary>
        Select,

        /// <summary>
        /// Indicates the items in the new selection are merged with the items in the existing selection.
        /// </summary>
        Add,

        /// <summary>
        /// Indicates that any items in the existing selection which also exist in the new selection, are removed from the existing selection.
        /// </summary>
        Remove,
    }
}
