using UnityEngine;
using System.Collections.Generic;

namespace AlexDev.CatchMe.UI
{
    public class PlayerTableUI : MonoBehaviour
    {
        [SerializeField] private Transform _playerTableTransform;
        [SerializeField] private PlayerSlotUI _playerSlotPrefab;

        private List<PlayerSlotUI> _playerList = new List<PlayerSlotUI>();

        public void AddPlayer(int playerID, string playerName, int freeTimeCount, int maxFreeTimeCount)
        {
            PlayerSlotUI newPlayer = Instantiate(_playerSlotPrefab, _playerTableTransform.transform);
            newPlayer.SetStats(playerID, playerName, freeTimeCount);
            _playerList.Add(newPlayer);
        }

        public void RefreshPlayerStats(int playerID, int freeTimeCount, int maxFreeTimeCount)
        {
            _playerList.Find(slot => slot.GetPlayerID == playerID).RefreshStats(freeTimeCount, maxFreeTimeCount);
        }

        public void RemovePlayer(int playerID)
        {
            PlayerSlotUI player = _playerList.Find(slot => slot.GetPlayerID == playerID);
            player.RemovePlayer();
            _playerList.Remove(player);
        }

    }
}
