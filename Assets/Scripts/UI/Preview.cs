using UnityEngine.UI;
using UnityEngine;

public class Preview : MonoBehaviour
{
    public Image chest;
    public Image leg;
    public Image foot;

    [HideInInspector] public Cloth[] wearing = new Cloth[3];

    //Called when object is activated
    public void OnEnable()
    {
        //Reset Preview
        chest.sprite = null;
        CanvasManager.canvasManager.SetTransparency(chest, 0f);
        leg.sprite = null;
        CanvasManager.canvasManager.SetTransparency(leg, 0f);
        foot.sprite = null;
        CanvasManager.canvasManager.SetTransparency(foot, 0f);

        //Set player clothes on preview
        var clothes = CanvasManager.canvasManager.playerWearing;
        for (int i = 0; i < clothes.Length; i++)
        {
            wearing[i] = null;
            if (clothes[i] != null)
                CanvasManager.canvasManager.SetClothPreview(gameObject, clothes[i]);
        }
    }
}
