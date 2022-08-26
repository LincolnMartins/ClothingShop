using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager canvasManager; //Singleton

    //Game UI
    public GameObject clothStoreUI;
    public GameObject WardrobeUI;
    public GameObject messageBox;

    [HideInInspector] public Dictionary<GameObject, bool> openAnimation = new Dictionary<GameObject, bool>();
    [HideInInspector] public Dictionary<GameObject, bool> closeAnimation = new Dictionary<GameObject, bool>();

    //Player Stats
    [HideInInspector] public float playerBalance = 1000;
    [HideInInspector] public List<Cloth> playerClothes = new List<Cloth>();
    [HideInInspector] public Cloth[] playerWearing = new Cloth[3];

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

        //Fill GUI Animations dicts
        openAnimation.Add(clothStoreUI, false);
        closeAnimation.Add(clothStoreUI, false);

        openAnimation.Add(WardrobeUI, false);
        closeAnimation.Add(WardrobeUI, false);

        openAnimation.Add(messageBox, false);
        closeAnimation.Add(messageBox, false);
    }

    // Update is called once per frame
    void Update()
    {
        //Animate GUI when show
        foreach (var opengui in openAnimation)
        {
            if(opengui.Value)
            {
                var tmpval = 1f;
                if (opengui.Key == messageBox) tmpval = .5f;
                opengui.Key.transform.localScale += new Vector3(tmpval, tmpval, tmpval) * 5 * Time.deltaTime;

                if (opengui.Key.transform.localScale.x > tmpval || opengui.Key.transform.localScale.y > tmpval || opengui.Key.transform.localScale.z > tmpval)
                {
                    opengui.Key.transform.localScale = new Vector3(tmpval, tmpval, tmpval);
                    openAnimation[opengui.Key] = false;
                    break;
                }
            }
        }

        //Animate GUI when hide
        foreach (var closegui in closeAnimation)
        {
            if (closegui.Value)
            {
                var tmpval = 1f;
                if (closegui.Key == messageBox) tmpval = .5f;

                closegui.Key.transform.localScale -= new Vector3(tmpval, tmpval, tmpval) * 5 * Time.deltaTime;

                if (closegui.Key.transform.localScale.x <= 0 || closegui.Key.transform.localScale.y <= 0 || closegui.Key.transform.localScale.z <= 0)
                {
                    closegui.Key.transform.localScale = new Vector3(0, 0, 0);
                    closeAnimation[closegui.Key] = false;
                    closegui.Key.SetActive(false);
                    break;
                }
            }
        }
    }

    // Set cloth on target preview object
    public void SetClothPreview(GameObject preview, Cloth cloth)
    {
        foreach (Transform child in preview.transform)
        {
            if (child.name == cloth.type)
            {
                switch (child.name)
                {
                    case "Chest":
                        {
                            preview.GetComponent<Preview>().chest.sprite = cloth.Preview;
                            SetTransparency(preview.GetComponent<Preview>().chest, 255f);
                            preview.GetComponent<Preview>().wearing[0] = cloth;
                            break;
                        }
                    case "Leg":
                        {
                            preview.GetComponent<Preview>().leg.sprite = cloth.Preview;
                            SetTransparency(preview.GetComponent<Preview>().leg, 255f);
                            preview.GetComponent<Preview>().wearing[1] = cloth;
                            break;
                        }
                    case "Foot":
                        {
                            preview.GetComponent<Preview>().foot.sprite = cloth.Preview;
                            SetTransparency(preview.GetComponent<Preview>().foot, 255f);
                            preview.GetComponent<Preview>().wearing[2] = cloth;
                            break;
                        }
                }

                break;
            }
        }
    }

    public void SetTransparency(Image img, float val)
    {
        var tempColor = img.color;
        tempColor.a = val;
        img.color = tempColor;
    }

    //Called when click on close button in messagebox
    public void Close()
    {
        closeAnimation[messageBox] = true;
    }
}
