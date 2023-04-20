using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Gameplay.Level.Generator
{

    public sealed class LevelGenerator
    {
        private readonly Random _random = new();

        private readonly Array _levelDirections;

        public LevelGenerator()
        {
            _levelDirections = Enum.GetValues(typeof(LevelDirection));
        }

        public List<Vector2> GenerateCoordinates(int levelPartCount)
        {
            var lastCoordinate = Vector2.zero;
            var lastDirection = PickRandomLevelDirection();

            var coordinates = new List<Vector2>
            {
                lastCoordinate
            };

            var count = 1;
            while (count < levelPartCount)
            {
                var levelDirection = PickRandomLevelDirection();

                if(CheckDirectionsOverlap(lastDirection, levelDirection))
                {
                    continue;
                }

                var coordinate = GetNewCoordinate(levelDirection, lastCoordinate);

                if (CheckCoordinatesOverlap(coordinates, coordinate))
                {
                    continue;
                }

                lastDirection = levelDirection;
                lastCoordinate = coordinate;
                coordinates.Add(coordinate);
                count++;
            }

            return coordinates;
        }

        private LevelDirection PickRandomLevelDirection()
        {
            var index = _random.Next(_levelDirections.Length);
            return (LevelDirection)_levelDirections.GetValue(index);
        }

        private bool CheckDirectionsOverlap(LevelDirection lastDirection, LevelDirection levelDirection)
        {
            var opositeDirection = lastDirection;
            switch (lastDirection)
            {
                case LevelDirection.Back:
                    opositeDirection = LevelDirection.Forward;
                    break;
                case LevelDirection.Left:
                    opositeDirection = LevelDirection.Right;
                    break;
                case LevelDirection.Right:
                    opositeDirection = LevelDirection.Left;
                    break;
                case LevelDirection.Forward:
                    opositeDirection = LevelDirection.Back;
                    break;
            }

            if(opositeDirection == levelDirection)
            {
                return true;
            }

            return false;
        }

        private Vector2 GetNewCoordinate(LevelDirection levelDirection, Vector2 lastCoordinate)
        {
            var direction = Vector2.zero;
            switch (levelDirection)
            {
                case LevelDirection.Back:
                    direction = new(0, -1);
                    break;
                case LevelDirection.Left:
                    direction = new(-1, 0);
                    break;
                case LevelDirection.Right:
                    direction = new(1, 0);
                    break;
                case LevelDirection.Forward:
                    direction = new(0, 1);
                    break;
            }

            return new Vector2(lastCoordinate.x + direction.x, lastCoordinate.y + direction.y);
        }

        private bool CheckCoordinatesOverlap(List<Vector2> coordinates, Vector2 coordinate)
        {
            foreach (var pastCoordinate in coordinates)
            {
                if (coordinate == pastCoordinate)
                {
                    return true;
                }
            }

            return false;
        }
    }
}