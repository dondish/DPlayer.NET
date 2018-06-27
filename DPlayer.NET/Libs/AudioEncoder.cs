using System;
using System.Collections.Generic;
using System.Text;

namespace DPlayer.NET.Libs
{
    interface AudioEncoder : IDisposable
    {
        /// <summary>
        /// Encodes the stuff you need to the AudioEncoder type (Opus)
        /// </summary>
        /// <param name="inputPcmSamples">
        /// Samples of pcm to encode
        /// </param>
        /// <param name="sampleLength">
        /// The length of the sample
        /// </param>
        /// <param name="encodedLength">
        /// The encoded length int to initialize
        /// </param>
        /// <returns></returns>
        byte[] Encode(byte[] inputPcmSamples, int sampleLength, out int encodedLength);
    }
}
