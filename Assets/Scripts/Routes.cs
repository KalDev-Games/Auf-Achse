using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Routes
{
    public Neighbour startingPoint;
    public Neighbour destinationPoint;

    public int reward;
    public int cargo;
    public int[] deals;
    public string routeName;
    public Routes(string nameOfRoute, Neighbour origin, Neighbour destination, int reward, int cargo)
    {
        startingPoint = origin;
        destinationPoint = destination;

        routeName = nameOfRoute;
        this.cargo = cargo;
        this.reward = reward;

    }
}
