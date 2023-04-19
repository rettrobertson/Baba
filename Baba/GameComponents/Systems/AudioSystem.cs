using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Baba.GameComponents.Systems
{
    public class AudioSystem : System
    {
        SoundEffect push;
        SoundEffect move;
        SoundEffect hurt;
        SoundEffect win;
        SoundEffectInstance bgm;

        public void PlayHurt()
        {
            hurt.Play();
        }
        public void PlayMove()
        {
            move.Play();
        }
        public void PlayVictory()
        {
            win.Play();
        }
        public void PlayPush()
        {
            push.Play();
        }
        public void StartBGM()
        {
            bgm.IsLooped = true;
            bgm.Play();
        }
        public void StopBGM()
        {
            bgm.Stop();
        }

        public void LoadContent(ContentManager content)
        {
            push = content.Load<SoundEffect>("");
        }
    }
}
