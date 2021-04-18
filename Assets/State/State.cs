﻿namespace UState
{
    public class State
    {
        private GameState _gameState;

        public State(GameState gameState)
        {
            _gameState = gameState;
        }

        public GameState GetState() => _gameState;

        public void SetNoneState()
        {
            _gameState = GameState.None;
        }

        public void SetSelectState()
        {
            _gameState = GameState.Select;
        }

        public void SetMoveState()
        {
            _gameState = GameState.Move;
        }
    }

    public enum GameState
    {
        None,
        Select,
        Selected,
        Move,
        Moved
    }
}