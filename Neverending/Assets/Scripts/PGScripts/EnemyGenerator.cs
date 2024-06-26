using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : GameObjectGenerator
{
    [SerializeField]
    protected List<Enemy> enemyTypeList = new List<Enemy>();
    protected override void Initialise()
    {
        base.Initialise();
    }
    protected override void Generate()
    {
        
    }
}
