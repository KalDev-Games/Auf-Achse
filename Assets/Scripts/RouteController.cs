using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Routes> assignments = new List<Routes>();
    public MoveTruck moveTruck;
    public Assignments assignment;

    public void UpdateRouteInfo()
    {

        moveTruck.possibleOrigins.Clear();
        moveTruck.possibleDestinations.Clear();
        moveTruck.possibleOriginsStatus.Clear();
        moveTruck.possibleDestinationStatus.Clear();

        for (int i = 0; i < assignments.Count; i++)
        {
            moveTruck.possibleOrigins.Add(assignments[i].startingPoint);
            moveTruck.possibleOriginsStatus.Add(false);
            
            moveTruck.possibleDestinations.Add(assignments[i].destinationPoint);
            moveTruck.possibleDestinationStatus.Add(false);
        }
    }
}
