using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private SoundSettings soundSettings;

    void Start()
    {
        if (soundSettings != null && volumeSlider != null)
        {
            // Inicializar el slider con el valor guardado
            volumeSlider.value = soundSettings.volume;
            
            // Asignar el evento para cuando cambia el valor del slider
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    public void OnVolumeChanged(float value)
    {
        if (soundSettings != null)
        {
            // Guardar el valor en el ScriptableObject
            soundSettings.volume = value;
            // Actualizar el volumen global a través del AudioListener
            AudioListener.volume = value;
        }
    }
}
