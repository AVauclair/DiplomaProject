using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    Transform player;
    Vector3 playerVector;

    void Start()
    {
        player = GameObject.Find("Player").transform; //player тут - это название игрока    
    }

    void Update()
    {
        playerVector = player.position;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector, Time.deltaTime);
    }
}
