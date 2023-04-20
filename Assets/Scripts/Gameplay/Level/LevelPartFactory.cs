using UnityEngine;
using Zenject;

namespace Gameplay.Level
{
    public sealed class LevelPartFactory : PlaceholderFactory<Vector3, LevelPartView, LevelPart>
    {
        private readonly DiContainer _diContainer;
        private readonly Transform _environmentTransform;

        public LevelPartFactory(DiContainer diContainer, Transform environmentTransform)
        {
            _diContainer = diContainer;
            _environmentTransform = environmentTransform;
        }

        public override LevelPart Create(Vector3 position, LevelPartView levelPartView)
        {
            var view = _diContainer.InstantiatePrefabForComponent<LevelPartView>(levelPartView, _environmentTransform);
            view.transform.position = position;
            return new(view);
        }
    }
}