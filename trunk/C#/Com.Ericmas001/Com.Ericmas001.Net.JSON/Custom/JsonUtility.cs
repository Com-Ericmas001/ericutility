using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Com.Ericmas001.Net.JSON.Custom
{
    public static class JsonUtility
    {
        public const string MIME_TYPE = "application/json";

        internal const char BEGIN_ARRAY = '[';
        internal const char END_ARRAY = ']';
        internal const char BEGIN_OBJECT = '{';
        internal const char END_OBJECT = '}';
        internal const char NAME_SEPARATOR = ':';
        internal const char VALUE_SEPARATOR = ',';
        internal const char QUOTE = '"';

        internal static readonly CultureInfo CultureInfo = new CultureInfo("en-US", false);

        internal static string EscapeNonPrintCharacter(char c)
        {
            const string start = "\\u";
            return start + ((int)c).ToString("x4");
        }

        internal static string EscapeString(string text)
        {
            var sb = new StringBuilder();
            sb.Append('"');
            foreach (var c in text)
            {
                //bool hex = false;

                if (c == '\"') sb.Append("\\\"");
                else if (c == '\\') sb.Append("\\\\");
                else if (c == '\b') sb.Append("\\b");
                else if (c == '\f') sb.Append("\\f");
                else if (c == '\n') sb.Append("\\n");
                else if (c == '\r') sb.Append("\\r");
                else if (c == '\t') sb.Append("\\t");
                else if (char.IsLetterOrDigit(c)) sb.Append(c);
                else if (char.IsPunctuation(c)) sb.Append(c);
                else if (char.IsSeparator(c)) sb.Append(c);
                else if (char.IsWhiteSpace(c)) sb.Append(c);
                else if (char.IsSymbol(c)) sb.Append(c);
                else
                {
                    sb.Append(EscapeNonPrintCharacter(c));
                }

                //else if (c >= 0x0020 && c <= 0x0021) hex = true;
                //else if (c >= 0x0023 && c <= 0x005b) hex = true;
                //else if (c >= 0x005d) hex = true;
            }
            sb.Append('"');

            return sb.ToString();
        }

        internal static string UnEscapeString(string text)
        {
            text = text.Trim();

            if (text.StartsWith("\"")) text = text.Remove(0, 1);
            if (text.EndsWith("\"")) text = text.Remove(text.Length - 1, 1);

            var sb = new StringBuilder();

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                switch (c)
                {
                    case '\\':
                    {
                        i++;
                        if (text[i] == 'u' || text[i] == 'U')
                        {
                            var hex = text.Substring(i + 1, 4);
                            var u = (char)int.Parse(hex, NumberStyles.HexNumber);

                            i += 4;
                            sb.Append(u);
                            continue;
                        }

                        if (text[i] == 'n')
                        {
                            sb.Append('\n');
                            continue;
                        }

                        if (text[i] == 'r')
                        {
                            sb.Append('\r');
                            continue;
                        }

                        if (text[i] == 't')
                        {
                            sb.Append('\t');
                            continue;
                        }

                        if (text[i] == 'f')
                        {
                            sb.Append('\f');
                            continue;
                        }

                        if (text[i] == 'b')
                        {
                            sb.Append('\b');
                            continue;
                        }

                        if (text[i] == '\\')
                        {
                            sb.Append('\\');
                            continue;
                        }

                        if (text[i] == '/')
                        {
                            sb.Append('/');
                            continue;
                        }

                        if (text[i] == '"')
                        {
                            sb.Append('"');
                            continue;
                        }

                        throw new FormatException("Unrecognized escape sequence '\\" + text[i] +
                                                  "' in position: " + i + ".");
                    }

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }

        #region Indentation

        private static bool m_GenerateIndentedJsonText = true;

        internal const char INDENT = '\t';
        internal const char SPACE = ' ';

        internal static int ThreadId { get { return Thread.CurrentThread.ManagedThreadId; } }

        internal static readonly SortedDictionary<int, int> IndentDepthCollection = new SortedDictionary<int, int>();

        internal static int IndentDepth
        {
            get
            {
                var tid = ThreadId;
                try
                {
                    return IndentDepthCollection[tid];
                }
                catch (KeyNotFoundException)
                {
                    return 0;
                }
            }
            set
            {
                IndentDepthCollection[ThreadId] = value;
            }
        }

        public static bool GenerateIndentedJsonText
        {
            get { return m_GenerateIndentedJsonText; }
            set { m_GenerateIndentedJsonText = value; }
        }

        internal static string GetIndentString()
        {
            var len = IndentDepth;
            if (len <= 0)
            {
                return string.Empty;
            }
            return new string(INDENT, len);
        }

        internal static void WriteIndent(TextWriter writer)
        {
            if (GenerateIndentedJsonText)
            {
                writer.Write(GetIndentString());
            }
        }

        internal static void WriteSpace(TextWriter writer)
        {
            if (GenerateIndentedJsonText)
            {
                writer.Write(SPACE);
            }
        }

        internal static void WriteLine(TextWriter writer)
        {
            if (GenerateIndentedJsonText)
            {
                writer.Write(Environment.NewLine);
            }
        }

        #endregion Indentation
    }
}