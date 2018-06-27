﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DPlayer.NET.Libs.mp3
{
    internal class MP3Library32
    {
        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern errors mpg123_init();

        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_exit();

        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr mpg123_new(string decoder, IntPtr error);

        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_delete(IntPtr handle);

        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern errors mpg123_open(IntPtr handle, string path);

        [DllImport("libmpg32.dll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern errors mpg123_close(IntPtr handle);
    }

    enum errors : int
    {
        MPG123_DONE = -12,
        MPG123_NEW_FORMAT = -11,
        MPG123_NEED_MORE = -10,
        MPG123_ERR = -1,
        MPG123_OK = 0,
        MPG123_BAD_OUTFORMAT,
        MPG123_BAD_CHANNEL,
        MPG123_BAD_RATE,
        MPG123_ERR_16TO8TABLE,
        MPG123_BAD_PARAM,
        MPG123_BAD_BUFFER,
        MPG123_OUT_OF_MEM,
        MPG123_NOT_INITIALIZED,
        MPG123_BAD_DECODER,
        MPG123_BAD_HANDLE,
        MPG123_NO_BUFFERS,
        MPG123_BAD_RVA,
        MPG123_NO_GAPLESS,
        MPG123_NO_SPACE,
        MPG123_BAD_TYPES,
        MPG123_BAD_BAND,
        MPG123_ERR_NULL,
        MPG123_ERR_READER,
        MPG123_NO_SEEK_FROM_END,
        MPG123_BAD_WHENCE,
        MPG123_NO_TIMEOUT,
        MPG123_BAD_FILE,
        MPG123_NO_SEEK,
        MPG123_NO_READER,
        MPG123_BAD_PARS,
        MPG123_BAD_INDEX_PAR,
        MPG123_OUT_OF_SYNC,
        MPG123_RESYNC_FAIL,
        MPG123_NO_8BIT,
        MPG123_BAD_ALIGN,
        MPG123_NULL_BUFFER,
        MPG123_NO_RELSEEK,
        MPG123_NULL_POINTER,
        MPG123_BAD_KEY,
        MPG123_NO_INDEX,
        MPG123_INDEX_FAIL,
        MPG123_BAD_DECODER_SETUP,
        MPG123_MISSING_FEATURE,
        MPG123_BAD_VALUE,
        MPG123_LSEEK_FAILED,
        MPG123_BAD_CUSTOM_IO,
        MPG123_LFS_OVERFLOW,
        MPG123_INT_OVERFLOW
    }
}