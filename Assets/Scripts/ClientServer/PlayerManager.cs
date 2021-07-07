using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public string username = "";
    public int id;
    [SerializeField] TextMesh text;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = username;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
