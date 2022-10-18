using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touching safe ground mate!");
                break;
            case "Finish":
                Debug.Log("Made it to the end mate!");
                break;
            case "Fuel":
                Debug.Log("Right on! You filled up mate!");
                break;
            default:
                Debug.Log("Hit the dirt mate!");
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
