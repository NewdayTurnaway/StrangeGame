using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Level
{
	[RequireComponent(typeof(BoxCollider))]
	public sealed class LevelPartView : MonoBehaviour
	{
		[SerializeField] private BoxCollider _boxCollider;
		[field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
		[field: SerializeField] public List<Transform> EnemySpawnPoints { get; private set; }

		public event Action PlayerInThisLevelPart = () => { };

        private void OnValidate()
		{
			_boxCollider ??= GetComponent<BoxCollider>();
            _boxCollider.isTrigger = true;
        }

		private void OnTriggerEnter(Collider other)
		{
			//CheckCollider
			PlayerInThisLevelPart.Invoke();
		}
	} 
}
