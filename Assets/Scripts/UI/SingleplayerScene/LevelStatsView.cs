using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class LevelStatsView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text LevelNumber { get; private set; }
        [field: SerializeField] public TMP_Text Timer { get; private set; }
        [field: SerializeField] public TMP_Text EnemiesCount { get; private set; }
        [field: SerializeField] public TMP_Text Score { get; private set; }
        [field: SerializeField] public TMP_Text RecordScore { get; private set; }
    }
}
