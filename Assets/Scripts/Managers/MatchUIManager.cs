using Enums;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class MatchUIManager : MonoBehaviour
    {
        [SerializeField] private TeamsTracker teamsTracker;

        [Header("UI")] [SerializeField] private TMP_Text gameOverText;

        [SerializeField] private TMP_Text staminaText;
        [SerializeField] private TMP_Text chargeText;
        [SerializeField] private TMP_Text team1Hp;
        [SerializeField] private TMP_Text team2Hp;

        private void Start()
        {
            gameOverText.gameObject.SetActive(false);
        }

        private void Update()
        {
            HandleStaminaUI();
            HandleChargeUpText();
            HandleTeamHpTexts();
        }

        private void HandleTeamHpTexts()
        {
            team1Hp.text = $"Team 1: {teamsTracker.Team1Hp.ToString()}";
            team2Hp.text = $"Team 2: {teamsTracker.Team2Hp.ToString()}";
        }

        private void HandleChargeUpText()
        {
            chargeText.gameObject.SetActive(Game.CharacterController.CurrentCharacter.WeaponsHandler.IsCharging);
            if (!chargeText.gameObject.activeSelf)
            {
                return;
            }

            chargeText.text = "Charge " +
                              (Game.CharacterController.CurrentCharacter.WeaponsHandler.ChargedForce *
                               (100 / Game.CharacterController.CurrentCharacter.WeaponsHandler.MaxLaunchForce))
                              .ToString("0");
        }

        private void HandleStaminaUI()
        {
            switch (Game.TurnManager.gamePhase)
            {
                case GamePhase.PreAction:
                    staminaText.text = "Stamina: " + (Game.CharacterController.MovementFramesLeft / 2.5f).ToString("0");
                    break;
                case GamePhase.PostAction:
                case GamePhase.TurnEnded:
                    staminaText.text = "";
                    break;
            }
        }

        public void GameOver(bool team1Won, bool team2Won)
        {
            gameOverText.gameObject.SetActive(true);
            if (team1Won && team2Won)
            {
                gameOverText.text = "Game is a draw! \n Backspace to go to desktop";
                return;
            }

            gameOverText.text = team1Won switch
            {
                true => "Player 1 Won!",
                false when team2Won => "Player 2 Won! \n Backspace to go to desktop",
                _ => gameOverText.text
            };
        }
    }
}