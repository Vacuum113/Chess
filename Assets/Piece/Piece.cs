using UBoard;
using UnityEngine;

namespace UPiece
{
    public class Piece : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject _chessMan;
        private int _row;
        private int _column;
        
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            CheckClick();
        }
        
        private void CheckClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var mousePos2D = new Vector2(mousePos.x, mousePos.y);

                var hit = Physics2D.Raycast(mousePos2D, Vector2.zero, 20, 1 << LayerMask.NameToLayer("Default"));
                var collider = gameObject.GetComponent<BoxCollider2D>();
                var board = FindObjectOfType<Board>();
                var selectedPiece = board.GetSelectedPiece();
                if (hit.transform != null && hit.collider == collider && _chessMan != null)
                {
                    board.SetSelectedPiece(gameObject);
                }
                else if (hit.transform != null && hit.collider == collider)
                {
                    if (selectedPiece != null && selectedPiece.transform != hit.transform)
                        board.ResetSelect();
                }
            }
        }

        public void SetChessMan(GameObject chessMan) => _chessMan = chessMan;
    
        public GameObject GetChessMan() => _chessMan;

        public void SetRow(int row) => _row = row;

        public void SetColumn(int column) => _column = column;
        
        public int GetRow() => _row;

        public int GetColumn() => _column;
    }
}
