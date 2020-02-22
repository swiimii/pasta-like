using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressTracker : MonoBehaviour
{
    public int numberOfRooms;

    private void Start()
    {
        numberOfRooms = 1;
    }
    
    public void AddRoom(Vector2 position, int fromDirection, GameObject room)
    {
        numberOfRooms += 1;
        room.GetComponent<RoomData>().roomNumber = numberOfRooms;
        room.GetComponent<RoomData>().doorHitboxes[fromDirection].SetActive(false);
    }
}
