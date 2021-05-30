using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RealisticOptimumPath
{
    public partial class Form1 : Form
    {
        private const int CELL_SIZE = 30;
        private const int NUM_OF_CELLS = 20;
        private const int OFFSET = 20;

        private int startX = 1, startY = 1;
        private int goalX = 7, goalY = 7;

        private IList<Vertex> solutionPath;
        private IList<Point> ropPath;
        private IList<Vertex> aStarPath;


        public Form1()
        {
            InitializeComponent();
        }

        private void pbGrid_Paint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);

            DrawMap(GridFactory.getGrid(comboBoxMaps.SelectedIndex), e.Graphics);

            DrawStart(startY, startX, e.Graphics);

            DrawGoal(goalY, goalX, e.Graphics);

            IList<Point> astarPathPoints = GetSolutionPath(aStarPath);
            DrawSolutionPath(e.Graphics, new Pen(Color.Gold, 3.0f), new Pen(Color.DarkOrange, 3.0f), astarPathPoints);

            IList<Point> solutionPathPoints = GetSolutionPath(solutionPath);

            DrawSolutionPath(e.Graphics, new Pen(Color.Green, 3.0f), new Pen(Color.Blue, 3.0f), solutionPathPoints);

            DrawSolutionPath(e.Graphics, new Pen(Color.Brown, 3.0f), new Pen(Color.DarkOrange, 3.0f), ropPath);
            
        }

        private void DrawGrid(Graphics g)
        {

            Pen p = new Pen(Color.Gray);
            Font drawFont = new Font("Arial", 8);

            for (int y = 0; y <= NUM_OF_CELLS; ++y)
            {
                g.DrawLine(p, OFFSET, y * CELL_SIZE + OFFSET, NUM_OF_CELLS * CELL_SIZE + OFFSET, y * CELL_SIZE + OFFSET);

                if (y < 20)
                {
                    g.DrawString(string.Format("{0}", y), drawFont, Brushes.Black, 0.0F, (y + 1) * CELL_SIZE * 1.0F);
                }
            }

            for (int x = 0; x <= NUM_OF_CELLS; ++x)
            {
                g.DrawLine(p, x * CELL_SIZE + OFFSET, OFFSET, x * CELL_SIZE + OFFSET, NUM_OF_CELLS * CELL_SIZE + OFFSET);
                if (x < 20)
                {
                    g.DrawString(string.Format("{0}", x), drawFont, Brushes.Black, (x + 1) * CELL_SIZE * 1.0F, 0.0F);
                }
            }
        }

        private void DrawMap(int[,] map, Graphics g)
        {
            Brush brush = Brushes.Black;

            for (int r = 0; r < NUM_OF_CELLS; r++)
            {
                for (int c = 0; c < NUM_OF_CELLS; c++)
                {
                    if (map[r, c] == 1)
                    {
                        g.FillRectangle(brush, c * CELL_SIZE + OFFSET, r * CELL_SIZE + OFFSET, CELL_SIZE, CELL_SIZE);                    
                    }
                }
            }
        }

        private void DrawStart(int x, int y, Graphics g)
        {
            g.FillRectangle(Brushes.Yellow, x * CELL_SIZE + OFFSET, y * CELL_SIZE + OFFSET, CELL_SIZE, CELL_SIZE);
        }

        private void DrawGoal(int x, int y, Graphics g)
        {
            g.FillRectangle(Brushes.Red, x * CELL_SIZE + OFFSET, y * CELL_SIZE + OFFSET, CELL_SIZE, CELL_SIZE);
        }

        private void DrawSolutionPath(Graphics g, Pen p, Pen dotPen, IList<Point> path)
        {
            if (path == null || path.Count == 0)
            {
                return;
            }

            for (int i = 0; i < path.Count - 1; i++)
            {
                g.DrawLine(p, path[i].X + OFFSET, path[i].Y + OFFSET, path[i + 1].X + OFFSET, path[i + 1].Y + OFFSET);

                g.DrawEllipse(dotPen, path[i].X + OFFSET - 2, path[i].Y + OFFSET - 2, 5, 5);
            }
            g.DrawEllipse(dotPen, path[path.Count - 1].X + OFFSET, path[path.Count - 1].Y + OFFSET, 5, 5);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxMaps.SelectedIndex = 0;
            comboBoxAlgorithms.SelectedIndex = 0;

            UpdateStartGoalValues();
        }

        private void UpdateStartGoalValues()
        {
            startX = int.Parse(textBoxStartX.Text.Trim());
            startY = int.Parse(textBoxStartY.Text.Trim());

            goalX = int.Parse(textBoxGoalX.Text.Trim());
            goalY = int.Parse(textBoxGoalY.Text.Trim());
        }

        private IList<Point> GetSolutionPath(IList<Vertex> paths)
        {
            IList<Point> path = new List<Point>();
            if (paths != null)
            {
                foreach (Vertex vertext in paths)
                {
                    path.Add(new Point(vertext.x * CELL_SIZE, vertext.y * CELL_SIZE));
                }
            }

            return path;
        }

        private void ShowPath()
        {
            listBoxPath.Items.Clear();

            foreach (Vertex vertext in solutionPath)
            {
                listBoxPath.Items.Add(vertext.ToString());
            }
        }

        private void ShowAStarPath()
        {
            listBoxAStar.Items.Clear();

            foreach (Vertex vertext in aStarPath)
            {
                listBoxAStar.Items.Add(vertext.ToString());
            }
        }

        private void ShowROPPath()
        {
            listBoxROP.Items.Clear();

            foreach (Point point in ropPath)
            {
                listBoxROP.Items.Add(string.Format("x:{0},y:{1}", point.X / CELL_SIZE, point.Y / CELL_SIZE));
            }
        }

        private void buttonPathPlanning_Click(object sender, EventArgs e)
        {
            UpdateStartGoalValues();

            var grid = GridFactory.getGrid(comboBoxMaps.SelectedIndex);

            RealisticOptimumPath rop = new RealisticOptimumPath(grid, "EuclideanDistance");

            rop.PrepareMap();

            rop.PrepareStartGoal("start", "southEast", startX, startY);
            //rop.PrepareStartGoal("start", "northWest", startX, startY);
            rop.PrepareStartGoal("goal", "northWest", goalX, goalY);
            //rop.PrepareStartGoal("goal", "southEast", goalX, goalY);
            //rop.PrepareStartGoal("goal", "northEast", goalX, goalY);

            AStar astart = new AStar(grid,"EuclideanDistance");
    
            VertextPoint point = rop.GetVertexPoint();

            if (point.Start == null)
            {
                MessageBox.Show("Invalid start");
            }
            else if (point.Goal == null)
            {
                MessageBox.Show("Invalid goal");
            }
            else
            {
                aStarPath = astart.Search(point.Start, point.Goal);

                solutionPath = rop.Search();

                if (solutionPath.Count > 0)
                {
                    int delta = int.Parse(textBoxPathDelta.Text);
                    ropPath = rop.GetROP(solutionPath, delta, CELL_SIZE);

                    labelStatus.Text = "Goal found!";
                    labelPathEdges.Text = string.Format("{0}", solutionPath.Count - 1);

                    double cost = Helpers.GetPathCost(solutionPath);
                    labelPathCost.Text = cost.ToString();

                    IList<Vertex> vertexList = rop.GetVertexListFromPoints(ropPath);
                    cost = Helpers.GetPathCost(vertexList) / CELL_SIZE;
                    labelPathROPCost.Text = cost.ToString();

                    cost = Helpers.GetPathCost(aStarPath);
                    labelAStarCost.Text = cost.ToString();
                     
                    ShowPath();
                    ShowAStarPath();
                    ShowROPPath();
                }
                else
                {
                    labelStatus.Text = "Path does not exist!";
                    labelPathEdges.Text = "";
                    labelPathCost.Text = "";
                    labelPathROPCost.Text = "";
                    labelAStarCost.Text = "";
                    listBoxPath.Items.Clear();
                    listBoxAStar.Items.Clear();
                    listBoxROP.Items.Clear();
                    
                    if (ropPath != null)
                    {
                        ropPath.Clear();
                    }

                    if (aStarPath != null)
                    {
                        aStarPath.Clear();
                    }
                }               

                pbGrid.Refresh();
            }
           
        }

        private void comboBoxMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (solutionPath != null)
            {                
                labelPathEdges.Text = "";
                labelPathCost.Text = "";
                labelPathROPCost.Text = "";
                labelAStarCost.Text = "";

                listBoxPath.Items.Clear();
                listBoxAStar.Items.Clear();
                listBoxROP.Items.Clear();

                solutionPath.Clear();

                if (ropPath != null)
                {
                    ropPath.Clear();
                }

                if (aStarPath != null)
                {
                    aStarPath.Clear();
                }
            }

            pbGrid.Refresh();
            
        }

    }
}
