using System.Collections.Generic;
using UnityEngine;
using _game.Scripts.Data;

namespace _game.Scripts.Canvas
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private List<CanvasController> canvasList;
        [SerializeField] private CanvasType startCanvas;

        private CanvasController _current;
        private readonly Stack<CanvasController> _history = new();
		
        private void Start()
        {
            Open(startCanvas);
        }

        public void Dispose()
        {
            _history.Clear();
        }
		
        public void Open(CanvasType canvasType)
        {
            if (_current)
                _history.Push(_current);

            foreach (var canvasController in canvasList)
            {
                if (canvasController.CanvasType == canvasType)
                {
                    _current = canvasController;
                    _current.Open();
                }
                else
                {
                    canvasController.Close();
                }
            }
        }

        public void Back()
        {
            if (_history.Count == 0)
                return;
			
            _current.Close();

            var canvas = _history.Pop();
            _current = canvas;
            _current.Open();
        }
    }
}