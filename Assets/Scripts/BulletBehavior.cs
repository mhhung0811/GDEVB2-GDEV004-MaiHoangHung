using System.Collections;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(float speed, Vector2 direction)
    {
        _rigidbody.linearVelocity = direction * speed;
        StartCoroutine(DestroyCoolDown());
    }

    IEnumerator DestroyCoolDown()
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
