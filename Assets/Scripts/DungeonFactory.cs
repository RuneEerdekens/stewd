using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFactory : MonoBehaviour
{
    public int maxLevels;
    public int maxChildCount;
    public List<GameObject> roomList;
    private Dungeon d;

    private void Start()
    {
        d =  new Dungeon(maxLevels, maxChildCount, roomList);
        d.PrintTree();
    }
}
