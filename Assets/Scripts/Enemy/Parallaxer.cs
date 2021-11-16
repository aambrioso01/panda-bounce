using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour
{
   class PoolObject
    {
        public GameObject gameObject;
        public Transform transform;
        public bool inUse;
        public PoolObject(Transform t, GameObject go) { 
            transform = t;
            gameObject = go;
        }
        public void Use() { inUse = true; }
        public void Dispose() { inUse = false;}
    }

    [System.Serializable]
    public struct YSpawnRange {
        public float minY;
        public float maxY;
    }

    int rand;
    private GameObject Prefab;
    public GameObject[] Prefabs;
    public int poolSize;
    public float shiftSpeed;
    public float spawnRate;

    public YSpawnRange ySpawnRange;
    public Vector3 defaultSpawnPos;
    public bool spawnImmediate;
    public Vector3 immediateSpawnPos;
    public Vector2 targetAspectRatio;
    public Sprite backgroundScreen;

    private float textureUnitSizeX;
    float spawnTimer;
    float targetAspect;
    PoolObject[] poolObjects;
    GameObject[] obstacles;
    GameManager game;

    
    void Awake()
    {
        Configure();
    }

    void Start()
    {
        game = GameManager.Instance;

        Texture2D texture = backgroundScreen.texture;
        textureUnitSizeX = (texture.width / backgroundScreen.pixelsPerUnit) / 4;
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameOverConfirmed()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].Dispose();
            poolObjects[i].transform.position = Vector3.one * 1000;
        }
        Configure();
    }

    void Update()
    {
        if (game.GameOver) return;

        Shift();
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            Configure();
            Spawn();
            spawnTimer = 0;
            //Debug.Log("Array element 0 = " + poolObjects[0]);
        }
    }

    void Configure()
    {
        targetAspect = targetAspectRatio.x / targetAspectRatio.y;
        poolObjects = new PoolObject[poolSize];

        // probably should use a pushable/poppable list here for poolObjects
        // would allow us to pop old one and push a new one in CheckDisposeObject

        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (Prefabs.Length == 1)
            {
                Prefab = Prefabs[0];
            }
            else
            {
                // pick next obstacle prefab randomly from the obstacle prefab array
                rand = Random.Range(0, 4);
                //Debug.Log("random # >>> " + rand);
                //Debug.Log("array length = " + Prefabs.Length);
                Prefab = Prefabs[rand];
            }
            

            // create and position obstacle
            GameObject go = Instantiate(Prefab) as GameObject;
            //GameObject obj = Instantiate(Prefab) as GameObject;

            // works but sucks
            //Destroy(go, spawnRate);
            Transform t = go.transform;
            //t.SetParent(transform);
            t.position = Vector3.one * 1000;
            poolObjects[i] = new PoolObject(t, go);
        }

        if (spawnImmediate)
        {
            spawnImmediate = false;
            SpawnImmediate();
        }
    }

    void Spawn()
    {
        Transform t = GetPoolObject();
        if (t == null) return;
        
        Vector3 pos = Vector3.zero;
        pos.x = (defaultSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.y = Random.Range(ySpawnRange.minY, ySpawnRange.maxY);
        
        t.position = pos;
    }

    void SpawnImmediate()
    {
        Transform t = GetPoolObject();
        if (t == null) return;
        Vector3 pos = Vector3.zero;
        pos.x = (immediateSpawnPos.x * Camera.main.aspect) / targetAspect;
        pos.y = Random.Range(ySpawnRange.minY, ySpawnRange.maxY);
        t.position = pos;
        Spawn();
    }

    void Shift()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].transform.localPosition += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposeObject(poolObjects[i]);
            
        }
    }

    void CheckDisposeObject(PoolObject poolObject)
    {
        // need to differentiate between types here mushroom vs ground vs stars
        if (poolObject.transform.position.x < (-defaultSpawnPos.x * Camera.main.aspect) / targetAspect)
        {
            //Debug.Log("go: " + poolObject.gameObject.name);
            //Destroy(poolObject.gameObject);
            //Configure();

            //GameObject clone = Instantiate(poolObject.go) as GameObject;
            //Destroy(clone);
            //Debug.Log("this: " + this);
            //Destroy(this.transform.GetChild(0));
            //poolObject.Dispose();
            //poolObject.transform.position = Vector3.one * 1000;
        }
        /*
        // reset prefab to the right of the screen
        if(poolObject.transform.position.x < -textureUnitSizeX)
        {
            poolObject.transform.position += new Vector3(textureUnitSizeX * 3, 0, 0);
        }
        */
    }

    Transform GetPoolObject()
    {
        for(int i = 0; i < poolObjects.Length; i++)
        {
            if (!poolObjects[i].inUse)
            {
                poolObjects[i].Use();
                return poolObjects[i].transform;
            }
        }
        return null;
    }

}
