interface IEnemy
{
    int Size { get; set; }
    int Health { get; set; }
    float Speed { get; set; }
    void Move();
    void OnHit(float damage);
}