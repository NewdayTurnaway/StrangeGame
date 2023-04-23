using Gameplay.Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
    [RequireComponent(typeof(BoxCollider))]
    public sealed class LevelPartView : MonoBehaviour
    {
        [field: SerializeField] public BoxCollider LevelPartCollider { get; private set; }
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
        [field: SerializeField] public List<Transform> EnemySpawnPoints { get; private set; }

        public event Action PlayerInThisLevelPart = () => { };
        public event Action<PlayerView> PlayerViewRecived = _ => { };
        public event Action PlayerLeftThisLevelPart = () => { };

        private void OnValidate()
        {
            LevelPartCollider ??= GetComponent<BoxCollider>();
            LevelPartCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.TryGetComponent(out PlayerView playerView))
            {
                PlayerInThisLevelPart.Invoke();
                PlayerViewRecived.Invoke(playerView);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.TryGetComponent<PlayerView>(out _))
            {
                PlayerLeftThisLevelPart.Invoke();
            }
        }
    } 
}
