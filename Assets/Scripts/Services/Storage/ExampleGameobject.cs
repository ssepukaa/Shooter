
using UnityEngine;

namespace Assets.Scripts.Services.Storage {
    public class ExampleGameobject: MonoBehaviour {

        // Обучение по сериализации векторов

        private Storage storage;
        private GameData gameData;

        public void Start() {
            storage = new Storage();
            Load();
        }

        private void Load() {
            gameData = (GameData)storage.Load(new GameData());

            transform.position = gameData.position;
            transform.rotation = gameData.rotation;
        }

        private void Save() {
            gameData.position = transform.position;
            gameData.rotation = transform.rotation;
            storage.Save(gameData);

        }
    }
}
