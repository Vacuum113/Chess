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

        public override List<Variants> GetMoveVariants(int row, int column, GameObject[,] pieceReference)
        {
            return GetRookMoveVariants(row, column, pieceReference);
        }

        private List<Variants> GetRookMoveVariants(int row, int column, GameObject[,] pieceReference)
        {
            var variants = new List<Variants>();
            variants.AddRange(GetRowVariants(row, column, pieceReference));
            variants.AddRange(GetColumnVariants(column, row, pieceReference));
            return variants;
        }

        private List<Variants> GetRowVariants(int row, int column, GameObject[,] pieceReference)
        {
            var variants = new List<Variants>();
            for (var i = column; i >= 0 ; i--)
            {
                var piece = pieceReference[row, i].GetComponent<Piece>();
                if (piece.GetChessMan() != null)
                    break;
                    
                variants.Add(new Variants(row, i));
            }
            
            for (var i = column; i < 8 ; i++)
            {
                var piece = pieceReference[row, i].GetComponent<Piece>();
                if (piece.GetChessMan() != null)
                    break;
                
                variants.Add(new Variants(row, i));
            }

            return variants;
        }
        
        private List<Variants> GetColumnVariants(int column, int row, GameObject[,] pieceReference)
        {
            var variants = new List<Variants>();
            for (var i = row + 1; i >= 0 && i > row ; i--)
            {
                var piece = pieceReference[i, column].GetComponent<Piece>();
                if (piece.GetChessMan() != null)
                    break;
                    
                variants.Add(new Variants(i, column));
            }
            
            for (var i = row + 1; i < 8 && i > row; i++)
            {
                var piece = pieceReference[i, column].GetComponent<Piece>();
                if (piece.GetChessMan() != null)
                    break;
                
                variants.Add(new Variants(i, column));
            }

            return variants;
        }
    }
}