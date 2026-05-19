using System.Collections;
using UnityEngine;

public class DissolveDoor : MonoBehaviour
{
    public Renderer doorRenderer;
    public Collider doorCollider;

    public Material materialDissolve;
    public float duracion = 2f;

    private Material materialInstanciado;

    private void Awake()
    {
        if (doorRenderer == null)
            doorRenderer = GetComponent<Renderer>();

        if (doorCollider == null)
            doorCollider = GetComponent<Collider>();
    }

    public void AbrirPuerta()
    {
        StartCoroutine(Desvanecer());
    }

    IEnumerator Desvanecer()
    {
        materialInstanciado = new Material(materialDissolve);
        doorRenderer.material = materialInstanciado;

        materialInstanciado.SetFloat("Dissolve", 0f);

        float tiempo = 0f;

        while (tiempo < duracion)
        {
            tiempo += Time.deltaTime;
            float valor = tiempo / duracion;

            materialInstanciado.SetFloat("Dissolve", valor);

            yield return null;
        }

        if (doorCollider != null)
            doorCollider.enabled = false;

        gameObject.SetActive(false);
    }
}