using System;
using UnityEngine;
using UnityEngine.UI;

namespace Navigator
{
    public class NavigatorButton : MonoBehaviour
    {
        [SerializeField] private Button _activateButton;
        [SerializeField] private string _nameScene;

        public event Action<string> Activated;

        private void OnEnable() =>
            _activateButton.onClick.AddListener(OnClick);

        private void OnDisable() =>
            _activateButton.onClick.RemoveListener(OnClick);

        private void OnClick() =>
            Activated?.Invoke(_nameScene);
    }
}