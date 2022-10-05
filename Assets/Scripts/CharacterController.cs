﻿using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private List<PlayableCharacter> playableCharacters;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] [Range(1, 1000)] private int moveLenghtMax;
    public int moveLenght { get; private set; }
    private int characterIndex;
    private Vector3 movementDirection;

    private void Start()
    {
        foreach (var child in GetComponentsInChildren<PlayableCharacter>()) playableCharacters.Add(child);
        characterIndex = 0;
        moveLenght = moveLenghtMax;
    }

    private void Update()
    {
        movementDirection = InputManager.instance.GetMovementDirection();
    }

    private void FixedUpdate()
    {
        if (movementDirection != Vector3.zero) MoveCharacter();
        if (playableCharacters[characterIndex].CanJump && InputManager.instance.GetJumpInput())
            JumpCharacter();
    }

    private void LateUpdate()
    {
        playableCharacters[characterIndex].StatusText.UpdateText(moveLenght.ToString());
    }

    public void NextCharacter()
    {
        characterIndex = characterIndex == playableCharacters.Count ? characterIndex = 0 : characterIndex++;
    }

    public void PreviousCharacter()
    {
        characterIndex = characterIndex == 0 ? characterIndex = playableCharacters.Count : characterIndex--;
    }

    private void JumpCharacter()
    {
        playableCharacters[characterIndex].Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        playableCharacters[characterIndex].FlipCanJump();
    }

    private void MoveCharacter()
    {
        if (moveLenght <= 0) return;
        playableCharacters[characterIndex].transform.Translate(movementDirection * movementSpeed * Time.fixedDeltaTime);
        moveLenght--;
    }
}