using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager is responsible for handling overall game state, such as game over and restarting the game.
/// Attach this script to an empty GameObject in your scene named "GameManager".
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton instance for easy access from other scripts.
    public static GameManager Instance { get; private set; }

    // Tracks whether the game is currently over.
    private bool isGameOver = false;

    /// <summary>
    /// Unity's Awake method is called when the script instance is being loaded.
    /// This ensures only one GameManager exists in the scene.
    /// </summary>
    private void Awake()
    {
        // If an instance already exists and it's not this, destroy this object.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // Set this as the singleton instance.
        Instance = this;
        // Optional: Persist this object across scene loads.
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Unity's Update method is called once per frame.
    /// Here, we check if the user presses the Delete key to restart the game.
    /// </summary>
    private void Update()
    {
        // Input.GetKeyDown checks if the Delete key was pressed this frame.
        // KeyCode.Delete refers to the Delete key on the keyboard.
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            // Call the RestartGame method to restart the game.
            RestartGame();
        }
    }

    /// <summary>
    /// Called to trigger the game over state.
    /// This method can be called from other scripts (e.g., ChaserController).
    /// </summary>
    /// <param name="reason">A string explaining why the game ended.</param>
    public void GameOver(string reason)
    {
        // If the game is already over, do nothing.
        if (isGameOver)
            return;

        isGameOver = true;

        // Log the reason for game over to the Unity Console.
        Debug.Log("Game Over: " + reason);

        // Pause the game by setting time scale to 0 (stops all movement and physics).
        Time.timeScale = 0f;

        // In a real game, you would show a UI panel here.
        // For now, we just log to the console.
    }

    /// <summary>
    /// Restarts the current scene, resetting the game state.
    /// This can be called from a UI button or after a delay.
    /// </summary>
    public void RestartGame()
    {
        // Reset the time scale to normal speed.
        Time.timeScale = 1f;

        // Reload the current active scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Reset game over state.
        isGameOver = false;
    }
}