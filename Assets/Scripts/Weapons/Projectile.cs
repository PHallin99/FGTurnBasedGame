using System.Collections;
using Managers;
using UnityEngine;

namespace Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public Enums.ProjectileType weaponType;

        [Header("Rocket Launcher")] [SerializeField]
        private float explosionRadius;

        [Header("Grenade")] [SerializeField] private float fuseTimer;

        private bool hasCollided;
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            if (weaponType == Enums.ProjectileType.Grenade)
            {
                StartCoroutine(GrenadeFuseTimer());
            }
        }

        private void LateUpdate()
        {
            if (weaponType != Enums.ProjectileType.Rocket || hasCollided || rigidbody.velocity == Vector3.zero)
            {
                return;
            }

            HandleRotation();
        }

        private void OnCollisionEnter()
        {
            hasCollided = true;
            if (weaponType == Enums.ProjectileType.Rocket)
            {
                Explosion();
            }
        }

        private void OnDrawGizmos()
        {
            DrawExplosionGizmo();
        }

        private IEnumerator GrenadeFuseTimer()
        {
            yield return new WaitForSeconds(fuseTimer);
            Explosion();
        }

        private void HandleRotation()
        {
            transform.forward = rigidbody.velocity.normalized;
        }

        private void DrawExplosionGizmo()
        {
            var explosionGizmoColor = new Color
            {
                a = 0.5f,
                b = 1,
                g = 0,
                r = 0
            };
            Gizmos.color = explosionGizmoColor;
            Gizmos.DrawSphere(transform.position, explosionRadius);
        }

        private void Explosion()
        {
            var localTransform = transform;
            var sphereCastAll = Physics.SphereCastAll(localTransform.position, explosionRadius, localTransform.forward);
            foreach (var raycastHit in sphereCastAll)
            {
                if (raycastHit.collider.CompareTag($"PickUp"))
                {
                    raycastHit.collider.GetComponent<HealthPickUp>().Respawn();
                }

                if (!raycastHit.collider.CompareTag($"Player"))
                {
                    continue;
                }

                var playableCharacter = raycastHit.collider.GetComponent<PlayableCharacter>();
                playableCharacter.TakeDamage(10);
            }

            Game.TurnManager.EndTurnPostAction();
            Destroy(gameObject);
        }

        public void SetVelocity(Vector3 velocity)
        {
            rigidbody.velocity = velocity;
        }
    }
}