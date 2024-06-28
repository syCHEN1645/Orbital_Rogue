using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera virtualCamera;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        virtualCamera.LookAt = player.transform;
        virtualCamera.Follow = player.transform;
        
    }

    void Update()
    {
        
    }
}
