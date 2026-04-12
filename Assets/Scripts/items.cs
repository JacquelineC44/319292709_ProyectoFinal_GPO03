using UnityEngine;
public enum WeaponType
{
    none,
    sword,
    heal,
    bomb
}
[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObject/Items", order = 1)]

public class items : ScriptableObject
{
    public string itemName;
    public WeaponType typeItem;
    public int pto;
    [TextArea]
    public string msg;

}
