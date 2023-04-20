using UnityEngine;
using Zenject;

namespace Gameplay.Level
{
    public sealed class LevelPartFactory : PlaceholderFactory<Vector3, LevelPartView, LevelPart>
    {
        private readonly DiContainer _diContainer;

        public LevelPartFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public override LevelPart Create(Vector3 position, LevelPartView levelPartView)
        {
            var view = _diContainer.InstantiatePrefabForComponent<LevelPartView>(levelPartView);
            view.transform.position = position;
            return new(view);
        }
    }
}