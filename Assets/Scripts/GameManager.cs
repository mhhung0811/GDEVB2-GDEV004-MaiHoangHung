using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] public Collider2D WallTop;
    [SerializeField] public Collider2D WallBottom;
    [SerializeField] public Collider2D WallLeft;
    [SerializeField] public Collider2D WallRight;
    
    private bool _isGameOver;
    void Start()
    {
        _isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void GameOver()
    {
        if (_isGameOver) return;
        _isGameOver = true;
        Debug.Log("Game Over");
    }
}
