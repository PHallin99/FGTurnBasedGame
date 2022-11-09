using System;
using UI;
using UnityEngine;
using Weapons;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
public class PlayableCharacter : MonoBehaviour
{
    public Team team;

    [SerializeField] private CharacterController controller;
    [SerializeField] private StatusText statusText;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Material team1Mat;
    [SerializeField] private Material team2Mat;
    [SerializeField] private new Renderer renderer;
    [SerializeField] private GameObject characterVisuals;

    [Header("Variables")] [SerializeField] private bool showProjectileSpawnPoint;
    [SerializeField] private float weaponZOffset;

    [SerializeField] private int maxHp;

    public PlayableCharacter(Team teamAssignment)
    {
        team = teamAssignment;
    }

    public Rigidbody Rigidbody { get; private set; }
    public bool CanJump { get; private set; }
    public StatusText StatusText => statusText;
    public Transform WeaponTransform => weaponTransform;

    public int Hp { get; private set; }

    public WeaponsHandler WeaponsHandler { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        CanJump = false;
        WeaponsHandler = GetComponent<WeaponsHandler>();
    }

    private void Start()
    {
        Hp = maxHp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        CanJump = collision.gameObject.CompareTag("Floor");
    }

    private void OnDrawGizmos()
    {
        if (!showProjectileSpawnPoint)
        {
            return;
        }

        DrawSpawnPointGizmo();
    }

    private void DrawSpawnPointGizmo()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(weaponTransform.position + weaponTransform.up * weaponZOffset, 0.3f);
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp > 0) return;
        DisableCharacter();
        controller.RemoveCharacter(this);
    }

    public void GiveHealth(int health)
    {
        Hp += health;
    }

    private void DisableCharacter()
    {
        characterVisuals.SetActive(false);
    }

    public Vector3 ProjectileSpawnPoint()
    {
        return weaponTransform.position + weaponTransform.up * weaponZOffset;
    }

    public void Initialize()
    {
        renderer.material = team switch
        {
            Team.Team1 => team1Mat,
            Team.Team2 => team2Mat,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void FlipCanJump()
    {
        CanJump = !CanJump;
    }
}

public enum Team
{
    Team1,
    Team2
}