using UnityEngine;

/// <summary>
/// This class is only for getting and setting player datas
/// </summary>
public class PlayerData : MonoBehaviour
{
    private int _highScore;
    private float _damage;
    private float _coinEarning;
    private float _offlineEarning;
    private float _playerMovementSpeed;
    private float _bulletMovementSpeed;
    private int _speedUpgradeLevel;

    public int HighScore
    {
        get{ return _highScore = PlayerPrefs.GetInt("highScore", 0); }
        set { PlayerPrefs.SetInt("highScore", value); }
    }

    public float Damage
    {
        get { return _damage = PlayerPrefs.GetFloat("damage", 1.0f); }
        set { PlayerPrefs.SetFloat("damage", value); }
    }

    public float CoinEarning
    {
        get { return _coinEarning = PlayerPrefs.GetFloat("coinEarning", 1.0f); }
        set { PlayerPrefs.SetFloat("coinEarning", value); }
    }

    public float OfflineEarning
    {
        get { return _offlineEarning = PlayerPrefs.GetFloat("offlineEarning", 1.0f); }
        set { PlayerPrefs.SetFloat("offlineEarning", value); }
    }

    public float PlayerMovementSpeed
    {
        get { return _playerMovementSpeed = PlayerPrefs.GetFloat("playerMovementSpeed", 60); }
        set { PlayerPrefs.SetFloat("playerMovementSpeed", value); }
    }

    public float BulletMovementSpeed
    {
        get { return _bulletMovementSpeed = PlayerPrefs.GetFloat("bulletMovementSpeed", 15); }
        set { PlayerPrefs.SetFloat("bulletMovementSpeed", value); }
    }

    public int SpeedUpgradeLevel
    {
        get { return _speedUpgradeLevel = PlayerPrefs.GetInt("speedUpgradeLevel", 1); }
        set { PlayerPrefs.SetInt("speedUpgradeLevel", value); }
    }

    //private void Start()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

    #region Singleton
    public static PlayerData Instance = null;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);

        DontDestroyOnLoad(this);
    }
#endregion
}
