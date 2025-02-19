using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageSpike : MonoBehaviour
{
    public int Damages;
    float _elapsedTime;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if( _elapsedTime > 0.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
