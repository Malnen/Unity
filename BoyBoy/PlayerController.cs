using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    bool executing = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!executing)
        {
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {
        executing = true;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Game.instance.camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (ButtonComponent.buttonClicked)
                {
                    yield return new WaitForSeconds(0.1f);
                }
                else
                {
                    navMeshAgent.SetDestination(hit.point);
                }
            }
        }
        executing = false;
        yield return 0;
    }
    public void playerWarp(Vector3 destination)
    {
        navMeshAgent.Warp(destination);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<RoomDetection>() != null)
        {
            Room room = other.gameObject.GetComponent<RoomDetection>().getRoom();
            if (room != Room.currentRoom)
            {
                Maze.getInstance().changeRoom(room);
            }
        }
    }   
}
