using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
    public int roomNumber;
    public Vector2 roomPosition;

    public GameObject[] doors;

    public GameObject[] doorHitboxes; //right, up, left, down
}
