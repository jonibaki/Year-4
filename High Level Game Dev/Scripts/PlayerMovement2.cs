
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour
{
    private AnimatorIDs ids;
    public GameManagement gm;
    private Animator animator;
    public AudioSource audio;
    public AudioClip audioToplay;

    private float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
        ids = GameObject.FindGameObjectWithTag("GameController").GetComponent<AnimatorIDs>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && speed < 5) speed += 0.05f;
        else if (speed > 1) speed -= 0.1f;

        if (speed < 1) speed = 1;

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat(ids.Speed, vertical * speed);
        animator.SetFloat(ids.TurnSpeed, horizontal * speed);
    }
    //Collide with objectives of the map
    void OnTriggerEnter(Collider collision)
    {

        //score adds up, life increases and deactives objectives
        if (collision.gameObject.tag == "Objectives")
        {
            //audio.Play();
            collision.gameObject.SetActive(false);
            //Destroy(collision.gameObject);
       
            gm.score++;
            gm.life++;
            gm.lifeText.text = "Live : " + gm.life.ToString();
            gm.scoreText.text = gm.score.ToString();
        }

    }
}
