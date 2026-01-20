using UnityEngine;

/// AI generated quit game script

public class QuitManager : MonoBehaviour
{
    /// <summary>
    /// Closes the application. 
    /// Works in the final build and the Unity Editor.
    /// </summary>
    public void QuitGame()
    {
        // Logs the action so you can see it working in the Console
        Debug.Log("Quit Game requested.");

        // If running in the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // If running as a standalone build (.exe, .app)
            Application.Quit();
        #endif
    }
}