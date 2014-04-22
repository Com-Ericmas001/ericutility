/*
 * This code is provided under the Code Project Open Licence (CPOL)
 * See http://www.codeproject.com/info/cpol10.aspx for details
*/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Com.Ericmas001.Windows.Forms.CustomTabControl
{
    /// <summary>
    /// Description of NativeMethods.
    /// </summary>
    //[SecurityPermission(SecurityAction.Assert, Flags=SecurityPermissionFlag.UnmanagedCode)]
    internal sealed class NativeMethods
    {
        private NativeMethods()
        {
        }

        #region Windows Constants

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_GETTABRECT = 0x130a;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_TRANSPARENT = 0x20;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_SETFONT = 0x30;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_FONTCHANGE = 0x1d;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_HSCROLL = 0x114;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int TCM_HITTEST = 0x130D;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WM_PAINT = 0xf;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_LAYOUTRTL = 0x400000;

        [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        public const int WS_EX_NOINHERITLAYOUT = 0x100000;

        #endregion Windows Constants

        #region Content Alignment

        public const ContentAlignment ANY_LEFT_ALIGN = ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft | ContentAlignment.TopLeft;

        public const ContentAlignment ANY_CENTER_ALIGN = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;

        #endregion Content Alignment

        #region User32.dll

        //        [DllImport("user32.dll"), SecurityPermission(SecurityAction.Demand)]
        //		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        public static IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            //	This Method replaces the User32 method SendMessage, but will only work for sending
            //	messages to Managed controls.
            var control = Control.FromHandle(hWnd);
            if (control == null)
            {
                return IntPtr.Zero;
            }

            var message = new Message();
            message.HWnd = hWnd;
            message.LParam = lParam;
            message.WParam = wParam;
            message.Msg = msg;

            var wproc = control.GetType().GetMethod("WndProc"
                                                           , BindingFlags.NonPublic
                                                            | BindingFlags.InvokeMethod
                                                            | BindingFlags.FlattenHierarchy
                                                            | BindingFlags.IgnoreCase
                                                            | BindingFlags.Instance);

            var args = new object[] { message };
            wproc.Invoke(control, args);

            return ((Message)args[0]).Result;
        }

        //		[DllImport("user32.dll")]
        //		public static extern IntPtr BeginPaint(IntPtr hWnd, ref PAINTSTRUCT paintStruct);
        //
        //		[DllImport("user32.dll")]
        //		[return: MarshalAs(UnmanagedType.Bool)]
        //		public static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT paintStruct);
        //

        #endregion User32.dll

        #region Misc Functions

        public static int LoWord(IntPtr dWord)
        {
            return dWord.ToInt32() & 0xffff;
        }

        public static int HiWord(IntPtr dWord)
        {
            if ((dWord.ToInt32() & 0x80000000) == 0x80000000)
                return (dWord.ToInt32() >> 16);
            return (dWord.ToInt32() >> 16) & 0xffff;
        }

        [SuppressMessage("Microsoft.Security", "CA2106:SecureAsserts")]
        [SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public static IntPtr ToIntPtr(object structure)
        {
            IntPtr lparam;
            lparam = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
            Marshal.StructureToPtr(structure, lparam, false);
            return lparam;
        }

        #endregion Misc Functions

        #region Windows Structures and Enums

        [Flags]
        public enum Tchittestflags
        {
            TchtNowhere = 1,
            TchtOnitemicon = 2,
            TchtOnitemlabel = 4,
            TchtOnitem = TchtOnitemicon | TchtOnitemlabel
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Tchittestinfo
        {
            public Tchittestinfo(Point location)
            {
                pt = location;
                flags = Tchittestflags.TchtOnitem;
            }

            public Point pt;
            public Tchittestflags flags;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct Paintstruct
        {
            public IntPtr hdc;
            public int fErase;
            public Rect rcPaint;
            public int fRestore;
            public int fIncUpdate;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] rgbReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;

            public Rect(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }

            public Rect(Rectangle r)
            {
                left = r.Left;
                top = r.Top;
                right = r.Right;
                bottom = r.Bottom;
            }

            public static Rect FromXywh(int x, int y, int width, int height)
            {
                return new Rect(x, y, x + width, y + height);
            }

            public static Rect FromIntPtr(IntPtr ptr)
            {
                var rect = (Rect)Marshal.PtrToStructure(ptr, typeof(Rect));
                return rect;
            }

            public Size Size
            {
                get
                {
                    return new Size(right - left, bottom - top);
                }
            }
        }

        #endregion Windows Structures and Enums
    }
}