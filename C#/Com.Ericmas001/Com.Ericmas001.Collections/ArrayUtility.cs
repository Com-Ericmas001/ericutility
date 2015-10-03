using Com.Ericmas001.Portable.Util;
using System;
using System.Collections.Generic;

namespace Com.Ericmas001.Collections
{
    public class ArrayUtility
    {
        /// <summary>
        /// Choisis un élément dans la liste parmis tous les minimums !!
        /// </summary>
        /// <typeparam name="T">Le type d'objet du Array</typeparam>
        /// <param name="values">L'array</param>
        /// <returns>La position de la valeur choisie dans le array</returns>
        public static int GetRandomMin<T>(T[] values) where T : IComparable<T>
        {
            var min = values[0];
            var possibles = new List<int>() { 0 };
            for (var i = 1; i < 16; ++i)
            {
                if (values[i].CompareTo(min) < 0)
                {
                    min = values[i];
                    possibles.Clear();
                }

                if (values[i].CompareTo(min) == 0)
                    possibles.Add(i);
            }
            return possibles[Hasard.RandomWithMax(possibles.Count - 1)];
        }

        /// <summary>
        /// Choisis un élément dans la liste parmis tous les maximums
        /// </summary>
        /// <typeparam name="T">Le type d'objet du Array</typeparam>
        /// <param name="values">L'array</param>
        /// <returns>La position de la valeur choisie dans le array</returns>
        public static int GetRandomMax<T>(T[] values) where T : IComparable<T>
        {
            var max = values[0];
            var possibles = new List<int>() { 0 };
            for (var i = 1; i < 16; ++i)
            {
                if (values[i].CompareTo(max) > 0)
                {
                    max = values[i];
                    possibles.Clear();
                }

                if (values[i].CompareTo(max) == 0)
                    possibles.Add(i);
            }
            return possibles[Hasard.RandomWithMax(possibles.Count - 1)];
        }
    }
}