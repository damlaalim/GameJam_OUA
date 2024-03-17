using System;
using System.Collections;
using _game.Scripts.Data;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

namespace _game.Scripts.Manager
{
    public class TimeManager : MonoBehaviour
    {
        public Action<CountdownType> CountdownIsOver;
        
        [SerializedDictionary("CountdownType", "Time")]
        [SerializeField] private SerializedDictionary<CountdownType, float> _countdownTimeList;
        [SerializeField] private TextMeshProUGUI _timeText;

        private Coroutine _countdownRoutine;
        
        public void StartCountdown(CountdownType countdownType)
        {
            if (!_countdownTimeList.ContainsKey(countdownType))
                return;

            _countdownRoutine = StartCoroutine(StartCountdownRoutine(countdownType));
        }

        public void StopCountDown()
        {
            _timeText.enabled = false;
            
            if (_countdownRoutine is not null)
                StopCoroutine(_countdownRoutine);
        }

        private IEnumerator StartCountdownRoutine(CountdownType countdownType)
        {
            _timeText.enabled = true;
            
            var elapsed = _countdownTimeList[countdownType];

            while (elapsed > 0)
            {
                elapsed -= Time.deltaTime;
                
                var time = TimeSpan.FromSeconds(elapsed);
                _timeText.text = time.ToString("mm' : 'ss");
                
                yield return 0;
            }

            CountdownIsOver?.Invoke(countdownType);
        }
    }
}