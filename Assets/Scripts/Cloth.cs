using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName = "Cloth", menuName = "ScriptableObjects/Cloth")]
public class Cloth : ScriptableObject
{
    public Sprite Icon;
    public Sprite Preview;
    //public Texture2D spriteSheet;
    public AnimatorController animator;
    public string itemName;
    public float price;
    public string type;
}
