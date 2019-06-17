using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemy
{
    public static event Action<int, EnemyBehaviour> Die;
    public static event Action<int, int, EnemyBehaviour> Split;

    [SerializeField] private EnemyHealthTextBehaviour _enemyHealthTextBehaviour;

    private string _moveCoroutineString = "MoveToScreenCoroutine";
    private Rigidbody2D _rBody;
    private bool _enemyEnabled = false;
    private int _health;
    private int _size;
    private int _direction = 0;
    private int _initialHealth;

    public float Speed
    {
        get
        {
            return 3;
        }            
        
        set { }
    }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
        }
    }

    public int Size
    {
        get
        {
            return _size;
        }

        set
        {
            _size = value;
        }
    }

    public void StartMovingToScreen()
    {
        //we are setting a direction so we can give proper velocity based on direction
        if (transform.position.x < 0)
        {
            _direction = -1;
        }
        else if (transform.position.x > 0)
        {
            _direction = 1;
        }

        StartCoroutine(_moveCoroutineString);
    }

    public void UpdateEnemyHealth(int health)
    {
        _enemyHealthTextBehaviour.UpdateHealth(Health);
    }

    private void OnEnable()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _initialHealth = Health;
    }

    private void OnDisable()
    {
        ResetEnemy();
    }

    private void ResetEnemy()
    {
        StopCoroutine(_moveCoroutineString);
        _enemyEnabled = false;
        _rBody.isKinematic = true;
    }

    public void SetSize(int size)
    {
        Size = size;
    }

    public void SetHealth(int health)
    {
        Health = health;
        _initialHealth = Health;
    }

    public void OnHit(float damage)
    {
        Health -= (int)damage;
        UpdateEnemyHealth(Health);

        if (Health <= 0)
        {
            if(Size == 1)
            {
                if (Die != null)
                {
                    Die.Invoke(_initialHealth, this);
                }
            }
            else
            {
                Size--;
                if(Split != null)
                {
                    Split.Invoke(Size, (int)Mathf.Ceil(_initialHealth / 2), this);
                }
            }

            EnemyPool.Instance.ReturnToPool(this);
        }
    }

    // since we spawn objects out off screen, we are moving them towards to screen
    private IEnumerator MoveToScreenCoroutine()
    {
        var visible = false;

        while(!visible)
        {
            var pos = Camera.main.WorldToViewportPoint(transform.position);
            Move();

            // checking if object is visible in screen
            if(pos.x > 0 && pos.x < 1)
            {
                StopCoroutine(_moveCoroutineString);
                visible = true;
                _enemyEnabled = true;

                var velX = _direction == -1 ? UnityEngine.Random.Range(2, 4) : UnityEngine.Random.Range(-2, -4);
                EnableEnemy(new Vector2(velX, 0));
            }

            yield return null;
        }
    }

    public void EnableEnemy(Vector2 vel)
    {
        _rBody.isKinematic = false;
        _rBody.gravityScale = 0.5f;
        _rBody.mass = 1;

        AddVelocity(vel);
    }

    public void AddVelocity(Vector2 vel)
    {
        _rBody.velocity = vel;
    }

    public void Move()
    {
        if(transform.position.x < 0)
        {
            MoveRight();
        }
        else if (transform.position.x > 0)
        {
            MoveLeft();
        }
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            var damage = (int)collision.GetComponent<IBullet>().Damage;
            OnHit(damage);
        }
    }
}
