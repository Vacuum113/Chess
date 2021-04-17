using System;
using System.Linq;
using UnityEngine;
using UPiece;
using Сhessmen;

namespace UBoard
{
    public class Board : MonoBehaviour
    {
        private GameObject[,] _pieceReference;
        public GameObject Piece;
        private const int RowCount = 8;
        private const int ColumnCount = 8;
        public Material CanMove;
        public Material CannotMove;
        public Material SelectedChess;
        private readonly float _firstPiecePositionX = 0;
        private readonly float _firstPiecePositionY = 0;
        public float Step = 9.91f;

        private GameObject SelectedPiece;
        // Start is called before the first frame update
        void Start()
        {
            Validation();
        }

        // Update is called once per frame
        void Update()
        {
            if (SelectedPiece != null)
            {
                var piece = SelectedPiece.GetComponent<Piece>();
                piece.GetComponent<Renderer>().material = SelectedChess;
                var chessMan = piece.GetChessMan().GetComponent<ChessMan>();
                var variants = chessMan.GetMoveVariants(piece.GetRow(), piece.GetColumn(), _pieceReference);
                foreach (var variant in variants)
                {
                    var pieceVariant = _pieceReference[variant.Row, variant.Column];
                    
                    pieceVariant.GetComponent<Renderer>().material = pieceVariant.GetComponent<Piece>().GetChessMan() != null ? CannotMove : CanMove;
                }
            }
        }

        public void SetPiece(GameObject chessMan, int column, int row)
        {
            var piece = _pieceReference[row, column];
            var pieceTransform = piece.transform.position;
            chessMan.transform.position = new Vector3(pieceTransform.x, pieceTransform.y, -2);
            var pieceObject = piece.GetComponent<Piece>();
            pieceObject.SetRow(row);
            pieceObject.SetColumn(column);
            pieceObject.SetChessMan(chessMan);
        } 
        
        private void Validation()
        {
            if (Piece == null) {
                Debug.LogError("assign the gameobject CanMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }

            
            if (CanMove == null) {
                Debug.LogError("assign the gameobject CanMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }

            if (CannotMove == null)
            {
                Debug.LogError("assign the gameobject CannotMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        public void InitialBoard()
        {
            _pieceReference = new GameObject[RowCount, ColumnCount];
            for (var i = 0; i < RowCount; i++)
            {
                for (var y = 0; y < ColumnCount; y++)
                {
                    _pieceReference[i, y] = InitialPiece(y, i);
                }
            }
        }
        
        private GameObject InitialPiece(int column, int row)
        {
            var piece = Instantiate(Piece,  new Vector3(_firstPiecePositionX + column * Step, _firstPiecePositionY + row * Step, -1), Quaternion.identity, new RectTransform());
            var position = piece.transform.position;
            position.Set(position.x, -2, position.z);
            return piece;
        }

        public void SetSelectedPiece(GameObject piece) => SelectedPiece = piece;

        public GameObject GetSelectedPiece() => SelectedPiece;

        public void ResetSelect()
        {
            ResetPiecesMaterial();
            SetSelectedPiece(null);
        }

        private void ResetPiecesMaterial()
        {
            var variantsPieces = _pieceReference.OfType<GameObject>().Where(p => p.GetComponent<Renderer>().material != null);
            foreach (var variantsPiece in variantsPieces)
            {
                variantsPiece.GetComponent<Renderer>().material = null;
            }

            SelectedPiece.GetComponent<Renderer>().material = null;
        }
    }
}
