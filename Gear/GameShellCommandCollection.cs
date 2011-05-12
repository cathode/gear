/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
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
