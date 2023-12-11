using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject elevatorCam;
    public GameObject mainCam;
    public GameObject lever;

    public float targetHeight = 5.0f;
    public float moveSpeed = 2.0f;

    //private bool moving;
    
    public void StartElevator() {
        Debug.Log("going up");
        StartCoroutine(WaitThenMove());
    }

    void OnTriggerEnter() {
        elevatorCam.SetActive(true);
        mainCam.SetActive(false);
    }

    void OnTriggerExit() {
        mainCam.SetActive(true);
        elevatorCam.SetActive(false);
    }

    private IEnumerator WaitThenMove() {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(MoveUp());
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<SceneChange>().ChangeLevel();
    }

    private IEnumerator MoveUp()
    {
        while (Mathf.Abs(targetHeight - transform.position.y) > 0.01f)
        {
            float moveStep = moveSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position,
                                                     new Vector3(transform.position.x, targetHeight, transform.position.z), moveStep);

            yield return null;
        }
    }
}
