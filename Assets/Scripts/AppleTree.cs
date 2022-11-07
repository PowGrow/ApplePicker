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
    private const float OFFSET = 0.5f;
    private float _direction = 1;


    public float Speed
    {
        get { return _speed * _game.CurrentLevel; }
        set { _speed = value * _game.CurrentLevel; }
    }

    private void Moving()
    {
        RandomChangeDirection();
        ChangeDirection();
        transform.position = new Vector2(transform.position.x + _direction * Speed * Time.deltaTime, transform.position.y);
    }

    private void ChangeDirection()
    {
        if (transform.position.x > _edge)
            _direction = -1;

        if (transform.position.x < _edge * -1)
            _direction = 1;
    }

    private void RandomChangeDirection()
    {
        if (UnityEngine.Random.value < _rndChance && transform.position.x < _edge && transform.position.x > -1 * _edge)
            _direction *= -1;
    }

    private void DropApple()
    {
        GameObject apple = Instantiate(_applePrefab);
        apple.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, -2);
    }

    private void TreeRage()
    {
        _spriteRenderer.color = new Color(_spriteRenderer.color.r,
                                  Math.Clamp(_spriteRenderer.color.g - 0.05f, 0, 255),
                                  Math.Clamp(_spriteRenderer.color.b - 0.05f, 0, 255));
    }
    private void Awake()
    {
        _edge = Camera.main.orthographicSize * Screen.width / Screen.height - OFFSET;
        _spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        Messenger.AddListener(GameEvent.NEXT_LEVEL, TreeRage);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.NEXT_LEVEL, TreeRage);
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
}
