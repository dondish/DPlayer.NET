using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DPlayer.NET.Libs
{
    interface AudioEncoder : IDisposable
    {
        /// <summary>
        /// Encodes the input stream (PCM audio bytes) to the output (Opus encoded bytes)
        /// </summary>
        /// <param name="input">PCM MemoryStream</param>
        /// <param name="output">Opus MemoryStream</param>
        /// <param name="frameSize">The amount of PCM samples in one channel</param>
        /// <returns></returns>
        int Encode(MemoryStream input, MemoryStream output, int frameSize);
    }
}
