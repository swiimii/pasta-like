using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int direction;
    public string type;
    public GameObject container;
    public bool locked = false;

    [SerializeField] GameObject roomPrefab, hallwayPrefab;
    private float placementOffset = 1.76f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!locked && collision.gameObject.layer.Equals(LayerMask.NameToLayer("Player")))
        {
            Vector3 newPosition = container.transform.position + Vector3.right * placementOffset;

            if (type == "Room")
            {
                // create hallway
                container.GetComponent<RoomData>().doorHitboxes[direction].SetActive(false);
                var hallway = Instantiate(hallwayPrefab);
                hallway.transform.position = new Vector3(newPosition.x, newPosition.y, hallway.transform.position.z);

                // show menu
                var state = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
                var menu = state.ShowPlayerInteractionUI();
                menu.SetActive(true);
                menu.GetComponent<PlayerInteractionMenu>().room = hallway.GetComponent<LockedRoom>();
            }

            if(type == "Hallway")
            {
                // create room
                var room = Instantiate(roomPrefab);
                room.GetComponent<RoomData>().doorHitboxes[(direction + 2)%4].SetActive(false);
                room.transform.position = new Vector3(newPosition.x, newPosition.y, room.transform.position.z);

            }
            
            gameObject.SetActive(false);


        }
    }

}
