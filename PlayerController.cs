using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public GameObject[] lanes;
    public float movementSpeed = 10.0f; 
    private int currentLane = 1;
    public TMPro.TMP_Text redEnergyText;
    public TMPro.TMP_Text greenEnergyText;
    public TMPro.TMP_Text blueEnergyText;
    public TMPro.TMP_Text scoreText;
    private int redEnergy=0;
    private int greenEnergy=0;
    private int blueEnergy=0;
    private int score=0;
    private string form = "white"; 
    private bool greenActivated=false;
    private bool blueActivated = false;
    public GameObject shield;
    public GameObject canvas;
    public GameObject gameOverCnvas;
    public Button resume;
    public Button restart;
    public Button restart1;
    public Button menu;
    public Button menu1;
    public TMPro.TMP_Text finalScore;
    private bool escaped = false;
    private bool gameOver = false;
    //public AudioClip start0;
    //public AudioClip game0;
    public AudioSource game;
    public AudioSource start;
    public AudioSource action;
    public AudioSource error;
    private void Start()
    {
        scoreText.text = "Score: 0";
        redEnergyText.text = "Red Points: 0";
        greenEnergyText.text = "Green Points: 0";
        blueEnergyText.text = "Blue Points: 0";
        Button resu = resume.GetComponent<Button>();
        Button resr = restart.GetComponent<Button>();
        Button men = menu.GetComponent<Button>();
        Button resr1 = restart1.GetComponent<Button>();
        Button men1 = menu1.GetComponent<Button>();
        resu.onClick.AddListener(onResuClick);
        resr.onClick.AddListener(onRestClick);
        men.onClick.AddListener(onMenClick);
        resr1.onClick.AddListener(onRestClick);
        men1.onClick.AddListener(onMenClick);
       // game = GetComponent<AudioSource>();
        game.Play();
        
    }
    void onResuClick()
    {
        escaped = false;
        Time.timeScale = 1f;
        canvas.SetActive(false);
        game.Play();
        start.Stop();

    }
    void onRestClick()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
    void onMenClick()
    {
        SceneManager.LoadScene("titleScreen");
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        Renderer playerRenderer = GetComponent<Renderer>();
        if (Input.GetKeyDown(KeyCode.J)&& redEnergy==5)
        {
            action.Play();
            Material material = playerRenderer.material;
            material.color = Color.red;
            redEnergyText.text = "Red Points: " + (--redEnergy);
            form = "red";
            greenActivated = false;
            blueActivated = false;
            shield.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.J) && redEnergy != 5)
        {
            error.Play();
        }

        if (Input.GetKeyDown(KeyCode.K) && greenEnergy == 5)
        {
            action.Play();
            Material material = playerRenderer.material;
            material.color = Color.green;
            greenEnergyText.text = "Green Points: " + (--greenEnergy);
            form = "green";
            blueActivated = false;
            shield.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.K) && greenEnergy != 5)
        {
            error.Play();
        }
        if (Input.GetKeyDown(KeyCode.L) && blueEnergy == 5)
        {
            action.Play();
            Material material = playerRenderer.material;
            material.color = Color.blue;
            blueEnergyText.text = "Blue Points: " + (--blueEnergy);
            form = "blue";
            greenActivated = false;
        }
        if (Input.GetKeyDown(KeyCode.L) && blueEnergy != 5)
        {
            error.Play();
        }
            if (Input.GetKeyDown(KeyCode.Space) && form == "red")
        {
            redEnergyText.text = "Red Points: " + (--redEnergy);
            action.Play();
            if (redEnergy == 0)
                revertToNormal();
           
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag("obstacle");

                foreach (GameObject obj in objectsWithTag)
                {
                    Object.DestroyImmediate(obj, true);
                }
                greenActivated = false;
                blueActivated = false;
                shield.SetActive(false);
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && form == "green"&&!greenActivated)
        {
            action.Play();
            greenEnergyText.text = "Green Points: " + (--greenEnergy);
            if (greenEnergy == 0)
                revertToNormal();
            else
            {
                greenActivated = true;
                blueActivated = false;
                shield.SetActive(false);
            }
            

        }
        if (Input.GetKeyDown(KeyCode.Space) && form == "blue"&&!blueActivated)
        {
            action.Play();
            blueEnergyText.text = "Blue Points: " + (--blueEnergy);
            if (blueEnergy == 0)
                revertToNormal();
            else
            {
                blueActivated = true;
                shield.SetActive(true);
                greenActivated = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.Space) && form == "white")
        {
            error.Play();
        }
            if (Input.GetKeyDown(KeyCode.Escape) && !gameOver )
        {
            if (!escaped)
            {
                start.Play();
                game.Stop();
                escaped = true;
                Time.timeScale = 0f;
                canvas.SetActive(true);
            }
            else
                onResuClick();

        }

    }

    private void MoveLeft()
    {
        if (currentLane > 0)
        {
            currentLane--;
            MoveToLane(currentLane);
        }
    }

    private void MoveRight()
    {
        if (currentLane < 2)
        {
            currentLane++;
            MoveToLane(currentLane);
        }
    }

    private void MoveToLane(int targetLane)
    {

        float targetX = lanes[targetLane].transform.position.x;
        Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        transform.position = newPosition;

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision "+collision.gameObject);
        if (collision.gameObject.CompareTag("redOrb"))
        {
            action.Play();
            Debug.Log("score before " + score);
            if (form == "red" )
            {
                score += 2;
                scoreText.text = "Score: " + (score);
            }
            else if (greenActivated)
            {
                score += 5;
                redEnergy += 2;
                if (redEnergy > 5)
                    redEnergy = 5;
                scoreText.text = "Score: " + (score);
                redEnergyText.text = "Red Points: " + (redEnergy);
                greenActivated = false;

            }
           /*else if (form=="green")
            {
                score += 5;
                redEnergy += 2;
                if (redEnergy > 5)
                    redEnergy = 5;
                scoreText.text = "Score: " + (score);
                redEnergyText.text = "Red Points: " + (redEnergy);

            }*/
            else
            {
                scoreText.text = "Score: " + (++score);
                if (redEnergy < 5 && form != "red")
                    redEnergyText.text = "Red Points: " + (++redEnergy);
            }
            Object.Destroy(collision.gameObject);
            Debug.Log("score after " + score);
        }
        if (collision.gameObject.CompareTag("greenOrb"))
        {
            action.Play();
            Debug.Log("score before " + score);
            if (form == "green" && !greenActivated)
            {
                score += 2;
                scoreText.text = "Score: " + (score);
            }
            else 
            if (greenActivated)
            {
                score += 10;
               /* greenEnergy *= 2;
                if (greenEnergy > 5)
                    greenEnergy = 5;*/
                scoreText.text = "Score: " + (score);
                greenEnergyText.text = "Green Points: " + (greenEnergy);
                greenActivated = false;

            }

            else
            {
                scoreText.text = "Score: " + (++score);
                if (greenEnergy < 5 && form != "green")
                    greenEnergyText.text = "Green Points: " + (++greenEnergy);
            }
            Object.Destroy(collision.gameObject);
            Debug.Log("score after " + score);
        }
        if (collision.gameObject.CompareTag("blueOrb"))
        {
            action.Play();
            Debug.Log("score before " + score);
            if (form == "blue" )
            {
                score+=2;
                scoreText.text = "Score: " + (score);
            }

            else if (greenActivated)
            {
                score += 5;
                blueEnergy += 2;
                if (blueEnergy > 5)
                    blueEnergy = 5;
                scoreText.text = "Score: " + (score);
                blueEnergyText.text = "Blue Points: " + (blueEnergy);
                greenActivated = false;

            }
          /*  else if (form == "green")
            {
                score += 5;
                blueEnergy += 2;
                if (blueEnergy > 5)
                    blueEnergy = 5;
                scoreText.text = "Score: " + (score);
                blueEnergyText.text = "Blue Points: " + (blueEnergy);

            }*/
            else
            {
                scoreText.text = "Score: " + (++score);
                if (blueEnergy < 5 && form != "blue")
                    blueEnergyText.text = "Blue Points: " + (++blueEnergy);
            }
            Object.Destroy(collision.gameObject);
            Debug.Log("score after " + score);
        }
        if (collision.gameObject.CompareTag("obstacle"))
        {
            error.Play();
            if (form != "white" &&!blueActivated)
            {
                Debug.Log("call revert1");
                Object.Destroy(collision.gameObject);
                revertToNormal();
                greenActivated = false;
            }
            else if (blueActivated)
            {
                Debug.Log("call revert2");
                Object.Destroy(collision.gameObject);
                blueActivated = false;
                shield.SetActive(false);
            }
            else
            {
                gameOver = true;
                Time.timeScale = 0f;
                gameOverCnvas.SetActive(true);
                finalScore.text = "Final Score: " + score;
                game.Stop();
                start.Play();
            }

        }
    }
    private void revertToNormal()
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        
            Material material = playerRenderer.material;
            material.color = Color.white;
            form = "white";
        Debug.Log("reverted herer");
    }
}

