using Zenject;
using UnityEngine;
using _game.Scripts.Player;
using _game.Scripts.Manager;

namespace _game.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        private LevelManager _levelManager;

        public virtual void LoadLevel(TimeManager timeManager, LevelManager levelManager, PlayerController player)
        {
            _levelManager = levelManager;
        }
        
        public virtual void DisposeLevel()
        {
            
        }
        
        public void RefreshLevel()
        {
            _levelManager.LoadLevel();
        }

        public void ResetLevels()
        {
            _levelManager.ResetLevels();
        }

        public void FinishLevel()
        {
            _levelManager.LoadNextLevel();
        }
    }
}