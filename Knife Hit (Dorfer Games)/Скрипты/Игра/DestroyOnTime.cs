using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    public float Time;
    void Start()
    {
        StartCoroutine("DestroyTime");
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(Time);
        Destroy(gameObject);
    }

}
