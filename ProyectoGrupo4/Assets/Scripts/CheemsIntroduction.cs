using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheemsIntroduction : MonoBehaviour
{

    public GameObject player;
    public GameObject reflection;
    private Vector3 position;
    private Vector3 positionR;
    bool doThis;
    // Start is called before the first frame update
    void Start()
    {
        doThis = true;
        position = new Vector3(-8.84f,-9.24f,0.0f);
        positionR = new Vector3(9.15f,-9.24f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(player && reflection && doThis)
        {
            player.transform.position += new Vector3(0,0.5f,0);
            reflection.transform.position += new Vector3(0,0.5f,0);
        }

        if (player && reflection)
        {
            if(player.transform.position == position && reflection.transform.position == positionR)
            {
                doThis = false;
            }
        }
    }
}
