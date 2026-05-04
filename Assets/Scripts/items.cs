using UnityEngine;
public enum WeaponType
{
    none,
    sword,
    crossbow,
    heal,
    bomb
}
[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObject/Items", order = 1)]

public class items : ScriptableObject
{
    public string itemName;
    public WeaponType typeItem;
    public Sprite itemImage;
    //puntos de daño
    public int pto;
    [TextArea]
    public string msg;
    [TextArea]
    public string description;

}
