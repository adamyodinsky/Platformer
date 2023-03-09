using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    
    [SerializeField] int playersLives = 10;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playersLives.ToString();
        scoresText.text = score.ToString();
    }

    public void AddScore(int points)
    {
        score = score + points;
        scoresText.text = score.ToString();
    }

   public void ProcessPlayerDeath()
   {
        if(playersLives > 1) {
            TakeLife();
        } else {
            ResetGameSession();
        }
   }

   public void TakeLife()
   {
       playersLives--;
       int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       SceneManager.LoadScene(currentSceneIndex);
       livesText.text = playersLives.ToString();
   }

   public void ResetGameSession()
   {
       SceneManager.LoadScene(0);
       Destroy(gameObject);
   }
}
