using System;
using System.Collections.Generic;
using System.Text;
namespace DPlayer.NET.Libs.mp3
{
    class MP3Decoder : AudioDecoder
    {
        private IntPtr handle;

        public static MP3Decoder Create(int outputSampleRate, int outputChannels) 
        {
            if (outputSampleRate != 44100)
                throw new ArgumentOutOfRangeException("outputSamplingRate");
            if (outputChannels != 1 && outputChannels != 2)
                throw new ArgumentOutOfRangeException("outputChannels");

            if ((Environment.Is64BitOperatingSystem? MP3Library64.mpg123_init() : MP3Library32.mpg123_init()) != Errors.MPG123_OK)
                throw new Exception("Exception occured while creating decoder");

            IntPtr error;
            IntPtr handle = Environment.Is64BitOperatingSystem ? MP3Library64.mpg123_new(null, out error): MP3Library32.mpg123_new(null, out error);
            
            if ((Errors) error != Errors.MPG123_OK)
                throw new Exception("Exception occured while creating decoder");

            return new MP3Decoder(handle);
        }

        public MP3Decoder(IntPtr handle)
        {
            this.handle = handle;
        }



        public unsafe byte[] Decode(byte[] inputMp3Data, int dataLength, out int decodedLength)
        {
            
        }

        ~MP3Decoder() {
            Dispose();
        }

        private bool isMpegVersion1(byte[] buf, int offset)
        {

        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);

            if (handle != IntPtr.Zero)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    MP3Library64.mpg123_delete(handle);
                    MP3Library64.mpg123_exit();
                } else
                {
                    MP3Library32.mpg123_delete(handle);
                    MP3Library32.mpg123_exit();
                }
            }

            disposed = true;
        }
    }
}
