﻿/******************************************************************************
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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace Gear.Client.Modeling
{
    public class ProcedureParameterCollection : KeyedCollection<string, ProcedureParameter>
    {
        protected override string GetKeyForItem(ProcedureParameter item)
        {
            return item.Name;
        }
    }
}
