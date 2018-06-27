using System;
using System.Collections.Generic;
using System.Text;

namespace DPlayer.NET.Libs
{
    interface AudioDecoder
    {
        byte[] Decode(byte[] input, int dataLength, out int decodedLength);
    }
}
