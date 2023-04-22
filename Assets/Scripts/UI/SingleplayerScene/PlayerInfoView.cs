using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PlayerInfoView : BaseCanvasView
    {
        [field: SerializeField] public TMP_Text PlayerName { get; private set; }
        [field: SerializeField] public Slider HealthSlider { get; private set; }
        [field: SerializeField] public Color ColorActive { get; private set; }
        [field: SerializeField] public Color ColorNotActive { get; private set; }
        [field: SerializeField] public Image FristAbilityIcon { get; private set; }
        [field: SerializeField] public TMP_Text FristAbilityCount { get; private set; }
        [field: SerializeField] public Image SecondAbilityIcon { get; private set; }
        [field: SerializeField] public TMP_Text SecondAbilityCount { get; private set; }
    }
}
