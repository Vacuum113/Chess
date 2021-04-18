using System.Collections.Generic;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class King : ChessMan
    {
        private void Update()
        {
        }

        public override List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieceReference)
        {
            throw new System.NotImplementedException();
        }
    }
}