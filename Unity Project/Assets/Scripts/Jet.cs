using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Linq;

public class Jet : Agent
{
    [SerializeField]
    private float speed = 100;

    [SerializeField]
    private float pitchSpeed = 6;

    [SerializeField]
    private float rollSpeed = 4;

    [SerializeField]
    private GameObject PackagePrefab;

    private GameObject nearestPackage;

    public bool trainingMode;

    [SerializeField]
    private Transform JetTip;

    private bool Frozen = false;

    public float PackagesReceived { get; private set; }

    public override void Initialize()
    {
        if (!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        PackagesReceived = 0f;
        speed = 100;

        transform.position = transform.parent.transform.position + new Vector3(0, 3f, -195f);
        transform.rotation = Quaternion.identity;

        ResetPackage();
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        if (Frozen) return;

        transform.Rotate(vectorAction[0] * pitchSpeed, 0.0f, vectorAction[1] * rollSpeed);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (nearestPackage == null)
        {
            sensor.AddObservation(new float[19]);
            return;
        }

        RaycastHit hit;
        sensor.AddObservation(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) && hit.transform.CompareTag("Package")); // 1

        sensor.AddObservation(Vector3.Distance(nearestPackage.transform.position, JetTip.position)); // 1

        Vector3 toPackage = nearestPackage.transform.position - transform.position;
        sensor.AddObservation(Vector3.Dot(transform.forward.normalized, -toPackage));

        sensor.AddObservation(transform.position); // 3

        sensor.AddObservation(nearestPackage.transform.position); // 3

        sensor.AddObservation((nearestPackage.transform.position - JetTip.position).normalized); // 3

        sensor.AddObservation(transform.forward); // 3

        sensor.AddObservation(transform.localRotation); //3

        //1 + 1 + 3 + 3 + 3 + 3 + 3 = 19 total Observations
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = -Input.GetAxis("Horizontal");
    }

    public void FreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        Frozen = true;
    }

    public void UnfreezeAgent()
    {
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        Frozen = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Package") && collision.transform.parent.gameObject == nearestPackage.gameObject)
        {
            PackagesReceived += 1f;

            ResetPackage();

            if (trainingMode)
            {
                AddReward(1f);
            }
        }
        else if (trainingMode && collision.gameObject.CompareTag("Muur") && collision.gameObject.transform.parent.transform.parent.gameObject == transform.parent.gameObject)
        {
            AddReward(-1f);
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }
    }

    public void Movement()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    public void ResetPackage()
    {
        Destroy(nearestPackage);

        GameObject package = Instantiate(PackagePrefab, new Vector3(transform.parent.transform.position.x + Random.Range(-450, 450), transform.parent.transform.position.y + Random.Range(100, 950), transform.parent.transform.position.z + Random.Range(-450, 450)), Quaternion.identity);
        package.transform.parent = transform.parent.transform;

        nearestPackage = package;
    }

    void FixedUpdate()
    {
        if (nearestPackage != null)
            Debug.DrawLine(JetTip.position, nearestPackage.transform.position, Color.green);

        RequestDecision();
        Movement();

        Academy.Instance.StatsRecorder.Add("Packages Recieved: " + transform.parent.name, PackagesReceived);
    }
}
