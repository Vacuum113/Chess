using System;
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
        private Piece[,] _pieceReference;
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

        public void SetPiece(GameObject chessMan, int column, int row, Player player)
        {
            var piece = _pieceReference[row, column];
            var pieceTransform = piece.transform.position;
            chessMan.transform.position = new Vector3(pieceTransform.x, pieceTransform.y, -2);
            chessMan.GetComponent<ChessMan>().SetPlayer(player);
            var pieceObject = piece.GetComponent<Piece>();
            pieceObject.SetChessMan(chessMan.GetComponent<ChessMan>());
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
            _pieceReference = new Piece[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var y = 0; y < ColumnCount; y++)
                {
                    _pieceReference[i, y] = InitialPiece(y, i);
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
            var variantsPieces = _pieceReference.OfType<Piece>().Where(p => p.GetComponent<Renderer>().material.name != DefaultPiece.GetComponent<Renderer>().material.name).ToList();
            foreach (var variantsPiece in variantsPieces)
            {
                variantsPiece.ResetPiece(Piece.GetComponent<Renderer>().sharedMaterial);
            }
        }

        public void MoveChessMan(Piece newPiece)
        {
            var selectedPiece = SelectedPiece.GetComponent<Piece>();
            var selectedChessMan = selectedPiece.GetChessMan();
            
            newPiece.SetChessMan(selectedChessMan);
            selectedPiece.SetChessMan(null);

            var transformSelected = selectedChessMan.transform;
            var newPiecePosition = newPiece.transform.position;
            
            transformSelected.position = new Vector3(newPiecePosition.x, newPiecePosition.y, transformSelected.position.z);
        }

        public void MarkCanMovePieces()
        {
            var piece = SelectedPiece.GetComponent<Piece>();
            piece.SetSelectedMaterial();
            var chessMan = piece.GetChessMan();
            var variants = chessMan.GetMoveVariants(piece.GetRow(), piece.GetColumn(), _pieceReference);
            
            foreach (var variant in variants)
            {
                var pieceVariant = _pieceReference[variant.Row, variant.Column];
                pieceVariant.SetCanMove(!pieceVariant.GetChessMan());
            }
        }
    }
}
