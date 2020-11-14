using System.Drawing;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace HexGrid
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
            this.widthSelect = new System.Windows.Forms.NumericUpDown();
            this.heightSelect = new System.Windows.Forms.NumericUpDown();
            this.updateButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.colorBorder = new HexGrid.ColorPickerButton();
            this.trackBarBorderColor = new HexGrid.FloatTrackBar();
            this.colorCenter = new HexGrid.ColorPickerButton();
            this.trackBarCenterColor = new HexGrid.FloatTrackBar();
            this.trackBarStroke = new HexGrid.FloatTrackBar();
            this.trackBarLineCnt = new HexGrid.FloatTrackBar();
            this.trackBarTwist = new HexGrid.FloatTrackBar();
            this.trackBarRotate = new HexGrid.FloatTrackBar();
            this.trackBarCenter = new HexGrid.FloatTrackBar();
            this.trackBarScale = new HexGrid.FloatTrackBar();
            this.trackBarYOffset = new HexGrid.FloatTrackBar();
            this.trackBarXOffset = new HexGrid.FloatTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.widthSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSelect)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenterColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineCnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTwist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // widthSelect
            // 
            this.widthSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.widthSelect.ForeColor = System.Drawing.Color.White;
            this.widthSelect.Location = new System.Drawing.Point(12, 12);
            this.widthSelect.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.widthSelect.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.widthSelect.Name = "widthSelect";
            this.widthSelect.Size = new System.Drawing.Size(120, 20);
            this.widthSelect.TabIndex = 1;
            this.widthSelect.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.widthSelect.ValueChanged += new System.EventHandler(this.widthSelect_ValueChanged);
            // 
            // heightSelect
            // 
            this.heightSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.heightSelect.ForeColor = System.Drawing.Color.White;
            this.heightSelect.Location = new System.Drawing.Point(140, 12);
            this.heightSelect.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.heightSelect.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.heightSelect.Name = "heightSelect";
            this.heightSelect.Size = new System.Drawing.Size(120, 20);
            this.heightSelect.TabIndex = 2;
            this.heightSelect.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.heightSelect.ValueChanged += new System.EventHandler(this.heightSelect_ValueChanged);
            // 
            // updateButton
            // 
            this.updateButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.updateButton.Location = new System.Drawing.Point(265, 12);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(75, 23);
            this.updateButton.TabIndex = 12;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(265, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 512);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 512);
            this.panel2.TabIndex = 0;
            this.panel2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(58, 66);
            this.panel3.Margin = new System.Windows.Forms.Padding(8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(319, 317);
            this.panel3.TabIndex = 1;
            this.panel3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDoubleClick);
            // 
            // saveButton
            // 
            this.saveButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.saveButton.Location = new System.Drawing.Point(346, 13);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(56, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "offset X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(176, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "offset Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(113, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "hex size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(102, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "center size";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(113, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "rotation";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(118, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "twist";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(102, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "line count";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(102, 281);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "line width";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(118, 311);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "center color";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(113, 345);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 27;
            this.label10.Text = "border color";
            // 
            // colorBorder
            // 
            this.colorBorder.AutoAddColors = false;
            this.colorBorder.CellSize = new System.Drawing.Size(16, 16);
            this.colorBorder.Color = System.Drawing.Color.White;
            this.colorBorder.Columns = 1;
            this.colorBorder.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorBorder.Location = new System.Drawing.Point(14, 325);
            this.colorBorder.Name = "colorBorder";
            this.colorBorder.ShowCustomColors = false;
            this.colorBorder.Size = new System.Drawing.Size(26, 26);
            this.colorBorder.TabIndex = 16;
            // 
            // trackBarBorderColor
            // 
            this.trackBarBorderColor.LargeChange = 50000D;
            this.trackBarBorderColor.Location = new System.Drawing.Point(46, 325);
            this.trackBarBorderColor.Maximum = 0D;
            this.trackBarBorderColor.Minimum = 0D;
            this.trackBarBorderColor.Name = "trackBarBorderColor";
            this.trackBarBorderColor.Precision = 1D;
            this.trackBarBorderColor.Size = new System.Drawing.Size(213, 45);
            this.trackBarBorderColor.SmallChange = 1D;
            this.trackBarBorderColor.TabIndex = 11;
            this.trackBarBorderColor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarBorderColor.Value = 0D;
            // 
            // colorCenter
            // 
            this.colorCenter.AutoAddColors = false;
            this.colorCenter.CellSize = new System.Drawing.Size(16, 16);
            this.colorCenter.Columns = 1;
            this.colorCenter.EditMode = Cyotek.Windows.Forms.ColorEditingMode.None;
            this.colorCenter.Location = new System.Drawing.Point(14, 297);
            this.colorCenter.Name = "colorCenter";
            this.colorCenter.ShowCustomColors = false;
            this.colorCenter.Size = new System.Drawing.Size(26, 26);
            this.colorCenter.TabIndex = 15;
            // 
            // trackBarCenterColor
            // 
            this.trackBarCenterColor.LargeChange = 50000D;
            this.trackBarCenterColor.Location = new System.Drawing.Point(46, 297);
            this.trackBarCenterColor.Maximum = 0D;
            this.trackBarCenterColor.Minimum = 0D;
            this.trackBarCenterColor.Name = "trackBarCenterColor";
            this.trackBarCenterColor.Precision = 1D;
            this.trackBarCenterColor.Size = new System.Drawing.Size(213, 45);
            this.trackBarCenterColor.SmallChange = 1D;
            this.trackBarCenterColor.TabIndex = 10;
            this.trackBarCenterColor.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarCenterColor.Value = 0D;
            // 
            // trackBarStroke
            // 
            this.trackBarStroke.LargeChange = 50000D;
            this.trackBarStroke.Location = new System.Drawing.Point(11, 263);
            this.trackBarStroke.Maximum = 0D;
            this.trackBarStroke.Minimum = 0D;
            this.trackBarStroke.Name = "trackBarStroke";
            this.trackBarStroke.Precision = 1D;
            this.trackBarStroke.Size = new System.Drawing.Size(248, 45);
            this.trackBarStroke.SmallChange = 1D;
            this.trackBarStroke.TabIndex = 17;
            this.trackBarStroke.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarStroke.Value = 0D;
            // 
            // trackBarLineCnt
            // 
            this.trackBarLineCnt.LargeChange = 50000D;
            this.trackBarLineCnt.Location = new System.Drawing.Point(12, 230);
            this.trackBarLineCnt.Maximum = 0D;
            this.trackBarLineCnt.Minimum = 0D;
            this.trackBarLineCnt.Name = "trackBarLineCnt";
            this.trackBarLineCnt.Precision = 1D;
            this.trackBarLineCnt.Size = new System.Drawing.Size(248, 45);
            this.trackBarLineCnt.SmallChange = 1D;
            this.trackBarLineCnt.TabIndex = 9;
            this.trackBarLineCnt.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarLineCnt.Value = 0D;
            // 
            // trackBarTwist
            // 
            this.trackBarTwist.LargeChange = 50000D;
            this.trackBarTwist.Location = new System.Drawing.Point(12, 192);
            this.trackBarTwist.Maximum = 0D;
            this.trackBarTwist.Minimum = 0D;
            this.trackBarTwist.Name = "trackBarTwist";
            this.trackBarTwist.Precision = 1D;
            this.trackBarTwist.Size = new System.Drawing.Size(248, 45);
            this.trackBarTwist.SmallChange = 1D;
            this.trackBarTwist.TabIndex = 8;
            this.trackBarTwist.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarTwist.Value = 0D;
            // 
            // trackBarRotate
            // 
            this.trackBarRotate.LargeChange = 50000D;
            this.trackBarRotate.Location = new System.Drawing.Point(12, 154);
            this.trackBarRotate.Maximum = 0D;
            this.trackBarRotate.Minimum = 0D;
            this.trackBarRotate.Name = "trackBarRotate";
            this.trackBarRotate.Precision = 1D;
            this.trackBarRotate.Size = new System.Drawing.Size(248, 45);
            this.trackBarRotate.SmallChange = 1D;
            this.trackBarRotate.TabIndex = 7;
            this.trackBarRotate.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarRotate.Value = 0D;
            // 
            // trackBarCenter
            // 
            this.trackBarCenter.LargeChange = 50000D;
            this.trackBarCenter.Location = new System.Drawing.Point(12, 116);
            this.trackBarCenter.Maximum = 0D;
            this.trackBarCenter.Minimum = 0D;
            this.trackBarCenter.Name = "trackBarCenter";
            this.trackBarCenter.Precision = 1D;
            this.trackBarCenter.Size = new System.Drawing.Size(248, 45);
            this.trackBarCenter.SmallChange = 1D;
            this.trackBarCenter.TabIndex = 6;
            this.trackBarCenter.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarCenter.Value = 0D;
            // 
            // trackBarScale
            // 
            this.trackBarScale.LargeChange = 50000D;
            this.trackBarScale.Location = new System.Drawing.Point(12, 78);
            this.trackBarScale.Maximum = 0D;
            this.trackBarScale.Minimum = 0D;
            this.trackBarScale.Name = "trackBarScale";
            this.trackBarScale.Precision = 1D;
            this.trackBarScale.Size = new System.Drawing.Size(248, 45);
            this.trackBarScale.SmallChange = 1D;
            this.trackBarScale.TabIndex = 5;
            this.trackBarScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarScale.Value = 0D;
            // 
            // trackBarYOffset
            // 
            this.trackBarYOffset.LargeChange = 50000D;
            this.trackBarYOffset.Location = new System.Drawing.Point(140, 40);
            this.trackBarYOffset.Maximum = 0D;
            this.trackBarYOffset.Minimum = 0D;
            this.trackBarYOffset.Name = "trackBarYOffset";
            this.trackBarYOffset.Precision = 1D;
            this.trackBarYOffset.Size = new System.Drawing.Size(120, 45);
            this.trackBarYOffset.SmallChange = 1D;
            this.trackBarYOffset.TabIndex = 4;
            this.trackBarYOffset.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarYOffset.Value = 0D;
            // 
            // trackBarXOffset
            // 
            this.trackBarXOffset.LargeChange = 50000D;
            this.trackBarXOffset.Location = new System.Drawing.Point(12, 40);
            this.trackBarXOffset.Maximum = 0D;
            this.trackBarXOffset.Minimum = 0D;
            this.trackBarXOffset.Name = "trackBarXOffset";
            this.trackBarXOffset.Precision = 1D;
            this.trackBarXOffset.Size = new System.Drawing.Size(120, 45);
            this.trackBarXOffset.SmallChange = 1D;
            this.trackBarXOffset.TabIndex = 3;
            this.trackBarXOffset.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarXOffset.Value = 0D;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(21)))), ((int)(((byte)(21)))));
            this.ClientSize = new System.Drawing.Size(797, 573);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorBorder);
            this.Controls.Add(this.trackBarBorderColor);
            this.Controls.Add(this.colorCenter);
            this.Controls.Add(this.trackBarCenterColor);
            this.Controls.Add(this.trackBarStroke);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.trackBarLineCnt);
            this.Controls.Add(this.trackBarTwist);
            this.Controls.Add(this.trackBarRotate);
            this.Controls.Add(this.trackBarCenter);
            this.Controls.Add(this.trackBarScale);
            this.Controls.Add(this.trackBarYOffset);
            this.Controls.Add(this.trackBarXOffset);
            this.Controls.Add(this.heightSelect);
            this.Controls.Add(this.widthSelect);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.widthSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightSelect)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenterColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarStroke)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLineCnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarTwist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarXOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown widthSelect;
        private System.Windows.Forms.NumericUpDown heightSelect;
        private FloatTrackBar trackBarXOffset;
        private FloatTrackBar trackBarYOffset;
        private FloatTrackBar trackBarScale;
        private FloatTrackBar trackBarCenter;
        private FloatTrackBar trackBarRotate;
        private FloatTrackBar trackBarTwist;
        private FloatTrackBar trackBarLineCnt;
        private FloatTrackBar trackBarCenterColor;
        private FloatTrackBar trackBarBorderColor;
        private FloatTrackBar trackBarStroke;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Button saveButton;
        private ColorPickerButton colorCenter;
        private ColorPickerButton colorBorder;
        private Panel panel3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
    }
}

