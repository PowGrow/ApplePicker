using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game : MonoBehaviour
{
    [SerializeField] private GameObject _basketPrefab;
    [SerializeField] private int _basketNumber;
    [SerializeField] private float _basketBottomY = -5.5f;
    [SerializeField] private float _basketSpacingY = 0.5f;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private GameObject _appleTree;
    [SerializeField] private float _currentLevel = 1f;
    [SerializeField] private TextMeshProUGUI _currentLevelLabel;

    private List<GameObject> _baskets;
    private float _currentScore;
    private PlayerControls _playerControls;

    public List<GameObject> Baskets
    {
        get { return _baskets; }
        set { _baskets = value; }
    }

    public float CurrentLevel
    {
        get { return _currentLevel; }
    }

    private void DestoryBasket()
    {
        if (_baskets.Count != 0)
        {
            var basket = _baskets.Last();
            _baskets.Remove(basket);
            Destroy(basket);
        }

        if (_baskets.Count <= 0)
        {
            OnGameOver();
        }
    }

    public void RestatLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    public void OnMainMenuButtonClick()
    {
        SceneManager.LoadScene(0);
        ResumeGame();
    }

    private void OnGameOver()
    {
        PauseGame();
        _gameOverUI.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        _gameUI.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _gameUI.SetActive(false);
    }

    private void ChangeGameStatus()
    {
        if (Time.timeScale == 1)
            PauseGame();
        else
            ResumeGame();
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _gameOverUI.SetActive(false);
        _gameUI.SetActive(false);
        _baskets = new List<GameObject>();
        _currentLevelLabel.text = _currentLevel.ToString();

        _playerControls.GUI.Keyboard.performed += callbackContext => ChangeGameStatus();
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.APPLE_DROPED, DestoryBasket);
        _playerControls.Enable();
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.APPLE_DROPED, DestoryBasket);
        _playerControls.Disable();
    }

    private void Start()
    {
        for(int i = 0; i < _basketNumber; i++)
        {
            var basket = Instantiate(_basketPrefab);
            _baskets.Add(basket);
            basket.transform.position = new Vector2(0,_basketBottomY + _basketSpacingY * i);
        }
    }

    private void FixedUpdate()
    {
        _currentScore = PlayerPrefs.GetInt("score") / 10;

        if(_currentScore == _currentLevel)
        {
            _currentLevel += 1;
            _currentLevelLabel.text = _currentLevel.ToString();
        }
    }
}
