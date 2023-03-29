using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventRessourceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text t_mana;
    [SerializeField] private TMP_Text t_os;
    [SerializeField] private TMP_Text t_fer;
    [SerializeField] private TMP_Text t_bois;
    [SerializeField] private TMP_Text t_gold;


    public void InitTexts(platforme pf)
    {
        t_mana.text = pf.stack_mana.ToString();
        t_os.text = pf.stack_os.ToString();
        t_fer.text = pf.stack_fer.ToString();
        t_bois.text = pf.stack_bois.ToString();
        t_gold.text = pf.stack_gold.ToString();
    }


}
