using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assignments
{
    public List<Routes> routes = new List<Routes>();
    public GameObject[] listOfAllCities;
    public Dictionary<string, Neighbour> nameNeighbourDict = new Dictionary<string, Neighbour>();
    public void Initialize()
    {
        listOfAllCities = GameObject.FindGameObjectsWithTag("Cities");
        InitializeDictionary();


        var count = (int)Cities.Germany.Count;
        for (int i = 1; i < count; i++)
        {
            for (int j = 1; j < count; j++)
            {
                if (i != j)
                {
                    var routeName = NameOfCity(i) +
                    " - " + NameOfCity(j);

                    routes.Add(new Routes(routeName,
                        GetCity(NameOfCity(i)),
                        GetCity(NameOfCity(j)),
                        1000,
                        Random.Range(1,7)));
                }
                
            }
        }
        routes = Fisher_Yates_CardDeck_Shuffle(routes);
        
    }

    Neighbour GetCity(string name)
    {
        return nameNeighbourDict[name];
    }

    void InitializeDictionary()
    {
        var count = (int)Cities.Germany.Count;
        

        for (int j = 1; j < count; j++)
        {
            var name = ((Cities.Germany)j).ToString();
            nameNeighbourDict.Add(name, GetNeighbourComponent(name));
        }
    }

    Neighbour GetNeighbourComponent(string name)
    {
        for (int i = 0; i < listOfAllCities.Length; i++)
        {
            if (name.Equals(listOfAllCities[i].name))
            {
                return listOfAllCities[i].GetComponent<Neighbour>();
            }
        }
        return null;
    }

    string NameOfCity(int count)
    {
        return ((Cities.Germany)count).ToString();
    }

    public List<Routes> Fisher_Yates_CardDeck_Shuffle(List<Routes> aList)
    {

        System.Random _random = new System.Random();

        Routes myGO;

        int n = aList.Count;
        for (int i = 0; i < n; i++)
        {
            // NextDouble returns a random number between 0 and 1.
            // ... It is equivalent to Math.random() in Java.
            int r = i + (int)(_random.NextDouble() * (n - i));
            myGO = aList[r];
            aList[r] = aList[i];
            aList[i] = myGO;
        }

        return aList;
    }

    public Routes DealCard()
    {
        var card = routes[0];
        routes.RemoveAt(0);
        return card;
    }

    public void PutCardBack(Routes routes)
    {
        this.routes.Add(routes);
    }
}
