using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class OwnedCloth : MonoBehaviour
{
    [HideInInspector] public Cloth clothData;
    public Image clothIcon;
    public TextMeshProUGUI clothName;

    public AudioSource soundClick;

    private Wardrobe wardrobe;

    //Called when script loads
    private void Awake()
    {
        wardrobe = CanvasManager.canvasManager.WardrobeUI.GetComponent<Wardrobe>();
    }

    public void SetClothData(Cloth data)
    {
        clothData = data;
        clothIcon.sprite = data.Icon;
        clothName.text = data.name;
    }


    //Called when player clicks over cloth on the list
    public void SetClothOnPreview()
    {
        soundClick.Play();
        CanvasManager.canvasManager.SetClothPreview(wardrobe.preview, clothData);
        wardrobe.selectedCloth = this;
    }
}
