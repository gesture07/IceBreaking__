using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d_PlayerName : MonoBehaviour
{

    public GameObject Cam;

    Vector3 startScale;
    public float distance = 3;

    // Start is called before the first frame update
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(Cam.transform.position, transform.position);
        Vector3 newScale = startScale * dist / distance;
        transform.localScale = newScale;
        transform.rotation = Cam.transform.rotation;
    }
}
