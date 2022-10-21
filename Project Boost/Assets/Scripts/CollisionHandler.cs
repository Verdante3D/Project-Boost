using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayDuration = 1f;
    [SerializeField] AudioClip successAudio;
    [SerializeField] AudioClip crashAudio;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Touching safe ground mate!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartSuccessSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(successAudio);

        successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayDuration);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(crashAudio);

        crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayDuration);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
