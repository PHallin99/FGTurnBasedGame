using UnityEngine.Events;

public class CharacterSwapping
{
    private readonly UnityEvent<PlayableCharacter> onCharacterSwap = new UnityEvent<PlayableCharacter>();

    public void SwapCharacter(PlayableCharacter character)
    {
        onCharacterSwap.Invoke(character);
    }
}