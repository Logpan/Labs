using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(KillCat());
    }

    IEnumerator KillCat()
    {
        yield return new WaitForSeconds(0);

        Destroy(gameObject, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
