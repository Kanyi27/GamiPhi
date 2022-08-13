using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Trigger : MonoBehaviour
{
    public Collider Col;
  

    // Update is called once per frame
    void Update()
    {
        if(Player2Actions.HitsP2 == false)
        {
            Col.enabled = true;
        }
        else
        {
            Col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player1"))
        {
            Player2Actions.HitsP2 = true;
        }
    }
}
