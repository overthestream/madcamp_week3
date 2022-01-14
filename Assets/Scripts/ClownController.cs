using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ClownController : MonoBehaviour
{
    NavMeshAgent agent;
    public new Camera camera;
    public Animator animator;
    private bool isInitialized = false;

    public GameObject player;
    public AudioSource appearanceSound, nearSound, footStepSound;

    void Start()
    {
        camera = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Main Camera");
        appearanceSound = GameObject.Find("ClownAppearEffect").GetComponent<AudioSource>();
        nearSound = GameObject.Find("ClownNearEffect").GetComponent<AudioSource>();
        footStepSound = GameObject.Find("ClownFootStepEffect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInNear(5.0f) && !isInitialized)
        {
            Init();
            isInitialized = true;   
        }

        if(isInitialized && !appearanceSound.isPlaying && !nearSound.isPlaying && IsPlayerInNear(10.0f))
        {
            nearSound.Play();
        }

        if (isInitialized && !appearanceSound.isPlaying && !nearSound.isPlaying && !footStepSound.isPlaying)
        {
            footStepSound.Play();
        }

        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("Lying")) && (!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up")))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie Walk"))
            {
                agent.speed = 1;
            }
            else
            {
                agent.speed = 3.5f;
            }
            agent.destination = camera.transform.position;
        }
        
    }

    void Init()
    {
        animator.Play("Stand Up", -1, 0);
        appearanceSound.Play();
    }

    bool IsPlayerInNear(float threshold)
    {
        if ((player.transform.position - transform.position).magnitude < threshold)
            return true;
        else return false;
    }
}
