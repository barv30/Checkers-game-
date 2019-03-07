using System;
using System.Collections.Generic;
using System.Text;

namespace B18_Ex02
{
    public struct Point
    {
        private int m_x;
        private int m_y;

        public Point(int i_x, int i_y)
        {
            m_x = i_x;
            m_y = i_y;
        }

        public int X
        {
            get { return m_x; }
            set { m_x = value; }
        }

        public int Y
        {
            get { return m_y; }
            set { m_y = value; }
        }
    }
}
