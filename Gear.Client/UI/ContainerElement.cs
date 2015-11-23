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

namespace Gear.Client.UI
{
    /// <summary>
    /// Provides functionality that allows an element to contain other elements.
    /// </summary>
    public class ContainerElement : Element, ICollection<Element>
    {
        public void Add(Element item)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(Element item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(Element[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public bool Remove(Element item)
        {
            throw new NotImplementedException();
        }
        public IEnumerator<Element> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
