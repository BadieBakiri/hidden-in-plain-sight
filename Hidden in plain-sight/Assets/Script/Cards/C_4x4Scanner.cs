using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_4x4Scanner : MonoBehaviour, ICard 
{
    [SerializeField]
    private GameObject m_AreaPrefab;
    [SerializeField]
    private GameObject m_EffectPrefab;

    private GameObject m_Area;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Use the card if you press down the button
        {
            if (m_Area != null)
            {
                Use();
            }
        }
    }
    public void Use()
    {
        foreach(GameObject children in m_Area.GetComponent<AreaControl>().GetAllChildren())
        {
            Instantiate(m_EffectPrefab, children.transform.position,Quaternion.identity);
        }
        UnPick();
        Destroy(this.gameObject);
    }

    public void Pick()
    {
        m_Area = Instantiate(m_AreaPrefab);
    }

    public void UnPick()
    {
        Destroy(m_Area);

    }
}
