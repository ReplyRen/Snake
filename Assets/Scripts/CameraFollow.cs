using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smooth = 5f;                // how smooth the camera movement is   
    private Vector3 targetPosition;     // the position the camera is trying to be in  
    private Transform follow;
    private void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        targetPosition = new Vector3(follow.position.x, follow.position.y, -10f); 
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}
