using UnityEngine;

[CreateAssetMenu(fileName = "Mineral", menuName = "ScriptableObjects/MineralScriptableObject", order =1)]
public class MineralScriptableObject : ScriptableObject
{
    public string mineralName;
    public Sprite mineralSprite;
    
    [Header("Attributes")]
    [Tooltip("Based off Moh's Scale")]
    public float hardness;
    [Tooltip("1 = Dull\n" +
             "2 = Pearly\n" +
             "3 = Adamantine\n" +
             "4 = Silky\n" +
             "5 = Greasy\n" +
             "6 = Resinous\n" +
             "7 = Vitreous")]
    public float luster;
    public float density;
    
    [Header("Animation")]
    public RuntimeAnimatorController animatorController; // each mineral's shine animation
}
