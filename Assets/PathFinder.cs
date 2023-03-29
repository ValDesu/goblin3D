
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    private Vector2 ActualCoord = new Vector2(0, 0);

    private Rigidbody m_RigidBody;
    private float m_Speed = 20f;
    private Vector3 Direction = new Vector3(0, 0, 0);
    private int maximum_size = 0;
    private bool destroy_active = true;
    private int wall_destroyed = 0;

    private List<GameObject> wall_to_destroy = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        Direction = Random.value > 0.5f ? new Vector3(1, 0, 0) : new Vector3(0, 0, 1); // gerer aussi les -1
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_RigidBody.MovePosition(transform.position + Direction * Time.deltaTime * m_Speed);
    }

    public void SetMaximum(int size) { maximum_size = size; }

    private void ChangeDirection()
    {
        
        List<Vector3> possible_directions = new List<Vector3>();
        List<float> weight_directions = new List<float>();

        possible_directions.Add(new Vector3(1, 0, 0));
        possible_directions.Add(new Vector3(-1, 0, 0));
        possible_directions.Add(new Vector3(0, 0, 1));
        possible_directions.Add(new Vector3(0, 0, -1));

        int index_last_move = 0;
        foreach(Vector3 d in possible_directions)
        {
            if(d == Direction * -1) { break; }
            index_last_move++;
        }

        weight_directions.Add(Random.value);
        weight_directions.Add(Random.value);
        weight_directions.Add(Random.value);
        weight_directions.Add(Random.value);


        if (ActualCoord.x == 0) { possible_directions[1] = possible_directions[0]; }
        if (ActualCoord.y == 0) { possible_directions[3] = possible_directions[2]; }
        if (ActualCoord.x == maximum_size) { possible_directions[0] = possible_directions[1]; }
        if (ActualCoord.y == maximum_size) { possible_directions[2] = possible_directions[3]; }

        possible_directions.RemoveAt(index_last_move);
        weight_directions.RemoveAt(index_last_move);


        float hw = 0;
        int iw = 0;
        int index = 0;

        foreach (float weight in weight_directions)
        {
            if (weight > hw) { hw = weight; iw = index; }
            index++;
        }


        
        Vector3 random_direction = possible_directions[iw];
        Direction = random_direction;

    }

    private void OnTriggerEnter(Collider other)
    {
 
        if(other.tag == "Wall") {
            if (destroy_active)
            {
                other.GetComponent<BoxCollider>().size = Vector3.zero; //triger wallchecker
                other.transform.position = new Vector3(0, 10, 0);
                other.GetComponent<MeshRenderer>().enabled = false;
                wall_to_destroy.Add(other.gameObject);

                //Destroy(other.gameObject);
                wall_destroyed++;

                if(wall_destroyed >= maximum_size * 4) { foreach (GameObject w in wall_to_destroy) { Destroy(w); }; Destroy(this.gameObject);  }
            }
            destroy_active = Random.value > 0.7f;
        }
        

        if (other.tag == "Platforme")
        {
            ActualCoord = other.GetComponent<platforme>().GetCoord();
            transform.position = other.transform.position + new Vector3(0, 0.3f, 0);
            ChangeDirection();
        }
    }
}
