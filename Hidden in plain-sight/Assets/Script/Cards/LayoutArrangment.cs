using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutArrangment : MonoBehaviour
{
    public HorizontalLayoutGroup Layout;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int sub = 10 * GetAllChildren().Length;
        if (sub > 110) sub = 110;
        Layout.spacing = 20 - sub;
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
