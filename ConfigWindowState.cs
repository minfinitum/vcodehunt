using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VCodeHunt.Config
{
    public class WindowState
    {
        public WindowState()
        {
            State = FormWindowState.Normal;
        }

        public void SetSize(Size size)
        {
            UseSize = true;
            Size = size;
        }

        public void SetLocation(Point location)
        {
            UseLocation = true;
            Location = location;
        }

        public void SetState(FormWindowState state)
        {
            UseState = true;
            State = state;
        }

        public bool UseSize { get; set; }
        public Size Size { get; set; }

        public bool UseLocation { get; set; }
        public Point Location { get; set; }

        public bool UseState { get; set; }
        public FormWindowState State { get; set; }
    }
}
