using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTruck : MonoBehaviour
{
    // Start is called before the first frame update
    public Neighbour currentPosition;
    public Neighbour origin;

    public List<Neighbour> possibleOrigins = new List<Neighbour>();
    public List<bool> possibleOriginsStatus = new List<bool>();
    public List<Neighbour> possibleDestinations = new List<Neighbour>();
    public List<bool> possibleDestinationStatus = new List<bool>();

    public int maxFields;

    public List<Neighbour> possibleFields = new List<Neighbour>();
    public Neighbour newDestination;

    public RouteController routeController;
    public Cargo cargoController;

    public Transform indicator;

    public int index = 0;
    public int steps = 0;



    void Start()
    {
        origin = currentPosition;
        origin.isPassable = false;
        UpdateFields(currentPosition.neighbours);
        newDestination = possibleFields[0];
        RotateIndicator(newDestination.transform);
    }


    public void UpdateFields(Neighbour[] neighbours)
    {
        possibleFields.Clear();

        for (int i = 0; i < neighbours.Length; i++)
        {
            if (neighbours[i].isPassable)
            {
                possibleFields.Add(neighbours[i]);
            }
            
        }

        for (int i = 0; i < possibleFields.Count; i++)
        {
            if (possibleFields[i] == origin)
            {
                possibleFields.RemoveAt(i);
                break;
            }
        }
    }

    // Update is called once per frame
    public void RollDice()
    {
        System.Random randDice = new System.Random();
        steps = randDice.Next(1, 6);
        print(name + " has " + steps + " left");
        
    }


    public Neighbour GetFieldByIndex(int index)
    {
        return possibleFields[index];
    }

    public void Moving(Neighbour newDestination)
    {
        origin.isPassable = true;
        origin = currentPosition;

        var destination = new Vector3(newDestination.transform.position.x, transform.position.y, newDestination.transform.position.z);
        transform.position = destination;
        currentPosition = newDestination;

        if (currentPosition.triggerForActionCards)
        {
            GameController.actionCards.TriggerActionCard();
        }

        index = 0;
        UpdateFields(currentPosition.neighbours);
        if (possibleFields.Count > 0)
        {
            RotateIndicator(possibleFields[0].transform);
        }

        steps--;
        origin.isPassable = false;
    }


    public void EndMoveHere()
    {
        CheckForLoading();
        steps = 0;
    }
    private void CheckForLoading()
    {
        print(currentPosition);
        if (possibleOrigins.Contains(currentPosition) &&
            !possibleOriginsStatus[GetAssignmentID(possibleOrigins,currentPosition)]
            && cargoController.currentLoaded + routeController.assignments[GetAssignmentID(possibleOrigins, currentPosition)].cargo <= cargoController.capacity)
        {
            possibleOriginsStatus[GetAssignmentID(possibleOrigins, currentPosition)] = true;
            cargoController.LoadCargo(routeController.assignments[GetAssignmentID(possibleOrigins, currentPosition)].cargo);
        }
        CheckForUnloading();
    }

    private void CheckForUnloading()
    {
        if (possibleDestinations.Contains(currentPosition) &&
            possibleOriginsStatus[GetAssignmentID(possibleDestinations, currentPosition)])
        {
            print("Abladen");
            int ID = GetAssignmentID(possibleDestinations, currentPosition);
            cargoController.RemoveCargo(routeController.assignments[ID].cargo);

            GetComponent<PlayerController>().money += routeController.assignments[ID].reward;

            print(routeController.assignments.Count);
            possibleDestinations.RemoveAt(ID);
            possibleDestinationStatus.RemoveAt(ID);
            possibleOrigins.RemoveAt(ID);
            possibleOriginsStatus.RemoveAt(ID);

            routeController.assignment.PutCardBack(routeController.assignments[ID]);
            routeController.assignments.RemoveAt(ID);
        }
    }

    int GetAssignmentID(List<Neighbour> fields, Neighbour field)
    {
        for (int i = 0; i < fields.Count; i++)
        {
            if (field == fields[i])
            {
                return i;
            }
        }
        return -1;
    }


    public void RotateIndicator(Transform target)
    {
        var dir = transform.position - target.position;
        Quaternion LookAtRotation = Quaternion.LookRotation(dir);

        Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y - 90, transform.rotation.eulerAngles.z);
        transform.rotation = LookAtRotationOnly_Y;
    }
}
