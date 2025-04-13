using System.Collections;
using System.Net;
using UnityEngine;

public class FallingObjectNPCDestroy : MonoBehaviour
{
    [Header("Destroy target with layer:")]
    [SerializeField] string targetLayerName;
    [SerializeField] float damage;
    private void OnTriggerEnter(Collider other)
    {
        int targetLayer = LayerMask.NameToLayer(targetLayerName);

        if (other.gameObject.layer == targetLayer)
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damage);
                
            }
        }
    }
    IEnumerable Delay(int delay)
    {
       
        yield return new WaitForSeconds(delay);
    }
}