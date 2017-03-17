using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameParameters
{
    [Header("Asteroids")]
    public BigAsteroid bigAsteroid;
    public SmallAsteroid smallAsteroid;
    public int maximumAsteroids = 1;
    public int numberOfAsteroidsToSpawn = 3;
    public float delayBetweenSpawn = 2f;
    public int minimumAsteroidSize = 1;
    public int maximumAsteroidSize = 3;
}

public class GameManagement : MonoBehaviour
{
    private static GameManagement m_Instance;
    public static GameManagement Instance { get { return m_Instance; } }

    [SerializeField]
    private GameParameters m_GameParameters;

    private int m_NumberOfSpawnedAsteroids;
    private List<Asteroid> m_Asteroids = new List<Asteroid>();

    protected void Awake()
    {
        InitialiseGameManager();
        StartCoroutine(PeriodicAsteroidSpawn());
    }

    private void InitialiseGameManager()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }

        if (m_Instance == null || m_Instance != this)
        {
            Debug.LogError("Error with the initialisation of the GameManagement");
        }
    }

    private IEnumerator PeriodicAsteroidSpawn()
    {
        while (m_NumberOfSpawnedAsteroids < m_GameParameters.numberOfAsteroidsToSpawn)
        {
            if (m_Asteroids.Count < m_GameParameters.maximumAsteroids)
            {
                SpawnAsteroid(AsteroidType.big);
            }

            yield return new WaitForSeconds(m_GameParameters.delayBetweenSpawn);
        }
    }

    public void SpawnAsteroid(AsteroidType type)
    {
        Asteroid asteroidToSpawn = null;

        switch (type)
        {
            case AsteroidType.small:
                asteroidToSpawn = m_GameParameters.smallAsteroid;
                break;
            case AsteroidType.big:
                asteroidToSpawn = m_GameParameters.bigAsteroid;
                break;
            default:
                break;
        }

        Asteroid asteroid = Instantiate(asteroidToSpawn, transform.position, Quaternion.identity);
        asteroid.SetAcceleration(ExtensionMethods.RandomVector() * asteroid.EntityParameters.accelerationScalar);
        asteroid.ApplyForces();

        m_Asteroids.Add(asteroid);
        m_NumberOfSpawnedAsteroids++;
    }
}