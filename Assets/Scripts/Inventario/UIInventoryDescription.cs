using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;
    [SerializeField]
    private TMP_Text tittle;
    [SerializeField]
    private TMP_Text description;

    public void Awake()
    {
        ResetDescription();
    }

    public void ResetDescription()
    {
        this.itemImage.gameObject.SetActive(false);
        this.tittle.text = "";
        this.description.text = "";
    }

    public void SetDescription(Sprite sprite, string itemName, string itemDescription)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.tittle.text = itemName;
        this.description.text = itemDescription;
    }
}
