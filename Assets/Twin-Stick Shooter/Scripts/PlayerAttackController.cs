 using UnityEngine;

[RequireComponent( typeof( PlayerController ) )]
public class PlayerAttackController : MonoBehaviour
{
    PlayerController m_playerController;

    public GameObject m_bulletPrefab;

    public float m_attackSpeed;

    public enum m_FireMode 
    { 
        SemiAuto,
        FullAudo
    }

    public m_FireMode m_fireMode;

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

    void PlayerInput() 
    {
        Vector3 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        aimPosition.z = 0;

        Vector3 aimDirection = aimPosition - transform.position;

        aimDirection = aimDirection.normalized;

        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            SpawnBullet(aimDirection);
        }

    }

    void SpawnBullet(Vector3 aimDirection)
    {
        GameObject bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
        var bC = bullet.GetComponent<BulletController>();
        bC.m_direction = aimDirection;
        bC.m_damage = m_playerController.m_attackDamage;
    }
}