using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int _hp = 1;
    [SerializeField] private int _damage = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void TakeDamage(int damage)
    {
        _hp -= damage;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(_damage);
        }
    }
}
