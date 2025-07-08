using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager is a singleton class that manages the overall game state,
/// such as handling game over events, restarting the game, and pausing the game.
/// Attach this script to an empty GameObject in your scene and set it up as needed.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Static instance to allow easy access from other scripts (Singleton pattern)
    public static GameManager Instance { get; private set; }

    // Tracks whether the game is currently over
    private bool isGameOver = false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// This method ensures there is only one GameManager in the scene.
    /// </summary>
    private void Awake()
    {
        // If an instance already exists and it's not this, destroy this object
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the singleton instance
        Instance = this;

        // Optional: Persist this object across scene loads
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called to trigger the game over state.
    /// This method can be called from other scripts (e.g., ChaserController).
    /// </summary>
    /// <param name="reason">A string explaining why the game ended.</param>
    public void GameOver(string reason)
    {
        // If the game is already over, do nothing
        if (isGameOver)
            return;

        isGameOver = true;

        // Log the reason for game over to the Unity Console
        Debug.Log("Game Over: " + reason);

        // Pause the game by setting time scale to 0 (stops all movement and physics)
        Time.timeScale = 0f;

        // Show a simple message on the screen (for demonstration)
        // In a real game, you would show a UI panel here
        // For now, we just log to the console

        // Optionally, you can implement a UI popup or restart button here
    }

    /// <summary>
    /// Restarts the current scene, resetting the game state.
    /// This can be called from a UI button or after a delay.
    /// </summary>
    public void RestartGame()
    {
        // Reset the time scale to normal speed
        Time.timeScale = 1f;

        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Reset game over state
        isGameOver = false;
    }
}