using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class move : MonoBehaviour
{

    Rigidbody rb;
    Canvas canvas;

    public GameObject player;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI welcomeText;
    public Button restartButton;
    public Button startButton;
    public TextMeshProUGUI hpText;
    public GameObject[] gemArray;
    Vector3 firstPos, lastPos;
    public Camera cam;
    public Animator animator;
    public AudioSource audioSource;

    public Image hpBar;
    public Image hpRemain;
    public Image welcomeImage;
    public Button mute;

    public int hp = 100;
    public int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        canvas = GetComponent<Canvas>();
        audioSource = GetComponent<AudioSource>();

        Time.timeScale = 0;
        scoreText.text = "Score : " + score;

        for (int i = 0; i < 10; i++)
        {
            Instantiate(gemArray[Random.Range(0, 16)], new Vector3(Random.Range(-4.9f, 4.9f), 0.45f, Random.Range(3, 95)), Quaternion.LookRotation(Vector3.up));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            other.gameObject.SetActive(false);
            Destroy(other.gameObject, 2f);
            score++;
            scoreText.text = "Score : " + score;
        }


    }



    [System.Obsolete]
    void OnCollisionEnter(Collision collisionInfo)
    {


        if (collisionInfo.collider.tag == "Blocks")
        {
            hp -= 10;
            hpText.text = "Health : " + hp.ToString();
            hpBar.fillAmount = hp / 100f;
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (hp <= 0)
            {

                GameOverText();
            }
        }

        else if (collisionInfo.collider.tag == "Finish")
        {

            GameOverText();
        }
    }




    void FixedUpdate()
    {
        float yatay = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");

        Vector3 direction = new Vector3(yatay, 0, 10f);
        player.transform.Translate(direction * Time.deltaTime / 2f);
        player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -4.8f, 4.8f), player.transform.position.y, player.transform.position.z);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 6, player.transform.position.z - 5);
        cam.transform.rotation = Quaternion.Euler(30, 0, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * 3f, ForceMode.VelocityChange);

        }


    }

    public void StartGame()
    {

        Time.timeScale = 1;
        audioSource.mute = false;
        scoreText.gameObject.SetActive(true);
        hpBar.gameObject.SetActive(true);
        hpText.gameObject.SetActive(true);
        hpRemain.gameObject.SetActive(true);
        welcomeImage.gameObject.SetActive(false);
        welcomeText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        mute.gameObject.SetActive(true);



    }


    public void RestartGame()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GameOverText()
    {
        Time.timeScale = 0;
        gameOverText.text = "\n Game Over! \n Score: " + score;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        audioSource.mute = true;
        scoreText.gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
        hpText.gameObject.SetActive(false);
        hpRemain.gameObject.SetActive(false);
        welcomeImage.gameObject.SetActive(true);
        welcomeText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        mute.gameObject.SetActive(true);
    }

}
