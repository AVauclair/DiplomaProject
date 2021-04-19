using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchPlayer : MonoBehaviour
{
    Transform player;
    Vector3 playerVector;

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float downLimit;
    [SerializeField]
    float upLimit;

    void Start()
    {
        player = GameObject.Find("Player").transform; //player тут - это название игрока    
    }

    void Update()
    {
        playerVector = player.position;
        //playerVector.y = playerVector.y + 0.7f;
        playerVector.z = -10;
        transform.position = Vector3.Lerp(transform.position, playerVector, Time.deltaTime);

        /*transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, downLimit, upLimit),
            transform.position.z
            );*/
    }
}
