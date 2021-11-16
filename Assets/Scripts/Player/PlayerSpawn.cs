using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{

    public List<GameObject> playerPrefabs;

    public void PlayerSelect(int selection)
    {
        Instantiate(playerPrefabs[selection].gameObject, new Vector3(-0.97f, 1.7f, 0f), Quaternion.identity);
    }
}