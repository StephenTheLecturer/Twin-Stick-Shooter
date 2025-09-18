using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;

    public GameObject m_NPCPrefab;
    public int m_maxNPCAmonut;
    public List<NPCController> m_NPCList = new List<NPCController>();

    public float m_NPCSpeed = 5f;
    public Vector2 m_playerLocation;

    public Vector2 m_verticalBounds;
    public Vector2 m_horizontalBounds;
    public float m_spawnPadding = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        SpawnNPC();

    }

    // Update is called once per frame
    void Update()
    {
        ManageNPC();
    }

    public void SpawnNPC() 
    {
        GetScreenBounds();

        for (int i = m_NPCList.Count; i < m_maxNPCAmonut; ++i) 
        {
            GameObject temp = Instantiate(m_NPCPrefab);
            m_NPCList.Add(temp.GetComponent<NPCController>());

            int spawnSide = Random.Range(0,4);
            Vector2 spawnPosition = Vector2.zero;

            switch (spawnSide) 
            {
                case 0:
                    //right side spawn
                    spawnPosition = new Vector2(m_horizontalBounds.y, Random.Range(m_verticalBounds.x, m_verticalBounds.y));
                    break;
                case 1:
                    //left side spawn
                    spawnPosition = new Vector2(m_horizontalBounds.x, Random.Range(m_verticalBounds.x, m_verticalBounds.y));
                    break;
                case 2:
                    //top side spawn
                    spawnPosition = new Vector2(Random.Range(m_horizontalBounds.x, m_horizontalBounds.y), m_verticalBounds.y);
                    break;
                case 3:
                    //bottom side spawn
                    spawnPosition = new Vector2(Random.Range(m_horizontalBounds.x, m_horizontalBounds.y), m_verticalBounds.x);
                    break;
            }
            
            temp.transform.position = spawnPosition;

        }
    }

    public void ManageNPC() 
    {
        m_playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;

        foreach (NPCController nC in m_NPCList) 
        {
            nC.m_movementSpeed = m_NPCSpeed;

            nC.m_targetMoveLocation = m_playerLocation;
        }
        
    }

    void GetScreenBounds()
    {
        float verticalBounds = Camera.main.orthographicSize;
        float horizontalBounds = verticalBounds * Screen.width / Screen.height;

        float tempVertical = verticalBounds + m_spawnPadding;
        float tempHorizontal = horizontalBounds + m_spawnPadding;

        Vector2 currentCameraPosition = Camera.main.transform.position;

        m_verticalBounds = new Vector2(currentCameraPosition.y - tempVertical, currentCameraPosition.y + tempVertical);
        m_horizontalBounds = new Vector2(currentCameraPosition.x - tempHorizontal, currentCameraPosition.x + tempHorizontal);
    }

}
