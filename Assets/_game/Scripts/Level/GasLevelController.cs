using _game.Scripts.Data;
using Zenject;
using _game.Scripts.Manager;
using _game.Scripts.Player;
using UnityEngine;

namespace _game.Scripts.Level
{
    public class GasLevelController : LevelController
    {
        private PlayerController _player;
        private TimeManager _timeManager;

        public override void LoadLevel(TimeManager timeManager, LevelManager levelManager, PlayerController player)
        {
            base.LoadLevel(timeManager, levelManager, player);
            _player = player;
            _timeManager = timeManager;
            _timeManager.CountdownIsOver += DOCountdownIsOver;
            _timeManager.StartCountdown(CountdownType.GasLevel);
        }

        public override void DisposeLevel()
        {
            base.DisposeLevel();
            
            _timeManager.StopCountDown();
        }

        private void DOCountdownIsOver(CountdownType obj)
        {
            _player.Dead();
        }
    }
}