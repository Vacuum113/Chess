using UBoard;
using UnityEngine;
using UPlayer;

namespace UInspector
{
    public class Inspector : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject Board;
        
        public GameObject BBishop;
        public GameObject BKing;
        public GameObject BKnight;
        public GameObject BQueen;
        public GameObject BRook;
        public GameObject BPawn;
        
        public GameObject WBishop;
        public GameObject WKing;
        public GameObject WKnight;
        public GameObject WQueen;
        public GameObject WRook;
        public GameObject WPawn;

        private Player _bPlayer;
        private Player _wPlayer;

        private Player _currentPlayer;
        private Player _otherPlayer;
        
        
        void Start()
        {
            Validation();

            _bPlayer = new Player(TypePlayer.Black);
            _wPlayer = new Player(TypePlayer.White);
            _currentPlayer = _wPlayer;
            _otherPlayer = _bPlayer;
            
            var board = Instantiate(Board).GetComponent<Board>();
            board.InitialBoard();
            
            board.SetPiece(Instantiate(WRook), 0, 0);
            board.SetPiece(Instantiate(WKnight), 1, 0);
            board.SetPiece(Instantiate(WBishop), 2, 0);
            board.SetPiece(Instantiate(WQueen), 3, 0);
            board.SetPiece(Instantiate(WKing), 4, 0);
            board.SetPiece(Instantiate(WBishop), 5, 0);
            board.SetPiece(Instantiate(WKnight), 6, 0);
            board.SetPiece(Instantiate(WRook), 7, 0);
            for (int i = 0; i < 8; i++)
            {
                board.SetPiece(Instantiate(WPawn), i, 1);
            }
            
            board.SetPiece(Instantiate(BRook), 0, 7);
            board.SetPiece(Instantiate(BKnight), 1, 7);
            board.SetPiece(Instantiate(BBishop), 2, 7);
            board.SetPiece(Instantiate(BQueen), 3, 7);
            board.SetPiece(Instantiate(BKing), 4, 7);
            board.SetPiece(Instantiate(BBishop), 5, 7);
            board.SetPiece(Instantiate(BKnight), 6, 7);
            board.SetPiece(Instantiate(BRook), 7, 7);
            for (int i = 0; i < 8; i++)
            {
                board.SetPiece(Instantiate(BPawn), i, 6);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    
        private void Validation()
        {
            if (Board == null) {
                Debug.LogError("assign the gameobject Board in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
}
