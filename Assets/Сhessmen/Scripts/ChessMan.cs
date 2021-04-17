using System;
using System.Collections.Generic;
using NUnit.Framework;
using UBoard;
using UnityEngine;

namespace Сhessmen
{
    public abstract class ChessMan : MonoBehaviour
    {
        public abstract List<Variants> GetMoveVariants(int getRow, int getColumn, GameObject[,] pieceReference);
    }
}