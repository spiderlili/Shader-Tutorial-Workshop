using UnityEngine;

// Make sure FOV encapsulates the objToBeFramed. If you move the obj: update FOV accordingly to make sure it's always visible.
// Because of the symmetry of the camera, having > 1 target that it should frame is just a matter of getting the maximum of them.
public class TrigonometryFOV : MonoBehaviour
{
    public Transform objToBeFramed;
    public Camera cam;
    private void OnDrawGizmos()
    {
        if (cam == null) {
            cam = GetComponent<Camera>();
        }

        Vector2 relativeObjPos = objToBeFramed.position - cam.transform.position;

        // Will only work if camera is at (0, 0)
        float opposite = relativeObjPos.y;
        float adjacent = relativeObjPos.x;
        
        // Tan(angle) = opp / adj 
        // Angle = atan(opp / adj) 
        float angleInRadians = Mathf.Atan(opposite / adjacent);
        cam.fieldOfView = 2 * angleInRadians * Mathf.Rad2Deg;
    }
}
