using UnityEngine;

namespace GameLoop
{
    public class BallFabric : MonoBehaviour
    {
        [SerializeField] private BallProperty[] _propertys;
        [SerializeField] private Ball _prefab;
        [SerializeField] private Transform _gridParent;

        public Ball Create()
        {
            BallProperty property = _propertys[Random.Range(0, _propertys.Length)];
            Ball ball = Instantiate(_prefab).GetComponent<Ball>();
            ball.SetColor(property.Color);
            ball.SetColorId(property.ColorID);
            ball.transform.SetParent(_gridParent);

            return ball;
        }
    }
}