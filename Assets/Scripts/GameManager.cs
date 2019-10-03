using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        Env = GameObject.FindWithTag("Env").transform;
    }
    private Transform Env;
    public int Lenth;
    /// <summary>
    /// 食物的数量
    /// </summary>
    public int foodCount = 20;
    /// <summary>
    /// 毒草的数量
    /// </summary>
    public int weedCount = 10;
    /// <summary>
    /// 炸弹的数量
    /// </summary>
    public int boomCount = 5;
    /// <summary>
    /// 蘑菇的数量
    /// </summary>
    public int mushroomCount = 5;
    /// <summary>
    /// 食物prefab
    /// </summary>
    public GameObject foodPrefab;
    /// <summary>
    /// 炸弹prefab
    /// </summary>
    public GameObject boomPrefab;
    /// <summary>
    /// 毒草prefab
    /// </summary>
    public GameObject weedPrefab;
    /// <summary>
    /// 蘑菇prefab
    /// </summary>
    public GameObject mushroomPrefab;
    /// <summary>
    /// x边界
    /// </summary>
    public float boundaryX = 29.5f;
    /// <summary>
    /// y边界
    /// </summary>
    public float boundaryY = 30f;
    private void Start()
    {
        CreateMap();

    }
    private void CreateMap()
    {
        for (int i = 0; i < foodCount; i++)
        {
            CreateItem(foodPrefab);
        }
        for (int i = 0; i < weedCount; i++)
        {
            CreateItem(weedPrefab);
        }
        for (int i = 0; i < boomCount; i++)
        {
            CreateItem(boomPrefab);
        }
        for (int i = 0; i < mushroomCount; i++)
        {
            CreateItem(mushroomPrefab);
        }
    }
    private void CreateItem(GameObject prefab)
    {
        Vector3 pos = RandomPos();
        GameObject item = Instantiate(prefab);
        item.transform.position = pos;
        item.transform.SetParent(Env,true);
    }
    private Vector3 RandomPos()
    {
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        return new Vector3(x, y, 0);
    }
}
