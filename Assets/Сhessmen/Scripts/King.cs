﻿using System.Collections.Generic;
using UnityEngine;

namespace Сhessmen
{
    public class King : ChessMan
    {
        private void Update()
        {
        }

        public override List<Variants> GetMoveVariants(int getRow, int getColumn, GameObject[,] pieceReference)
        {
            throw new System.NotImplementedException();
        }
    }
}