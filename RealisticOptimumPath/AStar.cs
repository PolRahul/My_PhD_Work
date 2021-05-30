using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RealisticOptimumPath
{
    class AStar
    {
        private string Heuristic;

        private List<Vertex> OpenList = new List<Vertex>();
        private List<Vertex> ClosedList = new List<Vertex>();
        private Vertex Goalcorner;

        private ArrayList cornersMap = new ArrayList();
        private List<Vertex> reference = new List<Vertex>();

        private int[,] gridMap;

        private VertextPoint point;
        private Size size;

        public AStar(int[,] grid, String heuristic)
        {
            gridMap = grid;
            Heuristic = heuristic;
            point = new VertextPoint();

            size = new Size(grid.GetUpperBound(0), grid.GetUpperBound(1));
        }

        public IList<Vertex> AddToCloseList(Vertex node)
        {
            bool inClosed;
            int indexOfNode;
            double ourFval;

            ClosedList.Add(node);

            int indexToRemove = OpenList.FindIndex(item => item == node);

            if (indexToRemove > -1)
            {
                OpenList.RemoveAt(indexToRemove);
            }

            IList<Vertex> reference = node.getSuccessors();
            IList<Vertex> results = new List<Vertex>();

            for (int i = 0, len = reference.Count; i < len; i++)
            {
                Vertex corner = reference[i];
                inClosed = false;
                IList<Vertex> ref1 = this.ClosedList;
                for (int j = 0, len1 = ref1.Count; j < len1; j++)
                {
                    Vertex inListNode = ref1[j];
                    if (inListNode.x == corner.x)
                    {
                        if (inListNode.y == corner.y)
                        {
                            inClosed = true;
                            break;
                        }
                    }
                }
                if (inClosed)
                {
                    continue;
                }
                indexOfNode = -1;
                int num = 0;
                IList<Vertex> ref2 = this.OpenList;
                for (int k = 0, len2 = ref2.Count; k < len2; k++)
                {
                    Vertex inListNode = ref2[k];
                    if (inListNode.x == corner.x)
                    {
                        if (inListNode.y == corner.y)
                        {
                            indexOfNode = num;
                            break;
                        }
                    }
                    num += 1;
                }
                if (indexOfNode < 0)
                {
                    //Not found in Open node
                    corner.g = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)));

                    if (Heuristic.Equals("EuclideanDistance"))
                    {
                        corner.h = Helpers.EuclideanDistance(corner, this.Goalcorner);
                    }
                    else if (Heuristic.Equals("OctileDistance"))
                    {
                        corner.h = Helpers.OctileDistance(corner, this.Goalcorner);
                    }
                    else
                    {
                        corner.h = Helpers.ManhattanDistance(corner, this.Goalcorner);
                    }

                    corner.f = corner.g + corner.h;
                    corner.Parent = node;

                    OpenList.Add(corner);

                    results.Add(corner);
                }
                else
                {
                    //Found in open node
                    if (Heuristic.Equals("EuclideanDistance"))
                    {
                        ourFval = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y))) +
                        Helpers.EuclideanDistance(corner, this.Goalcorner);
                    }
                    else if (Heuristic.Equals("OctileDistance"))
                    {
                        ourFval = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)))
                        + Helpers.OctileDistance(corner, this.Goalcorner);
                    }
                    else
                    {
                        ourFval = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)))
                        + Helpers.ManhattanDistance(corner, this.Goalcorner);
                    }

                    if (ourFval < this.OpenList[indexOfNode].f)
                    {

                        indexToRemove = OpenList.FindIndex(item => item == node);

                        if (indexToRemove > -1)
                        {
                            OpenList.RemoveAt(indexToRemove);
                        }

                        corner.g = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)));
                        if (Heuristic.Equals("EuclideanDistance"))
                        {
                            corner.h = Helpers.EuclideanDistance(corner, this.Goalcorner);
                        }
                        else if (Heuristic.Equals("OctileDistance"))
                        {
                            corner.h = Helpers.OctileDistance(corner, this.Goalcorner);
                        }
                        else
                        {
                            corner.h = Helpers.ManhattanDistance(corner, this.Goalcorner);
                        }
                        corner.f = corner.g + corner.h;
                        corner.Parent = node;

                        OpenList.Add(corner);
                        results.Add(corner);
                    }
                    else
                    {
                        results.Add(null);
                    }
                }
            }

            return results;
        }

        public List<Vertex> Search(Vertex startVertex, Vertex goalVertex)
        {
            Vertex curr, currNode = null, node;
            bool reachedGoal;
            double minFval;

            List<Vertex> path = new List<Vertex>();

            Goalcorner = goalVertex;

            if (startVertex == goalVertex)
            {
                Console.WriteLine("Start is Goal");
                return path;
            }

            startVertex.g = 0;
            if (Heuristic.Equals("EuclideanDistance"))
            {
                startVertex.h = Helpers.EuclideanDistance(startVertex, goalVertex);
                startVertex.f = Helpers.EuclideanDistance(startVertex, goalVertex);
            }
            else if (Heuristic.Equals("OctileDistance"))
            {
                startVertex.h = Helpers.OctileDistance(startVertex, goalVertex);
                startVertex.f = Helpers.OctileDistance(startVertex, goalVertex);
            }
            else
            {
                startVertex.h = Helpers.ManhattanDistance(startVertex, goalVertex);
                startVertex.f = Helpers.ManhattanDistance(startVertex, goalVertex);
            }

            AddToCloseList(startVertex);
            reachedGoal = false;

            while (OpenList.Count > 0)
            {
                minFval = 9999999999;
                currNode = null;
                reference = OpenList;

                for (int i = 0, len = reference.Count; i < len; i++)
                {
                    node = reference[i];

                    if (node.f <= minFval)
                    {
                        minFval = node.f;
                        currNode = node;
                    }
                }
                if (currNode != null)
                {
                    AddToCloseList(currNode);

                    if (currNode.x == goalVertex.x && currNode.y == goalVertex.y
                        && currNode.CornerType.Equals(goalVertex.CornerType))
                    {
                        //Console.WriteLine("Reached goal!");
                        reachedGoal = true;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Open list empty");
                    break;
                }
            }

            if (reachedGoal)
            {
                curr = currNode;
                while (curr != startVertex)
                {
                    path.Add(curr);
                    curr = curr.Parent;
                }
                path.Add(startVertex);
            }

            path.Reverse();

            return path;
        }
    }
}
