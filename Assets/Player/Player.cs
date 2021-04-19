using UnityEngine;

namespace UPlayer
{
    public class Player
    {
        private readonly TypePlayer _typePlayer;
        private readonly int _positiveYMovement;

        public Player(TypePlayer typePlayer, bool positiveYMovement)
        {
            _typePlayer = typePlayer;
            _positiveYMovement = positiveYMovement ? 1 : -1;
        }

        public int GetPositiveYMovement() => _positiveYMovement;

        public TypePlayer GetTypePlayer() => _typePlayer;
    }

    public enum TypePlayer
    {
        Black,
        White
    }
}