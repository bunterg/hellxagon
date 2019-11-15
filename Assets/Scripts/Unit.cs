using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new Unit", menuName = "ScriptableObjects/Unit", order = 1)]
public class Unit : ScriptableObject
{
    public int ID;
    public Tile tile;
    public Sprite UnitIcon;
    public float HealthPoints;
    public float CurrentHealthPoints;
    public float AttackPoints;
    public Vector3Int[] AttackPattern;
    public Vector3Int[] MovePattern;
    

    public Vector3Int[] getMoveTilePositions(Vector3Int currentPosition)
    {
        return MovePattern.Where(m => m != Vector3Int.zero).Select(delta =>
        {
            Vector3Int position = currentPosition + delta;
            if (currentPosition.y % 2 != 0 && delta.y % 2 != 0)
            {
                position.x += 1;
            }
            return position;
        }).ToArray();
    }

    public Vector3Int[] getAttackTilePositions(Vector3Int currentPosition)
    {
        return AttackPattern.Where(m => m != Vector3Int.zero).Select(delta =>
        {
            Vector3Int position = currentPosition + delta;
            if (currentPosition.y % 2 != 0 && delta.y % 2 != 0)
            {
                position.x += 1;
            }
            return position;
        }).ToArray(); ;
    }

}
