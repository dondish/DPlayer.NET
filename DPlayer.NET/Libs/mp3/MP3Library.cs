using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DPlayer.NET.Libs.mp3
{
    internal class MP3Library32
    {
        internal const string libname = "libmpg32.dll";

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Errors mpg123_init();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_exit();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr mpg123_new(string decoder, out IntPtr error);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_delete(IntPtr handle);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Errors mpg123_decode(IntPtr handle, IntPtr inMemory, UIntPtr inMemSize, IntPtr outMemory, UIntPtr outMemSize, ref UIntPtr done);

    }
    internal class MP3Library64
    {
        internal const string libname = "libmpg64.dll";

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Errors mpg123_init();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_exit();

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        internal static extern IntPtr mpg123_new(string decoder, out IntPtr error);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void mpg123_delete(IntPtr handle);

        [DllImport(libname, CallingConvention = CallingConvention.Cdecl)]
        internal static extern Errors mpg123_decode(IntPtr handle, IntPtr inMemory, UIntPtr inMemSize, IntPtr outMemory, UIntPtr outMemSize, ref UIntPtr done);

    }

    enum Errors : int
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

    enum Encodings { 
        mpg123_text_unknown = 0, mpg123_text_utf8 = 1, 
        mpg123_text_latin1 = 2, mpg123_text_icy = 3, 
        mpg123_text_cp1252 = 4, mpg123_text_utf16 = 5, 
        mpg123_text_utf16bom = 6, mpg123_text_utf16be = 7, 
        mpg123_text_max = 7 
    }
}
