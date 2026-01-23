using UnityEngine;

[CreateAssetMenu(fileName = "Mineral", menuName = "ScriptableObjects/MineralScriptableObject", order =1)]
public class MineralScriptableObject : ScriptableObject
{
    public string mineralName;
    public Sprite mineralSprite;
    public Color spriteColor = Color.white;
    
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
    
    [Tooltip("0 = White\n" +
             "1 = Red\n" +
             "2 = Orange\n" +
             "3 = Yellow\n" +
             "4 = Green\n" +
             "5 = Blue\n" +
             "6 = Indigo\n" +
             "7 = Violet\n")]
    [Range(0, 7)] public int color;
    
    [Header("Animation")]
    public RuntimeAnimatorController animatorController; // each mineral's shine animation
}
