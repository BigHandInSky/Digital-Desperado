using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class TooltipFunctions : MonoBehaviour 
{
    [SerializeField]
    SelectObject objectSelecter;

    [SerializeField]
    InputField posXText;
    [SerializeField]
    InputField posYText;
    [SerializeField]
    InputField posZText;
    [SerializeField]
    InputField rotYText;
    [SerializeField]
    InputField rotXText;
    [SerializeField]
    InputField scaleXText;
    [SerializeField]
    InputField scaleYText;
    [SerializeField]
    InputField scaleZText;

    public void UpdateTextFields()
    {
        posXText.text = objectSelecter.ObjectSelected.transform.position.x.ToString();
        posYText.text = objectSelecter.ObjectSelected.transform.position.y.ToString();
        posZText.text = objectSelecter.ObjectSelected.transform.position.z.ToString();
        rotYText.text = objectSelecter.ObjectSelected.transform.rotation.eulerAngles.y.ToString();
        rotXText.text = objectSelecter.ObjectSelected.transform.rotation.eulerAngles.x.ToString();
        scaleXText.text = objectSelecter.ObjectSelected.transform.localScale.x.ToString();
        scaleYText.text = objectSelecter.ObjectSelected.transform.localScale.y.ToString();
        scaleZText.text = objectSelecter.ObjectSelected.transform.localScale.z.ToString();
    }

    // Position
    public void SetPositionX(InputField input)
    {
        Vector3 position = objectSelecter.ObjectSelected.transform.position;
        position.x = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.position = position;
    }

    public void SetPositionY(InputField input)
    {
        Vector3 position = objectSelecter.ObjectSelected.transform.position;
        position.y = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.position = position;
    }

    public void SetPositionZ(InputField input)
    {
        Vector3 position = objectSelecter.ObjectSelected.transform.position;
        position.z = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.position = position;
    }

    // Rotation
    public void SetRotationX(InputField input)
    {
        Quaternion rotation = objectSelecter.ObjectSelected.transform.rotation;
        rotation = Quaternion.Euler(float.Parse(input.text), rotation.eulerAngles.y, rotation.eulerAngles.z);
        objectSelecter.ObjectSelected.transform.rotation = rotation;
    }
    public void SetRotationY(InputField input)
    {
        Quaternion rotation = objectSelecter.ObjectSelected.transform.rotation;
        rotation = Quaternion.Euler(rotation.eulerAngles.x, float.Parse(input.text), rotation.eulerAngles.z);
        objectSelecter.ObjectSelected.transform.rotation = rotation;
    }

    // Scale
    public void SetScaleX(InputField input)
    {
        Vector3 scale = objectSelecter.ObjectSelected.transform.localScale;
        scale.x = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.localScale = scale;
    }

    public void SetScaleY(InputField input)
    {
        Vector3 scale = objectSelecter.ObjectSelected.transform.localScale;
        scale.y = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.localScale = scale;
    }

    public void SetScaleZ(InputField input)
    {
        Vector3 scale = objectSelecter.ObjectSelected.transform.localScale;
        scale.z = float.Parse(input.text);
        objectSelecter.ObjectSelected.transform.localScale = scale;
    }

    // Confirm
    public void ConfirmChanges()
    {
        objectSelecter.DeselectObject();
    }
    
    // Random
    public void RandomizeStats()
    {
        Vector3 position = new Vector3(Random.Range(-10, 11), Random.Range(-10, 11), Random.Range(-10, 11));
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        Vector3 scale = new Vector3(Random.Range(0.1f, 5), Random.Range(0.1f, 5), Random.Range(0.1f, 5));

        objectSelecter.ObjectSelected.transform.position = position;
        objectSelecter.ObjectSelected.transform.rotation = rotation;
        objectSelecter.ObjectSelected.transform.localScale = scale;

        UpdateTextFields();
    }

    // Default
    public void DefaultSettings()
    {
        objectSelecter.ObjectSelected.transform.position = Vector3.zero;
        objectSelecter.ObjectSelected.transform.rotation = Quaternion.identity;
        objectSelecter.ObjectSelected.transform.localScale = Vector3.one;

        UpdateTextFields();
    }
}