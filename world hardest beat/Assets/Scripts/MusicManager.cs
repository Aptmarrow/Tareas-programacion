using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource audioSource;

    public SoundSettings soundSettings;

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }

    void Start()
    {
        // Aplicar el volumen inicial guardado en el ScriptableObject usando AudioListener
        if (soundSettings != null)
        {
            AudioListener.volume = soundSettings.volume;
        }
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
