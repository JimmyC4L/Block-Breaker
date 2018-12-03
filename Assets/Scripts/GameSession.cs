using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f)] [FormerlySerializedAs("gameSpeed")] [SerializeField]
    private float _gameSpeed;

    [FormerlySerializedAs("pointsPerBlockDestroyed")] [SerializeField]
    private int _pointsPerBlockDestroyed = 83;

    [FormerlySerializedAs("scoreText")] [SerializeField]
    private TextMeshProUGUI _scoreText;

    // state
    [FormerlySerializedAs("autoPlay")] [SerializeField]
    private bool _autoPlayEnabled;

    private void Awake()
    {
        var gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {    
            gameObject.SetActive(false);
            DestroyItself();
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        _scoreText.text = _currentScore.ToString();
    }

    // state variables
    [FormerlySerializedAs("currentScore")] [SerializeField]
    private int _currentScore = 0;

    private void Update()
    {
        Time.timeScale = _gameSpeed;
    }

    public void AddToScore()
    {
        _currentScore += _pointsPerBlockDestroyed;
        SetScoreText();
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return _autoPlayEnabled;
    }
}