using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Managers
{
    public class TeamsTracker : MonoBehaviour
    {
        private List<PlayableCharacter> team1Characters = new List<PlayableCharacter>();
        private List<PlayableCharacter> team2Characters = new List<PlayableCharacter>();

        private bool team1Won;
        private bool team2Won;

        public int Team1Hp { get; private set; }
        public int Team2Hp { get; private set; }

        private void Start()
        {
            Initialize();
            UpdateHp();
        }

        private void Initialize()
        {
            team1Characters = Game.CharacterController.PlayerIndexPlayableCharacters[0];
            team2Characters = Game.CharacterController.PlayerIndexPlayableCharacters[1];
        }

        public void UpdateHp()
        {
            Team1Hp = 0;
            Team2Hp = 0;
            foreach (var playableCharacter in team1Characters)
            {
                Team1Hp += playableCharacter.Hp;
            }

            foreach (var playableCharacter in team2Characters)
            {
                Team2Hp += playableCharacter.Hp;
            }

            CheckWinningConditions();
        }

        private void CheckWinningConditions()
        {
            team2Won = Team1Hp <= 0;
            team1Won = Team2Hp <= 0;

            if (!team1Won && !team2Won) return;
            Game.InputManager.SetInputEnabled(false);
            InputManager.ToggleMouseLocked();
            Game.MatchUIManager.GameOver(team1Won, team2Won);
            Game.TurnManager.gamePhase = GamePhase.GameOver;
        }
    }
}