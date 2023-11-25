using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

namespace Systems.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Score _score;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _gameOverMenu;

        private bool _isPaused;

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

        private void Start()
        {
            EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
            //Debug.Log(transform.GetChild(0).gameObject.name);
            //Debug.Log(EventSystem.current.currentSelectedGameObject.name);

            if (_pauseMenu != null)
            {
                _pauseMenu.SetActive(false);
            }
            if (_gameOverMenu != null)
            {
                _gameOverMenu.SetActive(false);
            }
        }

        public void AddScore(int value)
        {
            if (_score != null)
            {
                _score.SetScore(value);
            }
            else
            {
                Debug.LogWarning("There is no reference to the score's script !");
            }
        }

        public void OnLoadSceneButton(int index)
        {
            if (index <= SceneManager.sceneCount)
            {
                if (Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }

                SceneManager.LoadSceneAsync(index);
                Debug.Log("Loading: " + SceneManager.GetSceneByBuildIndex(index).name);
            }
            else
            {
                Debug.LogWarning("This scene's index is not valid !");
            }
        }

        public void OnQuitButton()
        {
            Debug.Log("Application quit");
            Application.Quit();
        }

        public void OpenClosePauseMenu()
        {
            if (_pauseMenu != null)
            {
                PauseGame();

                _pauseMenu.SetActive(_isPaused);

                EventSystem.current.SetSelectedGameObject(_pauseMenu.transform.GetChild(0).gameObject);
            }
        }

        public void OpenGameOverMenu(string gameOverTxt)
        {
            PauseGame();

            _gameOverMenu.SetActive(true);
            _gameOverMenu.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameOverTxt;
            EventSystem.current.SetSelectedGameObject(_gameOverMenu.transform.GetChild(0).gameObject);

            MoveScore();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (!focus && !_isPaused)
            {
                OpenClosePauseMenu();
            }
        }

        public void PauseGame()
        {
            _isPaused = !_isPaused;
            int scale = _isPaused ? 0 : 1;
            Time.timeScale = scale;
        }

        private void MoveScore()
        {
            RectTransform scoreRect = _score.transform.GetComponent<RectTransform>();

            _score.transform.SetParent(_gameOverMenu.transform, false);

            scoreRect.anchorMax = new Vector2(0.5f, 0.5f);
            scoreRect.anchorMin = new Vector2(0.5f, 0.5f);
            scoreRect.pivot = new Vector2(0.5f, 0.5f);
            scoreRect.localPosition = Vector3.zero;
        }
    }

}