using UPiece;
using Сhessmen;

namespace UBoard
{
    public class FakeMove
    {
        public Piece NewPiece { get; }
        public ChessMan NewPieceChessMan { get; }
        public Piece SelectedPiece { get; }
        public ChessMan SelectedChessMan { get; }
        public bool Destroyed { get; private set; }

        public FakeMove(Piece newPiece, ChessMan newPieceChessMan, Piece selectedPiece, ChessMan selectedChessMan)
        {
            NewPiece = newPiece;
            NewPieceChessMan = newPieceChessMan;
            SelectedPiece = selectedPiece;
            SelectedChessMan = selectedChessMan;
        }

        public void SetDestroyed() => Destroyed = true;
    }
}