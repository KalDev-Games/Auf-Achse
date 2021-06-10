using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour
{
    [Header("Auftrag 1")]
    public Text displayStartOfTask1;
    public Text displayDestinationOfTask1;
    public Text displayRewardOfTask1;
    public Text displayCargoOfTask1;

    [Header("Auftrag 2")]
    public Text displayStartOfTask2;
    public Text displayDestinationOfTask2;
    public Text displayRewardOfTask2;
    public Text displayCargoOfTask2;

    [Header("Auftrag 3")]
    public Text displayStartOfTask3;
    public Text displayDestinationOfTask3;
    public Text displayRewardOfTask3;
    public Text displayCargoOfTask3;

    [Header("Auftrag 4")]
    public Text displayStartOfTask4;
    public Text displayDestinationOfTask4;
    public Text displayRewardOfTask4;
    public Text displayCargoOfTask4;

    [Header("Auftrag 5")]
    public Text displayStartOfTask5;
    public Text displayDestinationOfTask5;
    public Text displayRewardOfTask5;
    public Text displayCargoOfTask5;

    [Header("Animation")]
    public Text displayTeamName;
    public Image displayIcon;

    [Header("Rounds")]
    public Button stopRound;
    public void UpdateGUI()
    {
        ResetText(displayStartOfTask1, displayDestinationOfTask1, displayRewardOfTask1, displayCargoOfTask1);
        ResetText(displayStartOfTask2, displayDestinationOfTask2, displayRewardOfTask2, displayCargoOfTask2);
        ResetText(displayStartOfTask3, displayDestinationOfTask3, displayRewardOfTask3, displayCargoOfTask3);
        ResetText(displayStartOfTask4, displayDestinationOfTask4, displayRewardOfTask4, displayCargoOfTask4);
        ResetText(displayStartOfTask5, displayDestinationOfTask5, displayRewardOfTask5, displayCargoOfTask5);
        for (int i = 0; i < GameController.player.assignments.Count; i++)
        {
            if (i == 0) {
                SetData(displayStartOfTask1, displayDestinationOfTask1, displayRewardOfTask1, displayCargoOfTask1, 0);
            }
            if (i == 1) {
                SetData(displayStartOfTask2, displayDestinationOfTask2, displayRewardOfTask2, displayCargoOfTask2, 1);
            }
            if (i == 2){
                SetData(displayStartOfTask3, displayDestinationOfTask3, displayRewardOfTask3, displayCargoOfTask3, 2);
            }
            if (i == 3){
                SetData(displayStartOfTask4, displayDestinationOfTask4, displayRewardOfTask4, displayCargoOfTask4, 3);
            }
            if (i == 4){
                SetData(displayStartOfTask5, displayDestinationOfTask5, displayRewardOfTask5, displayCargoOfTask5, 4);
            }
        }

    }

    void SetData(Text a, Text b, Text c, Text d, int i)
    {
        a.text = GameController.player.assignments[i].startingPoint.name;
        b.text = GameController.player.assignments[i].destinationPoint.name;
        c.text = GameController.player.assignments[i].reward.ToString();
        d.text = GameController.player.assignments[i].cargo.ToString();
    }

    void ResetText(Text a, Text b, Text c, Text d)
    {
        a.text = "";
        b.text = "";
        c.text = "";
        d.text = "";
    }

}


