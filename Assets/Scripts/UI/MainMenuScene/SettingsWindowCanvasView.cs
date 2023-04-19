using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace UI
{
    public sealed class SettingsWindowCanvasView : BaseMenuWindowCanvasView
    {
        [field: Header("Graphics")]
        [field:SerializeField] public Toggle FullscreenToggle { get; private set; }
        [field:SerializeField] public TMP_Dropdown ResolutionDropdown { get; private set; }
        [field:SerializeField] public TMP_Dropdown QualityDropdown { get; private set; }


        [field: Header("Audio")]
        [field:SerializeField] public Slider  VolumeSlider  { get; private set; }
    }
}
