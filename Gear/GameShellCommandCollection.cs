/******************************************************************************
 * Gear: A game of block-based sandbox fun. http://github.com/cathode/gear/   *
 * Copyright © 2009-2013 William 'cathode' Shelley. All Rights Reserved.      *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System.Collections.ObjectModel;

namespace Gear
{
    /// <summary>
    /// Represents a collection of <see cref="GShellCommand"/> items.
    /// </summary>
    public sealed class GameShellCommandCollection : KeyedCollection<string, GShellCommand>
    {
        protected override string GetKeyForItem(GShellCommand item)
        {
            return item.Name;
        }
    }
}
