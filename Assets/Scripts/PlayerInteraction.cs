using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour, IPointerClickHandler
{
    private GameObject player;

    //Materials to change in object component when outlined
    public Material standardMaterial;
    public Material outlineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Show Outline around interactable objects from scenario when player is near
        if(Vector2.Distance(transform.position, player.transform.position) <= 3)
            gameObject.GetComponent<SpriteRenderer>().material = outlineMaterial;
        else gameObject.GetComponent<SpriteRenderer>().material = standardMaterial;
    }

    // Called when player click over Object
    public void OnPointerClick(PointerEventData eventData)
    {
        //Check if player is near clicked object
        if (Vector2.Distance(transform.position, player.transform.position) <= 3)
        {
            //Check what object is and show UI to interact
            switch (gameObject.tag)
            {
                case "Shopkeeper": {
                    CanvasManager.canvasManager.clothStoreUI.SetActive(true);
                    break;
                }
                case "Wardrobe": {
                    CanvasManager.canvasManager.WardrobeUI.SetActive(true);
                    break;
                }
            }
        }
    }
}
