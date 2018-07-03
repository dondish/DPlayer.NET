using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DPlayer.NET.Libs.aac
{
    /// <summary>
    /// Opposed to the Opus and MP3, the AAC decoder will first need to be filled up with data to decode and then be decoded frame by frame
    /// </summary>
    class AACDecoder : IDisposable
    {
        private IntPtr _decoder;

        public AACDecoder()
        {
            _decoder = AACLibrary.Open(0, 1); // the layer will be 1 for now, maybe will need to check some stuff later.
        }


        /// <summary>
        /// Used to configure the decoder.
        /// Must be done before start of decoding
        /// </summary>
        /// <param name="config">Raw configuration (ASC format)</param>
        public unsafe void Config(byte[] config)
        {
            if (config.Length > 8)
                throw new InvalidOperationException("Error! Header is larger than 8!");

            int error;
            fixed (byte* conf = config)
            {
                IntPtr ptr = new IntPtr((void*)conf);
                error = AACLibrary.ConfigRaw(_decoder, ptr, Convert.ToUInt32(config.Length));
            }

            if (error != 0)
                throw new InvalidOperationException("Got an error configuring the decoder, " + error);
        }

        /// <summary>
        /// Fill the decoder with AAC encoded data
        /// </summary>
        /// <param name="input">The MemoryStream which the encoded bytes are in</param>
        /// <returns>The amount of bytes added</returns>
        public unsafe int Fill(MemoryStream input)
        {
            if (disposed)
                throw new ObjectDisposedException("AACDecoder");

            int read;
            byte[] buffer = input.GetBuffer();
            fixed (byte* buf = buffer)
            {
                IntPtr inbuf = new IntPtr((void*)buf);
                read = AACLibrary.Fill(_decoder, inbuf, Convert.ToUInt32(input.Position), Convert.ToUInt32(input.Length-input.Position));
            }

            if (read != 0) // error happened
            {
                throw new InvalidOperationException("Error filling decoder, errors - " + read);
            }

            input.Position += read;
            return read;
        }


        /// <summary>
        /// Decodes one audio frame
        /// </summary>
        /// <param name="output">The MemoryStream to put the PCM encoded data</param>
        /// <param name="f">Wether to add the flush flag or not</param>
        /// <returns>Error number</returns>
        public unsafe int Decode(MemoryStream output, bool f)
        {
            if (disposed)
                throw new ObjectDisposedException("AACDecoder");

            int res;
            byte[] buffer = output.GetBuffer();
            int flags = f ? 2 : 0;
            fixed (byte* buf = buffer)
            {
                IntPtr outbuf = new IntPtr((void*)buf);
                res = AACLibrary.Decode(_decoder, outbuf, flags);
            }
            if (res != 0)
                throw new InvalidOperationException("Decoding frame returned error, " + res);

            return res;
        }


        ~AACDecoder() {
            Dispose();
        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);
            if (_decoder != IntPtr.Zero)
            {
                AACLibrary.Close(_decoder);
                _decoder = IntPtr.Zero;
            }
            disposed = true;
        }
    }
}
