using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed=5f;
    /// <summary>
    /// 蛇身prefab
    /// </summary>
    public GameObject bodyPrefab;

    /// <summary>
    /// 蛇身List
    /// </summary>
    private List<GameObject> bodyList = new List<GameObject>();
    /// <summary>
    /// 蛇头路径
    /// </summary>
    private List<Vector3> headWayList = new List<Vector3>();
    /// <summary>
    /// 蛇的初始节数
    /// </summary>
    private int initNum = 5;
    private void Awake()
    {
        headWayList.Clear();
        bodyList.Clear();
        for(int i = 0; i < initNum; i++)
        {
            CreateBody();
        }

    }
    private void Update()
    {
        if ((transform.position - MousePosition()).magnitude > 1f)
            MoveRotation(MousePosition());
        Move();
        Debug.Log(headWayList.Count);
        BodyMove(bodyList);
        CleanList();
        GameManager.Instance.Lenth = bodyList.Count;
    }
    #region 蛇身
    private void CreateBody()
    {
        GameObject newbody = Instantiate(bodyPrefab);
        bodyList.Add(newbody);

    }
    private void BodyMove(List<GameObject> body)
    {
        for(int i = 0; i < body.Count; i++)
        {
            if(headWayList.Count>3*(i+1))
                body[i].transform.position = headWayList[headWayList.Count-3*(i+1)];
        }

    }
    #endregion

    #region 蛇头
    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        gameObject.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        headWayList.Add(gameObject.transform.position);
        
    }
    /// <summary>
    /// 旋转
    /// </summary>
    /// <param name="targetPos"></param>
    private void MoveRotation(Vector3 targetPos)
    {
        Vector2 direction = targetPos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
    /// <summary>
    /// 获得鼠标位置
    /// </summary>
    /// <returns></returns>
    private Vector3 MousePosition()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 v1 = Camera.main.ScreenToWorldPoint(mousePos);
        v1 = new Vector3(v1.x, v1.y, 0f);
        return v1;
    }
    #endregion
    private void EatFood()
    {
        CreateBody();
    }
    private void EatWeed()
    {
        DestroyLastBody();
    }
    private void EatBoom()
    {
        for(int i = 0; i < bodyList.Count / 2; i++)
        {
            DestroyLastBody();
        }
    }
    private void EatMushroom()
    {
        for (int i = 0; i < bodyList.Count; i++)
        {
            CreateBody();
        }

    }
    private void CleanList()
    {
        if (headWayList.Count > 6 * bodyList.Count + 5)
            headWayList.Remove(headWayList[0]);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Food":
                EatFood();
                collision.transform.position = RandomPos();
                break;
            case "Weed":
                EatWeed();
                collision.transform.position = RandomPos();
                break;
            case "Boom":
                EatBoom();
                collision.transform.position = RandomPos();
                break;
            case "Mushroom":
                EatMushroom();
                collision.transform.position = RandomPos();
                break;
        }
    }
    private Vector3 RandomPos()
    {
        float x = Random.Range(-GameManager.Instance.boundaryX, GameManager.Instance.boundaryX);
        float y = Random.Range(-GameManager.Instance.boundaryY, GameManager.Instance.boundaryY);
        return new Vector3(x, y, 0);
    }
    private void DestroyLastBody()
    {
        Destroy(bodyList[bodyList.Count - 1]);
        bodyList.RemoveAt(bodyList.Count - 1);
    }

}
