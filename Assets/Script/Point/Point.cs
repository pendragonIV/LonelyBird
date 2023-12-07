using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    private void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -5;
        this.transform.position = mousePos;
        LineController.instance.SetPoint(int.Parse(this.name), this.transform.position);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = -5;
        this.transform.position = mousePos;
        LineController.instance.SetPoint(int.Parse(this.name), this.transform.position);
    }

    private void OnMouseUp()
    {
        GameObject attachable = IsAttachable();
        if(attachable)
        {
            this.transform.position = attachable.transform.position;    
            LineController.instance.SetPoint(int.Parse(this.name), this.transform.position);
            LineController.instance.AttachTo(this.gameObject, attachable);
        }
        else
        {
            LineController.instance.BackToPrevious(this.gameObject);
        }
    }


    private GameObject IsAttachable()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null && hit.collider.CompareTag("Attachable"))
        {
            hit.transform.position.Set(this.transform.position.x, this.transform.position.y, 0);
            return hit.collider.gameObject;
        }
        return null;
    }
}
