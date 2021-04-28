using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace Velentr.Input.GamePad
{
    public static class GamePadHelpers
    {

        /// <summary>
        /// Converts a PlayerIndex to an int index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The converted index.</returns>
        /// <exception cref="System.Exception">Invalid PlayerIndex!</exception>
        public static int PlayerIndexToIntIndex(PlayerIndex index)
        {
            switch (index)
            {
                case PlayerIndex.One:
                    return 0;
                case PlayerIndex.Two:
                    return 1;
                case PlayerIndex.Three:
                    return 2;
                case PlayerIndex.Four:
                    return 3;
                default:
                    throw new Exception("Invalid PlayerIndex!");
            }
        }

        /// <summary>
        /// Converts an int index to a PlayerIndex.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The converted index.</returns>
        /// <exception cref="System.Exception">Invalid PlayerIndex!</exception>
        public static PlayerIndex IntIndexToPlayerIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return PlayerIndex.One;
                case 1:
                    return PlayerIndex.Two;
                case 2:
                    return PlayerIndex.Three;
                case 3:
                    return PlayerIndex.Four;
                default:
                    throw new Exception("Invalid PlayerIndex!");
            }
        }

        /// <summary>
        /// Validates the index of the player.
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <param name="maxIndex">Max index of the player.</param>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static void ValidatePlayerIndex(int playerIndex, int maxIndex)
        {
            if (playerIndex < 0 || playerIndex > maxIndex)
            {
                throw new ArgumentOutOfRangeException(Constants.PlayerIndexExceptionMessage);
            }
        }
    }
}
