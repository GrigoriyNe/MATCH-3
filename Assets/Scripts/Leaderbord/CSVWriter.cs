using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Leaderboard
{
    public class CSVWriter : MonoBehaviour
    {
        private const string Game = "game_";
        private const string Data = "data";
        private const string Current = "current";
        private const string SeparateChar = ";";
        private const string NewString = "\r";

        private List<RecordString> _recordList;

        private int _countLines;
        private void Awake()
        {
            if (PlayerPrefs.GetString(Current) == null
                || PlayerPrefs.GetString(Current) == string.Empty)
                PlayerPrefs.SetString(Current, "0");

            Read();
            InitRecordScreen();
        }

        public List<RecordString> GetList() =>
            _recordList;

        public void SetNewRecord(int value)
        {
            PlayerPrefs.SetString(Game + _countLines, DateTime.Now.ToString()
                + SeparateChar + value);
            PlayerPrefs.SetString(Current, value.ToString());
            PlayerPrefs.Save();

            string textContent = DateTime.Now.ToString() + SeparateChar + value + NewString;

            TextAsset dataAsset = Resources.Load<TextAsset>(Data);
            TextAsset newTextAsset = new TextAsset(dataAsset + textContent);
            dataAsset = newTextAsset;
        }

        private void Read()
        {
            List<string> newLines = new List<string>();

            TextAsset DataAsset = Resources.Load<TextAsset>(Data);
            string csvText = DataAsset.text;
            string[] records = csvText.Split(NewString);

            for (int i = 0; i < records.Length; i++)
            {
                if (i > 0)
                {
                    newLines.Add(records[i].Remove(0, 1));
                }
                else
                {
                    newLines.Add(records[i]);
                }

            }

            _countLines = newLines.Count;

            for (int i = 0; i < newLines.Count; i++)
            {
                PlayerPrefs.SetString(Game + i, newLines[i]);
            }
        }

        private void InitRecordScreen()
        {
            _recordList = new List<RecordString>();

            int j = 0;

            while (PlayerPrefs.HasKey(Game + j))
            {
                string line = PlayerPrefs.GetString(Game + j);
                string[] values = line.Split(SeparateChar);

                if (values.Length > 1)
                {
                    _recordList.Add(new RecordString(values[0], int.Parse(values[1])));
                }

                j++;
            }

            _recordList.Sort(delegate (RecordString x, RecordString y)
            {
                return y.Record.CompareTo(x.Record);
            });
        }
    }
}