using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager; //Singleton

    //Game UI
    public GameObject clothStoreUI;
    public GameObject WardrobeUI;
    public GameObject messageBox;

    //Player Stats
    [HideInInspector] public float playerBalance = 1000;
    [HideInInspector] public List<Cloth> playerClothes = new List<Cloth>();

    private bool activeMessageBox = false;

    public float animationSpeed = 5f;

    private void Awake()
    {
        if (canvasManager != null)
        {
            if (canvasManager != this)
            {
                Destroy(canvasManager.gameObject);
                canvasManager = this;
            }
        }
        else canvasManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Animate Message Box UI when show/hide
        if (messageBox.transform.localScale != new Vector3(1, 1, 1) && activeMessageBox)
        {
            messageBox.transform.localScale += new Vector3(1, 1, 1) * 5 * Time.deltaTime;

            if (messageBox.transform.localScale.x > 1 || messageBox.transform.localScale.y > 1 || messageBox.transform.localScale.z > 1)
            {
                messageBox.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if(messageBox.transform.localScale != new Vector3(0, 0, 0) && !activeMessageBox)
        {
            messageBox.transform.localScale -= new Vector3(1, 1, 1) * 5 * Time.deltaTime;

            if (messageBox.transform.localScale.x <= 0 || messageBox.transform.localScale.y <= 0 || messageBox.transform.localScale.z <= 0)
            {
                messageBox.transform.localScale = new Vector3(0, 0, 0);
                messageBox.SetActive(false);
            }
        }
    }

    //Called when click purchase button
    public void Purchase()
    {
        if (!messageBox.activeSelf)
        {
            var total = clothStoreUI.GetComponent<ClothStore>().PurchaseValue();
            if (playerBalance >= total)
            {
                playerBalance -= total;
                foreach(Transform child in clothStoreUI.GetComponent<ClothStore>().cartView.transform)
                    playerClothes.Add(child.gameObject.GetComponent<ClothSlot>().clothData);
                clothStoreUI.GetComponent<ClothStore>().CloseStore();
            }
            else
            {
                messageBox.SetActive(true);
                activeMessageBox = true;
            }
        }
    }

    //Called when click on close button in messagebox
    public void Close()
    {
        activeMessageBox = false;
    }
}
