using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class King : ChessMan
    {
        public override List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces)
        {
            var variants = new List<Variants>();

            for (var (r, c) = (getRow + 1, getColumn + 1); r < 8 && c < 8; r++, c++)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow - 1, getColumn - 1); r >= 0 && c >= 0; r--, c--)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow + 1, getColumn - 1); r < 8 && c >= 0; r++, c--)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow - 1, getColumn + 1); r >= 0 && c < 8; r--, c++)
            {
                var piece = pieces[r, c];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var i = getColumn - 1; i >= 0 ; i--)
            {
                var piece = pieces[getRow, i];
                if (piece.GetChessMan())
                    break;
                    
                variants.Add(new Variants(getRow, i));
                break;
            }
            
            for (var i = getColumn + 1; i < 8 ; i++)
            {
                var piece = pieces[getRow, i];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(getRow, i));
                break;
            }
            
            for (var i = getRow - 1; i >= 0; i--)
            {
                var piece = pieces[i, getColumn];
                if (piece.GetChessMan())
                    break;
                    
                variants.Add(new Variants(i, getColumn));
                break;
            }
            
            for (var i = getRow + 1; i < 8; i++)
            {
                var piece = pieces[i, getColumn];
                if (piece.GetChessMan())
                    break;
                
                variants.Add(new Variants(i, getColumn));
                break;
            }

            return variants;
        }
    }
}