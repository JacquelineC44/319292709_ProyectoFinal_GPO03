//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine.UI;
//using System;
//using UnityEngine.EventSystems;
//using Unity.VisualScripting;

//public class UIInventoryItem : MonoBehaviour
//{
//    [SerializeField]
//    private Image itemImage;
//    [SerializeField]
//    private TMP_Text quantityTxt;

//    [SerializeField]
//    private Image borderImage;

//    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;
//    private bool empty = true;

//    public void Awake()
//    {
//        ResetData();
//        Deselect();
//    }
//    public void ResetData()
//    {
//        this.itemImage.gameObject.SetActive(false);
//        empty = true;        
//    }
//    public void Deselect()
//    {
//        borderImage.enabled = false;
//    }
//    public void SetData(Sprite sprite, int quantity)
//    {
//        this.itemImage.gameObject.SetActive(true);
//        this.itemImage.sprite = sprite;
//        this.quantityTxt.text = quantity + "";
//        empty = false;

//    }
//    public void Select()
//    {
//        borderImage.enabled = true;
//    }
//    public void OnBeginDrag()
//    {
//        if (empty)
//            return;
//        OnItemBeginDrag?.Invoke(this);
//    }
//    public void OnDrop()
//    {
//        OnItemDroppedOn?.Invoke(this);
//    }
//    public void OnPointerClick(BaseEventData data)
//    {
//        PointerEventData pointerData = (PointerEventData)data;
//        if(pointerData.button == PointerEventData.InputButton.Right)
//        {
//            OnRightMouseBtnClick?.Invoke(this);
//        }
//        else
//        {
//            OnItemClicked?.Invoke(this);
//        }
//    }
//}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UIInventoryItem : MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IBeginDragHandler,
    IEndDragHandler,
    IDropHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;
    [SerializeField] private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked;
    public event Action<UIInventoryItem> OnItemDroppedOn;
    public event Action<UIInventoryItem> OnItemBeginDrag;
    public event Action<UIInventoryItem> OnItemEndDrag;
    public event Action<UIInventoryItem> OnItemActionRequested;

    private bool empty = true;

    private void Awake()
    {
        Deselect();
    }

    public void ResetData()
    {
        itemImage.gameObject.SetActive(false);

        quantityTxt.text = "";
        quantityTxt.gameObject.SetActive(false);

        empty = true;
    }

    public void SetData(Sprite sprite, int quantity)
    {
        empty = false;

        itemImage.sprite = sprite;
        itemImage.enabled = true;
        itemImage.gameObject.SetActive(true);

        quantityTxt.text = quantity.ToString();
        quantityTxt.gameObject.SetActive(true);
    }

    public void Select()
    {
        borderImage.enabled = true;
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (empty) 
            return;

        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnItemActionRequested?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
    public void OnSelect(BaseEventData eventData)
    {
        OnItemClicked?.Invoke(this);
    }

    public void OnSubmit(BaseEventData eventData)
    {
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (empty)
            return;

        OnItemClicked?.Invoke(this);
    }
}