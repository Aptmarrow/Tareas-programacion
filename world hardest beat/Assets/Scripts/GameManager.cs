using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Slider rhythmSlider;
    public float maxRhythm = 100f;
    public float drainRate = 20f;
    public float refillAmount = 15f;
    public TMP_Text deathCounterText;

    [Header("Rhythm Settings")]
    public AudioSource backgroundMusic;
    public float bpm = 136f;
    public float timingWindow = 0.2f;
    public float missPenalty = 10f;

    public float CurrentBeatPhase { get; private set; }

    private float timePerBeat;
    private float currentRhythm;
    private static int totalDeaths = 0;
    private bool isGameOver = false;

    void Start()
    {
        SetupGame();
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        UpdateBeatPhase();
        LoseRhythmOverTime();
        CheckPlayerInput();
        UpdateSliderUI();
        CheckGameOver();
    }

    private void UpdateBeatPhase()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            CurrentBeatPhase = (backgroundMusic.time % timePerBeat) / timePerBeat;
        }
        else
        {
            CurrentBeatPhase = 0f;
        }
    }

    private void SetupGame()
    {
        timePerBeat = 60f / bpm;

        // Auto-buscar el MusicManager si no se asignó a mano
        if (backgroundMusic == null)
        {
            MusicManager musicManager = FindFirstObjectByType<MusicManager>();
            if (musicManager != null)
            {
                backgroundMusic = musicManager.GetAudioSource();
            }
        }

        if (backgroundMusic != null)
        {
            // ¡Clave! Solo le damos Play si NO estaba sonando ya (por si venimos del nivel anterior)
            if (!backgroundMusic.isPlaying)
            {
                backgroundMusic.Play();
            }
        }

        currentRhythm = maxRhythm;
        rhythmSlider.maxValue = maxRhythm;
        rhythmSlider.value = currentRhythm;
        
        if (deathCounterText != null)
        {
            deathCounterText.text = "Muertes: " + totalDeaths;
        }
    }

    private void LoseRhythmOverTime()
    {
        currentRhythm -= drainRate * Time.deltaTime;
    }

    private void CheckPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (backgroundMusic == null || !backgroundMusic.isPlaying)
            {
                currentRhythm += refillAmount;

                if (currentRhythm > maxRhythm)
                {
                    currentRhythm = maxRhythm;
                }
                return;
            }

            float timePosition = backgroundMusic.time;
            float timeSinceLastBeat = timePosition % timePerBeat;
            float timeToNextBeat = timePerBeat - timeSinceLastBeat;

            if (timeSinceLastBeat <= (timingWindow / 2f) || timeToNextBeat <= (timingWindow / 2f))
            {
                currentRhythm += refillAmount;

                if (currentRhythm > maxRhythm)
                {
                    currentRhythm = maxRhythm;
                }
            }
            else
            {
                currentRhythm -= missPenalty;
            }
        }
    }

    private void UpdateSliderUI()
    {
        rhythmSlider.value = currentRhythm;
    }

    private void CheckGameOver()
    {
        if (currentRhythm <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        totalDeaths++;
        
        RestartCurrentLevel();
    }

    public void WinLevel()
    {
        isGameOver = true;
        // Las muertes ya no se resetean acá para que sea un score global
        
        LoadNextLevel();
    }

    private void RestartCurrentLevel()
    {
        string currentLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevelName);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        
        // Verificamos si hay una escena cargada después de la actual en los Build Settings
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Si ya no hay más niveles, te pasaste el juego. Cargamos el Menú o reiniciamos a la fuerza
            // Por ahora asumo que si no tenés MainMenu, te reinicia al nivel 1 (indice 0)
            if (Application.CanStreamedLevelBeLoaded("MainMenu"))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                // Si aún no creaste un MainMenu, vuelve al Nivel 1 (índice 0)
                totalDeaths = 0; // Acá sí reseteamos porque arranca de cero el juego entero
                SceneManager.LoadScene(0);
            }
        }
    }
}