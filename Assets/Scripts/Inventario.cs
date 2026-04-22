using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public GameObject sword; 
    public List<items> weapons;
    public List<items> items;
    public bool swordUse;
    Animator anim;
    PlayerCombat playerCombat;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        weapons = new List<items>();
        items = new List<items>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    public void swordActive(items item)
    {
        sword.SetActive(true);
        anim.SetFloat("WeaponN", 0);
        if (!swordUse)
        {
            swordUse = true;
            anim.SetBool("Weapon", true);
            playerCombat.enabled = true;
        }
        anim.SetTrigger("SwitchWeapon");
        playerCombat.weaponActual = item;
        sword.GetComponent<swordCollision>().attack = playerCombat.weaponActual.pto;
    }


}
