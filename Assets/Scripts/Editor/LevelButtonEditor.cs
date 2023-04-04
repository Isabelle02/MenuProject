#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(LevelButton))]
public class LevelButtonEditor : SoundButtonEditor
{
    private SerializedProperty _locked;
    private SerializedProperty _unlocked;
    private SerializedProperty _passed;
    private SerializedProperty _comingSoon;
    private SerializedProperty _levelNumberText;
    private SerializedProperty _starsLayout;

    protected override void OnEnable()
    {
        base.OnEnable();
        _locked = serializedObject.FindProperty("_locked");
        _unlocked = serializedObject.FindProperty("_unlocked");
        _passed = serializedObject.FindProperty("_passed");
        _comingSoon = serializedObject.FindProperty("_comingSoon");
        _levelNumberText = serializedObject.FindProperty("_levelNumberText");
        _starsLayout = serializedObject.FindProperty("_starsLayout");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_locked);
        EditorGUILayout.PropertyField(_unlocked);
        EditorGUILayout.PropertyField(_passed);
        EditorGUILayout.PropertyField(_comingSoon);
        EditorGUILayout.PropertyField(_levelNumberText);
        EditorGUILayout.PropertyField(_starsLayout);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif