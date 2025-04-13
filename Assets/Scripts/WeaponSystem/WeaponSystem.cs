using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;
    
    [SerializeField] string targetLayer;
    [Header("Weapon Stats")]
    [SerializeField] AttackShape attackShape;
    [SerializeField] StatusEffect effect;
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    [Range(0, 2)]
    public float attackSpeed;

    [Header("For Circular Attack")]
    [Range(5, 360)]
    public float angle;
    [SerializeField] bool isClockwise = true;
    [SerializeField] float attackDuration;
    [Header("For Rectangular Attack")]
    [Range(1, 10)]
    public float attackWidth;
    [Header("Raycast")]
    [SerializeField] int rayCount = 50;
    EffectInstance effectInstance;
    int targetLayerMask;
    Animator animator;
    void Start()
    {
        targetLayerMask = LayerMask.GetMask(targetLayer);
        animator = transform.GetComponent<Animator>();
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
        
    }

    void Update()
    {
        animator.SetFloat("attackSpeed", attackSpeed);
        
        if (canDealDamage)
        {
            if(attackShape.Equals(AttackShape.Rectangular)) StartDrawingRectRay();
            if(attackShape.Equals(AttackShape.Circular)) StartDrawingConeRay();

            attackDuration = 0;
        }
    }
    public void StartDealDamage()
    {
        attackDuration = Time.deltaTime;
        canDealDamage = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        attackDuration = Time.deltaTime;
        canDealDamage = false;
    }
    public IEnumerator DrawConeRayOverTime(float duration)
    {
        float adjustedAngle = isClockwise ? angle : -angle;
        float halfangle = adjustedAngle / 2;
        float timeStep = duration / rayCount; 
        RaycastHit hit;
        for (int i = 0; i < rayCount; i++)
        {
            float step = adjustedAngle / (rayCount - 1);
            float currentAngle = (-halfangle + step * i) * Mathf.Deg2Rad;

            float x = Mathf.Sin(currentAngle);
            float z = Mathf.Cos(currentAngle);

            Vector3 direction = transform.forward * z + transform.right * x;

            if (Physics.Raycast(transform.position + transform.up * 1.25f, direction.normalized, out hit, weaponLength, targetLayerMask))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (!hasDealtDamage.Contains(hitObject))
                {
                    hasDealtDamage.Add(hitObject); 

                    if (hitObject.TryGetComponent(out HealthSystem healthSystem))
                    {
                        healthSystem.TakeDamage(weaponDamage);
                        //healthSystem.HitVFX(hit.point);
                    }
                    if (hitObject.TryGetComponent(out StatusEffectManager statusEffectManager))
                    {
                        effectInstance = new EffectInstance(effect, hitObject);
                        if(!statusEffectManager.GetEffectInstances().Contains(effectInstance))
                        {
                            effectInstance.AddEffectToTarget(); //doesnt prevent dupe effects...
                        }
                    }
                    
                
                }
            }
            Debug.DrawRay(transform.position + transform.up*1.25f, direction * weaponLength, Color.magenta); 

            yield return new WaitForSeconds(timeStep);
        }
    }
    public IEnumerator DrawRectRayOverTime(float duration)
    {
      
        
        yield return new WaitForSeconds(attackSpeed );
        RaycastHit hit;
        for (int i = 0; i < rayCount; i++)
        {
            float step = attackWidth / (rayCount - 1);
            float currentStep = (-attackWidth / 2 + step * i);

            Vector3 origin = transform.position + transform.right * currentStep + transform.up*1.25f; // Offset along right vector
            Vector3 direction = transform.forward; // Always shoot forward


            int layerMask = LayerMask.GetMask(targetLayer);

            if (Physics.Raycast(origin, direction, out hit, weaponLength, targetLayerMask))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (!hasDealtDamage.Contains(hitObject))
                {
                    hasDealtDamage.Add(hitObject);

                    if (hitObject.TryGetComponent(out HealthSystem healthSystem))
                    {
                        healthSystem.TakeDamage(weaponDamage);
                        //healthSystem.HitVFX(hit.point);
                    }
                    if (hitObject.TryGetComponent(out StatusEffectManager statusEffectManager))
                    {
                        effectInstance = new EffectInstance(effect, hitObject);
                        if (!statusEffectManager.GetEffectInstances().Contains(effectInstance))
                        {
                            effectInstance.AddEffectToTarget(); //doesnt prevent dupe effects...
                        }
                    }


                }
            }
            Debug.DrawRay(origin, direction * weaponLength, Color.magenta);
        }
    }

    public void StartDrawingConeRay()
    {
        StartCoroutine(DrawConeRayOverTime(attackDuration));
    }
    public void StartDrawingRectRay()
    {
        StartCoroutine(DrawRectRayOverTime(attackSpeed));
    }
    public enum AttackShape
    {
       Rectangular,
       Circular
    }
}
