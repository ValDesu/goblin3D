using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforme_gen : MonoBehaviour
{
    private static platforme_gen instance;
    private platforme_gen() { }
    public static platforme_gen Instance { get; private set; }
    void Awake() { if (instance != null && instance != this) Destroy(gameObject); instance = this;}

    [SerializeField] private GameObject pf;
    [SerializeField] private GameObject PathFinderCube;
    

    [SerializeField] private int width;
    [SerializeField] private int height;

    [Range(1f, 10f)]
    public float floatRange;

    public Dictionary<Vector2, platforme> coord_plat = new Dictionary<Vector2, platforme>();

    // Start is called before the first frame update
    void Start()
    {
        GameObject spawn = null;
        GameObject right_spawn = null;
        GameObject left_spawn = null;
        GameObject end_spawn = null;

        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject npf = Instantiate(pf, pf.transform.localScale + new Vector3(x*floatRange, 0, y* floatRange), pf.transform.rotation, this.gameObject.transform);
                npf.GetComponent<platforme>().InitCoord(x, y);

                coord_plat.Add(new Vector2(x,y), npf.GetComponent<platforme>());

                if(x == 0 && y == 0) { spawn = npf; }
                if(x == width-1 && y == height-1) { end_spawn = npf; }
                if(x == width-1 && y == 0) { right_spawn = npf; }
                if(x == 0 && y == height - 1) { left_spawn = npf; }

            }
        }

        GameObject cube = Instantiate(PathFinderCube, spawn.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        GameObject cube_end = Instantiate(PathFinderCube, end_spawn.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        GameObject cube_right = Instantiate(PathFinderCube, right_spawn.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        GameObject cube_left = Instantiate(PathFinderCube, left_spawn.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        cube.GetComponent<PathFinder>().SetMaximum(width-1); //if width and height are always the same
        cube_end.GetComponent<PathFinder>().SetMaximum(width-1); //if width and height are always the same
        cube_right.GetComponent<PathFinder>().SetMaximum(width-1); //if width and height are always the same
        cube_left.GetComponent<PathFinder>().SetMaximum(width-1); //if width and height are always the same
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
