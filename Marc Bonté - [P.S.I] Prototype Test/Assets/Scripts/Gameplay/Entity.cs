using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Entity : MonoBehaviour
{
    private const float BORDER_MARGIN = 0.5f;
    protected Rigidbody m_EntityRigidbody;

    protected void Awake()
    {
        InitialiseRigidbody();
    }

    private void InitialiseRigidbody()
    {
        m_EntityRigidbody = GetComponent<Rigidbody>();
        m_EntityRigidbody.useGravity = false;
    }

    protected virtual void Update()
    {
        ConstrainPositionToCamera();
    }

    private void ConstrainPositionToCamera()
    {
        // Horizontal constrain
        if (transform.position.x < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(Camera.main.orthographicSize - BORDER_MARGIN, 0f, transform.position.z);
        }
        else if (transform.position.x > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(-Camera.main.orthographicSize + BORDER_MARGIN, 0f, transform.position.z);
        }

        // Vertical constrain
        if (transform.position.z < -Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, 0f, Camera.main.orthographicSize - BORDER_MARGIN);
        }
        else if (transform.position.z > Camera.main.orthographicSize)
        {
            transform.position = new Vector3(transform.position.x, 0f, -Camera.main.orthographicSize + BORDER_MARGIN);
        }
    }
}