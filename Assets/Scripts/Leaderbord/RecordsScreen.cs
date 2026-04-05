using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Leaderboard
{
    public class RecordsScreen : MonoBehaviour
    {
        private const string Current = "current";
        private const string IsRecord = "isRecord";
        private const string Game = "game_";
        private const int True = 1;

        private const int OffsetY = -30;

        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _prefabRecord;
        [SerializeField] private CSVWriter _writer;

        private void Start()
        {
            GameObject record;
            List<RecordString> recordList = _writer.GetList();

            int lastRecord = Int32.Parse(PlayerPrefs.GetString(Current));
            int j = 0;

            while (PlayerPrefs.HasKey(Game + j))
            {
                record = Instantiate(_prefabRecord, new Vector2(0, 0 + (OffsetY * j)), Quaternion.identity);
                record.transform.SetParent(_canvas.transform, false);

                record.transform.GetChild(0).GetComponent<Text>().text = recordList[j].Date.ToString();
                record.transform.GetChild(1).GetComponent<Text>().text = recordList[j].Record.ToString();

                if (recordList[j].Record == lastRecord && PlayerPrefs.GetInt(IsRecord) == True)
                {
                    record.transform.GetComponent<YellowText>().SetYellowText();
                }

                j++;
            }
        }
    }
}