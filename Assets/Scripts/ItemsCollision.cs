using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class ItemsCollision : MonoBehaviour
{
    public items drop;
    public WeaponType wType;
    public int arrows, potions;
    //    public bool fireMagic;
    public bool open;
    public string notificationText;
    public Transform upPoint;
    GameObject player;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (drop != null)
        {
            wType = drop.typeItem;
            notificationText = drop.msg;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !open)
        {
            player = other.gameObject;
            player.GetComponent<PlayerMotion>().chest = this;
            UIManager.Instance.showInteractuar();
        }
        if (other.CompareTag("Item"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !open)
        {
            player.GetComponent<PlayerMotion>().chest = null;
            player = null;
            UIManager.Instance.hideInteractuar(); ;
        }
    }
    public void Open()
    {
        if (open)
            return;
        open = true;
        player.GetComponent<PlayerMotion>().interacting = true;
        //player.GetComponent<PlayerMotion>().Stopping();
        UIManager.Instance.hideInteractuar();
        anim.enabled = true;
        StartCoroutine("Finish");
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(2f);
        player.GetComponent<PlayerMotion>().selectTarget(upPoint);
        UIManager.Instance.showNotification(drop.msg);
        yield return new WaitForSeconds(.2f);
        //item.SetActive(true);
        //yield return new WaitForSeconds(2f);
        UIManager.Instance.hideNotification();
        //item.SetActive(false);
        player.GetComponent<PlayerMotion>().chest = null;
        player.GetComponent<PlayerMotion>().interacting = false;
        player.GetComponent<PlayerMotion>().StopEnd();
        //        if (fireMagic)
        //        {
        //            player.GetComponent<PlayerCombat>().fireExist = fireMagic;
        //            UIManager.Instance.ShowFire();

        //        }
        if (arrows != 0)
        {
            player.GetComponent<Inventario>().arrows += arrows;
            UIManager.Instance.showIcon();
            UIManager.Instance.UpdateArrows(player.GetComponent<Inventario>().arrows);

        }
        switch (wType)
        {
            case WeaponType.sword:
                player.GetComponent<Inventario>().swordActive(drop);
                player.GetComponent<Inventario>().weapons.Add(drop);
                break;
            case WeaponType.crossbow:
                player.GetComponent<Inventario>().crossbowActive(drop);
                player.GetComponent<Inventario>().weapons.Add(drop);
                break;
            case WeaponType.heal:
                if (player.GetComponent<Inventario>().items.Where(i => i == drop).Count() == 0)
                {
                    player.GetComponent<Inventario>().items.Add(drop);
                    if (player.GetComponent<PlayerCombat>().itemActual == null)
                    {
                        player.GetComponent<PlayerCombat>().itemActual = player.GetComponent<Inventario>().items[0];
                    }
                }
                player.GetComponent<Inventario>().potions += potions;
                UIManager.Instance.showIcon();
                UIManager.Instance.UpdatePotions(player.GetComponent<Inventario>().potions);
                break;
            default:
                break;
        }
        player.GetComponent<PlayerMotion>().noTarget();
        player = null;
    }
}
