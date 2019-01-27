using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluntObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 3f;

    private bool travelling = false;

    private void Update()
    {
        if (travelling)
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }
    public void Shoot()
    {
        this.travelling = true;
    }
}
