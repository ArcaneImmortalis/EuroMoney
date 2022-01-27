namespace EuroMoney.Library.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public struct ArenaLocation
    {
        public ArenaLocation(int xLocation, int yLocation)
        {
            this.XLocation = xLocation;
            this.YLocation = yLocation;
        }

        /// <summary>
        /// Gets a value representing this location along the X-axis of the arena.
        /// </summary>
        public int XLocation { get; }

        /// <summary>
        /// Gets a value representing this location along the Y-axis of the arena.
        /// </summary>
        public int YLocation { get; }
    }
}
