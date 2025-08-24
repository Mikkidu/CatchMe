using System;
using UnityEngine;

namespace AlexDev.CatchMe.Data
{

    [Serializable]
    public class PlayerData
    {

        public string playerName;
        public int honor;

        [NonSerialized]
        public int catchTimeMark;

        [NonSerialized]
        public bool isTagger;

        [NonSerialized]
        public Color playerColor;

        [NonSerialized]
        public int actorNumber;

        public PlayerData()
        {
            this.playerName = "PlayerName";
            this.honor = 0;
        }

        public PlayerData(string playerName, int honor = 0)
        {
            this.playerName = playerName;
            this.honor = honor;
        }

    }
}
