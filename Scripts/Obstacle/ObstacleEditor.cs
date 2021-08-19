using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Obstacle))]
[CanEditMultipleObjects]
public class ObstacleEditor : Editor
{
    SerializedProperty m_HealthProp;
    SerializedProperty m_SpriteProp;
    SerializedProperty data;
    SerializedProperty data_health;
    SerializedProperty data_sprite;

    bool healthWereSet = false;

    //SerializedObject spriteRenderer;
    SerializedObject serializedData;

    private void OnEnable()
    {
        m_HealthProp = serializedObject.FindProperty("_health");
        data = serializedObject.FindProperty("data");

        //var script = ((MonoBehaviour)target).gameObject.GetComponent<SpriteRenderer>();
        //spriteRenderer = new SerializedObject(script);
        //m_SpriteProp = spriteRenderer.FindProperty("m_Sprite");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //spriteRenderer.Update();

        //get data properties
        if (data.objectReferenceValue != null && serializedData == null)
        {
            serializedData = new SerializedObject(data.objectReferenceValue);
            healthWereSet = false;
        }            
        else if (data.objectReferenceValue == null)
            serializedData = null;
        else if (serializedData != null)
            serializedData.Update();

        //health
        if (serializedData != null)
        {
            if(data_health == null)
                data_health = serializedData.FindProperty("health");

            if (!healthWereSet)
            {
                m_HealthProp.intValue = data_health.intValue;
                healthWereSet = true;
            }                
        }
        else m_HealthProp.intValue = 0;

        EditorGUILayout.PropertyField(m_HealthProp, new GUIContent("Health"), GUILayout.Height(20));

        //data field(scriptable object)
        EditorGUILayout.PropertyField(data, new GUIContent("Assigned properties"), GUILayout.Height(20));

        serializedObject.ApplyModifiedProperties();
    }

}