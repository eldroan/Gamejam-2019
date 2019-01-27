using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluntObject : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;

    private bool travelling = false;

    private void Update()
    {
        if (travelling)
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }
    public void Shoot()
    {
        this.travelling = true;

        Destroy(this.gameObject, 4f);
    }
}
