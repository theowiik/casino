using Godot;
using System.Collections.Generic;
using System.Linq;

public sealed class Jukebox : Node, IInteractable, IHoverHintable
{
    private AudioStreamPlayer3D _musicPlayer;
    private float _resumeTime;
    private IList<string> _musicFiles;
    private int _currentMusicIndex;

    public override void _Ready()
    {
        _musicFiles = FileUtil.GetAllFiles("res://Assets/Audio/Music", new[] { "wav", "mp3" }).ToList().Shuffle();

        _musicPlayer = GetNode<AudioStreamPlayer3D>("MusicPlayer");

        var nextButton = GetNode<Button>("NextButton");
        nextButton.Connect(nameof(Button.ButtonPressed), this, nameof(OnNextPressed));
        nextButton.SetHint(new Hint("E", "Next song"));
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

    private void PlayPause()
    {
        if (_musicPlayer.Stream == null)
        {
            Play(_currentMusicIndex);
            return;
        }

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

    public void Interact(Player interactedBy)
    {
        PlayPause();
    }

    public Hint GetHint()
    {
        return new Hint("E", "Play/Pause");
    }
}
