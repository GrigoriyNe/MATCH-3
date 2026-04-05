using UnityEngine;

[CreateAssetMenu(fileName = "Ball", menuName = "SwipeItems/Ball", order = 51)]
public class BallProperty : ScriptableObject
{
    [SerializeField] private Color _color;
    [SerializeField] private int _colorID;

    public Color Color => _color;
    public int ColorID => _colorID;
}
