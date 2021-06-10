using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCards
{
    public int amountOfCards = 2;



    public void DoActionCard(int cardID, PlayerController playerController)
    {
        switch (cardID)
        {
            case 1:
                playerController.money += 1000;
                break;
            case 2:
                if (playerController.cargo.capacity > 6)
                {
                    playerController.cargo.capacity = 6;
                }
                break;
            default:
                return;
        }
    }
}
