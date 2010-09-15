/******************************************************************************
 * Gear: A Steampunk Action-RPG - http://trac.gearedstudios.com/gear/         *
 * Copyright © 2009-2010 Will 'cathode' Shelley. All Rights Reserved.         *
 * This software is released under the terms and conditions of the Microsoft  *
 * Reference Source License (MS-RSL). See the 'license.txt' file for details. *
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear.Net.Messaging
{
    public sealed class StringMessageField : MessageField
    {
        #region Fields

        #endregion
        public override byte Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int CopyTo(byte[] buffer, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public override int CopyFrom(byte[] buffer, int startIndex, int count)
        {
            throw new NotImplementedException();
        }
    }
}
