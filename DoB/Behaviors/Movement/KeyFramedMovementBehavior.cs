using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using DoB.Xaml;
using Microsoft.Xna.Framework;
using DoB.GameObjects;
using System.Diagnostics;
using DoB.Utility;

namespace DoB.Behaviors
{
    public class KeyFrame
    {
        public double DurationMs { get; set; }
        public double HoldMs { get; set; }

		public double? FromR { get; set; } //typically you'll need this to avoid jumping at the first frame
		public double? ToR { get; set; }
        public double? ToX { get; set; }
        public double? ToY { get; set; }
		public double TranslateX { get; set; }
		public double TranslateY { get; set; }
    }

    [ContentProperty("KeyFrames")]
    public class KeyFramedMovementBehavior : Behavior
    {
        public List<KeyFrame> KeyFrames { get; set; }
        public int EasingPow { get; set; }
        
        int activeIndex = 0;
        float keyFrameStartX;
        float keyFrameStartY;
		float keyFrameStartR;
        Cooldown keyFrameDuration;
        Cooldown keyFrameHold;
        private bool isHoldingFrame;

        public KeyFramedMovementBehavior()
        {
            KeyFrames = new List<KeyFrame>();
            keyFrameDuration = new Cooldown();
            keyFrameHold = new Cooldown();
            EasingPow = 1;
        }

        public override void ResetTimers()
        {
            throw new NotSupportedException();
        }

        public override IPrototype Clone()
        {
            var b = (KeyFramedMovementBehavior)base.Clone();
            b.KeyFrames = KeyFrames.ToList();
            for (int i = 0; i < KeyFrames.Count; i++)
            {
                b.KeyFrames[i] = KeyFrames[i];
            }

            b.keyFrameDuration = new Cooldown();
            keyFrameHold = new Cooldown();
            b.activeIndex = 0;
            b.isHoldingFrame = false;

            return b;
        }

        public override void OnFirstUpdate(GameTime gameTime, GameObject gameObject)
        {
            base.OnFirstUpdate(gameTime, gameObject);
            KeyFrameStarted(KeyFrames[0], gameTime, gameObject);
        }

        public override void UpdateOverride(GameTime gameTime, GameObject gameObject)
        {
            base.UpdateOverride(gameTime, gameObject);

            if (activeIndex >= KeyFrames.Count)
                return;

            var akf = KeyFrames[activeIndex];

            keyFrameDuration.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            keyFrameHold.Update(gameTime.ElapsedGameTime.TotalMilliseconds);
            if (keyFrameDuration.IsElapsed )
            {
                if (!isHoldingFrame)
                {
                    isHoldingFrame = true;
                    keyFrameHold.Reset(akf.HoldMs);
                }
                else if (keyFrameHold.IsElapsed)
                {
                    activeIndex++;

                    if (activeIndex >= KeyFrames.Count)
                        return;

                    akf = KeyFrames[activeIndex];
                    KeyFrameStarted(akf, gameTime, gameObject);
                }

                return;
            }

            var amount = keyFrameDuration.Progress;

            var cx = MathHelper.Lerp(keyFrameStartX, (float)(akf.ToX ?? gameObject.Ox + akf.TranslateX), (float)Ease(amount));
            var cy = MathHelper.Lerp(keyFrameStartY, (float)(akf.ToY ?? gameObject.Oy + akf.TranslateY), (float)Ease(amount));
			if (akf.ToR.HasValue || akf.FromR.HasValue)
				gameObject.R = MathHelper.Lerp((float)(akf.FromR ?? keyFrameStartR), (float)(akf.ToR ?? keyFrameStartR), (float)Ease(amount));

            gameObject.MoveX += cx - gameObject.X;
            gameObject.MoveY += cy - gameObject.Y;
        }

        private void KeyFrameStarted(KeyFrame akf, GameTime gameTime, GameObject gameObject)
        {
            isHoldingFrame = false;
            keyFrameDuration.Reset(akf.DurationMs);
            keyFrameStartX = (float)gameObject.X;
            keyFrameStartY = (float)gameObject.Y;
			keyFrameStartR = (float)gameObject.R;
        }

        private double Ease(double amount)
        {
            return (amount <= 0.5) ? EaseIn(amount * 2) / 2 : ((1 - EaseIn((1 - amount) * 2)) / 2 + 0.5);
        }

        private double EaseIn(double amount)
        {
            return Math.Pow(amount, EasingPow);
        }
    }
}
