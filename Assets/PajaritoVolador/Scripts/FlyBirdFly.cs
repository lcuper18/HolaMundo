using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PajaritoVolador {
public class FlyBirdFly : MonoBehaviour {
  [Header("Configuration")]
  public float maxSpeed = 1;

  [Header("Information")]
  public float speed;
  public Vector3 relativeSpeed;
  public Camera mainCamera;

  [Header("Initialization")]
  public Animator animator;
  public Transform target;
  public Rigidbody body;

  void Update () {
    target.position = GetTargetFromMouse();
    body.linearVelocity = Vector3.ClampMagnitude(body.linearVelocity, maxSpeed);
    speed = body.linearVelocity.magnitude;
    relativeSpeed = mainCamera.transform.InverseTransformPoint(mainCamera.transform.position + body.linearVelocity);
    animator.SetFloat("horizontal", relativeSpeed.x / maxSpeed);
    animator.SetFloat("vertical", relativeSpeed.y / maxSpeed);
  }

  public Vector3 GetTargetFromMouse () {
    mainCamera = Camera.main;
    Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

    float distance = Vector3.Project(mainCamera.transform.InverseTransformPoint(transform.position),
                                     mainCamera.transform.forward).magnitude;
    Vector3 inPoint = mainCamera.transform.position + mainCamera.transform.forward * distance;

    Plane plane = new Plane(-mainCamera.transform.forward, inPoint);
    float rayDistance;
    plane.Raycast(ray, out rayDistance);
    return ray.GetPoint(rayDistance);
  }
}
}
