using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<platforme> Pathway;
    [SerializeField] private EventHistory EventHistory;
    [SerializeField] private TMP_Text stat_pm;
    private int max_pm = 10;
    
    void Start()
    {
        stat_pm.text = max_pm.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateStepUI()
    {
        stat_pm.text = (max_pm - Pathway.Count +1).ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Platforme")
        {

            

            platforme pf = other.GetComponent<platforme>();
            if(Pathway.Count == 0) { Pathway.Add(pf);  return; } //spawn

            if(Pathway.Count >= 2)
            {
                if (pf == Pathway[Pathway.Count - 2])
                {
                    if (!Pathway[Pathway.Count - 1].is_nothing) {
                        EventHistory.RemoveHistoryEvent();
                    }

                    Pathway[Pathway.Count - 1].DisablePlatforme();
                    Pathway.RemoveAt(Pathway.Count - 1);

                    UpdateStepUI();

                    return;
                }
            }

            pf.EnablePlatforme();
            Pathway.Add(pf);
            EventHistory.AddHistoryEvent(pf);

            UpdateStepUI();

            if (Pathway.Count > max_pm)
            {
                Pathway[Pathway.Count - 1].CloseCell(Pathway[Pathway.Count - 1].GetCoord() - Pathway[Pathway.Count - 2].GetCoord());
                Debug.Log(Pathway[Pathway.Count - 1].GetCoord() - Pathway[Pathway.Count - 2].GetCoord());
                return;
            }

            

        }
        
    }
}
