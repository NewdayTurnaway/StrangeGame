using System;

namespace Gameplay.Level
{
    public sealed class LevelPart : IDisposable
    {
        //private readonly Enemies _enemies;

        public LevelPartView LevelPartView { get; }

        public event Action<LevelPart> LevelPartChanged = _ => { };

        public LevelPart(
            LevelPartView levelPartView
            )
        {
            LevelPartView = levelPartView;
            //_enemies = enemies;

            LevelPartView.PlayerInThisLevelPart += PlayerChangeLevelPart;
        }

        public void Dispose()
        {
            //_enemies.Dispose();

            LevelPartView.PlayerInThisLevelPart -= PlayerChangeLevelPart;
        }

        private void PlayerChangeLevelPart()
        {
            LevelPartChanged.Invoke(this);
        }
    }
}