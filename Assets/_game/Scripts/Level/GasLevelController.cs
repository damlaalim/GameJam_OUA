using _game.Scripts.Data;
using _game.Scripts.Manager;

namespace _game.Scripts.Level
{
    public class GasLevelController : LevelController
    {
        private TimeManager _timeManager;

        public override void LoadLevel(TimeManager timeManager, LevelManager levelManager)
        {
            base.LoadLevel(timeManager, levelManager);
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
            playerController.Dead();
        }
    }
}