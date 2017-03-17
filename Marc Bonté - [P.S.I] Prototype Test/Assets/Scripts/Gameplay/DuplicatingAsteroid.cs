using UnityEngine;

public class DuplicatingAsteroid : Asteroid
{
    [SerializeField]
    private AsteroidType m_AsteroidToSpawn;
    protected const float DOT_OFFSET = -0.8f;

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

        switch (AsteroidType)
        {
            case AsteroidType.medium:

                break;
            case AsteroidType.big

                break;
        }

        for (int i = 0; i < 2; i++)
        {
            Vector3 randomDirection = ExtensionMethods.RandomVector();
            /*
            float dot = Vector3.Dot(randomDirection, direction);

            if (dot < DOT_OFFSET)
            {
                randomDirection = Vector3.zero;
            }
            */

            GameManagement.Instance.SpawnAsteroid(m_AsteroidToSpawn, transform.position, randomDirection);
        }

        Destroy(gameObject);
    }
}