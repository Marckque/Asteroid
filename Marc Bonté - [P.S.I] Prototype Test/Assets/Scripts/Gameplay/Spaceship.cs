using UnityEngine;

[System.Serializable]
public class SpaceshipControls
{
    public string rotation = "Horizontal";
    public string moveForward = "MoveForward";
    public string shoot = "Shoot";
}

[System.Serializable]
public class SpaceshipParameters
{
    public float accelerationAmount = 1f;
    public float maxVelocityMagnitude = 1f;
    public float rotationSpeed = 1f;
}

public class Spaceship : Entity
{
    #region Variables
    [SerializeField]
    private SpaceshipControls m_Controls = new SpaceshipControls();
    [SerializeField]
    private SpaceshipParameters m_Parameters = new SpaceshipParameters();

    private Vector3 m_Acceleration;
    #endregion Variables

    protected override void Update()
    {
        base.Update();

        // Inputs
        RotateSpaceship();
        MoveSpaceship();

        ApplyForces();
    }

    private void RotateSpaceship()
    {
        float rotationDirection = Input.GetAxisRaw(m_Controls.rotation);

        if (rotationDirection != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + (rotationDirection * (transform.up * m_Parameters.rotationSpeed)));
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, m_Parameters.rotationSpeed * Time.deltaTime);

            m_EntityRigidbody.MoveRotation(newRotation);
        }
    }

    private void MoveSpaceship()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            SetAcceleration(transform.InverseTransformDirection(transform.forward));
        }
    }

    private void SetAcceleration(Vector3 force)
    {
        m_Acceleration += force * m_Parameters.accelerationAmount;
    }

    private void ApplyForces()
    {
        m_EntityRigidbody.AddForce(transform.TransformDirection(m_Acceleration));
        m_EntityRigidbody.velocity = Vector3.ClampMagnitude(m_EntityRigidbody.velocity, m_Parameters.maxVelocityMagnitude);

        m_Acceleration = Vector3.zero;
    }
}