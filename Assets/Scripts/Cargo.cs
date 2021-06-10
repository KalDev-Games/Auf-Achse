using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cargo : MonoBehaviour
{
    // Start is called before the first frame update
    public int capacity = 6;

    public int currentLoaded;

    public void LoadCargo(int amount)
    {
        if (capacity > currentLoaded + amount)
        {
            currentLoaded += amount;
        }
    }

    public void RemoveCargo(int amount)
    {
        currentLoaded -= amount;
    }

    public void IncreaseCapacity(int size)
    {
        if (capacity < 16)
        {
            capacity += size;
        }
    }

}
