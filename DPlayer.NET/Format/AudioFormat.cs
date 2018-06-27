using System;
using System.Collections.Generic;
using System.Text;

namespace DPlayer.NET.Format
{
    abstract class AudioFormat
    {
        public readonly int channels;

        public readonly int samplerate;

        public readonly int samplecount;
        
        AudioFormat(int channels, int samplerate, int samplecount)
        {
            this.channels = channels;
            this.
        }

    }
}
