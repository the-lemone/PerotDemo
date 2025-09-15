using UnityEngine;

[CreateAssetMenu(fileName = "Mineral", menuName = "ScriptableObjects/MineralScriptableObject", order =1)]
public class MineralScriptableObject : ScriptableObject
{
    public string mineralName;
    public Sprite mineralSprite;
    public float hardness;
    public float luster;
    public float density;
}
