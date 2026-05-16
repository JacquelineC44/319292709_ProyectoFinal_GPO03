using UnityEngine;

public class TutorialGate : MonoBehaviour
{
    public TutorialManager tutorial;
    public GameObject panelAviso;

    private void Start()
    {
        if (panelAviso != null)
            panelAviso.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (tutorial != null && !tutorial.TutorialTerminado())
        {
            if (panelAviso != null)
                panelAviso.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        if (panelAviso != null)
            panelAviso.SetActive(false);
    }
}
