using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    public AudioClip levelExitSFX;
    private bool exit = false;
    SoundManager SoundManager;

    void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SoundManager.PlaySound(levelExitSFX, 0.6f);
        StartCoroutine(LoadNextLevel());
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
