using UnityEngine;
using UnityEngine.UI;

public class RhythmIndicator : MonoBehaviour
{
    public GameManager gameManager;
    public Slider indicatorSlider;

    void Start()
    {
        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameManager>();
        }
        
        if (indicatorSlider != null)
        {
            indicatorSlider.minValue = 0f;
            indicatorSlider.maxValue = 1f;
        }
    }

    void Update()
    {
        if (gameManager != null && indicatorSlider != null)
        {
            // El valor va de 0 a 1 indicando el progreso del compás.
            // Cuando llega a 1 (y vuelve a 0 de golpe), es el momento exacto para tocar la barra.
            indicatorSlider.value = gameManager.CurrentBeatPhase;
        }
    }
}
