using UnityEngine.Events;

public class CharacterSwapping
{
    private UnityEvent<PlayableCharacter> OnCharacterSwap;

    public void SwapCharacter(PlayableCharacter character)
    {
        OnCharacterSwap.Invoke(character);
    }
}