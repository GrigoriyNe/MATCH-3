using UnityEngine;
using TMPro;

namespace GameLoop
{
    public class StepsView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textValue;

        public void ChangeValue(int value)
        {
            _textValue.text = value.ToString();
        }
    }
}