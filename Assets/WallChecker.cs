using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public platforme cell { get; set; }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall") { cell.SetBoosted(false); Destroy(gameObject); }
    }


}
