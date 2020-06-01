using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveV : MonoBehaviour
{

    public float up;

    private Vector2 iniPos;
    private Vector2 newPos;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = new Vector2(0, Mathf.Sin(Time.time) * up);
        transform.localPosition = iniPos + newPos;
    }
}
