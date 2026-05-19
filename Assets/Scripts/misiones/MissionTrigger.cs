using UnityEngine;

public class MissionTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        MissionManager.Instance.SiguientePaso();
        gameObject.SetActive(false);
    }
}