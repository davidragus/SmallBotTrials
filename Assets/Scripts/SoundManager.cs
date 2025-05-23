using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType
{
	BuildUpDeath,
	Explosion,
	Death,
	ShootArms,
	RetrieveArms,
	HardHit,
	MediumHit,
	SoftHit,
	GrabPowerUp
}

[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{

	[SerializeField] private SoundList[] soundList;
	private static SoundManager instance;
	private AudioSource audioSource;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public static float MasterVolume = 0.5f;
	public static void PlaySound(SoundType sound, float volume = 1f, float pitch = 1f)
	{
		AudioClip[] clips = instance.soundList[(int)sound].Sounds;
		AudioClip randClip = clips[UnityEngine.Random.Range(0, clips.Length)];
		instance.audioSource.pitch = pitch;
		instance.audioSource.PlayOneShot(randClip, volume * MasterVolume);
	}

#if UNITY_EDITOR
	private void OnEnable()
	{
		string[] names = Enum.GetNames(typeof(SoundType));
		Array.Resize(ref soundList, names.Length);
		for (int i = 0; i < soundList.Length; i++)
		{
			soundList[i].name = names[i];
		}
	}
#endif
}

[Serializable]
public struct SoundList
{
	public AudioClip[] Sounds
	{
		get => sounds;
	}
	[HideInInspector]
	public string name;
	[SerializeField] private AudioClip[] sounds;
}
