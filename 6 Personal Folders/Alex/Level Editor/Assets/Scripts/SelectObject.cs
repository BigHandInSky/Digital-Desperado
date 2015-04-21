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
        //print("Start Running");

        //while (true)
        //{
            if (Input.GetMouseButtonDown(0))
            {
                print("Click");

                RaycastHit hit;

                Physics.Raycast(levelCamera.transform.position, levelCamera.ScreenToWorldPoint(Input.mousePosition),
                    out hit, 100000, collisionMask);

                if (hit.collider != null)
                {
                    DeselectObject();
                    SelectNewObject(hit.collider.gameObject);
                }
                else
                {
                    DeselectObject();
                }
            }

            //yield return new WaitForEndOfFrame();
        //}
    }

    private void SelectNewObject(GameObject clickedObject)
    {
        print("Select");
        print(clickedObject);

        Renderer objectRenderer = goSelectedObject.GetComponent<MeshRenderer>();

        goSelectedObject = clickedObject;
        mObjectOriginalMaterial = objectRenderer.material;
        objectRenderer.material = mSelectMaterial;

        goEditObjectPanel.SetActive(true);
    }

    private void DeselectObject()
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