using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveF : MonoBehaviour
{
<<<<<<< HEAD

    public int mov;

    private Vector2 iniPos;
    private Vector2 newPos;
    private Vector2 finalPos;
    private Vector2 referencePos;
    private Vector2 actualPos;

=======
    private Vector2 iniPos;
    public float mov;
>>>>>>> c27ea9eea33f1c9884eaa91877a6db9cec555965
    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        actualPos = transform.localPosition;
        newPos = new Vector2(Mathf.Sin(Time.time) * mov, 0);
        finalPos = iniPos + newPos;
        referencePos = finalPos - actualPos;

        if (referencePos.x > 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(referencePos.x < 0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        transform.localPosition = finalPos;
    }

}
