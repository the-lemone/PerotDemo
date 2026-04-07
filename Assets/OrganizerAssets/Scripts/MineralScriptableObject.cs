using UnityEngine;

[CreateAssetMenu(fileName = "Mineral", menuName = "ScriptableObjects/MineralScriptableObject", order =1)]
public class MineralScriptableObject : ScriptableObject
{
    public string mineralName;
    public Sprite mineralSprite;
    public Color spriteColor = Color.white;
    
    [Header("Attributes")]
    [Tooltip("Based off Moh's Scale")]
    public int hardness;
    
    [Tooltip("1 = Cubic\n" +
             "2 = Tetragonal\n" +
             "3 = Hexagonal\n" +
             "4 = Rhombohedral\n" +
             "5 = Orthorhombic\n" +
             "6 = Monoclinic\n" +
             "7 = Triclinic\n")]
    [Range(1, 7)] public int crystalStructure;
    
    [Header("Animation")]
    public RuntimeAnimatorController animatorController; // each mineral's shine animation
}
