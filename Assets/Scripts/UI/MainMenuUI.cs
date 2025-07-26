using UnityEngine;
using UnityEngine.SceneManagement; // Importing the SceneManagement namespace to manage scenes

// This class handles the functionality of the main menu UI.
// Attach this script to a GameObject in your MainMenu scene (commonly an empty GameObject named "MainMenuUI").
// Then, assign the OnStartButtonClicked and OnExitButtonClicked methods to the respective button OnClick() events in the Unity Inspector.
public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update by Unity.
    // You can use this method to initialize UI elements or settings if needed.
    void Start()
    {
        // Optionally, you can initialize UI elements or settings here.
    }

    // This method is called when the Start button is clicked.
    // It loads the main game scene.
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    // This method is called when the Exit button is clicked.
    // It quits the application if built, or stops play mode if running in the Unity Editor.
    public void OnExitButtonClicked()
    {
        Application.Quit(); // Quits the application (only works in a built executable).

        // The following code ensures that if you are running the game in the Unity Editor,
        // pressing Exit will stop play mode instead of quitting the editor itself.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
