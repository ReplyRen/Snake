using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed=5f;
    private Vector3 moveTo;
    private void Update()
    {
        if ((transform.position - MousePosition()).magnitude > 1f) 
            moveTo = MoveDirection(transform.position, MousePosition());
        Move(moveTo);
    }
    private void Move(Vector3 targetDir)
    {
        gameObject.transform.Translate(targetDir * moveSpeed * Time.deltaTime);
        
    }
    private Vector3 MousePosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 v1 = Camera.main.ScreenToWorldPoint(mousePos);
        v1 = new Vector3(v1.x, v1.y, 0f);
        return v1;
    }
    private Vector3 MoveDirection(Vector3 from,Vector3 to)
    {
        Vector3 vector = to - from;
        vector = vector.normalized;
        return vector;
    }
}
