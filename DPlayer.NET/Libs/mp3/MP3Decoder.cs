using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace DPlayer.NET.Libs.mp3
{
    class MP3Decoder : AudioDecoder
    {
        private IntPtr handle;
      

        public MP3Decoder()
        {
            Errors error = (Errors) MP3Library.Init();
            if (error != Errors.MPG123_OK)
                throw new InvalidOperationException("Couldn't create the MP3 decoder, Error " + error);

            IntPtr err;
            handle = MP3Library.New(null, out err);

            if (error != Errors.MPG123_OK)
                throw new InvalidOperationException("Couldn't create the MP3 decoder, Error " + error);
        }

        /// <summary>
        /// Decodes mp3 to PCM samples.
        /// </summary>
        /// <param name="input">MP3 in a MemoryStream</param>
        /// <param name="output">PCM samples in a MemoryStream</param>
        /// <returns>The amount of samples decoded</returns>
        public unsafe int Decode(MemoryStream input, MemoryStream output)
        {
            if (disposed)
                throw new ObjectDisposedException("MP3Decoder");

            IntPtr inPtr;
            IntPtr outPtr;
            byte[] buf = output.GetBuffer();
            byte[] inbuf = output.GetBuffer();
            UIntPtr done = new UIntPtr(0);
            fixed (byte* bufptr = buf, inbufptr = inbuf)
            {
                inPtr = new IntPtr(inbufptr);
                outPtr = new IntPtr(bufptr);

                int err = MP3Library.Decode(handle, inPtr, new UIntPtr(Convert.ToUInt32(input.Length - input.Position)), outPtr, new UIntPtr(Convert.ToUInt32(output.Length - output.Position)*2), ref done);
                while ((Errors) err == Errors.MPG123_NEW_FORMAT)  // Try out new format 
                {
                    err = MP3Library.Decode(handle, inPtr, new UIntPtr(0), outPtr, new UIntPtr(Convert.ToUInt32(output.Length - output.Position) * 2), ref done);
                }

                if ((Errors) err < Errors.MPG123_OK && (Errors) err != Errors.MPG123_NEED_MORE) // oopsie doopsie didn't worky
                {
                    throw new InvalidOperationException("Failed to decode, error " + (Errors)err);
                }
            }

            output.Position = done.ToUInt32() / 2;
            output.SetLength(output.Position);
            output.Position = 0;
            return (int) done.ToUInt32();
        }

        ~MP3Decoder() {
            Dispose();
        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);

            if (handle != IntPtr.Zero)
            {
                MP3Library.Delete(handle);
                MP3Library.Exit();
            }

            disposed = true;
        }

    }
}
