using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace text_modal.components
{
    public class Helpers
    {
       public Color HaxToColor(string hax)
{
    // Remove # if present
    if (hax.StartsWith("#"))
        hax = hax.Substring(1);

    // Parse ARGB
    if (hax.Length == 8) // ARGB format
    {
        byte a = Convert.ToByte(hax.Substring(0, 2), 16);
        byte r = Convert.ToByte(hax.Substring(2, 2), 16);
        byte g = Convert.ToByte(hax.Substring(4, 2), 16);
        byte b = Convert.ToByte(hax.Substring(6, 2), 16);
        return Color.FromArgb(a, r, g, b);
    }
    else if (hax.Length == 6) // RGB format (no alpha)
    {
        byte r = Convert.ToByte(hax.Substring(0, 2), 16);
        byte g = Convert.ToByte(hax.Substring(2, 2), 16);
        byte b = Convert.ToByte(hax.Substring(4, 2), 16);
        return Color.FromArgb(255, r, g, b); // Alpha = 255
    }
    else
    {
        throw new ArgumentException("Invalid hex color format");
    }
}

    }
}
