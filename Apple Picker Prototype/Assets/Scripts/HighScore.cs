using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    static public int score = 1000;

    void Awake()
    {
        if(PlayerPrefs.HasKey("HighScore")){
            score = PlayerPrefs.GetInt("HighScore");
        }

        PlayerPrefs.SetInt("HighScore", score);
    }

    void Update()
    {
        TMP_Text gt = this.GetComponent<TMP_Text>();
        gt.text = "High Score: " + score;
        if(score>PlayerPrefs.GetInt("HighScore")){
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
