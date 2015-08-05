using System;
using System.Collections.Generic;
using System.Linq;

namespace SnakeWars.SampleBot
{
    internal class SnakeEngine
    {
        private readonly string _mySnakeId;
        private readonly Random _random = new Random();

        public SnakeEngine(string mySnakeId)
        {
            _mySnakeId = mySnakeId;
        }

        public Move GetNextMove(GameBoardState gameBoardState)
        {
            //===========================
            // Your snake logic goes here
            //===========================

            var mySnake = gameBoardState.GetSnake(_mySnakeId);

            Console.Title = $"Alive: {mySnake.IsAlive} Direction: {mySnake.Direction}";

            if (mySnake.IsAlive)
            {
                var occupiedCells = gameBoardState.GetOccupiedCells();

                // Check possible moves in random order.
                
                var nearestFood = gameBoardState.GetFood()
                    .OrderBy(x => x.DistanceTo(mySnake.Head, gameBoardState.BoardSize()))
                    .Select(x => (PointDTO?)x)
                    .FirstOrDefault();

                if (nearestFood != null)
                {
                    Console.WriteLine("Food at {0}, {1}", nearestFood?.X, nearestFood?.Y);

                    var bestMove = moves
                        .Select(x => new { Move = x, NewHead = gameBoardState.GetSnakeNewHeadPosition(_mySnakeId, x) })
                        .Where(x => !occupiedCells.Contains(x.NewHead))
                        .Where(x => GetHeadsNearPoint(gameBoardState, x.NewHead).Count() == 1)
                        .OrderBy(x => x.NewHead.DistanceTo(nearestFood.Value, gameBoardState.BoardSize()))
                        .FirstOrDefault();

                    if (bestMove != null)
                    {
                        return bestMove.Move;
                    }
                    else
                    {
                        Console.WriteLine("DUPA nie ma gdzie iœæ");
                    }
                }

                var moves = new List<Move>
                {
                    Move.Left,
                    Move.Right,
                    Move.Straight
                };


                while (moves.Any())
                {
                    // Select random move.
                    var move = moves[_random.Next(moves.Count)];
                    moves.Remove(move);

                    var newHead = gameBoardState.GetSnakeNewHeadPosition(_mySnakeId, move);

                    var nearHeads = GetHeadsNearPoint(gameBoardState, newHead);

                    if (nearHeads.Count() == 1)
                    {
                        if (!occupiedCells.Contains(newHead))
                        {
                            return move;
                        }
                    }
                }
                moves = new List<Move>
                {
                    Move.Left,
                    Move.Right,
                    Move.Straight
                };


                while (moves.Any())
                {
                    // Select random move.
                    var move = moves[_random.Next(moves.Count)];
                    moves.Remove(move);

                    var newHead = gameBoardState.GetSnakeNewHeadPosition(_mySnakeId, move);

                    if (!occupiedCells.Contains(newHead))
                    {
                        return move;
                    }
                }
            }
            return Move.None;
        }

        private IEnumerable<SnakeDTO> GetHeadsNearPoint(GameBoardState state, PointDTO point)
        {
            var surrounding = Move.Offsets.Select(x => Move.OffsetModulo(point, x.Offset, state.BoardSize())).ToArray();

            return state.GetSnakes()
                .Where(x => x.IsAlive)
                .Where(x => surrounding.Contains(x.Head));

        }
    }
}