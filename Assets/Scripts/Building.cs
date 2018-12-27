using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int ID;
    public GameObject Next;
    public GameObject Before;

    // Start is called before the first frame update
    void Start()
    {
        
        Debug.Log(ID.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
