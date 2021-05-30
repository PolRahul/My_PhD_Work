namespace RealisticOptimumPath
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbGrid = new System.Windows.Forms.PictureBox();
            this.buttonPathPlanning = new System.Windows.Forms.Button();
            this.textBoxStartY = new System.Windows.Forms.TextBox();
            this.textBoxStartX = new System.Windows.Forms.TextBox();
            this.textBoxGoalY = new System.Windows.Forms.TextBox();
            this.textBoxGoalX = new System.Windows.Forms.TextBox();
            this.comboBoxMaps = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAlgorithms = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelPathEdges = new System.Windows.Forms.Label();
            this.labelPathCost = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.listBoxPath = new System.Windows.Forms.ListBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPathDelta = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.labelPathROPCost = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelAStarCost = new System.Windows.Forms.Label();
            this.listBoxROP = new System.Windows.Forms.ListBox();
            this.listBoxAStar = new System.Windows.Forms.ListBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGrid
            // 
            this.pbGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbGrid.Location = new System.Drawing.Point(12, 12);
            this.pbGrid.Name = "pbGrid";
            this.pbGrid.Size = new System.Drawing.Size(640, 640);
            this.pbGrid.TabIndex = 0;
            this.pbGrid.TabStop = false;
            this.pbGrid.Paint += new System.Windows.Forms.PaintEventHandler(this.pbGrid_Paint);
            // 
            // buttonPathPlanning
            // 
            this.buttonPathPlanning.BackColor = System.Drawing.Color.Navy;
            this.buttonPathPlanning.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPathPlanning.ForeColor = System.Drawing.Color.White;
            this.buttonPathPlanning.Location = new System.Drawing.Point(674, 266);
            this.buttonPathPlanning.Name = "buttonPathPlanning";
            this.buttonPathPlanning.Size = new System.Drawing.Size(223, 45);
            this.buttonPathPlanning.TabIndex = 1;
            this.buttonPathPlanning.Text = "Get Path Solution";
            this.buttonPathPlanning.UseVisualStyleBackColor = false;
            this.buttonPathPlanning.Click += new System.EventHandler(this.buttonPathPlanning_Click);
            // 
            // textBoxStartY
            // 
            this.textBoxStartY.Location = new System.Drawing.Point(674, 135);
            this.textBoxStartY.Name = "textBoxStartY";
            this.textBoxStartY.Size = new System.Drawing.Size(100, 20);
            this.textBoxStartY.TabIndex = 2;
            this.textBoxStartY.Text = "0";
            // 
            // textBoxStartX
            // 
            this.textBoxStartX.Location = new System.Drawing.Point(797, 135);
            this.textBoxStartX.Name = "textBoxStartX";
            this.textBoxStartX.Size = new System.Drawing.Size(100, 20);
            this.textBoxStartX.TabIndex = 3;
            this.textBoxStartX.Text = "0";
            // 
            // textBoxGoalY
            // 
            this.textBoxGoalY.Location = new System.Drawing.Point(674, 183);
            this.textBoxGoalY.Name = "textBoxGoalY";
            this.textBoxGoalY.Size = new System.Drawing.Size(100, 20);
            this.textBoxGoalY.TabIndex = 4;
            this.textBoxGoalY.Text = "10";
            // 
            // textBoxGoalX
            // 
            this.textBoxGoalX.Location = new System.Drawing.Point(797, 183);
            this.textBoxGoalX.Name = "textBoxGoalX";
            this.textBoxGoalX.Size = new System.Drawing.Size(100, 20);
            this.textBoxGoalX.TabIndex = 5;
            this.textBoxGoalX.Text = "10";
            // 
            // comboBoxMaps
            // 
            this.comboBoxMaps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaps.FormattingEnabled = true;
            this.comboBoxMaps.Items.AddRange(new object[] {
            "Map 1",
            "Map 2",
            "Map 3",
            "Map 4",
            "Map 5"});
            this.comboBoxMaps.Location = new System.Drawing.Point(674, 30);
            this.comboBoxMaps.Name = "comboBoxMaps";
            this.comboBoxMaps.Size = new System.Drawing.Size(223, 21);
            this.comboBoxMaps.TabIndex = 6;
            this.comboBoxMaps.SelectedIndexChanged += new System.EventHandler(this.comboBoxMaps_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(671, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Select Map Type";
            // 
            // comboBoxAlgorithms
            // 
            this.comboBoxAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAlgorithms.FormattingEnabled = true;
            this.comboBoxAlgorithms.Items.AddRange(new object[] {
            "Realistic Optimum Path",
            "AStar",
            "DStar",
            "ThetaStar"});
            this.comboBoxAlgorithms.Location = new System.Drawing.Point(674, 86);
            this.comboBoxAlgorithms.Name = "comboBoxAlgorithms";
            this.comboBoxAlgorithms.Size = new System.Drawing.Size(223, 21);
            this.comboBoxAlgorithms.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(671, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Path Planning Algorithm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(671, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Start X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(794, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Start Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(671, 167);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Goal X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(794, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Goal Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(671, 336);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Status:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(671, 362);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Path Edges:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(672, 411);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Theta Cost:";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(750, 336);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(0, 13);
            this.labelStatus.TabIndex = 14;
            // 
            // labelPathEdges
            // 
            this.labelPathEdges.AutoSize = true;
            this.labelPathEdges.Location = new System.Drawing.Point(750, 362);
            this.labelPathEdges.Name = "labelPathEdges";
            this.labelPathEdges.Size = new System.Drawing.Size(0, 13);
            this.labelPathEdges.TabIndex = 14;
            // 
            // labelPathCost
            // 
            this.labelPathCost.AutoSize = true;
            this.labelPathCost.Location = new System.Drawing.Point(751, 411);
            this.labelPathCost.Name = "labelPathCost";
            this.labelPathCost.Size = new System.Drawing.Size(0, 13);
            this.labelPathCost.TabIndex = 14;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(671, 466);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "AStar Vertex:";
            // 
            // listBoxPath
            // 
            this.listBoxPath.FormattingEnabled = true;
            this.listBoxPath.Location = new System.Drawing.Point(780, 482);
            this.listBoxPath.Name = "listBoxPath";
            this.listBoxPath.Size = new System.Drawing.Size(100, 160);
            this.listBoxPath.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(671, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Path Delta";
            // 
            // textBoxPathDelta
            // 
            this.textBoxPathDelta.Location = new System.Drawing.Point(674, 228);
            this.textBoxPathDelta.Name = "textBoxPathDelta";
            this.textBoxPathDelta.Size = new System.Drawing.Size(223, 20);
            this.textBoxPathDelta.TabIndex = 17;
            this.textBoxPathDelta.Text = "10";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(672, 436);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "ROP Cost:";
            // 
            // labelPathROPCost
            // 
            this.labelPathROPCost.AutoSize = true;
            this.labelPathROPCost.Location = new System.Drawing.Point(751, 436);
            this.labelPathROPCost.Name = "labelPathROPCost";
            this.labelPathROPCost.Size = new System.Drawing.Size(0, 13);
            this.labelPathROPCost.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(673, 387);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 13);
            this.label13.TabIndex = 20;
            this.label13.Text = "A-Star Cost:";
            // 
            // labelAStarCost
            // 
            this.labelAStarCost.AutoSize = true;
            this.labelAStarCost.Location = new System.Drawing.Point(751, 387);
            this.labelAStarCost.Name = "labelAStarCost";
            this.labelAStarCost.Size = new System.Drawing.Size(0, 13);
            this.labelAStarCost.TabIndex = 21;
            // 
            // listBoxROP
            // 
            this.listBoxROP.FormattingEnabled = true;
            this.listBoxROP.Location = new System.Drawing.Point(886, 482);
            this.listBoxROP.Name = "listBoxROP";
            this.listBoxROP.Size = new System.Drawing.Size(100, 160);
            this.listBoxROP.TabIndex = 15;
            // 
            // listBoxAStar
            // 
            this.listBoxAStar.FormattingEnabled = true;
            this.listBoxAStar.Location = new System.Drawing.Point(674, 482);
            this.listBoxAStar.Name = "listBoxAStar";
            this.listBoxAStar.Size = new System.Drawing.Size(100, 160);
            this.listBoxAStar.TabIndex = 15;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(883, 466);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "ROP Vertex:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(777, 466);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 14;
            this.label15.Text = "Theta Vertex:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 654);
            this.Controls.Add(this.labelAStarCost);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.labelPathROPCost);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.textBoxPathDelta);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.listBoxROP);
            this.Controls.Add(this.listBoxAStar);
            this.Controls.Add(this.listBoxPath);
            this.Controls.Add(this.labelPathCost);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelPathEdges);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxAlgorithms);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMaps);
            this.Controls.Add(this.textBoxGoalX);
            this.Controls.Add(this.textBoxGoalY);
            this.Controls.Add(this.textBoxStartX);
            this.Controls.Add(this.textBoxStartY);
            this.Controls.Add(this.buttonPathPlanning);
            this.Controls.Add(this.pbGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Optimum Path Planning";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGrid;
        private System.Windows.Forms.Button buttonPathPlanning;
        private System.Windows.Forms.TextBox textBoxStartY;
        private System.Windows.Forms.TextBox textBoxStartX;
        private System.Windows.Forms.TextBox textBoxGoalY;
        private System.Windows.Forms.TextBox textBoxGoalX;
        private System.Windows.Forms.ComboBox comboBoxMaps;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxAlgorithms;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelPathEdges;
        private System.Windows.Forms.Label labelPathCost;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBoxPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxPathDelta;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelPathROPCost;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelAStarCost;
        private System.Windows.Forms.ListBox listBoxROP;
        private System.Windows.Forms.ListBox listBoxAStar;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
    }
}

