using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    public List<GameObject> grassPrefab;
    private List<GameObject[]> grassClones = new List<GameObject[]>();

    [SerializeField] private float SCROLL_SPEED;

    private float textureUnitSizeX;
    public Vector3 position0;
    private Vector3 position1;
    private Vector3 position2;
    public Vector3 rotation;

    GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = GameManager.Instance;

        foreach (GameObject pf in grassPrefab)
        {
            // create clones for each and store GameObject[]'s in a list
            grassClones.Add(createPrefab(pf));   
        }

        Sprite sprite = grassPrefab[0].GetComponentInChildren<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) / 4;

        // build vectors for the prefab locations
        createVectors();

        // place the tiles in correct location
        foreach (GameObject[] pfClone in grassClones)
        {
            transformGrass(pfClone);
        }
    }

    void Update()
    {
        if (game.GameOver) return;

        foreach (GameObject[] pfClone in grassClones)
        {
            foreach (GameObject pf in pfClone)
            {
                // move pf downwards
                pf.transform.position += GlobarVars.VECTOR_LEFT * SCROLL_SPEED * Time.deltaTime;

                // reset prefab to the right of the screen
                if(pf.transform.position.x < -textureUnitSizeX)
                {
                    pf.transform.position += new Vector3(textureUnitSizeX * 3, 0, 0);
                }
            }
        }
    }


    void createVectors()
    {
        //position0 = new Vector3(0, 0, 0);
        position1 = new Vector3(textureUnitSizeX, position0.y, 0);
        position2 = new Vector3(textureUnitSizeX * 2, position0.y, 0);
    }

    void transformGrass(GameObject[] grassAndClones){
        grassAndClones[0].transform.position += new Vector3(position0.x, 0, 0);
        grassAndClones[1].transform.position += position1;
        grassAndClones[2].transform.position += position2;
    }

    GameObject[] createPrefab(GameObject pf) 
    {
        GameObject[] pfs = new GameObject[] 
        {
            Instantiate(pf, position0, Quaternion.Euler(rotation)),
            Instantiate(pf, position1, Quaternion.Euler(rotation)),
            Instantiate(pf, position2, Quaternion.Euler(rotation))
        };
        return pfs;
    }
}
