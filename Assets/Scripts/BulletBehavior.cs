using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private int _bounces = 2;
    [SerializeField] private int _damage = 1;
    private Vector2 _direction;
    private float _speed;
    private float _minX, _maxX, _minY, _maxY;

    public void SetUp(float speed, Vector2 direction)
    {
        _speed = speed;
        _direction = direction.normalized;

        var gm = GameManager.Instance;
        _minX = gm.WallLeft.bounds.max.x;
        _maxX = gm.WallRight.bounds.min.x;
        _minY = gm.WallBottom.bounds.max.y;
        _maxY = gm.WallTop.bounds.min.y;

        StartCoroutine(DestroyCoolDown());
    }

    void Update()
    {
        Vector2 nextPos = (Vector2)transform.position + _direction * _speed * Time.deltaTime;

        if (nextPos.x <= _minX || nextPos.x >= _maxX)
        {
            if (_bounces <= 0) { Destroy(gameObject); return; }
            _bounces--;
            _direction.x = -_direction.x;
            nextPos.x = Mathf.Clamp(nextPos.x, _minX, _maxX);
        }

        if (nextPos.y <= _minY || nextPos.y >= _maxY)
        {
            if (_bounces <= 0) { Destroy(gameObject); return; }
            _bounces--;
            _direction.y = -_direction.y;
            nextPos.y = Mathf.Clamp(nextPos.y, _minY, _maxY);
        }

        transform.position = nextPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyCoolDown()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
}
