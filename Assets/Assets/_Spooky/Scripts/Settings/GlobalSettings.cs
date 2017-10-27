using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Settings/GlobalSettings")]
public class GlobalSettings : ScriptableObject 
{
	public PlayerSettings playerSettings;
	
	[Serializable]
	public class PlayerSettings
	{
		public Player.Settings SpookySettings;
		public PlayerEnemyAutoDetect.Settings SpookyEnemyAutoDetectSettings;
		public PlayerMove.Settings SpookyMoveSettings;
		public PlayerPlantPointDetect.Settings SpookyPlantPointDetectSettings;
		public PlayerShoot.Settings SpookyShootSettings;
	}
}
