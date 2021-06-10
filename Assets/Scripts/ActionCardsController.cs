using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCardsController
{
    public List<int> listOfActionCards = new List<int>();
    private ActionCards cards = new ActionCards();

    public void Initialize()
    {
        for (int i = 0; i < cards.amountOfCards; i++)
        {
            listOfActionCards.Add(i);
        }

        listOfActionCards = Fisher_Yates_CardDeck_Shuffle(listOfActionCards);
    }

    public void TriggerActionCard()
    {
        Debug.Log(listOfActionCards.Count);
        cards.DoActionCard(listOfActionCards[0], GameController.player.player);
        listOfActionCards.Add(listOfActionCards[0]);
        listOfActionCards.RemoveAt(0);
    }

    private List<int> Fisher_Yates_CardDeck_Shuffle(List<int> aList)
    {

        System.Random _random = new System.Random();

        int myGO;

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
}
