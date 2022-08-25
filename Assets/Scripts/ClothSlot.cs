using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class ClothSlot : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public Cloth clothData = null;
    [HideInInspector] public bool selected = false;

    private ClothStore clothstore;

    //Called when script loads
    private void Awake()
    {
        clothstore = CanvasManager.canvasManager.clothStoreUI.GetComponent<ClothStore>();
    }

    public void SetClothData(Cloth data)
    {
        clothData = data;
        GetComponent<Image>().sprite = data.Icon;
        GetComponentInChildren<TextMeshProUGUI>().text = $"${data.price}";
    }

    // Called when player click over this object
    public void OnPointerClick(PointerEventData eventData)
    {
        if (clothData != null && !CanvasManager.canvasManager.messageBox.activeSelf)
        {
            if (transform.parent.gameObject == clothstore.shopView) //Check if the object is in the shop
            {
                SelectCloth();
            }
            else if (transform.parent.gameObject == clothstore.cartView) //Check if the object is in the cart
            {
                Destroy(gameObject);
            }

            //Set cloth on preview
            foreach (Transform child in clothstore.preview.transform)
            {
                if (child.name == clothData.type)
                {
                    switch (child.name)
                    {
                        case "Chest":
                            {
                                clothstore.preview.GetComponent<Preview>().chest.sprite = clothData.Preview;
                                break;
                            }
                        case "Leg":
                            {
                                clothstore.preview.GetComponent<Preview>().leg.sprite = clothData.Preview;
                                break;
                            }
                        case "Foot":
                            {
                                clothstore.preview.GetComponent<Preview>().foot.sprite = clothData.Preview;
                                break;
                            }
                    }

                    break;
                }
            }
        }
    }

    //Called when player click on cloth in shop
    void SelectCloth()
    {
        if (!selected)
        {
            clothstore.UnselectCloth(); //Unselect last selected cloth
            selected = true; //Set Cloth selected
            transform.localScale += new Vector3(.3f, .3f, .3f);
        }
    }
}