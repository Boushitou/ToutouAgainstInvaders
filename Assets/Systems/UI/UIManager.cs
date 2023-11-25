using TMPro;
using UnityEngine;

namespace Systems.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Score _score;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void AddScore(int value)
        {
           _score.SetScore(value);
        }
    }

}