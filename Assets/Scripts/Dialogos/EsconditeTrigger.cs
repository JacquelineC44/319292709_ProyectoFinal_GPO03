using UnityEngine;

public class EsconditeTrigger : MonoBehaviour
{
    public SecuenciaDialogoEnemigos secuencia;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            secuencia.JugadoraSeEscondio();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            secuencia.JugadoraSalioDelEscondite();
        }
    }
}