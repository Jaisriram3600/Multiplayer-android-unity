using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class ballmove : MonoBehaviourPun,IPunObservable{
    

    private Vector3 smoothMove;
Animator anim;
SpriteRenderer sr;
public PhotonView pv;
FixedJoystick joy;
private GameObject sceneCamera;
public GameObject playerCamera;
public Text textname;
public float speed;

float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
private void Awake()
{

}
void Start()


{



anim = GetComponent<Animator>();
sr = GetComponent<SpriteRenderer>();


if(photonView.IsMine)

{
sceneCamera= GameObject.Find("Main Camera");
joy = GameObject.FindWithTag("joystick").GetComponent<FixedJoystick>();
 textname.text=PhotonNetwork.NickName;
sceneCamera.SetActive(true);
playerCamera.SetActive(true);
}
else{

 textname.text=pv.Owner.NickName;


}
}
void move()
{


Vector3 hori= new Vector3((joy.Horizontal*speed),(joy.Vertical*speed),0f);
transform.position=transform.position+hori*Time.deltaTime;



if(hori.x>0)
{
sr.flipX=false;
pv.RPC("flipp",RpcTarget.Others);
}

else if(hori.x<0){
sr.flipX=true;
pv.RPC("flipp2",RpcTarget.Others);
}

if(hori.x >0||hori.y>0)
{
anim.SetBool("walk",true);
pv.RPC("walkanim",RpcTarget.Others);
}
else if (hori.x==0&& hori.y==0)
{
anim.SetBool("walk",false);
pv.RPC("stopanim",RpcTarget.Others);
}
else if(hori.x<0||hori.y<0)
{
anim.SetBool("walk",true);
pv.RPC("walkanim",RpcTarget.Others);
}



}








    void Update()
    {


if(photonView.IsMine)

{
move();

}


else

{

smoothMovement();

}



}

[PunRPC]
void walkanim()
{

anim.SetBool("walk",true);
}

[PunRPC]
void stopanim()
{

anim.SetBool("walk",false);
}



[PunRPC]
void flipp()
{

sr.flipX=false;
}

[PunRPC]
void flipp2()
{

sr.flipX=true;
}
 private void smoothMovement()
    {
  double timeToReachGoal = currentPacketTime - lastPacketTime;
   currentTime = currentTime+ Time.deltaTime;

        transform.position = Vector3.Lerp(positionAtLastPacket,smoothMove,(float)(currentTime / timeToReachGoal));
    }




 public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3) stream.ReceiveNext();
              currentTime = 0.0f;
            lastPacketTime = currentPacketTime;
            currentPacketTime = info.SentServerTime;
            positionAtLastPacket = transform.position;
        }

    }
}