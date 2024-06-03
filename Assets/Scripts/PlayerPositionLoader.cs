using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionLoader : MonoBehaviour
{
    private void Start()
    {
        // Get current scene name
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Check if the player's position has been saved for this scene
        if (PlayerPrefs.HasKey(currentSceneName + "_PlayerX") && PlayerPrefs.HasKey(currentSceneName + "_PlayerY"))
        {
            float x = PlayerPrefs.GetFloat(currentSceneName + "_PlayerX");
            float y = PlayerPrefs.GetFloat(currentSceneName + "_PlayerY");

            // Apply the saved offset
            if (PlayerPrefs.HasKey(currentSceneName + "_OffsetX"))
            {
                float offsetX = PlayerPrefs.GetFloat(currentSceneName + "_OffsetX");
                x += offsetX;
            }

            transform.position = new Vector2(x, y);
        }
    }
}