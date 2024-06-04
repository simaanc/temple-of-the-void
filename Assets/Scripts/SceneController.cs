using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Save player's position with scene name
            Vector2 playerPosition = other.transform.position;
            string currentSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetFloat(currentSceneName + "_PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat(currentSceneName + "_PlayerY", playerPosition.y);
            PlayerPrefs.Save();

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}