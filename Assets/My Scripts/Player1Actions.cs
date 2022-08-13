using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Actions : MonoBehaviour
{
    public float JumpSpeed = 1.0f;
    public GameObject Player1;
    private Animator Anim;
    private AnimatorStateInfo Player1Layer0;
    public float PunchSlideAmt = 2f;
    private bool HeavyMoving = false;
    private AudioSource MyPlayer;
    public AudioClip PunchWoosh;
    public AudioClip KickWoosh;
    public static bool Hits = false;


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
            if (Player1Move.FacingRight == true)
            {


                Player1.transform.Translate(PunchSlideAmt * Time.deltaTime, 0, 0);
            }
            if (Player1Move.FacingLeft == true)
            {


                Player1.transform.Translate(-PunchSlideAmt * Time.deltaTime, 0, 0);
            }
        }


        //Listen to the Animator
        Player1Layer0 = Anim.GetCurrentAnimatorStateInfo(0);

        //Standing Attacks
        if (Player1Layer0.IsTag("Motion"))
        {


            if (Input.GetButtonDown("Fire1"))
            {
                Anim.SetTrigger("LightPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                Anim.SetTrigger("HeavyPunch");
                Hits = false;
            }
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                Hits = false;
            }
            if (Input.GetButtonDown("Block"))
            {
                Anim.SetTrigger("BlockOn");
            }

        }

        if (Player1Layer0.IsTag("Block"))
        {
            if (Input.GetButtonUp("Block"))
            {
                Anim.SetTrigger("BlockOff");
            }
        }

        //Crouching attack
        if (Player1Layer0.IsTag("Crouching"))
        {
            if (Input.GetButtonDown("Fire3"))
            {
                Anim.SetTrigger("LightKick");
                Hits = false;
            }


        }
        //Aerial Moves
        if (Player1Layer0.IsTag("Jumping"))
        {
            if (Input.GetButtonDown("Jump"))
            {
                Anim.SetTrigger("HeavyKick");
                Hits = false;
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

    public void FlipUp()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        Player1.transform.Translate(0.1f, 0, 0);
    }
    public void FlipBack()
    {
        Player1.transform.Translate(0, JumpSpeed, 0);
        Player1.transform.Translate(-0.1f, 0, 0);
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
        yield return new WaitForSeconds(0.05f);
        HeavyMoving = false;

    }
}