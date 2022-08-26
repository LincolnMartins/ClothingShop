using UnityEngine;

[CreateAssetMenu(fileName = "Cloth", menuName = "ScriptableObjects/Cloth")]
public class Cloth : ScriptableObject
{
    public Sprite Icon;
    public Sprite Preview;
    //public Texture2D spriteSheet;
    public RuntimeAnimatorController animator;
    public string itemName;
    public float price;
    public string type;
}
