using Zenject;
using UnityEngine;
using _game.Scripts.Data;
using _game.Scripts.Level;

namespace _game.Scripts.Canvas
{
    public class CanvasController : MonoBehaviour
    {
        public CanvasType CanvasType;

        private UnityEngine.Canvas _canvas;

        [Inject] private CanvasManager _canvasManager;
        [Inject] private LevelManager _levelManager;
        
        private void Awake()
        {
            _canvas = GetComponent<UnityEngine.Canvas>();
            _canvas.enabled = false;
        }

        public void Open()
        {
            _canvas.enabled = true;
        }   

        public void Close()
        {
            _canvas.enabled = false;
        }

        public void OnClick_StartGame()
        {
            _canvasManager.Open(CanvasType.InGame);
            _levelManager.LoadLevel();
        }
    }
}