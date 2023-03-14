using UnityEngine;
using static HomeRunGameManager;

[RequireComponent(typeof(Rigidbody))]
public class Parabola : MonoBehaviour
{
    public Transform m_Target;
    public float m_InitialAngle = 30f; // 처음 날라가는 각도
    private Rigidbody m_Rigidbody;

    private Vector3 oriPos;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        oriPos = transform.localPosition;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hit(0);
        }
    }

    public void Hit(eHIT_TYPE hitType)
    {
        SetTargetPos(hitType);

        gameObject.SetActive(true);
        transform.localPosition = oriPos;
        Vector3 velocity = GetVelocity(transform.position, m_Target.position, m_InitialAngle);
        m_Rigidbody.velocity = velocity;
        GetComponent<TrailRenderer>().Clear();
    }

    private void SetTargetPos(eHIT_TYPE hitType)
    {
        float posX, posY, posZ;
        posY = 0;

        switch (hitType)
        {
            case eHIT_TYPE.BASE_1:
            default:
                posZ = Random.Range(-20, -25);
                break;
            case eHIT_TYPE.BASE_2:
                posZ = Random.Range(-30, -25);
                break;
            case eHIT_TYPE.BASE_3:
                posZ = Random.Range(-35, -30);
                break;
            case eHIT_TYPE.HOMERUN:
                posZ = Random.Range(-100, -70);
                break;
        }
        if (posZ < -25) posX = Random.Range(-18.5f, 18.5f);
        else if (posZ < -30) posX = Random.Range(-20, 20);
        else posX = Random.Range(-22, 22);

        m_Target.localPosition = new Vector3(posX, posY, posZ);
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }
}