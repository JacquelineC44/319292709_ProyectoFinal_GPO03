using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Life : MonoBehaviour
{
    [SerializeField]  public int maxlife;
    public int currentLife
    {
        get { return m_life; }
        set
        {
            if (value > maxlife)
            {
                m_life = maxlife;
            }
            else
            {
                m_life = value;
            }
        }
    }
    int m_life;
    public Animator anim;
    public Rigidbody rb;
    public GameObject targetPoint;
    public GameObject player;
    private void Start()
    {
        m_life = maxlife;
        if (GetComponentInChildren<Animator>())
            anim = GetComponentInChildren<Animator>();
        if (GetComponent<Rigidbody>())
            rb = GetComponent<Rigidbody>();
    }
    public virtual void GetHit(int damage)
    {
        Debug.Log("ENEMIGO RECIBE DAÑO: " + damage + " tiempo: " + Time.time);
        currentLife -= damage;
    }
}
