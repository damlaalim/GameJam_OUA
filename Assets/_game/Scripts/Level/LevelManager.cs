using System;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using _game.Scripts.Player;
using _game.Scripts.Manager;

namespace _game.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel
        {
            get => PlayerPrefs.GetInt("level");
            set => PlayerPrefs.SetInt("level", value);
        }
        
        [SerializeField] private List<LevelController> _levelList;

        private LevelController _createdLevel;

        [Inject] private TimeManager _timeManager;
        [Inject] private PlayerController _player;

        private void Start()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            ClearLevel();
            
            if (CurrentLevel == 0)
                CurrentLevel = 1;
            
            _createdLevel = Instantiate(_levelList[CurrentLevel - 1]);
            _createdLevel.transform.position = Vector3.zero;
            
            _createdLevel.LoadLevel(_timeManager, this, _player);
        }

        public void ClearLevel()
        {
            if (_createdLevel is not null)
            {
                _createdLevel.DisposeLevel();
                DestroyImmediate(_createdLevel.gameObject);
                _createdLevel = null;
            }
        }

        public void ResetLevels()
        {
            CurrentLevel = 1;
            ClearLevel();
            LoadLevel();
        }

        public void LoadNextLevel()
        {
            CurrentLevel++;
            if (CurrentLevel > _levelList.Count)
                CurrentLevel = 1;
            
            ClearLevel();
            LoadLevel();
        }

        public int PreLevel()
        {
            CurrentLevel--;

            ClearLevel();
            LoadLevel();
            return CurrentLevel;
        }
    }
}