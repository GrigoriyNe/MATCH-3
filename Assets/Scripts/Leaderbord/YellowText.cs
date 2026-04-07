using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public class YellowText : MonoBehaviour
    {
        public void SetYellowText()
        {
            gameObject.transform.GetChild(0).GetComponent<Text>().color = Color.yellow;
            gameObject.transform.GetChild(1).GetComponent<Text>().color = Color.yellow;
        }
    }
}
