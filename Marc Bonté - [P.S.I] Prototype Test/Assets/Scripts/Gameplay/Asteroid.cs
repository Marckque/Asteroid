using UnityEngine;

public enum AsteroidType
{
    small = 0,
    big = 1
};

public class Asteroid : Entity
{
    [SerializeField, Range(0f, 1f)]
    protected float m_AccelerationMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected void OnTriggerEnter(Collider other)
    {
        Bullet bullet = other.GetComponent<Bullet>();

        if (bullet)
        {
            Destroy(bullet.gameObject);
            OnCollisionWithBullet();
        }
    }

    protected virtual void InitialiseVelocity()
    {
        // Use in child
    }

    protected virtual void OnCollisionWithBullet()
    {
        // Use in child
    }
}