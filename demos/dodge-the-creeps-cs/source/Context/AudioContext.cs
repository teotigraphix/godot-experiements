using System.Collections.Generic;
using Godot;

namespace Teoti.Context;

public partial class AudioContext : GodotObject
{
    public enum SFXType
    {
        Music,
        Death
    }

    //-----------------------------------------------------------------------------
    // Private :: Variables
    //-----------------------------------------------------------------------------

    private readonly AudioStreamPlayer _musicStream;
    private readonly AudioStreamPlayer _deathStream;
    private Dictionary<SFXType, AudioStreamPlayer> map = new();

    //-----------------------------------------------------------------------------
    // Constructors
    //-----------------------------------------------------------------------------

    public AudioContext(
        AudioStreamPlayer musicStream,
        AudioStreamPlayer deathStream)
    {
        _musicStream = musicStream;
        _deathStream = deathStream;

        AddSFX(SFXType.Music, _musicStream);
        AddSFX(SFXType.Death, _deathStream);
    }
    
    //-----------------------------------------------------------------------------
    // API :: Methods
    //-----------------------------------------------------------------------------

    public void PlaySFX(SFXType sfxType, bool playSfx)
    {
        AudioStreamPlayer sfxStream = map.GetValueOrDefault(sfxType);

        if (playSfx)
        {
            sfxStream?.Play();
        }
        else
        {
            sfxStream?.Stop();
        }
    }
    
    private void AddSFX(SFXType sfxType, AudioStreamPlayer stream)
    {
        map.Add(sfxType, stream);
    }
}