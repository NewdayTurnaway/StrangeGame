using UnityEngine;

namespace Gameplay.Player
{
	[RequireComponent(typeof(Rigidbody))]
	public sealed class PlayerView : MonoBehaviour
	{
		[field: SerializeField] public Rigidbody Rigidbody { get; private set; }
		[field: SerializeField] public Transform Head { get; private set; }

		private void OnValidate()
		{
			Rigidbody ??= GetComponent<Rigidbody>();
		}
	}
}
