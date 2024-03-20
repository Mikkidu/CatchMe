using Photon.Realtime;
using System;
using System.Collections.Generic;

namespace AlexDev.CatchMe
{
    public class RoomsBase
    {
        #region Private Fields

        private List<(int index, string name, int playersCount)> _roomList;
        private int _newRoomIndex = 0;

        private const int MAX_ROOM_COUNT = 100;

        private int GetNewRoomIndex
        {
            get
            {
                if (_roomList.Count == 0) return _newRoomIndex;

                Func<int, bool> IsIndexExist = index => 
                    _roomList.Exists(room => room.index == _newRoomIndex);

                for (int i = 0; i < MAX_ROOM_COUNT & IsIndexExist(_newRoomIndex); i++)
                {
                    if (++_newRoomIndex > MAX_ROOM_COUNT)
                    {
                        _newRoomIndex = 0;
                    }
                }
                return _newRoomIndex;
            }
        }

        #endregion

        public RoomsBase()
        {
            _roomList = new List<(int index, string name, int playersCount)>();
        }

        #region Events

        public event Action<int, string, int> NewRoomCreatedEvent;
        public event Action<int, int> RoomStateUpdatedEvent;
        public event Action<int> RoomRemovedEvent;

        #endregion

        #region Public Methods

        public void RefreshRoomList(List<RoomInfo> roomListFromPun)
        {
            if (_roomList.Count == 0)
            {
                foreach (var roomInfo in roomListFromPun)
                {
                    if (!roomInfo.RemovedFromList) AddRoomInfo(roomInfo);
                }
                return;
            }

            foreach (var roomInfo in roomListFromPun)
            {
                if (!roomInfo.RemovedFromList)
                {
                    var roomIndex = _roomList.FindIndex(room => room.name == roomInfo.Name);
                    if (roomIndex != 1)
                    {
                        RefresRoomState(roomIndex, roomInfo);
                    }
                    else
                    {
                        AddRoomInfo(roomInfo);
                    }
                }
                else
                {
                    RemoveRoomState(roomInfo);
                }
            }
        }

        #endregion

        #region Private Methods

        private void AddRoomInfo(RoomInfo roomInfo)
        {
            (int index, string name, int playersCount) newRoomState = (GetNewRoomIndex, roomInfo.Name, roomInfo.PlayerCount);
            _roomList.Add(newRoomState);
            NewRoomCreatedEvent?.Invoke(newRoomState.index, newRoomState.name, newRoomState.playersCount);
        }

        private void RefresRoomState(int roomIndex, RoomInfo roomInfo)
        {
            var tempRoomState = _roomList[_newRoomIndex];
            tempRoomState.playersCount = roomInfo.PlayerCount;
            _roomList[roomIndex] = tempRoomState;
            RoomStateUpdatedEvent?.Invoke(roomIndex, roomInfo.PlayerCount);
        }

        private void RemoveRoomState(RoomInfo roomInfo)
        {
            var roomIndex = _roomList.FindIndex(room => room.name == roomInfo.Name);
            _roomList.RemoveAt(roomIndex);
            RoomRemovedEvent?.Invoke(roomIndex);
        }

        #endregion
    }
}
