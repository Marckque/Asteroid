using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameParameters
{
    [Header("Asteroids")]
    public Asteroid asteroid;
    public int maximumAsteroids = 1;
    public int numberOfAsteroidsToSpawn = 3;
    public float delayBetweenSpawn = 2f;
}

public class GameManagement : MonoBehaviour
{
    [SerializeField]
    private GameParameters m_GameParameters;

    private int m_NumberOfSpawnedAsteroids;
    private List<Asteroid> m_Asteroids = new List<Asteroid>();

    protected void Start()
    {
        StartCoroutine(SpawnAsteroid());
    }

    private IEnumerator SpawnAsteroid()
    {
        while (m_NumberOfSpawnedAsteroids < m_GameParameters.numberOfAsteroidsToSpawn)
        {
            if (m_Asteroids.Count < m_GameParameters.maximumAsteroids)
            {
                Asteroid asteroid = Instantiate(m_GameParameters.asteroid, transform.position, Quaternion.identity);
                asteroid.SetAcceleration(ExtensionMethods.RandomVector() * asteroid.EntityParameters.accelerationScalar);
                asteroid.ApplyForces();

                m_Asteroids.Add(asteroid);
                m_NumberOfSpawnedAsteroids++;
            }

            yield return new WaitForSeconds(m_GameParameters.delayBetweenSpawn);
        }
    }
}