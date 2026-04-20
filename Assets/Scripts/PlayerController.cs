using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int _hp = 3;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 100f;
    [SerializeField] private float _fireRate = 0.5f;
    
    private bool _isMoving;
    private Vector3 _destination;
    private float _nextFireTime;
    private Vector2 _oldDirection;
    void Start()
    {
        if (Mouse.current != null && !Mouse.current.enabled)
        {
            UnityEngine.InputSystem.InputSystem.EnableDevice(Mouse.current);
        }
        if (Keyboard.current != null && !Keyboard.current.enabled)
        {
            UnityEngine.InputSystem.InputSystem.EnableDevice(Keyboard.current);
        }

        _nextFireTime = 0;
        _oldDirection = Vector2.right;
    }
    
    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.isPressed)
        {
            _isMoving = true;
            _destination = Camera.main.ScreenToWorldPoint(mousePos);
            _oldDirection = (_destination - transform.position).normalized;
        }
        
        if (Keyboard.current.spaceKey.isPressed && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;
            var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletBehavior>().SetUp(_bulletSpeed, _oldDirection);
        }

        if (_hp <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _destination, Time.deltaTime * _speed);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame || transform.position == _destination)
        {
            _isMoving = false;
        }
    }
    
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
}
