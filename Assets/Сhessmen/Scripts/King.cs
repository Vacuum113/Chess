using System.Collections.Generic;
using System.Linq;
using UInspector;
using UnityEngine;
using UPiece;

namespace Сhessmen
{
    public class King : ChessMan
    {
        public override List<Variants> GetMoveVariants(int getRow, int getColumn, Piece[,] pieces, bool forKing)
        {
            var variants = new List<Variants>();

            for (var (r, c) = (getRow + 1, getColumn + 1); r < 8 && c < 8; r++, c++)
            {
                if (CheckOnShah(r, c))
                    break;
                
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow - 1, getColumn - 1); r >= 0 && c >= 0; r--, c--)
            {
                if (CheckOnShah(r, c))
                    break;
                
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow + 1, getColumn - 1); r < 8 && c >= 0; r++, c--)
            {
                if (CheckOnShah(r, c))
                    break;
                
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var (r, c) = (getRow - 1, getColumn + 1); r >= 0 && c < 8; r--, c++)
            {
                if (CheckOnShah(r, c))
                    break;
                
                if(CanDestroy(pieces, r, c, variants))
                    break;
                
                variants.Add(new Variants(r, c));
                break;
            }
            
            for (var i = getColumn - 1; i >= 0 ; i--)
            {
                if (CheckOnShah(getRow, i))
                    break;
                
                if(CanDestroy(pieces, getRow, i, variants))
                    break;

                variants.Add(new Variants(getRow, i));
                break;
            }
            
            for (var i = getColumn + 1; i < 8 ; i++)
            {
                if (CheckOnShah(getRow, i))
                    break;
                
                if(CanDestroy(pieces, getRow, i, variants))
                    break;
                
                variants.Add(new Variants(getRow, i));
                break;
            }
            
            for (var i = getRow - 1; i >= 0; i--)
            {
                if (CheckOnShah(i, getColumn))
                    break;
                
                if(CanDestroy(pieces, i, getColumn, variants))
                    break;

                variants.Add(new Variants(i, getColumn));
                break;
            }
            
            for (var i = getRow + 1; i < 8; i++)
            {
                if (CheckOnShah(i, getColumn))
                    break;
                
                if(CanDestroy(pieces, i, getColumn, variants))
                    break;
                
                variants.Add(new Variants(i, getColumn));
                break;
            }

            return variants;
        }

        private bool CheckOnShah(int row, int column)
        {
            var board = Inspector.Instance.BoardInstance;
            var enemyChessMen = board.GetOtherAliveChessMen(Player.GetTypePlayer()).Where(c => c.GetType() != typeof(King));
            return enemyChessMen.Any(c => c.GetMoveVariantsForCurrentPiece().Any(v => v.Row == row && v.Column == column));
        }
    }
}