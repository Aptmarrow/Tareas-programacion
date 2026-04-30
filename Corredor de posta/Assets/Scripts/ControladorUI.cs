using UnityEngine;
using UnityEngine.UI;

public class ControladorUI : MonoBehaviour
{
    [SerializeField] Button btnPosicionar;
    [SerializeField] Button btnCorrer;
    [SerializeField] Slider sliderVelocidad;
    [SerializeField] ControladorPosta posta;

    void Start()
    {
        btnPosicionar.onClick.AddListener(AlClickearPosicionar);
        btnCorrer.onClick.AddListener(AlClickearCorrer);
        sliderVelocidad.onValueChanged.AddListener(AlCambiarSlider);
        posta.ActualizarVelocidad(sliderVelocidad.value);      
    }

    // Funciones afuera del Start y del Update
    void AlClickearPosicionar()
    {
        posta.Posicionar();
    }

    void AlClickearCorrer()
    {
        posta.IniciarCarrera();
    }

    void AlCambiarSlider(float valor)
    {
        posta.ActualizarVelocidad(valor);
    }   
}