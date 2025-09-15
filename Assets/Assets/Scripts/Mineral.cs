using UnityEngine;

public class Mineral : MonoBehaviour
{
    public MineralScriptableObject mineralValues;
    
    private void Start()
    {
        if (mineralValues != null)
        {
            GetComponent<SpriteRenderer>().sprite = mineralValues.mineralSprite;
            gameObject.name = mineralValues.mineralName;
        }
    }
}
