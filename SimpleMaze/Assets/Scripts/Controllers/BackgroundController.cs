using System.Collections.Generic;
using System.Linq;
using Helpers;
using UnityEngine;

namespace Controllers {
    public class BackgroundController: MonoBehaviourSingletonBase<BackgroundController> {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private List<Color> _colors;

        private Queue<Color> _colorsQueue;

        public void Init() {
            CreateColorsIfNeeded();
            ApplyColor();
        }

        private void CreateColorsIfNeeded() {
            if (_colorsQueue != null && _colorsQueue.Any()) {
                return;
            }
            
            _colorsQueue = new Queue<Color>(_colors.OrderBy(color => Random.Range(0, int.MaxValue)));
        }

        private void ApplyColor() {
            var newColor = _colorsQueue.Dequeue();
            _background.color = new Color(newColor.r, newColor.g, newColor.b);
        }
    }
}