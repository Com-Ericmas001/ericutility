using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;

namespace Com.Ericmas001.Net.JSON
{
    /// <summary>
    /// Implements a collection object.
    /// Object and Array collections derives from this class.
    /// </summary>
    public abstract class JsonCollection : JsonObject, IList<JsonObject>
    {
        private readonly List<JsonObject> m_List = new List<JsonObject>();

        /// <summary>
        /// Initializes a new instance of the JsonCollection class.
        /// </summary>
        public JsonCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the JsonCollection class.
        /// </summary>
        /// <param name="name">Collection's name,
        /// used then current collection is nested in other json object.</param>
        public JsonCollection(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the JsonCollection class.
        /// </summary>
        /// <param name="collection">Collection of nested objects.</param>
        public JsonCollection(IEnumerable<JsonObject> collection)
        {
            m_List.AddRange(collection);
        }

        /// <summary>
        /// Initializes a new instance of the JsonCollection class.
        /// </summary>
        /// <param name="name">Collection's name,
        /// used then current collection is nested in other json object.</param>
        /// <param name="collection">Collection of nested objects.</param>
        public JsonCollection(string name, IEnumerable<JsonObject> collection)
        {
            Name = name;
            m_List.AddRange(collection);
        }

        /// <summary>
        /// Begin collection marker. '[' for array, '{' for objects.
        /// </summary>
        protected abstract char BeginCollection { get; }

        /// <summary>
        /// End collection marker. ']' for array, '}' for objects.
        /// </summary>
        protected abstract char EndCollection { get; }

        /// <summary>
        /// Writes current JsonCollection in json data format to TextWriter.
        /// </summary>
        /// <param name="writer">Textwriter where to write current JsonCollection.</param>
        public override void WriteTo(TextWriter writer)
        {
            if (Name != string.Empty)
            {
                writer.Write(JsonUtility.QUOTE);
                writer.Write(Name);
                writer.Write(JsonUtility.QUOTE);
                writer.Write(JsonUtility.NAME_SEPARATOR);
                JsonUtility.WriteSpace(writer);
            }

            writer.Write(BeginCollection);

            JsonUtility.WriteLine(writer);
            JsonUtility.IndentDepth++;
            JsonUtility.WriteIndent(writer);

            for (var i = 0; i < Count; i++)
            {
                if (i > 0)
                {
                    writer.Write(JsonUtility.VALUE_SEPARATOR);
                    JsonUtility.WriteLine(writer);
                    JsonUtility.WriteIndent(writer);
                }

                this[i].WriteTo(writer);
            }

            JsonUtility.WriteLine(writer);
            JsonUtility.IndentDepth--;
            JsonUtility.WriteIndent(writer);

            writer.Write(EndCollection);
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override object GetValue()
        {
            return m_List;
        }

        #region IList<JsonObject> Members

        public int IndexOf(JsonObject item)
        {
            return m_List.IndexOf(item);
        }

        public void Insert(int index, JsonObject item)
        {
            m_List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            m_List.RemoveAt(index);
        }

        public JsonObject this[int index]
        {
            get
            {
                return m_List[index];
            }
            set
            {
                m_List[index] = value;
            }
        }

        #endregion IList<JsonObject> Members

        #region ICollection<JsonObject> Members

        public void Add(JsonObject item)
        {
            m_List.Add(item);
        }

        public void Clear()
        {
            m_List.Clear();
        }

        public bool Contains(JsonObject item)
        {
            return m_List.Contains(item);
        }

        public void CopyTo(JsonObject[] array, int arrayIndex)
        {
            m_List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return m_List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(JsonObject item)
        {
            return m_List.Remove(item);
        }

        #endregion ICollection<JsonObject> Members

        #region IEnumerable<JsonObject> Members

        public IEnumerator<JsonObject> GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        #endregion IEnumerable<JsonObject> Members

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_List.GetEnumerator();
        }

        #endregion IEnumerable Members
    }
}