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
    public float rotationSpeed = 10f;
    public float shootOffset = 1f;
}

public class Spaceship : Entity
{
    #region Variables
    [SerializeField]
    private SpaceshipControls m_Controls = new SpaceshipControls();
    [SerializeField]
    private SpaceshipParameters m_SpaceshipParameters = new SpaceshipParameters();
    [SerializeField]
    private Bullet m_Bullet;
    #endregion Variables

    protected override void Update()
    {
        base.Update();
        
        Shoot();
    }

    protected override void FixedUpdate()
    {
        RotateSpaceship();
        MoveSpaceship();

        ApplyForces();
    }

    private void RotateSpaceship()
    {
        float rotationDirection = Input.GetAxisRaw(m_Controls.rotation);

        if (rotationDirection != 0)
        {
            Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles + (rotationDirection * (transform.up * m_SpaceshipParameters.rotationSpeed)));
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, m_SpaceshipParameters.rotationSpeed * Time.deltaTime);

            m_EntityRigidbody.MoveRotation(newRotation);
        }
    }

    private void MoveSpaceship()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            SetAcceleration(transform.InverseTransformDirection(transform.forward));
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Bullet bullet = Instantiate(m_Bullet, transform.position + (transform.forward * m_SpaceshipParameters.shootOffset), Quaternion.identity);

            bullet.SetAcceleration(transform.forward * bullet.EntityParameters.accelerationScalar);
            bullet.ApplyForces();
        }
    }
}