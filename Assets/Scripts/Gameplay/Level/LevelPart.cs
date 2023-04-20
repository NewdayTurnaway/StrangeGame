using System;

namespace Gameplay.Level
{
    public sealed class LevelPart : IDisposable
    {
        //private readonly Enemies _enemies;

        public LevelPartView LevelPartView { get; }

        public LevelPart(
            LevelPartView levelPartView
            )
        {
            LevelPartView = levelPartView;
            //_enemies = enemies;
        }

        public void Dispose()
        {
            //_enemies.Dispose();
        }
    }
}