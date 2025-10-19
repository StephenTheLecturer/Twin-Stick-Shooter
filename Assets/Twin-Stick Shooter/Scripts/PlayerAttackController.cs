using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof( PlayerController ) )]
public class PlayerAttackController : MonoBehaviour
{
    PlayerController m_playerController;

    public GameObject m_bulletPrefab;

    public float m_attackSpeed;
    float m_currentAttackTimer;

    public float m_projectileSpeed = 15f;

    Vector3 aimPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    public void OnAttack(InputValue value) 
    {
        aimPosition = value.Get<Vector2>();
    }

    void PlayerInput() 
    {
        aimPosition.z = 0;

        Vector3 aimDirection = aimPosition.normalized;

        if (aimPosition != Vector3.zero)
        {
            m_currentAttackTimer -= Time.deltaTime;

            if (m_currentAttackTimer <= 0)
            {
                SpawnBullet(aimDirection);

                m_currentAttackTimer = m_attackSpeed;
            }
        }
        else
        {
            m_currentAttackTimer = 0;
        }

    }

    void SpawnBullet(Vector3 aimDirection)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, transform.position + aimDirection, Quaternion.identity);

        bullet.transform.up = ((transform.position + new Vector3(aimDirection.x, aimDirection.y)) - transform.position);

        var bC = bullet.GetComponent<BulletController>();
        bC.m_direction = aimDirection;
        bC.m_damage = m_playerController.m_attackDamage;
        bC.m_speed = m_projectileSpeed;
    }
} 