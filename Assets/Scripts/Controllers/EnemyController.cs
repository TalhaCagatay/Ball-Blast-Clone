using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private float _spawnRate = 1.5f;

    private List<EnemyBehaviour> _enemies = new List<EnemyBehaviour>();
    private float _spawnTimer = 0;
    private int _enemyCountOnScreen = 0;
    private int _splitAmount = 2;

    private void Awake()
    {
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void SubscribeEvents()
    {
        EnemyBehaviour.Die += OnDie;
        EnemyBehaviour.Split += OnSplit;
        GameController.ResetGame += OnResetGame;
    }

    private void UnSubscribeEvents()
    {
        EnemyBehaviour.Die -= OnDie;
        EnemyBehaviour.Split -= OnSplit;
        GameController.ResetGame -= OnResetGame;
    }

    private void OnResetGame()
    {
        _enemyCountOnScreen = 0;
        _spawnTimer = 0;

        foreach (var enemy in _enemies)
        {
            EnemyPool.Instance.ReturnToPool(enemy);
        }

        _enemies.Clear();
    }

    private void OnSplit(int size,int health, EnemyBehaviour enemy)
    {
        var direction = -1;
        _enemyCountOnScreen--;
        _enemies.Remove(enemy);
        for (int i = 0; i < _splitAmount; i++)
        {
            _enemyCountOnScreen++;
            Spawn(size, health, enemy, direction);
            direction = 1;
        }
    }

    private void OnDie(int health, EnemyBehaviour enemyTransform)
    {
        _enemies.Remove(enemyTransform);
        _enemyCountOnScreen--;
    }

    private void Update()
    {
        _spawnTimer += Time.deltaTime;
        if(_spawnTimer >= _spawnRate && _enemyCountOnScreen < 2)
        {
            _enemyCountOnScreen++;
            _spawnTimer = 0;
            Spawn(Random.Range(5, 15));
        }
    }

    //spawning naturally
    private void Spawn(int health)
    {
        var enemy = EnemyPool.Instance.Get();
        SetEnemy(enemy, health);
        _enemies.Add(enemy);
    }

    //spawning after split
    private void Spawn(int size, int health, EnemyBehaviour enemyBehav, int direction)
    {
        var enemy = EnemyPool.Instance.Get();
        SetEnemy(enemy, enemyBehav.transform, size, health, direction);
        _enemies.Add(enemy);
    }

    //setting enemies after spawning naturally
    private void SetEnemy(EnemyBehaviour enemy, int health)
    {
        enemy.transform.position = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length)].position;
        enemy.transform.rotation = _enemySpawnPoints[Random.Range(0, _enemySpawnPoints.Length)].rotation;
        enemy.SetHealth(health);
        enemy.UpdateEnemyHealth(health);
        enemy.SetSize(Random.Range(1, 4));
        enemy.gameObject.SetActive(true);
        enemy.StartMovingToScreen();
    }

    //setting enemies after split
    private void SetEnemy(EnemyBehaviour enemy, Transform transform, int size, int health, int direction)
    {
        enemy.transform.position = transform.position;
        enemy.transform.rotation = transform.rotation;
        enemy.gameObject.SetActive(true);
        enemy.SetSize(size);
        enemy.SetHealth(health);
        enemy.UpdateEnemyHealth(health);

        var vel = new Vector2(direction == -1 ? Random.Range(-2, -4) : Random.Range(2, 4), Random.Range(2, 5));
        enemy.EnableEnemy(vel);
    }
}
