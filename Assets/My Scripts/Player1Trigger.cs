using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Trigger : MonoBehaviour
{
    public Collider Col;
  

    // Update is called once per frame
    void Update()
    {
        if(Player1Actions.Hits == false)
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
        if(other.gameObject.CompareTag("Player2"))
        {
            Player1Actions.Hits = true;
        }
    }
}
