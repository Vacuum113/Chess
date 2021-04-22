using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class Queen : ChessMan
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
            
            for (var i = getColumn - 1; i >= 0 ; i--)
            {
                if(CanDestroy(pieces, getRow, i, variants))
                    break;
                    
                variants.Add(new Variant(getRow, i));
            }
            
            for (var i = getColumn + 1; i < 8 ; i++)
            {
                if(CanDestroy(pieces, getRow, i, variants))
                    break;

                variants.Add(new Variant(getRow, i));
            }
            
            for (var i = getRow - 1; i >= 0; i--)
            {
                if(CanDestroy(pieces, i, getColumn, variants))
                    break;
                    
                variants.Add(new Variant(i, getColumn));
            }
            
            for (var i = getRow + 1; i < 8; i++)
            {
                if(CanDestroy(pieces, i, getColumn, variants))
                    break;
                
                variants.Add(new Variant(i, getColumn));
            }

            return variants;
        }
    }
}