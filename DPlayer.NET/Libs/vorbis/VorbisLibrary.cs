using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DPlayer.NET.Libs.vorbis
{
    internal class VorbisLibrary
    {
        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_headerin")]
        internal static extern int HeaderIn(IntPtr info, IntPtr comment, IntPtr packet);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_blockin")]
        internal static extern int BlockIn(IntPtr state, IntPtr block);
         
        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_pcmout")]
        internal static extern int PCMOut(IntPtr state, IntPtr pcm);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_read")]
        internal static extern int Read(IntPtr state, int samples);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis")]
        internal static extern int Synth(IntPtr block, IntPtr packet);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_synthesis_init")]
        internal static extern int SynthInit(out IntPtr state, IntPtr info);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_info_init")]
        internal static extern void InfoInit(out IntPtr info);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_block_init")]
        internal static extern void BlockInit(IntPtr state, out IntPtr block);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_init")]
        internal static extern void CommentInit(out IntPtr comment);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_info_clear")]
        internal static extern void InfoClear(IntPtr info);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_block_clear")]
        internal static extern int BlockClear(IntPtr block);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_comment_clear")]
        internal static extern void CommentClear(IntPtr comment);

        [DllImport("libvorbis.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "vorbis_dsp_clear")]
        internal static extern void DSPClear(IntPtr state);

    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct vorbis_info
    {
        public int version;
        public int channels;
        public long rate;

        public long bitrate_upper;
        public long bitrate_nominal;
        public long bitrate_lower;
        public long bitrate_window;

        public void* codec_setup;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct ogg_packet
    {
        public byte* packet;
        public long bytes;
        public long b_o_s;
        public long e_o_s;

        public long granulepos;
        public long packetno;
    }
}
