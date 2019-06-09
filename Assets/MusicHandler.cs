
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum SoundType 
{
    AXE_SOUND,
	RED_ENEMY_ATTACK,
	SPEAR_THROW
}
public class MusicHandler: MonoBehaviour
{
    
    public AudioSource m_AudioSource;
    public static AudioSource s_AudioSource;
    public AudioClip m_AxeSound;
	public AudioClip m_RedEnemyAttack;
    public static AudioClip s_AxeSound;
	public AudioClip m_SpearThrow;
	public static AudioClip s_SpearThrow;
	public static AudioClip s_RedEnemyAttack;



    

    void Start()
    {

		m_AudioSource = GetComponent<AudioSource>();
        s_AudioSource = m_AudioSource;
        s_AxeSound = m_AxeSound;
		s_RedEnemyAttack = m_RedEnemyAttack;
		s_SpearThrow = m_SpearThrow;
    }
    void Update()
    {

    }
    public static void PlaySound(SoundType sound) {
        if (sound == SoundType.AXE_SOUND)
        {
            s_AudioSource.PlayOneShot(MusicHandler.s_AxeSound);
        }
		else if (sound == SoundType.RED_ENEMY_ATTACK)
		{
			s_AudioSource.PlayOneShot(MusicHandler.s_RedEnemyAttack);
		}
		else if (sound == SoundType.SPEAR_THROW)
		{
			s_AudioSource.PlayOneShot(MusicHandler.s_SpearThrow);

		}

    }

}