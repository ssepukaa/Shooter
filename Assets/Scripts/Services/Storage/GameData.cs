
using System;
using UnityEngine;

namespace Assets.Scripts.Services.Storage {

    // Обучение по сериализации векторов

    [Serializable]
    public class GameData {
        public int speed;
        public Vector3 position;
        public Quaternion rotation;

        public GameData() {
            speed = 7;
            position = Vector3.zero;
            rotation = Quaternion.identity;
        }
    }
}
