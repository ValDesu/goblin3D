using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventHistory : MonoBehaviour
{
    [SerializeField] private GameObject ui_panel;
    [SerializeField] private Sprite s_chest;
    [SerializeField] private Sprite s_ressource;
    [SerializeField] private Sprite s_sell;
    [SerializeField] private Sprite s_shop;
    [SerializeField] private Sprite s_trap;

    [SerializeField] private List<GameObject> EventImages;
    private List<platforme> EventPlatformes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHistoryEvent(platforme pf)
    {

        if (pf.is_nothing) { return; }
        EventPlatformes.Add(pf);

        GameObject o = new GameObject();
        Image im = o.AddComponent<Image>();

        if (pf.is_chest){im.sprite = s_chest;}

        if (pf.is_sell){ im.sprite = s_sell; }

        if (pf.is_shop){ im.sprite = s_shop; }

        if (pf.is_trap){ im.sprite = s_trap; }

        if (pf.is_ressource){ im.sprite = s_ressource; }


        EventImages.Add(Instantiate(o, ui_panel.transform));

    }

    public void RemoveHistoryEvent()
    {
        Destroy(EventImages[EventImages.Count - 1]);

        EventImages.Remove(EventImages[EventImages.Count - 1]);
        EventPlatformes.Remove(EventPlatformes[EventPlatformes.Count -1]);
    }



    public void NextEventIteration()
    {
        platforme current_event = EventPlatformes[0];

        if (current_event.is_ressource) { ActivateEventRessource(current_event); }
        //(...)

        EventPlatformes.Remove(current_event);
        EventImages.Remove(EventImages[0]);
    }

    private void ActivateEventRessource(platforme pf)
    {

    }

}
