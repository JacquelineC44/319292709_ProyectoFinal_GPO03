using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using JetBrains.Annotations;
using DG.Tweening;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public GameObject lifeBar;
    public GameObject notification;
    public GameObject interactuar;
    public GameObject icons;
    public TMPro.TextMeshProUGUI potionText;
    public TMPro.TextMeshProUGUI arrowText;
    public Image potionIcon;
    public TMPro.TextMeshProUGUI lifeText;
    public Life playerLife;
    public bool foundPotion = false;
    //    //fuego
    //    public Image fireIcon;
    //    public Text fireText;

    public static UIManager Instance;
    public float maxBarWidth = 1010f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateLifeText();
    }
    public void showInteractuar()
    {
        interactuar.SetActive(true);
    }
    public void hideInteractuar()
    {
        interactuar.SetActive(false);
    }
    //public void showNotification(string msg)
    //{
    //    if (!notification.activeSelf)
    //        notification.SetActive(true);
    //    notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
    //    notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720, 200), .2f).OnComplete(() => notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = msg);
    //}
    public void showNotification(string msg)
    {
        notification.SetActive(true);

        RectTransform rect = notification.GetComponent<RectTransform>();
        rect.localScale = Vector3.one;
        rect.sizeDelta = new Vector2(720, 200);

        TMPro.TextMeshProUGUI text = notification.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        text.gameObject.SetActive(true);
        text.text = msg;

        Debug.Log("Mostrando notificación: " + msg);
    }
    //public void hideNotification()
    //{
    //    notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720, 0), .2f).OnComplete(() => notification.SetActive(false));
    //}
    public void hideNotification()
    {
        TMPro.TextMeshProUGUI text = notification.GetComponentInChildren<TMPro.TextMeshProUGUI>(true);
        text.text = "";

        notification.GetComponent<RectTransform>().sizeDelta = new Vector2(720, 0);
        notification.SetActive(false);
    }
    public void showIcon()
    {
        if (!icons.activeSelf)
        {
            icons.SetActive(true);
            potionText.text = "0";
            arrowText.text = "0";
        }
    }
    public void UpdatePotions(int n)
    {
        potionText.text = n.ToString();
    }
    public void UpdateArrows(int n)
    {
        arrowText.text = n.ToString();
    }
    //public void UpdateLife(int currentLife)
    //{
    //    //lifeBar.fillAmount = (float)currentLife / playerLife.maxlife;
    //    Vector2 v = new Vector2(currentLife * 4, lifeBar.GetComponent<RectTransform>().sizeDelta.y);
    //    lifeBar.GetComponent<RectTransform>().DOSizeDelta(v, .2f);
    //    UpdateLifeText();
    //}
    public void UpdateLife(int currentLife)
    {
        float porcentaje = (float)currentLife / playerLife.maxlife;
        float nuevoAncho = maxBarWidth * porcentaje;

        Vector2 v = new Vector2(nuevoAncho, lifeBar.GetComponent<RectTransform>().sizeDelta.y);
        lifeBar.GetComponent<RectTransform>().DOSizeDelta(v, .2f);

        UpdateLifeText();
    }
    public void UpdateLifeText()
    {
        lifeText.text = playerLife.currentLife + "/" + playerLife.maxlife;
    }
    public void showPotion(int potions)
    {
        if (!foundPotion)
        {
            foundPotion = true;
            potionIcon.gameObject.SetActive(true);
        }
        
    }

    //    public void ShowFire()
    //    {
    //        fireIcon.gameObject.SetActive(true);
    //    }
    //    public void FireUse()
    //    {
    //        fireIcon.DOFade(0, 0);
    //    }
    //    public void ShowFireCooldown(float cooldown)
    //    {
    //        Sequence s = DOTween.Sequence();
    //        fireIcon.DOFade(1f, cooldown);
    //        s.Append(DOVirtual.Float(cooldown, 0f, cooldown, v => fireText.text = Mathf.RoundToInt(v).ToString())).OnComplete(()=> fireText.text = "");

    //    }

}
