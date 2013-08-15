using System;
using System.Text;
 
namespace AdaFruit_LCD
{
    public abstract class GFX
    {
        public Int16 WIDTH, HEIGHT;
        public uint textsize, rotation;
        public Int16 cursor_x, cursor_y;
        public UInt16 textcolor, textbgcolor;
        public bool wrap;
 
        private GFX(Int16 w, Int16 h)
        {
            WIDTH = w;
            HEIGHT = h;
            rotation = 0;
            cursor_y = cursor_x = 0;
            textsize = 1;
            textcolor = textbgcolor = 0xFFFF;
            wrap = true;
        }
 
        public void DrawCircle(Int16 x0, Int16 y0, Int16 r, UInt16 color)
        {
            Int16 f = (Int16)(1 - r);
            Int16 ddF_x = 1;
            Int16 ddF_y = (Int16)(-2 * r);
            Int16 x = 0;
            Int16 y = r;
 
            drawPixel(x0, (Int16)(y0 + r), color);
            drawPixel(x0, (Int16)(y0 - r), color);
            drawPixel((Int16)(x0 + r), y0, color);
            drawPixel((Int16)(x0 - r), y0, color);
 
            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;
 
                drawPixel((Int16)(x0 + x), (Int16)(y0 + y), color);
                drawPixel((Int16)(x0 - x), (Int16)(y0 + y), color);
                drawPixel((Int16)(x0 + x), (Int16)(y0 - y), color);
                drawPixel((Int16)(x0 - x), (Int16)(y0 - y), color);
                drawPixel((Int16)(x0 + y), (Int16)(y0 + x), color);
                drawPixel((Int16)(x0 - y), (Int16)(y0 + x), color);
                drawPixel((Int16)(x0 + y), (Int16)(y0 - x), color);
                drawPixel((Int16)(x0 - y), (Int16)(y0 - x), color);
 
            }
        }
 
        public abstract void drawPixel(Int16 x, Int16 y, UInt16 color);
 
        public void drawCircleHelper(Int16 x0, Int16 y0, Int16 r, uint cornername, UInt16 color)
        {
            Int16 f = (Int16)(1 - r);
            Int16 ddF_x = 1;
            Int16 ddF_y = (Int16)(-2 * r);
            Int16 x = 0;
            Int16 y = r;
 
            while (x < y)
            {
                if (f >= 0)
                {
                    y--;
                    ddF_y += 2;
                    f += ddF_y;
                }
                x++;
                ddF_x += 2;
                f += ddF_x;
                //Watch out here, had to change some C shit-circuiting
                if ((cornername & 0x4) > 0x01)
                {
                    drawPixel((Int16) (x0 + x), (Int16) (y0 + y), color);
                    drawPixel((Int16) (x0 + y), (Int16) (y0 + x), color);
                }
                //C shit-circuiting
                if ((cornername & 0x2) > 0x01)
                {
                    drawPixel((Int16)(x0 + x), (Int16)(y0 - y), color);
                    drawPixel((Int16)(x0 + y), (Int16)(y0 - x), color);
                }
                //C shit-circuiting
                if ((cornername & 0x8) > 0x01)
                {
                    drawPixel((Int16)(x0 - y), (Int16)(y0 + x), color);
                    drawPixel((Int16)(x0 - x), (Int16)(y0 + y), color);
                }
                //C shit-circuiting
                if ((cornername & 0x1) > 0x01)
                {
                    drawPixel((Int16)(x0 - y), (Int16)(y0 - x), color);
                    drawPixel((Int16)(x0 - x), (Int16)(y0 - y), color);
                }
            }
        }
 
        public void fillCircle(Int16 x0, Int16 y0, Int16 r,UInt16 color) {
            drawFastVLine((Int16)(x0), (Int16)(y0 - r), (Int16)(2 * r + 1), (UInt16)(color));
            fillCircleHelper(x0, y0, r, 3, 0, color);
        }
        public void drawFastVLine(Int16 x, Int16 y, Int16 h, UInt16 color)
        {
            // Update in subclasses if desired!
            drawLine((Int16)(x), (Int16)(y), (Int16)(x), (Int16)(y + h - 1), (UInt16)(color));
        }
    // Bresenham's algorithm - thx wikpedia
public void drawLine(Int16 x0, Int16 y0, Int16 x1, Int16 y1, UInt16 color) 
{
  Int16 steep = abs(y1 - y0) > abs(x1 - x0);
  if (steep) {
    swap(x0, y0);
    swap(x1, y1);
  }
 
  if (x0 > x1) {
    swap(x0, x1);
    swap(y0, y1);
  }
 
  Int16 dx, dy;
  dx = x1 - x0;
  dy = abs(y1 - y0);
 
  Int16 err = dx / 2;
  Int16 ystep;
 
  if (y0 < y1) {
    ystep = 1;
  } else {
    ystep = -1;
  }
 
  for (; x0<=x1; x0++) {
    if (steep) {
      drawPixel(y0, x0, color);
    } else {
      drawPixel(x0, y0, color);
    }
    err -= dy;
    if (err < 0) {
      y0 += ystep;
      err += dx;
    }
  }
}
    }
}