using UnityEngine;
using UnityEngine.UI;

public class Preview : MonoBehaviour
{
    private GameObject player;

    [HideInInspector] public Image chest;
    [HideInInspector] public Image leg;
    [HideInInspector] public Image foot;

    //Called when script is loaded (before Start is called)
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //This finds the cloth slots on preview
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Cloth"))
            {
                switch (child.name)
                {
                    case "Chest":
                        {
                            chest = child.GetComponent<Image>();
                            break;
                        }
                    case "Leg":
                        {
                            leg = child.GetComponent<Image>();
                            break;
                        }
                    case "Foot":
                        {
                            foot = child.GetComponent<Image>();
                            break;
                        }
                }
            }
        }
    }

    //Called when object is activated
    private void OnEnable()
    {
        //Set player clothes on preview
        foreach (Transform child in player.transform)
        {
            if (child.CompareTag("Cloth") && child.GetComponent<SpriteRenderer>().sprite != null)
            {
                switch (child.name)
                {
                    case "Chest":
                        {
                            chest.sprite = child.GetComponent<SpriteRenderer>().sprite;
                            break;
                        }
                    case "Leg":
                        {
                            leg.sprite = child.GetComponent<SpriteRenderer>().sprite;
                            break;
                        }
                    case "Foot":
                        {
                            foot.sprite = child.GetComponent<SpriteRenderer>().sprite;
                            break;
                        }
                }
            }
        }
    }
}
