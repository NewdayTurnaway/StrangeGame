using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class RecordContainerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _username;
        [SerializeField] private TMP_Text _score;

        public void Init(int number, string username, int score)
        {
            _number.text = number.ToString();
            _username.text = username;
            _score.text = score.ToString();
        }
    }
}
