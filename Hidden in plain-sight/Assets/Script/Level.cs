using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    public GameObject[] _nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetAllChildren().Length == 0)
        {
            foreach (GameObject next in _nextLevel)
            {
                    next.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }

    public GameObject[] GetAllChildren()
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in this.transform)
        {
            children.Add(child.gameObject);
        }

        return children.ToArray();
    }
}
