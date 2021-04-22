using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class Bishop : ChessMan
    {
        public override List<Variant> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing)
        {
            var variants = new List<Variant>();

            for (var (r, c) = (getRow + 1, getColumn + 1); r < 8 && c < 8; r++, c++)
            {
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variant(r, c));
            }
            
            for (var (r, c) = (getRow - 1, getColumn - 1); r >= 0 && c >= 0; r--, c--)
            {
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variant(r, c));
            }
            
            for (var (r, c) = (getRow + 1, getColumn - 1); r < 8 && c >= 0; r++, c--)
            {
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variant(r, c));
            }
            
            for (var (r, c) = (getRow - 1, getColumn + 1); r >= 0 && c < 8; r--, c++)
            {
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variant(r, c));
            }

            return variants;
        }
    }
}