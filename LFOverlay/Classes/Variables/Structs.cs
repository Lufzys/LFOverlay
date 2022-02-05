using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFOverlay.Classes.Variables
{
    class Structs
    {
        public struct DrawData
        {
            public int SourceX;
            public int SourceY;
            public int TargetX;
            public int TargetY;

            public int Width, Height;
            public Enums.DrawType Type;
            public Color Color;
            public float Thickness;

            public string Text;
            public string FontFamily;
            public float FontSize;

            public int Radius;
        }

        public struct RECT
        {
            public int left, top, right, bottom;
        }
    }
}
