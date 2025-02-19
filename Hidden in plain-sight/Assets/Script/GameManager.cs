using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Mana;

    private int _maxMana = 8;
    
    enum Turn { EnemyTurn, PlayerTurn};
    Turn _currentTurn;

    [SerializeField]
    GameObject[] _cards;

    [SerializeField]
    GameObject _hand;

    // Start is called before the first frame update
    void Start()
    {
        DrawNewCards(2); //Draws two cards at the beginning of the game
        _currentTurn = Turn.PlayerTurn;
    }
    public void RemoveMana(int amount)
    {
        Mana-=amount;
    }
    public void NextTurn()
    {
        if (_currentTurn == Turn.PlayerTurn) //Wait for input when it's player's turn
        {
            foreach(GameObject tomb in GameObject.FindGameObjectsWithTag("DeathMark"))
            {
                Destroy(tomb);
            }
            _currentTurn = Turn.EnemyTurn;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Mana > _maxMana)
        {
            Mana  = _maxMana;
        }
        if (_currentTurn == Turn.EnemyTurn) //Check for every enemy in the scene and call the function Start Turn at them
        {
            GameObject[] _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < _enemies.Length; i++)
            {
                _enemies[i].GetComponent<Enemy>().StartTurn();
            }
            _currentTurn = Turn.PlayerTurn;
            DrawNewCards(1); // Add a new card at the new turn
            Mana += 2; //Add a new mana point for the player
        }
    }

    void DrawNewCards(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject cardToDraw = _cards[Random.Range(0, _cards.Length)];

            Instantiate(cardToDraw, _hand.transform);
        }
    }
    
}
