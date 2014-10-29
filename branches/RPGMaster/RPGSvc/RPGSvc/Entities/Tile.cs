using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RPGSvc.Entities
{
    public class Tile
    {
        public double posX;
        public double posY;
        public double posZ;
        public int tileType;
        public DiffuseColor diffuseColor;
        public string materialName;

        public class DiffuseColor
        {
            public double r;
            public double g;
            public double b;
        }
    }
}