using System;
using System.Collections.Generic;
using Enums;
using Managers;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(PlayableCharacter))]
    public class WeaponsHandler : MonoBehaviour
    {
        private const float GizmoDrawDuration = 2;
        [SerializeField] private Enums.Weapons weapons;
        [SerializeField] private GameObject rocketPrefab;
        [SerializeField] private GameObject grenadePrefab;
        [SerializeField] [Range(0, 20)] private float maxLaunchForce;
        [SerializeField] private float chargeMultiplier;
        [SerializeField] private GameObject rocketLauncher;
        [SerializeField] private GameObject grenadeObject;

        private readonly List<Vector3> gizmoPoints = new List<Vector3>();
        private PlayableCharacter character;
        private bool maxCharged;
        private ProjectileType projectileType;
        private static Vector3 Acceleration => Physics.gravity;
        public bool IsCharging { get; private set; }
        public float ChargedForce { get; private set; }
        public float MaxLaunchForce => maxLaunchForce;

        private void Awake()
        {
            character = GetComponent<PlayableCharacter>();
        }

        private void Start()
        {
            projectileType = ProjectileType.Rocket;
            UpdateActiveWeapon();
        }

        private void Update()
        {
            if (Game.CharacterController == null || Game.CharacterController.CurrentCharacter != character)
            {
                return;
            }

            HandleFireInputs();
        }

        private void OnDrawGizmos()
        {
            if (!IsCharging)
            {
                return;
            }

            DrawRocketTrajectory();
        }

        private void UpdateActiveWeapon()
        {
            grenadeObject.SetActive(projectileType == ProjectileType.Grenade);
            rocketLauncher.SetActive(projectileType == ProjectileType.Rocket);
        }

        private void HandleFireInputs()
        {
            if (IsCharging)
            {
                HandleChargeUp();
                return;
            }

            if (Game.InputManager.PrimaryFirePressed)
            {
                PrimaryFire();
            }

            if (Game.InputManager.SecondaryFirePressed)
            {
                SecondaryFire();
            }
        }

        private void HandleChargeUp()
        {
            switch (projectileType)
            {
                case ProjectileType.Rocket:
                    if (Game.InputManager.PrimaryFireReleased)
                    {
                        FireProjectile();
                    }

                    break;

                case ProjectileType.Grenade:
                    if (Game.InputManager.SecondaryFireReleased)
                    {
                        FireProjectile();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (maxCharged)
            {
                return;
            }

            if (ChargedForce >= MaxLaunchForce)
            {
                ChargedForce = MaxLaunchForce;
                maxCharged = true;
            }

            ChargedForce += Time.deltaTime * chargeMultiplier;
        }


        private void DrawRocketTrajectory()
        {
            const int detail = 80;
            gizmoPoints.Clear();
            for (var i = 0; i < detail; i++)
            {
                var t = i / (detail - 1f);
                var time = t * GizmoDrawDuration;
                gizmoPoints.Add(GetPoint(time));
            }

            for (var i = 0; i < detail - 1; i++)
            {
                Gizmos.DrawLine(gizmoPoints[i], gizmoPoints[i + 1]);
            }
        }

        private Vector3 GetPoint(float time)
        {
            return character.ProjectileSpawnPoint() + character.WeaponTransform.up * ChargedForce * time +
                   Acceleration / 2 * time * time;
        }

        private void SecondaryFire()
        {
            if (projectileType != ProjectileType.Grenade)
            {
                projectileType = ProjectileType.Grenade;
                UpdateActiveWeapon();
                return;
            }

            IsCharging = true;
        }

        private void PrimaryFire()
        {
            if (projectileType != ProjectileType.Rocket)
            {
                projectileType = ProjectileType.Rocket;
                UpdateActiveWeapon();
                return;
            }

            IsCharging = true;
        }

        private void FireProjectile()
        {
            var instantiatedGameObject = projectileType == ProjectileType.Grenade
                ? Instantiate(grenadePrefab, character.ProjectileSpawnPoint(), Quaternion.identity)
                : Instantiate(rocketPrefab, character.ProjectileSpawnPoint(), Quaternion.identity);
            var projectile = instantiatedGameObject.GetComponent<Projectile>();
            projectile.SetVelocity(character.WeaponTransform.up * ChargedForce);
            projectile.weaponType = projectileType;
            ChargedForce = 0;
            IsCharging = false;
            maxCharged = false;
            Game.TurnManager.EndTurnAction(projectile);
        }
    }
}