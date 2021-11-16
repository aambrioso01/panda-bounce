using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MushScroller : MonoBehaviour
{
    public List<GameObject> mushPrefabs;
    private List<List<GameObject>> mushInstances = new List<List<GameObject>>();

    private float textureUnitSizeX;
    public Vector3 position0;
    private Vector3 position1;
    private Vector3 position2;
    private Vector3 position3;

    public float shiftSpeed;

    GameManager game;

    int rand;
    int rand2;
    int rand3;
    int rand4;

    public GameObject screenPrefab;

    void Awake()
    {
        Configure();
    }

    void Configure()
    {
        mushInstances.Add(createPrefab(mushPrefabs));

        // build vectors for the prefab locations
        createVectors();

        // place the tiles in correct location
        foreach (List<GameObject> pfClone in mushInstances)
        {
            transformMush(pfClone);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;

        Sprite sprite = screenPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * screenPrefab.transform.localScale.x;
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void Update()
    {
        if (game.GameOver) return;
        Shift();
    }

    void Shift()
    {
        foreach (List<GameObject> pfClone in mushInstances)
        {
            for (int i = 0; i < pfClone.Count; i++)
            {
                // move pf leftwards
                pfClone[i].transform.position += Vector3.left * shiftSpeed * Time.deltaTime;

                // reset prefab to the right of the screen if position is off screen
                if(pfClone[i].transform.position.x < -textureUnitSizeX)
                {
                    rand = Random.Range(0, mushPrefabs.Count);
                    Destroy(pfClone[i]);
                    pfClone[i] = Instantiate(mushPrefabs[rand].gameObject, new Vector3(2 * textureUnitSizeX, 0, 0), Quaternion.identity);
                }   
            }
        }
    }

    void createVectors()
    {
        position1 = new Vector3(2 * position0.x, position0.y, 0);
        position2 = new Vector3(3 * position0.x, position0.y, 0);
        position3 = new Vector3(4 * position0.x, position0.y, 0);
    }

    void transformMush(List<GameObject> mushAndClones)
    {
        mushAndClones[0].transform.position = position0;
        mushAndClones[1].transform.position = position1;
        mushAndClones[2].transform.position = position2;
        mushAndClones[3].transform.position = position3;
    }

    List<GameObject> createPrefab(List<GameObject> pfs) 
    {
        
        rand = Random.Range(0, mushPrefabs.Count);
        rand2 = Random.Range(0, mushPrefabs.Count);
        rand3 = Random.Range(0, mushPrefabs.Count);
        rand4 = Random.Range(0, mushPrefabs.Count);

        List<GameObject> prefabs = new List<GameObject>
        {
            Instantiate(pfs[rand].gameObject, position0, Quaternion.identity),
            Instantiate(pfs[rand2].gameObject, position1, Quaternion.identity),
            Instantiate(pfs[rand3].gameObject, position2, Quaternion.identity),
            Instantiate(pfs[rand4].gameObject, position3, Quaternion.identity)
        };

        return prefabs;
    }

    void OnGameOverConfirmed()
    {
        // clears the screen and resets positions
        Reconfigure();
    }

    void Reconfigure()
    {
        foreach (List<GameObject> pfClone in mushInstances)
        {
            for (int i = 0; i < pfClone.Count; i++)
            {
                rand = Random.Range(0, mushPrefabs.Count);
                Destroy(pfClone[i]);
                pfClone[i] = Instantiate(mushPrefabs[rand].gameObject, new Vector3(position0.x * (i + 1), 0, 0), Quaternion.identity);
            }
        }
    }
}