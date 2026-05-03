
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using System;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription;

    List<UIInventoryItem> listItems = new List<UIInventoryItem>();

    public event Action<int> OnDescriptionRequested, OnItemActionRequested, OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }
    public void InitializeInventoryUI(int inventoryszie)
    {
        for (int i = 0; i < inventoryszie; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            //item.transform.SetParent(contentPanel);
            item.transform.SetParent(contentPanel, false);
            listItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            //cambio de posicion
            //item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;
            item.OnItemActionRequested += HandleShowItemActions; ;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (listItems.Count > itemIndex)
        {
            listItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }
    private void HandleShowItemActions(UIInventoryItem obj)
    {

    }
    private void HandleEndDrag(UIInventoryItem obj)
    {
        ResetDraggedItem();
    }
    //este es ek que cambia de orden
    private void HandleSwap(UIInventoryItem obj)
    {

    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;

    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {

    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        int index = listItems.IndexOf(obj);
        if (index == -1)
            return;

    }


    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();
        ResetSelection();
    }

    private void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();

    }
    private void DeselectAllItems() {
        foreach (UIInventoryItem item in listItems)
        {
            item.Deselect();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ResetDraggedItem()
    {

    }
}


   


