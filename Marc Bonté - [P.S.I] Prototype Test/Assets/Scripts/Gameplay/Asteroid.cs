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
            OnCollisionWithBullet(bullet.EntityRigidbody.velocity.normalized);
            Destroy(bullet.gameObject);

            GameManagement.Instance.RemoveAsteroidFromList(this);
        }
    }

    protected virtual void InitialiseVelocity()
    {
        // Use in child
    }

    protected virtual void OnCollisionWithBullet(Vector3 direction)
    {
        // Use in child
    }
}