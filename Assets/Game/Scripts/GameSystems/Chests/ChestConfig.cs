using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest", menuName = "ScriptableObjects/Configs/Chest", order = 0)]
public class ChestConfig : ScriptableObject
{
    [Header("Chest")]
    [SerializeField] private ChestType type;
    [SerializeField] private new string name;

    [Min(1)]
    [SerializeField] private int level;
    [SerializeField] private float duration;

    [Header("Reward")]
    [SerializeReference] private RewardConfig[] possibleReward;

    public string Name => this.name;
    public float Duration => this.duration;
    public ChestType Type => this.type;
    

    public RewardConfig GetRewardConfig()
    {
        var index = Random.Range(0, this.possibleReward.Length);
        possibleReward[index].SetLevel(this.level);
        return this.possibleReward[index];
    }

    [Button]
    public void SaveChanges()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
        Debug.Log("Changes saved!");
#endif
    }
}