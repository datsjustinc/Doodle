using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
  public Transform player; //target object to move camera position to
  public Vector3 offset;  // difference in positions from camera to target
  [Range(1, 10)] // range of editable field value
  public float transitionFactor; // create editable fields to adjust factor that is multiplied to lerp frame time
  public float difference;
  public Vector3 minValue, maxValue; // define min and max values of camera limit by creating editable fields

  void Update()
  {
    difference = Vector3.Distance (transform.position, player.transform.position);
    //Debug.Log(difference);
  }
  private void FixedUpdate()
  {
    Follow(); // Call the follow method every frame
  }

  void Follow()
  {
      Vector3 playerPosition = player.position + offset; // create and store target position values including offset difference

      Vector3 boundPosition = new Vector3(Mathf.Clamp(playerPosition.x, minValue.x, maxValue.x), playerPosition.y, Mathf.Clamp(playerPosition.z, minValue.z, maxValue.z)); // create and store new camera positions with limits to min and max values if out of bounds

      Vector3 transitionPosition = Vector3.Lerp(transform.position, boundPosition, transitionFactor*Time.fixedDeltaTime); // create and store camera's new smooth transition position from current positions to new bound positions using lerp
      
      transform.position = transitionPosition; // update camera's position to new smooth lerp position
      
      /*
      if (difference > 10.02)
      {
        transform.position = transitionPosition; // update camera's position to new smooth lerp position
      }
      */
  }

}
