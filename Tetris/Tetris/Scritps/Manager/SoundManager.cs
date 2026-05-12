using NAudio.Wave;

public static class SoundManager
{
    public static void PlayCollect() => PlaySound("Sound/collect.mp3");
    public static void PlayDeath()   => PlaySound("Sound/death.mp3");
    public static void PlayPutPiece() => PlaySound("Sound/put_piece.mp3");
    public static void PlayMain()    => PlaySound("Sound/Tetris_main.mp3",true);

    private static void PlaySound(string path, bool loop = false) =>
        Task.Run(() =>
        {
            using var reader = new AudioFileReader(path);
            using var output = new WaveOutEvent();
            output.Init(reader);
            output.Play();

            if (loop)
            {
                output.PlaybackStopped += (sender, args) =>
                {
                    reader.Seek(0, SeekOrigin.Begin);
                    return;
                };
            }

            while (output.PlaybackState == PlaybackState.Playing)
                Thread.Sleep(50);
        });
}