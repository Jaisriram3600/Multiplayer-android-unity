using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NewBehaviourScript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
  
public GameObject connected;
public GameObject disconnected;


public void btn()

{
PhotonNetwork.ConnectUsingSettings();

}


public override void OnConnectedToMaster()

{
PhotonNetwork.JoinLobby();
}





public override void OnDisconnected(DisconnectCause cause)
{
disconnected.SetActive(true);
print(cause);
}

public override void OnJoinedLobby()
{


connected.SetActive(true);
}
}
