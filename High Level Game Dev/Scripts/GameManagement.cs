using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
   
    public class GameManagement : MonoBehaviour
    {
        public PlayerMovement pm;
        public PlayerMovement2 pm2;
        public GameObject player;
        public int life = 100;
        public  int score;
        public Text scoreText, speedCount, levelReached, lifeText, countDownText;
        public Button playButton, pauseButton, loadButton, quitButton;
        public Animator anim;
        public float currentT;
        public float startT=5f;
        // Start is called before the first frame update
        void Start()
        {
            currentT = startT;
            playButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            levelReached.enabled = false;
            score = 0;
    
           
        }

        // Update is called once per frame
        void Update()
        {
            countDownText.text = currentT.ToString();
            if (currentT >= 0)
            {
                currentT -= 1 * Time.deltaTime;
                countDownText.text = currentT.ToString();
            }
            else {
                countDownText.gameObject.SetActive(false);
                Intro();
            }

            if (pm != null) {
                if (pm.speed != 0)
                {

                    speedCount.text = "Speed : " + pm.updateSpeed.ToString();

                }

            }
        
        } //end of Update 

        public void OnButtonPlay() {
            Time.timeScale = 1f;
            pauseButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            loadButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);


        }
        public void OnButtonPause()
        {
            //player.GetComponent<PlayerMovement>().enabled = false;
            //anim.SetBool("run", false);
            Time.timeScale = 0f;
            pauseButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
            loadButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
        public void OnButtonLoad()
        {  
            SceneManager.LoadScene(1); 
            Time.timeScale = 1f;
      
        }
        public void OnButtonQuit()
        {
            Application.Quit();
        }


        void Intro() {
            if (pm != null) {
                player.GetComponent<PlayerMovement>().enabled = true;
                anim.SetBool("run", true);

            }
  
            
        }
    }
}
