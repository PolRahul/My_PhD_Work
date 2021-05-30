using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace RealisticOptimumPath
{
    enum HeuristicType { }

    class Corner
    {
        public IList<Vertex> Corners;
        public IList<Vertex> Vertexts;

        public Corner()
        {
            Corners = new List<Vertex>();
            Vertexts = new List<Vertex>();
        }
    }

    class Size
    {
        public int row;
        public int column;

        public Size(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }
    class RealisticOptimumPath
    {
        private string Heuristic;

        private List<Vertex> OpenList = new List<Vertex>();
        private List<Vertex> ClosedList = new List<Vertex>();
        private Vertex Goalcorner;

        private ArrayList cornersMap = new ArrayList();
        private int[,] gridMap;

        private VertextPoint point;
        private Size size;

        public RealisticOptimumPath(int[,] grid, String heuristic)
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
                //node and corner

                if (indexOfNode < 0)
                {
                    //Not found in Open
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

                    if (node.Parent != null)
                    {
                        if (LineOfSight(node.Parent, corner))
                        {
                            corner.g = node.Parent.g + Math.Sqrt(((corner.x - node.Parent.x) * (corner.x - node.Parent.x)) + ((corner.y - node.Parent.y) * (corner.y - node.Parent.y)));
                            corner.f = corner.g + corner.h;
                            corner.Parent = node.Parent;

                        }
                        else
                        {
                            corner.f = corner.g + corner.h;
                            corner.Parent = node;
                        }
                    }
                    else
                    {
                        corner.f = corner.g + corner.h;
                        corner.Parent = node;
                    }

                    OpenList.Add(corner);
                    results.Add(corner);
                }
                else
                {
                    //Found in Open
                    if (Heuristic.Equals("EuclideanDistance"))
                    {
                        ourFval = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)))
                        + Helpers.EuclideanDistance(corner, this.Goalcorner);
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

                    if (node.Parent != null)
                    {
                        if (LineOfSight(node.Parent, corner))
                        {
                            //ourFval = node.parent.gVal + EuclideanDistance(node.parent, corner);
                            if (this.Heuristic.Equals("EuclideanDistance"))
                            {
                                ourFval = node.Parent.g + Math.Sqrt(((corner.x - node.Parent.x) * (corner.x - node.Parent.x)) + ((corner.y - node.Parent.y) * (corner.y - node.Parent.y)))
                                + Helpers.EuclideanDistance(corner, this.Goalcorner);
                            }
                            else if (this.Heuristic.Equals("OctileDistance"))
                            {
                                ourFval = node.Parent.g + Math.Sqrt(((corner.x - node.Parent.x) * (corner.x - node.Parent.x)) + ((corner.y - node.Parent.y) * (corner.y - node.Parent.y)))
                                + Helpers.OctileDistance(corner, this.Goalcorner);
                            }
                            else
                            {
                                ourFval = node.Parent.g + Math.Sqrt(((corner.x - node.Parent.x) * (corner.x - node.Parent.x)) + ((corner.y - node.Parent.y) * (corner.y - node.Parent.y)))
                                + Helpers.ManhattanDistance(corner, this.Goalcorner);
                            }
                        }
                    }
                    if (ourFval < this.OpenList[indexOfNode].f)
                    {
                        OpenList.RemoveAt(indexOfNode);
                        corner.g = node.g + Math.Sqrt(((corner.x - node.x) * (corner.x - node.x)) + ((corner.y - node.y) * (corner.y - node.y)));
                        if (node.Parent != null)
                        {
                            if (Heuristic.Equals("EuclideanDistance"))
                            {
                                corner.h = Helpers.EuclideanDistance(corner, this.Goalcorner);
                            }
                            else if (this.Heuristic.Equals("OctileDistance"))
                            {
                                corner.h = Helpers.OctileDistance(corner, this.Goalcorner);
                            }
                            else
                            {
                                corner.h = Helpers.ManhattanDistance(corner, this.Goalcorner);
                            }

                            if (LineOfSight(node.Parent, corner))
                            {
                                corner.g = node.Parent.g + Math.Sqrt(((corner.x - node.Parent.x) * (corner.x - node.Parent.x)) + ((corner.y - node.Parent.y) * (corner.y - node.Parent.y)));
                                corner.f = corner.g + corner.h;
                                corner.Parent = node.Parent;

                            }
                            else
                            {
                                corner.f = corner.g + corner.h;
                                corner.Parent = node;
                            }
                        }
                        else
                        {
                            corner.f = corner.g + corner.h;
                            corner.Parent = node;
                        }
                        OpenList.Add(corner);
                        results.Add(corner);
                    }
                    else
                    {
                        //results.push(void 0);
                        results.Add(null);
                    }
                }

            }
            return results;
        }

        public IList<Vertex> SmoothPath(List<Vertex> path)
        {
            Vertex corner;
            IList<Vertex> smoothedPath = new List<Vertex>();

            if (path == null || path.Count == 0)
            {
                return path;
            }

            smoothedPath.Add(path[0]);

            for (int i = 0, len = path.Count; i < len; i++)
            {
                corner = path[i];
                Vertex prevCorner = null;
                if (i > 0 && path.ElementAt(i - 1) != null)
                {
                    prevCorner = path[i - 1];
                }
                if (!(LineOfSight(smoothedPath[smoothedPath.Count - 1], corner)))
                {
                    smoothedPath.Add(prevCorner);
                }
            }
            smoothedPath.Add(path[path.Count - 1]);
            return smoothedPath;
        }

        public List<Vertex> Search()
        {
            return Search(point.Start, point.Goal);
        }

        public VertextPoint GetVertexPoint()
        {
            return point;
        }

        public List<Vertex> Search(Vertex startVertex, Vertex goalVertex)
        {
            Vertex curr, currNode = null, node;
            bool reachedGoal;
            double minFval;
            List<Vertex> path = new List<Vertex>();
            //IDictionary<int, List<Vertex>> reference = new Dictionary<int, List<Vertex>>();
            List<Vertex> reference = new List<Vertex>();

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

            //Console.WriteLine("OpenList Count {0}", OpenList.Count);

            while (OpenList.Count > 0)
            {
                minFval = 9999999999;
                currNode = null;
                reference = OpenList;

                //int openListLength = OpenList.Count;
                //OpenList[openListLength] = new List<Vertex>();
                for (int i = 0, len = reference.Count; i < len; i++)
                {
                    node = reference[i];

                    //openList
                    /*var nodeInfo = [];
                    nodeInfo[0] = node.x;
                    nodeInfo[1] = node.y;
                    var openParent = [];
                    openParent[0] = node.Parent.x;
                    openParent[1] = node.Parent.y;
                    nodeInfo[2] = openParent; 
                    OpenList[openListLength].Add(nodeInfo); */
                    //openlist visualization 

                    if (node.f <= minFval)
                    {
                        minFval = node.f;
                        currNode = node;
                    }
                }

                if (currNode != null)
                {
                    AddToCloseList(currNode);
                    //var tem = $scope.tempPath2.length;
                    //  $scope.tempPath2[tem] = [];
                    //  $scope.tempPath2[tem][0] = [];
                    //  $scope.tempPath2[tem][1] = [];
                    //  $scope.tempPath2[tem][0][0] = currNode.x;
                    //  $scope.tempPath2[tem][0][1] = currNode.y;
                    //  $scope.tempPath2[tem][1][0] = currNode.parent.x;
                    //  $scope.tempPath2[tem][1][1] = currNode.parent.y;
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
                while (curr.x != startVertex.x || curr.y != startVertex.y
                    || !curr.CornerType.Equals(startVertex.CornerType))
                {

                    path.Add(curr);
                    //   var tem = $scope.tempPath2.length;
                    curr = curr.Parent;

                }
                path.Add(startVertex);
            }

            path.Reverse();
            //SmoothPath(path);

            return path;
        }

        public bool IsTraversable(int x, int y)
        {
            if (x < 0 || y < 0) return false;

            if (x > 19 || y > 19) return false;

            return !(gridMap[y, x] == 1);
        }

        public bool LineOfSight(Vertex startCorner, Vertex goalCorner)
        {
            int x1 = startCorner.x, x2 = goalCorner.x;
            int y1 = startCorner.y, y2 = goalCorner.y;

            int dy = y2 - y1, dx = x2 - x1;
            int f = 0, sy = 0, sx = 0;

            if (dy < 0)
            {
                dy = -dy;
                sy = -1;
            }
            else
            {
                sy = 1;
            }

            if (dx < 0)
            {
                dx = -dx;
                sx = -1;
            }
            else
            {
                sx = 1;
            }

            if (dx >= dy)
            {
                while (x1 != x2)
                {
                    f = f + dy;
                    if (f >= dx)
                    {
                        if (!IsTraversable(x1 + ((sx - 1) / 2), y1 + (sy - 1) / 2))
                        {
                            return false;
                        }

                        y1 = y1 + sy;
                        f = f - dx;
                    }
                    if (f != 0 && !IsTraversable(x1 + ((sx - 1) / 2), y1 + (sy - 1) / 2))
                    {
                        return false;
                    }
                    if (dy == 0 && !IsTraversable(x1 + ((sx - 1) / 2), y1) && !IsTraversable(x1 + ((sx - 1) / 2), y1 - 1))
                    {
                        return false;
                    }
                    x1 = x1 + sx;
                }
            }
            else
            {
                while (y1 != y2)
                {
                    f = f + dx;
                    if (f >= dy)
                    {
                        if (!IsTraversable(x1 + ((sx - 1) / 2), y1 + (sy - 1) / 2))
                        {
                            return false;
                        }
                        x1 = x1 + sx;
                        f = f - dy;
                    }
                    if (f != 0 && !IsTraversable(x1 + ((sx - 1) / 2), y1 + (sy - 1) / 2))
                    {
                        return false;
                    }
                    if (dy == 0 && !IsTraversable(x1, y1 + ((sy - 1) / 2)) && !IsTraversable(x1 - 1, y1 + ((sy - 1) / 2)))
                    {
                        return false;
                    }
                    y1 = y1 + sy;
                }
            }
            return true;
        }

        public void PrepareMap()
        {
            IList<Vertex> corners = new List<Vertex>();

            //ArrayList cornersMap = new ArrayList();

            for (var i = 0; i < size.row + 1; i++)
            {
                cornersMap.Add(new ArrayList());
                //cornersMap[i] = new ArrayList();

                for (var j = 0; j < size.column + 1; j++)
                {
                    ((ArrayList)cornersMap[i]).Add(new ArrayList());
                }
            }

            //for (var i = 0; i < $scope.size.row; i++){

            //    for (var j = 0; j < $scope.size.column; j++){
            //$scope.cell[i][j].corners = [];
            //    }
            //}

            for (var i = 0; i < size.row + 1; i++)
            {
                for (var j = 0; j < size.column + 1; j++)
                {
                    int[][] temp = CheckCase(i, j);

                    if (temp == null)
                    {
                        temp = new int[2][];
                        temp[0] = new int[1] { 1 };
                    }

                    //if normal corner
                    if (temp[0][0] == 0)
                    {
                        for (var k = 0; k < temp[1].Length; k++)
                        {
                            if (temp[1][k] == 1)
                            {
                                Vertex cornerObj = null;

                                if (k == 0)
                                {
                                    //northwest cell
                                    //cornerObj = new Vertex(j, i, "northWest");
                                    cornerObj = new Vertex(j, i, "normal");
                                    // $scope.cell[i - 1][j - 1].corners[$scope.cell[i - 1][j - 1].corners.length] = cornerObj;
                                }
                                if (k == 1)
                                {
                                    //northeast cell
                                    //cornerObj = new Vertex(j, i, "northEast");
                                    cornerObj = new Vertex(j, i, "normal");
                                    //$scope.cell[i - 1][j].corners[$scope.cell[i - 1][j].corners.length] = cornerObj;
                                }
                                if (k == 2)
                                {
                                    //southeast cell
                                    //cornerObj = new Vertex(j, i, "southEast");
                                    cornerObj = new Vertex(j, i, "normal");
                                    //$scope.cell[i][j].corners[$scope.cell[i][j].corners.length] = cornerObj;
                                }
                                if (k == 3)
                                {
                                    //southwest cell
                                    //cornerObj = new Vertex(j, i, "southWest");
                                    cornerObj = new Vertex(j, i, "normal");
                                    //$scope.cell[i][j - 1].corners[$scope.cell[i][j - 1].corners.length] = cornerObj;
                                }

                                ((ArrayList)((ArrayList)cornersMap[i])[j]).Add(cornerObj);
                                corners.Add(cornerObj);
                            }
                        }
                    }

                    //if one corner
                    if (temp[0][0] == 1)
                    {
                        for (var k = 0; k < temp[1].Length; k++)
                        {
                            if (temp[1][k] == 1)
                            {
                                Vertex cornerObj = null;
                                if (k == 0)
                                {
                                    //northwest cell
                                    cornerObj = new Vertex(j, i, "southEast");
                                    //$scope.cell[i - 1][j - 1].corners[$scope.cell[i - 1][j - 1].corners.length] = cornerObj;
                                }
                                if (k == 1)
                                {
                                    //northeast cell
                                    cornerObj = new Vertex(j, i, "southWest");
                                    //$scope.cell[i - 1][j].corners[$scope.cell[i - 1][j].corners.length] = cornerObj;
                                }
                                if (k == 2)
                                {
                                    //southeast cell
                                    cornerObj = new Vertex(j, i, "northWest");
                                    //$scope.cell[i][j].corners[$scope.cell[i][j].corners.length] = cornerObj;
                                }
                                if (k == 3)
                                {
                                    //southwest cell
                                    cornerObj = new Vertex(j, i, "northEast");
                                    //$scope.cell[i][j - 1].corners[$scope.cell[i][j - 1].corners.length] = cornerObj;
                                }

                                ((ArrayList)((ArrayList)cornersMap[i])[j]).Add(cornerObj);
                                corners.Add(cornerObj);
                            }
                        }
                    }

                    // two corners special case
                    if (temp[0][0] == 2)
                    {
                        for (int k = 0; k < temp[1].Length; k++)
                        {
                            if (temp[1][k] == 1)
                            {
                                Vertex cornerObj = null;

                                if (k == 0)
                                {
                                    //northwest cell
                                    cornerObj = new Vertex(j, i, "southEast");

                                    //$scope.cell[i - 1][j - 1].corners[$scope.cell[i - 1][j - 1].corners.length] = cornerObj;
                                }
                                if (k == 1)
                                {
                                    //northeast cell
                                    cornerObj = new Vertex(j, i, "southWest");
                                    //$scope.cell[i - 1][j].corners[$scope.cell[i - 1][j].corners.length] = cornerObj;
                                }
                                if (k == 2)
                                {
                                    //southeast cell
                                    cornerObj = new Vertex(j, i, "northWest");
                                    //$scope.cell[i][j].corners[$scope.cell[i][j].corners.length] = cornerObj;
                                }
                                if (k == 3)
                                {
                                    //southwest cell
                                    cornerObj = new Vertex(j, i, "northEast");
                                    //$scope.cell[i][j - 1].corners[$scope.cell[i][j - 1].corners.length] = cornerObj;
                                }
                                ((ArrayList)((ArrayList)cornersMap[i])[j]).Add(cornerObj);
                                corners.Add(cornerObj);
                            }
                        }
                    }

                }
            }

            //load each corner's neighborhoods
            for (int i = 0; i < size.row; i++)
            {
                for (int j = 0; j < size.column; j++)
                {
                    for (int o = 0; o < ((ArrayList)((ArrayList)cornersMap[i])[j]).Count; o++)
                    {
                        Vertex corner = ((ArrayList)((ArrayList)cornersMap[i])[j])[o] as Vertex;
                        try
                        {
                            CheckNeighborhood(corner, cornersMap);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }

            //var holder = "";
            //for (int j = 0; j < 20; j++)
            //{
            //    for (int k = 0; k < 30; k++)
            //    {
            //        if ($scope.cell[j][k].isObstacle){
            //    holder += "[" + j + "," + k + "],\n";
            //}

            //return true;
        }
        public void PrepareStartGoal(string type, string cornerType, int row, int column)
        {
            point.StartX = column;
            point.StartY = row;

            if (type.Equals("start"))
            {
                point.StartType = cornerType;
            }
            else
            {
                point.GoalType = cornerType;

            }

            if (gridMap[row, column] == 0)
            { //not isObstacle
                if (cornerType.Equals("northWest"))
                {
                    ArrayList vertexes = ((ArrayList)((ArrayList)cornersMap[row])[column]);
                    for (int i = 0; i < vertexes.Count; i++)
                    {
                        Vertex vertex = vertexes[i] as Vertex;
                        vertex.Type = cornerType;
                        if (!string.IsNullOrEmpty(vertex.Type))
                        {
                            if (type == "start")
                            {
                                point.Start = vertex;
                            }
                            else
                            {
                                point.Goal = vertex;
                            }
                            break;
                        }
                    }

                }
                else if (cornerType.Equals("northEast"))
                {
                    ArrayList vertexes = ((ArrayList)((ArrayList)cornersMap[row])[column + 1]);
                    for (int i = 0; i < vertexes.Count; i++)
                    {
                        Vertex vertex = vertexes[i] as Vertex;
                        vertex.Type = cornerType;
                        if (!string.IsNullOrEmpty(vertex.Type))
                        {
                            if (type == "start")
                            {
                                point.Start = vertex;
                            }
                            else
                            {
                                point.Goal = vertex;
                            }
                            break;
                        }
                    }
                }
                else if (cornerType.Equals("southWest"))
                {
                    ArrayList vertexes = ((ArrayList)((ArrayList)cornersMap[row + 1])[column]);
                    for (int i = 0; i < vertexes.Count; i++)
                    {
                        Vertex vertex = vertexes[i] as Vertex;
                        vertex.Type = cornerType;

                        if (!string.IsNullOrEmpty(vertex.Type))
                        {
                            if (type == "start")
                            {
                                point.Start = vertex;
                            }
                            else
                            {
                                point.Goal = vertex;
                            }
                            break;
                        }
                    }
                }
                else if (cornerType.Equals("southEast"))
                {
                    ArrayList vertexes = ((ArrayList)((ArrayList)cornersMap[row + 1])[column + 1]);
                    for (var i = 0; i < vertexes.Count; i++)
                    {
                        Vertex vertex = vertexes[i] as Vertex;
                        vertex.Type = cornerType;

                        if (!string.IsNullOrEmpty(vertex.Type))
                        {
                            if (type == "start")
                            {
                                point.Start = vertex;
                            }
                            else
                            {
                                point.Goal = vertex;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public int[][] CheckCase(int y, int x)
        {

            bool northWest = false;
            bool northEast = false;
            bool southWest = false;
            bool southEast = false;

            int[][] result = new int[2][];

            //check northWest Block
            if (x == 0 || y == 0)
            {
                northWest = true;
            }
            else
            {
                if (gridMap[y - 1, x - 1] == 1)
                {
                    northWest = true;
                }
            }

            //check northEast Block
            if (y == 0 || x == 20) //TODO: Need to check for const val 30
            {
                northEast = true;
            }
            else
            {
                if (gridMap[y - 1, x] == 1)
                {
                    northEast = true;
                }
            }

            //check southWest Block
            if (x == 0 || y == 20) //TODO: Need to check for const val 20
            {
                southWest = true;
            }
            else
            {
                if (gridMap[y, x - 1] == 1)
                {
                    southWest = true;
                }
            }
            //check southEast Block
            if (x == 20 || y == 20)
            {
                southEast = true;
            }
            else
            {
                if (gridMap[y, x] == 1)
                {
                    southEast = true;
                }
            }

            if (!northWest && !northEast && !southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 1, 1, 1 };
                return result; // all way normal
            }

            //three way normal
            if (northWest && !northEast && !southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 0, 1, 1, 1 };
                return result; //except northwest
            }

            if (!northWest && northEast && !southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 0, 1, 1 };
                return result; //except northeast
            }
            if (!northWest && !northEast && southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 1, 1, 0 };
                return result; //except southwest
            }
            if (!northWest && !northEast && !southWest && southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 1, 0, 1 };
                return result; //except southeast
            }


            //two way normal
            if (northWest && northEast && !southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 0, 0, 1, 1 };
                return result; //except northwest and east
            }
            if (!northWest && northEast && !southWest && southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 0, 0, 1 };
                return result; //except northeast and southeast
            }
            if (!northWest && !northEast && southWest && southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 1, 1, 0, 0 };
                return result; //except southwest and southeast
            }

            if (northWest && !northEast && southWest && !southEast)
            {
                result[0] = new int[1] { 0 };
                result[1] = new int[4] { 0, 1, 1, 0 };
                return result; //except southwest and northwest
            }

            //one corner
            if (northWest && northEast && southWest && !southEast)
            {
                result[0] = new int[1] { 1 };
                result[1] = new int[4] { 0, 0, 1, 0 };
                return result;
            }
            if (northWest && northEast && !southWest && southEast)
            {
                result[0] = new int[1] { 1 };
                result[1] = new int[4] { 0, 0, 0, 1 };
                return result;
            }
            if (!northWest && northEast && southWest && southEast)
            {
                result[0] = new int[1] { 1 };
                result[1] = new int[4] { 1, 0, 0, 0 };
                return result;
            }
            if (northWest && !northEast && southWest && southEast)
            {
                result[0] = new int[1] { 1 };
                result[1] = new int[4] { 0, 1, 0, 0 };
                return result;
            }


            //two corners
            if (!northWest && northEast && southWest && !southEast)
            {
                result[0] = new int[1] { 2 };
                result[1] = new int[4] { 1, 0, 1, 0 };
                return result;
            }
            if (northWest && !northEast && !southWest && southEast)
            {
                result[0] = new int[1] { 2 };
                result[1] = new int[4] { 0, 1, 0, 1 };
                return result;
            }
            return null;
        }

        //public int[][] CheckCase(int y, int x)
        //{

        //    bool northWest = false;
        //    bool northEast = false;
        //    bool southWest = false;
        //    bool southEast = false;

        //    int[][] result = new int[2][];

        //    //check northWest Block
        //    if (x == 0 || y == 0)
        //    {
        //        northWest = true;
        //    }
        //    else
        //    {
        //        if (gridMap[y - 1, x - 1] == 1)
        //        {
        //            northWest = true;
        //        }
        //    }

        //    //check northEast Block
        //    if (y == 0 || x == 19) //TODO: Need to check for const val 30
        //    {
        //        northEast = true;
        //    }
        //    else
        //    {
        //        if (gridMap[y - 1, x] == 1)
        //        {
        //            northEast = true;
        //        }
        //    }

        //    //check southWest Block
        //    if (x == 0 || y == 19) //TODO: Need to check for const val 20
        //    {
        //        southWest = true;
        //    }
        //    else
        //    {
        //        if (gridMap[y, x - 1] == 1)
        //        {
        //            southWest = true;
        //        }
        //    }
        //    //check southEast Block
        //    if (x == 19 || y == 19)
        //    {
        //        southEast = true;
        //    }
        //    else
        //    {
        //        if (gridMap[y, x] == 1)
        //        {
        //            southEast = true;
        //        }
        //    }

        //    if (!northWest && !northEast && !southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 1, 1, 1 };
        //        return result; // all way normal
        //    }

        //    //three way normal
        //    if (northWest && !northEast && !southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 0, 1, 1, 1 };
        //        return result; //except northwest
        //    }

        //    if (!northWest && northEast && !southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 0, 1, 1 };
        //        return result; //except northeast
        //    }
        //    if (!northWest && !northEast && southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 1, 0, 1 };
        //        return result; //except southwest
        //    }
        //    if (!northWest && !northEast && !southWest && southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 1, 1, 0 };
        //        return result; //except southeast
        //    }


        //    //two way normal
        //    if (northWest && northEast && !southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 0, 0, 1, 1 };
        //        return result; //except northwest and east
        //    }
        //    if (!northWest && northEast && !southWest && southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 0, 1, 0 };
        //        return result; //except northeast and southeast
        //    }
        //    if (!northWest && !northEast && southWest && southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 1, 1, 0, 0 };
        //        return result; //except southwest and southeast
        //    }

        //    if (northWest && !northEast && southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 0 };
        //        result[1] = new int[4] { 0, 1, 0, 1 };
        //        return result; //except southwest and northwest
        //    }

        //    //one corner
        //    if (northWest && northEast && southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 1 };
        //        result[1] = new int[4] { 0, 0, 0, 1 };
        //        return result;
        //    }
        //    if (northWest && northEast && !southWest && southEast)
        //    {
        //        result[0] = new int[1] { 1 };
        //        result[1] = new int[4] { 0, 0, 1, 0 };
        //        return result;
        //    }
        //    if (!northWest && northEast && southWest && southEast)
        //    {
        //        result[0] = new int[1] { 1 };
        //        result[1] = new int[4] { 1, 0, 0, 0 };
        //        return result;
        //    }
        //    if (northWest && !northEast && southWest && southEast)
        //    {
        //        result[0] = new int[1] { 1 };
        //        result[1] = new int[4] { 0, 1, 0, 0 };
        //        return result;
        //    }


        //    //two corners
        //    if (!northWest && northEast && southWest && !southEast)
        //    {
        //        result[0] = new int[1] { 2 };
        //        result[1] = new int[4] { 1, 0, 0, 1 };
        //        return result;
        //    }
        //    if (northWest && !northEast && !southWest && southEast)
        //    {
        //        result[0] = new int[1] { 2 };
        //        result[1] = new int[4] { 0, 1, 1, 0 };
        //        return result;
        //    }
        //    return null;
        //}

        public void CheckNeighborhood(Vertex corner, ArrayList cornersMap)
        {
            int x = corner.x;
            int y = corner.y;
            ArrayList vertexes;
            //check not normal corner cases
            if (corner.CornerType.Equals("northWest"))
            {
                //south
                vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.South = vertex;
                    }
                }

                //east
                vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x + 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.East = vertex;
                    }
                }
                //southeast
                vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x + 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.SouthEast = vertex;
                    }
                }
            }
            else if (corner.CornerType.Equals("northEast"))
            {
                //west
                vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x - 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.West = vertex;
                    }
                }
                //south
                vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.South = vertex;
                    }
                }
                //southwest
                vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x - 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.SouthWest = vertex;
                    }
                }
            }
            else if (corner.CornerType.Equals("southEast"))
            {
                //north
                vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.North = vertex;
                    }
                }
                //northWest
                vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x - 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.NorthWest = vertex;
                    }
                }
                //west
                vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x - 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.West = vertex;
                    }
                }

            }
            else if (corner.CornerType.Equals("southWest"))
            {
                //north
                vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("normal"))
                    {
                        corner.NorthWest = vertex;
                    }
                }
                //northEast
                vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x + 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.NorthEast = vertex;
                    }
                }
                //east
                vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x + 1]);
                for (int k = 0; k < vertexes.Count; k++)
                {
                    Vertex vertex = vertexes[k] as Vertex;
                    if (vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("normal"))
                    {
                        corner.East = vertex;
                    }
                }
            }
            else if (corner.CornerType.Equals("normal"))
            {
                bool northWest = false;
                bool northEast = false;
                bool southWest = false;
                bool southEast = false;

                //check northWest Block
                if (x == 0 || y == 0)
                {
                    northWest = true;
                }
                else
                {
                    //if ($scope.cell[y - 1][x - 1].isObstacle){
                    if (gridMap[y - 1, x - 1] == 1)
                    {
                        northWest = true;
                    }
                }
                //check northEast Block
                if (y == 0 || x == 20)
                {
                    northEast = true;
                }
                else
                {
                    //if ($scope.cell[y - 1][x].isObstacle){
                    if (gridMap[y - 1, x] == 1)
                    {
                        northEast = true;
                    }
                }
                //check southWest Block
                if (x == 0 || y == 20)
                {
                    southWest = true;
                }
                else
                {
                    //if ($scope.cell[y][x - 1].isObstacle){
                    if (gridMap[y, x - 1] == 1)
                    {
                        southWest = true;
                    }
                }
                //check southEast Block
                if (x == 20 || y == 20)
                {
                    southEast = true;
                }
                else
                {
                    //if ($scope.cell[y][x].isObstacle){
                    if (gridMap[y, x] == 1)
                    {
                        southEast = true;
                    }
                }

                bool addNorthWest = false;
                bool addNorth = false;
                bool addNorthEast = false;
                bool addEast = false;
                bool addSouthEast = false;
                bool addSouth = false;
                bool addSouthWest = false;
                bool addWest = false;

                /*if (x == 7 && y == 2)
                {
                    Console.WriteLine("here");
                }*/

                //all way case
                if (!northWest && !northEast && !southWest && !southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }
                //three way normal
                if (northWest && !northEast && !southWest && !southEast)
                {
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }
                if (!northWest && northEast && !southWest && !southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }
                if (!northWest && !northEast && southWest && !southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                    addWest = true;
                }
                if (!northWest && !northEast && !southWest && southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }


                //two way normal
                if (northWest && northEast && !southWest && !southEast)
                {
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }
                if (!northWest && northEast && !southWest && southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addSouth = true;
                    addSouthWest = true;
                    addWest = true;
                }
                if (!northWest && !northEast && southWest && southEast)
                {
                    addNorthWest = true;
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addWest = true;
                }
                if (northWest && !northEast && southWest && !southEast)
                {
                    addNorth = true;
                    addNorthEast = true;
                    addEast = true;
                    addSouthEast = true;
                    addSouth = true;
                }


                if (addNorthWest)
                {
                    //add northWest
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x - 1]);
                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("normal"))
                        {
                            corner.NorthWest = vertex;
                        }
                    }
                }
                if (addNorth)
                {
                    //add north
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x]);
                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("normal"))
                        {
                            corner.North = vertex;
                        }
                    }
                }
                if (addNorthEast)
                {
                    //add northEast
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y - 1])[x + 1]);
                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("normal"))
                        {
                            corner.NorthEast = vertex;
                        }
                    }
                }
                if (addEast)
                {
                    //add east
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x + 1]);
                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("northEast") || vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("normal"))
                        {
                            corner.East = vertex;
                        }
                    }
                }
                if (addSouthEast)
                {
                    //add southEast
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x + 1]);
                    for (int k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("normal"))
                        {
                            corner.SouthEast = vertex;
                        }
                    }
                }
                if (addSouth)
                {
                    //add south
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x]);
                    for (var k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("southEast") || vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("normal"))
                        {
                            corner.South = vertex;
                        }
                    }
                }
                if (addSouthWest)
                {
                    //add southWest
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y + 1])[x - 1]);
                    for (var k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("normal"))
                        {
                            corner.SouthWest = vertex;
                        }
                    }
                }
                if (addWest)
                {
                    //add west
                    vertexes = ((ArrayList)((ArrayList)cornersMap[y])[x - 1]);
                    for (var k = 0; k < vertexes.Count; k++)
                    {
                        Vertex vertex = vertexes[k] as Vertex;
                        if (vertex.CornerType.Equals("southWest") || vertex.CornerType.Equals("northWest") || vertex.CornerType.Equals("normal"))
                        {
                            corner.West = vertex;
                        }
                    }
                }
            }
        }

        public IList<Point> GetROP(IList<Vertex> foundPath, int delta, int cellSize)
        {
            IList<Point> ropPath = new List<Point>();

            ropPath.Add(new Point(foundPath[0].x * cellSize, foundPath[0].y * cellSize));

            foreach (Vertex vertext in foundPath)
            {
                if (vertext.Parent == null)
                {
                    continue;
                }

                int xdir = vertext.x - vertext.Parent.x < 0 ? -1 : 1;
                int ydir = vertext.y - vertext.Parent.y < 0 ? -1 : 1;

                if (vertext.SouthWest == null)
                {
                    if (xdir == 1)
                    {
                        if (vertext.West != null)
                        {
                            ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize - delta));
                        }

                        ropPath.Add(new Point(vertext.x * cellSize + delta, vertext.y * cellSize));

                    }
                    else
                    {
                        ropPath.Add(new Point(vertext.x * cellSize + delta, vertext.y * cellSize));
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize - delta));
                    }
                }
                else if (vertext.NorthEast == null)
                {
                    if (xdir == 1)
                    {
                        ropPath.Add(new Point(vertext.x * cellSize - delta, vertext.y * cellSize));

                        if (vertext.East != null)
                        {
                            ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize + delta));
                        }
                    }
                    else
                    {
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize + delta));
                        ropPath.Add(new Point(vertext.x * cellSize - delta, vertext.y * cellSize));
                    }
                }
                else if (vertext.NorthWest == null)
                {
                    if (xdir == 1)
                    {
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize + delta));
                        ropPath.Add(new Point(vertext.x * cellSize + delta, vertext.y * cellSize));
                    }
                    else
                    {
                        ropPath.Add(new Point(vertext.x * cellSize + delta, vertext.y * cellSize));
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize + delta));
                    }

                }
                else if (vertext.SouthEast == null)
                {
                    if (xdir == 1)
                    {
                        ropPath.Add(new Point(vertext.x * cellSize - delta, vertext.y * cellSize));
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize - delta));
                    }
                    else
                    {
                        ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize - delta));
                        ropPath.Add(new Point(vertext.x * cellSize - delta, vertext.y * cellSize));
                    }
                }
                else
                {
                    ropPath.Add(new Point(vertext.x * cellSize, vertext.y * cellSize));
                }

            }


            //ropPath.Add(new Point(foundPath[foundPath.Count - 1].x * cellSize, foundPath[foundPath.Count - 1].y * cellSize));

            return ropPath;
        }

        public IList<Vertex> GetVertexListFromPoints(IList<Point> points)
        {
            IList<Vertex> vertexList = new List<Vertex>();

            foreach (Point point in points)
            {
                vertexList.Add(new Vertex(point.X,point.Y,""));
            }

            return vertexList;
        }

    }
}
