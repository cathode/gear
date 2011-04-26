/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2011 Will 'cathode' Shelley. All Rights Reserved.         *
 *****************************************************************************/
using System.Collections.ObjectModel;

namespace Gear
{
    /// <summary>
    /// Represents a collection of <see cref="GameShellCommand"/> items.
    /// </summary>
    public sealed class GameShellCommandCollection : KeyedCollection<string, GameShellCommand>
    {
        protected override string GetKeyForItem(GameShellCommand item)
        {
            return item.Name;
        }
    }
}
