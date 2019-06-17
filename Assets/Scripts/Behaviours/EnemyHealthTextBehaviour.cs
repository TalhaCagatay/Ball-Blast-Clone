using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthTextBehaviour : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;

    public void UpdateHealth(int health)
    {
        _healthText.text = health.ToString();
    }
}
