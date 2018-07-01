using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DPlayer.NET.Libs.opus
{
    /// <summary>
    /// Opus codec wrapper.
    /// Code was modified from: https://github.com/DevJohnC/Opus.NET
    /// </summary>
    public class OpusEncoder : AudioEncoder
    {
        IntPtr _encoder;

        public OpusEncoder(int sampleRate, int channels, int quality)
        {
            IntPtr error;
            _encoder = OpusLibrary.opus_encoder_create(sampleRate, channels, (int) Application.Audio, out error);

            if ((Errors)error != Errors.OK)
            {
                throw new InvalidOperationException("Unable to create decoder of sampleRate " + sampleRate + " and channels " + channels);
            }
        }

        /// <summary>
        /// Produces Opus encoded audio from PCM samples.
        /// </summary>
        /// <param name="inputPcmSamples">PCM samples to encode.</param>
        /// <param name="sampleLength">How many bytes to encode.</param>
        /// <param name="encodedLength">Set to length of encoded audio.</param>
        /// <returns>Opus encoded audio buffer.</returns>
        public unsafe int Encode(MemoryStream input, MemoryStream output, int frameSize)
        {
            if (disposed)
                throw new ObjectDisposedException("OpusEncoder");

            output.SetLength(0);
            IntPtr encodedPtr;
            byte[] encoded = output.GetBuffer();
            int res;
            fixed (byte* benc = encoded)
            {
                encodedPtr = new IntPtr((void*)benc);
                res = OpusLibrary.opus_encode(_encoder, input.ToArray(), frameSize, encodedPtr, output.Capacity);
            }

            if ((Errors)res != Errors.OK)
                throw new Exception("Encoding failed - " + ((Errors)res).ToString());
            output.Position = res;
            output.SetLength(output.Position);
            output.Position = 0;

            return res;
        }

        ~OpusEncoder()
        {
            Dispose();
        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);

            if (_encoder != IntPtr.Zero)
            {
                OpusLibrary.opus_encoder_destroy(_encoder);
                _encoder = IntPtr.Zero;
            }

            disposed = true;
        }
    }
}
