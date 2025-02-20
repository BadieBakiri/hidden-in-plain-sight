using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StartAnimation : MonoBehaviour
{

    public Transform Object, Target;

    public float Duration;

    public GameObject _HUD;

    private bool _stupidBool = false;

    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _stupidBool == false)
        {
            _stupidBool=true;
            StartCoroutine(MoveLerp(Object, Target, Duration));
            _animator.Play("GameStarts");
        }
    }

    

    private IEnumerator MoveLerp(Transform obj, Transform target, float moveDuration)
    {
        float elapsed = 0f;
        Vector3 startPosition = obj.position;
        Quaternion startRotation = obj.rotation;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / moveDuration);
            obj.position = Vector3.Lerp(startPosition, target.position, t);
            obj.rotation = Quaternion.Lerp(startRotation, target.rotation, t);
            yield return null;
        }

        obj.position = target.position;
        obj.rotation = target.rotation;
        _HUD.SetActive(true);

        this.gameObject.SetActive(false);
    }
}
