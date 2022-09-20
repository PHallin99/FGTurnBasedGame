using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Script controls the controlling of the characters.
    private int characterIndex;
    [SerializeField] private List<Transform> playableCharacters;

    private void Start()
    {
        foreach (var child in GetComponentsInChildren<PlayableCharacter>())
        {
            playableCharacters.Add(child.transform);
        }
    }

    public void NextCharacter()
    {
        characterIndex = characterIndex == playableCharacters.Count ? characterIndex = 0 : characterIndex++;
    }

    public void PreviousCharacter()
    {
        characterIndex = characterIndex == 0 ? characterIndex = playableCharacters.Count : characterIndex--;
    }

    private void MoveCharacter()
    {
        
    }
}