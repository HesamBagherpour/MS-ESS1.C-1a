using System.Collections;
using System.Collections.Generic;
using Runtime.Manager;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Win(800);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
