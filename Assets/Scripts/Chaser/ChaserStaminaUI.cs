using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This MonoBehaviour script should be attached to a UI GameObject with a Slider component.
/// It updates the Slider to reflect the chaser's current stamina in real time.
/// </summary>
public class ChaserStaminaUI : MonoBehaviour
{
    [Tooltip("Reference to the ChaserController whose stamina will be displayed. " +
             "Assign the Chaser GameObject from your scene here.")]
    public ChaserController chaserController;

    [Tooltip("Reference to the UI Slider that displays the chaser's stamina. " +
             "Assign your UI Slider GameObject here.")]
    public Slider staminaSlider;

    /// <summary>
    /// Unity's Start method is called before the first frame update.
    /// Sets up the slider's max value to match the chaser's max stamina.
    /// </summary>
    private void Start()
    {
        // Check if references are assigned in the Inspector
        if (chaserController == null)
        {
            Debug.LogWarning("[ChaserStaminaUI] ChaserController reference is not set in the Inspector.");
            return;
        }
        if (staminaSlider == null)
        {
            Debug.LogWarning("[ChaserStaminaUI] Stamina Slider reference is not set in the Inspector.");
            return;
        }

        // Set the slider's maximum value to the chaser's maximum stamina
        staminaSlider.maxValue = chaserController.maxStamina;
        // Set the slider's initial value to the chaser's current stamina
        staminaSlider.value = chaserController.GetCurrentStamina();
    }

    /// <summary>
    /// Unity's Update method is called once per frame.
    /// Updates the slider value to match the chaser's current stamina.
    /// </summary>
    private void Update()
    {
        // Only update if references are valid
        if (chaserController != null && staminaSlider != null)
        {
            // Update the slider to reflect the chaser's current stamina
            staminaSlider.value = chaserController.GetCurrentStamina();
        }
    }
}