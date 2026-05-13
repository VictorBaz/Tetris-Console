using NAudio.Wave;

public static class SoundManager
{
    private static IWavePlayer? _musicPlayer;
    private static AudioFileReader? _musicReader;

    public static void PlayCollect(int scoreMultiplier)
    {
        Task.Run(async () =>
        {
            for (int i = 0; i < scoreMultiplier; i++)
            {
                PlaySound("Sound/collect.mp3");
                await Task.Delay(100); 
            }
        });
    }

    public static void PlayDeath() => PlaySound("Sound/death.mp3");
    public static void PlayPutPiece() => PlaySound("Sound/put_piece.mp3");

    public static void PlayMain()
    {
        if (_musicPlayer != null) return;

        _musicReader = new AudioFileReader("Sound/Tetris_main.mp3");
        var loop = new LoopStream(_musicReader); 
        
        _musicPlayer = new WaveOutEvent();
        _musicPlayer.Init(loop);
        _musicPlayer.Play();
    }

    private static void PlaySound(string path)
    {
        Task.Run(() =>
        {
            using var output = new WaveOutEvent();
            using var reader = new AudioFileReader(path);
            output.Init(reader);
            output.Play();
            while (output.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(100);
            }
        });
    }
}

public class LoopStream(WaveStream sourceStream) : WaveStream
{
    public override WaveFormat WaveFormat => sourceStream.WaveFormat;
    public override long Length => sourceStream.Length;
    public override long Position
    {
        get => sourceStream.Position;
        set => sourceStream.Position = value;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int totalBytesRead = 0;
        while (totalBytesRead < count)
        {
            int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
            if (bytesRead == 0)
            {
                sourceStream.Position = 0;
            }
            totalBytesRead += bytesRead;
        }
        return totalBytesRead;
    }
}