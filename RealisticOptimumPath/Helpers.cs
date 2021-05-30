using System;
using System.Collections.Generic;
using System.Linq;

namespace RealisticOptimumPath
{
    static class Helpers
    {
        public static double Square(double num)
        {
            return num * num;
        }

        public static double EuclideanDistance(Vertex startVertex, Vertex goalVertex)
        {
            return Math.Sqrt(Square(startVertex.x - goalVertex.x) + Square(startVertex.y - goalVertex.y));
        }

        public static double ManhattanDistance(Vertex startVertex, Vertex goalVertex)
        {
            return Math.Abs(goalVertex.x - startVertex.x) + Math.Abs(goalVertex.y - startVertex.y);
        }

        public static double OctileDistance(Vertex startVertex, Vertex goalVertex)
        {
            double dx = Math.Abs(goalVertex.x - startVertex.x);
            double dy = Math.Abs(goalVertex.y - startVertex.y);

            return dx + dy + (1 - Math.Sqrt(2)) * (Math.Min(dx, dy));
        }

        public static double GetPathCost(IList<Vertex> path)
        {
            double cost = 0.0;
           for(int i=0; i<path.Count - 1; i++)
            {
                cost += EuclideanDistance(path.ElementAt(i), path.ElementAt(i + 1));
            }
            

            return cost;
        }

        public static void DisplayGrid(int[,] grid)
        {           
            int rows = grid.GetUpperBound(0) + 1;
            int cols = grid.GetUpperBound(1) + 1;

            Console.WriteLine("Grid Size:{0},{1}{2}", rows, cols, Environment.NewLine);

            for (int r = 0; r < rows; r++)
            {
                for(int c = 0; c < cols; c++)
                {
                    Console.Write("{0} ",grid[r, c]);
                }
                Console.WriteLine("");
            }
        }

    }
}
