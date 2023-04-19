using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = nameof(CameraConfig), menuName = "Configs/" + nameof(CameraConfig))]
    public sealed class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float SensitivityX { get; private set; } = 250;
        [field: SerializeField] public float SensitivityY { get; private set; } = 250;
    }
}