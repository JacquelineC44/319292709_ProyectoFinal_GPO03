using UnityEngine;
using System.Collections.Generic;

public class Inventario : MonoBehaviour
{
    public GameObject sword;
    public GameObject crossbow;
    public List<items> weapons;
    public List<items> items;
    public bool swordUse;
    public int arrows, potions;
    Animator anim;
    PlayerCombat playerCombat;
    private items equippedItem;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        weapons = new List<items>();
        items = new List<items>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    public void swordActive(items item)
    {
        crossbow.SetActive(false);
        sword.SetActive(true);
        swordUse = true;
        anim.SetFloat("WeaponN", 0);
        anim.SetBool("Weapon", true);
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
    public void crossbowActive(items item)
    {
        sword.SetActive(false);
        crossbow.SetActive(true);
        //cambiar a 1 (solo es prueba con el otro anim)
        anim.SetFloat("WeaponN", 2);
        anim.SetTrigger("SwitchWeapon");
        playerCombat.weaponActual = item;
    }
    public void swordInactive()
    {
        sword.SetActive(false);

        swordUse = false;

        anim.SetBool("Weapon", false);
        anim.SetFloat("WeaponN", -1);

        anim.ResetTrigger("SwitchWeapon");
        anim.SetTrigger("SwitchWeapon");
    }
    public void EquipItem(items item)
    {
        if (item == null)
            return;

        if (equippedItem == item)
        {
            swordInactive();
            equippedItem = null;
            return;
        }

        equippedItem = item;

        switch (item.typeItem)
        {
            case WeaponType.sword:
                swordActive(item);
                break;
        }
    }
    private void UnequipItem(items item)
    {
        switch (item.typeItem)
        {
            case WeaponType.sword:
                sword.SetActive(false);

                if (anim != null)
                    anim.SetBool("Weapon", false);

                break;

            case WeaponType.crossbow:
                // crossbow.SetActive(false);
                break;
        }
    }

}
