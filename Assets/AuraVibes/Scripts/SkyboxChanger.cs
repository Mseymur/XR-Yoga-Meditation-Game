using UnityEngine;
using UnityEngine.SceneManagement; // REQUIRED for loading scenes!

public class SkyboxChanger : MonoBehaviour
{
    [Tooltip("Assign your Skybox materials here in the order you want them.")]
    public Material[] skyboxMaterials;

    [Tooltip("The name of the scene to load after selection.")]
    public string sceneToLoad = "Yoga_Main";

    // This is the public function that the buttons will call.
    public void SelectAndLoadScene(int index)
    {
        // Check if the index is valid.
        if (index >= 0 && index < skyboxMaterials.Length)
        {
            // 1. Save the chosen material to our static class.
            EnvironmentData.selectedSkyboxMaterial = skyboxMaterials[index];
            Debug.Log(skyboxMaterials[index].name + " was selected. Loading next scene.");

            // 2. Load the main yoga scene.
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Invalid skybox index: " + index);
        }
    }
}