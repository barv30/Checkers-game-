using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public struct SoliderTypes
    {
        private const char k_Empty = ' ';
        private const char k_OSolider = 'O';
        private const char k_XSolider = 'X';
        private const char k_KingX = 'K';
        private const char k_KingO = 'U';

        public static char OSolider
        {
            get { return k_OSolider; }
        }

        public static char XSolider
        {
            get { return k_XSolider; }
        }

        public static char KingO
        {
            get { return k_KingO; }
        }

        public static char KingX
        {
            get { return k_KingX; }
        }

        public static char Empty
        {
            get { return k_Empty; }
        }
    }
}
