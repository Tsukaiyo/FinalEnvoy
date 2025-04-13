using UnityEngine;

public class PlayerPassFail : MonoBehaviour
{
    // INCORPORATE THIS SCRIPT's METHODS IN THE PLAYER MOVEMENT SCRIPT

    [SerializeField] private float launchForce; 
    [SerializeField] private float dodgeForce; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Dodge()
    {
        rb.AddForce(transform.forward * -1 * dodgeForce, ForceMode.VelocityChange);
    }
    public void BlastOff()
    {
        rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
    }

}
