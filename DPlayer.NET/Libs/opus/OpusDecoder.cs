﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DPlayer.NET.Libs.opus
{
    /// <summary>
    /// Opus audio decoder.
    /// </summary>
    public class OpusDecoder : AudioDecoder
    {
        private IntPtr _decoder;
        private int channels;

        public OpusDecoder(int sampleRate, int channels)
        {
            IntPtr error;
            _decoder = OpusLibrary.opus_decoder_create(sampleRate, channels, out error);
            this.channels = channels;

            if ((Errors)error != Errors.OK)
            {
                throw new InvalidOperationException("Unable to create decoder of sampleRate " + sampleRate + " and channels " + channels);
            }
        }

        /// <summary>
        /// Decodes the Opus encoded bytes to PCM audio samples
        /// </summary>
        /// <param name="input">The memorystream with the bytes</param>
        /// <param name="output">The memorystream to put the decoded bytes to</param>
        /// <returns>The amount of samples done</returns>
        public unsafe int Decode(MemoryStream input, MemoryStream output)
        {
            if (disposed)
                throw new ObjectDisposedException("OpusDecoder");

            IntPtr decodedPtr;
            output.SetLength(0);
            byte[] buffer =  output.GetBuffer();
            int res;
            fixed (byte* add = buffer)
            {
                decodedPtr = new IntPtr((void*)add);
                res = OpusLibrary.opus_decode(_decoder, input.ToArray(), Convert.ToInt32(input.Length - input.Position), decodedPtr, Convert.ToInt32(output.Length - output.Position) / channels, 0);
            }
            if ((Errors)res != Errors.OK)
                throw new InvalidOperationException("Error decoding");
            output.Position = res * channels;
            output.SetLength(output.Position);
            output.Position = 0;
            return res;
        }

        ~OpusDecoder()
        {
            Dispose();
        }

        private bool disposed;

        /// <summary>
        /// Disposes all the resources used to clean the memory
        /// </summary>
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);
            if (_decoder != IntPtr.Zero)
            {
                OpusLibrary.opus_decoder_destroy(_decoder);
                _decoder = IntPtr.Zero;
            }
            disposed = true;
        }
    }
}

