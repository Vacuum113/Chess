using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UPiece;
using UPlayer;

namespace Сhessmen
{
    public class Pawn : ChessMan
    {
        private bool _firstMove = true;
        
        public override List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieceReference)
        {
            if (Player.GetTypePlayer() == TypePlayer.White)
                return WhiteMove(getRow, getColumn, pieceReference);
            else
                return BlackMove(getRow, getColumn, pieceReference);

        }

        private List<Variants> BlackMove(int getRow, int getColumn, Piece[,] pieceReference)
        {
            if (_firstMove)
            {
                _firstMove = false;
                return new List<Variants> {new Variants(getRow - 1, getColumn), new Variants(getRow - 2, getColumn)};
            }
            
            return new List<Variants> {new Variants(getRow - 1, getColumn)};
        }

        private List<Variants> WhiteMove(int getRow, int getColumn, Piece[,] pieceReference)
        {
            if (_firstMove)
            {
                _firstMove = false;
                return new List<Variants> {new Variants(getRow + 1, getColumn), new Variants(getRow + 2, getColumn)};
            }
            
            return new List<Variants> {new Variants(getRow + 1, getColumn)};
        }
    }
}