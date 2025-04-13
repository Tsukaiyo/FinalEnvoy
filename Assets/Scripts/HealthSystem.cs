using System.Collections.Generic;
using System.Data;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    
    [SerializeField] float health;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;
 
    Animator animator;
    Transform healthDisplay;
    Slider currentHealthSlider;

    Camera camera;
    Vector3 height;
    void Start()
    {
        height = new Vector3(0, 3, 0);
        camera = FindAnyObjectByType<Camera>();
        healthDisplay = transform.Find("Health Display");
        if (healthDisplay != null) DrawHealthBar();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (healthDisplay != null)
        {
            healthDisplay.transform.rotation = camera.transform.rotation;
            healthDisplay.transform.position = healthDisplay.parent.transform.position + height;
        }
    }
    public void TakeDamage(float damageAmount)
    {
        if (healthDisplay != null) UpdateHealthbar();
        health -= damageAmount;
        animator.SetTrigger("damage");
        
        //CameraShake.Instance.ShakeCamera(1f, 0.2f);
        if (health <= 0)
        {
            Die();
        }
    }
    public void TakeEffectDamage(float damageAmount)
    {
        UpdateHealthbar();
        health -= damageAmount;
        //HitVFx(effectVFX)
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }
    public void DrawHealthBar()
    {
        currentHealthSlider = healthDisplay.GetComponent<Slider>();
        if (currentHealthSlider == null)
        {
            Debug.Log("NO SLIDER");
        }
        currentHealthSlider.maxValue = health;
        currentHealthSlider.value = health;
    }
    public void UpdateHealthbar()
    {
        currentHealthSlider.value = health;
    }
}