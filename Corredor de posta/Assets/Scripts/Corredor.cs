using UnityEngine;

public class Corredor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform target;
    [SerializeField] float speed;
    float distance;
    [SerializeField] ControladorPosta posta;
    bool estaCorriendo = false;
    void Start()
    {
        
    }
    public void DarLaOrden(Transform nuevoObjetivo)
    {
        target = nuevoObjetivo;
        estaCorriendo = true;
    }

    public void CambiarVelocidad(float nuevaVelocidad)
    {
        speed = nuevaVelocidad;
    }
    // Update is called once per frame
    void Update()
    {
        if (target && estaCorriendo)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            distance = Vector3.Distance(transform.position, target.position);

            Debug.Log("Distancia: " + Mathf.Round( distance));
            if (distance < 0.01f)
            {
                estaCorriendo = false; 
                posta.ElCorredorLlego();
            }
        }
    }
}
