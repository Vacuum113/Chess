using System.Collections.Generic;
using System.Net.Http.Headers;
using NUnit.Framework;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class Rook : ChessMan
    {
        private void Update()
        {
        }

        public override List<Variant> GetMoveVariants(int row, int column, Piece[,] pieces, bool forKing)
        {
            return GetRookMoveVariants(row, column, pieces);
        }

        private List<Variant> GetRookMoveVariants(int row, int column, Piece[,] pieces)
        {
            var variants = new List<Variant>();
            variants.AddRange(GetRowVariants(row, column, pieces));
            variants.AddRange(GetColumnVariants(column, row, pieces));
            return variants;
        }

        private List<Variant> GetRowVariants(int row, int column, Piece[,] pieces)
        {
            var variants = new List<Variant>();
            for (var i = column - 1; i >= 0 ; i--)
            {
                if(CanDestroy(pieces, row, i, variants))
                    break;
                    
                variants.Add(new Variant(row, i));
            }
            
            for (var i = column + 1; i < 8 ; i++)
            {
                if(CanDestroy(pieces, row, i, variants))
                    break;
                
                variants.Add(new Variant(row, i));
            }

            return variants;
        }
        
        private List<Variant> GetColumnVariants(int column, int row, Piece[,] pieces)
        {
            var variants = new List<Variant>();
            for (var i = row - 1; i >= 0; i--)
            {
                if(CanDestroy(pieces, i, column, variants))
                    break;
                    
                variants.Add(new Variant(i, column));
            }
            
            for (var i = row + 1; i < 8; i++)
            {
                if(CanDestroy(pieces, i, column, variants))
                    break;
                
                variants.Add(new Variant(i, column));
            }

            return variants;
        }
    }
}