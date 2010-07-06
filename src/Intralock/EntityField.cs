using System;
using System.Collections.Generic;
using System.Text;

namespace Intralock
{
    /// <summary>
    /// Represents a dynamic field within an entity.
    /// </summary>
    public abstract class EntityField<T>
    {
        #region Fields
        private T value;
        #endregion

    }
}
