using Zenject;
using UnityEngine;

namespace _game.Scripts.Level
{
    public class LevelController : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;

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