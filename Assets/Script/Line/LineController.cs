using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LineController : MonoBehaviour
{
    public static LineController instance;

    #region Line Initializer
    [SerializeField]
    private GameObject line;
    private LineRenderer lineRenderer;
    private EdgeCollider2D edgeCollider2D;

    private Vector2[] colliderPoints;

    [SerializeField]
    private GameObject pointPrefab;

    private const int MAX_POINT = 2;
    #endregion

    #region Line Movement
    private Dictionary<GameObject, Vector3> pointPositions;
    private GameObject[] attachingObjects;
    #endregion

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this; 
        }
    }

    public void SetLine(GameObject linePrefab)
    {
        this.line = Instantiate(linePrefab);

        lineRenderer = line.GetComponent<LineRenderer>();
        edgeCollider2D = line.GetComponent<EdgeCollider2D>();
        pointPositions = new Dictionary<GameObject, Vector3>();
        attachingObjects = new GameObject[MAX_POINT];
        colliderPoints = new Vector2[MAX_POINT];
        edgeCollider2D.enabled = false;

    }

    public void InitNewLine(Vector3 startPoint, Vector3 endPoint)
    {
        InitPoints(new Vector3[] { startPoint, endPoint });

        lineRenderer.positionCount = MAX_POINT;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);
        edgeCollider2D.enabled = true;
        SetCollider(0);
        SetCollider(1);
    }

    private void InitPoints(Vector3[] pointPosition)
    {
        for (int i = 0; i < MAX_POINT; i++)
        {
            GameObject newPoint = Instantiate(pointPrefab);
            newPoint.transform.position = pointPosition[i];
            newPoint.name = i.ToString();
            pointPositions.Add(newPoint, pointPosition[i]);
        }
    }
    
    public void SetPoint(int index, Vector3 pointPosition)
    {  
        lineRenderer.SetPosition(index, pointPosition);
        SetCollider(index);
    }

    private void SetCollider(int index)
    {
        colliderPoints[index] =  line.transform.InverseTransformPoint(lineRenderer.GetPosition(index));
        edgeCollider2D.points = colliderPoints;
    }

    public void AttachTo(GameObject attachingObject, GameObject objectToAttack)
    {
        pointPositions[attachingObject] = objectToAttack.transform.position;

        GameObject lastAttached = attachingObjects[int.Parse(attachingObject.name)];

        if(lastAttached != null)
        {
            lastAttached.transform.position = new Vector3(lastAttached.transform.position.x, lastAttached.transform.position.y, -6);
        }

        attachingObjects[int.Parse(attachingObject.name)] = objectToAttack;
        objectToAttack.transform.position = new Vector3(objectToAttack.transform.position.x, objectToAttack.transform.position.y, -3);
    }

    public void BackToPrevious(GameObject gameObject)
    {
        if(pointPositions.ContainsKey(gameObject))
        {
            gameObject.transform.position = pointPositions[gameObject];
            SetPoint(int.Parse(gameObject.name), pointPositions[gameObject]);
        }
    }
}
