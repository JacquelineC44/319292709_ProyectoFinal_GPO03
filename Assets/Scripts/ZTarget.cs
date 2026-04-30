using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections;
public class ZTarget : MonoBehaviour
{

    public float viewScope;
    public Transform cam;
    public Transform t;
    public List<Transform> impacts;
    public List<Transform> targetsL;
    public List<Transform> targetsR;

    private void Awake()
    {
        impacts = new List<Transform>();
        targetsL = new List<Transform>();
        targetsR = new List<Transform>();
    }
    //Solamente usarlo para que el focus sea los enemigos que tiene de enfrente, escoge el mas cercano
    public Transform FirstTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);
        impacts.Clear();
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Target")
            {
                if (!impacts.Contains(hitCollider.transform))
                {
                    Vector3 dir = (hitCollider.transform.position - cam.position).normalized;
                    float f = Vector3.Dot(dir, cam.forward);
                    if (f > 0)
                        impacts.Add(hitCollider.transform);
                }
            }
        }

        impacts = impacts.OrderBy(i => Vector3.Distance(cam.position, i.position)).ToList();
        if (impacts.Count == 0)
            impacts.Add(null);
        t = impacts[0];
        return t;
    }
    private void UpdateImpacts()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);
        impacts.Clear();
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Target")
            {
                if (!impacts.Contains(hitCollider.transform))
                    impacts.Add(hitCollider.transform);
            }
        }
        impacts = impacts.OrderBy(i => Vector3.Distance(cam.position, i.position)).ToList();
        if (impacts.Count == 0)
        {
            impacts.Add(null);
            t = impacts[0];
        }
    }
    public Transform NextToLeft()
    {
        UpdateImpacts();
        if (impacts.Count > 1)
        {
            targetsL.Clear();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Target"))
                {
                    if (!targetsL.Contains(hitCollider.transform))
                        targetsL.Add(hitCollider.transform);
                }
            }
            targetsL = targetsL.OrderBy(i =>
            {
                Vector3 dir = (i.position - cam.position).normalized;
                float f = Vector3.Dot(dir, cam.right);
                return f;
            }).ToList();

            if (targetsL[0] == t)
            {
                MaxRight();
            }
            else
            {
                Transform previous = null;
                foreach (Transform e in targetsL)
                {
                    if (e == t)
                        break;
                    previous = e;
                }
                t = previous;

            }
        }
        return t;
    }

    public Transform NextToRight()
    {
        UpdateImpacts();
        if (impacts.Count > 1)
        {
            targetsR.Clear();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Target"))
                {
                    if (!targetsR.Contains(hitCollider.transform))
                        targetsR.Add(hitCollider.transform);
                }
            }

            targetsR = targetsR.OrderByDescending(i =>
            {
                Vector3 dir = (i.position - cam.position).normalized;
                float f = Vector3.Dot(dir, cam.right);
                return f;
            }).ToList();

            if (targetsR[0] == t)
            {
                MaxLeft();
            }
            else
            {
                Transform previous = null;
                foreach (Transform e in targetsR)
                {
                    if (e == t)
                        break;
                    previous = e;
                }
                t = previous;

            }
        }
        return t;
    }

    public void MaxLeft()
    {
        targetsL.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Target"))
            {
                if (!targetsL.Contains(hitCollider.transform))
                    targetsL.Add(hitCollider.transform);
            }
        }
        targetsL = targetsL.OrderBy(i =>
        {
            Vector3 dir = (i.position - cam.position).normalized;
            float f = Vector3.Dot(dir, cam.right);
            return f;
        }).ToList();

        targetsL.Remove(t);
        t = targetsL[0];

    }
    public void MaxRight()
    {
        targetsR.Clear();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, viewScope);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("Target"))
            {
                if (!targetsR.Contains(hitCollider.transform))
                    targetsR.Add(hitCollider.transform);
            }
        }
        targetsR = targetsR.OrderBy(i =>
        {
            Vector3 dir = (i.position - cam.position).normalized;
            float f = Vector3.Dot(dir, cam.right);
            return f;
        }).ToList();

        targetsR.Remove(t);
        t = targetsR[0];
    }
}
