using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    public List<GameObject> players = new List<GameObject>();
    public int followingPlayer;
    public float smoothSpeed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        
    }

   
    void LateUpdate()
    {
        if (players[followingPlayer] != null)
        {
            Vector3 playersPos = new Vector3(players[followingPlayer].transform.position.x, 0, -10);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, playersPos, smoothSpeed);
            transform.position = smoothedPosition;
        }
        
    }
}
