using System.Collections.Generic;
using UnityEngine;

public class ControladorPosta : MonoBehaviour
{
    // Las listas y conexiones
    [SerializeField] List<Transform> objetivos;
    [SerializeField] List<Corredor> corredores;
    [SerializeField] int cantidadVueltas;
    [SerializeField] ControladorUI ui; 

    int turnoActual = 0;
    int vueltasDadas = 0;

    public void Posicionar()
    {
        corredores[0].transform.position = objetivos[0].position;
        corredores[1].transform.position = objetivos[1].position;
        corredores[2].transform.position = objetivos[2].position;
        corredores[3].transform.position = objetivos[3].position;    
    }

    public void IniciarCarrera()
    {
        turnoActual = 0;
        vueltasDadas = 0;
        MandarAlSiguienteCorredor();
    }

    void MandarAlSiguienteCorredor()
    {
        Corredor corredorDeTurno = corredores[turnoActual];
        int indiceDelObjetivo = turnoActual + 1;

        if (indiceDelObjetivo >= corredores.Count)
        {
            indiceDelObjetivo = 0;
        }

        Transform postaDestino = objetivos[indiceDelObjetivo];
        corredorDeTurno.DarLaOrden(postaDestino);
    }

    public void ElCorredorLlego()
    {
        turnoActual++; 
        if (turnoActual >= corredores.Count)
        {
            turnoActual = 0; 
            vueltasDadas++;  
            
            if (vueltasDadas >= cantidadVueltas)
            {
                return; 
            }
        }
        MandarAlSiguienteCorredor();
    }

    // Le cambié el "nuevoValor" por "valor" para que coincida con lo de los paréntesis
    public void ActualizarVelocidad(float valor)
    {
        corredores[0].CambiarVelocidad(valor);
        corredores[1].CambiarVelocidad(valor);
        corredores[2].CambiarVelocidad(valor);
        corredores[3].CambiarVelocidad(valor);    
    }
}