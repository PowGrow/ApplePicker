using System;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [SerializeField] private GameObject _applePrefab;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rndChance = 0.1f;
    [SerializeField] private float _dropInterval = 2f;
    [SerializeField] private Game _game;

    private float _edge;
    private float _timer = 0f;
    private SpriteRenderer _spriteRenderer;


    public float Speed
    {
        get { return _speed * _game.CurrentLevel; }
        set { _speed = value * _game.CurrentLevel; }
    }

    private void Moving()
    {
        transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        if(Math.Abs(transform.position.x) > _edge)
            _speed *= -1;
    }

    private void RandomChangeDirection()
    {
        if (UnityEngine.Random.value < _rndChance)
            _speed *= -1;
    }

    private void DropApple()
    {
        GameObject apple = Instantiate(_applePrefab);
        apple.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, -2);
    }
    private void Awake()
    {
        _edge = Camera.main.orthographicSize * Screen.width / Screen.height;
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _dropInterval / (_game.CurrentLevel / 2))
        {
            DropApple();
            _timer = 0f;
        }  
        Moving();
    }

    private void FixedUpdate()
    {
        RandomChangeDirection();
        _spriteRenderer.color = new Color(Math.Clamp(_spriteRenderer.color.r + _game.CurrentLevel * 2,0,255), 
                                          Math.Clamp(_spriteRenderer.color.g - _game.CurrentLevel * 2,0,255), 
                                          Math.Clamp(_spriteRenderer.color.b - _game.CurrentLevel * 2,0,255));
    }
}
