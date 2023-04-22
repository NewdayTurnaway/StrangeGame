using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class LevelProgressInfoView : BaseCanvasView
    {
        [field: SerializeField] public TMP_Text LevelNumber { get; private set; }
        [field: SerializeField] public TMP_Text Timer { get; private set; }
    }
}
