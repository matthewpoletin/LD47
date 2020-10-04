using UnityEngine;

[CreateAssetMenu(fileName = "CommonAssets", menuName = "Params/CommonAssets", order = 2)]
public class CommonAssets : ScriptableObject
{
    public GameObject DialogPrefab = default;
    public GameObject OrderPrefab = default;
    public GameObject MinigamePrefab = default;
}