using UnityEngine;
using System.Collections;

/* 
 * Usage:
 	* Add to GameOject which wants to use a direct particle system
 	* Track particle system on public spot and define the desired speed
 	* To call script get the component using GetComponent, calculate the direction of the particles(destination position - current position).normalized
 	* GetComponent<DirectedParticleSystem>().createParticleSystem((rayhitwhat.point - transform.position).normalized);
 	* //EDIT: Now it calculates its direction. Just give him the target (Vector3);
 	* To end the particle flow call
 	* GetComponent<DirectedParticleSystem>().deactivate();
 */

public class ODirectedParticleSystem : MonoBehaviour {
	
	public ParticleEmitter part;
	private Vector3 _target;
	public float speed;
	public bool animate = false;
	
	public AnimationCurve particle_offset_horizontal = new AnimationCurve(new Keyframe[4]
	                                                           {new Keyframe(0,0), 
	                                                            new Keyframe(0.5f,0), 
	                                                            new Keyframe(0.8f,1),
																new Keyframe(1,0)});
	public AnimationCurve particle_offset_vertical = new AnimationCurve(new Keyframe[4]
	                                                           {new Keyframe(0,0), 
	                                                            new Keyframe(0.5f,0), 
	                                                            new Keyframe(0.8f,1),
																new Keyframe(1,0)});
	public AnimationCurve particle_offset_depth = new AnimationCurve(new Keyframe[4]
	                                                           {new Keyframe(0,0), 
	                                                            new Keyframe(0.5f,0), 
	                                                            new Keyframe(0.8f,1),
																new Keyframe(1,0)});
	void Start()
	{
		part.emit = false;
		_target = transform.position;
	}
	
	void LateUpdate(){

		if (animate){
			Particle[] particles = part.particles;
			for(int i = 0; i < particles.Length; i++){
				float factor = Mathf.Clamp01(particles[i].energy/particles[i].startEnergy);
				particles[i].position = Vector3.Lerp(_target,transform.position,factor) + (new Vector3(particle_offset_horizontal.Evaluate(factor), particle_offset_vertical.Evaluate(factor), particle_offset_depth.Evaluate(factor)))*factor;
			}
			part.particles = particles;
		}
	}
	public void createParticleSystem(Vector3 whereTo)
	{
		if(!animate){
			part.emit = true;
			part.worldVelocity = (whereTo - transform.position).normalized * speed;
		}
		else{
			part.emit = true;
			part.worldVelocity = Vector3.zero;
			_target = whereTo;
		}
	}
	
	public void deactivate()
	{
		part.emit = false;	
	}
}
