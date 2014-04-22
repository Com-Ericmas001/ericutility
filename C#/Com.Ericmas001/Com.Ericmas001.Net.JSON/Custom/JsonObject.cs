﻿using System.IO;

namespace Com.Ericmas001.Net.JSON.Custom
{
    /// <summary>
    /// Common object for all JSON data structures.
    /// </summary>
    public abstract class JsonObject
    {
        private string m_Name;

        /// <summary>
        /// Name of json object.
        /// <example>
        /// {
        ///     "name": value
        /// }
        /// </example>
        /// </summary>
        public string Name
        {
            set
            {
                if (value == null)
                {
                    m_Name = string.Empty;
                }
                else
                {
                    m_Name = value.Trim();
                }
            }
            get
            {
                if (m_Name == null)
                {
                    return string.Empty;
                }
                return m_Name;
            }
        }

        public abstract object GetValue();

        /// <summary>
        /// Returns string that represents object is json format.
        /// </summary>
        /// <returns>String that represents object is json format.</returns>
        public override string ToString()
        {
            var sw = new StringWriter();
            WriteTo(sw);
            return sw.ToString();
        }

        /// <summary>
        /// Compares two json objects.
        /// </summary>
        /// <param name="obj">Other object to compare.</param>
        /// <returns>true, if objects are equal; otherwise false.</returns>
        public abstract override bool Equals(object obj);

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current JsonObject.</returns>
        public abstract override int GetHashCode();

        /// <summary>
        /// Writes current JsonObject in json data format to TextWriter.
        /// </summary>
        /// <param name="writer">Textwriter where to write current JsonObject.</param>
        public abstract void WriteTo(TextWriter writer);
    }
}