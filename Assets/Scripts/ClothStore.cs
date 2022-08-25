using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private bool openAnimation = false;
    private bool closeAnimation = false;

    public float animationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        //Put clothes for sale
        foreach(var cloth in clothes)
        {
            var prefab = Instantiate(clothSlotPrefab, shopView.transform);
            prefab.GetComponent<ClothSlot>().SetClothData(cloth);
        }
    }

    //Called when object is activated
    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.zero; //Set UI scale to zero
        openAnimation = true; //set animation to be executed
    }

    // Update is called once per frame
    void Update()
    {
        //Animate UI when show/hide
        if(openAnimation)
        {
            gameObject.transform.localScale += new Vector3(1, 1, 1) * animationSpeed * Time.deltaTime;

            if (gameObject.transform.localScale.x > 1 || gameObject.transform.localScale.y > 1 || gameObject.transform.localScale.z > 1)
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                openAnimation = false;
            }
        }
        else if(closeAnimation)
        {
            gameObject.transform.localScale -= new Vector3(1, 1, 1) * animationSpeed * Time.deltaTime;

            if (gameObject.transform.localScale.x <= 0 || gameObject.transform.localScale.y <= 0 || gameObject.transform.localScale.z <= 0)
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                closeAnimation = false;
                UnselectCloth(); //Unselect selected cloth in the shop
                gameObject.SetActive(false);
            }
        }

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
            val += child.gameObject.GetComponent<ClothSlot>().clothData.price;
        return val;
    }

    //Close Cloth Store Window
    public void CloseStore()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            closeAnimation = true;
            foreach (Transform child in cartView.transform)
                Destroy(child.gameObject);
        }
    }

    //Add cloth to cart
    public void AddToCart()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            foreach (Transform child in shopView.transform)
            {
                var clothslot = child.gameObject.GetComponent<ClothSlot>();
                if (!clothslot.selected) continue; //Check what cloth is selected

                var clothstore = CanvasManager.canvasManager.clothStoreUI.GetComponent<ClothStore>();

                //Check if player already has same cloth in cart
                /*foreach (Transform child in clothstore.cartView.transform)
                    if (child.gameObject.GetComponent<ClothSlot>().clothData == clothslot.clothData)
                        return;*/

                //Create a clone of the selected cloth in the cart
                var prefab = Instantiate(clothslot, clothstore.cartView.transform);
                prefab.GetComponent<ClothSlot>().SetClothData(clothslot.clothData);
                prefab.transform.localScale = new Vector3(1, 1, 1);
            }
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
                clothslot.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}