using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuDropdown : MonoBehaviour {
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    public GameObject parentCanvas;
    // Use this for initialization
    void Start () {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = parentCanvas.GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }
	
	// Update is called once per frame
	void Update () {

        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Input.GetMouseButtonDown(0))
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();
            if (Physics.Raycast(ray, out hitInfo))
            {

                // Move thecursor to the point where the raycast hit.
                var position = hitInfo.point;
            }

        
        }

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {

            // Move thecursor to the point where the raycast hit.
            var position = hitInfo.point;
           
        }
        else
        {
            
        }
    }
}
