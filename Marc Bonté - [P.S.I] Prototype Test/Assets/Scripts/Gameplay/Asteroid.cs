using UnityEngine;

public class Asteroid : Entity
{
    [SerializeField, Range(0f, 1f)]
    private float m_AccelerationMultiplier = 1f;

    protected override void Awake()
    {
        base.Awake();

        EntityParameters.accelerationScalar *= (1 + (Random.Range(-m_AccelerationMultiplier, m_AccelerationMultiplier)));
    }
}