using UnityEngine;

public class TargetMarkerDestroy : MonoBehaviour
{
    [Header("Destroyed by objects in Layer:")]
    [SerializeField] string layerMaskName;
    private void OnTriggerEnter(Collider collider)
    {
        
        int layerIndex = LayerMask.NameToLayer(layerMaskName);
        Debug.Log("Collided with: " + collider.gameObject.name);
        if (collider.gameObject.layer == layerIndex)
        {

            Destroy(this.gameObject);
        }
      
    }
}
