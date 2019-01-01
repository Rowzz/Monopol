using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;

public class NetworkingController : MonoBehaviourPunCallbacks
{
    public GameController GameController;
    static string GAME_VERSION = "0.1.0";
    static byte MAX_PLAYERS = 4;
    private Player CurrentPlayer;
    

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;    
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = GAME_VERSION;
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        CurrentPlayer = PhotonNetwork.LocalPlayer;
        Debug.LogFormat($"Your id is {PhotonNetwork.LocalPlayer.UserId}");
        InstantiatePlayer();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room existed, creating new room");
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MAX_PLAYERS }, null);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat($"Player Joined with ID: {other.ActorNumber}"); 
    }

    public void InstantiatePlayer()
    {
        PlayerFigure Player = PhotonNetwork.Instantiate("Player", GameObject.Find("Start").transform.position, Quaternion.identity).GetComponent<PlayerFigure>();
        Player.gameController = GameController;
        Player.currentField = GameObject.Find("Start").GetComponent<FieldDefinition>();
        GameController.AddPlayer(Player);
    }
}
