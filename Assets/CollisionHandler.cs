using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip levelComplete;
    [SerializeField] AudioClip playerExplosion;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        RespondToHackerKeys();
    }

    private void RespondToHackerKeys() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            collisionDisabled = !collisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (isTransitioning || collisionDisabled) {
            return;
        }

        switch (other.gameObject.tag) {
            case "Friendly":
                Debug.Log("This is a friendly platform.");
                break;
            case "Fuel":
                Debug.Log("You fueled up!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    private void StartSuccessSequence() {
        
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartCrashSequence() {
        isTransitioning = true;
        audioSource.Stop();
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(playerExplosion);
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
