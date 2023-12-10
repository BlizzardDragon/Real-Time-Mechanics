using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class RewardConfig : ScriptableObject
{
    [SerializeField] private new string name;

    [PreviewField]
    [SerializeField] private Sprite icon;

    protected int level;

    public string Name => name;
    public Sprite Icon => icon;


    internal void SetLevel(int level) => this.level = level;

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