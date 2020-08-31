using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
//1. player moves according to the user input: 
//left arrow= move to the left
//right arrow = move to the right 
//space bar = jump the player
//ctrl = change the camera view of



    public class PlayerMovement : MonoBehaviour
    {
        #region Public Fields
        public float speed, sideSpeed,jumpForce, updateSpeed;
        public float sideForce,forwardForce;
        public Rigidbody player;
        public GameManagement gm;
    

        #endregion

        #region Private Fields
        private bool isGround;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            player = GetComponent<Rigidbody>();
            GetComponent<PlayerMovement>().enabled = false; // turn of the player script when the scene 1 loads
         }
         void OnCollisionStay(Collision col)
        {
            if (col.gameObject.tag == "Ground") {
            
                gm.anim.SetBool("jump", false);
                
                isGround = true;
            }
          
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)){
                Application.Quit();
            }
        }
        // Update is called once per frame
        void FixedUpdate()
        {          
          
            player.AddForce(0,0, forwardForce * speed * Time.deltaTime);
            updateSpeed =     forwardForce * speed * Time.deltaTime;


            //position to the right of the ground
            if (Input.GetKey(KeyCode.LeftArrow))
                {
                //gm.anim.SetBool("leftturn", true);
              
                player.AddForce(-sideForce*sideSpeed * Time.deltaTime, 0,0);

            }

                //position to the right of the ground
                if (Input.GetKey(KeyCode.RightArrow))
                {
                //gm.anim.SetBool("rightturn", true);
                player.AddForce(sideForce* sideSpeed * Time.deltaTime, 0, 0);
            }

                //player jump off the ground
                if (Input.GetKey(KeyCode.Space) && isGround )
                {
                gm.anim.SetBool("jump", true);

                player.AddForce(new Vector3(0,15,0) * jumpForce*Time.deltaTime, ForceMode.Impulse);
                isGround = false;
                }
            


        }
        //Collide with objectives of the map
        void OnTriggerEnter(Collider collision)
        {
          
            //score adds up, life increases and deactives objectives
            if (collision.gameObject.tag == "Objectives" && speed !=0   ) {
                collision.gameObject.SetActive(false);
                gm.score++;
                gm.life++;
                gm.lifeText.text = "Live : " + gm.life.ToString();
                gm.scoreText.text = gm.score.ToString();
            }
           
        }

        //Collision with Walls push the player in the oppsite aixes
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "LeftWalls")
            {

                player.AddForce(sideForce * sideSpeed * Time.deltaTime * 50, 0, 0);
            }
            if (collision.gameObject.tag == "RightWalls")
            {
                player.AddForce(-sideForce * sideSpeed * Time.deltaTime * 50, 0, 0);
            }


            if (collision.gameObject.tag == "Barriers")
            {
                //player.constraints = RigidbodyConstraints.None; //will make the player falls in all direction
           
                gm.life--;
                gm.lifeText.text = "Live : " + gm.life.ToString();
                gm.anim.SetBool("run", false);
                player.velocity = Vector3.zero;
                speed = 0;
            }

            if (collision.gameObject.tag == "Level1")
            {
        
                player.velocity = Vector3.zero;
                speed = 0;
                gm.anim.SetBool("run", false);
                gm.levelReached.enabled = true;
                SceneManager.LoadScene(2);
            }
        }
       
    }
}
