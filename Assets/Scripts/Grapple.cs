using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] private float pullSpeed = 0.5f;
    [SerializeField] private float stopDistance = 4f;
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform shootTransform;

    private Hook hook;
    private bool isPulling;
    private Rigidbody rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        isPulling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hook == null && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            isPulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this,shootTransform);
            StartCoroutine(DestroyHookAfterLifetime());
        }
    }

    public void StartPull()
    {
        isPulling = true;
    }

    private void DestroyHook()
    {
        if (hook == null) return;

        isPulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }

    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(8f);
        
        DestroyHook();
    }
}
