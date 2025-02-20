using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class C_AreaCard : MonoBehaviour, ICard 
{
    [SerializeField]
    private TMPro.TMP_Text m_Text;
    [SerializeField]
    private GameObject m_AreaPrefab;
    [SerializeField]
    private GameObject m_EffectPrefab;

    private GameObject m_Area;

    [SerializeField]
    private int _cost;

    private int _mana;

    [SerializeField]
    AudioSource _cardPick, _playSound;

    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        this.transform.rotation = Quaternion.Euler(0, 0, 2);

        
    }

    void Update()
    {
        m_Text.text = _cost.ToString();

        if (Input.GetMouseButtonDown(1)) //Use the card if you press down the button
        {
            UnPick();
        }

        if (Input.GetMouseButtonDown(0)) //Use the card if you press down the button
        {
            _mana = _gameManager.Mana;
            if (m_Area != null && m_Area.GetComponent<AreaControl>().PointsBoard)
            {
                if (_mana >= _cost)
                {
                    _gameManager.RemoveMana(_cost);
                    Use();
                }
                else
                {
                    UnPick();
                }
            }
            else
            {
                UnPick();
            }
        }
    }
    public void Use()
    {
        foreach(GameObject children in m_Area.GetComponent<AreaControl>().GetAllChildren())
        {
            Instantiate(m_EffectPrefab, children.transform.position,Quaternion.identity);
        }

        Debug.Log(this.gameObject.name);

        if (this.gameObject.name.Contains("SatelliteScan")) //If the card you use is a satellite scan, reset the numbers
        {
            _gameManager.PlaneCount = 0;
            _gameManager.InfantryCount = 0;
            _gameManager.TankCount = 0;
            _gameManager.PanelAnim.AnimatePanel();
            Debug.Log("Reset");
        }

        _playSound.Play();


        UnPick();

        this.transform.position = new Vector3(1000, 2000, 3000); //This is the worst code i've ever written lmfao
        this.transform.parent = null;
        Invoke("DestroyThisShit", 2);

        //Destroy(this.gameObject);
    }

    public void Pick()
    {
        m_Area = Instantiate(m_AreaPrefab);
        _cardPick.Play();
    }

    void DestroyThisShit()
    {
        Destroy(this.gameObject); // <-- Destroys this shit 
    }

    public void UnPick()
    {
        Destroy(m_Area);
    }
}
