using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

//[CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    public Mesh itemMesh;
    public Image itemThumbnail;
    public string itemName;
    public string itemDescription;
    
}
