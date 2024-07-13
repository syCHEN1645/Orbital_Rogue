using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10f;
    

    [Header("Player Stats")]
    public float Health = 100f;
    public float Damage = 5f;

    public void Upgrade()
    {

    }
}
