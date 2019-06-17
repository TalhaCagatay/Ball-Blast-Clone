using UnityEngine;

/// <summary>
/// this class is for Vector2.Reflect to cheat ball bouncing physics
/// </summary>
public class EnemyBounceBehaviour : MonoBehaviour
{
    [Tooltip("Just for debugging, adds some velocity during OnEnable")]
    private Vector2 _initialVelocity;

    [SerializeField]
    private float _minVelocity = 10f;

    private Vector2 _lastFrameVelocity;
    private Rigidbody2D _rb;

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();

        _initialVelocity = new Vector2(Random.Range(-5, 5), -2);
        _rb.velocity = _initialVelocity;
    }

    private void Update()
    {
        _lastFrameVelocity = _rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var speed = _lastFrameVelocity.magnitude;
        var direction = Vector2.Reflect(_lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        _rb.velocity = direction * Mathf.Max(speed, _minVelocity);
    }
}
