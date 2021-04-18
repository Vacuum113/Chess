using System;
using UBoard;
using UnityEditor;
using UnityEngine;
using UPiece;
using UPlayer;
using UState;

namespace UInspector
{
    public class Inspector : MonoBehaviour
    {
        public static Inspector Instance;
        
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

        private Board BoardInstance;
        
        private State _gameState;
        
        private Player _bPlayer;
        private Player _wPlayer;

        private Player _currentPlayer;
        private Player _otherPlayer;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            Validation();
            _gameState = new State(GameState.None);
            
            _bPlayer = new Player(TypePlayer.Black);
            _wPlayer = new Player(TypePlayer.White);
            _currentPlayer = _wPlayer;
            _otherPlayer = _bPlayer;
            
            BoardInstance = Instantiate(Board).GetComponent<Board>();
            
            BoardInstance.InitialBoard();
            
            BoardInstance.SetPiece(Instantiate(WRook), 0, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WKnight), 1, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WBishop), 2, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WQueen), 3, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WKing), 4, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WBishop), 5, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WKnight), 6, 0, _wPlayer);
            BoardInstance.SetPiece(Instantiate(WRook), 7, 0, _wPlayer);
            for (int i = 1; i < 8; i++)
            {
                BoardInstance.SetPiece(Instantiate(WPawn), i, 1, _wPlayer);
            }
            
            BoardInstance.SetPiece(Instantiate(BRook), 0, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BKnight), 1, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BBishop), 2, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BQueen), 3, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BKing), 4, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BBishop), 5, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BKnight), 6, 7, _bPlayer);
            BoardInstance.SetPiece(Instantiate(BRook), 7, 7, _bPlayer);
            for (int i = 0; i < 8; i++)
            {
                BoardInstance.SetPiece(Instantiate(BPawn), i, 6, _bPlayer);
            }
        }

        private void Validation()
        {
            if (Board == null) {
                Debug.LogError("assign the gameobject Board in the inspector before resuming");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        public void TryUpdateState(Piece piece)
        {
            switch (_gameState.GetState())
            {
                case GameState.None:
                    NoneState(piece);
                    break;
                case GameState.Select:
                    SelectState(piece);
                    break;
                case GameState.Move:
                    MoveState(piece);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void NoneState(Piece piece)
        {
            var chessMan = piece.GetChessMan();
            if (chessMan != null && chessMan.GetPlayer().GetTypePlayer() != _currentPlayer.GetTypePlayer())
                return;

            NoneToSelect(piece);
        }

        private void SelectState(Piece piece)
        {
            if (piece.GetCanMove() == true)
                SelectToMove(piece);
            else
            {
                if (piece.GetChessMan()) 
                    SelectToSelect(piece);
                else
                    SelectToNone(piece);
            }
        }

        private void SelectToSelect(Piece piece)
        {
            var chessMan = piece.GetChessMan();
            if (chessMan != null && chessMan.GetPlayer().GetTypePlayer() != _currentPlayer.GetTypePlayer())
            {
                BoardInstance.ResetSelect();
                return;
            }
            BoardInstance.ResetSelect();
            BoardInstance.SetSelectedPiece(piece);
            BoardInstance.MarkCanMovePieces();
        }

        private void MoveState(Piece piece)
        {
            // if ()
            //     MoveToNone(piece, board);
            // else
            //     MoveToSelect(piece, board);
        }

        private void NoneToSelect(Piece piece)
        {
            if (piece.GetChessMan())
            {
                BoardInstance.SetSelectedPiece(piece);
                BoardInstance.MarkCanMovePieces();
                
                _gameState.SetSelectState();
            }
        }
        
        private void SelectToNone(Piece piece)
        {
            BoardInstance.ResetSelect();
            _gameState.SetNoneState();
        }

        private void SelectToMove(Piece piece)
        {
            BoardInstance.MoveChessMan(piece);
            BoardInstance.ResetSelect();
            SwapPlayers();
            _gameState.SetNoneState();
        }

        private void MoveToSelect(Piece piece)
        {
            throw new NotImplementedException();
        }

        private void MoveToNone(Piece piece)
        {
            
            _gameState.SetNoneState();
        }
        
        private void SwapPlayers()
        {
            var currentPlayer = _currentPlayer;
            _currentPlayer = _otherPlayer;
            _otherPlayer = currentPlayer;
        }
        
    }
}
