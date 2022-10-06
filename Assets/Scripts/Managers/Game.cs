using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class Game : MonoBehaviour
    {
        public static CharacterSwapping CharacterSwapping;
        public static PlayerTurns PlayerTurns;
        public static InputManager InputManager;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeGame();
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        private void InitializeGame()
        {
            CharacterSwapping = new CharacterSwapping();
            PlayerTurns = new PlayerTurns();
            InputManager = GetComponentInChildren<InputManager>();
            LoadScene(1);
        }

        private static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            switch (currentSceneBuildIndex)
            {
                case 0:
                    break;
                case 1:
                    InputManager.InputEnabled(true);
                    break;
                case 2:
                    break;
            }
        }
    }
}