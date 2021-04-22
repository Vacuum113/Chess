using System;
using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class Knight : ChessMan
    {
        public override List<Variant> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing)
        {
            var variants = new List<Variant>();
            
            if (getRow + 2 < 8 && getColumn + 1 < 8)
                variants.Add(new Variant(getRow + 2, getColumn + 1));
            
            if (getRow + 2 < 8 && getColumn - 1 >= 0)
                variants.Add(new Variant(getRow + 2, getColumn - 1));
            
            if (getRow - 2 >= 0 && getColumn + 1 < 8)
                variants.Add(new Variant(getRow - 2, getColumn + 1));
            
            if (getRow - 2 >= 0 && getColumn - 1 >= 0)
                variants.Add(new Variant(getRow - 2, getColumn - 1));
            
            if (getRow + 1 < 8 && getColumn + 2 < 8)
                variants.Add(new Variant(getRow + 1, getColumn + 2));
            
            if (getRow + 1 < 8 && getColumn - 2 >= 0)
                variants.Add(new Variant(getRow + 1, getColumn - 2));  
            
            if (getRow - 1 >= 0 && getColumn + 2 < 8)
                variants.Add(new Variant(getRow - 1, getColumn + 2));  
         
            if (getRow - 1 >= 0 && getColumn - 2 >= 0)
                variants.Add(new Variant(getRow - 1, getColumn - 2));  
            
            return variants;
        }
    }
}