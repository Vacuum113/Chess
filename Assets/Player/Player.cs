using UnityEngine;

namespace UPlayer
{
    public class Player
    {
        private TypePlayer _typePlayer;
        public Player(TypePlayer typePlayer)
        {
            _typePlayer = typePlayer;
        }

        public TypePlayer GetTypePlayer() => _typePlayer;
    }

    public enum TypePlayer
    {
        Black,
        White
    }
}