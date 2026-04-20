using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
