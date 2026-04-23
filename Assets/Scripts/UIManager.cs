using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using JetBrains.Annotations;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public GameObject notification;
    public GameObject interactuar;
    public GameObject icons;
    public TMPro.TextMeshProUGUI potionText;
    public TMPro.TextMeshProUGUI arrowText;
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void showInteractuar()
    {
        interactuar.SetActive(true);
    }
    public void hideInteractuar()
    {
        interactuar.SetActive(false);
    }
    public void showNotification(string msg)
    {
        if(!notification.activeSelf)
            notification.SetActive(false);
        notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
        notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720,200), .2f).OnComplete(() => notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = msg);
    }
    public void hideNotification()
    {
        notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720, 0), .2f).OnComplete(() => notification.SetActive(false));
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


}
