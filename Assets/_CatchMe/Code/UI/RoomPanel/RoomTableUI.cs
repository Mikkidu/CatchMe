using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace AlexDev.CatchMe.UI
{
    public class RoomTableUI : MonoBehaviour
    {
        [SerializeField] private Transform _roomsTable;
        [SerializeField] private RoomSlotUI _roomSlotPrefab;

        private List<RoomSlotUI> _roomsList = new List<RoomSlotUI>();

        public void AddRoom(int roomIndex, string roomName, int playerCount)
        {
            RoomSlotUI newRoom = Instantiate(_roomSlotPrefab, _roomsTable.transform);
            newRoom.SetStats(roomIndex, roomName, playerCount);
            _roomsList.Add(newRoom);
        }

        public void RefreshRoom(int roomIndex, int playerCount)
        {
            _roomsList.Find(slot => slot.GetRoomIndex == roomIndex).RefreshStats(playerCount);
        }

        public void RemoveRoom(int roomIndex)
        {
            RoomSlotUI room = _roomsList.Find(slot => slot.GetRoomIndex == roomIndex);
            room.RemoveRoom();
            _roomsList.Remove(room);
        }

    }
}
