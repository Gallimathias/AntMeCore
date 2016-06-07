﻿using System;

namespace AntMe.Basics.MapTiles
{
    /// <summary>
    /// Map Tile for the concave Part of the Cliff.
    /// </summary>
    public class ConcaveCliffMapTile : CliffMapTile
    {
        //
        // Default Position with Orientation East
        //
        // #####
        // #++++
        // #+
        // #+
        //

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ConcaveCliffMapTile() { }

        /// <summary>
        /// Returns the Level to enter on the East Side.
        /// </summary>
        public override byte? EnterLevelEast
        {
            get
            {
                switch (Orientation)
                {
                    case Compass.East: return null;
                    case Compass.South: return null;
                    case Compass.West: return (byte)(HeightLevel + 1);
                    case Compass.North: return (byte)(HeightLevel + 1);
                    default: throw new NotSupportedException("Wrong Orientation");
                }
            }
        }

        /// <summary>
        /// Returns the Level to enter on the South Side.
        /// </summary>
        public override byte? EnterLevelSouth
        {
            get
            {
                switch (Orientation)
                {
                    case Compass.East: return (byte)(HeightLevel + 1);
                    case Compass.South: return null;
                    case Compass.West: return null;
                    case Compass.North: return (byte)(HeightLevel + 1);
                    default: throw new NotSupportedException("Wrong Orientation");
                }
            }
        }

        /// <summary>
        /// Returns the Level to enter on the West Side.
        /// </summary>
        public override byte? EnterLevelWest
        {
            get
            {
                switch (Orientation)
                {
                    case Compass.East: return (byte)(HeightLevel + 1);
                    case Compass.South: return (byte)(HeightLevel + 1);
                    case Compass.West: return null;
                    case Compass.North: return null;
                    default: throw new NotSupportedException("Wrong Orientation");
                }
            }
        }

        /// <summary>
        /// Returns the Level to enter on the North Side.
        /// </summary>
        public override byte? EnterLevelNorth
        {
            get
            {
                switch (Orientation)
                {
                    case Compass.East: return null;
                    case Compass.South: return (byte)(HeightLevel + 1);
                    case Compass.West: return (byte)(HeightLevel + 1);
                    case Compass.North: return null;
                    default: throw new NotSupportedException("Wrong Orientation");
                }
            }
        }
    }
}
