using System;
using System.Collections.Generic;

namespace SnakeWars.SampleBot
{
    public class GameStateDTO
    {
        public int Turn { get; set; }
        public IEnumerable<SnakeDTO> Snakes { get; set; }
        public IEnumerable<PointDTO> Walls { get; set; }
        public IEnumerable<PointDTO> Food { get; set; }
        public SizeDTO BoardSize { get; set; }
        public int TurnTimeMilliseconds { get; set; }
    }

    public class SnakeDTO
    {
        public IEnumerable<PointDTO> Cells { get; set; }
        public string Id { get; set; }
        public PointDTO Head { get; set; }
        public bool IsAlive { get; set; }
        public SnakeDirection Direction { get; set; }
        public int Score { get; set; }
        public int Weight { get; set; }
        public int MaxWeight { get; set; }
    }

    public enum SnakeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public struct SizeDTO
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public struct PointDTO : IEquatable<PointDTO>
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int DistanceTo(PointDTO point, SizeDTO boardSize)
        {
            var distance = Math.Abs(point.X - this.X) + Math.Abs(point.Y - this.Y);

            var half = boardSize.Height/2 + boardSize.Width/2;
            return distance;
            ;
            if (distance > half)
            {
                return half - distance;
            }

            return distance;            
        }

        public bool Equals(PointDTO other) => other.X == this.X && other.Y == this.Y;
    }
}