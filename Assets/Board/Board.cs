using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UPiece;
using UPlayer;
using Сhessmen;

namespace UBoard
{
    public class Board : MonoBehaviour
    {
        private Piece[,] _pieces;
        public GameObject Piece;
        private const int RowCount = 8;
        private const int ColumnCount = 8;
        private readonly float _firstPiecePositionX = 0;
        private readonly float _firstPiecePositionY = 0;
        public float Step = 9.91f;

        private Piece DefaultPiece;

        private Piece SelectedPiece;
        // Start is called before the first frame update
        void Start()
        {
            DefaultPiece = Instantiate(Piece,  new Vector3(-100, 0, -1), Quaternion.identity, new RectTransform()).GetComponent<Piece>();
            Validation();
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedPiece)
            {
                if (Input.GetMouseButtonDown(0) && SelectedPiece)
                {

                    return;
                }
            }
        }

        public void SetPiece(GameObject chessManObject, int column, int row, Player player)
        {
            var piece = _pieces[row, column];
            var pieceTransform = piece.transform.position;
            chessManObject.transform.position = new Vector3(pieceTransform.x, pieceTransform.y, -2);
            var chessMan = chessManObject.GetComponent<ChessMan>();
            chessMan.SetPlayer(player);
            chessMan.SetPiece(piece);
            var pieceObject = piece.GetComponent<Piece>();
            pieceObject.SetChessMan(chessMan);
        } 
        
        private void Validation()
        {
            if (Piece == null) {
                Debug.LogError("assign the gameobject CanMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        public void InitialBoard()
        {
            _pieces = new Piece[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var y = 0; y < ColumnCount; y++)
                {
                    _pieces[i, y] = InitialPiece(y, i);
                }
            }
        }
        
        private Piece InitialPiece(int column, int row)
        {
            var piece = Instantiate(Piece,  new Vector3(_firstPiecePositionX + column * Step, _firstPiecePositionY + row * Step, -1), Quaternion.identity, new RectTransform()).GetComponent<Piece>();
            var position = piece.transform.position;
            position.Set(position.x, -2, position.z);
            piece.SetRow(row);
            piece.SetColumn(column);
            return piece;
        }

        public void SetSelectedPiece(Piece piece) => SelectedPiece = piece;

        public Piece GetSelectedPiece() => SelectedPiece;

        public void ResetSelect()
        {
            ResetPiecesMaterial();
            SetSelectedPiece(null);
        }

        private void ResetPiecesMaterial()
        {
            var variantsPieces = _pieces.OfType<Piece>().Where(p => p.GetComponent<Renderer>().material.name != DefaultPiece.GetComponent<Renderer>().material.name).ToList();
            foreach (var variantsPiece in variantsPieces)
            {
                variantsPiece.ResetPiece(Piece.GetComponent<Renderer>().sharedMaterial);
            }
        }

        public void MoveChessMan(Piece newPiece)
        {
            var selectedPiece = SelectedPiece.GetComponent<Piece>();
            var selectedChessMan = selectedPiece.GetChessMan();

            if (newPiece.GetChessMan())
            {
                newPiece.DestroyChessMan();
                newPiece.SetChessMan(null);
            }

            newPiece.SetChessMan(selectedChessMan);
            selectedChessMan.SetPiece(newPiece);
            selectedPiece.SetChessMan(null);
            

            var transformSelected = selectedChessMan.transform;
            var newPiecePosition = newPiece.transform.position;
            
            transformSelected.position = new Vector3(newPiecePosition.x, newPiecePosition.y, transformSelected.position.z);
        }
        
        private FakeMove FakeMoveChessMan(Piece newPiece)
        {
            var selectedPiece = SelectedPiece.GetComponent<Piece>();
            var selectedChessMan = selectedPiece.GetChessMan();

            var fakeMove = new FakeMove(newPiece, newPiece.GetChessMan(), SelectedPiece, selectedChessMan);

            if (newPiece.GetChessMan())
            {
                newPiece.GetChessMan().SetPiece(null);
                newPiece.SetChessMan(null);
                fakeMove.SetDestroyed();
            }

            newPiece.SetChessMan(selectedChessMan);
            selectedChessMan.SetPiece(newPiece);
            selectedPiece.SetChessMan(null);

            return fakeMove;
        }

        private void RestoreFakeMove(FakeMove fakeMove)
        {
            var selectedPiece = fakeMove.SelectedPiece;
            var selectedChessMan = fakeMove.SelectedChessMan;
            var newPiece = fakeMove.NewPiece;
            var newPieceChessMan = fakeMove.NewPieceChessMan;
            
            selectedPiece.SetChessMan(selectedChessMan);
            selectedChessMan.SetPiece(selectedPiece);

            if (fakeMove.Destroyed)
            {
                newPieceChessMan.SetPiece(newPiece);
                newPiece.SetChessMan(newPieceChessMan);
            }
            else
            {
                newPiece.SetChessMan(null);
            }
        }

        //todo: Need to make more clearly
        public void MarkCanMovePieces()
        {
            var piece = SelectedPiece;
            piece.SetSelectedMaterial();
            var chessMan = piece.GetChessMan();
            var variants = chessMan.GetMoveVariants(piece.GetRow(), piece.GetColumn(), _pieces, false);
            var chessManType = chessMan.GetPlayer().GetTypePlayer();
            
            var enemyChessMen = GetOtherAliveChessMen(chessManType).Where(c => c.GetType() != typeof(King)).ToArray();

            var finalVariants = new List<Variant>();
            
            foreach (var variant in variants)
            {
                var pieceVariant = _pieces[variant.Row, variant.Column];
                var pieceChessMan = pieceVariant.GetChessMan();
                if (pieceChessMan && pieceChessMan.GetPlayer().GetTypePlayer() == chessManType)
                    continue;

                var fakeMove = FakeMoveChessMan(_pieces[variant.Row, variant.Column]);
                var kingPiece = _pieces.OfType<Piece>().First(p => p.GetChessMan() is King k && k.GetPlayer().GetTypePlayer() == chessMan.GetPlayer().GetTypePlayer());
                var isShah = enemyChessMen.Where(e => e.Piece != null).Any(c => c.GetMoveVariantsForCurrentPiece().Any(v => v.Row == kingPiece.GetRow() && v.Column == kingPiece.GetColumn()));
                if (!isShah)
                    finalVariants.Add(variant);
                RestoreFakeMove(fakeMove);
            }

            if (chessMan is King && finalVariants.Count == 0)
            {
                if (!AnyCheckCanMove(chessManType == TypePlayer.Black ? TypePlayer.White : TypePlayer.Black))
                    Debug.Log("Checkmate!!!!!");

                
            }
            
            foreach (var variant in finalVariants)
            {
                var pieceVariant = _pieces[variant.Row, variant.Column];
                var pieceChessMan = pieceVariant.GetChessMan();
                if (!pieceChessMan)
                {
                    pieceVariant.SetCanMove();
                }
                else
                {
                    if (pieceChessMan.GetPlayer().GetTypePlayer() != chessManType)
                    {
                        pieceVariant.SetCanDestroyChessMan();
                    }
                }
            }
        }

        private bool AnyCheckCanMove(TypePlayer typePlayer)
        {
            var chessMen = GetOtherAliveChessMen(typePlayer).Where(c => c.GetType() != typeof(King)).ToArray();
            foreach (var chessMan in chessMen)
            {
                if (!HasCanMoveVariants(chessMan.Piece))
                    return false;
            }

            return false;
        }

        private bool HasCanMoveVariants(Piece piece)
        {
            var chessMan = piece.GetChessMan();
            var variants = chessMan.GetMoveVariants(piece.GetRow(), piece.GetColumn(), _pieces, false);
            var chessManType = chessMan.GetPlayer().GetTypePlayer();
            
            var enemyChessMen = GetOtherAliveChessMen(chessManType).Where(c => c.GetType() != typeof(King)).ToArray();

            var finalVariants = new List<Variant>();
            
            foreach (var variant in variants)
            {
                var pieceVariant = _pieces[variant.Row, variant.Column];
                var pieceChessMan = pieceVariant.GetChessMan();
                if (pieceChessMan && pieceChessMan.GetPlayer().GetTypePlayer() == chessManType)
                    continue;

                var fakeMove = FakeMoveChessMan(_pieces[variant.Row, variant.Column]);
                var kingPiece = _pieces.OfType<Piece>().First(p => p.GetChessMan() is King k && k.GetPlayer().GetTypePlayer() == chessMan.GetPlayer().GetTypePlayer());
                var isShah = enemyChessMen.Where(e => e.Piece != null).Any(c => c.GetMoveVariantsForCurrentPiece().Any(v => v.Row == kingPiece.GetRow() && v.Column == kingPiece.GetColumn()));
                if (!isShah)
                    finalVariants.Add(variant);
                RestoreFakeMove(fakeMove);
            }

            return finalVariants.Count != 0;
        }

        public IEnumerable<ChessMan> GetOtherAliveChessMen(TypePlayer typePlayer)
        {
            return _pieces.OfType<Piece>().Where(p => p.GetChessMan() != null && p.GetChessMan().GetPlayer().GetTypePlayer() != typePlayer)
                .Select(p => p.GetChessMan());
        }

        public Piece[,] GetPieces() => _pieces;
    }
}
