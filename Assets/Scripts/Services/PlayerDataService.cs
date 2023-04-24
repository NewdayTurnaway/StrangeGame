using System;
using UnityEngine;

namespace Services
{
    public sealed class PlayerDataService : IDisposable
    {
        private const string PLAYER_NAME = "PlayerName";
        private const string PLAYER_EMAIL = "PlayerEmail";
        private const string PLAYER_PASSWORD = "PlayerPassword";
        private const string STATISTICS_POSITION = "RecordPosition";
        private const string STATISTICS_SCORE = "RecordScore";

        public string PlayerName { get; private set; }
        public string PlayerEmail { get; private set; }
        public string PlayerPassword { get; private set; }
        public int RecordPosition { get; private set; }
        public int RecordScore { get; private set; }

        public bool IsLoginSuccess { get; private set; }

        public event Action<int> UpdateStatistics = _ => { };

        public PlayerDataService()
        {
            PlayerName = PlayerPrefs.GetString(PLAYER_NAME);
            PlayerEmail = PlayerPrefs.GetString(PLAYER_EMAIL);
            PlayerPassword = PlayerPrefs.GetString(PLAYER_PASSWORD);
            RecordScore = PlayerPrefs.GetInt(STATISTICS_SCORE);
            RecordPosition = PlayerPrefs.GetInt(STATISTICS_POSITION);
        }

        public void UpdateRecordScore(int newRecord)
        {
            RecordScore = newRecord;
        }

        public void ChangeLoginStatus(bool status)
        {
            IsLoginSuccess = status;
        }

        public void SetPlayer(string name, string email, string password, int position, int score)
        {
            PlayerName = name;
            PlayerEmail = email;
            PlayerPassword = password;
            RecordPosition = position;
            UpdateRecordScore(score);

            SetLoginPrefs();
        }

        public void UpdateRecord()
        {
            UpdateStatistics.Invoke(RecordScore);
        }

        public void ForgetPlayer()
        {
            PlayerName = "";
            PlayerEmail = "";
            PlayerPassword = "";
            
            SetLoginPrefs();
        }

        public void Dispose()
        {
            SetLoginPrefs();
        }

        private void SetLoginPrefs()
        {
            PlayerPrefs.SetString(PLAYER_NAME, PlayerName);
            PlayerPrefs.SetString(PLAYER_EMAIL, PlayerEmail);
            PlayerPrefs.SetString(PLAYER_PASSWORD, PlayerPassword);
            PlayerPrefs.GetInt(STATISTICS_POSITION, RecordPosition);
            PlayerPrefs.GetInt(STATISTICS_SCORE, RecordScore);
        }
    }
}