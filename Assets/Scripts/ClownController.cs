using System.Collections;
using System.Collections.Generic;

using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClownController : MonoBehaviour
{
    NavMeshAgent agent;
    private new Camera camera;
    private Animator animator;
    private bool isInitialized = false;
    private bool isOver = false;
    private bool isOverScreen = false;

    public GameObject playerCamera, player, gameOverScreen, gameOverText;
    public AudioSource appearanceSound, nearSound, footStepSound;

    void Start()
    {
        camera = Camera.main;
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        playerCamera = GameObject.Find("Main Camera");
        player = GameObject.Find("Capsule");
        gameOverScreen = GameObject.Find("GameOver");
        gameOverText = GameObject.Find("GameOverText");
        appearanceSound = GameObject.Find("ClownAppearEffect").GetComponent<AudioSource>();
        nearSound = GameObject.Find("ClownNearEffect").GetComponent<AudioSource>();
        footStepSound = GameObject.Find("ClownFootStepEffect").GetComponent<AudioSource>();

        gameOverScreen.SetActive(false);
        gameOverText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOver)
        {

            if (isInitialized && !appearanceSound.isPlaying && !nearSound.isPlaying && IsPlayerInNear(12.0f))
            {
                footStepSound.Stop();
                nearSound.Play();
            }

            if (isInitialized && !appearanceSound.isPlaying && !nearSound.isPlaying && !footStepSound.isPlaying)
            {
                footStepSound.Play();
            }

            if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Lying")) && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up")))
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Walk"))
                {
                    agent.speed = 1;
                }
                else
                {
                    agent.speed = 3.5f;
                }

                agent.destination = camera.transform.position;
            }

            if(isInitialized && IsPlayerInNear(2.5f))
            {
                KillPlayer();
            }
        }
        if(isOverScreen)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    public void Init()
    {
        isInitialized = true;
        animator.Play("Stand Up", -1, 0);
        appearanceSound.Play();
    }

    bool IsPlayerInNear(float threshold)
    {
        if ((playerCamera.transform.position - transform.position).magnitude < threshold)
            return true;
        else return false;
    }

    void KillPlayer()
    {
        isOver = true;

        agent.speed = 0;
        animator.Play("HeadButt");

        nearSound.Stop();
        footStepSound.Stop();
        appearanceSound.Play();

        PlayerCam script = player.GetComponent<PlayerCam>();
        script.SetLookSensitivity(0);
        script.SetWalkSpeed(0);

        camera.transform.LookAt(this.transform.position - new Vector3(0, (this.transform.position - camera.transform.position).y, 0));
        camera.transform.position += 0.4f * camera.transform.forward;
        player.transform.LookAt(this.transform.position - new Vector3(0, (this.transform.position - camera.transform.position).y, 0));

        Invoke("GameOver", 4.0f);
        

    }

    public void Die()
    {
        isOver = true;
        agent.speed = 0;
        nearSound.Stop();
        footStepSound.Stop();
        appearanceSound.Stop();

        animator.Play("die");
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverText.SetActive(true);
        gameOverScreen.GetComponent<RawImage>().canvasRenderer.SetAlpha(0.1f);
        Invoke("SetOverScreen", 3.0f);
        gameOverScreen.GetComponent<RawImage>().CrossFadeAlpha(1f, 3f, false);
    }

    void SetOverScreen()
    {
        isOverScreen = true;
    }
}