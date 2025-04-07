using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public bool clockWise;
    public int speed; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (clockWise) transform.Rotate(new Vector3 (0, speed , 0) * Time.deltaTime);
        else transform.Rotate(new Vector3 (0, -speed , 0) * Time.deltaTime);
    }
}
