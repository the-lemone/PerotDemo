using System.Data;
using UnityEngine;

public class HardnessTool : MonoBehaviour
{
    [Header("Dial")]
    public HardnessDial hardnessDial;

    [Header("Visuals")]
    public SpriteRenderer indicator;
    public Color scratchColor = Color.green;
    public Color noScratchColor = Color.red;
    public Color neutralColor = Color.white;

    private Mineral currentMineral;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Mineral m = other.GetComponent<Mineral>();
        if (m)
        {
            currentMineral = m;
            Evaluate();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Mineral m = other.GetComponent<Mineral>();
        if (m == currentMineral)
        {
            currentMineral = null;
            indicator.color = neutralColor;
        }
    }

    private void Update()
    {
        if (currentMineral)
        {
            Evaluate();
        }
    }
    private void Evaluate()
    {
        float mineralHardness = currentMineral.mineralValues.hardness;
        int toolHardness = hardnessDial.GetHardnessValue();

        bool scratches = toolHardness > mineralHardness;

        indicator.color = scratches ? scratchColor : noScratchColor;
    }
}
