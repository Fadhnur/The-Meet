using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    // List of renderers to apply outline effect
    [SerializeField]
    private List<Renderer> renderers;

    // Outline color
    [SerializeField]
    private Color outlineColor = Color.white;

    // Outline width
    [SerializeField]
    private float outlineWidth = 0.03f;

    // List to cache all materials of this object
    private List<Material> materials;

    // Gets all the materials from each renderer
    private void Awake()
    {
        materials = new List<Material>();
        foreach (var renderer in renderers)
        {
            materials.AddRange(renderer.materials);
        }
    }

    // Toggles the outline effect
    public void ToggleOutline(bool val)
    {
        if (val)
        {
            foreach (var material in materials)
            {
                // Check if material already has an outline shader
                if (!material.shader.name.Contains("Outlined"))
                {
                    // Set outline shader
                    material.shader = Shader.Find("Outlined/Silhouetted Diffuse");
                }

                // Set outline color
                material.SetColor("_OutlineColor", outlineColor);

                // Set outline width
                material.SetFloat("_Outline", outlineWidth);
            }
        }
        else
        {
            foreach (var material in materials)
            {
                // Reset to default shader
                material.shader = Shader.Find("Standard");
            }
        }
    }
}
