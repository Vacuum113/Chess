using System;
using System.Collections.Generic;
using NUnit.Framework;
using UBoard;
using UnityEngine;
using UPiece;
using UPlayer;

namespace Сhessmen
{
    public abstract class ChessMan : MonoBehaviour
    {
        protected Player Player;
        public abstract List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieceReference);

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public Player GetPlayer() => Player;
    }
}