using System;
using UnityEngine;

namespace Services
{
    public sealed class PlayerDataService : IDisposable
    {
        private const string PLAYER_NAME = "PlayerName";
        private const string PLAYER_EMAIL = "PlayerEmail";
        private const string PLAYER_PASSWORD = "PlayerPassword";

        public string PlayerName { get; private set; }
        public string PlayerEmail { get; private set; }
        public string PlayerPassword { get; private set; }
        public bool IsLoginSuccess { get; private set; }

        public PlayerDataService()
        {
            PlayerName = PlayerPrefs.GetString(PLAYER_NAME);
            PlayerEmail = PlayerPrefs.GetString(PLAYER_EMAIL);
            PlayerPassword = PlayerPrefs.GetString(PLAYER_PASSWORD);
        }

        public void ChangeLoginStatus(bool status)
        {
            IsLoginSuccess = status;
        }

        public void SetPlayer(string name, string email, string password)
        {
            PlayerName = name;
            PlayerEmail = email;
            PlayerPassword = password;

            SetLoginPrefs();
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
        }
    }
}