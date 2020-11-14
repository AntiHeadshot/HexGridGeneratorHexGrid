using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexGrid
{
    public partial class Form1 : Form
    {
        private readonly HexGridRenderer _grid;

        private readonly List<(ColorPickerButton button, ColorPos cp)> _gradients = new List<(ColorPickerButton button, ColorPos cp)>();

        public Form1()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                                                         | BindingFlags.Instance | BindingFlags.NonPublic, null,
                panel3, new object[] { true });

            _grid = new HexGridRenderer();

            trackBarLineCnt.Precision = 0.01;
            trackBarLineCnt.Minimum = 0;
            trackBarLineCnt.Maximum = Math.Log(1024 * 8.0);
            trackBarLineCnt.Value = Math.Log(_grid.LayerCnt);
            trackBarLineCnt.ValueChanged += TrackBarLineCnt_ValueChanged;

            trackBarStroke.Precision = 0.002;
            trackBarStroke.Minimum = Math.Log(0.001);
            trackBarStroke.Maximum = Math.Log(2);
            trackBarStroke.Value = Math.Log(_grid.StrokeW);
            trackBarStroke.ValueChanged += TrackBarStrokeOnValueChanged;

            trackBarBorderColor.Precision = 0.01;
            trackBarBorderColor.Minimum = 0;
            trackBarBorderColor.Maximum = 1;
            trackBarBorderColor.Value = _grid.ColorBorderStrength;
            trackBarBorderColor.ValueChanged += TrackBarBorderColorOnValueChanged;

            trackBarCenterColor.Precision = 0.01;
            trackBarCenterColor.Minimum = 0;
            trackBarCenterColor.Maximum = 1;
            trackBarCenterColor.Value = _grid.ColorCenterStrength;
            trackBarCenterColor.ValueChanged += TrackBarCenterColorOnValueChanged;

            trackBarRotate.Precision = 0.5;
            trackBarRotate.Minimum = -60;
            trackBarRotate.Maximum = 0;
            trackBarRotate.Value = _grid.Rotation;
            trackBarRotate.ValueChanged += TrackBarRotateOnValueChanged;

            trackBarCenter.Precision = 1;
            trackBarCenter.Minimum = 0;
            trackBarCenter.Maximum = 200;
            trackBarCenter.Value = _grid.MinHexS;
            trackBarCenter.ValueChanged += TrackBarCenterOnValueChanged;

            trackBarScale.Precision = 1;
            trackBarScale.Minimum = 50;
            trackBarScale.Maximum = 400;
            trackBarScale.Value = _grid.HexS;
            trackBarScale.ValueChanged += TrackBarScaleOnValueChanged;

            trackBarTwist.Precision = 0.5;
            trackBarTwist.Minimum = -180;
            trackBarTwist.Maximum = 180;
            trackBarTwist.Value = _grid.Twist;
            trackBarTwist.ValueChanged += TrackBarTwistOnValueChanged;

            trackBarXOffset.Precision = 1;
            trackBarXOffset.Minimum = 0;
            trackBarXOffset.Maximum = 400;
            trackBarXOffset.Value = _grid.OffsetX;
            trackBarXOffset.ValueChanged += TrackBarXOffsetOnValueChanged;

            trackBarYOffset.Precision = 1;
            trackBarYOffset.Minimum = 0;
            trackBarYOffset.Maximum = 400;
            trackBarYOffset.Value = _grid.OffsetY;
            trackBarYOffset.ValueChanged += TrackBarYOffsetOnValueChanged;

            colorCenter.Color = _grid.ColorCenter;
            colorCenter.ColorChanged += ColorCenter_ColorChanged;
            colorBorder.Color = _grid.ColorBorder;
            colorBorder.ColorChanged += ColorBorderOnColorChanged;

            widthSelect.Value = _grid.Width;
            widthSelect.ValueChanged += WidthSelectOnValueChanged;
            heightSelect.Value = _grid.Height;
            heightSelect.ValueChanged += HeightSelectOnValueChanged;

            _grid.OnRender += x =>
            {
                Invoke((MethodInvoker)delegate
               {
                   Rectangle pos = GetVisibleRectangle(panel1, panel2);

                   panel3.Width = pos.Width;
                   panel3.Height = pos.Height;
                   panel3.Left = pos.Left;
                   panel3.Top = pos.Top;

                   panel3.BackgroundImage = x.Bitmap;
               });
            };

            _grid.ColorAdded += ColorAdded;
            _grid.ColorRemoved += ColorRemoved;

            _grid.Colors.Add(new ColorPos(Color.HotPink, new Vector(100, 100)));
            _grid.Colors.Add(new ColorPos(Color.Coral, new Vector(500, 500)));
            _grid.Colors.Add(new ColorPos(Color.Aqua, new Vector(200, 100)));

            UpdateImage();

            panel1.Resize += (s, w) => { UpdateImage(); };
            panel1.Scroll += (s, e) => { UpdateImage(); };
        }

        private void HeightSelectOnValueChanged(object sender, EventArgs e)
        {
            panel2.Height = (int)heightSelect.Value;
            _grid.Height = (int)heightSelect.Value;
        }

        private void WidthSelectOnValueChanged(object sender, EventArgs e)
        {
            panel2.Width = (int)widthSelect.Value;
            _grid.Width = (int)widthSelect.Value;
        }

        private Dictionary<ColorPos, ColorPickerButton> _buttons = new Dictionary<ColorPos, ColorPickerButton>();

        private void ColorRemoved(ColorPos hgr)
        {
            _buttons[hgr].Dispose();
            _buttons.Remove(hgr);
            UpdateImage();
        }

        private void ColorAdded(ColorPos cp)
        {
            ColorPickerButton btn = new ColorPickerButton
            {
                Color = cp.Color,
            };
            _buttons.Add(cp, btn);
            btn.Left = (int)cp.Pos.X - btn.Width / 2 - panel1.HorizontalScroll.Value;
            btn.Top = (int)cp.Pos.Y - btn.Height / 2 - panel1.VerticalScroll.Value;
            panel1.Controls.Add(btn);
            panel1.Controls.SetChildIndex(btn, 0);
            _gradients.Add((btn, cp));

            btn.MouseDown += btn_MouseDown;
            btn.MouseMove += (sender, args) => btn_MouseMove(sender, args, cp.Pos);
            btn.ColorChanged += (sender, args) =>
            {
                cp.Color = btn.Color;
                UpdateImage();
            };
            btn.MouseUp += (sender, args) =>
            {
                if (args.Button == MouseButtons.Right)
                {
                    _grid.Colors.Remove(cp);
                }
            };
        }

        private Point _mouseDownLocation;

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _mouseDownLocation = e.Location;
            }
        }

        private void btn_MouseMove(object sender, MouseEventArgs e, Vector pos)
        {
            if (e.Button == MouseButtons.Left)
            {
                ColorPickerButton btn = sender as ColorPickerButton;
                btn.Left += e.X - _mouseDownLocation.X;
                pos.X += e.X - _mouseDownLocation.X;
                btn.Top += e.Y - _mouseDownLocation.Y;
                pos.Y += e.Y - _mouseDownLocation.Y;
                _grid.UpdateTriangles();
                UpdateImage();
            }
        }

        private void ColorBorderOnColorChanged(object sender, EventArgs e)
        {
            _grid.ColorBorder = colorBorder.Color;
            UpdateImage();
        }

        private void ColorCenter_ColorChanged(object sender, EventArgs e)
        {
            _grid.ColorCenter = colorCenter.Color;
            UpdateImage();
        }

        private void TrackBarXOffsetOnValueChanged(object sender, EventArgs e)
        {
            _grid.OffsetX = (float)trackBarXOffset.Value;
            UpdateImage();
        }

        private void TrackBarYOffsetOnValueChanged(object sender, EventArgs e)
        {
            _grid.OffsetY = (float)trackBarYOffset.Value;
            UpdateImage();
        }

        private void TrackBarScaleOnValueChanged(object sender, EventArgs e)
        {
            _grid.HexS = (float)trackBarScale.Value;
            UpdateImage();
        }

        private void TrackBarRotateOnValueChanged(object sender, EventArgs e)
        {
            _grid.Rotation = (float)trackBarRotate.Value;
            UpdateImage();
        }

        private void TrackBarCenterOnValueChanged(object sender, EventArgs e)
        {
            _grid.MinHexS = (float)trackBarCenter.Value;
            UpdateImage();
        }

        private void TrackBarTwistOnValueChanged(object sender, EventArgs e)
        {
            _grid.Twist = (float)trackBarTwist.Value;
            UpdateImage();
        }

        private void TrackBarCenterColorOnValueChanged(object sender, EventArgs e)
        {
            _grid.ColorCenterStrength = (float)trackBarCenterColor.Value;
            UpdateImage();
        }

        private void TrackBarBorderColorOnValueChanged(object sender, EventArgs e)
        {
            _grid.ColorBorderStrength = (float)trackBarBorderColor.Value;
            UpdateImage();
        }

        private void TrackBarStrokeOnValueChanged(object sender, EventArgs e)
        {
            _grid.StrokeW = (float)Math.Exp(trackBarStroke.Value);
            UpdateImage();
        }

        private void TrackBarLineCnt_ValueChanged(object sender, EventArgs e)
        {
            _grid.LayerCnt = (int)Math.Round(Math.Min(2048, Math.Exp(trackBarLineCnt.Value)));
            UpdateImage();
        }

        public static Rectangle GetVisibleRectangle(ScrollableControl sc, Control child)
        {
            Rectangle bounds = child.Bounds;
            bounds.Intersect(sc.ClientRectangle);
            bounds.X -= child.Left;
            bounds.Y -= child.Top;
            return bounds;
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            UpdateImage();
        }

        private void UpdateImage()
        {
            Rectangle pos = GetVisibleRectangle(panel1, panel2);

            _grid.RenderWidth = pos.Width;
            _grid.RenderHeight = pos.Height;
            _grid.OffsetX = pos.X + (int)trackBarXOffset.Value;
            _grid.OffsetY = pos.Y + (int)trackBarYOffset.Value;

            _grid.RenderThreaded();
        }

        private void widthSelect_ValueChanged(object sender, EventArgs e)
        {
            panel2.Width = (int)widthSelect.Value;
        }

        private void heightSelect_ValueChanged(object sender, EventArgs e)
        {
            panel2.Height = (int)heightSelect.Value;
        }

        Random _random = new Random();
        private void panel2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            List<Color> colors = _grid.Colors.Select(x => x.Color).Distinct().ToList();
            _grid.Colors.Add(new ColorPos(colors[_random.Next(colors.Count)],
                new Vector(e.X + panel1.HorizontalScroll.Value, e.Y + panel1.VerticalScroll.Value)));
            UpdateImage();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            _grid.RenderWidth = _grid.Width;
            _grid.RenderHeight = _grid.Height;
            _grid.OffsetX = (int)trackBarXOffset.Value;
            _grid.OffsetY = (int)trackBarYOffset.Value;
            Task.Run(() =>
            {
                Invoke((MethodInvoker)delegate { UseWaitCursor = true; });
                _grid.RenderThreaded().Wait();
                _grid.Save($"Test_File_{DateTime.Now.Ticks}.png");
                Invoke((MethodInvoker)delegate { UseWaitCursor = false; });
            });
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            _grid.Load(files[0]);

            trackBarLineCnt.Value = Math.Log(_grid.LayerCnt);
            trackBarStroke.Value = Math.Log(_grid.StrokeW);
            trackBarBorderColor.Value = _grid.ColorBorderStrength;
            trackBarCenterColor.Value = _grid.ColorCenterStrength;
            trackBarRotate.Value = _grid.Rotation;
            trackBarCenter.Value = _grid.MinHexS;
            trackBarScale.Value = _grid.HexS;
            trackBarTwist.Value = _grid.Twist;
            trackBarXOffset.Value = _grid.OffsetX;
            trackBarYOffset.Value = _grid.OffsetY;
            colorCenter.Color = _grid.ColorCenter;
            colorBorder.Color = _grid.ColorBorder;
            widthSelect.Value = _grid.Width;
            heightSelect.Value = _grid.Height;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1) e.Effect = DragDropEffects.Copy;
        }
    }
}
