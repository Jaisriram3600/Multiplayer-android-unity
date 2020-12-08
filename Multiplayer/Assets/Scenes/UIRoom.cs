using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class UIRoom : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
   public InputField createroom;
public InputField joinroom;
public InputField namee;


public void createbtn()
{

PhotonNetwork.CreateRoom(createroom.text,new RoomOptions{MaxPlayers = 4},null);



}


public void name()
{
 PhotonNetwork.NickName=namee.text;

}
public void joinbtn()
{

PhotonNetwork.JoinRoom(joinroom.text,null);

print("joined");
}

public override void OnJoinedRoom()
{
print("created");
PhotonNetwork.LoadLevel(1);
}

}
