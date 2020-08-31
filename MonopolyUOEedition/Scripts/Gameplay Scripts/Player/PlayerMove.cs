using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Summary: The script does all the movement and game play decision of the game
namespace Monopoly
{
    public class PlayerMove : MonoBehaviourPun, IPunObservable
    {
        public PhotonView PV; //player owned photonview
        private Vector3 TargetPosition;
        private Quaternion TargetRotation;

        public int routePos;
        int currentPos;
        public int steps;
        bool isMoving;

        public Rigidbody player;

        public Vector3 previousPos;

        public GameManager gm;

        private void Awake()
        {
            player = GetComponent<Rigidbody>();
            gm = FindObjectOfType<GameManager>();

        }

        void Start()
        {
          
            PV = GetComponent<PhotonView>();
            previousPos = Vector3.forward;          

        }

        void Update()
        {
            // only monitor other players gameplay until it is your turn
            if (!BoardManagement.Instance.Turn() && !PV.IsMine) {

                transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.25f);
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, 500.0f * Time.deltaTime);
            }
            //Your turn to roll dice
            else
            { 
                if (BoardManagement.Instance.buttonPressed && !isMoving)
                {
                    Debug.Log(PV.GetComponent<PlayerProfile>().Money.ToString());

                    //coloured all property bought, if yes then buy house and pay for per house and substract money

                    //if pass Go, collect 200 to the player account

                    steps = BoardManagement.Instance.GetDiceNumber(); // get the dice value

                    StartCoroutine(Move());

                    //testing lines
                    //string p = BlockManager.Instance.childBlockList[currentPos].GetComponent<PropertyDetails>().PropertyOwner;

                    //if (p == "")
                    //{
                    //    p = PV.Owner.NickName;
                    //}

                    //get the board location if it has bought then player pay to owner based on house or railroad
                    //if it is owns by the owner upgrade 
                    //else buy the property, pay for it or else next player turn


                    //if it is community chest then draw random card and act upon and next player turn

                    //land on jail/three turn/draw go to jail card, then go to jail, then next player turn

                    //if it is tax property then pay tax and then next player turn 
                    BoardManagement.Instance.buttonPressed = false;
                }
            }
         

        }

        
        IEnumerator Move()
        {
            BoardManagement.Instance.rollButton.interactable = false;
            if (isMoving)
            {
                yield break;
            }
            isMoving = false;
            while (steps > 0)
            {
                routePos++;
                routePos %= BlockManager.Instance.childBlockList.Count; //set postion back to 0 if it go beyond last block
                Vector3 nextPos = BlockManager.Instance.childBlockList[routePos].position;
                nextPos.y = transform.position.y;

                while (MoveToNextBlock(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.1f);

                //check if it pass a go position
                if (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID == ObjectReferences.GO_ID &&
                    PV.GetComponent<PlayerManager>().hasPassOnce())
                {
                    //collect 200 from the bank
                    FindObjectOfType<AudioManager>().PlayAudio("Celebrate");
                    PV.GetComponent<PlayerProfile>().Money += ObjectReferences.GO_MONEY;
                    Debug.Log("Your player in GO position");

                }
                currentPos = routePos;
               // Decrement the Text in the panel as it move to the goal, a RPC can demonsrate across all the player

                steps--;
                gm.photonView.RPC("RPC_DecrementDice", RpcTarget.All, steps);
            }
           
            isMoving = false; //player stopped here
            //TODO: erase the dice numbe
            Debug.Log(isMoving.ToString());

            #region GAME LOGIC 

            //check if player is in a go position
            if (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID == ObjectReferences.GO_ID &&
                PV.GetComponent<PlayerManager>().hasPassOnce())
            {
                //collect 200 from the bank
                PV.GetComponent<PlayerProfile>().Money += ObjectReferences.GO_MONEY;
                FindObjectOfType<AudioManager>().PlayAudio("Celebrate");
                Debug.Log("Your player in GO position");

            }

            // check if the player in the  GO TO Jail position
            else if (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID == ObjectReferences.JAIL_ID)
            {
                //go to Jail
                Debug.Log("Your player in Jail position");
                steps = 20; //Give this value accurately
                //play sad audio music 
                yield return new WaitForSeconds(2f);
                yield return StartCoroutine(MoveBackward());
                //change the turn directly from here
            }


            //landed on unowned property; either buy it or move on
            else if (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID == ObjectReferences.CHANCE_ID ||
                     BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID == ObjectReferences.COMMUNITY_ID)
            {
                Debug.Log("Your player in CC position");
                BoardManagement.Instance.ShowCCDialougPanel(BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID);
            }
            
            //check if landed property has any owner
            else if (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyOwner != PV.ViewID &&
                BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyOwner !=0 &&
                    (BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID != ObjectReferences.CHANCE_ID ||
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID != ObjectReferences.COMMUNITY_ID))
            {
                Debug.Log(PV.Owner.NickName.ToString());
                Debug.Log(BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyOwner.ToString());
                Debug.Log("Your player in a Own Property position");
                //check for color and house

                //pay money accordingly;

                //no money? sell house //bankkrupt etc
            }
            
            //calls the property panel with appropiate information in the panel
            else
            {
                //ask if the player want to build in this property or skip the turn
                Debug.Log("Your player in a unImproved property Property position");
                BoardManagement.Instance.ShowPropertyCardPanel
                    (
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyName,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyID,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().PropertyPrice,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().mortgage,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent1,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent2,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent3,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent4,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().rent5,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().houseCost,
                    BlockManager.Instance.childBlockList[routePos].GetComponent<PropertyDetails>().hotelCost
                     );
                
            }

            #endregion

            BoardManagement.Instance.rollButton.interactable = true; //put this line somewhere that keep the button inactive for a while

            BoardManagement.Instance.endRollButton.gameObject.SetActive(true);
          
        }
  
        bool MoveToNextBlock(Vector3 goal)
        {
            currentPos = routePos;
            return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, ObjectReferences.PLAYER_SPEED * Time.deltaTime)); //delay between moving to next block;

        }

        // move player backward to the jail position
        IEnumerator MoveBackward()
        {
            if (isMoving)
            {
                yield break;
            }
            isMoving = false;
            while (steps > 0)
            {
                routePos--;
                routePos %= BlockManager.Instance.childBlockList.Count; //set postion back to 0 if it go beyond last block
                Vector3 nextPos = BlockManager.Instance.childBlockList[routePos].position;
                nextPos.y = transform.position.y;

                while (MoveToNextBlock(nextPos)) { yield return null; }

                yield return new WaitForSeconds(0.1f);
                steps--;
            }
        }

        //call back that occurs each frame and update local player and remote player's data across the network
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
                stream.SendNext(BoardManagement.Instance.turnNumber);
            }
            else
            {
                TargetPosition = (Vector3)stream.ReceiveNext();
                TargetRotation = (Quaternion)stream.ReceiveNext();
                BoardManagement.Instance.turnNumber = (int)stream.ReceiveNext();

            }

        }

        public static void RefershInstance(ref PlayerMove playerMove, PlayerMove Prefab, GameObject SpawnSpot, Player player)
        {
            var rotation = Quaternion.identity;
            Vector3 position = SpawnSpot.transform.position;
            if (playerMove != null)
            {
                position = playerMove.transform.position;
                rotation = playerMove.transform.rotation;
                PhotonNetwork.Destroy(playerMove.gameObject);
            }
    
            playerMove = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation, 0).GetComponent<PlayerMove>();
            Debug.Log(playerMove.GetComponent<PhotonView>().ViewID.ToString());
            GameManager.RegisterPlayer(player, playerMove);
            //become the child of the spawn point 
            //player.transform.SetParent(g.transform, false);
        }
    }
}
