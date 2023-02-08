using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Player;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Managers
{
    public class MoveManager: MonoBehaviour
    {
        private PlayerC _playerC;
        private GameManager _gameManager;
        private MoveService _moveService;

        private void Start()
        {
            
           // DontDestroyOnLoad(this);
        }

        public void Initialize(GameManager gameManager, PlayerC playerC)
        {
            _gameManager = gameManager;
            _playerC = playerC;
            CreateMoveService();
        }

        private void CreateMoveService()
        {
            _moveService = new MoveService(_playerC.transform);
        }
    }
}
