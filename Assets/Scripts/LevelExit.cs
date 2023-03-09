using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    float levelLoadDelay = 4f;
    [SerializeField] AudioClip levelExitSFX;
    [SerializeField] AudioClip levelExitMusic;
    private bool exit = false;
    SoundManager SoundManager;
    PlayerMovement playerMovement;
    bool isActivated = false;

    void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActivated) {
            isActivated = true;
            SoundManager.PlaySound(levelExitSFX, 0.7f);
            playerMovement.TurnOffBgMusic();
            SoundManager.PlaySound(levelExitMusic, 1f);
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel() {
        if (exit) {
            yield break;
        } else {
            exit = true;
        }

        yield return new WaitForSeconds(levelLoadDelay);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        Debug.Log("Loading next level..." + nextSceneIndex + "From" + currentSceneIndex);

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
