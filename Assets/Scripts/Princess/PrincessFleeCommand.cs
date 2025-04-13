using UnityEngine;
using UnityEngine.AI;

public class PrincessFleeCommand : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3[] enemyPositions;
    Vector3 fleeVector;
    private GameObject[] enemies;
    private NavMeshAgent agent;
    private Animator m_Animator;
    private Transform player;
    public void Initialize()
    {
        agent = GetComponent<NavMeshAgent>();
        enemies = GetEnemies();
        enemyPositions = GetEnemyPositions();
        m_Animator = GetComponentInChildren<Animator>();
        if (m_Animator != null)
        {
            m_Animator.SetFloat("SpeedMagnitude", 5);
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Flee()
    {
        CalculateFleeVector();
        //transform.position += -fleeVector.normalized * 6;
        agent.SetDestination(transform.position + fleeVector.normalized * 1);
        
    }

    public void CalculateFleeVector()
    {
        fleeVector = Vector3.zero;
        foreach (var epos in enemyPositions)
        {
            fleeVector += transform.position - epos;
        }
        
    }
    public GameObject[] GetEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        return enemies;
    }

    public Vector3[] GetEnemyPositions()
    {
        Vector3[] enemyPositions = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
        }
        return enemyPositions;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + fleeVector.normalized * 100);
    }
}
