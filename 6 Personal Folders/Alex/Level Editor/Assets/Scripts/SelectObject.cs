using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour 
{
    Camera levelCamera;

    [SerializeField]
    LayerMask collisionMask;

    GameObject goSelectedObject;

    [SerializeField]
    Material mSelectMaterial;
    Material mObjectOriginalMaterial;

    [SerializeField]
    GameObject goEditObjectPanel;
    [SerializeField]
    TooltipFunctions tooltipFunctions;

    public GameObject ObjectSelected
    {
        get { return goSelectedObject; }
    }

    void Awake()
    {
        levelCamera = Camera.main;
        goSelectedObject = null;
        mObjectOriginalMaterial = null;

        //StartCoroutine("UpdateClick");
    }

    //IEnumerator UpdateClick()
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Click");

            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Physics.Raycast(mouseRay, out hit, 100000, collisionMask);

            if (hit.collider != null)
            {
                SelectNewObject(hit.collider.gameObject);
            }
        }
    }

    public void SelectNewObject(GameObject clickedObject)
    {
        print("Select Object");
        print(clickedObject);

        DeselectObject();
        goSelectedObject = clickedObject;

        Renderer objectRenderer = goSelectedObject.GetComponent<Renderer>();
        mObjectOriginalMaterial = objectRenderer.material;
        objectRenderer.material = mSelectMaterial;

        goEditObjectPanel.SetActive(true);
        tooltipFunctions.UpdateTextFields();
    }

    public void DeselectObject()
    {
        print("Deselect Object");

        if (goSelectedObject == null || mObjectOriginalMaterial == null)
        {
            return;
        }

        goSelectedObject.GetComponent<Renderer>().material = mObjectOriginalMaterial;

        goSelectedObject = null;
        mObjectOriginalMaterial = null;

        goEditObjectPanel.SetActive(false);
    }
}