using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobberBehaviour : MonoBehaviour
{
    BehaviourTree tree;
    public GameObject diamond;
    public GameObject van;
    public GameObject frontDoor;
    public GameObject backDoor;
    NavMeshAgent agent;

    public enum ActionState
    {
        IDLE,
        WORKING
    };
    ActionState state = ActionState.IDLE;

    [Range(0, 1000)]
    public int money = 800;

    BNode.Status treeStatus = BNode.Status.RUNNING;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        tree = new BehaviourTree();
        Sequence steal = new Sequence("Steal Something");
        Leaf hasGotMoney = new Leaf("Has Got Money", HasMoney);
        Leaf goToFrontDoor = new Leaf("Go To Front Door", GoToFrontDoor);
        Leaf goToBackDoor = new Leaf("Go To Back Door", GoToBackDoor);
        Leaf goToDiamond = new Leaf("Go To Diamond", GoToDiamond);
        Leaf goToVan = new Leaf("Go To Van", GoToVan);
        Selector openDoor = new Selector("Open Door");

        openDoor.AddChild(goToFrontDoor);
        openDoor.AddChild(goToBackDoor);

        steal.AddChild(hasGotMoney);
        steal.AddChild(openDoor);
        steal.AddChild(goToDiamond);
        //steal.AddChild(goToBackDoor);
        steal.AddChild(goToVan);
        tree.AddChild(steal);

        tree.PrintTree();
    }

    public BNode.Status HasMoney()
    {
        if (money >= 500)
            return BNode.Status.FAILURE;

        return BNode.Status.SUCCESS;
    }

    public BNode.Status GoToDiamond()
    {
        BNode.Status s =  GoToLocation(diamond.transform.position);
        if (s == BNode.Status.SUCCESS)
        {
            diamond.transform.parent = this.gameObject.transform;
        }
        return s;
    }

    public BNode.Status GoToVan()
    {
        BNode.Status s = GoToLocation(van.transform.position);
        if (s == BNode.Status.SUCCESS)
        {
            money += 300;
            diamond.SetActive(false);
        }
        return s;
    }

    public BNode.Status GoToDoor(GameObject door)
    {
        BNode.Status s = GoToLocation(door.transform.position);
        if(s == BNode.Status.SUCCESS)
        {
            if (!door.GetComponent<Lock>().isLocked)
            {
                door.SetActive(false);
                return BNode.Status.SUCCESS;
            }
            return BNode.Status.FAILURE;
        }
        else
        {
            return s;
        }
    }

    public BNode.Status GoToFrontDoor()
    {
        return GoToDoor(frontDoor);
    }

    public BNode.Status GoToBackDoor()
    {
        return GoToDoor(backDoor);
    }

    BNode.Status GoToLocation(Vector3 destination)
    {
        float distanceToTarget = Vector3.Distance(destination, this.transform.position);
        if(state == ActionState.IDLE)
        {
            agent.SetDestination(destination);
            state = ActionState.WORKING;
        }
        else if(Vector3.Distance(agent.pathEndPosition, destination) >= 2)
        {
            state = ActionState.IDLE;
            return BNode.Status.FAILURE;
        }
        else if(distanceToTarget < 2)
        {
            state = ActionState.IDLE;
            return BNode.Status.SUCCESS;
        }

        return BNode.Status.RUNNING;
    }

    // Update is called once per frame
    void Update()
    {
        if(treeStatus != BNode.Status.SUCCESS)
        {
            treeStatus = tree.Process();
        }
    }
}
