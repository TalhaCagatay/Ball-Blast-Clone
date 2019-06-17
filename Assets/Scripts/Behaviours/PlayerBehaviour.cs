using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour,IMovable
{
    public static event Action PlayerDeath;

    private PlayerInput _playerInput;
    private Rigidbody2D _myBody;

    public float _speed
    {
        get
        {
            return PlayerData.Instance.PlayerMovementSpeed;
        }

        set
        {
            _speed = value;
        }
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _myBody = GetComponent<Rigidbody2D>();

        GameController.ResetGame += OnResetGame;
    }

    private void OnDestroy()
    {
        GameController.ResetGame -= OnResetGame;
    }

    private void Update()
    {
        Move();
    }

    //i have choosed to move the player with physic so i can have smoother movement
    //movement could also be done with transform.Translate()...
    public void Move()
    {
        if (Input.GetMouseButton(0))
        {
            _myBody.velocity = new Vector2(_playerInput.Input.x * _speed, -0);
        }
        else
        {
            _myBody.velocity = Vector2.zero;
        }
    }

    private void OnResetGame()
    {
        ResetPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(PlayerDeath != null)
            {
                PlayerDeath.Invoke();
            }
        }
    }

    //reset player after death
    private void ResetPlayer()
    {
        transform.position = new Vector2(0, transform.position.y);
    }
}