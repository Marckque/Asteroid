using UnityEngine;

public class BigAsteroid : Asteroid
{
    protected override void Awake()
    {
        base.Awake();

        InitialiseVelocity();
    }

    protected override void InitialiseVelocity()
    {
        base.InitialiseVelocity();

        EntityParameters.accelerationScalar *= 1 - (Random.Range(0f, m_AccelerationMultiplier));
    }

    protected override void OnCollisionWithBullet(Vector3 direction)
    {
        base.OnCollisionWithBullet(direction);

        for (int i = 0; i < 2; i++)
        {
            GameManagement.Instance.SpawnAsteroid(AsteroidType.small, transform.position, direction + ExtensionMethods.RandomVector());
        }

        Destroy(gameObject);
    }
}