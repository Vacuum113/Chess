using System;
using System.Collections.Generic;
using NUnit.Framework;
using UBoard;
using UInspector;
using UnityEngine;
using UPiece;
using UPlayer;

namespace Сhessmen
{
    public abstract class ChessMan : MonoBehaviour
    {
        protected Player Player;
        public Piece Piece { get; set; }

        public abstract List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing);

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public Player GetPlayer() => Player;

        public List<Variants> GetMoveVariantsForCurrentPiece() => GetMoveVariants(Piece.GetRow(), Piece.GetColumn(),
            Inspector.Instance.BoardInstance.GetPieces(), true);

        protected bool CanDestroy(Piece[,] pieces, int row, int column, List<Variants> variants)
        {
            var piece = pieces[row, column];
            if (piece.GetChessMan())
            {
                variants.Add(new Variants(row, column));
                return true;
            }

            return false;
        }

        public void SetPiece(Piece piece) => Piece = piece;
    }
}