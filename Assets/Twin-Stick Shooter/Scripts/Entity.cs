using UnityEngine;

public class Entity : MonoBehaviour
{
    public enum MovementType 
    { 
        transform,
        rigidbody
    }

    public MovementType m_movementType;

    public Transform m_transform;

    public Rigidbody m_rigidbody;

    public float m_health = 100f;

    public float m_movementSpeed = 1f;

    public float m_attackDamage = 5f;

    private void OnEnable()
    {
        m_transform = transform;

        if (GetComponent<Rigidbody>())
        { 
            m_rigidbody = GetComponent<Rigidbody>();
        }

    }

    public void MoveEntity(Vector2 movementValue) 
    {
        Vector3 newPosition = transform.position += new Vector3(movementValue.x * m_movementSpeed, movementValue.y * m_movementSpeed, 0) * Time.deltaTime;

        if (m_transform != null && m_movementType == MovementType.transform)
            m_transform.position = newPosition;
        else if (m_rigidbody != null && m_movementType == MovementType.rigidbody)
            m_rigidbody.Move(newPosition, Quaternion.identity);

    }

    public void ChangeHealth(float changeAmount) 
    {
        m_health += changeAmount;

        if (m_health <= 0)
            OnDeath();
    }

    public virtual void OnDeath() 
    { 
        //Death Logic Will Go Here...
    }

}
