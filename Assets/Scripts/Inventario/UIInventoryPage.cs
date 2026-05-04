
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
    private List<items> inventoryItems = new List<items>();


    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem item = Instantiate(itemPrefab, contentPanel);
            listItems.Add(item);
            inventoryItems.Add(null);

            item.OnItemClicked += HandleItemSelection;
        }
    }

    public void UpdateData(int itemIndex, items itemData, int itemQuantity)
    {
        if (itemIndex < 0 || itemIndex >= listItems.Count)
            return;

        inventoryItems[itemIndex] = itemData;
        listItems[itemIndex].SetData(itemData.itemImage, itemQuantity);
    }


    private void HandleItemSelection(UIInventoryItem obj)
    {
        int index = listItems.IndexOf(obj);

        if (index == -1)
            return;

        if (inventoryItems[index] == null)
            return;

        DeselectAllItems();
        listItems[index].Select();

        items itemData = inventoryItems[index];

        itemDescription.SetDescription(
            itemData.itemImage,
            itemData.itemName,
            itemData.description
        );
    }

    public void Show()
    {
        gameObject.SetActive(true);

        DeselectAllItems();

        if (listItems.Count > 0 && inventoryItems[0] != null)
        {
            HandleItemSelection(listItems[0]);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listItems)
        {
            item.Deselect();
        }
    }

    /*public void InitializeInventoryUI(int inventoryszie)
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

    private void HandleItemSelection(UIInventoryItem obj)
    {
        int index = listItems.IndexOf(obj);

        if (index == -1)
            return;

        DeselectAllItems();
        listItems[index].Select();

        OnDescriptionRequested?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {

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
    private void HandleBeginDrag(UIInventoryItem obj)
    {
    }*/
}


   


