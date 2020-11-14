using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace HexGrid
{
    public class ColorPickerButton : ColorGrid
    {
        private Color _oldColor;

        public ColorPickerButton()
        {
            AutoAddColors = false;

            Columns = 1;
            EditMode = ColorEditingMode.None;
            ShowCustomColors = false;

            Colors = new ColorCollection(new[] { Color.HotPink });

            MouseDoubleClick += ThisOnMouseClick;
        }

        public new Color Color
        {
            set
            {
                base.Color = value;
                Colors = new ColorCollection(new[] { value });
            }
            get => base.Color;
        }

        private void ThisOnMouseClick(object sender, MouseEventArgs e)
        {
            ColorPickerDialog picker = new ColorPickerDialog();
            picker.ShowAlphaChannel = true;

            picker.Shown += (sender, args) => picker.Color = _oldColor = Color;

            picker.PreviewColorChanged += (o, args) =>
            {
                Color = picker.Color;
            };

            picker.Closed += (sender, args) =>
            {
                switch (picker.DialogResult)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        break;
                    case DialogResult.None:
                    case DialogResult.Cancel:
                    case DialogResult.Abort:
                    case DialogResult.Retry:
                    case DialogResult.Ignore:
                    case DialogResult.No:
                        Color = _oldColor;
                        break;
                }
            };

            picker.Show();
        }
    }
}
