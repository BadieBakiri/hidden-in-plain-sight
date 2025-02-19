using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaControl : MonoBehaviour
{
    public LayerMask hitLayers; // Set this to the layers you want the ray to interact with
    private GameObject[] _tiles;
    public bool PointsBoard;

    private void Start()
    {
        _tiles = GetAllChildren();
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
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitLayers))
        {
            PointsBoard = true;
            foreach (GameObject child in GetAllChildren())
            {
                child.SetActive(true);
            }

            if (_tiles.Length % 2 == 0)
            {
                hit.point -= new Vector3(hit.point.x % 1, 0, hit.point.z % 1); //This is terrible trial and error code lmao wtf is this
                this.transform.position = hit.point + new Vector3(0.5f, 0, 0.5f);
            }
            else
            {
                hit.point -= new Vector3((hit.point.x - 0.5f) % 1, 0, (hit.point.z - 0.5f) % 1); //This is terrible trial and error code lmao wtf is this
                this.transform.position = hit.point + new Vector3(0.5f, 0, 0.5f);
            }
        }
        else
        {
            PointsBoard = false;
            foreach (GameObject child in GetAllChildren())
            {
                child.SetActive(false);
            }
        }
    }
}
