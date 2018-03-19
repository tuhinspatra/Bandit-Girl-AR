using UnityEngine;
using UnityEditor;
using System.Collections;
 
 
public class TurnOffRenderers : ScriptableObject
{
 
    [MenuItem ("ASCL/TurnOffRenderers")]
    static void MenuDumpModels()
    {
        GameObject go = Selection.activeGameObject;
		
		Transform[] tms = go.transform.GetComponentsInChildren<Transform>();
		
		foreach(Transform tm in tms)
		{
			if(tm.gameObject.GetComponent<Renderer>()!=null)
			{
				tm.gameObject.GetComponent<Renderer>().enabled = false;				
			}
		}
    }
 
}