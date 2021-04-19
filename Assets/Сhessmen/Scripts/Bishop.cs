using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class Bishop : ChessMan
    {
        public override List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces)
        {
            var variants = new List<Variants>();

            for (var (r, c) = (getRow + 1, getColumn + 1); r < 8 && c < 8; r++, c++)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(r, c));
                    break;
                }
                
                variants.Add(new Variants(r, c));
            }
            
            for (var (r, c) = (getRow - 1, getColumn - 1); r >= 0 && c >= 0; r--, c--)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(r, c));
                    break;
                }
                
                variants.Add(new Variants(r, c));
            }
            
            for (var (r, c) = (getRow + 1, getColumn - 1); r < 8 && c >= 0; r++, c--)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(r, c));
                    break;
                }
                
                variants.Add(new Variants(r, c));
            }
            
            for (var (r, c) = (getRow - 1, getColumn + 1); r >= 0 && c < 8; r--, c++)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(r, c));
                    break;
                }
                
                variants.Add(new Variants(r, c));
            }

            return variants;
        }
    }
}