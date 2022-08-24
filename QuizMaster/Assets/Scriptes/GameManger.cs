using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    Quiz quiz;
    EndSceen endSceen;
    // Start is called before the first frame update
    void Start()
    {
        quiz = GetComponent<Quiz>();
        endSceen = GetComponent<EndSceen>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
