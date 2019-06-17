using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour, IBullet
{
    public float Speed
    {
        get
        {
            return PlayerData.Instance.BulletMovementSpeed;
        }

        set
        {
            
        }
    }

    public float Damage
    {
        get
        {
            return PlayerData.Instance.Damage;
        }

        set
        {
            
        }
    }

    //moving bullets
    public void Move()
    {
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        var pos = Camera.main.WorldToViewportPoint(transform.position);
        // bullet is out of screen
        if(pos.y > 1)
        {            
            BulletPool.Instance.ReturnToPool(this);
            ResetBullet();
        }
    }

    private void Update()
    {
        Move();
    }

    private void ResetBullet()
    {
        transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            BulletPool.Instance.ReturnToPool(this);            
        }        
    }
}
