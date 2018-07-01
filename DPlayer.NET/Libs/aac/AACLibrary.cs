using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DPlayer.NET.Libs.aac
{
    /// <summary>
    /// The library wrapper for libfdk_aac modified from https://github.com/mstorsjo/fdk-aac
    /// </summary>
    internal class AACLibrary
    {
        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_Open")]
        internal static extern IntPtr Open(int transportType, uint numOfLayers);

        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_Close")]
        internal static extern void Close(IntPtr decoder);

        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_DecodeFrame")]
        internal static extern int Decode(IntPtr decoder, IntPtr outmem, int flags);

        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_Fill")]
        internal static extern int Fill(IntPtr decoder, IntPtr inmem, uint inmemsize, uint bytesValid);

        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_GetStreamInfo")]
        internal static extern int GetStreamInfo(IntPtr decoder);

        [DllImport("fdkaac.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "aacDecoder_ConfigRaw")]
        internal static extern int ConfigRaw(IntPtr decoder, IntPtr config, uint length);
    }
}
