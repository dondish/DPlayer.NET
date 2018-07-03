using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DPlayer.NET.Libs.vorbis
{
    /// <summary>
    /// A decoder for the vorbis format
    /// </summary>
    class VorbisDecoder : IDisposable
    {
        private IntPtr _info;
        private IntPtr _comment;
        private IntPtr _state;
        private IntPtr _block;
        public int channels { get; private set; } = 0;

        public VorbisDecoder()
        {
            VorbisLibrary.CommentInit(out _comment);
            VorbisLibrary.InfoInit(out _info);
        }


        /// <summary>
        /// Parse a header of the vorbis stream
        /// </summary>
        /// <param name="input">MemoryStream with the header</param>
        /// <param name="length">Length of the header</param>
        /// <param name="bos">Boolean if this is the beginning of the stream</param>
        public unsafe void ProcessHeader(MemoryStream input, int length, bool bos)
        {
            if (disposed)
                throw new ObjectDisposedException("VorbisDecoder");

            ogg_packet packet= new ogg_packet();
            byte[] buffer = input.GetBuffer();
            int res;
            fixed (byte* buf = buffer)
            {
                packet.packet = &buf[input.Position];
                packet.bytes = length;
                packet.b_o_s = bos ? 1 : 0;
                packet.e_o_s = 0;
                packet.granulepos = 0;
                packet.packetno = 0;
                res = VorbisLibrary.HeaderIn(_info, _comment, new IntPtr((void*)&packet));
            }
            input.Position = input.Position + length;

            if (res != 0) // error ocurred
                throw new InvalidOperationException("Failed to process header, error - " + res);
        }

        /// <summary>
        /// Initialize the decoder
        /// </summary>
        public unsafe void Init()
        {
            if (disposed)
                throw new ObjectDisposedException("VorbisDecoder");

            int res = VorbisLibrary.SynthInit(out _state, _info);

            if (res != 0)
                throw new InvalidOperationException("Failed to initialize library, error - " + res);

            VorbisLibrary.BlockInit(_state, out _block);
            channels = (*((vorbis_info*)_info.ToPointer())).channels;
        }

        /// <summary>
        /// Fill the decoder with data
        /// </summary>
        /// <param name="input">MemoryStream with the data</param>
        public unsafe void Fill(MemoryStream input)
        {
            if (disposed)
                throw new ObjectDisposedException("VorbisDecoder");

            ogg_packet packet = new ogg_packet();
            byte[] buffer = input.GetBuffer();
            int res;
            fixed (byte* buf = buffer)
            {
                packet.packet = &buf[input.Position];
                packet.bytes = input.Length - input.Position;
                packet.b_o_s = 0;
                packet.e_o_s = 0;
                packet.granulepos = 0;
                packet.packetno = 0;
                int err = VorbisLibrary.Synth(_block, new IntPtr((void*)&packet));
                if (err != 0)
                    throw new InvalidOperationException("Failed to fill decoder, error - " + err);

                res = VorbisLibrary.BlockIn(_state, _block);
            }
            if (res != 0)
                throw new InvalidOperationException("Failed to fill decoder, error - " + res);
        }

        /// <summary>
        /// Gets the decoded info from the decoder
        /// </summary>
        /// <param name="pcmc">An array of buffers containing data for every channel</param>
        /// <returns>Amount of samples decoded</returns>
        public unsafe int Decode(float[][] pcmc)
        {
            if (disposed)
                throw new ObjectDisposedException("VorbisDecoder");

            if (pcmc.Length == 0)
                throw new InvalidOperationException("Cannot decode to empty PCM buffers");

            float** pcmcs;
            int samples = Math.Min(VorbisLibrary.PCMOut(_state, new IntPtr((void*)&pcmcs)), pcmc[0].Length);
            
            if (samples > 0)
            {
                for (int b=0;b<(*((vorbis_info*)_info.ToPointer())).channels;b++) // yeah, I know, ew
                {
                    float[] buffer = pcmc[b];
                    if (buffer != null) {
                        for (int i=0;i<samples;i++)
                        {
                            buffer[i] = *pcmcs[i];
                        }
                    }
                }
                VorbisLibrary.Read(_state, samples);
                return samples;
            }
            throw new InvalidOperationException("Failed to read the decoded data, error - " + samples);
        }

        ~VorbisDecoder()
        {
            Dispose();
        }

        private bool disposed;
        public void Dispose()
        {
            if (disposed)
                return;

            GC.SuppressFinalize(this);
            channels = 0;

            if (_info != IntPtr.Zero&&_comment != IntPtr.Zero)
            {
                VorbisLibrary.CommentClear(_comment);
                VorbisLibrary.InfoClear(_info);
            }
            if (_state != IntPtr.Zero&&_block != IntPtr.Zero)
            {
                VorbisLibrary.BlockClear(_block);
                VorbisLibrary.DSPClear(_state);
            }

            disposed = true;
        }
    }
}
