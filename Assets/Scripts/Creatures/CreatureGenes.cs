using UnityEngine;

public class CreatureGenes : MonoBehaviour
{
    [Header("Heritable Visual Traits")]
    public Color bodyColor = Color.white;
    public Vector3 bodyScale = Vector3.one;

    [Header("Heritable Gameplay Traits")]
    public float speedBonus = 0f;

    private void Start()
    {
        ApplyVisuals();
    }

    public void ApplyVisuals()
    {
        // 1. Apply inherited scale transform
        transform.localScale = bodyScale;

        // 2. Apply inherited color to mesh renderer
        Renderer meshRenderer = GetComponentInChildren<Renderer>();
        if (meshRenderer != null)
        {
            // Instantiates a material instance so changes don't affect shared project assets
            meshRenderer.material.color = bodyColor;
        }
    }
}
