using System;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnTransform;
    [SerializeField] private float _defaultFireRate = 0;

    private float _fireTimer = 0;
    private bool _canFire = true;

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
        InGamePanel.FingerDown += OnFingerDown;
        InGamePanel.FingerUp += OnFingerUp;
    }

    private void UnSubscribeEvents()
    {
        InGamePanel.FingerDown -= OnFingerDown;
        InGamePanel.FingerUp -= OnFingerUp;
    }

    private void OnFingerUp()
    {
        _canFire = true;
    }

    private void OnFingerDown()
    {
        _canFire = false;
    }

    //firing in a fire rate
    private void Update()
    {
        if (!_canFire) return;

        _fireTimer += Time.deltaTime;
        if (_fireTimer >= _defaultFireRate - PlayerData.Instance.SpeedUpgradeLevel * 0.01f)
        {
            _fireTimer = 0;
            Fire();
        }
    }

    //getting bullets from pool to fire
    private void Fire()
    {
        var bullet = BulletPool.Instance.Get();
        SetBullet(bullet);
    }

    //resetting bullet
    private void SetBullet(BulletBehaviour bullet)
    {
        bullet.transform.rotation = _bulletSpawnTransform.rotation;
        bullet.transform.position = _bulletSpawnTransform.position;
        bullet.gameObject.SetActive(true);
    }
}
