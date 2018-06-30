using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DPlayer.NET.Libs
{
    interface AudioDecoder
    {
        int Decode(MemoryStream input, MemoryStream output);
    }
}
