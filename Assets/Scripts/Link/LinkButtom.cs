using UnityEngine;
using UnityEngine.UI;

public class LinkButtom : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] public string _patch;

    private void OnEnable() =>
        _button.onClick.AddListener(OnClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnClick);

    private void OnClick() =>
        Application.OpenURL(_patch);
}
