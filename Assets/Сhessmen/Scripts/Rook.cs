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

        public override List<Variants> GetMoveVariants(int row, int column, Piece[,] pieces)
        {
            return GetRookMoveVariants(row, column, pieces);
        }

        private List<Variants> GetRookMoveVariants(int row, int column, Piece[,] pieces)
        {
            var variants = new List<Variants>();
            variants.AddRange(GetRowVariants(row, column, pieces));
            variants.AddRange(GetColumnVariants(column, row, pieces));
            return variants;
        }

        private List<Variants> GetRowVariants(int row, int column, Piece[,] pieces)
        {
            var variants = new List<Variants>();
            for (var i = column - 1; i >= 0 ; i--)
            {
                var piece = pieces[row, i];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(row, i));
                    break;
                }
                    
                variants.Add(new Variants(row, i));
            }
            
            for (var i = column + 1; i < 8 ; i++)
            {
                var piece = pieces[row, i];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(row, i));
                    break;
                }
                
                variants.Add(new Variants(row, i));
            }

            return variants;
        }
        
        private List<Variants> GetColumnVariants(int column, int row, Piece[,] pieces)
        {
            var variants = new List<Variants>();
            for (var i = row - 1; i >= 0; i--)
            {
                var piece = pieces[i, column];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(i, column));
                    break;
                }
                    
                variants.Add(new Variants(i, column));
            }
            
            for (var i = row + 1; i < 8; i++)
            {
                var piece = pieces[i, column];
                if (piece.GetChessMan())
                {
                    variants.Add(new Variants(i, column));
                    break;
                }
                
                variants.Add(new Variants(i, column));
            }

            return variants;
        }
    }
}