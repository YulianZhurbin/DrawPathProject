using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;

    private static PlayerController instance;
    private Vector3 destination;
    private Queue<Vector3> points;

    public bool DoesPathExist
    {
        get
        {
            if (points.Count == 0)
            { 
                return false; 
            }
            else 
            { 
                return true; 
            }
        }
    }

    public static PlayerController Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
        points = new Queue<Vector3>();
    }

    private void Start()
    {
        LineDrawer.Instance.AddPoint(transform.position);
    }

    void Update()
    {
        if(GameManager.Instance.IsGameOver)
        {
            return;
        }

        if (!GameManager.Instance.IsLaunched)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AddPoint();
            }
        }
        else
        {
            Move();

            if (transform.position == destination)
            {
                ScoreCounter.Instance.IncreaseScore();

                SetDestination();
            }
        }
    }

    private void AddPoint()
    {
        Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Physics.Raycast(screenToWorldPoint, Camera.main.transform.forward, out RaycastHit hit) && hit.collider.CompareTag("Plane"))
        {
            Vector3 newPoint = new Vector3(hit.point.x, 0.25f, hit.point.z);
            points.Enqueue(newPoint);
            LineDrawer.Instance.AddPoint(newPoint);
        }
    }

    private void SetDestination()
    {
        if (points.Count > 0)
        {
            destination = points.Dequeue();
        }
        else
        {
            if(GameManager.Instance.IsLaunched)
            {
                GameManager.Instance.StopGame();
            }
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }
}

