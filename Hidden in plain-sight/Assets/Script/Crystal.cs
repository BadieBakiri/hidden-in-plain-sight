using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour
{
    // Start is called before the first frame update
    public int position;
    int _currentMana;

    public Sprite FullCrystal, EmptyCrystal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentMana = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Mana;
        if (_currentMana >= position)
        {
            GetComponent<Image>().sprite = FullCrystal;
            //GetComponent<Image>().color = Color.cyan;
        }
        else
        {
            GetComponent<Image>().sprite = EmptyCrystal;
            //GetComponent<Image>().color = Color.gray;
        }
    }

    void UpdateStatus()
    {
        _currentMana = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Mana;
        if (_currentMana >= position)
        {
            GetComponent<Image>().color = Color.blue;
        }
        else
        {
            GetComponent<Image>().color = Color.gray;
        }
    }
}
