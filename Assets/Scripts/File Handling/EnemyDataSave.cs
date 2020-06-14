using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class EnemyDataSave
{
    public int id;
    public EnemyType type;

    public float positionX;
    public float positionY;

    public EnemyDataSave(Enemy enemy)
    {
        id = enemy.id;

        positionX = enemy.transform.position.x;
        positionY = enemy.transform.position.y;

        type = enemy.type;
    }
}

