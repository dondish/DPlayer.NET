using System;
using System.Collections.Generic;
using System.Text;

namespace DPlayer.NET.Libs
{
    interface AudioEncoder : IDisposable
    {
        byte[] Encode(byte[] inputPcmSamples, int sampleLength, out int encodedLength);
    }
}
