using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Com.Ericmas001.Net.JSON
{
    public sealed class JsonTextParser
    {
        /// <summary>
        /// current position.
        /// </summary>
        private int m_CurrentPos;

        /// <summary>
        /// json text to parse
        /// </summary>
        private string m_JsonToParse = string.Empty;

        /// <summary>
        /// sync object for thread locking.
        /// </summary>
        private readonly object m_SyncObject = new object();

        /// <summary>
        /// Parses JSON text into JSonObject.
        /// </summary>
        /// <param name="text">JSON text to parse.</param>
        /// <returns>Json object parsed from text.</returns>
        public JsonObject Parse(string text)
        {
            lock (m_SyncObject)
            {
                m_CurrentPos = 0;

                if (text == null)
                {
                    return null;

                    //throw new FormatException();
                }

                m_JsonToParse = text.Trim();
                if (m_JsonToParse == string.Empty)
                {
                    return null;

                    //throw new FormatException();
                }
#if !DEBUG
                try
#endif
                {
                    return ParseSomethingWithoutName();
                }
#if !DEBUG
                catch (Exception)
                {
                    throw;
                }
#endif
            }
        }

        private bool IsEos { get { return m_CurrentPos >= m_JsonToParse.Length; } }

        private void SkipWhiteSpace()
        {
            for (; ; )
            {
                if (IsEos)
                {
                    break;
                }

                if (char.IsWhiteSpace(m_JsonToParse[m_CurrentPos]))
                {
                    m_CurrentPos++;
                }
                else
                {
                    break;
                }
            }

            if (IsEos)
            {
                throw new FormatException();
            }
        }

        private JsonObject ParseSomethingWithoutName()
        {
            SkipWhiteSpace();

            // collection?
            if (m_JsonToParse[m_CurrentPos] == '{' | m_JsonToParse[m_CurrentPos] == '[')
            {
                return ParseCollection();
            }

            // string?
            if (m_JsonToParse[m_CurrentPos] == '"')
            {
                return ParseStringValue();
            }

            // number?
            if (char.IsDigit(m_JsonToParse[m_CurrentPos]) || m_JsonToParse[m_CurrentPos] == '-')
            {
                return ParseNumericValue();
            }

            // literal?
            if (m_JsonToParse[m_CurrentPos] == 't' || m_JsonToParse[m_CurrentPos] == 'f' || m_JsonToParse[m_CurrentPos] == 'n')
            {
                return ParseLiteralValue();
            }

            throw new FormatException("Cannot parse a value.");
        }

        private JsonStringValue ParseStringValue()
        {
            if (m_JsonToParse[m_CurrentPos] != '"')
            {
                throw new FormatException();
            }

            // skip open quote
            m_CurrentPos++;

            var value = new StringBuilder();

            for (; ; m_CurrentPos++)
            {
                if (IsEos)
                {
                    throw new FormatException();
                }

                if (m_JsonToParse[m_CurrentPos] == '"')
                {
                    if (m_JsonToParse[m_CurrentPos - 1] != '\\')
                    {
                        break;
                    }
                }

                value.Append(m_JsonToParse[m_CurrentPos]);
            }

            if (m_JsonToParse[m_CurrentPos] != '"')
            {
                throw new FormatException();
            }

            m_CurrentPos++;

            var result = new JsonStringValue {Value = JsonUtility.UnEscapeString(value.ToString())};
            return result;
        }

        private static readonly Regex m_RegexLiteral = new Regex(
                @"(?<value>false|true|null)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private JsonBooleanValue ParseLiteralValue()
        {
            var m = m_RegexLiteral.Match(m_JsonToParse, m_CurrentPos);
            if (!m.Success)
            {
                throw new FormatException("Cannot parse a literal value.");
            }

            var captured = m.Captures[0].Value;
            m_CurrentPos += captured.Length;

            return new JsonBooleanValue(null, captured);
        }

        private JsonNumericValue ParseNumericValue()
        {
            //Match mnum = _regexNumber.Match(m_JsonToParse, m_CurrentPos);
            //if (!mnum.Success)
            //{
            //    throw new FormatException("Cannot parse a number value.");
            //}
            var deb = m_CurrentPos;
            for (; !",]}".Contains("" + m_JsonToParse[m_CurrentPos]); ++m_CurrentPos)
            {
            }
            var captured = m_JsonToParse.Substring(deb, m_CurrentPos - deb);

            //string captured = mnum.Captures[0].Value;
            //m_CurrentPos += captured.Length;

            var value = double.Parse(captured,
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign,
                JsonUtility.CultureInfo);

            return new JsonNumericValue(value);
        }

        private JsonCollection ParseCollection()
        {
            SkipWhiteSpace();

            JsonCollection result;
            bool isArray;

            if (m_JsonToParse[m_CurrentPos] == '{')
            {
                isArray = false;
                result = new JsonObjectCollection();
            }
            else if (m_JsonToParse[m_CurrentPos] == '[')
            {
                isArray = true;
                result = new JsonArrayCollection();
            }
            else
                throw new FormatException();

            // skip open bracket
            m_CurrentPos++;
            SkipWhiteSpace();

            if (m_JsonToParse[m_CurrentPos] != '}' && m_JsonToParse[m_CurrentPos] != ']')
            {
                // parse collection items
                for (; ; )
                {
                    var name = string.Empty;
                    if (!isArray)
                    {
                        name = ParseName();
                    }

                    var obj = ParseSomethingWithoutName();

                    if (obj == null)
                    {
                        throw new Exception();
                    }

                    // add name to item, if object.
                    if (!isArray)
                    {
                        obj.Name = name;
                    }

                    result.Add(obj);

                    SkipWhiteSpace();

                    if (m_JsonToParse[m_CurrentPos] == ',')
                    {
                        m_CurrentPos++;

                        SkipWhiteSpace();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            SkipWhiteSpace();

            if (isArray)
            {
                if (m_JsonToParse[m_CurrentPos] != ']')
                {
                    throw new FormatException();
                }
            }
            else
            {
                if (m_JsonToParse[m_CurrentPos] != '}')
                {
                    throw new FormatException();
                }
            }

            m_CurrentPos++;

            return result;
        }

        private string ParseName()
        {
            if (IsEos)
            {
                throw new FormatException("Cannot find object item'm_JsonToParse name.");
            }

            if (m_JsonToParse[m_CurrentPos] != '"')
            {
                throw new FormatException();
            }

            // skip open quote
            m_CurrentPos++;

            var name = new StringBuilder();

            for (; ; m_CurrentPos++)
            {
                if (IsEos)
                {
                    throw new FormatException();
                }

                if (m_JsonToParse[m_CurrentPos] == '"')
                {
                    if (m_JsonToParse[m_CurrentPos - 1] != '\\')
                    {
                        break;
                    }
                }

                name.Append(m_JsonToParse[m_CurrentPos]);
            }
            m_CurrentPos++;

            SkipWhiteSpace();
            if (IsEos)
            {
                throw new FormatException();
            }
            if (m_JsonToParse[m_CurrentPos] != ':')
            {
                throw new FormatException();
            }
            m_CurrentPos++;
            return name.ToString();
        }
    }
}