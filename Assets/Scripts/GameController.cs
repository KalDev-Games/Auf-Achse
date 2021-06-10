using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameObject[] players;
    public static int playerID = 0;

    public Assignments assignments = new Assignments();
    public static ActionCardsController actionCards = new ActionCardsController();
    public static CurrentPlayer player = new CurrentPlayer();
    public Animator _animator;
    public static Animator animator;
    private static GameController instance;

    public GUIController controllerForGUI;


    public MoveTruck truckEngine;
    void Start()
    {
        instance = this;
        animator = _animator;
        player = new CurrentPlayer();
        players = GameObject.FindGameObjectsWithTag("Player");
        assignments.Initialize();
        actionCards.Initialize();

        ChangePlayer(controllerForGUI, false);
        truckEngine = player.player.truckEngine;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                var temp = assignments.DealCard();
                players[j].GetComponent<RouteController>().assignments.Add(temp);
                players[j].GetComponent<RouteController>().assignment = assignments;
                players[j].GetComponent<RouteController>().UpdateRouteInfo();
                print(temp.routeName);
                //players[i].GetComponent<MoveTruck>().currentPosition = players[i].GetComponent<RouteController>().assignments[0].startingPoint;
                //players[i].GetComponent<MoveTruck>().origin = players[i].GetComponent<RouteController>().assignments[0].destinationPoint;
            }
        }
        controllerForGUI.UpdateGUI();
        controllerForGUI.stopRound.onClick.AddListener(delegate { truckEngine.EndMoveHere(); ChangePlayer(controllerForGUI, true); });
    }

    public static void ChangePlayer(GUIController controller, bool increaseID)
    {
        Debug.Log(players[playerID].name);

        Transform playerCam = players[playerID].transform.GetChild(0);

        if (playerCam != null)
        {
            playerCam.transform.gameObject.SetActive(false);
        }




        if (increaseID)
        {
            playerID++;
            if (playerID >= players.Length)
            {
                playerID = 0;
            }
        }
        
        
        Debug.Log(playerID);
        var currentPlayer = players[playerID].GetComponent<RouteController>();
        player.assignments = currentPlayer.assignments;
        player.money = players[playerID].GetComponent<PlayerController>().money;
        player.nameOfTeam = players[playerID].GetComponent<PlayerController>().teamName;
        player.player = players[playerID].GetComponent<PlayerController>();
        player.player.truckEngine = players[playerID].GetComponent<MoveTruck>();
        playerCam = players[playerID].transform.GetChild(0);
        playerCam.transform.gameObject.SetActive(true);
        player.player.truckEngine.UpdateFields(player.player.truckEngine.currentPosition.neighbours);
        controller.displayIcon.sprite = player.player.icon;
        controller.displayTeamName.text = player.player.teamName;

        controller.UpdateGUI();
        instance.StartCoroutine(DelayGame(3));

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return)&& player.player.truckEngine.steps == 0)
        {
            player.player.truckEngine.RollDice();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && player.player.truckEngine.steps > 0)
        {
            var nextField = player.player.truckEngine.GetFieldByIndex(player.player.truckEngine.index);
            player.player.truckEngine.Moving(nextField);

            if (player.player.truckEngine.steps == 0)
            {
                player.player.truckEngine.UpdateFields(player.player.truckEngine.currentPosition.neighbours);
                ChangePlayer(controllerForGUI, true);
            }
        }

        if (player.player.truckEngine.possibleFields.Count >= 2 && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            player.player.truckEngine.index--;

            if (player.player.truckEngine.index < 0)
            {
                player.player.truckEngine.index = player.player.truckEngine.possibleFields.Count - 1;
            }
            player.player.truckEngine.newDestination = player.player.truckEngine.GetFieldByIndex(player.player.truckEngine.index);
            player.player.truckEngine.RotateIndicator(player.player.truckEngine.newDestination.transform);

        }
        else if (player.player.truckEngine.possibleFields.Count >= 2 && Input.GetKeyUp(KeyCode.RightArrow))
        {
            player.player.truckEngine.index++;

            if (player.player.truckEngine.index == player.player.truckEngine.possibleFields.Count)
            {
                player.player.truckEngine.index = 0;
            }

            player.player.truckEngine.newDestination = player.player.truckEngine.GetFieldByIndex(player.player.truckEngine.index);
            player.player.truckEngine.RotateIndicator(player.player.truckEngine.newDestination.transform);
        }


    }

    static IEnumerator DelayGame(int transTime)
    {
        animator.SetBool("playerIsChanged", true);
        yield return new WaitForSeconds(transTime);
        animator.SetBool("playerIsChanged", false);
    }

}

public struct CurrentPlayer{
    public string nameOfTeam;
    public int money;
    public List<Routes> assignments;
    public PlayerController player;

}
