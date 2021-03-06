﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DPlayer.NET.Libs.opus
{
    /// <summary>
    /// Wraps the Opus API.
    /// Code was modified from: https://github.com/DevJohnC/Opus.NET
    /// </summary>
    internal class OpusLibrary
    {
        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr opus_encoder_create(int Fs, int channels, int application, out IntPtr error);

        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void opus_encoder_destroy(IntPtr encoder);

        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int opus_encode(IntPtr st, byte[] pcm, int frame_size, IntPtr data, int max_data_bytes);

        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr opus_decoder_create(int Fs, int channels, out IntPtr error);

        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void opus_decoder_destroy(IntPtr decoder);

        [DllImport("opus.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int opus_decode(IntPtr st, byte[] data, int len, IntPtr pcm, int frame_size, int decode_fec);
    }

    /// <summary>
    /// Supported coding modes.
    /// </summary>
    public enum Application : int
    {
        /// <summary>
        /// Best for most VoIP/videoconference applications where listening quality and intelligibility matter most.
        /// </summary>
        Voip = 2048,
        /// <summary>
        /// Best for broadcast/high-fidelity application where the decoded audio should be as close as possible to input.
        /// </summary>
        Audio = 2049,
        /// <summary>
        /// Only use when lowest-achievable latency is what matters most. Voice-optimized modes cannot be used.
        /// </summary>
        Restricted_LowLatency = 2051
    }

    public enum Errors : int
    {
        /// <summary>
        /// No error.
        /// </summary>
        OK = 0,
        /// <summary>
        /// One or more invalid/out of range arguments.
        /// </summary>
        BadArg = -1,
        /// <summary>
        /// The mode struct passed is invalid.
        /// </summary>
        BufferToSmall = -2,
        /// <summary>
        /// An internal error was detected.
        /// </summary>
        InternalError = -3,
        /// <summary>
        /// The compressed data passed is corrupted.
        /// </summary>
        InvalidPacket = -4,
        /// <summary>
        /// Invalid/unsupported request number.
        /// </summary>
        Unimplemented = -5,
        /// <summary>
        /// An encoder or decoder structure is invalid or already freed.
        /// </summary>
        InvalidState = -6,
        /// <summary>
        /// Memory allocation has failed.
        /// </summary>
        AllocFail = -7
    }
}
