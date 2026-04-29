//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using JetBrains.Annotations;
//using DG.Tweening;

//public class UIManager : MonoBehaviour
//{
//    public GameObject lifeBar;
//    public GameObject notification;
//    public GameObject interactuar;
//    public GameObject icons;
//    public TMPro.TextMeshProUGUI potionText;
//    public TMPro.TextMeshProUGUI arrowText;
//    public Image potionIcon;
//    public TMPro.TextMeshProUGUI lifeText;
//    public Life playerLife;
//    //fuego
//    public Image fireIcon;
//    public Text fireText;

//    public static UIManager Instance;
//    private void Awake()
//    {
//        Instance = this;
//    }
//    private void Start()
//    {
//        UpdateLifeText();
//    }
//    public void showInteractuar()
//    {
//        interactuar.SetActive(true);
//    }
//    public void hideInteractuar()
//    {
//        interactuar.SetActive(false);
//    }
//    public void showNotification(string msg)
//    {
//        if(!notification.activeSelf)
//            notification.SetActive(false);
//        notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";
//        notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720,200), .2f).OnComplete(() => notification.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = msg);
//    }
//    public void hideNotification()
//    {
//        notification.GetComponent<RectTransform>().DOSizeDelta(new Vector2(720, 0), .2f).OnComplete(() => notification.SetActive(false));
//    }
//    public void showIcon()
//    {
//        if (!icons.activeSelf)
//        {
//            icons.SetActive(true);
//            potionText.text = "0";
//            arrowText.text = "0";
//        }
//    }
//    public void UpdatePotions(int n)
//    {
//        potionText.text = n.ToString();
//    }
//    public void UpdateArrows(int n)
//    {
//        arrowText.text = n.ToString();
//    }
//    public void UpdateLife(int currentLife)
//    {
//        //lifeBar.fillAmount = (float)currentLife / playerLife.maxlife;
//        Vector2 v = new Vector2(currentLife, lifeBar.GetComponent<RectTransform>().sizeDelta.y);
//        lifeBar.GetComponent<RectTransform>().DOSizeDelta(v, .2f);
//        UpdateLifeText();
//    }
//    public void UpdateLifeText()
//    {
//        lifeText.text = playerLife.currentLife + "/" + playerLife.maxlife;
//    }
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

//}
