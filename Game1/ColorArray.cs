using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
   public static class ColorArray
    {
        public static Color [] Colors = new Color[]
        {
            Color.Aquamarine,
            Color.Magenta,
            Color.DarkTurquoise,
            Color.Orange,
            Color.Orchid,
            Color.PaleGoldenrod,
            Color.PaleGreen,
            Color.PaleTurquoise,
            Color.PaleVioletRed,
        };

        public static Color RandomColor()
        {
            Random r = new Random();
            return Colors[r.Next() % Colors.Length];
        }
    }
}
