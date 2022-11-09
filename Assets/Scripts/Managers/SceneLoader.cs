using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons;

namespace Managers
{
    public class SceneLoader : MonoBehaviour
    {
        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        public static void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        private static void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            var currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
            switch (currentSceneBuildIndex)
            {
                case 0:
                    break;
                case 1:
                    Game.InputManager.SetInputEnabled(true);
                    Game.InputManager.ActionInputsEnabled = true;
                    InputManager.ToggleMouseLocked();
                    Game.TurnManager.SetPlayersPerTurn(2);
                    Game.CharacterController = FindObjectOfType<CharacterController>();
                    Game.MatchUIManager = FindObjectOfType<MatchUIManager>();
                    Game.CameraMovement = FindObjectOfType<CameraMovement>();
                    Game.WeaponsHandler = FindObjectOfType<WeaponsHandler>();
                    Game.TeamsTracker = FindObjectOfType<TeamsTracker>();
                    Game.CameraMovement.UpdateCamera();
                    FindObjectOfType<Light>().transform.rotation = Quaternion.Euler(50, -30, 0);
                    break;
                case 2:
                    break;
            }
        }
    }
}