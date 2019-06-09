using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

	private int health;
	private int healthMax;
	public event EventHandler onHealthChanged;

	public HealthSystem(int healthMax) {
		this.healthMax = healthMax;
		//this.health = helathMax;
	}

	public int getHealth() {
		return health;
	}

	public float getHealthPercent() {
		return (float) health / healthMax;
	}

	public void damage(int damageAmount) {
		health -= damageAmount;

		if (health < 0) {
			health = 0;
		}

		if (onHealthChanged != null) {
			onHealthChanged(this, EventArgs.Empty);
		}
	}
}
