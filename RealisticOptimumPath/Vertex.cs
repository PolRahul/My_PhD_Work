using System.Collections.Generic;

namespace RealisticOptimumPath
{
    class Vertex
    {
        public string CornerType;
        public string Type;
        public int x;
        public int y;
        public double g = 9999999999;
        public double h = 9999999999;
        public double f = 9999999999;

        public Vertex Parent;

        public Vertex North;
        public Vertex NorthWest;
        public Vertex NorthEast;
        public Vertex East;
        public Vertex West;
        public Vertex South;
        public Vertex SouthWest;
        public Vertex SouthEast;

        public Vertex(int x, int y,string cornerType)
        {
            this.x = x;
            this.y = y;
            CornerType = cornerType;
        }

        public IList<Vertex> getSuccessors()
        {
            IList<Vertex> successors = new List<Vertex>();
            if (North != null)
            {
                successors.Add(North);
            }
            if (NorthWest != null)
            {
                successors.Add(NorthWest);
            }
            if (NorthEast != null)
            {
                successors.Add(NorthEast);
            }
            if (West != null)
            {
                successors.Add(this.West);
            }
            if (East != null)
            {
                successors.Add(East);
            }
            if (SouthWest != null)
            {
                successors.Add(SouthWest);
            }
            if (SouthEast != null)
            {
                successors.Add(SouthEast);
            }
            if (South != null)
            {
                successors.Add(South);
            }
            return successors;
        }

        public override string ToString()
        {
            //return string.Format("(x:{0}, y:{1})", y, x);
            return string.Format("x:{0}, y:{1}", x, y);
        }
    }
}
