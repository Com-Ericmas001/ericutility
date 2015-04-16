﻿namespace Com.Ericmas001.Util
{
    public class BitUtility
    {
        /// <summary>
        /// Compte le nombre de bits allumés
        /// </summary>
        /// <param name="n">Le nombre</param>
        /// <returns>Le nombre de bits</returns>
        public static int CountBits(int n)
        {
            int count;
            for (count = 0; n != 0; count++)
                n &= (n - 1);
            return count;
        }
    }
}