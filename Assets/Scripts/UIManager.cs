using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject notification;
    public GameObject interactuar;
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
    public void showNotification()
    {
        if(!notification.activeSelf)
            notification.SetActive(false);
    }
}
