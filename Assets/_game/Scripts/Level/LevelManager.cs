using System;
using UnityEngine;
using System.Collections.Generic;

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

        private GameObject _createdLevel;

        private void Start()
        {
            LoadLevel();
        }

        public void LoadLevel()
        {
            if (_createdLevel is not null)
                DestroyImmediate(_createdLevel);
            
            if (CurrentLevel == 0)
                CurrentLevel = 1;
            
            _createdLevel = Instantiate(_levelList[CurrentLevel - 1].gameObject);
            _createdLevel.transform.position = Vector3.zero;
        }

        public void ClearLevel()
        {
            if (_createdLevel is not null)
                DestroyImmediate(_createdLevel);
        }

        public void ResetLevels()
        {
            CurrentLevel = 1;
            if (_createdLevel is not null)
                DestroyImmediate(_createdLevel);
            LoadLevel();
        }

        public void LoadNextLevel()
        {
            CurrentLevel++;
            if (_createdLevel is not null)
                DestroyImmediate(_createdLevel);
            LoadLevel();
        }

        public int PreLevel()
        {
            CurrentLevel--;

            if (_createdLevel is not null)
                DestroyImmediate(_createdLevel);
            
            LoadLevel();
            return CurrentLevel;
        }
    }
}