using AlexDev.CatchMe.Data;
using ExitGames.Client.Photon;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace AlexDev.CatchMe
{
    public class PlayersBase
    {

        #region Private Fields

        private List<PlayerData> _playersList;

        #endregion

        public PlayersBase()
        {
            _playersList = new List<PlayerData>();
        }

        public PlayersBase(Player[] players)
        {
            _playersList = new List<PlayerData>();
            AddPlayers(players);
        }

        #region Events

        public event Action<int, string, int> PlayerJoinedEvent;
        public event Action<int, int, bool> PlayerStateUpdatedEvent;
        public event Action<int> PlayerRemovedEvent;

        #endregion

        #region Public Methods

        public void UpdatePlayerStates(Player upadatedPlayer, Hashtable changedProps)
        {
            PlayerData player = _playersList.Find(user => user.actorNumber == upadatedPlayer.ActorNumber);
            if (player != null)
            {
                string txt = upadatedPlayer.NickName + " ";
                foreach (object property in changedProps.Keys)
                {
                    txt += property.ToString() + changedProps[property] + "|";
                }
                Debug.Log(txt);
            }
        }

        public void AddPlayer(Player player)
        {
            var playerData = MapFromPhoton(player);
            int idx = _playersList.FindIndex(u => u.actorNumber == playerData.actorNumber);
            if (idx >= 0)
                _playersList[idx] = playerData;
            else
                _playersList.Add(playerData);
            //_playersList.Add(player);
        }

        public void AddPlayers(Player[] players)
        {
            foreach (var player in players)
            {
                AddPlayer(player);
            }
        }

        public void RemovePlayer(int playerActorNumber)
        {
            int playersRemoved = _playersList.RemoveAll(player => player.actorNumber == playerActorNumber);
            if (playersRemoved == 0)
            {
                Debug.Log("PlayerBase. Have no player with this actorNumber " + playerActorNumber);
            }
        }

        #endregion

        #region Private Methods

        private static void InitializePlayerCustomProperties(Player player)
        {
            Hashtable hash = new Hashtable()
            {
                { "ActorNumber", player.ActorNumber },
                { "Color", PlayerColors.GetNewColor },
                { "TimeMark", 0 },
                { "IsTagger", 0 }
            };
            player.SetCustomProperties(hash);
            Debug.Log(player.NickName + " " + player.CustomProperties["Color"] + hash["Color"]);
        }

        private void SetTimeMarkToPlayer(int playerActorNumber, int timeMark, bool isTagger)
        {
            var player = _playersList.Find(player => player.actorNumber == playerActorNumber);
            if (player != null)
            {
                player.catchTimeMark = timeMark;
                player.isTagger = isTagger;
            }
            else
            {
                Debug.Log("PlayerBase. Wrong actorNumber " + playerActorNumber);
            }
        }

        private static PlayerData MapFromPhoton(Player photonPlayer)
        {
            var pd = new PlayerData(
                playerName: string.IsNullOrEmpty(photonPlayer.NickName) ? $"Player_{photonPlayer.ActorNumber}" : photonPlayer.NickName,
                honor: TryGet<int>(photonPlayer, "Honor", 0)
            );

            pd.actorNumber = photonPlayer.ActorNumber;
            pd.catchTimeMark = TryGet<int>(photonPlayer, "TimeMark", 0);
            pd.isTagger = TryGet<bool>(photonPlayer, "IsTagger", false);
            pd.playerColor = TryGetColor(photonPlayer, "Color", Color.white);

            return pd;
        }

        private static T TryGet<T>(Player p, string key, T fallback)
        {
            if (p.CustomProperties != null && p.CustomProperties.ContainsKey(key))
            {
                try { return (T)p.CustomProperties[key]; } catch { }
            }
            return fallback;
        }

        private static Color TryGetColor(Player p, string key, Color fallback)
        {
            if (p.CustomProperties != null && p.CustomProperties.ContainsKey(key))
            {
                var v = p.CustomProperties[key];
                // поддержим несколько форматов: Color как string "#RRGGBBAA" или Vector3/Vector4/float[]
                if (v is string hex && ColorUtility.TryParseHtmlString(hex, out var c)) return c;
                if (v is Vector4 v4) return new Color(v4.x, v4.y, v4.z, v4.w);
                if (v is Vector3 v3) return new Color(v3.x, v3.y, v3.z, 1f);
            }
            return fallback;
        }

        #endregion
    }
}
