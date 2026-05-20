
using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    public bool esMisionSecundaria;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (esMisionSecundaria)
            MissionManager.Instance.SiguientePasoSecundario();
        else
            MissionManager.Instance.SiguientePaso();

        gameObject.SetActive(false);
    }
}