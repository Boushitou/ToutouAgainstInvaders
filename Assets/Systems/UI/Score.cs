using TMPro;
using UnityEngine;

namespace Systems.UI
{
    public class Score : MonoBehaviour
    {
        private int _score = 0;
        private int _maxScore = 999999;
        private TextMeshProUGUI _scoreTxt;

        private void Awake()
        {
            _scoreTxt = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            _scoreTxt.text = "Score: " + _score;
        }

        public void SetScore(int value)
        {
            _score += value;

            if (_score > _maxScore)
            {
                _score = _maxScore;
            }

            _scoreTxt.text = "Score: " + _score;
        }
    }
}
