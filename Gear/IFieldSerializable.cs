using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gear
{
    interface IFieldSerializable
    {
        IEnumerable<Field> GetFields();
        Field GetField(FieldKind id, short tag);
    }
}
