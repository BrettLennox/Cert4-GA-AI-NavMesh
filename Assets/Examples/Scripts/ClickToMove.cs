using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    [SerializeField] bool _isRandomPosition = false;
    [SerializeField] float _randomDistanceRadius = 10f;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_isRandomPosition)
        {
            RandomMove();
        }
        ClickMove();
    }

    private void RandomMove()
    {
        if (m_Agent.remainingDistance != Mathf.Infinity
            && m_Agent.pathStatus == NavMeshPathStatus.PathComplete
            && m_Agent.remainingDistance == 0)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-_randomDistanceRadius, _randomDistanceRadius),
            0,
            Random.Range(-_randomDistanceRadius, _randomDistanceRadius));

            Vector3 newPosition = m_Agent.transform.position + randomOffset;

            m_Agent.SetDestination(newPosition);
        }
    }

    private void ClickMove()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                m_Agent.destination = m_HitInfo.point;
        }
    }
}
