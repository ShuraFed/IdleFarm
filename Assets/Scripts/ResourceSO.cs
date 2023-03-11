using UnityEngine;

[CreateAssetMenu(fileName ="New Resource")]
public class ResourceSO : ScriptableObject
{
    public ResourceType ResourceType;
    public int cost;
    public GameObject objPrefab;
    public Sprite ArtWork;
}

public enum ResourceType {Wood,Stone,Wheat}
