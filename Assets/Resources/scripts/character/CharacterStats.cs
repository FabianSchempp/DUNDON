using UnityEngine;
using System.Collections;

public class CharacterStats{
	private float _healthCurrent= 5;
	private float _healthMax = 5;
	
	private float _enduranceCurrent = 5;
	private float _enduranceMax = 5;
	
	private float _enduranceRegenerationRate = 0.1f;
	private float _healthRegenerationRate = 0.1f;
	
	private bool _invincible = false;
	private float _durationInvincible = 0;
	private float _startInvincible = 0;
	
	private bool _enduranceRegeneration = true;
	
	public float healthCurrent{
		get {return _healthCurrent;}
		set {_healthCurrent = value;}
	}
	
	public float enduranceRegenerationRate{
		get {return _enduranceRegenerationRate;}
		set {_enduranceRegenerationRate = value;}
	}
	public float healthRegenerationRate{
		get {return _healthRegenerationRate;}
		set {_healthRegenerationRate = value;}
	}
	
	public float healthMax{
		get {return _healthMax;}
		set {_healthMax = value;}
	}
	public float healthFactor{
		get {return (healthCurrent/healthMax);}
	}
	
	public float enduranceCurrent{
		get {return _enduranceCurrent;}
		set {_enduranceCurrent = value;}
	}
	
	public float enduranceMax{
		get {return _enduranceMax;}
		set {_enduranceMax = value;}
	}
	public float enduranceFactor{
		get {return (_enduranceCurrent/_enduranceMax);}
	}
	
	public bool invincile{
		get {return _invincible;}
		set {_invincible = value;}
	}
	
	public CharacterStats(float HealthMax){
		healthMax = HealthMax;
		healthCurrent = HealthMax;
	}
	
	public bool reduceHealth(float subtractHealth){
		if(!_invincible){
			healthCurrent -= subtractHealth;
			if(healthCurrent > 0){
				return true;
			}
			else{
				return false;
			}
		}
		return true;
	}
	public bool reduceHealthImpact(float subtractHealth){
		if(!_invincible){
			healthCurrent -= subtractHealth;
			setInvincible(1.0f,true);
			if(healthCurrent > 0){
				return true;
			}
			else{
				return false;
			}
		}
		return true;
	}
	
	public bool reduceEndurance(float subtractEndurance){
			if(_enduranceCurrent > subtractEndurance){
				_enduranceCurrent -= subtractEndurance;
				return true;
			}
			else{
				return false;
			}
	}
	
	public void regenerateHealth(){
		healthCurrent =  Mathf.Clamp(healthCurrent + healthRegenerationRate * Time.deltaTime,0,healthMax);
	}
	
	public void regenerateEndurance(){
		if(_enduranceRegeneration){
			enduranceCurrent =  Mathf.Clamp(enduranceCurrent + enduranceRegenerationRate * Time.deltaTime,0,enduranceMax);
		}
	}
	
	public void addEndurance(float addEndurance){
		enduranceCurrent =  Mathf.Min(enduranceMax,enduranceCurrent + addEndurance);
	}
	
	public void addHealth(float addHealth){
		healthCurrent =  Mathf.Min(healthMax,healthCurrent + addHealth);
	}
	public void setInvincible(float duration, bool t){
		_startInvincible = Time.timeSinceLevelLoad;
		_durationInvincible = duration <= 0? 1000000 : duration;
		_invincible = t;
	}
	public void updateStats(){
		if (Time.timeSinceLevelLoad - _startInvincible > _durationInvincible){
			_invincible = false;
		}
		regenerateHealth();
		regenerateEndurance();
	}
}
