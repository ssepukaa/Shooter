using Assets.Scripts.Services;
using Assets.Scripts.Units.Players;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers {
    public class MoveManager : MonoBehaviour {
        private Player player;
        private GameManager _gameManager;
        private MoveService _moveService;

        private void Start() {
            // DontDestroyOnLoad(this);
        }

        public void Initialize(GameManager gameManager, Player player) {
            _gameManager = gameManager;
            this.player = player;
            CreateMoveService();
        }

        private void CreateMoveService() {
            _moveService = new MoveService(player.transform);
        }
    }
}