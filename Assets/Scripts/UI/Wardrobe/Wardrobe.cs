using TMPro;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    public GameObject ownedClothPrefab;
    public GameObject viewList;
    public GameObject preview;

    [HideInInspector] public OwnedCloth selectedCloth;

    void OnEnable()
    {
        //List all player Clothes
        foreach (var cloth in CanvasManager.canvasManager.playerClothes)
        {
            var prefab = Instantiate(ownedClothPrefab, viewList.transform);
            prefab.GetComponent<OwnedCloth>().SetClothData(cloth);
        }

        CanvasManager.canvasManager.openAnimation[gameObject] = true; //execute open animation
    }

    //Close Wardrobe Window
    public void CloseWardrobe()
    {
        if (!CanvasManager.canvasManager.messageBox.activeSelf)
        {
            foreach (Transform child in viewList.transform)
                Destroy(child.gameObject);

            CanvasManager.canvasManager.closeAnimation[gameObject] = true; //execute close animation
        }
    }

    //Called when player click in "save outfit" button
    public void SaveOutfit()
    {
        CanvasManager.canvasManager.playerWearing = (Cloth[])preview.GetComponent<Preview>().wearing.Clone();
        SetPlayerClothes();
        CloseWardrobe();
    }

    //Set preview clothes on player
    void SetPlayerClothes()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        foreach (Transform child in player.transform)
        {
            child.GetComponent<SpriteRenderer>().sprite = null;
            child.GetComponent<Animator>().runtimeAnimatorController = null;

            switch (child.name)
            {
                case "Chest":
                    {
                        if (CanvasManager.canvasManager.playerWearing[0] != null)
                        {
                            child.GetComponent<SpriteRenderer>().sprite = CanvasManager.canvasManager.playerWearing[0].Preview;
                            child.GetComponent<Animator>().runtimeAnimatorController = CanvasManager.canvasManager.playerWearing[0].animator;
                        }
                        break;
                    }
                case "Leg":
                    {
                        if (CanvasManager.canvasManager.playerWearing[1] != null)
                        {
                            child.GetComponent<SpriteRenderer>().sprite = CanvasManager.canvasManager.playerWearing[1].Preview;
                            child.GetComponent<Animator>().runtimeAnimatorController = CanvasManager.canvasManager.playerWearing[1].animator;
                        }
                        break;
                    }
                case "Foot":
                    {
                        if (CanvasManager.canvasManager.playerWearing[2] != null)
                        {
                            child.GetComponent<SpriteRenderer>().sprite = CanvasManager.canvasManager.playerWearing[2].Preview;
                            child.GetComponent<Animator>().runtimeAnimatorController = CanvasManager.canvasManager.playerWearing[2].animator;
                        }
                        break;
                    }
            }
        }
        player.SetActive(true);
        player.GetComponent<Animator>().SetFloat("Idle_Vertical", -1); //bug correction
    }

    //Called when player click in "drop cloth" button
    public void DropCloth()
    {
        int totalcloth = 0;
        foreach(var cloth in CanvasManager.canvasManager.playerClothes)
            if (cloth.name == selectedCloth.clothData.name)
                totalcloth++;

        if (totalcloth == 1)
        {
            //Remove Cloth from player
            var wear = CanvasManager.canvasManager.playerWearing;
            for (int i = 0; i < wear.Length; i++)
            {
                if (wear[i] != null && selectedCloth.clothData.name == wear[i].name)
                {
                    wear[i] = null;
                    SetPlayerClothes();
                    break;
                }
            }

            //Remove cloth from preview
            var wearprev = preview.GetComponent<Preview>();
            for (int i = 0; i < wearprev.wearing.Length; i++)
            {
                if (wearprev.wearing[i] != null && selectedCloth.clothData.name == wearprev.wearing[i].name)
                {
                    wearprev.wearing[i] = null;

                    switch(i)
                    {
                        case 0: //Chest
                            {
                                wearprev.chest.sprite = null;
                                CanvasManager.canvasManager.SetTransparency(wearprev.chest, 0f);
                                break;
                            }
                        case 1: //Leg
                            {
                                wearprev.leg.sprite = null;
                                CanvasManager.canvasManager.SetTransparency(wearprev.leg, 0f);
                                break;
                            }
                        case 2: //Foot
                            {
                                wearprev.foot.sprite = null;
                                CanvasManager.canvasManager.SetTransparency(wearprev.foot, 0f);
                                break;
                            }
                    }

                    break;
                }
            }
        }

        //Remove Cloth
        foreach (var cloth in CanvasManager.canvasManager.playerClothes)
        {
            if (cloth.name == selectedCloth.clothData.name)
            {
                Destroy(selectedCloth.gameObject);
                CanvasManager.canvasManager.playerClothes.Remove(cloth);
                break;
            }
        }

        selectedCloth.clothData = null;
    }
}
