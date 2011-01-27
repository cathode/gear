using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rust.Assets
{
    public interface IFieldValue
    {
        int WriteTo(byte[] buffer, int bufferIndex);
        int ReadFrom(byte[] buffer, int bufferIndex);
    }
}
