using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public Material player1Color;
    public Material player2Color;

    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;
    [SerializeField]
    private List<LayerMask> playerLayers;

    private PlayerInputManager playerInputManager;
    GameObject carBody;

    private void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }


    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    public void AddPlayer(PlayerInput player)
    {
        players.Add(player);

        //need to use the parent due to the structure of the prefab
        Transform playerParent = player.transform.parent;
        playerParent.position = startingPoints[players.Count - 1].position;

        ////convert layer mask (bit) to an integer 
        int layerToAdd = (int)Mathf.Log(playerLayers[players.Count - 1].value, 2);

        //set the layer
        playerParent.GetComponentInChildren<CinemachineVirtualCamera>().gameObject.layer = layerToAdd;
        //add the layer
        playerParent.GetComponentInChildren<Camera>().cullingMask |= 1 << layerToAdd;


        // Set the color of the player

        if (players.Count == 1)
        {
            player.GetComponentInChildren<MeshRenderer>().material = player1Color;

            foreach (Transform t in player.transform)
            {
                if (t.gameObject.tag == "Player")
                {
                    t.gameObject.tag = "Player1";
                }
            }
            //player.GetComponentInChildren<GameObject>().gameObject.tag = "Player1";
        }

        if (players.Count == 2)
        {
            player.GetComponentInChildren<MeshRenderer>().material = player2Color;

            foreach (Transform t in player.transform)
            {
                if (t.gameObject.tag == "Player")
                {
                    t.gameObject.tag = "Player2";
                }
            }
            //player.GetComponentInChildren<GameObject>().gameObject.tag = "Player2";
        }
    }

}
