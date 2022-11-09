using UnityEngine;
using Weapons;

namespace Managers
{
    public class Game : MonoBehaviour
    {
        public static InputManager InputManager;
        public static TurnManager TurnManager;
        public static CharacterController CharacterController;
        public static TeamsTracker TeamsTracker;
        public static MatchUIManager MatchUIManager;
        public static CameraMovement CameraMovement;
        public static WeaponsHandler WeaponsHandler;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            TurnManager = GetComponentInChildren<TurnManager>();
            InputManager = GetComponentInChildren<InputManager>();
            SceneLoader.LoadScene(1);
        }
    }
}