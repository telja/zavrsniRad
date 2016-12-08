using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class packmanController : MonoBehaviour {

    
    public GameObject packman;
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    public GameObject ghost4;
    public GameObject ghost5;
    public GameObject ghostBusters;
    public GameObject ghostBuster;
    public GameObject bonusTime;
    public GameObject bonusLife;
    // public AudioClip death;
    public AudioClip numnum;
    public AudioClip eatGhost;
    public AudioClip eatBonus;
    AudioSource audio;
    public float movementSpeed = 0f;
    public Text countObjects;
    public Text time;
    public Text winText;
    public Text lifes;
    public Button rightMove;
    public Button mainMenu;
    public Button playAgain;
    public Text pause;
    public bool paused;
    private Vector3     up = new Vector3(0, 0, 0),
                        right = new Vector3(0, 90, 0),
                        down = new Vector3(0, 180, 0),
                        left = new Vector3(0, 270, 0),
                        currentDirection = new Vector3(0, 0, 0);
    Animator anim;
    private int count;
    public float timeLeft;
    private Rigidbody rb;
    private GameObject[] ghostColor;
    private Color color;
    int lifeCount;
    int tempTime;
    float bonusTimeLeft;
    bool isMoving;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        count = 0;
        timeLeft = 299;
        mainMenu.gameObject.SetActive(false);
        playAgain.gameObject.SetActive(false);
        ghostBusters.gameObject.SetActive(false);
        ghostBuster.gameObject.SetActive(false);
        bonusTime.gameObject.SetActive(false);
        bonusLife.gameObject.SetActive(false);
        audio = GetComponent<AudioSource>();
        lifeCount = 1;
        lifes.text = lifeCount.ToString();
    }
	
	// Update is called once per frame
	void Update () {

        pausePc();
        //pausePhone();
        if (paused)
        {
            Time.timeScale = 0;
            pause.text = "Paused";
            mainMenu.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pause.text = "";
            mainMenu.gameObject.SetActive(false);
        }
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        if (count == 254)
        { 
            finalText(0);
        }
        if (timeLeft < 1 && winText.text!="You Win!")
        {
            finalText(1);
        }
        
        tempTime = (int)Mathf.Round(timeLeft);
        time.text = "Time left: " + tempTime.ToString()+" seconds";
        lifes.text = "Lifes: " + lifeCount.ToString();
        if (tempTime % 30 == 0 && ghostBusters.active==false && ghostBuster.active==false && bonusTime.active==false && bonusLife.active==false)
        {
            ghostBusters.transform.position = randomPosition();
            ghostBusters.gameObject.SetActive(true);

            ghostBuster.transform.position = randomPosition();
            ghostBuster.gameObject.SetActive(true);
          
            bonusTime.transform.position = randomPosition();
            bonusTime.gameObject.SetActive(true);
           
            bonusLife.transform.position = randomPosition();
            bonusLife.gameObject.SetActive(true);

            bonusTimeLeft = 10;
        }
       
        if (bonusTimeLeft >= 0 && bonusTimeLeft <= 10)
        {
            bonusTimeLeft -= Time.deltaTime;
        }
        else
        {
            ghostBuster.gameObject.SetActive(false);
            ghostBusters.gameObject.SetActive(false);
            bonusTime.gameObject.SetActive(false);
            bonusLife.gameObject.SetActive(false);
        }
        if (ghost1.active == false || ghost2.active == false || ghost3.active == false || ghost4.active == false || ghost5.active == false)
        {
            if (bonusTimeLeft < 0)
            {
                ghostStatus(true);
                changeGhostColor(Color.green);
            }
        }

        isMoving = true;
        anim.enabled = true;
        //playerMoveAcceleration();
        playerMovePc();



        transform.localEulerAngles = currentDirection;
        if (isMoving)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
            anim.enabled = false;
        transform.Rotate(new Vector3(0, 90, 0));

        if (lifeCount < 1 && winText.text!="You Win!")
        {
            finalText(1);
        }
        if (winText.text == "You Win!")
        {
            ghostStatus(false);
        }
        if (packman.gameObject.transform.position.y < 0)
        {
            lifeCount--;
            packman.transform.position = new Vector3(0, 7, 0);
        }
    }
    /// //////////////////////////////////////////////////////////////////////////
    
    void finalText(int final)
    {
        if (final == 0)
        {
            winText.text = "You Win!";
            winText.color = Color.blue;
            mainMenu.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            
        }
        else
        {
            winText.text = "Game Over!";
            winText.color = Color.red;
            mainMenu.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            lifeCount = 0;
            packman.gameObject.SetActive(false);
        }
       
            
    }
    void ghostStatus(bool status)
    {
            ghost1.gameObject.SetActive(status);
            ghost2.gameObject.SetActive(status);
            ghost3.gameObject.SetActive(status);
            ghost4.gameObject.SetActive(status);
            ghost5.gameObject.SetActive(status);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            audio.PlayOneShot(numnum);
            other.gameObject.SetActive(false);
            count++;
            countObjects.text = "Count " + count.ToString()+"/254";
        }
        if (other.gameObject.CompareTag("ghost"))
        {
            int count=0;
            ghostColor = GameObject.FindGameObjectsWithTag("ghostColor");
            foreach (GameObject ghost in ghostColor)
            {
                if (ghost.GetComponent<Renderer>().material.color == Color.black )
                {
                    count++;
                }
            }
            if (count != 0)
            {
                bonusTimeLeft = 10;
                audio.PlayOneShot(eatGhost);
                if (other.gameObject == ghost1)
                {
                    ghost1.SetActive(false);
                }
                if (other.gameObject == ghost2)
                {
                    ghost2.SetActive(false);
                }
                if (other.gameObject == ghost3)
                {
                    ghost3.SetActive(false);
                }
                if (other.gameObject == ghost4)
                {
                    ghost4.SetActive(false);
                }
                if (other.gameObject == ghost5)
                {
                    ghost5.SetActive(false);
                }
            }
            else
            {
                lifeCount--;
                if(lifeCount>0)
                packman.transform.position=new Vector3(0,7,0);
            }
            
        }
        if (other.gameObject.CompareTag("ghostBusters"))
        {
            bonusTimeLeft = 10;
            ghostStatus(false);
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);

        }
        if (other.gameObject.CompareTag("ghostBuster"))
        {
            bonusTimeLeft = 10;
            other.gameObject.SetActive(false);
            changeGhostColor(Color.black);
            audio.PlayOneShot(eatBonus);
        }
        if (other.gameObject.CompareTag("timeBonus"))
        {
            bonusTimeLeft = 10;
            timeLeft += 50;
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);
        }
        if (other.gameObject.CompareTag("lifeBonus"))
        {
            bonusTimeLeft = 10;
            other.gameObject.SetActive(false);
            audio.PlayOneShot(eatBonus);
            lifeCount++;

        }
    }
    void changeGhostColor(Color col)
    {
        ghostColor = GameObject.FindGameObjectsWithTag("ghostColor");
        foreach (GameObject ghost in ghostColor)
        {
            ghost.GetComponent<Renderer>().material.color = col;
        }
    }
    Vector3 randomPosition()
    {
        float x = Random.Range(-110, 110);
        float z = Random.Range(-110, 110);
        Vector3 randomPosition = new Vector3(x, 4, z);
        return randomPosition;
    }
    void playerMoveAcceleration()
    {

        
        float x = Input.acceleration.x;
        float z = Input.acceleration.z;
        float y = Input.acceleration.y;
        
         if (y > 0)
                currentDirection = up;
         if (x > 0.07f && (y > 0.01f || y < 0.01f))
                currentDirection = right;
         if (x < -0.07f && (y > 0.01f || y < 0.01f))
                currentDirection = left;
         if (y < 0)
                currentDirection = down;
        
      
    }
    void playerMovePc()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentDirection = up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDirection = right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentDirection = down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDirection = left;
        }
         else
             isMoving = false;
    }
    void pausePc()
    {
        if (Input.GetKey(KeyCode.P))
        {
            paused = !paused;
        }
    }
    void pausePhone()
    {
        if (Input.touchCount > 0)
        {
            paused = !paused;
        }
    }
  
    }

