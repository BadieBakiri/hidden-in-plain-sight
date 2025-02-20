using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    [SerializeField]
    GameObject LosingScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        LosingScreen.SetActive(true);
        LosingScreen.GetComponent<LosinScreen>().Appear();
        //Debug.Log("Mission Failed");
    }
}
