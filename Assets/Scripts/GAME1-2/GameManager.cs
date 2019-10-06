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
    /// 保护罩的数量
    /// </summary>
    public int coverCount = 10;
    /// <summary>
    /// 静止的墙的数量
    /// </summary>
    public int staticWallCount = 15;
    /// <summary>
    /// 旋转的墙的数量
    /// </summary>
    public int rotatingWallCount = 5;
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
    /// 墙prefab
    /// </summary>
    public GameObject[] wall;
    /// <summary>
    /// 保护罩prefab
    /// </summary>
    public GameObject coverPrefab;
    private GameObject[] rotatingWall;
    /// <summary>
    /// x边界
    /// </summary>
    public float boundaryX = 29.5f;
    /// <summary>
    /// y边界
    /// </summary>
    public float boundaryY = 30f;
    private float timer;
    private void Start()
    {
        CreateMap();
        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        Rotate(rotatingWall);
    }
    private void CreateMap()
    {
        CreateItem(foodPrefab,foodCount);
        CreateItem(weedPrefab,weedCount);
        CreateItem(boomPrefab,boomCount);
        CreateItem(mushroomPrefab,mushroomCount);
        CreateItem(wall, staticWallCount);
        CreateItem(coverPrefab, coverCount);
        rotatingWall = CreateItem(wall, rotatingWallCount, true);
    }
    private void CreateItem(GameObject prefab,int count)
    {
        for(int i = 0; i < count; i++)
        {
            Vector3 pos = RandomPos();
            GameObject item = Instantiate(prefab);
            item.transform.position = pos;
            item.transform.SetParent(Env,true);
        }

    }
    private void CreateItem(GameObject[] prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = RandomPos();
            GameObject item = Instantiate(prefab[RandomIndex(0,prefab.Length-1)]);
            item.transform.position = pos;
            item.transform.SetParent(Env, true);
            item.transform.Rotate(new Vector3(0, 0, RandomIndex(0, 180)), Space.World);
        }

    }
    private GameObject[] CreateItem(GameObject[] prefab, int count,bool x)
    {
        GameObject[] gameObjects = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = RandomPos();
            GameObject item = Instantiate(prefab[RandomIndex(0, prefab.Length - 1)]);
            item.transform.position = pos;
            item.transform.SetParent(Env, true);
            item.transform.Rotate(new Vector3(0, 0, RandomIndex(0, 180)), Space.World);
            gameObjects[i] = item; 
        }
        return gameObjects;

    }
    private Vector3 RandomPos()
    {
        float x = Random.Range(-boundaryX, boundaryX);
        float y = Random.Range(-boundaryY, boundaryY);
        return new Vector3(x, y, 0);
    }
    private int RandomIndex(int from ,int to)
    {
        int index = Random.Range(from, to+1);
        return index;
    }
    private float RandomIndex(float from, float to)
    {
        float index = Random.Range(from, to + 1);
        return index;
    }
    private void Rotate(GameObject[] gameObjects)
    {
        foreach(GameObject x in gameObjects)
        {
            x.transform.Rotate(new Vector3(0, 0, 1), Space.World);
        }
    }
}
