using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeWars.SampleBot
{
    internal class GameBoardState
    {
        private readonly GameStateDTO _gameState;

        public SizeDTO BoardSize() => _gameState.BoardSize;

        public GameBoardState(GameStateDTO gameState)
        {
            _gameState = gameState;
        }

        public PointDTO GetSnakeNewHeadPosition(string snakeId, Move move)
        {
            var snake = GetSnake(snakeId);
            var newHead = move.GetSnakeNewHead(snake, _gameState.BoardSize);
            return newHead;
        }
        
        public HashSet<PointDTO> GetOccupiedCells()
        {
            return new HashSet<PointDTO>(_gameState.Walls.Concat(_gameState.Snakes.SelectMany(snake => snake.Cells)));
        }

        public IEnumerable<SnakeDTO> GetSnakes()
        {
            return _gameState.Snakes;
        } 

        public SnakeDTO GetSnake(string snakeId)
        {
            return _gameState.Snakes.First(s => s.Id == snakeId);
        }

        public IEnumerable<PointDTO> GetFood()
        {
            return this._gameState.Food;
        } 
    }
}