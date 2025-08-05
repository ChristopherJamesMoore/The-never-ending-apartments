using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int score = 0;
    public TMP_Text counterText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore()
    {
        score++;
        counterText.text = "Score: " + score;
    }

    public void RemoveScore()
    {
        score = Mathf.Max(0, score - 1); // prevents negative score
        counterText.text = "Score: " + score;
    }

}
