using UnityEngine;

public class ApplyEnvironment : MonoBehaviour
{
    // The Awake function is called as soon as the object is loaded,
    // before the first frame. It's perfect for setup tasks.
    void Awake()
    {
        // Check if a material was actually selected and passed from the previous scene.
        if (EnvironmentData.selectedSkyboxMaterial != null)
        {
            // Apply the saved material to the new scene's RenderSettings.
            RenderSettings.skybox = EnvironmentData.selectedSkyboxMaterial;

            // Update the scene's lighting to match the new skybox.
            DynamicGI.UpdateEnvironment();

            Debug.Log("Applied skybox: " + EnvironmentData.selectedSkyboxMaterial.name);
        }
        else
        {
            Debug.LogWarning("No environment data was passed to this scene. Using default skybox.");
        }
    }
}