using System.Collections.Generic;
using UnityEngine;

public class Bobble : MonoBehaviour
{
    // We'll spring about this transform's origin point.
    public Transform pivot;

    // Tunable parameters for how sharply the bobble spring pulls back,
    // and how long we keep bobbling after a shake.
    public float stiffness = 100f;
    [Range(0, 1)]
    public float conservation = 0.98f;
    [Range(0, 1)]
    public float dampingStrength=0.1f;

    // Used to constrain the bobble to a shell around the pivot.
    Vector3 _restPosition;
    float _radius;

    // Track physics state for Verlet integration.
    Vector3 _worldPosNew;
    Vector3 _worldPosOld;

    // Used for interpolation between fixed steps.
    float _timeElapsed;

    private void Start()
    {
        // Cache our initial offset from the pivot as our resting position.
        _restPosition = pivot.InverseTransformDirection(transform.position - pivot.position);
        _radius = _restPosition.magnitude;
    }

    private void FixedUpdate()
    {
        // Compute a desired position our spring wants to push us to.
        Vector3 desired = pivot.TransformPoint(_restPosition);

        Vector3 velocity = _worldPosNew - _worldPosOld;
        Vector3 damping = -velocity * dampingStrength;
        Vector3 acceleration = stiffness * (desired - _worldPosNew) + damping;

        //// The further we are from this position, the more correcting force it applies.
        //Vector3 acceleration = stiffness * (desired - _worldPosNew);

        // Step forward a new position using Verlet integration.
        Vector3 newPos = _worldPosNew + conservation * (_worldPosNew - _worldPosOld)
            + Time.deltaTime * Time.deltaTime * acceleration;
        _worldPosOld = _worldPosNew;

        // Constrain the bobble within our radius.
        _worldPosNew = ClampedOffset(newPos) + pivot.position;

        // Clear the accumulated time now that we have a new sample.
        _timeElapsed = 0f;
    }


    private void Update()
    {
        // Interpolate our position so we get nice smooth movement,
        // without stutters or beats with the FixedUpdate rate.
        _timeElapsed += Time.deltaTime;
        float t = (_timeElapsed / Time.fixedDeltaTime) % 1.0f;
        Vector3 blend = Vector3.Lerp(_worldPosOld, _worldPosNew, t);

        // Correct the interpolated position to one on the shell around our pivot.
        Vector3 offset = ClampedOffset(blend);
        // transform.position = offset + pivot.position;
        transform.position = Vector3.Lerp(_worldPosOld, _worldPosNew, Mathf.Clamp01(_timeElapsed / Time.fixedDeltaTime));

        // Orient so "up" points away from the pivot,
        // and "forward" aligns roughly to the pivot's forward.
        transform.rotation = Quaternion.LookRotation(offset, -pivot.forward)
            * Quaternion.Euler(90f, 0f, 0f);
    }

    Vector3 ClampedOffset(Vector3 position)
    {
        // Clamp our position onto a spherical shell surrounding the pivot
        // (as though we were swivelling on a rod of fixed length)
        Vector3 offset = position - pivot.position;
        return offset.normalized * _radius;
    }
}
