using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LoadingCanvasView : BaseCanvasView
	{
        [field: SerializeField] public Slider LoadingSlider { get; private set; }
    }
}
