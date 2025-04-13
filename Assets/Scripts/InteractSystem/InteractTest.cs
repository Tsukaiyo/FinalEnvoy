using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour, IInteractable
{
    public string Prompt => "Open Chest";
    private bool isBeingPushed = false;
    private Vector3 pushDirection;
    private Vector3 targetPosition;
    public float moveSpeed = 3f;

    void Update()
    {
        if (isBeingPushed)
        {
            // Move towards the target position at a consistent speed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Stop moving when close enough
            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                isBeingPushed = false;
            }
        }
    }

    public void interact (GameObject interactor) {
        if (!isBeingPushed)
        {
            pushDirection = transform.position - interactor.transform.position;
            pushDirection.y = 0;
            pushDirection = pushDirection.normalized;
            targetPosition = transform.position + pushDirection * 2f;
            isBeingPushed = true;
        }
    }
}
