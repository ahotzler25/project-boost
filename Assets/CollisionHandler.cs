using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    private void OnCollisionEnter(Collision other) {
        switch (other.gameObject.tag) {
            case "Ground":
                StartCrashSequence();
                break;
            case "Obstacle":
            // Invoke allows us to delay a method by X seconds (remember to remove the parentheses)
                StartCrashSequence();
                break;
            case "Fuel":
                Debug.Log("You fueled up!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                Invoke("ReloadScene", 2f);
                break;
        }
    }
    private void StartSuccessSequence() {
        /* TODO add SFX upon success
        // TODO add particle effect upon success
        */
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartCrashSequence() {
        /* TODO add SFX upon crash
        // TODO add particle effect upon crash
        */
        GetComponent<Movement>().enabled = false;
        GetComponent<AudioSource>().enabled = false;
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            Debug.Log("Congrats! You won!");
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadScene() {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
}
