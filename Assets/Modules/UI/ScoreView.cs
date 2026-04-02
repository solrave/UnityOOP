using TMPro;
using UnityEngine;

namespace Modules.UI
{
    public sealed class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private float _currentScore;

        public void SetValue()
        {
            _currentScore += 1;
            _text.text = $"SCORE: {_currentScore}";
        }
    }
}