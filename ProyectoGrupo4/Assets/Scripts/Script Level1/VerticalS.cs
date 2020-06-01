using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalS : MonoBehaviour
{
    private Vector2 iniPos;
    public float ampli;
    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = iniPos + new Vector2(0, Mathf.Sin(Time.time) * ampli);
    }
}
