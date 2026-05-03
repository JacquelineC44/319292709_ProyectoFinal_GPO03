using UnityEngine;
using UnityEngine.InputSystem;


public class InventoryCOntroller : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;

    public int inventorySize = 10;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        inventoryUI.Hide();
    }

    public void OnActiveInventory()
    {
        if (inventoryUI.isActiveAndEnabled == false)
            inventoryUI.Show();
        else
            inventoryUI.Hide();
    }
}
