using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Vector3 cameraPos;
    [SerializeField]
    private float zOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            // cameraPos, same x and y as Player, z + offset (-ve z value is outwards screen)
            cameraPos = player.transform.position + new Vector3(0, 0, zOffset);
            
            gameObject.transform.SetPositionAndRotation(cameraPos, new Quaternion(0, 0, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        } else {
            cameraPos = player.transform.position + new Vector3(0, 0, zOffset);
            
            gameObject.transform.SetPositionAndRotation(cameraPos, new Quaternion(0, 0, 0, 0));
        }
        
    }
}
