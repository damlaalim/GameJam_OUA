using Zenject;
using UnityEngine;
using _game.Scripts.Player;
using _game.Scripts.Manager;

namespace _game.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        public PlayerController playerController; 
            
        private LevelManager _levelManager;

        public virtual void LoadLevel(TimeManager timeManager, LevelManager levelManager)
        {
            _levelManager = levelManager;
            playerController.GetComponent<PlayerMovementController>().levelManager = levelManager;
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