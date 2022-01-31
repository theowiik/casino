using Godot;
using System.Collections.Generic;
using System.Linq;

public sealed class Jukebox : Node
{
    private AudioStreamPlayer3D _musicPlayer;
    private float _resumeTime;
    private IList<string> _musicFiles;
    private int _currentMusicIndex;

    public override void _Ready()
    {
        _musicFiles = FileUtil.GetAllFiles("res://Assets/Audio/Music", new[] { "wav", "mp3" }).ToList();
        _musicPlayer = GetNode<AudioStreamPlayer3D>("MusicPlayer");
        var nextButton = GetNode<Button>("NextButton");
        nextButton.Connect(nameof(Button.ButtonPressed), this, nameof(OnNextPressed));
    }

    private void OnNextPressed()
    {
        _currentMusicIndex++;

        if (_currentMusicIndex >= _musicFiles.Count)
            _currentMusicIndex = 0;

        Play(_currentMusicIndex);
    }

    private void Play(int songIndex)
    {
        if (songIndex < 0 || songIndex >= _musicFiles.Count) return;

        _musicPlayer.Stream = ResourceLoader.Load<AudioStream>(_musicFiles[songIndex]);
        _musicPlayer.Play();
        _resumeTime = 0f;
    }

    public void PlayPause()
    {
        if (_musicPlayer.Playing)
        {
            _resumeTime = _musicPlayer.GetPlaybackPosition();
            _musicPlayer.Stop();
        }
        else
        {
            _musicPlayer.Play(_resumeTime);
        }
    }


}
