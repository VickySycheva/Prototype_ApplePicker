using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplePicker : MonoBehaviour
{
    public float LevelUP { get; private set; }

    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    
    [SerializeField] private Basket basketPrefab;
    [SerializeField] private AppleTree appleTreePrefab;
    [SerializeField] private int numBaskets = 3;
    [SerializeField] private float basketBottomY = -14f;
    [SerializeField] private float basketSpacingY = 2f;
    
    [Header("End Screen"), Space]
    [SerializeField] private Canvas endScreen;
    [SerializeField] private Button buttonEnd;
    [SerializeField] private Button buttonRestart;

    private List<Basket> basketList;
    private AppleTree appleTree;

    private int _highScore;
    private int _score;

    void Start()
    {
        StartGame();
    }

    void StartGame ()
    {
        LevelUP = 1;
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _score = 0;

        UpdateScoreUI();

        InstBasket();
        appleTree = Instantiate(appleTreePrefab);
        appleTree.Init(this, AppleDestroyed);
    }

    private void AddScore(int value)
    {
        _score += value;
        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
        }
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        _highScoreText.text = _highScore.ToString();
        _scoreText.text = _score.ToString();
    }

    void InstBasket()
    {
        basketList = new List<Basket>();
        for(int i = 0; i < numBaskets; i++)
        {
            Basket tBasketGO = Instantiate(basketPrefab);
            tBasketGO.Init(AddScore);
            Vector3 pos = Vector3.zero;
            pos.y = basketBottomY+(basketSpacingY*i);
            tBasketGO.transform.position = pos;
            basketList.Add(tBasketGO);
        }
    }

    private void AppleDestroyed()
    {
        List<Apple> apples = appleTree.GetApples();
        for (int i = 0; i < apples.Count; i++)
        {
            if (apples[i] == null) continue;

            apples[i].NeedToInvokeOnDestroy = false;
            Destroy(apples[i].gameObject);
        }

        int basketIndex = basketList.Count - 1;
        Basket tBasketGO = basketList[basketIndex];
        basketList.RemoveAt(basketIndex);
        Destroy(tBasketGO.gameObject);
        LevelUP *= 2;

        if(basketList.Count == 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Destroy(appleTree.gameObject);
        endScreen.gameObject.SetActive(true);
        buttonEnd.onClick.RemoveAllListeners();
        buttonEnd.onClick.AddListener(EndButton);
        buttonRestart.onClick.RemoveAllListeners();
        buttonRestart.onClick.AddListener(RestartGame);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void EndButton()
    {
        SceneManager.LoadScene("StartScene");
    }
}
