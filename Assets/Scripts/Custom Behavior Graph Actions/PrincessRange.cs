using Unity.Behavior;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PrincessRange : MonoBehaviour
{
    private float followRange = 5;
    private GameObject player;
    private float enemyDetectRange = 20;
    private Color color;
    private Color enemyColor = Color.green;
    private GameObject[] enemies;
    private Vector3[] enemyPositions;
    // Start is called once before the first execution of Update after the MonoBehaviour is createds
    

    // Update is called once per frame

    public void Initialize(GameObject player, float followRange)
    {
        this.player = player;
        this.followRange = followRange;
        enemies = GetEnemies();
        enemyPositions = GetEnemyPositions();
        Debug.Log("From Range (enemy count): " + enemyPositions.Length);
    }


    public bool isPlayerDetected()
    {
        float distanceFromPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        if (distanceFromPlayer < followRange) 
        {
            color = Color.green;
            return true;
        }
        color = Color.yellow;
        return false;
    }
    
    public bool isEnemyDetected()
    {
       foreach(GameObject e in enemies)
       {
            float distanceFromEnemy = Vector3.Distance(e.transform.position, transform.position);

            if (distanceFromEnemy < enemyDetectRange)
            {
                enemyColor = Color.red;
                return true;
            }
            
       }
        Debug.Log("Enemies Detected: " + enemies.Length);
       enemyColor = Color.green;    
       return false;
    }
    public GameObject[] GetEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
        return enemies;
    }

    public Vector3[] GetEnemyPositions()
    {
        Vector3[] enemyPositions = new Vector3[enemies.Length];

        for(int i = 0; i < enemies.Length; i++)
        {
            enemyPositions[i] = enemies[i].transform.position;
        }
        return enemyPositions;
    }
    
   

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, followRange);
        //Gizmos.DrawLine(transform.position, player.transform.position);
        Gizmos.color = enemyColor;
        Gizmos.DrawWireSphere(transform.position, enemyDetectRange);
        foreach(var epos in (ReadOnlySpan<Vector3>)enemyPositions)
        {
            Gizmos.DrawLine(transform.position, epos);
        }

    }
}
