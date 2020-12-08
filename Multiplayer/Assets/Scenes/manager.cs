 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;
public class manager : MonoBehaviour
{
    // Start is called before the first frame update
public GameObject fab;
   public CinemachineVirtualCamera cine;
    void Start()
    {
        spawn();

    }

    // Update is called once per frame
    void spawn()
{

var newt = PhotonNetwork.Instantiate("idle",fab.transform.position,fab.transform.rotation);
cine.Follow = newt.transform;

}
}