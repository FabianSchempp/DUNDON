using UnityEngine;
using System.Collections;

public class EFlowMapShader : MonoBehaviour {
	
	Material m;
	float FlowMapOffset0;
	float FlowMapOffset1;
	public float flowSpeed = 1;
	
	void Start () {
		m = renderer.material;
		FlowMapOffset0 = 0;
		FlowMapOffset1 = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
       FlowMapOffset0 += flowSpeed * Time.deltaTime;
       FlowMapOffset1 += flowSpeed * Time.deltaTime;
	
       if (FlowMapOffset0 >= 1) FlowMapOffset0 = 0.0f;
       if (FlowMapOffset1 >= 1 ) FlowMapOffset1 = 0.0f;
		
		m.SetFloat("FlowMapOffset0", FlowMapOffset0);
		m.SetFloat("FlowMapOffset1", FlowMapOffset1);
	}
}
