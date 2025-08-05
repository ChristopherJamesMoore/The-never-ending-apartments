using UnityEngine;
using System.Collections;

public class ResetPlatform : MonoBehaviour
{
    public Transform resetPoint;
    private bool isResetting = false;  // flag to avoid multiple resets

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isResetting) return; // skip if already resetting

        if (hit.collider.CompareTag("ResetPlatform"))
        {
            StartCoroutine(ResetRoutine(() => GameManager.Instance.AddScore()));
        }
        else if (hit.collider.CompareTag("KillPlane"))
        {
            StartCoroutine(ResetRoutine(() => GameManager.Instance.RemoveScore()));
        }
    }

    private IEnumerator ResetRoutine(System.Action scoreAction)
    {
        isResetting = true;

        // Disable movement in ThirdPersonController
        var thirdPersonController = GetComponent<StarterAssets.ThirdPersonController>();
        if (thirdPersonController != null)
        {
            thirdPersonController.IsResetting = true;
        }

        // Disable CharacterController before teleporting
        var controller = GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
            Debug.Log("Teleporting player to reset point at: " + resetPoint.position);
            transform.position = resetPoint.position;
            controller.enabled = true;
        }

        // Reset movement velocity and inputs
        if (thirdPersonController != null)
        {
            thirdPersonController.ResetController();
        }

        scoreAction?.Invoke();

        yield return new WaitForSeconds(0.5f);

        if (thirdPersonController != null)
        {
            thirdPersonController.IsResetting = false;
        }

        isResetting = false;
    }
}
