using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class RecordsWindowCanvasView : BaseMenuWindowCanvasView
    {
        [field: SerializeField] public Button RefreshButton { get; private set; }
        [field: SerializeField] public RectTransform ContentRectTransform { get; private set; }
        [field: SerializeField] public RecordContainerView PlayerRecordContainerView { get; private set;}
        [field: SerializeField] public RecordContainerView RecordContainerView { get; private set; }
    }
}
