using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClothStore : MonoBehaviour
{
    public List<Cloth> clothes; //Clothes to sell
    public GameObject clothSlotPrefab;

    //Shop, Cart and Preview
    public GameObject shopView;
    public GameObject cartView;
    public GameObject preview;

    //Store text
    public TextMeshProUGUI total;
    public TextMeshProUGUI balance;

    public AudioSource cartSound;

    public float animationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //Put clothes for sale
        foreach(var cloth in clothes)
        {
            foreach (var shopcloth in CanvasManager.canvasManager.shopClothes)
            {
                if (!shopcloth.activeSelf)
                {
                    shopcloth.SetActive(true);
                    shopcloth.GetComponent<ClothSlot>().SetClothData(cloth);
                    break;
                }
            }
        }
    }

    //Called when object is activated
    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero; //Set UI scale to zero
        CanvasManager.canvasManager.openAnimation[gameObject] = true; //set animation to be executed
        CanvasManager.canvasManager.openUI.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate and Show Total Value and Balance
        var totalvalue = PurchaseValue();
        total.text = $"${totalvalue}";
        balance.text = $"{CanvasManager.canvasManager.playerBalance - totalvalue}";
    }

    //Return total value of items on cart
    public float PurchaseValue()
    {
        var val = 0f;
        foreach (Transform child in cartView.transform)
            if(child.gameObject.activeSelf)
                val += child.gameObject.GetComponent<ClothSlot>().clothData.price;
        return val;
    }

    //Close Cloth Store Window
    public void CloseStore()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            UnselectCloth(); //Unselect selected cloth in the shop
            CanvasManager.canvasManager.closeAnimation[gameObject] = true;
            CanvasManager.canvasManager.closeUI.Play();
            foreach (Transform child in cartView.transform)
                child.gameObject.SetActive(false);
        }
    }

    //Add cloth to cart
    public void AddToCart()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            cartSound.Play();
            foreach (Transform child in shopView.transform)
            {
                var clothslot = child.gameObject.GetComponent<ClothSlot>();
                if (!clothslot.selected) continue; //Check if cloth is selected

                /*
                //Check if player already has same cloth in cart
                foreach (Transform child in CanvasManager.canvasManager.clothStoreUI.GetComponent<ClothStore>().cartView.transform)
                    if (child.gameObject.GetComponent<ClothSlot>().clothData == clothslot.clothData)
                        return;
                */

                //make a clone of the selected cloth in the cart
                foreach (var cartslot in CanvasManager.canvasManager.cartSlots)
                {
                    if (!cartslot.activeSelf)
                    {
                        cartslot.SetActive(true);
                        cartslot.GetComponent<ClothSlot>().SetClothData(clothslot.clothData);
                        cartslot.transform.localScale = new Vector3(.5f, .5f, .5f);
                        return;
                    }
                }

                //Open message box
                CanvasManager.canvasManager.messageBox.SetActive(true);
                CanvasManager.canvasManager.SetMessageBoxText("Your cart is full!");
                CanvasManager.canvasManager.openAnimation[CanvasManager.canvasManager.messageBox] = true;
                CanvasManager.canvasManager.openUI.Play();
                break;
            }
        }
    }

    //Called when click purchase button
    public void Purchase()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            var totalclothes = CanvasManager.canvasManager.playerClothes.Count;
            foreach (Transform child in cartView.transform) if (child.gameObject.activeSelf) totalclothes++;
            if (totalclothes < 100)
            {
                var total = PurchaseValue();
                if (CanvasManager.canvasManager.playerBalance >= total)
                {
                    CanvasManager.canvasManager.playerBalance -= total;

                    foreach (Transform child in cartView.transform)
                        if (child.gameObject.activeSelf)
                            CanvasManager.canvasManager.playerClothes.Add(child.gameObject.GetComponent<ClothSlot>().clothData);

                    CloseStore();
                    return;
                }
                else CanvasManager.canvasManager.SetMessageBoxText("Not enough money!");
            }
            else CanvasManager.canvasManager.SetMessageBoxText("Not enough space on wardrobe!");

            //Open Message box 
            CanvasManager.canvasManager.messageBox.SetActive(true);
            CanvasManager.canvasManager.openAnimation[CanvasManager.canvasManager.messageBox] = true;
            CanvasManager.canvasManager.openUI.Play();
        }
    }

    //Unselect selected cloth
    public void UnselectCloth()
    {
        foreach (Transform child in shopView.transform)
        {
            var clothslot = child.gameObject.GetComponent<ClothSlot>();
            if (clothslot.selected)
            {
                clothslot.selected = false;
                clothslot.transform.localScale = new Vector3(.5f, .5f, .5f);
            }
        }
    }
}