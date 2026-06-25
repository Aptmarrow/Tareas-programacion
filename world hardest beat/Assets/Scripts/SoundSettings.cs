using UnityEngine;

[CreateAssetMenu(fileName = "SoundSettings", menuName = "Settings/SoundSettings")]
public class SoundSettings : ScriptableObject
{
    [Range(0f, 1f)]
    public float volume = 1f;
}
