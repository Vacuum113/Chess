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

        public abstract List<Variant> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing);

        public void SetPlayer(Player player)
        {
            Player = player;
        }

        public Player GetPlayer() => Player;

        public List<Variant> GetMoveVariantsForCurrentPiece()
        {
            return GetMoveVariants(Piece.GetRow(), Piece.GetColumn(),
                Inspector.Instance.BoardInstance.GetPieces(), true);
        }

        protected bool CanDestroy(Piece[,] pieces, int row, int column, List<Variant> variants)
        {
            var piece = pieces[row, column];
            if (piece.GetChessMan())
            {
                if (piece.GetChessMan().Player.GetTypePlayer() != Player.GetTypePlayer())
                    variants.Add(new Variant(row, column));
                return true;
            }

            return false;
        }

        public void SetPiece(Piece piece) => Piece = piece;
    }
}