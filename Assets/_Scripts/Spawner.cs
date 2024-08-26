using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    [SerializeField] GameObject playerPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Spawn()
    {
        
        StartCoroutine(SpawnDelay());
        
        
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(1);
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CameraFollow.Instance.followingPlayer++;
    }
}
