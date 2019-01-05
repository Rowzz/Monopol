using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

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
        Vector3 StartField = GameObject.Find("Start").transform.position;
        StartField.y += 1; // Adjust the Spawning poisition to 1 above ground, prevents falling threw
        StartField.x += 1.5f;

        PlayerFigure Player = PhotonNetwork.Instantiate("Player", StartField, Quaternion.Euler(-90f, 90f, 0)).GetComponent<PlayerFigure>();
        Player.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        int ID = Player.GetComponent<PhotonView>().OwnerActorNr;

        GameController.AddPlayer(Player, ID);
        SendData(Player.ID, 1, false);
        SendData(null, 3, false);
    }

    public static void SendData(object Data, byte Code, bool toAll)
    {
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = toAll ? ReceiverGroup.All : ReceiverGroup.Others }; // You would have to set the Receivers to All in order to receive this event on the local client as well
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(Code, Data, raiseEventOptions, sendOptions);
    }
}
