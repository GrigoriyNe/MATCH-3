using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Leaderboard
{
    public class CSVWriter : MonoBehaviour
    {
        private const string FilePath = @"data.csv";
        private const string Game = "game_";
        private const string Current = "current";
        private const string SeparateChar = ";";

        private List<RecordString> _recordList;

        private void Awake()
        {
            if(PlayerPrefs.GetString(Current) == null)
                PlayerPrefs.SetString(Current, 0.ToString());

            Read();
            InitRecordScreen();
        }

        public List<RecordString> GetList() => 
            _recordList;

        public void SetNewRecord(int value)
        {
            using StreamWriter writer = new StreamWriter(FilePath);

            PlayerPrefs.SetString(Game, DateTime.Now.ToString() + SeparateChar + value);
            PlayerPrefs.SetString(Current, value.ToString());
            writer.WriteLine(DateTime.Now.ToString() + SeparateChar + value);
        }

        private void Read()
        {
            StreamReader reader = new StreamReader(File.OpenRead(FilePath));

            int i = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                PlayerPrefs.SetString(Game + i, line);
                i++;
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
                _recordList.Add(new RecordString(values[0], int.Parse(values[1])));
                j++;
            }

            _recordList.Sort(delegate (RecordString x, RecordString y)
            {
                return y.Record.CompareTo(x.Record);
            });
        }
    }
}