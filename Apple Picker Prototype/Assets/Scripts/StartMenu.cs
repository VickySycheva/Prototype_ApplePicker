using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _highScoreText;

    private void Awake()
    {
        HighScore.score = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = HighScore.score.ToString();
        
        _startButton.onClick.RemoveAllListeners();
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
