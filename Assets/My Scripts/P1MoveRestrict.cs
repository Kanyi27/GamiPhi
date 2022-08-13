using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1MoveRestrict : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.WalkRight = false;
        }
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.WalkLeft = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("P2Left"))
        {
            Player1Move.WalkRight = true;
        }
        if (other.gameObject.CompareTag("P2Right"))
        {
            Player1Move.WalkLeft = true;
        }
    }
}
