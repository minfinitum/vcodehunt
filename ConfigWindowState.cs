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
        }

        public void SetSize(Size size)
        {
            IsSizeValid = true;
            Size = size;
        }

        public void SetLocation(Point location)
        {
            IsLocationValid = true;
            Location = location;
        }

        public bool IsSizeValid { get; set; }
        public Size Size { get; set; }

        public bool IsLocationValid { get; set; }
        public Point Location { get; set; }
    }
}
