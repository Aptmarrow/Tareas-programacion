using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    void Awake()
    {
        // El patrón Singleton + Inmortalidad
        if (instance == null)
        {
            // Si soy el primero, me guardo como la única instancia y me vuelvo inmortal
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existía uno (porque venimos del nivel anterior), me destruyo para no duplicar la música
            Destroy(gameObject);
        }
    }
}
