using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class d_DisplayUserId : MonoBehaviour
{

    public Text userId;
    private PhotonView pv = null;

    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        userId.text = pv.Owner.NickName;    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
