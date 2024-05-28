using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;

namespace StorybrewScripts
{
    public class Foundation : StoryboardObjectGenerator
    {
        public override void Generate() {

		    Intro();
            Build1();
            Kiai1();
            Bridge1();
            Couplet1();
            Break3();
            Build2();
            Kiai2();
        }
        public void Intro() {
            Feather(167, 22310, 320, 0);
        }
        public void Feather(int startTime, int endTime, float startX, float startY) {

            StoryboardLayer layer = GetLayer("Feather");
            OsbSprite feather = layer.CreateSprite("sb/feather.png");

            double amplitude = 50; // how far on the side the feather goes
            double frequency = 0.05; // the speed of the feather from left to right
            int delayRot = 30;
            int currentTime = 0;

            feather.Scale(startTime, 0.15);
            feather.Fade(startTime, startTime+1400, 0, 1);
            feather.Fade(endTime-1400, endTime, 1, 0);

            while (currentTime < endTime) {

                float currentY = (float)(startY + 0.9);
                float currentX = (float)(320 + amplitude*Math.Sin(currentY*frequency)); // could have done a Move of the X position with sine easing you retard
                currentTime = startTime + 50;
                feather.Move(startTime, currentTime, new Vector2(startX, startY), new Vector2(currentX, currentY));
                if (currentX-startX<0) {feather.Rotate(startTime+delayRot, currentTime+delayRot, feather.RotationAt(startTime), feather.RotationAt(startTime)+0.01);}
                else {feather.Rotate(startTime+delayRot, currentTime+delayRot, feather.RotationAt(startTime), feather.RotationAt(startTime)-0.01);}
                startY = currentY;
                startX = currentX;
                startTime = currentTime;
            }

            foreach (int time in new List<int>{167, 5882, 11596, 17310, 20167}) {Ripple(feather, time);}
        }
        public void Ripple(OsbSprite feather, int startTime) {

            StoryboardLayer layer = GetLayer("Ripple");
            OsbSprite ripple = layer.CreateSprite("sb/ring.png");

            ripple.Scale(OsbEasing.OutQuad, startTime, startTime+1500, 0, 1);
            ripple.Move(startTime, feather.PositionAt(startTime));
            ripple.Fade(startTime, startTime+1500, 1, 0);
        }
        public void Build1() {

            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 900.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Build1");
            OsbSprite blur = layer.CreateSprite("sb/blur.jpg");

            blur.Scale(23025, scale);
            blur.Fade(OsbEasing.OutQuad, 45882, 45882+400, 1, 0);

            PositionCalculus(blur, 23025, 45882+400, 2500, 15);

            Flash(23025, 23739, 0.15);
            Flash(28739, 29453, 0.15);
            Flash(34453, 35167, 0.15);
            Flash(40167, 40882, 0.15);

            foreach (int time in new List<int> { 29453, 30882, 32310, 33739, 35167, 36596, 38025, 39453, 40882, 42310, 43739, 45167 } ) {
                Square(time);
            }
        }
        public void Flash(int startTime, int endTime, double opacity) {

            StoryboardLayer layer = GetLayer("Flash");
            OsbSprite flash = layer.CreateSprite("sb/p.png");

            flash.Additive(startTime, endTime);
            flash.ScaleVec(startTime, new Vector2(854, 480));
            flash.Fade(OsbEasing.OutQuad, startTime, endTime, opacity, 0);
        }
        public void Square(int startTime) {

            StoryboardLayer layer = GetLayer("Square");
            OsbSprite square = layer.CreateSprite("sb/square.png");

            int rotation = Random(-1, 2);
            while (rotation == 0) {
                rotation = Random(-1, 2);
            }

            square.Color(OsbEasing.OutQuad, startTime, startTime+600, Color4.White, new Color4(255, 191, 128,255));
            square.Scale(OsbEasing.OutQuad, startTime, startTime+600, 0.01, 0.15);
            square.Rotate(OsbEasing.OutQuad, startTime, startTime+600, Math.PI/4*rotation, Math.PI/3*2*rotation);
            square.Fade(OsbEasing.OutSine, startTime+400, startTime+600, 1, 0);

            foreach (var hitobject in Beatmap.HitObjects) {

                if (Math.Abs(hitobject.StartTime-startTime)<5) { square.Move(startTime, hitobject.Position); }
            }
        }
        public void Kiai1() {

            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 900.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Kiai1");
            OsbSprite bg = layer.CreateSprite("1gd6J30eSqQ.jpg");
            
            bg.Scale(51596, scale);
            bg.Fade(OsbEasing.OutQuad, 74453, 74453+400, 1, 0);

            PositionCalculus(bg, 51596, 74453+400, 2500, 15);
            
            Flash(51596, 52310, 0.4);
            Flash(63025, 63739, 0.4);

            foreach (int time in new List<int> { 51596, 54453, 57310, 60167, 63025, 65882, 68739, 71596 } ) {

                Square2(time);
            }

            int endTime = 63025;
            foreach (int time in new List<int> { 61596, 61775, 61953, 62132, 62310, 62489, 62667, 62846 } ) {
                
                Square3(time, endTime);
            }
        }
        public void Square3(int startTime, int endTime) {

            StoryboardLayer layer = GetLayer("Square3");
            OsbSprite square = layer.CreateSprite("sb/square.png");

            int rotation = Random(-1, 2);
            while (rotation == 0) {
                rotation = Random(-1, 2);
            }
            double scale = 0.09;

            square.Color(OsbEasing.OutQuad, startTime, endTime+200, Color4.White, new Color4(255, 191, 128,255));
            square.Scale(OsbEasing.OutQuad, startTime, startTime+100, 0.01, scale);
            square.Rotate(OsbEasing.OutExpo, startTime, endTime, Random(Math.PI/3*rotation, Math.PI/5*rotation), Random(Math.PI/2*2*rotation, Math.PI/4*2*rotation));
            square.Fade(OsbEasing.OutSine, endTime, endTime+200, 1, 0);
            square.Rotate(OsbEasing.OutQuad, endTime, endTime+200, square.RotationAt(endTime), square.RotationAt(endTime)+Math.PI/2*rotation*-1);
            square.Scale(OsbEasing.OutQuad, endTime, endTime+200, scale, 0.01);

            foreach (var hitobject in Beatmap.HitObjects) {

                if (Math.Abs(hitobject.StartTime-startTime)<5) { square.Move(startTime, hitobject.Position); }
            }
        }
        public void PositionCalculus(OsbSprite element, int startTime, int endTime, int shakeAmount, int Radius) {

            var angleCurrent = 0d;
            var radiusCurrent = 0;
            
            for (int i = startTime; i < endTime - shakeAmount; i += shakeAmount) {

                var angle = Random(angleCurrent - Math.PI / 4, angleCurrent + Math.PI / 4);
                var radius = Math.Abs(Random(radiusCurrent - Radius / 4, radiusCurrent + Radius /4));

                while (radius > Radius) {

                    radius = Math.Abs(Random(radiusCurrent - Radius / 4, radiusCurrent + Radius /4));
                }

                var start = element.PositionAt(i);
                var end = CirclePos(angle, radius);

                if (i + shakeAmount >= endTime) { element.Move(OsbEasing.InOutSine, i, endTime, start, end); }
                else { element.Move(OsbEasing.InOutSine, i, i+shakeAmount, start, end); }

                angleCurrent = angle;
                radiusCurrent = radius;
            }
        }
        public Vector2 CirclePos(double angle, int radius) {

            double posX = 320 + (radius * Math.Cos(angle));
            double posY = 240 + ((radius * 5) * Math.Sin(angle));

            return new Vector2((float)posX, (float)posY);
        }
        public void Square2(int startTime) {

            StoryboardLayer layer = GetLayer("Square2");
            OsbSprite square1 = layer.CreateSprite("sb/square.png");
            OsbSprite square2 = layer.CreateSprite("sb/square.png");

            int delay = 800;
            int rotation = Random(-1, 2);
            while (rotation == 0) {

                rotation = Random(-1, 2);
            }

            square1.Color(OsbEasing.OutQuad, startTime, startTime+delay, Color4.White, new Color4(255, 191, 128,255));
            square1.Scale(OsbEasing.OutQuad, startTime, startTime+delay-200, 0.01, 0.15);
            square1.Rotate(OsbEasing.OutQuad, startTime, startTime+delay-200, Math.PI/4*rotation, Math.PI/3*2*rotation);
            square1.Fade(OsbEasing.OutSine, startTime+delay-200, startTime+delay, 1, 0);
            square1.Scale(OsbEasing.InQuad, startTime+delay-200, startTime+delay, 0.15, 0);

            square2.Color(OsbEasing.OutQuad, startTime, startTime+delay, Color4.White, new Color4(255, 191, 128,255));
            square2.Scale(OsbEasing.OutQuad, startTime, startTime+delay-200, 0.01, 0.25);
            square2.Rotate(OsbEasing.OutQuad, startTime, startTime+delay-200, Math.PI/4*rotation*-1, Math.PI/3*2*rotation*-1);
            square2.Fade(OsbEasing.OutSine, startTime+delay-200, startTime+delay, 1, 0);
            square2.Scale(OsbEasing.InQuad, startTime+delay-200, startTime+delay, 0.25, 0);

            foreach (var hitobject in Beatmap.HitObjects) {

                if (Math.Abs(hitobject.StartTime-startTime)<5) { 

                    square1.Move(startTime, hitobject.Position);
                    square2.Move(startTime, hitobject.Position);  
                }
            }
        }
        public void Bridge1() {

            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 900.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Bridge1");
            OsbSprite bg = layer.CreateSprite("1gd6J30eSqQ.jpg");
            OsbSprite vignette = layer.CreateSprite("sb/vignette.png");

            bg.Scale(77310, scale);
            Pulse(bg, 77310, 16, 0.1);

            vignette.ScaleVec(77310, 0.54, 0.4);
            vignette.Fade(77310, 1);
            vignette.Fade(123025+100, 123025+200, 1, 0);
        }
        public void Pulse(OsbSprite sprite, int startTime, int loopCount, double opacity) {

            sprite.StartLoopGroup(startTime, loopCount);
            sprite.Fade(OsbEasing.InQuad, 0, 715/2, 0, opacity);
            sprite.Fade(OsbEasing.OutQuad, 715/2, 715, opacity, 0);
            sprite.EndGroup();
        }
        public void Couplet1() {

            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 900.0f / bitmap.Width;
            double scale2 = 900.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Couplet1");
            OsbSprite blur = layer.CreateSprite("sb/blur.jpg");
            OsbSprite bg = layer.CreateSprite("1gd6J30eSqQ.jpg");
            
            blur.Scale(88739, scale);
            blur.Fade(88739, 91596, 0, 0.5);
            blur.Fade(OsbEasing.InExpo, 99453+11, 100167, 0.5, 0);

            bg.Scale(88739, scale);
            bg.Additive(88739, 100167);
            Pulse(bg, 88739, 15, 0.2);
            bg.Fade(OsbEasing.InExpo, 99453+11, 100167, 0, 1);
            bg.Fade(123025, 123025+100, 1, 0);
            bg.Scale(100167, scale2);
            PositionCalculus(bg, 100167, 123025+100, 2500, 15);
        }
        public void Break3() {

            StoryboardLayer layer = GetLayer("Break3");

            int startTime = 125882;
            int endTime = 128560;
            int FadeDuration = 200;

            foreach (var hitobject in Beatmap.HitObjects) {

                if ((startTime != 0 || endTime != 0) &&
                    (hitobject.StartTime < startTime - 5 || endTime - 5 <= hitobject.StartTime))
                    continue;

                var stackOffset = hitobject.StackOffset;

                OsbSprite ripple = layer.CreateSprite("sb/ring.png", OsbOrigin.Centre, hitobject.Position + stackOffset);
                ripple.Scale(hitobject.StartTime, hitobject.EndTime + FadeDuration, 0.01, 0.2);
                ripple.Fade(OsbEasing.In, hitobject.EndTime, hitobject.EndTime + FadeDuration, 1, 0);
                ripple.Additive(hitobject.StartTime, hitobject.EndTime + FadeDuration);
                ripple.Color(hitobject.StartTime, hitobject.Color);
            }
        }
        public void Build2() {
            
            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 854.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Build2");
            OsbSprite blur = layer.CreateSprite("sb/blur.jpg");
            OsbSprite vignette = layer.CreateSprite("sb/vignette.png");

            blur.Scale(128739, scale);
            blur.Fade(128739, 1);
            blur.Fade(140167, 0);

            int startTime = 128739;
            int endTime = 140167;
            int delay = 2000;
            while (startTime<endTime) {
                Feather2(startTime, endTime+1400, Random(0,640), Random(-10, 240));
                startTime += Random(delay-200, delay+200);
            }
                
            vignette.ScaleVec(77310, 0.54, 0.4);
            vignette.Fade(128739, 1);
            vignette.Fade(140167, 0);   

            Flash(128739, 129453, 0.1);
            
        }
        public void Feather2(int startTime, int endTime, float startX, float startY) {

            StoryboardLayer layer = GetLayer("Feather2");
            OsbSprite feather = layer.CreateSprite("sb/feather.png");

            double amplitude = 50; // how far on the side the feather goes
            double frequency = 0.05; // the speed of the feather from left to right
            int delayRot = 30;
            int currentTime = 0;

            feather.Scale(startTime, 0.15);
            feather.Fade(startTime, startTime+1400, 0, 1);
            feather.Fade(endTime-1400, endTime, 1, 0);
            
            float startStartX = startX;
            while (currentTime < endTime) {
                
                float currentY = (float)(startY + 0.9);
                float currentX = (float)(startStartX + amplitude*Math.Sin(currentY*frequency)); // could have done a Move of the X position with sine easing you retard
                currentTime = startTime + 50;
                feather.Move(startTime, currentTime, new Vector2(startX, startY), new Vector2(currentX, currentY));
                if (currentX-startX<0) {feather.Rotate(startTime+delayRot, currentTime+delayRot, feather.RotationAt(startTime), feather.RotationAt(startTime)+0.01);}
                else {feather.Rotate(startTime+delayRot, currentTime+delayRot, feather.RotationAt(startTime), feather.RotationAt(startTime)-0.01);}
                startY = currentY;
                startX = currentX;
                startTime = currentTime;
            }
        }
        public void Kiai2() {

            Bitmap bitmap = GetMapsetBitmap("1gd6J30eSqQ.jpg");
            double scale = 1000.0f / bitmap.Width;
            StoryboardLayer layer = GetLayer("Kiai2");
            OsbSprite bg = layer.CreateSprite("1gd6J30eSqQ.jpg");
            
            bg.Scale(140167, scale);
            bg.Fade(OsbEasing.OutQuad, 185882, 185882+400, 1, 0);

            PositionCalculus(bg, 140167, 185882+400, 2500, 15);
            
            Flash(140167, 140882, 0.4);
            Flash(151596, 152310, 0.4);
            Flash(163025, 163739, 0.4);
            Flash(174453, 175167, 0.4);

            foreach (int time in new List<int> { 140167, 143025, 145882, 148739, 151596, 154453, 157310, 160167, 163025, 165882, 168739, 171596, 174453, 177310, 180167, 183025 } ) {

                Square2(time);
            }

            int endTime = 151596;
            foreach (int time in new List<int> { 150167, 150346, 150525, 150703, 150882, 151060, 151239, 151417 } ) {
                
                Square3(time, endTime);
            }
        }
    }
}
