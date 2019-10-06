using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public float smooth = 7f;                // how smooth the camera movement is   
    private Vector3 targetPosition;     // the position the camera is trying to be in  
    private Transform follow;
    private void Start()
    {
        follow = GameObject.FindWithTag("Player").transform;
    }
    private void LateUpdate()
    {
        targetPosition = follow.position;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
    }
}
