using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platforme : MonoBehaviour
{
    [SerializeField] private Material p_default;
    [SerializeField] private Material p_enabled;

    [SerializeField] private GameObject wall;
    [SerializeField] private WallChecker wc;

    [SerializeField] private ParticleSystem ps_random;
    [SerializeField] private ParticleSystem ps_mana;

    public int stack_mana = 0;
    public int stack_fer = 0;
    public int stack_os = 0;
    public int stack_bois = 0;
    public int stack_gold = 0;

    public bool is_shop = false;
    public bool is_trap = false;
    public bool is_sell = false;
    public bool is_chest = false;
    public bool is_ressource = false;
    public bool is_nothing = true;

    private bool is_boosted = false;
    public void SetBoosted(bool v) { is_boosted = v; }

    [SerializeField] private GameObject model_shop;
    [SerializeField] private GameObject model_trap;
    [SerializeField] private GameObject model_sell;
    [SerializeField] private GameObject model_chest;

    private bool is_blocked = false;
    [SerializeField] private GameObject wall_z_plus;
    [SerializeField] private GameObject wall_z_minus;
    [SerializeField] private GameObject wall_x_plus;
    [SerializeField] private GameObject wall_x_minus;


    private Vector2 coord = Vector2.zero;
    private int enabledCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(wall, this.transform.position + new Vector3(-0.5f, 0.5f, 0f), new Quaternion(0f,0f,0f,0f));
        Instantiate(wall, this.transform.position + new Vector3(0f, 0.5f, -0.5f), new Quaternion(0, 0.7f, 0, 0.7f)); //Replace Euler
        

        InitRessources();
    }

    public void CloseCell(Vector2 coming_from)
    {

        wall_z_plus.SetActive(coming_from != new Vector2(0, -1));
        wall_z_minus.SetActive(coming_from != new Vector2(0, 1));
        wall_x_plus.SetActive(coming_from != new Vector2(-1, 0));
        wall_x_minus.SetActive(coming_from != new Vector2(1, 0));
        is_blocked = true;
    }

    private void OpenCell()
    {
        wall_z_plus.SetActive(false);
        wall_z_minus.SetActive(false);
        wall_x_plus.SetActive(false);
        wall_x_minus.SetActive(false);
        is_blocked = false;
    }

    void InitRessources()
    {
        // vendeur / shop / piege / ressource / coffre
        
        if(Random.value > 1 - 1/4f)
        {
            is_nothing = false;

            WallChecker _wc = Instantiate(wc, this.transform.position, Quaternion.identity);
            _wc.cell = this; is_boosted = true;


            if (Random.value > 1 - 1/2f)
            {
                is_ressource = true;
                stack_mana = Random.Range(0, 5);
                stack_fer = Random.Range(0, 5);
                stack_os = Random.Range(0, 5);
                stack_bois = Random.Range(0, 5);
                stack_gold = Random.Range(0, 2);

                Instantiate(ps_random, transform);
            }
            else
            {
                switch(Random.Range(0, 4))
                {
                    case 0:
                        is_chest = true;
                        Instantiate(model_chest, transform.position, Quaternion.identity);
                        break;
                    case 1:
                        is_sell = true;
                        Instantiate(model_sell, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        is_shop = true;
                        Instantiate(model_shop, transform.position, Quaternion.identity);
                        break;
                    case 3:
                        is_trap = true;
                        Instantiate(model_trap, transform.position, Quaternion.identity);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitCoord(int x, int y) { this.coord = new Vector2(x, y); }
    public Vector2 GetCoord() { return this.coord; }
    public void EnablePlatforme(){
        this.gameObject.GetComponent<MeshRenderer>().material = p_enabled;
        enabledCounter++;
    }
    public void DisablePlatforme(){ enabledCounter--; if (enabledCounter == 0) { this.gameObject.GetComponent<MeshRenderer>().material = p_default; } }
    
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" && is_blocked)
        {
            OpenCell();
        }
    }
}
