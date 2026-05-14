using UnityEngine;
using UnityEngine.InputSystem;


public class InventoryCOntroller : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private UnityEngine.InputSystem.PlayerInput playerinput;
    [SerializeField] private items espadaInicial;

    public int inventorySize = 10;

    private void Awake()
    {
        playerinput = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        inventoryUI.UpdateData(0, espadaInicial, 1);
        inventoryUI.Hide();
    }

    public void OnActiveInventory()
    {
        inventoryUI.Show();
        playerinput.SwitchCurrentActionMap("UIInventory");
            
    }
    public void OnCloseInventory()
    {
        inventoryUI.Hide();
        playerinput.SwitchCurrentActionMap("CharacterController");
    }
    public void AddItemToInventory(items item, int quantity)
    {
        if (item == null)
            return;

        if (item.typeItem == WeaponType.heal)
        {
            inventoryUI.UpdateData(1, item, quantity);
        }
    }
}
