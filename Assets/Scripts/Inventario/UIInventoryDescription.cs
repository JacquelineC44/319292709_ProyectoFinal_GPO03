using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text titleTxt;
    [SerializeField] private TMP_Text descriptionTxt;

    public void Awake()
    {
    }
    private void Start()
    {
        ResetDescription();
    }

    public void SetDescription(Sprite image, string itemTitle, string itemDescription)
    {
        Debug.Log("Imagen recibida en descripcion: " + image);

        itemImage.sprite = image;
        itemImage.enabled = image != null;

        titleTxt.text = itemTitle;
        descriptionTxt.text = itemDescription;
    }

    public void ResetDescription()
    {
        itemImage.sprite = null;
        itemImage.enabled = false;

        titleTxt.text = "";
        descriptionTxt.text = "";
    }
}