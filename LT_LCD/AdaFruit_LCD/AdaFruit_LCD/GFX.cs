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
        public Int16 _width;
        public Int16 _height;
 
        private GFX(Int16 w, Int16 h)
        {
            WIDTH = w;
            HEIGHT = h;
            _width = WIDTH;
            _height = HEIGHT;
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
 
        public void drawCircleHelper(Int16 x0, Int16 y0, Int16 r, Byte cornername, UInt16 color)
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
                if ((cornername & 0x4) >= 0x01)
                {
                    drawPixel((Int16) (x0 + x), (Int16) (y0 + y), color);
                    drawPixel((Int16) (x0 + y), (Int16) (y0 + x), color);
                }
                //C shit-circuiting
                if ((cornername & 0x2) >= 0x01)
                {
                    drawPixel((Int16)(x0 + x), (Int16)(y0 - y), color);
                    drawPixel((Int16)(x0 + y), (Int16)(y0 - x), color);
                }
                //C shit-circuiting
                if ((cornername & 0x8) >= 0x01)
                {
                    drawPixel((Int16)(x0 - y), (Int16)(y0 + x), color);
                    drawPixel((Int16)(x0 - x), (Int16)(y0 + y), color);
                }
                //C shit-circuiting
                if ((cornername & 0x1) >= 0x01)
                {
                    drawPixel((Int16)(x0 - y), (Int16)(y0 - x), color);
                    drawPixel((Int16)(x0 - x), (Int16)(y0 - y), color);
                }
            }
        }
 
        public void fillCircle(Int16 x0, Int16 y0, Int16 r,UInt16 color) {
            drawFastVLine((Int16)(x0), (Int16)(y0 - r), (Int16)(2 * r + 1), color);
            fillCircleHelper(x0, y0, r, 3, 0, color);
        }
        // Used to do circles and roundrects
        void fillCircleHelper(Int16 x0, Int16 y0, Int16 r,
            Byte cornername, Int16 delta, UInt16 color) {
            
          Int16 f     = (Int16)(1 - r);
          Int16 ddF_x = 1;
          Int16 ddF_y = (Int16)(- 2 * r);
          Int16 x     = 0;
          Int16 y     = r;

          while (x<y) {
            if (f >= 0) {
              y--;
              ddF_y += 2;
              f     += ddF_y;
            }
            x++;
            ddF_x += 2;
            f     += ddF_x;

            if ((cornername & 0x1) >= 0x01) {
                drawFastVLine((Int16)(x0 + x), (Int16)(y0 - y), (Int16)(2 * y + 1 + delta), color);
                drawFastVLine((Int16)(x0 + y), (Int16)(y0 - x), (Int16)(2 * x + 1 + delta), color);
            }
            if ((cornername & 0x2) >= 0x01) {
              drawFastVLine((Int16)(x0-x),(Int16)( y0-y), (Int16)(2*y+1+delta), color);
              drawFastVLine((Int16)(x0-y), (Int16)(y0-x), (Int16)(2*x+1+delta), color);
            }
          }
        }
        // Bresenham's algorithm - thx wikpedia
        public void drawLine(Int16 x0, Int16 y0, Int16 x1, Int16 y1, UInt16 color) 
        {
    
          bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
          if (steep) {
            swap(x0, y0);
            swap(x1, y1);
          }
 
          if (x0 > x1) {
            swap(x0, x1);
            swap(y0, y1);
          }
 
          Int16 dx, dy;
          dx = (Int16)(x1 - x0);
          dy = (Int16)(Math.Abs(y1 - y0));
 
          Int16 err = (Int16)(dx / 2);
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
        // swap function used in drawLine
        public void swap(Int16 y0, Int16 y1)
        {
            Int16 temp = y0;
            y0 = y1;
            y1 = y0;    
        }
        // Draw a rectangle
        public void drawRect(Int16 x, Int16 y, Int16 w, Int16 h, UInt16 color) 
        {
          drawFastHLine(x, y, w, color);
          drawFastHLine(x, (Int16)(y+h-1), w, color);
          drawFastVLine(x, y, h, color);
          drawFastVLine((Int16)(x+w-1), y, h, color);
        }

        public void drawFastVLine(Int16 x, Int16 y, Int16 h, UInt16 color)
        {
            // Update in subclasses if desired!
            drawLine(x, y, x, (Int16)(y + h - 1), color);
        }

        public void drawFastHLine(Int16 x, Int16 y,  Int16 w, UInt16 color)
        {
          // Update in subclasses if desired!
          drawLine(x, y, (Int16)(x+w-1), y, color);
        }

        public void fillRect(Int16 x, Int16 y, Int16 w, Int16 h, UInt16 color)
        {
          // Update in subclasses if desired!
          for (Int16 i=x; i<x+w; i++) {
            drawFastVLine(i, y, h, color);
          }
        }
        public void fillScreen(UInt16 color)
        {
            fillRect(0, 0, _width, _height, color);
        }

        // Draw a rounded rectangle
        public void drawRoundRect(Int16 x, Int16 y, Int16 w,
            Int16 h, Int16 r, UInt16 color) {
            // smarter version
            drawFastHLine((Int16)(x+r), y, (Int16)(w-2*r), color); // Top
            drawFastHLine((Int16)(x+r), (Int16)(y+h-1), (Int16)(w-2*r), color); // Bottom
            drawFastVLine(x, (Int16)(y+r), (Int16)(h-2*r), color); // Left
            drawFastVLine((Int16)(x+w-1), (Int16)(y+r), (Int16)(h-2*r), color); // Right
            // draw four corners
            drawCircleHelper((Int16)(x+r), (Int16)(y+r), r, 1, color);
            drawCircleHelper((Int16)(x+w-r-1), (Int16)(y+r), r, 2, color);
            drawCircleHelper((Int16)(x+w-r-1), (Int16)(y+h-r-1), r, 4, color);
            drawCircleHelper((Int16)(x+r), (Int16)(y+h-r-1), r, 8, color);
        }

        // Fill a rounded rectangle
        public void fillRoundRect(Int16 x, Int16 y, Int16 w, Int16 h, Int16 r, UInt16 color)
        {
            // smarter version
            fillRect((Int16)(x+r), y, (Int16)(w-2*r), h, color);

            // draw four corners
            fillCircleHelper((Int16)(x+w-r-1), (Int16)(y+r), r, 1, (Int16)(h-2*r-1), color);
            fillCircleHelper((Int16)(x+r), (Int16)(y+r), r, 2, (Int16)(h-2*r-1), color);
        }
        // Draw a triangle
        public void drawTriangle(Int16 x0, Int16 y0,Int16 x1, Int16 y1, Int16 x2, Int16 y2, UInt16 color)
        {
          drawLine(x0, y0, x1, y1, color);
          drawLine(x1, y1, x2, y2, color);
          drawLine(x2, y2, x0, y0, color);
        }
        // Fill a triangle
        public void fillTriangle ( Int16 x0, Int16 y0, Int16 x1, Int16 y1, Int16 x2, Int16 y2, UInt16 color)
        {
          Int16 a, b, y, last;

          // Sort coordinates by Y order (y2 >= y1 >= y0)
          if (y0 > y1) {
            swap(y0, y1); swap(x0, x1);
          }
          if (y1 > y2) {
            swap(y2, y1); swap(x2, x1);
          }
          if (y0 > y1) {
            swap(y0, y1); swap(x0, x1);
          }

          if(y0 == y2) { // Handle awkward all-on-same-line case as its own thing
            a = b = x0;
            if(x1 < a)      a = x1;
            else if(x1 > b) b = x1;
            if(x2 < a)      a = x2;
            else if(x2 > b) b = x2;
            drawFastHLine(a, y0, (Int16)(b-a+1), color);
            return;
          }

          Int16
            dx01 = (Int16)(x1 - x0),
            dy01 = (Int16)(y1 - y0),
            dx02 = (Int16)(x2 - x0),
            dy02 = (Int16)(y2 - y0),
            dx12 = (Int16)(x2 - x1),
            dy12 = (Int16)(y2 - y1),
            sa   = 0,
            sb   = 0;

          // For upper part of triangle, find scanline crossings for segments
          // 0-1 and 0-2.  If y1=y2 (flat-bottomed triangle), the scanline y1
          // is included here (and second loop will be skipped, avoiding a /0
          // error there), otherwise scanline y1 is skipped here and handled
          // in the second loop...which also avoids a /0 error here if y0=y1
          // (flat-topped triangle).
          if(y1 == y2) last = y1;   // Include y1 scanline
          else         last = (Int16)(y1-1); // Skip it

          for(y=y0; y<=last; y++) {
            a = (Int16)(x0 + sa / dy01);
            b = (Int16)(x0 + sb / dy02);
            sa += dx01;
            sb += dx02;
            /* longhand:
            a = x0 + (x1 - x0) * (y - y0) / (y1 - y0);
            b = x0 + (x2 - x0) * (y - y0) / (y2 - y0);
            */
            if(a > b) swap(a,b);
            drawFastHLine(a, y, (Int16)(b-a+1), color);
          }

          // For lower part of triangle, find scanline crossings for segments
          // 0-2 and 1-2.  This loop is skipped if y1=y2.
          sa = (Int16)(dx12 * (y - y1));
          sb = (Int16)(dx02 * (y - y0));
          for(; y<=y2; y++) {
            a = (Int16)(x1 + sa / dy12);
            b = (Int16)(x0 + sb / dy02);
            sa += dx12;
            sb += dx02;
            /* longhand:
            a = x1 + (x2 - x1) * (y - y1) / (y2 - y1);
            b = x0 + (x2 - x0) * (y - y0) / (y2 - y0);
            */
            if(a > b) swap(a,b);
            drawFastHLine(a, y, (Int16)(b-a+1), color);
          }
        }
        //NEED TO CORRECT THIS FUNCTION!!!!
        public void drawBitmap(Int16 x, Int16 y, Byte[] bitmap, Int16 w, Int16 h, UInt16 color)
        {

          Int16 i, j, byteWidth = (Int16)((w + 7) / 8);
          //foreach (byte b in bitmap)
          //{
          //    if(
          //}
          //for(j=0; j<h; j++)
          //  {
          //  for(i=0; i<w; i++ ) {
          //    if(pgm_read_byte(bitmap + j * byteWidth + i / 8) & (128 >> (i & 7))) {
          //  drawPixel(x+i, y+j, color);
          //    }
          //  }
          //}
        }
        //******FIX THIS SHIT YO**************
        //#if ARDUINO >= 100
        //size_t Adafruit_GFX::write(Byte c) {
        //#else
        //void Adafruit_GFX::write(Byte c) {
        //#endif
        //  if (c == '\n') {
        //    cursor_y += textsize*8;
        //    cursor_x  = 0;
        //  } else if (c == '\r') {
        //    // skip em
        //  } else {
        //    drawChar(cursor_x, cursor_y, c, textcolor, textbgcolor, textsize);
        //    cursor_x += textsize*6;
        //    if (wrap && (cursor_x > (_width - textsize*6))) {
        //      cursor_y += textsize*8;
        //      cursor_x = 0;
        //    }
        //  }
        //#if ARDUINO >= 100
        //  return 1;
        //#endif
        //}
        //************************************
        // Draw a character
        public void drawChar(Int16 x, Int16 y, char c, UInt16 color, UInt16 bg, Byte size) {

          if((x >= _width)            || // Clip right
             (y >= _height)           || // Clip bottom
             ((x + 6 * size - 1) < 0) || // Clip left
             ((y + 8 * size - 1) < 0))   // Clip top
            return;

          for (sbyte i=0; i<6; i++ ) {
            Byte line;
            if (i == 5) 
              line = 0x0;
            else 
              line = pgm_read_byte(font+(c*5)+i);
            for (sbyte j = 0; j<8; j++) {
              if ((line & 0x1) >= 0x1)
                {
                if (size == 1) // default size
                  drawPixel((Int16)(x+i), (Int16)(y+j), color);
                else {  // big size
                  fillRect((Int16)(x+(i*size)), (Int16)(y+(j*size)), size, size, color);
                } 
              } else if (bg != color) {
                if (size == 1) // default size
                  drawPixel((Int16)(x+i), (Int16)(y+j), bg);
                else {  // big size
                  fillRect((Int16)(x+i*size), (Int16)(y+j*size), size, size, bg);
                }
              }
              line >>= 1;
            }
          }
        }

        public void setCursor(Int16 x, Int16 y) {
          cursor_x = x;
          cursor_y = y;
        }

        public void setTextSize(Byte s) {
          textsize = (s > 0) ? s : 1;
        }

        public void setTextColor(UInt16 c) {
          // For 'transparent' background, we'll set the bg 
          // to the same as fg instead of using a flag
          textcolor = textbgcolor = c;
        }

        public void setTextColor(UInt16 c, UInt16 b) {
          textcolor   = c;
          textbgcolor = b; 
        }

        public void setTextWrap(bool w) {
          wrap = w;
        }

        public void setRotation(Byte x) {
          rotation = (x & 3);
          switch(rotation) {
           case 0:
           case 2:
            _width  = WIDTH;
            _height = HEIGHT;
            break;
           case 1:
           case 3:
            _width  = HEIGHT;
            _height = WIDTH;
            break;
          }
        }

        void Adafruit_GFX::invertDisplay(boolean i) {
          // Do nothing, must be subclassed if supported
        }
    }
}