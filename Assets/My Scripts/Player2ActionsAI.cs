using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ActionsAI : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public float FlipSpeed = 0.8f;
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    public float PunchSlideAmt = 2f;
    public float HeavyReactAmt = 4f;
    private bool HeavyMoving = false;
    private bool HeavyReact = false;
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    public static bool HitsAI = false;
    public static bool FlyingJumpAI = false;
    public static bool Dazed = false;
    private int AttackNumber = 1;
    private bool Attacking = true;
    public float AttackRate = 1.0f;
    public float DazedTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        MyPlayer = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //Heavy Punch Slide
        if (HeavyMoving == true)
        {
            if (Player2MoveAI.FacingRightAI == true)
            {


                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player2MoveAI.FacingLeftAI == true)
            {


                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }

        //Heavy React Slide
        if (HeavyReact == true)
        {
            if (Player2MoveAI.FacingRightAI == true)
            {


                Player1.transform.Translate(-HeavyReactAmt * Time.deltaTime, 0, 0);
            }
            if (Player2MoveAI.FacingLeftAI == true)
            {


                Player1.transform.Translate(HeavyReactAmt * Time.deltaTime, 0, 0);
            }
        }


        //Listen to the Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Standing Attacks
        if (Player1Layer0.IsTag("Motion"))
        {
            if (Attacking == true)
            {
                Attacking = false;


                if (AttackNumber == 1)
                {
                    Anim.SetTrigger("LightPunch");
                    HitsAI = false;
                }
                if (AttackNumber == 2)
                {
                    Anim.SetTrigger("HeavyPunch");
                    HitsAI = false;
                }
                if (AttackNumber == 3)
                {
                    Anim.SetTrigger("LightKick");
                    HitsAI = false;
                }
                if (AttackNumber == 4)
                {
                    Anim.SetTrigger("HeavyKick");
                    HitsAI = false;
                }
                if (Input.GetButtonDown("BlockP2"))
                {
                    Anim.SetTrigger("BlockOn");
                }
            }

        }

        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("BlockP2"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }

        //Crouching attack
        if (Player1Layer0.IsTag("Crouching"))
        {
                Anim.SetTrigger("LightKick");
                HitsAI = false;
                Anim.SetBool("Crouch", false);
        }
        //Aerial Moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                HitsAI = false;
            }
        }

    }

    public void JumpUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
    }

    public void HeavyMove()
    {
        StartCoroutine(PunchSlide());
        
    }

    public void RandomAttack()
    {
       
       
        AttackNumber = Random.Range(1, 5);
        StartCoroutine(SetAttacking());
      
    }

    public void HeavyReaction()
    {
        
        StartCoroutine(HeavySlide());
        AttackNumber = 3;

    }


    public void FlipUp()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
        FlyingJumpAI = true;

    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, FlipSpeed, 0);
        FlyingJumpAI = true;

    }

    public void IdleSpeed()
    {
        FlyingJumpAI = false;
    }

    public void KickWooshSound()
    {
        MyPlayer.clip = KickWoosh;
        MyPlayer.Play();
    }

    public void PunchWooshSound()
    {
        MyPlayer.clip = PunchWoosh;
        MyPlayer.Play();
    }

    IEnumerator PunchSlide()
    {
        HeavyMoving = true;
        yield return new WaitForSeconds(0.1f);
        HeavyMoving = false;

    }

    IEnumerator HeavySlide()
    {
        HeavyReact = true;
        Dazed = true;
        yield return new WaitForSeconds(0.3f);
        HeavyReact = false;
        yield return new WaitForSeconds(DazedTime);
        Dazed = false;

    }

    IEnumerator SetAttacking()
    {
        yield return new WaitForSeconds(AttackRate);
        Attacking = true;
    }
}