using UnityEngine;

public class FinDemoTrigger : MonoBehaviour
{
    [SerializeField] private FinDemoController finDemoController;
    private bool yaSeActivo = false;

    private void OnTriggerEnter(Collider other)
    {
        if (yaSeActivo) return;

        if (other.CompareTag("Player"))
        {
            yaSeActivo = true;
            finDemoController.MostrarFinDemo();
        }
    }
}
