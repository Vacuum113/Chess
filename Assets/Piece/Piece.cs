using UBoard;
using UInspector;
using UnityEngine;
using Сhessmen;

namespace UPiece
{
    public class Piece : MonoBehaviour
    {
        public Material CanMoveMaterial;
        public Material CannotMoveMaterial;
        public Material SelectedChessMaterial;

        private ChessMan _chessMan;
        private int _row;
        private int _column;
        private bool? _canMove;
        private bool? _selected;

        void Start()
        {
            Validate();
        }
        
        void Update()
        {
            CheckClick();
        }
        
        private void CheckClick()
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(mousePos, Vector2.zero, 20, 1 << LayerMask.NameToLayer("Default"));
            if (hit)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.transform && hit.transform == transform)
                        Inspector.Instance.TryUpdateState(this);
                }
            }
        }

        public void SetChessMan(ChessMan chessMan)
        {
            _chessMan = chessMan;
        }

        public ChessMan GetChessMan() => _chessMan;

        public void SetRow(int row) => _row = row;

        public void SetColumn(int column) => _column = column;
        
        public int GetRow() => _row;

        public int GetColumn() => _column;

        public void SetSelectedMaterial()
        {
            _selected = true;
            gameObject.GetComponent<Renderer>().material = SelectedChessMaterial;
        }

        public void SetCanMove()
        {
            _canMove = true;
            gameObject.GetComponent<Renderer>().material = CanMoveMaterial;
        }
        
        public void SetCanDestroyChessMan()
        {
            _canMove = true;
            gameObject.GetComponent<Renderer>().material = CannotMoveMaterial;
        }

        public bool? GetCanMove() => _canMove;
        
        private void Validate()
        {
            if (CanMoveMaterial == null) {
                Debug.LogError("assign the gameobject CanMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }

            if (CannotMoveMaterial == null)
            {
                Debug.LogError("assign the gameobject CannotMove in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
            
            if (SelectedChessMaterial == null)
            {
                Debug.LogError("assign the gameobject SelectedPiece in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        public void ResetPiece(Material sharedMaterial)
        {
            _canMove = null;
            _selected = null;
            GetComponent<Renderer>().sharedMaterial = sharedMaterial;
        }

        public void DestroyChessMan()
        {
            Destroy(_chessMan.gameObject);
        }
    }
}
