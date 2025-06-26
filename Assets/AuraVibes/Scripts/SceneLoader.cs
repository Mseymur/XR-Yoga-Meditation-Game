using UnityEngine;
using UnityEngine.SceneManagement; // This line is ESSENTIAL for loading scenes!

public class SceneLoader : MonoBehaviour
{
    // This is a public function that our button can see and call.
    // It takes a string variable (the name of the scene) as a parameter.
    public void LoadSceneByName(string sceneName)
    {
        // Check if the scene name is not empty before trying to load.
        if (!string.IsNullOrEmpty(sceneName))
        {
            Debug.Log("Loading scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is empty! Cannot load scene.");
        }
    }
}