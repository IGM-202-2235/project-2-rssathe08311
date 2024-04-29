using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] Agent shadePrefab;
    [SerializeField] Obstacle obstaclePrefab1;
    [SerializeField] Obstacle obstaclePrefab2;

    public List<Agent> humans;

    public List<Agent> shades;

    public Agent aggressor;

    public List<Obstacle> obstacles;

    [SerializeField] int spawnCount = 100;
    [SerializeField] int obstacleCount = 5;



    Vector2 screenSize = Vector2.zero;


    public Vector2 ScreenSize
    {
        get { return screenSize; }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = screenSize.y * Camera.main.aspect;

        Spawn(shadePrefab);
        SpawnObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(Agent agentPrefab)
    {
        for(int i = 0; i < spawnCount; i++)
        {
            Agent newAgent = Instantiate(agentPrefab, PickRandomPosition(), Quaternion.identity);

            newAgent.agentManager = this;

            shades.Add(newAgent);
        }
       
    }

    void SpawnObstacles()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            Obstacle newObstacle = null;
            if (i % 2 == 0)
            {
                newObstacle = Instantiate(obstaclePrefab2, PickRandomPosition(), Quaternion.identity);
            }
            else
            {
                newObstacle = Instantiate(obstaclePrefab1, PickRandomPosition(), Quaternion.identity);
            }

            obstacles.Add(newObstacle);
        }
    }

    public void UserShadeSpawn()
    {

        for (int i = 0; i < 5; i++)
        {

            Agent newShade = Agent.Instantiate(shadePrefab, PickRandomPosition(), Quaternion.identity);

            newShade.GetComponent<Agent>().agentManager = this;

            shades.Add(newShade.GetComponent<Agent>());
        }
    }

    Vector2 PickRandomPosition()
    {
        Vector2 randPos = Vector2.zero;

        randPos.x = Random.Range(-screenSize.x, screenSize.x);
        randPos.y = Random.Range(-screenSize.y, screenSize.y);

        return randPos;
    }


}
