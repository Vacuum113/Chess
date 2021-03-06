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

        public override List<Variant> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing)
        {
            var variants = new List<Variant>();

            if (getRow + Player.GetPositiveYMovement() < 8 && getRow + Player.GetPositiveYMovement() >= 0)
            {
                var piece = pieces[getRow + Player.GetPositiveYMovement(), getColumn];
                if (!piece.GetChessMan() && !forKing)
                {
                    if (getRow + Player.GetPositiveYMovement() < 8 && getRow + Player.GetPositiveYMovement() >= 0)
                    {
                        variants.Add(new Variant(getRow + Player.GetPositiveYMovement(), getColumn));
                    }

                    if (_firstMove && !pieces[getRow + 2 * Player.GetPositiveYMovement(), getColumn].GetChessMan())
                    {
                        variants.Add(new Variant(getRow + 2 * Player.GetPositiveYMovement(), getColumn));
                    }
                }

                if (getRow + Player.GetPositiveYMovement() < 8 && getRow + Player.GetPositiveYMovement() >= 0 &&
                    getColumn - 1 >= 0)
                {
                    var leftPiece = pieces[getRow + Player.GetPositiveYMovement(), getColumn - 1];
                    if (leftPiece.GetChessMan() || forKing)
                    {
                        variants.Add(new Variant(getRow + Player.GetPositiveYMovement(), getColumn - 1));
                    }
                }

                if (getRow + Player.GetPositiveYMovement() < 8 && getRow + Player.GetPositiveYMovement() >= 0 &&
                    getColumn + 1 < 8)
                {
                    var rightPiece = pieces[getRow + Player.GetPositiveYMovement(), getColumn + 1];
                    if (rightPiece.GetChessMan()  || forKing)
                    {
                        variants.Add(new Variant(getRow + Player.GetPositiveYMovement(), getColumn + 1));
                    }
                }
            }

            return variants;
        }

        public void SetFirstMoveMade() => _firstMove = false;
    }
}